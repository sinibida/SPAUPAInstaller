using Newtonsoft.Json;

namespace SIConsole;

public class SIJob
{
    public double Progress { get; protected set; }
    [JsonProperty(PropertyName = "tags")]
    public string[] TagFilters { get; set; }

    public SIJob()
    {
        Progress = 0.0;
        TagFilters = Array.Empty<string>();
    }

    public virtual void Execute()
    {
    }
}