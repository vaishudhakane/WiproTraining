
using Calculator1;
using NUnit.Framework;
namespace Calci
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void AddTest()
        {
            var c = new Calculator();
            var  result = c.Add(2, 3);
            Assert.That(result,Is.EqualTo(5));

        }

    }
}
