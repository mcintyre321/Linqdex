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
            var actual = observableCollection.ToIndexedQueryable().Single(p => p.Name == "Arnold");
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void UpdateAnItemAndRetreiveIt() 
        {
            var observableCollection = new ObservableCollection<Person>();
            var expected = new Person() { Name = "Arnold" };
            observableCollection.Add(expected);
            expected.Name = "Todd";
            var actual = observableCollection.ToIndexedQueryable().Single(p => p.Name == "Todd");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CanRemoveAnItemAndNotRetreiveIt()
        {
            var observableCollection = new Linqdex<Person>();
            var expected = new Person() { Name = "Arnold" };
            observableCollection.Add(expected);
            observableCollection.Remove(expected);
            var actual = observableCollection.ToIndexedQuery().SingleOrDefault(p => p.Name == "Arnold");
            Assert.Null(actual);
        }
        [Test]
        public void CanUpdateAnItemAndNotRetreiveIt()
        {
            var observableCollection = new Linqdex<Person>();
            var expected = new Person() { Name = "Arnold" };
            observableCollection.Add(expected);
            expected.Name = "Todd";
            var actual = observableCollection.ToIndexedQuery().SingleOrDefault(p => p.Name == "Arnold");
            Assert.Null(actual);
        }

    }
}
