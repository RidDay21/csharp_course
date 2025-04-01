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
    
    [TestCase("Здравствуйте, меня зовут Николай. Вот мой номер телефона:+8-913-456-56-54. ", true)]
    [TestCase("Здравствуйте, меня зовут Николай. Вот мой номер телефона:+ 8-   913-456-56-54. ", true)]
    [TestCase("Здравствуйте, меня зовут Николай. Вот мой номер телефона(это рабочий)+8-913-456-56-54. ", true)]
    [TestCase("Здравствуйте, меня зовут Николай. Вот мой номер телефона(это рабочий)+79134565654. ", true)]
    
    public void PhoneNumberValidationTest(string inputString, bool expected)
    {
        var isValid = PhoneNumber.TryParsePhone(inputString, out _);
        
        Assert.That(isValid, Is.EqualTo(expected));
    }
    
    
    [TestCase("Номер телефона:+        8-913-456-56 - 54.", "+79134565654",TestName = "First")]
    [TestCase("Номер телефона:+8-   913-456-56-54. ", "+79134565654", TestName = "Second")]
    [TestCase("Номер телефона(это рабочий)+ 8-913-456-56-54. ", "+79134565654", TestName = "Third")]
    [TestCase("Номер телефона(это рабочий)+79134565654. ", "+79134565654",TestName = "Fourth")]
    public void Should_ReturnCorrectPhoneNumber_When_TextContainsPhoneNumber(string inputString, string expectedPhone)
    {
        _ = PhoneNumber.TryParsePhone(inputString, out var parsedPhone);
        
        Assert.That(parsedPhone, Is.EqualTo(expectedPhone));
    }
}