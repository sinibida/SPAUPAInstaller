using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SIConsole;

public class SIJSONLoader
{
    public Type[] JobTypes { get; set; }

    public SIJSONLoader() : this(Array.Empty<Type>())
    {

    }

    public SIJSONLoader(params Type[] jobTypes)
    {
        JobTypes = jobTypes;
    }

    public SIData Load(string json)
    {
        var converter = new SIJobConverter(JobTypes);
        var data = JsonConvert.DeserializeObject<SIData>(json, converter);

        return data ?? throw new JsonException("Jobs value is null");
    }

    public SIData Load(JsonReader json)
    {
        var converter = new SIJobConverter(JobTypes);
        var serializer = new JsonSerializer();
        serializer.Converters.Add(converter);
        var data = serializer.Deserialize<SIData>(json);

        return data ?? throw new JsonException("Jobs value is null");
    }
}