using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Collections.ObjectModel;

namespace AssemblyBrowser
{  
    namespace InnerTest
    {
        class Class2
        { }
    }
    public class Browser: IBrowser
    {
        private Element myAssembly;
        private Type[] types;

        public Element LoadAssembly(string path)
        {
            myAssembly = new Element();
            
            Assembly assembly = Assembly.Load(File.ReadAllBytes(path));
            myAssembly.Name = assembly.FullName;
            myAssembly.ClassNodes.Add(new Element() { Name = assembly.GetTypes()[0].Namespace });

            types = assembly.GetTypes();
            foreach (Type type in assembly.GetTypes())
                SelectingNamespace(type,myAssembly.ClassNodes);

            return myAssembly;
        }

        private void FillNamespace(Element el, Type type)
        {            
            el.ClassNodes.Add(new Element() { Name = type.Name });
            el = el.ClassNodes.Last();

            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in type.GetFields())
            {
                Element medium = new Element();                
                medium.Name = field.FieldType + " " + field.Name;
                if (medium.Name != "")
                    el.ClassNodes.Add(medium);
            }

            MethodInfo[] mes = type.GetMethods();
            for (int i=0;i<mes.Length-4;i++)
            {
                Element medium = new Element();
                MethodInfo method = mes[i];                
                if (method.Name.Contains("set_")|method.Name.Contains("get_"))
                    continue;
                string modificator = "";
                if (method.IsStatic)
                    modificator += "static ";
                if (method.IsVirtual)
                    modificator += "virtual ";
                string fullPar = "";
                foreach (ParameterInfo par in method.GetParameters())
                    fullPar += par.Name + "(" + par.ParameterType + ") ";
                medium.Name += modificator + method.ReturnType.Name + " " + method.Name + "(" + fullPar + ")";
                if (medium.Name != "")
                    el.ClassNodes.Add(medium);
            }

            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo prop in type.GetProperties())
            {
                Element medium = new Element();
                medium.Name = prop.PropertyType + " " + prop.Name;
                if (medium.Name != "")
                    el.ClassNodes.Add(medium);
            }
        }

        private void SelectingNamespace(Type type, ObservableCollection<Element> Node)
        {
            bool create = true;
            foreach (Element el in Node)
            {
                if (type.Namespace == el.Name && create == true)
                {
                    create = false;
                    FillNamespace(el, type);
                }
                else if (create == false)
                    break;
            }

            if (create == true)
            {
                string[] fullNamesp = type.Namespace.Split('.');
                string prev = "";
                for (int i = 0; i <= fullNamesp.Length - 2; i++)
                    prev += fullNamesp[i];
                foreach (Element el in Node)
                {
                    if (type.Namespace.Contains(el.Name) && type.Namespace != el.Name)
                    {
                        SelectingNamespace(type, el.ClassNodes);
                        create = false;
                    }
                    else if (type.Namespace.Contains(el.Name) && type.Namespace == el.Name)
                    {
                        FillNamespace(el, type);
                        create = false;
                    }
                  
                    if (create == false)
                        break;
                }                    
            }

            if (create == true)
            {
                Node.Add(new Element() { Name = type.Namespace });
                FillNamespace(Node.Last(),type);
            }
        }
    }
}