using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public class BaseTest
{
    protected IWebDriver driver;

    [SetUp]
    public void BaseSetup()
    {
        var options = new ChromeOptions();

        bool headless = true;

        if (headless)
        {
            options.AddArgument("--headless=new");
        }

        driver = new ChromeDriver(options);
        driver.Manage().Window.Maximize();
    }

    [TearDown]
    public void BaseTearDown()
    {
        driver.Quit();
        driver?.Dispose();
    }
}