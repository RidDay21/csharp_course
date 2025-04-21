using App;
using NUnit.Framework;

namespace AppTests;

public class PaymentsTests
{
    [TestCase(PaymentsPlan.Annuity, 7, 3, 10000, 10116.90)]
    [TestCase(PaymentsPlan.Differentiated, 3, 5, 200000, 201500)]
    [TestCase(PaymentsPlan.Annuity, 10, 12, 50000, 52795.62)] 
    [TestCase(PaymentsPlan.Differentiated, 5, 6, 100000, 101875.00)] 
    [TestCase(PaymentsPlan.Annuity, 0, 6, 60000, 60000)] 
    [TestCase(PaymentsPlan.Differentiated, 0, 6, 60000, 60000)] 
    public void TestPasses_When_Result_Correct(PaymentsPlan plan, decimal rate, int monthsCount, decimal amount, decimal expected)
    {
        var actual = Payments.CalculateTotalPayments(plan, rate, monthsCount, amount);
        Assert.That(actual, Is.EqualTo(expected).Within(10));
    }

}