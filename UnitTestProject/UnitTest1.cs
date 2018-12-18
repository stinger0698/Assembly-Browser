using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyBrowser;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private Element Setup()
        {
            Browser browser = new Browser();
            return browser.LoadAssembly("TestClass.dll");        }

        [TestMethod]
        public void GettingTest()
        {
            Element actual = Setup();
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void NameTest()
        {
            Element actual = Setup();
            Assert.IsTrue(actual.Name.StartsWith("TestClass"));
        }

        [TestMethod]
        public void MultipleNamespace()
        {
            Element actual = Setup();
            Assert.AreEqual("TestDll2", actual.ClassNodes[0].Name);
            Assert.AreEqual("TestDll", actual.ClassNodes[1].Name);
        }

        [TestMethod]
        public void CompositeNamespace()
        {
            Element actual = Setup();
            Assert.AreEqual("TestDll.InnerNamespace", actual.ClassNodes[1].ClassNodes[1].Name);          
        }

        [TestMethod]
        public void TypeTest()
        {
            Element actual = Setup();
            Assert.AreEqual("NormalClass", actual.ClassNodes[1].ClassNodes[0].Name);
        }

        [TestMethod]
        public void FieldTest()
        {
            Element actual = Setup();
            Assert.AreEqual("System.Int32 field", actual.ClassNodes[1].ClassNodes[1].ClassNodes[0].ClassNodes[0].Name);
        }

        [TestMethod]
        public void MethodTest()
        {
            Element actual = Setup();
            Assert.AreEqual("Void Method1()", actual.ClassNodes[1].ClassNodes[1].ClassNodes[0].ClassNodes[2].Name);
        }

        [TestMethod]
        public void PropertyTest()
        {
            Element actual = Setup();
            Assert.AreEqual("System.Int32 Property", actual.ClassNodes[1].ClassNodes[1].ClassNodes[0].ClassNodes[3].Name);
        }
    }
}
