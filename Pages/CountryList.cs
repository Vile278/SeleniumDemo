using OpenQA.Selenium;
using SeleniumDemo.Core;

namespace SeleniumDemo.Pages;

public class CountryListPage : BasePage3
{
    private readonly By countryDropdownOptions =
        By.CssSelector("#country option");

    public CountryListPage(IWebDriver driver)
        : base(driver)
    {
    }

    public void Open()
    {
        driver.Navigate().GoToUrl(
            "https://material.playwrightvn.com/01-xpath-register-page.html");
    }

    public List<string> GetCountryNames()
    {
        var result = new List<string>();

        var options = GetElements(countryDropdownOptions);

        foreach (var option in options)
        {
            result.Add(option.Text.Trim());
        }

        return result;
    }
}