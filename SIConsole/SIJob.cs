namespace SIConsole;

public class SIJob
{
    public double Progress { get; protected set; }

    public SIJob()
    {
        Progress = 0.0;
    }

    public virtual void Execute()
    {
    }
}