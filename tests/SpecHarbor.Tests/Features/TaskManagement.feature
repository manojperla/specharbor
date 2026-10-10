@tasks
Feature: Task management
  As a person planning my day
  I want to capture and complete tasks
  So that I can see what still needs attention

  Background:
    Given I have an empty task board

  @smoke @TASK-001
  Scenario: Capture a task
    When I add the task "Review release evidence"
    Then the task "Review release evidence" is visible
    And the remaining task count is 1

  @TASK-002
  Scenario: Complete a task
    When I add the task "Run acceptance checks"
    And I complete the task "Run acceptance checks"
    Then the remaining task count is 0

  @TASK-003
  Scenario Outline: Reject blank task descriptions
    When I submit "<kind>" input
    Then I see the validation message "Enter a task"
    And the remaining task count is 0
    Examples:
      | kind       |
      | empty      |
      | whitespace |

  @TASK-004
  Scenario: Remove a task
    When I add the task "Investigate a flaky test"
    And I delete the task "Investigate a flaky test"
    Then the task "Investigate a flaky test" is absent
    And the remaining task count is 0
