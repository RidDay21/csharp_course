using App.Practise2;

namespace AppTests.Practise2;

[TestFixture]
public class PhoneNumberTest
{

    [Test]
    [TestCase("My phone is +7(913) 456-45-43", true)]
    [TestCase("My phone is +7   (913) 456 45-43", true)]
    [TestCase("My phone is +7913 456-45-43", true)]
    [TestCase("My phone is +7 (913 4564543", true)]
    [TestCase("My phone is +8(913) 456-45-43", true)]
    [TestCase("My phone is 7(913) 456-45-43", true)]
    [TestCase("My phone is 8(913) 456-45-43", true)]
    [TestCase("My phone is +7-913-456-45-43", true)]
    [TestCase("My phone is +79134564543", true)]
    [TestCase("My phone is 79134564543", true)]
    [TestCase("My phone is 89134564543", true)]
    [TestCase("My phone is +7(9dasd13) 456-45-43", false)]
    [TestCase("My phone is +7(913)dasd456-45-43", false)]
    [TestCase("My phone is +7(913) adad456-45-43", false)]
    [TestCase("My phone is +7(913) 456-45-4334", false)]
    [TestCase("My phone is +7(913) 456-45-4312", false)]
    [TestCase("My phone is +9(913) 456-45-43", false)]
    [TestCase("My phone is +7(913) 456d-45-43", false)]
    
    public void METHOD(string inputString, bool expected)
    {
        var IsValid = PhoneNumber.TryParsePhone(inputString, out _);
        
        Assert.That(IsValid, Is.EqualTo(expected));
    }
}