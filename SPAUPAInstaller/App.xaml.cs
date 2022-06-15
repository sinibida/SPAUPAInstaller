using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using SPAUPAInstaller.Element;

namespace SPAUPAInstaller
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly Type[] JobTypes =
        {
            typeof(ArgBox), typeof(TagToggleBox), typeof(ImageBox), typeof(SeparatorBox)
        };

        private InstallerData _data;

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var loadingWindow = new LoadingWindow
            {
                Title = "Loading...",
                LoadingTask = new SIProgressTask(LoadJson)
            };
            loadingWindow.ShowDialog();

            var window = new MainWindow();
            window.SetSource(_data.Elements);
            window.Show();
            
            Current.ShutdownMode = ShutdownMode.OnLastWindowClose;
        }

        private IEnumerable<SIProgress> LoadJson()
        {
            yield return new SIProgress(0, 0, "Loading Settings...");
            
            var loader = new InstallerJSONLoader(JobTypes);

            string path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule!.FileName)!;
            using (var sr = new StreamReader(Path.Combine(path, "data", "data.json")))
                _data = loader.Load(new JsonTextReader(sr));

            yield return new SIProgress(0.5, 0, "Loading Images..");

            for (var i = 0; i < _data.Elements.Count; i++)
            {
                var element = _data.Elements[i];
                if (element is ImageBox imageBox)
                {
                    imageBox.Load(Path.Combine(Environment.CurrentDirectory, "data"));
                }

                yield return new SIProgress(
                    0.5,
                    (double)(i + 1) / _data.Elements.Count,
                    "Loading Images..");
            }

            yield return new SIProgress(1, 1, "Done!");
        }
    }
}