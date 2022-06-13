using System.Collections.ObjectModel;

namespace SPAUPAInstaller;

public class TagToggleBox : InstallerElement
{
    public ObservableCollection<TagToggle> Toggles { get; set; }

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