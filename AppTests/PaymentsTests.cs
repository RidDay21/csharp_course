using App;
using NUnit.Framework;

namespace AppTests;

public class PaymentsTests
{
    [TestCase(PaymentsPlan.Annuity, 7, 3, 10000, 10116.90)]
    [TestCase(PaymentsPlan.Differentiated, 3, 5, 200000, 201500)]
    [TestCase(PaymentsPlan.Annuity, 10, 12, 50000, 52795.62)] // Аннуитет: 50,000 на 12 месяцев под 10%
    [TestCase(PaymentsPlan.Differentiated, 5, 6, 100000, 101875.00)] // Дифференцированный: 100,000 на 6 месяцев под 5%
    public void TestPasses_When_Result_Correct(PaymentsPlan plan, decimal rate, int monthsCount, decimal amount, decimal expected)
    {
        var actual = Payments.CalculateTotalPayments(plan, rate, monthsCount, amount);
        Assert.That(actual, Is.EqualTo(expected));
    }

}