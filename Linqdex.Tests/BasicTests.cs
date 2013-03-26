using System.Linq;
using NUnit.Framework;

namespace Linqdex.Tests
{
    public class BasicTests
    {
        [Test]
        public void CanAddAnItemAndRetreiveIt()
        {
            var indexedList = new Linqdex<Person>();
            var expected = new Person() { Name = "Arnold" };
            indexedList.Add(expected);
            var actual = indexedList.Query().Single(p => p.Name == "Arnold");
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void CanRemoveAnItemAndNotRetreiveIt()
        {
            var indexedList = new Linqdex<Person>();
            var expected = new Person() { Name = "Arnold" };
            indexedList.Add(expected);
            indexedList.Remove(expected);
            var actual = indexedList.Query().SingleOrDefault(p => p.Name == "Arnold");
            Assert.Null(actual);
        }

        [Test]
        public void CanChangeAnItemAndRetreiveIt()
        {
            var indexedList = new Linqdex<Person>();
            var expected = new Person() { Name = "Arnold" };
            indexedList.Add(expected);

            expected.Name = "Marvin";

            var actual = indexedList.Query().SingleOrDefault(p => p.Name == "Marvin");
            Assert.NotNull(actual);
        }
        

    }
}