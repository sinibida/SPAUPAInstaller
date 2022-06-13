using System;
using System.Collections.Generic;
using System.Threading;

namespace SPAUPAInstaller;

public class SIProgressEventArgs : EventArgs
{
    public SIProgressTask Task { get; private set; }
    public SIProgress Progress { get; private set; }

    public SIProgressEventArgs(SIProgressTask task, SIProgress progress)
    {
        Task = task;
        Progress = progress;
    }
}

public delegate void SIProgressEventHandler(object sender, SIProgressEventArgs e);

public delegate IEnumerable<SIProgress> SIProgressTaskStart();

public struct SIProgress
{
    public double MainProgress;
    public double CurrentProgress;
    public string Message;

    public SIProgress(double mainProgress, double currentProgress, string message)
    {
        MainProgress = mainProgress;
        CurrentProgress = currentProgress;
        Message = message;
    }
}

public class SIProgressTask
{
    public event SIProgressEventHandler ProgressChanged;
    public event EventHandler Done;

    public SIProgress Progress { get; private set; }
    public SIProgressTaskStart StartTask { get; set; }

    private Thread currentThread;

    public SIProgressTask()
    {

    }

    public SIProgressTask(SIProgressTaskStart startTask)
    {
        StartTask = startTask;
    }

    public void Run()
    {
        currentThread = new Thread(() =>
        {
            foreach (var progress in StartTask())
            {
                Progress = progress;
                ProgressChanged?.Invoke(this, new SIProgressEventArgs(this, progress));
            }
            Done?.Invoke(this, EventArgs.Empty);
        });
        currentThread.Start();
    }

    public void Join()
    {
        currentThread.Join();
    }
}