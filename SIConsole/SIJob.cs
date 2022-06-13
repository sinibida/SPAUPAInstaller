using Newtonsoft.Json;

namespace SIConsole;

public class SIJobProgressEventArgs : EventArgs
{
    public SIJobProgressEventArgs(SIJob job, double progress)
    {
        Job = job;
        Progress = progress;
    }

    public SIJob Job { get; }
    public double Progress { get; }
}

public delegate void SIJobProgressEventHandler(object sender, SIJobProgressEventArgs arg);

public abstract class SIJob
{
    public event SIJobProgressEventHandler ProgressChanged;

    public double Progress
    {
        get => _progress;
        protected set
        {
            if (_progress != value)
            {
                _progress = value;
                ProgressChanged?.Invoke(this, new SIJobProgressEventArgs(this, _progress));
            }
        }
    }

    private double _progress;

    [JsonProperty(PropertyName = "tags")] public string[] TagFilters { get; set; }
    public string Name { get; set; }

    public SIJob()
    {
        Progress = 0.0;
        TagFilters = Array.Empty<string>();
        Name = "";
    }

    public abstract void Execute();
}