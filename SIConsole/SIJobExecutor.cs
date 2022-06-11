namespace SIConsole;

public class SIJobExecutor
{
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
        foreach (var job in Jobs)
        {
            job.Execute();
        }
    }

    public double GetProgress()
    {
        return Jobs.Average(x => x.Progress);
    }
}