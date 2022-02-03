using Microsoft.VisualStudio.TestTools.UnitTesting; // pour [ClassInitialize]
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Doit hériter de TestSession
/// </summary>
namespace TestWindowsDriver
{
    [TestClass]
    public class WPFTest : TestSession
    {
        /// <summary>
        /// A l'ouverture de l'appli
        /// </summary>
        /// <param name="context"></param>
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            Setup(context);
        }

        [TestMethod]
        public void TestApp()
        {
            //session.FindElementByName("UnNom").Click; // Attention c'est une recherche par Content
            session.FindElementByAccessibilityId("LoginButton").Click(); // ici c'est le x:Name présent dans du xaml
            Thread.Sleep(TimeSpan.FromSeconds(1));
            
        }

        /// <summary>
        /// A la fermeture de l'appli
        /// </summary>
        [ClassCleanup]
        public static void ClassCleanup()
        {
            //TearDown()
        }

    }
}
