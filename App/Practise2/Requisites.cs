namespace App.Practise2;

public class Requisites
{
    public static bool IsValidInn(string inn)
    {
        if (inn.Length != 10 && inn.Length != 12)
        {
            return false;
        }

        if (!IsAllDigits(inn))
        {
            return false;
        }

        return ValidateInnChecksum(inn);
    }

    private static bool IsAllDigits(string inn)
    {
        foreach (char c in inn)
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
                int[] coeffs10 = { 2, 4, 10, 3, 5, 9, 4, 6, 8 };
                int sum10 = 0;
                for (int i = 0; i < 9; i++)
                {
                    sum10 += coeffs10[i] * (inn[i] - '0');
                }

                if (sum10 == 0) return false;
                int controlDigit10 = (sum10 % 11) % 10;
                return (inn[9] - '0') == controlDigit10;

            case 12:
                int[] coeffs12_1 = { 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 };
                int sum12_1 = 0;
                for (int i = 0; i < 10; i++)
                {
                    sum12_1 += coeffs12_1[i] * (inn[i] - '0');
                }
                int controlDigit11 = (sum12_1 % 11) % 10;
                if ((inn[10] - '0') != controlDigit11)
                {
                    return false;
                }
                
                int[] coeffs12_2 = { 3, 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 };
                int sum12_2 = 0;
                for (int i = 0; i < 11; i++)
                {
                    sum12_2 += coeffs12_2[i] * (inn[i] - '0');
                }
                if (sum12_2 == 0) return false;
                int controlDigit12 = (sum12_2 % 11) % 10;
                return (inn[11] - '0') == controlDigit12;

            default:
                return false;
        }
    }
}