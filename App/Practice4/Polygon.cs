public interface I2DVertex
{
    double X { get; set; }
    double Y { get; set; } 
}

public interface IGeometry
{
    double CalculateArea();
    int VertexCount { get; }

    public string FigureName()
    {
        return "Undefined figure.";
    }
}

public class Vertex : I2DVertex
{
    public double X { get; set; }
    public double Y { get; set; }

    public Vertex(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double DistanceTo(I2DVertex other)
    {
        return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
    }

    public override string ToString()
    {
        string xValue = X.ToString("F3");
        string yValue = Y.ToString("0.000");
        return $"x = {xValue}; y = {yValue}";
    }
}


public class Rectangle : IGeometry  
{
    public Vertex[] Vertices { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
        
    public int VertexCount { get; } = 4;

    public Rectangle(Vertex[] vertices)
    {
        if (vertices.Length != 4)
        {
            throw new Exception("Vertex count must be 4 elements");
        }
        Vertices = vertices;
        CalculateSides();
    }
        
    public double CalculateArea()
    { 
        return Width * Height;
    }

    void CalculateSides()
    {
        var sides = new List<double>();
        for (var i = 1; i < VertexCount; i++)
        {
            sides.Add(Vertices[0].DistanceTo(Vertices[i]));
        }     
        sides.Sort();
        Height = sides[0];
        Width = sides[1];
    }

    public string FigureName()
    {
        return "Прямоугольник";
    }
}

public class Square : Rectangle  
{
    public Square(Vertex[] vertices) : base(vertices)
    {
        if (Width != Height)
        {
            throw new Exception("Square width and height must match.");
        }
    }

    public new string FigureName()
    {
        return "Квадрат";
    }
}

public class Triangle : IGeometry
    {
        public Vertex[] Vertices { get; set; }
        
        public int VertexCount { get; } = 3;

        public Triangle(Vertex[] vertices)
        {
            Vertices = vertices;
        }
        
        public double CalculateArea()
        {
            var area = 0.0d;
            for (var i = 0; i < VertexCount; i++)
            {
                area += Vertices[i].X * (Vertices[(i + 1) % 3].Y - Vertices[(i + 2) % 3].Y); 
            }
            return area * 0.5;
        }

        public string FigureName()
        {
            return "Треугольник";
        }
    }

public class Circle : IGeometry
{
    public int VertexCount { get; } = 0;

    public double CalculateArea()
    {
        throw new NotImplementedException();
    }
}