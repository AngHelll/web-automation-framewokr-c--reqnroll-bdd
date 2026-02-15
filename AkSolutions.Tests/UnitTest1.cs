using NUnit.Framework;

namespace AkSolutions.Tests;

public class UnitTest1
{
    [Test]
    public void Test1()
    {
        TestContext.Progress.WriteLine("Simple NUnit Test Running");
        Assert.Pass();
    }
}
