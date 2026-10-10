using NUnit.Framework;
using Reqnroll;
using SpecHarbor.Tests.Pages;
using SpecHarbor.Tests.Support;
namespace SpecHarbor.Tests.Steps;
[Binding]
public sealed class TaskSteps(BrowserSession session)
{
    private readonly TaskBoardPage board = new(session.Driver);
    [Given("I have an empty task board")] public void Empty() { board.Open(); Assert.That(board.Remaining, Is.EqualTo("0 tasks remaining")); }
    [When("I add the task {string}")] public void Add(string text) => board.Add(text);
    [When("I submit {string} input")] public void BlankInput(string kind) => board.Add(kind switch { "empty" => "", "whitespace" => "   ", _ => throw new ArgumentException($"Unknown input kind: {kind}") });
    [When("I complete the task {string}")] public void Complete(string text) => board.Complete(text);
    [When("I delete the task {string}")] public void Delete(string text) => board.Delete(text);
    [Then("the task {string} is visible")] public void Visible(string text) => board.WaitForTask(text, true);
    [Then("the task {string} is absent")] public void Absent(string text) => board.WaitForTask(text, false);
    [Then("the remaining task count is {int}")] public void Count(int n) => Assert.That(board.Remaining, Is.EqualTo($"{n} tasks remaining"));
    [Then("I see the validation message {string}")] public void Error(string text) => Assert.That(board.Error, Is.EqualTo(text));
}
