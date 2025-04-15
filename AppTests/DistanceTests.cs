using App;

namespace AppTests;

public class DistanceTests 
{
    //TODO напишите свои тесты
    [TestCase(1, 1, 1, 1, 1, 1, 0)]
    [TestCase(-30, 0, 30,  0, 0, -30, 42.4264)]
    
    [TestCase(0, 0, 0, 0, 10, 0, 0)]
    [TestCase(5, 5, 0, 0, 10, 0, 5)]
    [TestCase(-5, 0, 0, 0, 10, 0, 5)]
    public void TestPasses_When_Result_Correct(
        // позиция курсора
        double x, double y,
        // отрезок
        double x1, double y1, double x2, double y2,
        double expected)
    {
        var actual = Distance.DistanceToSegment(x, y, x1, y1, x2, y2);
        Assert.That(actual, Is.EqualTo(expected).Within(0.001));
    }
}