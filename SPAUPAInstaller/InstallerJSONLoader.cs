using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SPAUPAInstaller;

public class InstallerJSONLoader
{
    public Type[] JobTypes { get; set; }

    public InstallerJSONLoader() : this(Array.Empty<Type>())
    {

    }

    public InstallerJSONLoader(params Type[] jobTypes)
    {
        JobTypes = jobTypes;
    }

    public InstallerData Load(string json)
    {
        var converter = new InstallerElementConverter(JobTypes);
        var data = JsonConvert.DeserializeObject<InstallerData>(json, converter);

        return data ?? throw new JsonException("Jobs value is null");
    }

    public InstallerData Load(JsonReader json)
    {
        var converter = new InstallerElementConverter(JobTypes);
        var serializer = new JsonSerializer();
        serializer.Converters.Add(converter);
        var data = serializer.Deserialize<InstallerData>(json);

        return data ?? throw new JsonException("Jobs value is null");
    }
}