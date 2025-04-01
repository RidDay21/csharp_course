namespace App.Practise2;

public class Requisites
{
    public static bool IsValidInn(string inn)
    {
        if (!IsAllDigits(inn) || inn.Length != 10 && inn.Length != 12)
        {
            return false;
        }
        return ValidateInnChecksum(inn);
    }

    private static bool IsAllDigits(string inn)
    {
        foreach (var c in inn)
        {
            if (!char.IsDigit(c))
            {
                return false;
            }
        }
        return true;
    }

    private static bool ValidateInnChecksum(string inn)
    {
        switch (inn.Length)
        {
            case 10:
                return IsValidIndividTIN(inn);
            case 12:
                return IsValidCorporateTIN(inn);
            default:
                return false;
        }
    }

    static int GetMod11ChecksumDigit(int sum)
    {
        return (sum % 11) % 10;
    }
    
    static int GetDotProduct(string inn, int[] weights)
    {
        var total = 0;
        for (var i = 0; i < weights.Length; i++)
        {
            total += (inn[i] - '0') * weights[i];
        }
        return total;
    }

    /**
     * TIN -- TaxpayerIdentification Number
     */
    static bool IsValidIndividTIN(string inn)
    {
        int[] coefficients = { 2, 4, 10, 3, 5, 9, 4, 6, 8 };
        var sum10 = GetDotProduct(inn, coefficients);
        if (sum10 == 0) return false;
        var controlDigit10 = GetMod11ChecksumDigit(sum10);
        return (inn[9] - '0') == controlDigit10;
    }
    
    static bool IsValidCorporateTIN(string inn)
    {
        int[] firstCoefficients = { 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 };
        var firstSum = GetDotProduct(inn, firstCoefficients);
        var controlDigit11 = GetMod11ChecksumDigit(firstSum);
        
        if ((inn[10] - '0') != controlDigit11)
        {
            return false;
        }
                
        int[] secondCoefficients = { 3, 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 };
        var secondSum = GetDotProduct(inn, secondCoefficients);
        if (secondSum == 0) return false;
        var controlDigit12 = GetMod11ChecksumDigit(secondSum);
        return (inn[11] - '0') == controlDigit12;
    }
}