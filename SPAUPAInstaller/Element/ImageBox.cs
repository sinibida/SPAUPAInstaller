using System;
using System.Windows.Media.Imaging;

namespace SPAUPAInstaller.Element;

[InstallerElementInfo(Code = "image_box")]
public class ImageBox : InstallerElement
{
    public string Path { get; set; }
    public BitmapImage? Image { get; set; }

    public void Load(string basePath)
    {
        Image = new BitmapImage(new Uri(System.IO.Path.Combine(basePath, Path)));
        Image.Freeze();
    }

    public override void ReturnInput(ref SIConsoleExecuteInfo info)
    {
    }
}