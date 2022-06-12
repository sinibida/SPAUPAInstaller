namespace SIConsole;

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