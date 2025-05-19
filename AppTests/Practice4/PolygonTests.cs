using NUnit.Framework;
using System;
using App;

namespace AppTests
{
    [TestFixture]
    public class PolygonTests
    {
        [Test]
        [TestCase(0, 0, 3, 4, 5, TestName = "DistanceBetweenPoints_3and4_ShouldBe5")]
        [TestCase(1, 2, 1, 2, 0, TestName = "DistanceBetweenSamePoints_ShouldBe0")]
        public void VertexDistanceTo_ShouldReturnCorrectDistance(double x1, double y1, double x2, double y2, double expected)
        {
            var v1 = new Vertex(x1, y1);
            var v2 = new Vertex(x2, y2);
            Console.WriteLine(v1.ToString());
            
            var distance = v1.DistanceTo(v2);

            Assert.AreEqual(expected, distance, 1e-6);
        }

        [Test]
        public void RectangleArea_ShouldReturnCorrectArea()
        {
            var vertices = new[]
            {
                new Vertex(0, 0),
                new Vertex(4, 0),
                new Vertex(4, 3),
                new Vertex(0, 3)
            };

            var rectangle = new Rectangle(vertices);

            Assert.AreEqual(12, rectangle.CalculateArea(), 1e-6);
            Assert.AreEqual(4, rectangle.VertexCount);
        }

        [Test]
        public void Rectangle_WithInvalidVertexCount_ShouldThrow()
        {
            var vertices = new[]
            {
                new Vertex(0, 0),
                new Vertex(1, 0),
                new Vertex(1, 1)
            };

            Assert.Throws<Exception>(() => new Rectangle(vertices));
        }

        [Test]
        public void Square_WithEqualSides_ShouldWork()
        {
            var vertices = new[]
            {
                new Vertex(0, 0),
                new Vertex(2, 0),
                new Vertex(2, 2),
                new Vertex(0, 2)
            };

            var square = new Square(vertices);

            Assert.AreEqual(4, square.CalculateArea(), 1e-6);
        }

        [Test]
        public void Square_WithUnequalSides_ShouldThrow()
        {
            var vertices = new[]
            {
                new Vertex(0, 0),
                new Vertex(4, 0),
                new Vertex(4, 2),
                new Vertex(0, 2)
            };

            Assert.Throws<Exception>(() => new Square(vertices));
        }

        [Test]
        public void TriangleArea_ShouldReturnCorrectArea()
        {
            var vertices = new[]
            {
                new Vertex(0, 0),
                new Vertex(4, 0),
                new Vertex(0, 3)
            };

            var triangle = new Triangle(vertices);

            Assert.AreEqual(6, triangle.CalculateArea(), 1e-6);
            Assert.AreEqual(3, triangle.VertexCount);
        }
    }
}
