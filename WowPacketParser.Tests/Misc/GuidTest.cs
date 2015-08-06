using NUnit.Framework;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Tests.Misc
{
    [TestFixture]
    public class GuidTest
    {
        [Test]
        public void TestConstructor()
        {
            var guid = new WowGuid64(1);

            Assert.IsNotNull(guid);
            Assert.AreEqual(1, guid.Low);
            Assert.AreEqual(0, guid.High);
        }

        [Test]
        public void TestHasEntry()
        {
            var guid1 = new WowGuid64(0xF130005C0500105F);
            var guid2 = new WowGuid64(0x600000002B2D7C9);
            var guid3 = new WowGuid64(0);

            Assert.IsTrue(guid1.HasEntry());
            Assert.IsFalse(guid2.HasEntry());
            Assert.IsFalse(guid3.HasEntry());
        }

        [Test]
        public void TestGetLow()
        {
            var guid1 = new WowGuid64(0xF130005C0500105F);
            var guid2 = new WowGuid64(0x600000002B2D7C9);
            var guid3 = new WowGuid64(0);

            Assert.AreEqual(0x105F, guid1.GetLow());
            Assert.AreEqual(0x2B2D7C9, guid2.GetLow());
            Assert.AreEqual(0, guid3.GetLow());
        }

        [Test]
        public void TestGetEntry()
        {
            ClientVersion.SetVersion(ClientVersionBuild.V3_3_5a_12340);

            var guid1 = new WowGuid64(0xF130005C0500105F);
            var guid2 = new WowGuid64(0x600000002B2D7C9);
            var guid3 = new WowGuid64(0);

            Assert.AreEqual(0x5C05, guid1.GetEntry());
            Assert.AreEqual(0, guid2.GetEntry());
            Assert.AreEqual(0, guid3.GetEntry());
        }

        [Test]
        public void TestGetHighType()
        {
            var guid1 = new WowGuid64(0xF130005C0500105F);
            var guid2 = new WowGuid64(0x600000002B2D7C9);
            var guid3 = new WowGuid64(0);

            Assert.AreEqual(HighGuidType.Creature, guid1.GetHighType());
            Assert.AreEqual(HighGuidType.Player, guid2.GetHighType());
            Assert.AreEqual(HighGuidType.Null, guid3.GetHighType());
        }

        [Test]
        public void TestGetObjectType()
        {
            var guid1 = new WowGuid64(0xF130005C0500105F);
            var guid2 = new WowGuid64(0x600000002B2D7C9);
            var guid3 = new WowGuid64(0);

            Assert.AreEqual(ObjectType.Unit, guid1.GetObjectType());
            Assert.AreEqual(ObjectType.Player, guid2.GetObjectType());
            Assert.AreEqual(ObjectType.Object, guid3.GetObjectType());
        }

        [Test]
        public void TestOperators()
        {
            var guid1 = new WowGuid64(0xF130005C0500105F);
            var guid2 = new WowGuid64(0x600000002B2D7C9);
            var guid3 = new WowGuid64(0);

            Assert.IsFalse(guid1 == guid2);
            Assert.IsTrue(guid1 != guid2);

#pragma warning disable 1718
            // ReSharper disable once EqualExpressionComparison
            Assert.IsTrue(guid3 == guid3);
#pragma warning restore 1718
        }

        [Test]
        public void TestEquals()
        {
            var guid1 = new WowGuid64(0xF130005C0500105F);
            var guid2 = new WowGuid64(0x600000002B2D7C9);
            var guid25 = new WowGuid64(0x600000002B2D7C9);
            var guid3 = new WowGuid64(0);

            Assert.IsFalse(guid1.Equals(guid2));
            Assert.IsTrue(guid2.Equals(guid25));
            Assert.IsTrue(guid2.Equals((object) guid25));
            Assert.IsFalse(guid2.Equals(new object()));
            Assert.IsTrue(guid3.Equals(guid3));
        }

        [Test]
        public void TestGetHashCode()
        {
            var guid1 = new WowGuid64(0xF130005C0500105F);
            var guid15 = new WowGuid64(0xF130005C0500105F);
            var guid2 = new WowGuid64(0x600000002B2D7C9);

            Assert.AreEqual(guid1.GetHashCode(), guid15.GetHashCode());
            Assert.AreNotEqual(guid1.GetHashCode(), guid2.GetHashCode());
        }

        [Test, Ignore]
        public void TestToString()
        {
            // ...
        }
    }
}
