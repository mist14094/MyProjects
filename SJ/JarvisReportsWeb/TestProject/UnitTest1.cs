using System;
using System.Linq;
using JarvisBusinessAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
           JarvisBusinessAccess.JarvisBl bl = new JarvisBl();
        }
    }
}
