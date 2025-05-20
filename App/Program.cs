using App.Practice4;

namespace App;

public static class Program
{
    public static void Main()
    {  
        var vertex1 = new Vertex(1.3450004, 2.32000);
        var vertex2 = new Vertex(1.99, 5.32000);
        var vertex3 = new Vertex(2.3450004, 3.32000);
        var vertices = new[] { vertex1, vertex2, vertex3 };
        Console.WriteLine(vertex1.ToString());
        var triangle = new Triangle(vertices);
        var circle = new Circle();
        var square = new Square(new[]
        {
            new Vertex(1.0, 1.0),
            new Vertex(1.0, 4.0),
            new Vertex(4.0, 4.0),
            new Vertex(4.0, 1.0),
        });
        Console.WriteLine(LoggerGeometry.LogGeometry(circle));
        Console.WriteLine(LoggerGeometry.LogGeometry(triangle));
        Console.WriteLine(LoggerGeometry.LogGeometry(square));
    }
}