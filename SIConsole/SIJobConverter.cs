﻿using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SIConsole;

public class SIJobConverter : JsonConverter<SIJob>
{
    public Dictionary<string, Type> JobTypeDict { get; set; }

    public SIJobConverter(params Type[] jobTypes)
    {
        JobTypeDict = new Dictionary<string, Type>();

        foreach (var jobType in jobTypes)
        {
            if (!jobType.IsSubclassOf(typeof(SIJob)))
                throw new ArgumentException($"{jobType.Name} is not SIJobs type.");

            var codeField = jobType.GetFields().FirstOrDefault(x => x.Name == "Code");
            if (codeField == null)
                throw new ArgumentException($"Code value of {jobType.Name} does not exist.");
            if (!codeField.IsStatic)
                throw new ArgumentException($"Code value of {jobType.Name} is not static.");
            if (codeField.FieldType != typeof(string))
                throw new ArgumentException($"Code value of {jobType.Name} is not string.");

            var code = (string?) codeField.GetValue(null);
            if (code == null)
                throw new ArgumentException($"Code value of {jobType.Name} is null.");

            JobTypeDict.Add(code, jobType);
        }
    }
    
    public override void WriteJson(JsonWriter writer, SIJob? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
    
    public override SIJob? ReadJson(JsonReader reader, Type objectType, SIJob? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        var obj = JObject.Load(reader);
        var typeToken = obj["type"];
        if (typeToken == null)
            throw new JsonException("'type' parameter must be specified.");
        else
        {
            var type = typeToken.ToObject<string>();
            if (type == null)
                throw new JsonException("Type value is null.");
            if (!JobTypeDict.ContainsKey(type))
                throw new JsonException($"Unexpected type {type}.");

            var targetType = JobTypeDict[type];

            var paramsToken = obj["params"];
            if (paramsToken == null)
                throw new JsonException("'params' parameter must be specified.");

            var job = (SIJob?) paramsToken.ToObject(targetType);
            if (job == null)
                throw new JsonException("Job instance is null.");
            
            return job;
        }
    }
}