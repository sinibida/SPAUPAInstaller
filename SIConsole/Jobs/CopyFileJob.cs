namespace SIConsole.Jobs;

[SIJobInfo(Code = "copy")]
public class CopyFileJob : SIJob
{
    public string From { get; set; }
    public string To { get; set; }

    public override void Execute()
    {
        File.Copy(From, To);
    }
}