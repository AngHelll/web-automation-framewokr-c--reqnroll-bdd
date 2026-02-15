Feature: Reusable Navigation Flows
    In order to verify the BDD Guardian extension
    As a QA Automation Engineer
    I want to reuse existing steps to validate different navigation flows

@Ignore @BDDGuardian @Regression
Scenario: Navigate to Notes directly from Menu
    Given the user opens the Home page
    When they click on "Notes" in the menu
    And they open a specific note
    Then they should see the note content

@Ignore @BDDGuardian @Smoke
Scenario: Navigate to Projects and Verify
    Given the user opens the Home page
    When they click on "Projects" in the menu
    Then they should navigate to the projects page and see the list

@Ignore @BDDGuardian @Experimental
Scenario: Complex Navigation Flow
    Given the user opens the Home page
    Then they should see the main title and hero section
    When they navigate to the "Notes" section
    Then they should see the note content
