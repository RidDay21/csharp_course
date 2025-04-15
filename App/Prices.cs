namespace App;

public static class Prices
{
    public static string GetCurrencyAlias(int price, bool isShorNotation, bool isFirstCapital)
    {
        if (isShorNotation) {
            if (isFirstCapital)
            {
                return "Руб.";
            }
            return "руб.";
        }

        int remainderBy10 = price % 10;
        int remainderBy100 = price % 100;
        
        if (remainderBy10 == 1 && remainderBy100 != 11)
        {
            return isFirstCapital ? "Рубль" : "рубль";
        }

        else if (( remainderBy10 is >= 2 and <= 4) && !(remainderBy100 is >= 12 and <= 14))
        {
            return isFirstCapital ? "Рубля" : "рубля";
        }
        else
        {
            return isFirstCapital ? "Рублей" : "рублей";
        }
    }
}