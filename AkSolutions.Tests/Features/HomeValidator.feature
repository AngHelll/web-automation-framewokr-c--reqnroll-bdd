Feature: Home Validation and Navigation
    As a visitor
    I want to browse the personal site
    To see projects and notes

@Correct @MVP @Regression @Ignore
Scenario: Happy Path - Main Navigation
    Given the user opens the Home page
    Then they should see the main title and hero section
    When they click on "Projects" in the menu
    Then they should navigate to the projects page and see the list
    When they navigate to the "Notes" section
    And they open a specific note
    Then they should see the note content

# -------------------------------------------------------------------------
# BAD PRACTICE EXAMPLE 1: Imperative Style & Hardcoded Waits
# Why it's bad: 
# 1. Tightly coupled to implementation details (buttons, specific text).
# 2. Hardcoded waits make tests slow and flaky.
# 3. Describes "HOW" instead of "WHAT" (imperative vs declarative).
# -------------------------------------------------------------------------
@BadPractice @Ignore @Educational
Scenario: Bad Practice - Imperative Navigation
    Given I navigate to url "https://ak-solutions.app"
    And I wait for 5 seconds
    When I click on the button with text "Ver proyectos"
    And I scroll down to the bottom of the page
    Then I verify that the url contains "/projects"
    And I take a screenshot

# -------------------------------------------------------------------------
# BAD PRACTICE EXAMPLE 2: Technical Selectors in Gherkin
# Why it's bad:
# 1. Business stakeholders cannot read/understand CSS/XPath.
# 2. Refactoring the UI breaks the *Feature file* (Scenario), not just the Page Object.
# 3. Violates separation of concerns.
# -------------------------------------------------------------------------
@BadPractice @Ignore @Educational
Scenario: Bad Practice - Technical Selectors
    Given user is on page
    When user clicks css selector "nav > ul > li:nth-child(2) > a"
    Then element "#project-list" should he visible
    And element "//div[@class='card-body']" should contain text "Automation"
