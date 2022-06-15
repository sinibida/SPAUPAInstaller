using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SPAUPAInstaller.Element;
using SPAUPAInstaller.ViewModel;

namespace SPAUPAInstaller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel _notifyPropertyChanged;

        public MainWindow()
        {
            InitializeComponent();

            _notifyPropertyChanged = (MainWindowViewModel) DataContext;
        }

        public void SetSource(List<InstallerElement> list)
        {
            _notifyPropertyChanged.Elements = new ObservableCollection<InstallerElement>(list);
        }
    }
}