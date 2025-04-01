using App.Practise2;
using NUnit.Framework;

namespace AppTests.Practise2;

[TestFixture]
public class RequisitesTest
{
    [Test]
    [TestCase("7830002293", true)]
    [TestCase("9403000592", true)]
    [TestCase("7703399790", true)]
    
    [TestCase("500100732259", true)]
    [TestCase("667305416360", true)]
    [TestCase("532110684141", true)]
    
    [TestCase("5001007321", false)] 
    [TestCase("7728168910", false)]
    
    [TestCase("366220809013", false)]
    [TestCase("344401442035", false)]
    
    
    [TestCase("", false)]
    [TestCase("123", false)]
    [TestCase("1234567890123", false)]
    [TestCase("abcdefghij", false)]
    [TestCase("7707abcd123", false)]
    [TestCase("0000000000", false)] 
    [TestCase("000000000000", false)]  
    [TestCase("1", false)]  
    [TestCase("12345678901", false)]  
    
    public void INN_Validation_Tests(string inn, bool expected)
    {
        // Act
        var actual = Requisites.IsValidInn(inn);
        
        // Assert
        Assert.That(actual, Is.EqualTo(expected), 
            $"ИНН: {inn}. Ожидалось: {expected}, получено: {actual}");
    }
}