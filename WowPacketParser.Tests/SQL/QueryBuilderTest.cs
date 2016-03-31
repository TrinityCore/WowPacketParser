using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using WowPacketParser.SQL;

#pragma warning disable 414
#pragma warning disable 649
#pragma warning disable 169

namespace WowPacketParser.Tests.SQL
{
    [TestFixture]
    public class QueryBuilderTest
    {
        private class TestDataOnePK : IDataModel
        {
            [DBFieldName("ID", true)]
            public int? ID;

            [DBFieldName("TestInt1")]
            public int? TestInt1;

            [DBFieldName("TestInt2")]
            public int? TestInt2;

            [DBFieldName("TestString1")]
            public string TestString1;
        }

        private class TestDataTwoPK : IDataModel
        {
            [DBFieldName("ID", true)]
            public int? ID;

            [DBFieldName("TestInt1", true)]
            public int? TestInt1;

            [DBFieldName("TestInts", 2)]
            public int?[] TestInts;

            [DBFieldName("TestString1")]
            public string TestString1;
        }

        [SetUp]
        public void SetUp()
        {
            _conditionsOnePk = new RowList<TestDataOnePK>
            {
                new TestDataOnePK {ID = 1, TestInt1 = 2, TestString1 = "string1"},
                new TestDataOnePK {ID = 2, TestInt1 = 3}
            };

            _valuesOnePk = new RowList<TestDataOnePK>
            {
                new TestDataOnePK {ID = 4, TestInt1 = 5, TestString1 = "string2"},
                new TestDataOnePK {ID = 6, TestInt1 = 7}
            };

            _conditionsTwoPk = new RowList<TestDataTwoPK>
            {
                new TestDataTwoPK {ID = 10, TestInt1 = 20, TestString1 = "string10"},
                new TestDataTwoPK {ID = 20, TestInt1 = 30}
            };

            _valuesTwoPk = new RowList<TestDataTwoPK>
            {
                new TestDataTwoPK {ID = 40, TestInt1 = 50, TestString1 = "string20"},
                new TestDataTwoPK {ID = 60, TestInt1 = 70}
            };
        }

        [TearDown]
        public void TearDown()
        {
            _conditionsOnePk = null;
            _valuesOnePk = null;

            _conditionsTwoPk = null;
            _valuesTwoPk = null;
        }

        private RowList<TestDataOnePK> _conditionsOnePk;
        private RowList<TestDataOnePK> _valuesOnePk;

        private RowList<TestDataTwoPK> _conditionsTwoPk;
        private RowList<TestDataTwoPK> _valuesTwoPk;

        [Test]
        public void TestSQLSelectNoCond()
        {
            Assert.AreEqual("SELECT `ID`, `TestInt1`, `TestInt2`, `TestString1` FROM world.`test_data_one_p_k`",
                new SQLSelect<TestDataOnePK>().Build());

            Assert.AreEqual("SELECT `ID`, `TestInt1`, `TestInts1`, `TestInts2`, `TestString1` FROM world.`test_data_two_p_k`",
                new SQLSelect<TestDataTwoPK>().Build());
        }

        [Test]
        public void TestSQLSelectWithCond()
        {
            Assert.AreEqual(
                "SELECT `ID`, `TestInt1`, `TestInt2`, `TestString1` FROM world.`test_data_one_p_k` WHERE (`ID`=1 AND `TestInt1`=2 AND `TestString1`='string1') OR (`ID`=2 AND `TestInt1`=3)",
                new SQLSelect<TestDataOnePK>(_conditionsOnePk, onlyPrimaryKeys: false).Build());

            Assert.AreEqual(
                "SELECT `ID`, `TestInt1`, `TestInt2`, `TestString1` FROM world.`test_data_one_p_k` WHERE `ID` IN (1, 2)",
                new SQLSelect<TestDataOnePK>(_conditionsOnePk).Build());
        }

        [Test]
        public void TestSQLWhere()
        {
            var where = new SQLWhere<TestDataOnePK>(_conditionsOnePk);

            Assert.AreEqual(
                "(`ID`=1 AND `TestInt1`=2 AND `TestString1`='string1') OR (`ID`=2 AND `TestInt1`=3)",
                where.Build());
        }

        [Test]
        public void TestSQLWhereOnlyPrimaryKey()
        {
            var whereOnePk = new SQLWhere<TestDataOnePK>(_conditionsOnePk, true);
            Assert.AreEqual("`ID` IN (1, 2)", whereOnePk.Build());

            var whereTwoPk = new SQLWhere<TestDataTwoPK>(_conditionsTwoPk, true);
            Assert.AreEqual("(`ID`=10 AND `TestInt1`=20) OR (`ID`=20 AND `TestInt1`=30)", whereTwoPk.Build());
        }

        [Test]
        public void TestSQLUpdate()
        {

            var values = new Dictionary<Row<TestDataOnePK>, RowList<TestDataOnePK>>
            {
                {_valuesOnePk.ElementAt(0), _conditionsOnePk},
                {_valuesOnePk.ElementAt(1), _conditionsOnePk}
            };

            var update = new SQLUpdate<TestDataOnePK>(values);
            Assert.AreEqual("UPDATE `test_data_one_p_k` SET `ID`=4, `TestInt1`=5, `TestString1`='string2' WHERE `ID` IN (1, 2);" + Environment.NewLine +
                            "UPDATE `test_data_one_p_k` SET `ID`=6, `TestInt1`=7 WHERE `ID` IN (1, 2);" + Environment.NewLine,
                update.Build());
        }

        [Test]
        public void TestSQLInsert()
        {
            Assert.AreEqual("INSERT INTO `test_data_one_p_k` (`ID`, `TestInt1`, `TestInt2`, `TestString1`) VALUES" + Environment.NewLine +
                            "(4, 5, UNKNOWN, 'string2')," + Environment.NewLine +
                            "(6, 7, UNKNOWN, UNKNOWN);" + Environment.NewLine,
                            new SQLInsert<TestDataOnePK>(_valuesOnePk, false).Build());
        }
    }
}
