using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
namespace SpecHarbor.Tests.Support;
public sealed class BrowserSession : IDisposable
{
    public IWebDriver Driver { get; }
    public BrowserSession()
    {
        var options = new ChromeOptions();
        if (Environment.GetEnvironmentVariable("HEADLESS") != "false") options.AddArgument("--headless=new");
        options.AddArgument("--window-size=1440,1000");
        options.AddArgument("--disable-dev-shm-usage");
        var grid = Environment.GetEnvironmentVariable("SELENIUM_REMOTE_URL");
        Driver = string.IsNullOrWhiteSpace(grid) ? new ChromeDriver(options) : new RemoteWebDriver(new Uri(grid), options);
        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
        Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
    }
    public void Dispose() { Driver.Quit(); Driver.Dispose(); }
}
