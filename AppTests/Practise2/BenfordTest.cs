using App.Practise2;

namespace AppTests.Practise2;

[TestFixture]
[TestOf(typeof(Benford))]
public class BenfordTest
{

    [Test]
    [TestCase("0 1 2 3 4 5 6 7 8 9", new int[] {1,1,1,1,1,1,1,1,1})]
    [TestCase("0 01 02 03 04 05 06 07 08 09", new int[] {0,0,0,0,0,0,0,0,0})]
    [TestCase("Price: 1234. And my level of English is B2", new int[] {1,0,0,0,0,0,0,0,0})]
    [TestCase("N0 5nu5mbe4rs here12", new int[] {0,0,0,0,0,0,0,0,0})]
    [TestCase("100 101 102 200 201 300", new int[] {3,2,1,0,0,0,0,0,0})]
    [TestCase("a123,431;-321 405", new int[] {0,0,1,2,0,0,0,0,0})] //добавлен тест
    [TestCase("123_456_789", new int[] {1,0,0,1,0,0,1,0,0})]
    [TestCase("123,456,789",  new int[] {1,0,0,1,0,0,1,0,0})]

    public void TestingBenfordMethod(string text, int[] expected)
    {
        var array = Benford.GetBenfordStatistics(text);
        
        Assert.AreEqual(expected, array);
    }
}