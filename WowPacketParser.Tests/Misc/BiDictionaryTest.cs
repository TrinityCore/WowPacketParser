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
            Assert.That(3, Is.EqualTo(_biDictionary.Count));
        }

        [Test]
        public void TestClear()
        {
            _biDictionary.Clear();

            Assert.That(0, Is.EqualTo(_biDictionary.Count));
        }

        [Test]
        public void TestIndexers()
        {
            Assert.That(5.0, Is.EqualTo(_biDictionary[10]));
            Assert.That(int.MaxValue,Is.EqualTo( _biDictionary[double.MinValue]));

            Assert.That(default(int), Is.EqualTo(_biDictionary[1.0]));
            Assert.That(default(double), Is.EqualTo(_biDictionary[1]));
        }

        [Test]
        public void TestGetters()
        {
            Assert.That(5.0, Is.EqualTo(_biDictionary.GetByFirst(10)));
            Assert.That(int.MaxValue, Is.EqualTo(_biDictionary.GetBySecond(double.MinValue)));

            Assert.That(default(double), Is.EqualTo(_biDictionary.GetByFirst(1)));
            Assert.That(default(int), Is.EqualTo(_biDictionary.GetBySecond(1.0)));
        }

        [Test]
        public void TestTryGetters()
        {
            int first;
            double second;

            Assert.That(_biDictionary.TryGetByFirst(10, out second), Is.True);
            Assert.That(_biDictionary.TryGetBySecond(double.MinValue, out first), Is.True);

            Assert.That(5.0, Is.EqualTo(second));
            Assert.That(int.MaxValue, Is.EqualTo(first));

            Assert.That(_biDictionary.TryGetByFirst(1, out second), Is.False);
            Assert.That(_biDictionary.TryGetBySecond(1.0, out first), Is.False);

            Assert.That(_biDictionary.TryGetValue(10, out second), Is.True);
            Assert.That(_biDictionary.TryGetValue(double.MinValue, out first), Is.True);

            Assert.That(5.0, Is.EqualTo(second));
            Assert.That(int.MaxValue,Is.EqualTo(first));

            Assert.That(_biDictionary.TryGetValue(1, out second), Is.False);
            Assert.That(_biDictionary.TryGetValue(1.0, out first), Is.False);
        }

        [Test]
        public void TestSetters()
        {
            _biDictionary[10] = 6.0;
            Assert.That(6.0, Is.EqualTo(_biDictionary.GetByFirst(10)));

            _biDictionary[7.0] = 10;
            Assert.That(10, Is.EqualTo(_biDictionary.GetBySecond(7.0)));
        }

        [Test]
        public void TestEnumerator()
        {
            Assert.That(_biDictionary.GetEnumerator(), Is.Not.Null);

            foreach (KeyValuePair<int, double> keyValuePair in _biDictionary)
                Assert.That(keyValuePair, Is.Not.Null);
        }

        [Test]
        public void TestAdd()
        {
            _biDictionary.Add(default(int), default(double));

            _biDictionary.Add(new KeyValuePair<int, double>(1, 2.0));
            _biDictionary.Add(new KeyValuePair<double, int>(3.0, 4));

            Assert.That(6, Is.EqualTo(_biDictionary.Count));
            Assert.That(default(double), Is.EqualTo(_biDictionary[default(int)]));
        }

        [Test]
        public void TestRemove()
        {
            _biDictionary.Add(1, 2.0);
            _biDictionary.Add(3, 4.0);
            _biDictionary.Add(4, 6.0);
            _biDictionary.Add(7, 8.0);

            Assert.That(_biDictionary.Remove(1), Is.True);
            Assert.That(_biDictionary.Remove(4.0), Is.True);

            Assert.That(5, Is.EqualTo(_biDictionary.Count));

            Assert.That(_biDictionary.Remove(1), Is.False);
            Assert.That(_biDictionary.Remove(4.0), Is.False);

            Assert.That(_biDictionary.Remove(new KeyValuePair<int, double>(4, 6.0)), Is.True);
            Assert.That(_biDictionary.Remove(new KeyValuePair<double, int>(8.0, 7)), Is.True);

            Assert.That(3, Is.EqualTo(_biDictionary.Count));
        }

        [Test]
        public void TestContains()
        {
            Assert.That(_biDictionary.Contains(new KeyValuePair<int, double>(10, 5.0)), Is.True);
            Assert.That(_biDictionary.Contains(new KeyValuePair<double, int>(5.0, 10)), Is.True);

            Assert.That(_biDictionary.ContainsKey(10), Is.True);
            Assert.That(_biDictionary.ContainsValue(5.0), Is.True);

            Assert.That(_biDictionary.ContainsKey(5.0), Is.True);
            Assert.That(_biDictionary.ContainsValue(10), Is.True);
        }
    }
}
