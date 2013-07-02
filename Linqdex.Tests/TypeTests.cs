using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Linqdex.Tests
{
    public class TypeTests
    {
        public class TypeWithADateTime
        {
            public DateTime Now { get; set; }
        }
        [Test]
        public void CanStoreAndFindTypeWithADateTime()
        {
            var item = new TypeWithADateTime() { Now = DateTime.Now };
            var index = new Linqdex<TypeWithADateTime>();
            index.Add(item);
            Assert.AreEqual(item, index.ToIndexedQuery().Where(t => t.Now > DateTime.Today).ToArray().Single());
        }
        public class TypeWithADateTimeOffset
        {
            public DateTimeOffset Now { get; set; }
        }
        [Test]
        public void CanStoreAndFindTypeWithADateTimeOffset()
        {
            var item = new TypeWithADateTimeOffset() { Now = DateTimeOffset.Now };
            var index = new Linqdex<TypeWithADateTimeOffset>();
            index.Add(item);
            Assert.AreEqual(item, index.ToIndexedQuery().Where(t => t.Now > DateTimeOffset.Now.Date).ToArray().Single());
        }

        public class TypeWithANullableDateTimeOffset
        {
            public DateTimeOffset? Now { get; set; }
        }

        [Test]
        public void CanStoreAndFindTypeWithANullableDateTimeOffset()
        {
            var item = new TypeWithANullableDateTimeOffset() { Now = null };
            var dateTimeOffset = DateTimeOffset.Now;
            var item2 = new TypeWithANullableDateTimeOffset() { Now = dateTimeOffset };

            var index = new Linqdex<TypeWithANullableDateTimeOffset>();
            index.Add(item);
            index.Add(item2);
            Assert.AreEqual(item, index.ToIndexedQuery().Where(t => t.Now == null).ToArray().Single());
            
            Assert.AreEqual(item2, index.ToIndexedQuery().Where(t => t.Now == dateTimeOffset).ToArray().Single());
        }

        public class TypeWithAString
        {
            public string String { get; set; }
        }

        [Test]
        public void CanStoreAndFindTypeWithString()
        {
            var item = new TypeWithAString() { String = null };
            var item2 = new TypeWithAString() { String = "asdf" };

            var index = new Linqdex<TypeWithAString>();
            index.Add(item);
            index.Add(item2);
            Assert.AreEqual(item, index.ToIndexedQuery().Where(t => t.String == null).ToArray().Single());

            Assert.AreEqual(item2, index.ToIndexedQuery().Where(t => t.String == "asdf").ToArray().Single());
        }

    }

}
