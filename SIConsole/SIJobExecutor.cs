namespace SIConsole;

public class SIJobExecutor
{
    public string? WorkingDirectory { get; set; }
    public string[] Tags { get; set; }
    public SIJobCollection Jobs { get; set; }

    public SIJobExecutor() : this(new SIJobCollection())
    {
    }

    public SIJobExecutor(SIJobCollection jobs) : this(jobs, Array.Empty<string>())
    {
    }

    public SIJobExecutor(SIJobCollection jobs, string[] tags)
    {
        Jobs = jobs;
        Tags = tags;
        WorkingDirectory = null;
    }

    public void ExecuteAll()
    {
        var dirPush = string.Empty;
        if (WorkingDirectory != null)
        {
            dirPush = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(WorkingDirectory);
        }

        var filteredJobs = Jobs.FilterTag(Tags);
        foreach (var job in filteredJobs)
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