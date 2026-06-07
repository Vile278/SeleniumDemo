using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumDemo.Pages;

namespace SeleniumDemo.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class RegisterTest2 : BaseTest
{
    //private IWebDriver driver;
    private RegisterPage2 registerPage2;

    [SetUp]
    public void Setup()
    {
        //driver = new ChromeDriver();
        //driver.Manage().Window.Maximize();

        registerPage2 = new RegisterPage2(driver);
    }

    [Test]
    [Category("Register4")]
    [Parallelizable]
    public void Register_New_User2()
    {
        registerPage2.Open();

        registerPage2.Register(
            "vietle",
            "viet@test.com",
            "Canada");

        //Thread.Sleep(5000);
    }

    [TearDown]
    public void TearDown()
    {
        driver?.Quit();
        driver?.Dispose();
    }
}