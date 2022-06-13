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

namespace SPAUPAInstaller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var list = new TagToggleBox();
            list.Toggles = new ObservableCollection<TagToggle>
            {
                new TagToggle {Code = "a", DisplayName = "주바나보", IsOn = false},
                new TagToggle {Code = "b", DisplayName = "어바찬보", IsOn = true}
            };
            ItemsControlMain.ItemsSource = new List<InstallerElement>
                {list, new ArgBox {Value = "카이사 ㅋㅋㅋㅋㅋ", Title = "자동차가 집을 옮기면?"}};
        }
    }
}