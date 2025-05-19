namespace App.Practice4;

public class ClockWiseComparer : IComparer<Vertex>
{
    
    public int Compare(Vertex leftVertex, Vertex rightVertex)
    {
        if (leftVertex is null || rightVertex is null)
            throw new ArgumentNullException("x and y cannot be null");
        var leftVertexAtan = Math.Atan2(leftVertex.Y, leftVertex.X);
        var rightVertexAtan = Math.Atan2(rightVertex.Y, rightVertex.X);
        
        return rightVertexAtan.CompareTo(leftVertexAtan);
    }
}