using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPAUPAInstaller.Element;

namespace SPAUPAInstaller.ViewModel
{
    class MainWindowViewModel : BaseNotifyPropertyChanged
    {
        private ObservableCollection<InstallerElement> _elements;

        public ObservableCollection<InstallerElement> Elements
        {
            get => _elements;
            set => SetProperty(ref _elements, value);
        }

        public LoadPathToArgCommand SelectFolder { get; set; } = new();

        public MainWindowViewModel()
        {
            _elements = new ObservableCollection<InstallerElement>();
        }
    }
}
