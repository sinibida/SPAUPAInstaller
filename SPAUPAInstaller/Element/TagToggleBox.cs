using System.Collections.ObjectModel;

namespace SPAUPAInstaller.Element;

[InstallerElementInfo(Code = "tag_box")]
public class TagToggleBox : InstallerElement
{
    private ObservableCollection<TagToggle> _toggles;

    public ObservableCollection<TagToggle> Toggles
    {
        get => _toggles;
        set => SetProperty(ref _toggles, value);
    }

    public override void ReturnInput(ref SIConsoleExecuteInfo info)
    {
        foreach (var toggle in Toggles)
        {
            if (!toggle.IsOn) continue;
            if (info.Tags.Contains(toggle.Code)) continue;

            info.Tags.Add(toggle.Code);
        }
    }
}