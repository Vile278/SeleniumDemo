using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
//using SeleniumExtras.WaitHelpers; // chỉ dùng với selenium 3
using Microsoft.Extensions.Configuration;
namespace SeleniumDemo.Core;

// BasePage3 is an abstract class that provides common methods for interacting with web elements.
// các methods này sẽ được các page cụ thể kế thừa và sử dụng để tương tác với các element trên trang web, như RegisterPage3 chẳng hạn
public abstract class BasePage3
{
    protected readonly IWebDriver driver;
    protected readonly WebDriverWait wait; //dùng chung cho cả class nên mình khai báo ở đây luôn, chứ không khai báo trong từng method như cách 1.2 với Selenium Extras (selenium 3)

    protected BasePage3(IWebDriver driver)
    {
        this.driver = driver;
        // Cách 1: không declare pooling interval, thì mặc định nó sẽ là 500ms, nghĩa là sau mỗi 500ms thì WebDriverWait sẽ kiểm tra lại điều kiện một lần, nếu điều kiện đã thỏa mã
        //this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); // timeout 10 giây, nếu sau 10 giây mà element vẫn chưa xuất hiện thì sẽ throw exception, còn nếu element xuất hiện trước 10 giây thì sẽ tiếp tục thực thi code ngay lập tức mà không cần phải chờ hết 10 giây

        //Cách 2: có declare pooling interval, thì sau mỗi 200ms thì WebDriverWait sẽ kiểm tra lại điều kiện một lần, nếu điều kiện đã thỏa mã thì sẽ tiếp tục thực thi code ngay lập tức mà không cần phải chờ hết 10 giây, còn nếu sau 10 giây mà element vẫn chưa xuất hiện thì sẽ throw exception
        // cách này timeout và pooling interval set specific
        // this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
        // {
        //     PollingInterval = TimeSpan.FromMilliseconds(200)
        // };
        //Cách 3: đọc timeout và pooling interval từ appsettings.json
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettingsVile.json")
            .Build();

        var timeout = TimeSpan.FromSeconds(
            config.GetValue<int>("WaitSettings:TimeoutInSeconds"));

        var pollingInterval = TimeSpan.FromMilliseconds(
            config.GetValue<int>("WaitSettings:PollingIntervalInMilliseconds"));

        this.wait = new WebDriverWait(driver, timeout)
        {
            PollingInterval = pollingInterval
        };
    }
    //Cách 1: không dùng explicit wait:
    // protected IWebElement GetElement(By locator)
    // {
    //     // Cách 1.1: không dùng explicit wait:
    //     //return driver.FindElement(locator);
    //     // Cách 1.2: dùng explicit wait với Selenium Extras (selenium 3):
    //     //return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));     
    // }

    // Cách 2: dùng explicit wait với lambda expression (selenium 4):
    // lambda expression là một cách viết ngắn gọn của một phương thức ẩn danh (anonymous method) trong C#. Nó cho phép bạn viết một đoạn code ngắn gọn để thực hiện một hành động nào đó mà không cần phải định nghĩa một phương thức riêng biệt. Trong trường hợp này, lambda expression được sử dụng để chờ đợi cho đến khi element được tìm thấy và hiển thị trên trang web. Nếu element không được tìm thấy hoặc không hiển thị sau 10 giây, thì sẽ trả về null.
    // Lambda expression trong C# có cú pháp như sau: (parameters) => expression. Trong trường hợp này, d là tham số đại diện cho đối tượng IWebDriver, và phần sau => là biểu thức trả về giá trị của element nếu nó được tìm thấy và hiển thị, hoặc null nếu không tìm thấy hoặc không hiển thị.
    protected IWebElement GetElement(By locator)
    {
        return wait.Until(d =>
        {
            try
            {
                var element = d.FindElement(locator);
                return element.Displayed ? element : null;
            }
            catch (NoSuchElementException) // nếu không tìm thấy element thì sẽ trả về null
            {
                return null;
            }
            catch (StaleElementReferenceException) // nếu element bị stale (không còn tồn tại trên DOM nữa) thì sẽ trả về null
            {
                return null;
            }
            catch (ElementNotInteractableException) // nếu element không thể tương tác được thì sẽ trả về null
            {
                return null;
            }
            //hãy viết các stage khác nếu bạn muốn, nhưng mà 3 stage này là phổ biến nhất khi làm automation testing với Selenium, nên mình viết sẵn ở đây luôn để bạn có thể copy paste vào code của mình khi cần thiết
        });
    }
    //chẳng qua là click vào element thôi, nhưng mà mình đặt tên là Click cho nó ngắn gọn
    protected void Click(By locator)
    {
        GetElement(locator).Click();
    }

    protected void Type(By locator, string text) //chẳng qua là fill text vào input thôi, nhưng mà mình đặt tên là Type cho nó ngắn gọn
    {
        var element = GetElement(locator);

        //element.Clear(); không dùng code này với list Country vì nó là list        
        element.SendKeys(text);
    }

    protected string GetText(By locator)
    {
        return GetElement(locator).Text;
    }

    protected bool IsDisplayed(By locator)
    {
        return GetElement(locator).Displayed;
    }
    // apply for country list:
    protected IReadOnlyCollection<IWebElement> GetElements(By locator)
    {
        return driver.FindElements(locator);
    }
}
