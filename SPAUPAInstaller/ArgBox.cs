namespace SPAUPAInstaller;

public class ArgBox : InstallerElement
{
    public string Title { get; set; }
    public string Value { get; set; }
    public string Code { get; set; }

    public override void ReturnInput(ref SIConsoleExecuteInfo info)
    {
        if (info.Args.ContainsKey(Code))
            info.Args.Remove(Code);

        info.Args.Add(Code, Value);
    }
}