namespace SIConsole;

public class SIExecutorProgressEventArgs : EventArgs
{
    public SIExecutorProgressEventArgs(SIJobCollection jobs, SIJob currentJob, double totalProgress, double progress)
    {
        Jobs = jobs;
        CurrentJob = currentJob;
        TotalProgress = totalProgress;
        Progress = progress;
    }

    public SIJobCollection Jobs { get; }
    public SIJob CurrentJob { get; }
    public double TotalProgress { get; }
    public double Progress { get; }
}

public delegate void SIExecutorProgressEventHandler(object sender, SIExecutorProgressEventArgs arg);

public class SIJobExecutor
{
    public event SIExecutorProgressEventHandler ProgressChanged;

    public string? WorkingDirectory { get; set; }
    public string[] Tags { get; set; }
    public Dictionary<string, string> Args { get; set; }
    public SIJobCollection Jobs { get; set; }

    public class EnvironmentSetter : IDisposable
    {
        private readonly string? _workingDir;
        private readonly Dictionary<string, string> _args;
        private readonly string _dirPush;

        public EnvironmentSetter(string? workingDir, Dictionary<string, string> args)
        {
            _workingDir = workingDir;
            _args = args;
            if (_workingDir != null)
            {
                _dirPush = Directory.GetCurrentDirectory();
                Directory.SetCurrentDirectory(_workingDir);
            }
            foreach (var (key, value) in _args)
                Environment.SetEnvironmentVariable(key, value);
        }

        ~EnvironmentSetter()
        {
            ReleaseUnmanagedResources();
        }

        private void ReleaseUnmanagedResources()
        {
            foreach (var (key, _) in _args)
                Environment.SetEnvironmentVariable(key, null);
            if (_workingDir != null)
            {
                Directory.SetCurrentDirectory(_dirPush);
            }
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }
    }

    public SIJobExecutor() : this(new SIJobCollection())
    {
    }

    public SIJobExecutor(SIJobCollection jobs)
    {
        Jobs = jobs;
        Tags = Array.Empty<string>();
        Args = new Dictionary<string, string>();
        WorkingDirectory = null;
    }

    public void ExecuteAll()
    {
        using (var _ = new EnvironmentSetter(WorkingDirectory, Args))
        {
            var filteredJobs = Jobs.FilterTag(Tags);
            foreach (var job in filteredJobs)
            {
                job.ProgressChanged += Job_ProgressChanged; 
                job.Execute();
            }
        }
    }

    private void Job_ProgressChanged(object sender, SIJobProgressEventArgs arg)
    {
        ProgressChanged?.Invoke(this, new SIExecutorProgressEventArgs(Jobs, arg.Job, GetProgress(), arg.Progress));
    }

    public double GetProgress()
    {
        return Jobs.Average(x => x.Progress);
    }
}