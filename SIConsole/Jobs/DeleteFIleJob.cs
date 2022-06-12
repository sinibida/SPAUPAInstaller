namespace SIConsole.Jobs;

[SIJobInfo(Code = "delete")]
public class DeleteFileJob : SIJob
{
    public string Filename { get; set; }

    public override void Execute()
    {
        var expFilename = Environment.ExpandEnvironmentVariables(Filename);
        if (File.Exists(Filename))
            File.Delete(expFilename);
        Progress = 1;
    }
}