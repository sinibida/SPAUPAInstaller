using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SPAUPAInstaller
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var loadingWindow = new LoadingWindow()
            {
                Title = "Test Loading...",
                LoadingTask = new SIProgressTask(TestLoading)
            };
            
            loadingWindow.ShowDialog();

            var window = new MainWindow();
            window.Show();
            
            Current.ShutdownMode = ShutdownMode.OnLastWindowClose;
        }

        private static IEnumerable<SIProgress> TestLoading()
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Thread.Sleep(100);
                    yield return new SIProgress(j / 5.0, (i * 5 + j) / 25.0, "Loading...");
                }
            }

            yield return new SIProgress(1, 1, "Done!");
        }
    }
}