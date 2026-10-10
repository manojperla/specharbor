using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
namespace SpecHarbor.Tests.Support;
[Binding]
public sealed class Hooks(BrowserSession session, ScenarioContext scenario)
{
    [AfterScenario(Order = 0)]
    public void CaptureFailure()
    {
        if (scenario.TestError is null) return;
        try
        {
            var folder = Path.Combine(TestContext.CurrentContext.WorkDirectory, "artifacts");
            Directory.CreateDirectory(folder);
            var file = Path.Combine(folder, $"failure-{Guid.NewGuid():N}.png");
            ((ITakesScreenshot)session.Driver).GetScreenshot().SaveAsFile(file);
            TestContext.AddTestAttachment(file, scenario.ScenarioInfo.Title);
        }
        catch (WebDriverException e) { TestContext.Progress.WriteLine($"Screenshot unavailable: {e.Message}"); }
    }
    // Reqnroll disposes the scenario-scoped BrowserSession after scenario hooks.
}
