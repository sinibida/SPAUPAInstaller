namespace SIConsole.Jobs;

[SIJobInfo(Code = "copy")]
public class CopyFileJob : SIJob
{
    public string From { get; set; }
    public string To { get; set; }

    public override void Execute()
    {
        var expFrom = Environment.ExpandEnvironmentVariables(From);
        var expTo = Environment.ExpandEnvironmentVariables(To);
        Directory.CreateDirectory(Path.GetDirectoryName(expTo)!);
        File.Copy(expFrom, expTo, true);
        Progress = 1;
    }
}