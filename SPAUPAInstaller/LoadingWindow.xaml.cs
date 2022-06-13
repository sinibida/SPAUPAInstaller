using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SPAUPAInstaller
{
    /// <summary>
    /// LoadingWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoadingWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public double MainProgress { get; private set; }
        public double CurrentProgress { get; private set; }
        public string Message { get; private set; }

        public SIProgressTask LoadingTask;

        public LoadingWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void SetProgress(SIProgress progress)
        {
            MainProgress = progress.MainProgress;
            CurrentProgress = progress.CurrentProgress;
            Message = progress.Message;

            OnPropertyChanged(nameof(MainProgress));
            OnPropertyChanged(nameof(CurrentProgress));
            OnPropertyChanged(nameof(Message));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadingWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadingTask.ProgressChanged += (_, x) => SetProgress(x.Progress);
            LoadingTask.Done += (_, _) => Dispatcher.Invoke(Close);
            LoadingTask.Run();
        }
    }
}
