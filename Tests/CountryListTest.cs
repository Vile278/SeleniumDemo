using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumDemo.Pages;

namespace SeleniumDemo.Tests;

public class CountryListTest : BaseTest
{
    //private IWebDriver driver;
    private CountryListPage countryList;

    [SetUp]
    public void Setup()
    {
        //driver = new ChromeDriver();
        //driver.Manage().Window.Maximize();
        countryList = new CountryListPage(driver);
    }

    [Test]
    public void Verify_Country_Names()
    {
        countryList.Open();
        var actualCountries = countryList.GetCountryNames();
        var expectedCountries = TestData.CountryData.ExpectedCountries;

        //1. So sánh số lượng
        Assert.That(actualCountries.Count, Is.EqualTo(expectedCountries.Count)); 
        Console.WriteLine($"Expected Countries: {string.Join(", ", expectedCountries)}");

        //2. So sánh từng phần tử
        for (int i = 0; i < expectedCountries.Count; i++) 
        {
            Assert.That(
                actualCountries[i],
                Is.EqualTo(expectedCountries[i]),
                $"Country mismatch at index {i}");
            Console.WriteLine($"Actual Country at index {i}: {actualCountries[i]}");
        }

        //3. Kiểm tra xem tất cả các quốc gia có trong danh sách hay không (step này thừa vì step 2 đã có rồi)
        foreach (var expectedCountry in expectedCountries)
        {
            Assert.That(
                actualCountries,
                Does.Contain(expectedCountry),
                $"Expected country '{expectedCountry}' not found in the actual list");
            Console.WriteLine($"Found expected country: {expectedCountry}");
        }

        // 4. Kiểm tra xem có quốc gia nào không mong muốn xuất hiện trong danh sách hay không
        var unexpectedCountries = new List<string> { "Vietnam", "Lao" };
        foreach (var unexpectedCountry in unexpectedCountries)
        {
            Assert.That(
                actualCountries,
                Does.Not.Contain(unexpectedCountry),
                $"Unexpected country '{unexpectedCountry}' found in the actual list");
            Console.WriteLine($"Verified that unexpected country is not present: {unexpectedCountry}");
        }

        // 5. Kiểm tra xem danh sách có được sắp xếp theo thứ tự chữ cái hay không
        // Hãy change code dưới đây để test không failed nếu danh sách không được sắp xếp, nhưng vẫn in ra thông báo nếu nó không được sắp xếp
        var sortedCountries = new List<string>(actualCountries);
        sortedCountries.Sort();
        if (!actualCountries.SequenceEqual(sortedCountries))
        {
            Console.WriteLine("Countries are not sorted alphabetically.");
        }
        else
        {
            Console.WriteLine("Countries are sorted alphabetically.");
        }

        // 6. Kiểm tra xem có quốc gia nào bị trùng lặp trong danh sách hay không
        var duplicateCountries = actualCountries.GroupBy(c => c)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();
        if (duplicateCountries.Any())
        {
            Console.WriteLine($"Duplicate countries found: {string.Join(", ", duplicateCountries)}");
        }
        else
        {
            Console.WriteLine("No duplicate countries found.");
        }

        // 7. Kiểm tra xem có quốc gia nào bị thiếu trong danh sách hay không
        var missingCountries = expectedCountries.Except(actualCountries).ToList();
        if (missingCountries.Any())
        {
            Console.WriteLine($"Missing countries found: {string.Join(", ", missingCountries)}");
        }
        else
        {
            Console.WriteLine("No missing countries found.");
        }
        // 8. Dùng LINQ kiểm tra actualCountries có chứa tất cả các expectedCountries hay không (step này thừa vì step 2 đã có rồi, nhưng step 2 không dùng LINQ mà code check từng phần tử một, còn step này dùng LINQ để check tất cả cùng lúc)
        var allCountriesPresent = expectedCountries.All(ec => actualCountries.Contains(ec));
        Assert.That(allCountriesPresent, Is.True, "Not all expected countries are present in the actual list");
        Console.WriteLine("All expected countries are present in the actual list.");

        //9. Kiểm tra có quốc gia nào độ dài tên quốc gia lớn hơn 10 ký tự hay không
        var longCountryNames = actualCountries.Where(c => c.Length > 10).ToList();
        if (longCountryNames.Any())
        {
            Console.WriteLine($"Countries with names longer than 10 characters: {string.Join(", ", longCountryNames)}");
        }
        else
        {
            Console.WriteLine("No countries with names longer than 10 characters found.");
        }

        //10. Kiểm tra xem có quốc gia nào trong danh sách có chứa chữ "a" hay không
        var countriesWithA = actualCountries.Where(c => c.Contains("a")).ToList();
        if (countriesWithA.Any())
        {
            Console.WriteLine($"Countries containing the letter 'a': {string.Join(", ", countriesWithA)}");
        }
        else
        {
            Console.WriteLine("No countries containing the letter 'a' found.");
        }

        //11. Kiểm tra xem có quốc gia nào trong danh sách là null hoặc empty hay không
        var nullOrEmptyCountries = actualCountries.Where(c => string.IsNullOrEmpty(c)).ToList();
        if (nullOrEmptyCountries.Any())
        {
            Console.WriteLine($"Countries that are null or empty: {string.Join(", ", nullOrEmptyCountries)}");
        }
        else
        {
            Console.WriteLine("No countries that are null or empty found.");
        }   
        //12. Kiểm  tra null or whitespace
        Assert.That(actualCountries.All(c => !string.IsNullOrWhiteSpace(c)), "Null or empty countries found");

        //12. Check format của tên quốc gia (chữ cái đầu mỗi từ phải viết hoa, các chữ cái còn lại phải viết thường ví dụ: "United States", "United Kingdom", "France", không chấp nhận "united states", "UNITED STATES", "United states", "uNITED sTATES")
        var invalidFormatCountries = actualCountries.Where(c => !System.Text.RegularExpressions.Regex.IsMatch(c, @"^([A-Z][a-z]+)(\s[A-Z][a-z]+)*$")).ToList();
        if (invalidFormatCountries.Any())        {
            Console.WriteLine($"Countries with invalid format: {string.Join(", ", invalidFormatCountries)}");
        }
        else
        {
            Console.WriteLine("All countries have valid format.");
        }

        //13. Kiểm tra filter/Search quốc gia (ví dụ: filter "United" thì chỉ hiển thị "United States", "United Kingdom")   
        // Note: Step này chỉ là giả định vì trên UI không có chức năng filter/search, nên mình sẽ giả lập bằng cách dùng LINQ để filter danh sách actualCountries dựa trên keyword "United"
        // Chứ thực tế trên UI có chức năng search/filter thì mình sẽ viết code để thao tác với UI để nhập keyword vào ô search/filter rồi mới lấy kết quả ra để so sánh với expected result
        var filterKeyword = "United";
        var filteredCountries = actualCountries.Where(c => c.Contains(filterKeyword)).ToList(); 
        Console.WriteLine($"Countries containing the filter keyword '{filterKeyword}': {string.Join(", ", filteredCountries)}");
    }

    [TearDown]
    public void TearDown()
    {
        driver?.Quit();
        driver?.Dispose();
    }
}