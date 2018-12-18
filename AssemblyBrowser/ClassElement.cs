using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AssemblyBrowser 
{
    public class Element: INotifyPropertyChanged
    {
        private string name;
        private ObservableCollection<Element> classNodes;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public ObservableCollection<Element> ClassNodes
        {
            get
            {
                return classNodes;
            }

            set
            {
                classNodes = value;
                OnPropertyChanged("ClassNodes");
            }
        }

        public Element()
        {
            ClassNodes = new ObservableCollection<Element>();
            Name = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
