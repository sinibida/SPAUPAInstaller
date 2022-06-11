using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SIConsole;

public class SIJSONLoader
{
    private SIJob[] _jobs;

    public SIJSONLoader()
    {
        _jobs = Array.Empty<SIJob>();
    }

    public void Load(string json, params Type[] jobTypes)
    {
        var converter = new SIJobConverter(jobTypes);
        var jobs = JsonConvert.DeserializeObject<SIJob[]>(json, converter);

        _jobs = jobs ?? throw new JsonException("Jobs value is null");
    }

    public void Load(JsonReader json, params Type[] jobTypes)
    {
        var converter = new SIJobConverter(jobTypes);
        var serializer = new JsonSerializer();
        serializer.Converters.Add(converter);
        var jobs = serializer.Deserialize<SIJob[]>(json);

        _jobs = jobs ?? throw new JsonException("Jobs value is null");
    }

    public SIJob[] ToJobs()
    {
        return _jobs;
    }
}