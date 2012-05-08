using System.Linq;
using WowPacketParser.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace WowPacketParser.Tests.Misc
{
    [TestClass]
    public class ConcurrentMultiDictionaryTest
    {
        private ConcurrentMultiDictionary<string, int> _dictionary;

        [TestInitialize]
        public void Initialize()
        {
            _dictionary = new ConcurrentMultiDictionary<string, int>();
            _dictionary.TryAdd("Foo", 42);
            _dictionary.TryAdd("Foo", 100);
            _dictionary.TryAdd("Bar", 1);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dictionary = null;
        }

        [TestMethod]
        public void TestContainsValue()
        {
            Assert.IsTrue(_dictionary.ContainsValue("Foo", 42));
            Assert.IsTrue(_dictionary.ContainsValue("Bar", 1));
            Assert.IsTrue(_dictionary.ContainsValue("Foo", 100));

            Assert.IsFalse(_dictionary.ContainsValue("Baz", -1));
            Assert.IsFalse(_dictionary.ContainsValue("Bar", 0));
        }

        [TestMethod]
        public void TestGetValues()
        {
            var listExFoo = new List<int> { 42, 100 };
            var listExBar = new List<int> { 1 };
            var listExBaz = new List<int>();
            var listExFoo2 = new List<int> { 42 };

            var listFoo = _dictionary.GetValues("Foo");
            var listBar = _dictionary.GetValues("Bar");
            var listBaz = _dictionary.GetValues("Baz");

            Assert.IsFalse(listExFoo.Except(listFoo).Any() || listFoo.Except(listExFoo).Any());
            Assert.IsFalse(listExBar.Except(listBar).Any() || listBar.Except(listExBar).Any());
            Assert.IsFalse(listExBaz.Except(listBaz).Any() || listBaz.Except(listExBaz).Any());

            Assert.IsTrue(listExFoo2.Except(listFoo).Any() || listFoo.Except(listExFoo2).Any());
        }

        [TestMethod]
        public void TestTryAdd()
        {
            Assert.IsTrue(_dictionary.TryAdd("Baz", 84));
            Assert.IsTrue(_dictionary.ContainsValue("Baz", 84));
            Assert.IsTrue(_dictionary.ContainsKey("Baz"));
        }
    }
}
