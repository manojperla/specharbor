# Diagnosing a SpecHarbor run

1. **Demo unavailable:** start the README's HTTP server from the repository root.
   Open `BASE_URL` from the browser host. With Grid, a URL reachable only from
   the test process is insufficient.
2. **Browser cannot start:** check Chrome installation and Selenium Manager's
   driver-download connectivity. With Grid, check the Grid status and container
   logs before changing test assertions.
3. **Scenario fails after navigation:** inspect the TRX result and its screenshot
   attachment. CaptureFailure runs only on failures; a missing image can indicate
   that the driver itself was unavailable.
4. **Reproduce a smoke failure:** run
   `dotnet test tests/SpecHarbor.Tests --filter TestCategory=smoke --logger trx --results-directory TestResults`.
   Set `HEADLESS=false` for a visible local browser.
5. **Jenkins-only failure:** check demo/Grid readiness and ports 8080/4444 on the
   dedicated agent. Preserve archived logs before retrying.

Do not hide a reproducible assertion failure with a sleep or a blanket retry.
When reporting it, include the scenario, command, commit SHA, local/Grid mode,
and the first relevant error. Review screenshots for application data before sharing.
