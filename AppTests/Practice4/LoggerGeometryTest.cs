using NUnit.Framework;
using System.Collections.Generic;
using App;
using App.Practice4;
using NUnit.Framework.Internal;

namespace AppTests
{
    [TestFixture]
    public class LoggerGeometryTest
    {
        private Vertex[] verticesForRectangle;
        private Vertex[] verticesForTriangle;

        /*
        [SetUp]
        public void Initialize()
        {
            verticesForRectangle = new[]
            {
                new Vertex(1.023, 1.023),
                new Vertex(2.023, 2.023),
                new Vertex(3.023, 3.023),
                new Vertex(4.023, 4.023),
            };

            verticesForTriangle = new[]
            {
                new Vertex(1.023, 1.023),
                new Vertex(2.023, 2.023),
                new Vertex(3.023, 3.023),
            };
        }
        */
        
        public static IEnumerable<TestCaseData> GeometryTestCases()
        {
            var v1 = new Vertex(1.0, 1.0);
            var v2 = new Vertex(1.0, 4.0);
            var v3 = new Vertex(4.0, 4.0);
            var v4 = new Vertex(4.0, 1.0);
            yield return new TestCaseData(
                new Triangle(new[]
                {
                    v1,
                    v2,
                    v3,
                }), "Треугольник").SetName("LoggerTestForTriangle");

            yield return new TestCaseData(
                new Rectangle(new[]
                {
                    v1, v2, v3, v4,
                }), "Прямоугольник").SetName("LoggerTestForRectangle");
            
            yield return new TestCaseData(
                new Circle(), "Undefined figure").SetName("LoggerTestForUndefinedFigure");
            
            yield return new TestCaseData(
                new Square(new[]
                {
                    v1, v2, v3, v4,
                }), "Квадрат").SetName("LoggerTestForSquare");
        }

        [Test]
        [TestCaseSource(nameof(GeometryTestCases))]
        public void Logger_ShouldLogCorrectShapeName(IGeometry geomShape, string expectedName)
        {
            string actual = LoggerGeometry.LogGeometry(geomShape); // Предположим, IGeometry имеет метод GetName()
            Assert.AreEqual(expectedName, actual);
        }
    }
}