namespace App;

public static class Distance
{
    public static double DistanceToSegment(
        // позиция курсора
        double x, double y,
        // отрезок
        double x1, double y1, double x2, double y2)
    {
        if (x1 == x2 && y1 == y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x, 2) + Math.Pow(y2 - y, 2));
        }
        
        var dx = x2 - x1;
        var dy = y2 - y1;
        
        var value = ((x - x1) * dx + (y - y1) * dy) / (Math.Pow(dx, 2) + Math.Pow(dy, 2));
        value = value < 0 ? 0 : value > 1 ? 1 : value; // ограничиваем прямоугольником
        var distance = Math.Sqrt(Math.Pow(x - x1 - dx * value, 2) + Math.Pow(y - y1 - dy * value, 2));
        return distance;
    }
}