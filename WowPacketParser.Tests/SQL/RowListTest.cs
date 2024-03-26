using NUnit.Framework;
using WowPacketParser.SQL;

#pragma warning disable 414
#pragma warning disable 649
#pragma warning disable 169

namespace WowPacketParser.Tests.SQL
{
    [TestFixture]
    public class RowListTest
    {
        [DBTableName("testData")]
        private class TestDataModel : IDataModel
        {
            [DBFieldName("ID", true)]
            public int? ID;

            [DBFieldName("TestInt1", true)]
            public int? TestInt1;

            [DBFieldName("TestInt2")]
            public int? TestInt2;

            [DBFieldName("TestString1")]
            public string TestString1;
        }

        [SetUp]
        public void Initialize()
        {
            _condList = new RowList<TestDataModel>
            {
                new TestDataModel {ID = 1, TestInt1 = 2},
                new TestDataModel {ID = 3, TestInt1 = 4}
            };
        }

        [TearDown]
        public void Cleanup()
        {
            _condList = null;
        }

        private RowList<TestDataModel> _condList;

        [Test]
        public void TestAdd()
        {
            _condList.Add(new TestDataModel());
            Assert.That(2, Is.EqualTo(_condList.Count));

            _condList.Add(new TestDataModel {ID = 3, TestInt1 = 5});
            _condList.Add(new TestDataModel {ID = 4, TestInt1 = 6});
            Assert.That(4, Is.EqualTo(_condList.Count));

            _condList.Add(new TestDataModel {ID = 3, TestInt1 = 5});
            Assert.That(4, Is.EqualTo(_condList.Count));
        }

        [Test]
        public void TestClear()
        {
            _condList.Clear();

            Assert.That(0, Is.EqualTo(_condList.Count));
        }

        [Test]
        public void TestCount()
        {
            Assert.That(2, Is.EqualTo(_condList.Count));
        }

        [Test]
        public void TestEnumerator()
        {
            Assert.That(_condList.GetEnumerator(), Is.Not.Null);

            foreach (Row<TestDataModel> c in _condList)
                Assert.That(c, Is.Not.Null);
        }
    }
}
