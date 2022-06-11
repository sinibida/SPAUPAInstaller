namespace SIConsole;

public class SIJob
{
    public double Progress { get; protected set; }
    public string[] Tags { get; set; }

    public SIJob()
    {
        Progress = 0.0;
        Tags = Array.Empty<string>();
    }

    public virtual void Execute()
    {
    }
}