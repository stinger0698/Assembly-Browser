using System;
using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Collections.ObjectModel;
using AssemblyBrowser;

namespace AssemblyBrowser
{
    public class AssemblyReader : INotifyPropertyChanged
    {
        public OpenCommand OpenCommand { get; private set; }
        private ObservableCollection<Element> tree;
        private IBrowser assemblyLoader;

        public AssemblyReader(ObservableCollection<Element> tree, IBrowser browser)
        {
            this.tree = tree;
            assemblyLoader = browser;
            OpenCommand = new OpenCommand(OpenHandler);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void OpenHandler(object forFill)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "(*.dll)|*.dll";
            if (dialog.ShowDialog() == true)
            {
                tree.Clear();
                tree.Add(assemblyLoader.LoadAssembly(dialog.FileName));
                OnPropertyChanged();
            }
        }
    }
}
