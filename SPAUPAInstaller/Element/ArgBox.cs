namespace SPAUPAInstaller.Element;

[InstallerElementInfo(Code = "arg_box")]
public class ArgBox : InstallerElement
{
    private string _title;
    private string _value;
    private string _code;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Value
    {
        get => _value;
        set => SetProperty(ref _value, value);
    }

    public string Code
    {
        get => _code;
        set => SetProperty(ref _code, value);
    }

    public override void ReturnInput(ref SIConsoleExecuteInfo info)
    {
        if (info.Args.ContainsKey(Code))
            info.Args.Remove(Code);

        info.Args.Add(Code, Value);
    }
}