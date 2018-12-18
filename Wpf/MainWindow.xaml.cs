using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using AssemblyBrowser;
using Microsoft.Win32;

namespace Wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Element> Tree { get; set; }
        private Microsoft.Win32.OpenFileDialog openFile;
        private Browser browser;

        public MainWindow()
        {
            InitializeComponent();

            Tree = new ObservableCollection<Element>();           
            MyTreeView.ItemsSource = Tree;
            DataContext = new AssemblyReader(Tree, new Browser());

        }             
    }
}
