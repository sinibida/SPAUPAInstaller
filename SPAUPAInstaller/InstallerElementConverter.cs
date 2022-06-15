using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SPAUPAInstaller.Element;

namespace SPAUPAInstaller;

public class InstallerElementConverter : JsonConverter<InstallerElement>
{
    public Dictionary<string, Type> JobTypeDict { get; set; }

    public InstallerElementConverter(params Type[] jobTypes)
    {
        JobTypeDict = new Dictionary<string, Type>();

        foreach (var jobType in jobTypes)
        {
            if (!jobType.IsSubclassOf(typeof(InstallerElement)))
                throw new ArgumentException($"{jobType.Name} is not SIJobs type.");

            var attributes = jobType.GetCustomAttributes(typeof(InstallerElementInfoAttribute), false);
            if (attributes.Length == 0)
                throw new ArgumentException($"Code class needs InstallerElementInfoAttribute attribute.");
            
            var code = ((InstallerElementInfoAttribute)attributes[0]).Code;
            if (code == null)
                throw new ArgumentException($"Code value of {jobType.Name} is null.");

            JobTypeDict.Add(code, jobType);
        }
    }
    
    public override void WriteJson(JsonWriter writer, InstallerElement? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
    
    public override InstallerElement ReadJson(JsonReader reader, Type objectType, InstallerElement? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        var obj = JObject.Load(reader);
        var typeToken = obj["type"];
        if (typeToken == null)
            throw new JsonException("'type' parameter must be specified.");

        var type = typeToken.ToObject<string>();
        if (type == null)
            throw new JsonException("Type value is null.");
        if (!JobTypeDict.ContainsKey(type))
            throw new JsonException($"Unexpected type {type}.");

        var targetType = JobTypeDict[type];

        var paramsToken = obj["params"];
        if (paramsToken == null)
            return (InstallerElement)Activator.CreateInstance(targetType)!;

        var job = (InstallerElement?) paramsToken.ToObject(targetType);
        if (job == null)
            throw new JsonException("Job instance is null.");
            
        return job;
    }
}