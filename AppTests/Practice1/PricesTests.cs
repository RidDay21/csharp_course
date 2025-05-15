using App;
using NUnit.Framework;

namespace AppTests;

public class PricesTests
{
    [TestCase(1000052, "рубля")]
    [TestCase(25, "рублей")]
    [TestCase(3, "рубля")]
    [TestCase(20, "рублей")]
    [TestCase(23, "рубля")]
    [TestCase(22, "рубля")]
    [TestCase(27, "рублей")]
    [TestCase(122, "рубля")]
    [TestCase(28, "рублей")]
    [TestCase(7, "рублей")]
    [TestCase(126, "рублей")]
    [TestCase(24, "рубля")]
    [TestCase(1002, "рубля")]
    [TestCase(1, "рубль")]
    [TestCase(1000, "рублей")]
    [TestCase(6, "рублей")]
    [TestCase(1000055, "рублей")]
    [TestCase(100008, "рублей")]
    [TestCase(100009, "рублей")]
    [TestCase(26, "рублей")]
    [TestCase(1004, "рубля")]
    [TestCase(8, "рублей")]
    [TestCase(4, "рубля")]
    [TestCase(29, "рублей")]
    [TestCase(9, "рублей")]
    [TestCase(1005, "рублей")]
    [TestCase(2, "рубля")]
    [TestCase(5, "рублей")]
    [TestCase(111, "рублей")]
    public void TestPasses_When_Result_Correct(int price, string expected)
    {
        var actual = Prices.GetCurrencyAlias(price, false, false);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase(152, "Рубля")]
    [TestCase(625, "Рублей")]
    [TestCase(100103, "Рубля")]

    public void TestPasses_With_FirstCapital_True(int price, string expected)
    {
        var actual = Prices.GetCurrencyAlias(price, false, true);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase(1000052, "руб.")]
    [TestCase(25, "руб.")]
    [TestCase(1, "руб.")]
    [TestCase(11111, "руб.")]

    public void TestPasses_With_IsShorNotation(int price, string expected)
    {
        var actual = Prices.GetCurrencyAlias(price, true, false);
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [TestCase(1000052, "Руб.")]
    [TestCase(25, "Руб.")]
    [TestCase(1, "Руб.")]
    [TestCase(11111, "Руб.")]

    public void TestPasses_With_IsShorNotation_and_IsFirstCapital(int price, string expected)
    {
        var actual = Prices.GetCurrencyAlias(price, true, true);
        Assert.That(actual, Is.EqualTo(expected));
    }
    
}