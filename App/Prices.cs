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
        string firstLetter = "р";
        string[] remainers = new string[]
        {
            "убль",
            "убля",
            "ублей",
        };
        
        if (isFirstCapital)
        {
            firstLetter = "Р";
        }
        
        var remainderBy10 = price % 10;
        var remainderBy100 = price % 100;
        
        if (remainderBy10 == 1 && remainderBy100 != 11)
        {
            return firstLetter + remainers[0]; 
        }

        else if (( remainderBy10 is >= 2 and <= 4) && !(remainderBy100 is >= 12 and <= 14))
        {
            return firstLetter + remainers[1]; 
        }
        else
        {
            return firstLetter + remainers[2]; 
        }
    }
}