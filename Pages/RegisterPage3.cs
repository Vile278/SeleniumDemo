using OpenQA.Selenium;
using SeleniumDemo.Core;

namespace SeleniumDemo.Pages;

// Cũng giống như RegisterPage2, nhưng mở rộng từ Base để kết thừa các methods chung click, Input text, get text, check element is displayed, v.v... từ BasePage3
public class RegisterPage3 : BasePage3
{
    public RegisterPage3(IWebDriver driver)
        : base(driver)
    {
    }

    #region Locators

    private readonly By usernameTxt = By.Id("username");
    private readonly By emailTxt = By.Id("email");
    private readonly By maleRadio = By.Id("male");
    private readonly By readingCheckbox = By.Id("reading");
    private readonly By countryTxt = By.Id("country");

    private readonly By ratingSlider = By.Id("rating");
    private readonly By ratingValue = By.Id("ratingValue");
    #endregion

    #region Actions

    public void Open()
    {
        driver.Navigate().GoToUrl(
            "https://material.playwrightvn.com/01-xpath-register-page.html");
    }

    public void EnterUsername(string username)
    {
        Type(usernameTxt, username);
    }

    public void EnterEmail(string email)
    {
        Type(emailTxt, email);
    }

    public void SelectMale()
    {
        Click(maleRadio);
    }

    public void SelectReadingHobby()
    {
        Click(readingCheckbox);
    }

    public void EnterCountry(string country)
    {
        Type(countryTxt, country);
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
 public void SetRating(int value)
    {
        var slider = GetElement(ratingSlider);

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].value = arguments[1];" +
            "arguments[0].dispatchEvent(new Event('input'));",
            slider,
            value);
    }

    public string GetRatingValue()
    {
        return GetElement(ratingValue).Text;
    }

    public string? GetSliderValue()
    {
        return GetElement(ratingSlider)
            .GetAttribute("value");
    }
    #endregion
}