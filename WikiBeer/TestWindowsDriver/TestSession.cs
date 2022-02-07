using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;

/// <summary>
/// Classe qui gère les sessions de Test (reprise des exemples sur le github du WindowsAppDriver
/// et adapter par Nico)
/// </summary>
namespace TestWindowsDriver
{
    //[TestClass]
    public class TestSession
    {
        // Note: append /wd/hub to the URL if you're directing the test at Appium
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        // En dessous le nom de l'éxécutable que l'on veut tester
        private const string AppPath = @"C:\Users\armel\git\Formation_IPME_dot_net\Projet\EncyclopeBeer\WikiBeer\Wpf\bin\Debug\net6.0-windows\Ipme.WikiBeer.Wpf.exe";

        protected static WindowsDriver<WindowsElement> session;

        public static void Setup(TestContext context)
        {            
            if (session == null)
            {

                var options = new AppiumOptions();
                options.AddAdditionalCapability("app", AppPath);
                options.AddAdditionalCapability("deviceName", "WindowsPC"); // WindowsPc -> type de Device et pas le nom du PC
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), options); 
                Assert.IsNotNull(session);

                // Set implicit timeout to 1.5 seconds to make element search to retry every 500 ms for at most three times
                session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);
            }
        }

        public static void TearDown()
        {
            // Close the application and delete the session
            if (session != null)
            {
                session.Quit();
                session = null;
            }
        }
    }
}