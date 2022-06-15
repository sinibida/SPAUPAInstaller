namespace SPAUPAInstaller.Element;

public abstract class InstallerElement : BaseNotifyPropertyChanged
{
    public abstract void ReturnInput(ref SIConsoleExecuteInfo info);
}