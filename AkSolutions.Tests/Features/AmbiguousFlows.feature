Feature: BDD Guardian - Ambiguity Check
    As a QA Engineer
    I want to verify that BDD Guardian detects ambiguous steps
    So that I can prevent runtime errors in my test suite

@Ignore @BDDGuardian @Ambiguous @Manual
Scenario: Ambiguous Step Definition
    Given I have a step validation pending
    When I execute an action with multiple definitions
    Then I should see an ambiguity warning in the editor
