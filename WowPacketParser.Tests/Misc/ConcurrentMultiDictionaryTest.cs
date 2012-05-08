using NUnit.Framework;
using WowPacketParser.Misc;
using System.Collections.Generic;

namespace WowPacketParser.Tests.Misc
{
    [TestFixture]
    public class ConcurrentMultiDictionaryTest
    {
        private ConcurrentMultiDictionary<string, int> _dictionary;

        [SetUp]
        public void Initialize()
        {
            _dictionary = new ConcurrentMultiDictionary<string, int>();
            _dictionary.TryAdd("Foo", 42);
            _dictionary.TryAdd("Foo", 100);
            _dictionary.TryAdd("Bar", 1);
        }

        [TearDown]
        public void Cleanup()
        {
            _dictionary = null;
        }

        [Test]
        public void TestContainsValue()
        {
            Assert.IsTrue(_dictionary.ContainsValue("Foo", 42));
            Assert.IsTrue(_dictionary.ContainsValue("Bar", 1));
            Assert.IsTrue(_dictionary.ContainsValue("Foo", 100));

            Assert.IsFalse(_dictionary.ContainsValue("Baz", -1));
            Assert.IsFalse(_dictionary.ContainsValue("Bar", 0));
        }

        [Test]
        public void TestGetValues()
        {
            var listExFoo = new List<int> { 42, 100 };
            var listExBar = new List<int> { 1 };
            var listExBaz = new List<int>();

            var listFoo = _dictionary.GetValues("Foo");
            var listBar = _dictionary.GetValues("Bar");
            var listBaz = _dictionary.GetValues("Baz");

            CollectionAssert.AreEqual(listExFoo, listFoo);
            CollectionAssert.AreEqual(listExBar, listBar);
            CollectionAssert.AreEqual(listExBaz, listBaz);

            CollectionAssert.AreNotEqual(listExBar, listFoo);
        }

        [Test]
        public void TestTryAdd()
        {
            Assert.IsTrue(_dictionary.TryAdd("Baz", 84));
            Assert.IsTrue(_dictionary.ContainsValue("Baz", 84));
            Assert.IsTrue(_dictionary.ContainsKey("Baz"));
        }
    }
}
