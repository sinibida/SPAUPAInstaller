namespace SIConsole;

public class SIJobExecutor
{
    public string? WorkingDirectory { get; set; }
    public SIJob[] Jobs { get; set;  }

    public SIJobExecutor() : this(Array.Empty<SIJob>())
    {
        
    }

    public SIJobExecutor(SIJob[] jobs)
    {
        Jobs = jobs;
    }

    public void ExecuteAll()
    {
        var dirPush = string.Empty;
        if (WorkingDirectory != null)
        {
            dirPush = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(WorkingDirectory);
        }

        foreach (var job in Jobs)
        {
            job.Execute();
        }

        if (WorkingDirectory != null)
        {
            Directory.SetCurrentDirectory(dirPush);
        }
    }

    public double GetProgress()
    {
        return Jobs.Average(x => x.Progress);
    }
}