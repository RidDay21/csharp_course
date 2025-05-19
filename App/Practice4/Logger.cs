namespace App.Practice4;

public static class Logger
{
    public static string LogGeometry(IGeometry geom)
    {
        return geom.FigureName();
    }
}