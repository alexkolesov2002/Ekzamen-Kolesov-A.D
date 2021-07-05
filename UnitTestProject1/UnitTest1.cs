using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Ekzamen_Kolesov_A.D;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Critical MM = new Critical();
            MM.Reshenie();
            Assert.AreEqual(29, MM.maximal);
           
        }
        [TestMethod]
        public void TestMethod2()
        {
                Critical MM = new Critical();
                MM.Reshenie();
                Assert.IsInstanceOfType(MM.maximal, typeof(int));
        }

        [TestMethod]
        public void TestMethod3()
        {
            Critical MM = new Critical();
            MM.Reshenie();
            Assert.IsTrue(MM.maximal == 29);
        }
        [TestMethod]
        public void TestMethod4()
        {
            Critical MM = new Critical();
            MM.Reshenie();
            Assert.IsNotNull(MM.maximal);
        }
      
    }
}
