using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;

namespace SeleniumDemo.Tests;

public class RegisterTest1 : BaseTest
{
    // private IWebDriver driver; // Declare the WebDriver at the class level

    [SetUp]
    public void Setup()
    {
        // driver = new ChromeDriver(); // Initialize the WebDriver
        driver.Manage().Window.Maximize(); // Maximize the browser window
    }

    [Test]
    [Category("Register")]
    public void Register_New_User1()
    {
        driver.Navigate().GoToUrl(
            "https://material.playwrightvn.com/01-xpath-register-page.html");

        driver.FindElement(By.Id("username"))
            .SendKeys("vietle");

        driver.FindElement(By.Id("email"))
            .SendKeys("viet@test.com");

        driver.FindElement(By.Id("male"))
            .Click();

        driver.FindElement(By.Id("reading"))
            .Click();

        driver.FindElement(By.Id("country"))
            .SendKeys("Canada");
        
        //Debugger.Break();
        // Console.WriteLine("Press Enter to continue...");
        // Console.ReadLine();
        //Thread.Sleep(5000);
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }
}