using Reqnroll;

namespace AkSolutions.Tests.Steps;

[Binding]
public class AmbiguousStepsDefinition1
{
    [When("I execute an action with multiple definitions")]
    public void WhenIExecuteAnAction() 
    { 
        // Implementation 1
    }
}

[Binding]
public class AmbiguousStepsDefinition2
{
    // usage of same regex string causes ambiguity
    [When("I execute an action with multiple definitions")]
    public void WhenIExecuteAnActionDuplicate() 
    { 
        // Implementation 2
    }
}

[Binding]
public class CommonAmbiguousSteps
{
    [Given("I have a step validation pending")]
    public void GivenIHaveAStepValidationPending() { }

    [Then("I should see an ambiguity warning in the editor")]
    public void ThenIShouldSeeAnAmbiguityWarning() { }
}
