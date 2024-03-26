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

            Assert.That(lfg, Is.Not.Null);
            Assert.That(1, Is.EqualTo(lfg.Full));
        }

        [Test]
        public void TestLfgType()
        {
            var lfg1 = new LfgEntry(0x10000CA);
            var lfg2 = new LfgEntry(0);

            Assert.That(LfgType.Dungeon, Is.EqualTo(lfg1.LfgType));
            Assert.That(LfgType.None, Is.EqualTo(lfg2.LfgType));
        }

        [Test]
        public void TestInstanceId()
        {
            var lfg1 = new LfgEntry(0x10000CA);
            var lfg2 = new LfgEntry(0);

            Assert.That(0xCA, Is.EqualTo(lfg1.InstanceId));
            Assert.That(0, Is.EqualTo(lfg2.InstanceId));
        }

        [Test]
        public void TestOperators()
        {
            var lfg1 = new LfgEntry(0x10000CA);
            var lfg15 = new LfgEntry(0x10000CA);
            var lfg2 = new LfgEntry(0);

            Assert.That(lfg1 == lfg2, Is.False);
            Assert.That(lfg1 != lfg2, Is.True);

#pragma warning disable 1718
            // ReSharper disable once EqualExpressionComparison
            Assert.That(lfg1 == lfg1, Is.True);
#pragma warning restore 1718

            Assert.That(lfg1 == lfg15, Is.True);
        }

        [Test]
        public void TestEquals()
        {
            var lfg1 = new LfgEntry(0x10000CA);
            var lfg15 = new LfgEntry(0x10000CA);
            var lfg2 = new LfgEntry(0);

            Assert.That(lfg1.Equals(lfg2), Is.False);
            Assert.That(lfg1.Equals((object) lfg15), Is.True);
            Assert.That(lfg1.Equals(lfg1), Is.True);
            Assert.That(lfg1.Equals(lfg15), Is.True);
        }

        [Test]
        public void TestGetHashCode()
        {
            var lfg1 = new LfgEntry(0x10000CA);
            var lfg15 = new LfgEntry(0x10000CA);
            var lfg2 = new LfgEntry(0);

            Assert.That(lfg1.GetHashCode(), Is.EqualTo(lfg15.GetHashCode()));
            Assert.That(lfg1.GetHashCode(), Is.Not.EqualTo(lfg2.GetHashCode()));
        }

        [Test, Ignore("Nothing to test")]
        public void TestToString()
        {
            // ...
        }
    }
}
