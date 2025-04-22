using App.Practise2;

namespace AppTests.Practise2;

[TestFixture]
public class StackMachineTest{
    
    [Test]
    [TestCase(new string[] 
        {"push Привет! Это снова я! Пока!",
        "pop 5",
        "push Как твои успехи? Плохо?",
        "push qwertyuiop",
        "push 1234567890",
        "pop 27"},
        "Привет! Это снова я! Как твои успехи?",
    TestName = "BasicTest")]
    
    [TestCase(
        new string[] {
            "push Manchester",
            "push  is",
            "push  Red"
        },
        "Manchester is Red",
        TestName = "TestWithMultiplePushCommand"
    )]
    
    [TestCase(
        new string[] {
            "push 1234567890",
            "pop 3",
            "pop 2",
            "pop 1"
        },
        "1234",
        TestName = "TestWithMultiplePopCommand"
    )]
    
    [TestCase(
        new string[] {
            "push TestString",
            "pop 10"
        },
        "",
        TestName = "TestWithPopTheWholeString"
    )]
    
    [TestCase(
        new string[0],
        "",
        TestName = "TestWithEmptyString"
    )]
    
    [TestCase(
        new string[] {
            "pop 0"
        },
        "",
        TestName = "TestWithZeroPopAndEmptyString"
    )]
    
    [TestCase(
        new string[] {
            "push Hi, Bro!",
            "pop 0"
        },
        "Hi, Bro!",
        TestName = "TestWithZeroPopAndNotEmptyString"
    )]
    public void METHOD(string[] lines, string expected)
    {
        var result = StackMachine.CalculateString(lines);
        
        Assert.AreEqual(expected, result);
    }
}
