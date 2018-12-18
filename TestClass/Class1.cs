using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDll
{
    namespace InnerNamespace
    {
        public class Class1
        {
            public int field;
            public int Property { get; set; }
            public void Method1() { }
            public int[] arr;
        }
    }

    public class NormalClass
    {
        public void Method2( string str, int numb) { }
    }
}

namespace TestDll2
{
    public class TestClass2
    {
        public int Property2;
    }
}
