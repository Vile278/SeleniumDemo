using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumDemo.Pages;

namespace SeleniumDemo.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class RegisterTest3 : BaseTest
{
    //private IWebDriver driver;
    private RegisterPage3 registerPage;

    [SetUp]
    public void Setup()
    {
        //driver = new ChromeDriver();
        //driver.Manage().Window.Maximize();

        registerPage = new RegisterPage3(driver);
    }

    [Test]
    [Category("Register3")]
    [Parallelizable]
    public void Register_New_User_3()
    {
        registerPage.Open();

        registerPage.Register(
            "vietle",
            "viet@test.com",
            "Canada");

        //Thread.Sleep(5000);
    }
    [Test]
    [Category("Register4")]
    [Parallelizable]
    public void Verify_User_Can_Set_Rating_To_Min_Or_Max_Value()
    {
        registerPage.Open();

        //Min value
        registerPage.SetRating(1);

        Assert.Multiple(() =>
        {
            Assert.That(
                registerPage.GetSliderValue(),
                Is.EqualTo("1"));

            Assert.That(
                registerPage.GetRatingValue(),
                Is.EqualTo("1"));
        });
        //Max value
        registerPage.SetRating(10);

        Assert.Multiple(() =>
        {
            Assert.That(
                registerPage.GetSliderValue(),
                Is.EqualTo("10"));

            Assert.That(
                registerPage.GetRatingValue(),
                Is.EqualTo("10"));
        });

    }

    [TearDown]
    public void TearDown()
    {
        driver?.Quit();
        driver?.Dispose();
    }
}