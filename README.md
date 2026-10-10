# SpecHarbor

Specification-first acceptance testing with **C#, Selenium, Reqnroll BDD, and Jenkins**.

This reference framework turns documented requirements into executable Gherkin examples. Each scenario gets its own browser session, page objects own UI interactions, and failed scenarios attach screenshots. A bundled task-board demo keeps the suite reproducible.

## Run locally
Prerequisites: .NET 8 SDK, Python 3, and Google Chrome. Selenium Manager resolves the local driver on first execution (network access required).

Terminal 1, from the repository root:
```sh
python -m http.server 8080 --bind 127.0.0.1 --directory demo
```
Terminal 2:
```sh
dotnet restore tests/SpecHarbor.Tests --locked-mode
dotnet test tests/SpecHarbor.Tests --logger trx --results-directory TestResults
```
Run smoke examples with `--filter TestCategory=smoke`. Set `HEADLESS=false` for a visible browser. `BASE_URL` defaults to `http://127.0.0.1:8080`. Set `SELENIUM_REMOTE_URL` to run on a reachable Selenium 4 Grid instead of local Chrome. The browser must be able to reach `BASE_URL` from its own machine.

## Specification-driven workflow
1. Define behavior and boundaries in [the specification](specs/task-management.md).
2. Review examples in [TaskManagement.feature](tests/SpecHarbor.Tests/Features/TaskManagement.feature).
3. Implement or adapt step bindings and page objects.
4. Run the acceptance suite and review evidence in CI.

Reqnroll provides the maintained Gherkin/BDD integration. No paid SpecFlow dependency is required.

## Jenkins CI
Create a Pipeline (or Multibranch Pipeline) from this repository, using `Jenkinsfile`. Configure a Linux agent labeled `linux-docker-dotnet` with .NET 8 SDK, Docker, Python 3, curl, and the Jenkins JUnit and Pipeline plugins. Enable your GitHub webhook or repository polling to trigger builds.

The pipeline starts an isolated Selenium Chrome container and the bundled demo, waits for readiness, restores locked dependencies, runs acceptance tests, publishes JUnit results, archives TRX/screenshots/logs, and removes its container and demo process even after failure. It uses Linux host networking so the remote browser can reach the demo. Ports 4444 and 8080 must be free; use a dedicated agent. This is a test pipeline, not an application deployment pipeline.

## Layout
`specs/` requirements and traceability; `Features/` executable examples; `Steps/` bindings; `Pages/` UI interactions; `Support/` scenario browser lifetime and evidence; `ci/` Jenkins execution.

## Extending
Add a requirement ID and scenario before a new binding. Prefer explicit waits over sleeps; never add a shared static WebDriver. Keep credentials in Jenkins credentials or environment variables, never feature files. Review artifacts before sharing them if adapting the suite to real applications.

## Scope
The example app is deliberately local, has no accounts or backend, and resets on reload. This is a working starter rather than a claim of production coverage. See [Reqnroll](https://docs.reqnroll.net/) and [Selenium](https://www.selenium.dev/documentation/).
