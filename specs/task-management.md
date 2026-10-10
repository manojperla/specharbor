# Task management specification
Status: reference demo, not production task storage.

| Requirement | Acceptance rule | Executable scenario |
| --- | --- | --- |
| TASK-001 | A nonblank description creates one unchecked task. | Capture a task |
| TASK-002 | Completing a task removes it from the remaining count. | Complete a task |
| TASK-003 | Empty or whitespace-only input shows an error and creates nothing. | Reject blank task descriptions |
| TASK-004 | Deleting a task removes it and updates the count. | Remove a task |

The board starts empty on page load. Persistence, authentication, concurrent users, and backend APIs are explicitly outside this demo.

Workflow: agree on a requirement and examples here, add/update the tagged Gherkin scenario, implement bindings using page objects, run the suite, and review the specification and test evidence together. A changed behavior requires both the specification and examples to change in the same pull request.
