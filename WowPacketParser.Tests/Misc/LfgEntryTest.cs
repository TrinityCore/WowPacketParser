using NUnit.Framework;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Tests.Misc
{
    [TestFixture]
    public class LfgEntryTest
    {
        [Test]
        public void TestConstructor()
        {
            var lfg = new LfgEntry(1);

            Assert.IsNotNull(lfg);
            Assert.AreEqual(1, lfg.Full);
        }

        [Test]
        public void TestLfgType()
        {
            var lfg1 = new LfgEntry(0x10000CA);
            var lfg2 = new LfgEntry(0);

            Assert.AreEqual(LfgType.Dungeon, lfg1.LfgType);
            Assert.AreEqual(LfgType.None, lfg2.LfgType);
        }

        [Test]
        public void TestInstanceId()
        {
            var lfg1 = new LfgEntry(0x10000CA);
            var lfg2 = new LfgEntry(0);

            Assert.AreEqual(0xCA, lfg1.InstanceId);
            Assert.AreEqual(0, lfg2.InstanceId);
        }

        [Test]
        public void TestOperators()
        {
            var lfg1 = new LfgEntry(0x10000CA);
            var lfg15 = new LfgEntry(0x10000CA);
            var lfg2 = new LfgEntry(0);

            Assert.IsFalse(lfg1 == lfg2);
            Assert.IsTrue(lfg1 != lfg2);

#pragma warning disable 1718
            // ReSharper disable once EqualExpressionComparison
            Assert.IsTrue(lfg1 == lfg1);
#pragma warning restore 1718

            Assert.IsTrue(lfg1 == lfg15);
        }

        [Test]
        public void TestEquals()
        {
            var lfg1 = new LfgEntry(0x10000CA);
            var lfg15 = new LfgEntry(0x10000CA);
            var lfg2 = new LfgEntry(0);

            Assert.IsFalse(lfg1.Equals(lfg2));
            Assert.IsTrue(lfg1.Equals((object) lfg15));
            Assert.IsTrue(lfg1.Equals(lfg1));
            Assert.IsTrue(lfg1.Equals(lfg15));
        }

        [Test]
        public void TestGetHashCode()
        {
            var lfg1 = new LfgEntry(0x10000CA);
            var lfg15 = new LfgEntry(0x10000CA);
            var lfg2 = new LfgEntry(0);

            Assert.AreEqual(lfg1.GetHashCode(), lfg15.GetHashCode());
            Assert.AreNotEqual(lfg1.GetHashCode(), lfg2.GetHashCode());
        }

        [Test, Ignore]
        public void TestToString()
        {
            // ...
        }
    }
}
