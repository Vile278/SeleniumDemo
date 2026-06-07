using OpenQA.Selenium;

namespace SeleniumDemo.Pages;

public class RegisterPage2
{
    private readonly IWebDriver driver;


    public RegisterPage2(IWebDriver driver)
    {
        this.driver = driver;
    }

    // Locators
    // private IWebElement abc => driver.FindElement(By.Id("abc"));
    private IWebElement Username => driver.FindElement(By.Id("username"));

    private IWebElement Email => driver.FindElement(By.Id("email"));
    private IWebElement Male => driver.FindElement(By.Id("male"));
    private IWebElement Reading => driver.FindElement(By.Id("reading"));
    private IWebElement Country => driver.FindElement(By.Id("country"));

    // Actions
    public void Open()
    {
        driver.Navigate().GoToUrl(
            "https://material.playwrightvn.com/01-xpath-register-page.html");
    }

    public void EnterUsername(string username)
    {
        Username.SendKeys(username);
    }

    public void EnterEmail(string email)
    {
        Email.SendKeys(email);
    }

    public void SelectMale()
    {
        Male.Click();
    }

    public void SelectReadingHobby()
    {
        Reading.Click();
    }

    public void EnterCountry(string country)
    {
        Country.SendKeys(country);
    }

    public void Register(
        string username,
        string email,
        string country)
    {
        EnterUsername(username);
        EnterEmail(email);
        SelectMale();
        SelectReadingHobby();
        EnterCountry(country);
    }
}