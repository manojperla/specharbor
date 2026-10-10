using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
namespace SpecHarbor.Tests.Pages;
public sealed class TaskBoardPage(IWebDriver driver)
{
    private readonly WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
    public void Open() => driver.Navigate().GoToUrl(Environment.GetEnvironmentVariable("BASE_URL") ?? "http://127.0.0.1:8080");
    public void Add(string text) { var field = wait.Until(d => d.FindElement(By.Id("task"))); field.Clear(); field.SendKeys(text); driver.FindElement(By.CssSelector("button")).Click(); }
    private IWebElement Row(string text) => wait.Until(d => d.FindElements(By.CssSelector("#tasks li")).FirstOrDefault(e => e.FindElement(By.TagName("label")).Text == text))!;
    public void Complete(string text) => Row(text).FindElement(By.CssSelector("input")).Click();
    public void Delete(string text) => Row(text).FindElement(By.TagName("button")).Click();
    public bool HasTask(string text) => driver.FindElements(By.CssSelector("#tasks label")).Any(e => e.Text == text);
    public void WaitForTask(string text, bool present) => wait.Until(_ => HasTask(text) == present);
    public string Remaining => driver.FindElement(By.Id("count")).Text;
    public string Error => driver.FindElement(By.Id("error")).Text;
}
