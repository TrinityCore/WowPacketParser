using NUnit.Framework;
using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Tests.Misc
{
    [TestFixture]
    public class BiDictionaryTest
    {
        private BiDictionary<int, double> _biDictionary;

        [SetUp]
        public void Initialize()
        {
            _biDictionary = new BiDictionary<int, double>
            {
                {10, 5.0},
                {int.MaxValue, double.MinValue},
                {int.MinValue, double.MaxValue}
            };
        }

        [TearDown]
        public void Cleanup()
        {
            _biDictionary = null;
        }

        [Test]
        public void TestCount()
        {
            Assert.AreEqual(3, _biDictionary.Count);
        }

        [Test]
        public void TestClear()
        {
            _biDictionary.Clear();

            Assert.AreEqual(0, _biDictionary.Count);
        }

        [Test]
        public void TestIndexers()
        {
            Assert.AreEqual(5.0, _biDictionary[10]);
            Assert.AreEqual(int.MaxValue, _biDictionary[double.MinValue]);

            Assert.AreEqual(default(int), _biDictionary[1.0]);
            Assert.AreEqual(default(double), _biDictionary[1]);
        }

        [Test]
        public void TestGetters()
        {
            Assert.AreEqual(5.0, _biDictionary.GetByFirst(10));
            Assert.AreEqual(int.MaxValue, _biDictionary.GetBySecond(double.MinValue));

            Assert.AreEqual(default(double), _biDictionary.GetByFirst(1));
            Assert.AreEqual(default(int), _biDictionary.GetBySecond(1.0));
        }

        [Test]
        public void TestTryGetters()
        {
            int first;
            double second;

            Assert.IsTrue(_biDictionary.TryGetByFirst(10, out second));
            Assert.IsTrue(_biDictionary.TryGetBySecond(double.MinValue, out first));

            Assert.AreEqual(5.0, second);
            Assert.AreEqual(int.MaxValue, first);

            Assert.IsFalse(_biDictionary.TryGetByFirst(1, out second));
            Assert.IsFalse(_biDictionary.TryGetBySecond(1.0, out first));

            Assert.IsTrue(_biDictionary.TryGetValue(10, out second));
            Assert.IsTrue(_biDictionary.TryGetValue(double.MinValue, out first));

            Assert.AreEqual(5.0, second);
            Assert.AreEqual(int.MaxValue, first);

            Assert.IsFalse(_biDictionary.TryGetValue(1, out second));
            Assert.IsFalse(_biDictionary.TryGetValue(1.0, out first));
        }

        [Test]
        public void TestSetters()
        {
            _biDictionary[10] = 6.0;
            Assert.AreEqual(6.0, _biDictionary.GetByFirst(10));

            _biDictionary[7.0] = 10;
            Assert.AreEqual(10, _biDictionary.GetBySecond(7.0));
        }

        [Test]
        public void TestEnumerator()
        {
            Assert.IsNotNull(_biDictionary.GetEnumerator());

            foreach (KeyValuePair<int, double> keyValuePair in _biDictionary)
                Assert.IsNotNull(keyValuePair);
        }

        [Test]
        public void TestAdd()
        {
            _biDictionary.Add(default(int), default(double));

            _biDictionary.Add(new KeyValuePair<int, double>(1, 2.0));
            _biDictionary.Add(new KeyValuePair<double, int>(3.0, 4));

            Assert.AreEqual(6, _biDictionary.Count);
            Assert.AreEqual(default(double), _biDictionary[default(int)]);
        }

        [Test]
        public void TestRemove()
        {
            _biDictionary.Add(1, 2.0);
            _biDictionary.Add(3, 4.0);
            _biDictionary.Add(4, 6.0);
            _biDictionary.Add(7, 8.0);

            Assert.IsTrue(_biDictionary.Remove(1));
            Assert.IsTrue(_biDictionary.Remove(4.0));

            Assert.AreEqual(5, _biDictionary.Count);

            Assert.IsFalse(_biDictionary.Remove(1));
            Assert.IsFalse(_biDictionary.Remove(4.0));

            Assert.IsTrue(_biDictionary.Remove(new KeyValuePair<int, double>(4, 6.0)));
            Assert.IsTrue(_biDictionary.Remove(new KeyValuePair<double, int>(8.0, 7)));

            Assert.AreEqual(3, _biDictionary.Count);
        }

        [Test]
        public void TestContains()
        {
            Assert.IsTrue(_biDictionary.Contains(new KeyValuePair<int, double>(10, 5.0)));
            Assert.IsTrue(_biDictionary.Contains(new KeyValuePair<double, int>(5.0, 10)));

            Assert.IsTrue(_biDictionary.ContainsKey(10));
            Assert.IsTrue(_biDictionary.ContainsValue(5.0));

            Assert.IsTrue(_biDictionary.ContainsKey(5.0));
            Assert.IsTrue(_biDictionary.ContainsValue(10));
        }
    }
}
