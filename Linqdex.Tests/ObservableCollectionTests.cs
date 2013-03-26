using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Linqdex.Tests
{
    public class ObservableCollectionTests
    {
        [Test]
        public void CanAddAnItemAndRetreiveIt()
        {
            var observableCollection = new ObservableCollection<Person>();
            var expected = new Person() { Name = "Arnold" };
            observableCollection.Add(expected);
            var actual = observableCollection.Query().Single(p => p.Name == "Arnold");
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void CanRemoveAnItemAndNotRetreiveIt()
        {
            var observableCollection = new Linqdex<Person>();
            var expected = new Person() { Name = "Arnold" };
            observableCollection.Add(expected);
            observableCollection.Remove(expected);
            var actual = observableCollection.Query().SingleOrDefault(p => p.Name == "Arnold");
            Assert.Null(actual);
        }
    }
}
