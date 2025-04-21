namespace App;

public enum PaymentsPlan
{
    Differentiated,
    Annuity
}

public class Payments
{
    public static decimal CalculateTotalPayments(PaymentsPlan plan, decimal rate, int monthsCount, decimal amount)
    {
        if (rate == 0)
        {
            return amount;
        }
        
        var decimalRate = rate / 100;
        switch (plan)
        {
            case PaymentsPlan.Differentiated:
                var amountPerMonth = amount / monthsCount; 
                var remAmount = amount;
                var totalPayout = amount; 
                var monthlyRate = decimalRate / 12; 

                for (int i = 0; i < monthsCount; i++)
                {
                    totalPayout += remAmount * monthlyRate; 
                    remAmount -= amountPerMonth;
                }
                return Math.Round(totalPayout, 2, MidpointRounding.ToEven);

            case PaymentsPlan.Annuity:
                monthlyRate = decimalRate / 12; 
                var pow = DecimalPow(1 + monthlyRate, monthsCount);
                var monthlyPayment = amount * (monthlyRate * pow) / (pow - 1);
                return Math.Round(monthlyPayment * monthsCount, 2, MidpointRounding.ToEven);

            default:
                throw new ArgumentOutOfRangeException(nameof(plan), plan, null);
        }
    }
    
    private static decimal DecimalPow(decimal number, int degree)
    {
        decimal result = 1;
        var absDegree = Math.Abs(degree);
        for (int i = 0; i < absDegree; i++)
        {
            result *= number;
        }
        return result;
    }
}