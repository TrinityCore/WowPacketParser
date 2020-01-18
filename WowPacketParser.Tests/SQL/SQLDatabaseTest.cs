using NUnit.Framework;
using WowPacketParser.SQL;

#pragma warning disable 414
#pragma warning disable 649
#pragma warning disable 169

namespace WowPacketParser.Tests.SQL
{
    [TestFixture]
    public class SQLDatabaseTest
    {
        [DBTableName("testdata")]
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

        // This test isn't suited for running after each compile. Enable it if needed.
        [Test, Ignore("Ignore TestGet")]
        public void TestGet()
        {
            SQLConnector.Enabled = true;
            SQLConnector.Connect();

            var cond = new RowList<TestDataModel>
            {
                new TestDataModel {ID = 1, TestInt1 = 10, TestString1 = "a"},
                new TestDataModel {ID = 1, TestInt1 = 20, TestInt2 = 6},
                new TestDataModel {ID = 2, TestInt1 = 11, TestInt2 = 4},
                new TestDataModel {ID = 2, TestInt1 = 21, TestString1 = "b"}
            };
            var data = SQLDatabase.Get(cond, "wpp_test");
            Assert.NotNull(data);
        }
    }
}
