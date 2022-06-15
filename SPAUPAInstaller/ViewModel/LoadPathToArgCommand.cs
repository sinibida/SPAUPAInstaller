using System;
using System.IO;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using SPAUPAInstaller.Element;

namespace SPAUPAInstaller.ViewModel;

public class LoadPathToArgCommand : ICommand
{
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (parameter is not ArgBox box)
            throw new ArgumentException("Parameter must be ArgBox object");

        var dialog = new FolderBrowserDialog();
        if (Directory.Exists(box.Value))
            dialog.InitialDirectory = box.Value;
        if (dialog.ShowDialog() == DialogResult.OK)
            box.Value = dialog.SelectedPath;
    }

    public event EventHandler? CanExecuteChanged;
}