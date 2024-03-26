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

            Assert.That(guid, Is.Not.Null);
            Assert.That(1, Is.EqualTo(guid.Low));
            Assert.That(0, Is.EqualTo(guid.High));
        }

        [Test]
        public void TestHasEntry()
        {
            var guid1 = new WowGuid64(0xF130005C0500105F);
            var guid2 = new WowGuid64(0x600000002B2D7C9);
            var guid3 = new WowGuid64(0);

            Assert.That(guid1.HasEntry(), Is.True);
            Assert.That(guid2.HasEntry(), Is.False);
            Assert.That(guid3.HasEntry(), Is.False);
        }

        [Test]
        public void TestGetLow()
        {
            var guid1 = new WowGuid64(0xF130005C0500105F);
            var guid2 = new WowGuid64(0x600000002B2D7C9);
            var guid3 = new WowGuid64(0);

            Assert.That(0x105F, Is.EqualTo(guid1.GetLow()));
            Assert.That(0x2B2D7C9, Is.EqualTo(guid2.GetLow()));
            Assert.That(0, Is.EqualTo(guid3.GetLow()));
        }

        [Test]
        public void TestGetEntry()
        {
            ClientVersion.SetVersion(ClientVersionBuild.V3_3_5a_12340);

            var guid1 = new WowGuid64(0xF130005C0500105F);
            var guid2 = new WowGuid64(0x600000002B2D7C9);
            var guid3 = new WowGuid64(0);

            Assert.That(0x5C05, Is.EqualTo(guid1.GetEntry()));
            Assert.That(0, Is.EqualTo(guid2.GetEntry()));
            Assert.That(0, Is.EqualTo(guid3.GetEntry()));
        }

        [Test]
        public void TestGetHighType()
        {
            var guid1 = new WowGuid64(0xF130005C0500105F);
            var guid2 = new WowGuid64(0x600000002B2D7C9);
            var guid3 = new WowGuid64(0);

            Assert.That(HighGuidType.Creature, Is.EqualTo(guid1.GetHighType()));
            Assert.That(HighGuidType.Player, Is.EqualTo(guid2.GetHighType()));
            Assert.That(HighGuidType.Null, Is.EqualTo(guid3.GetHighType()));
        }

        [Test]
        public void TestGetObjectType()
        {
            var guid1 = new WowGuid64(0xF130005C0500105F);
            var guid2 = new WowGuid64(0x600000002B2D7C9);
            var guid3 = new WowGuid64(0);

            Assert.That(ObjectType.Unit, Is.EqualTo(guid1.GetObjectType()));
            Assert.That(ObjectType.Player, Is.EqualTo(guid2.GetObjectType()));
            Assert.That(ObjectType.Object, Is.EqualTo(guid3.GetObjectType()));
        }

        [Test]
        public void TestOperators()
        {
            var guid1 = new WowGuid64(0xF130005C0500105F);
            var guid2 = new WowGuid64(0x600000002B2D7C9);
            var guid3 = new WowGuid64(0);

            Assert.That(guid1 == guid2, Is.False);
            Assert.That(guid1 != guid2, Is.True);

#pragma warning disable 1718
            // ReSharper disable once EqualExpressionComparison
            Assert.That(guid3 == guid3, Is.True);
#pragma warning restore 1718
        }

        [Test]
        public void TestEquals()
        {
            var guid1 = new WowGuid64(0xF130005C0500105F);
            var guid2 = new WowGuid64(0x600000002B2D7C9);
            var guid25 = new WowGuid64(0x600000002B2D7C9);
            var guid3 = new WowGuid64(0);

            Assert.That(guid1.Equals(guid2), Is.False);
            Assert.That(guid2.Equals(guid25), Is.True);
            Assert.That(guid2.Equals((object) guid25), Is.True);
            Assert.That(guid2.Equals(new object()), Is.False);
            Assert.That(guid3.Equals(guid3), Is.True);
        }

        [Test]
        public void TestGetHashCode()
        {
            var guid1 = new WowGuid64(0xF130005C0500105F);
            var guid15 = new WowGuid64(0xF130005C0500105F);
            var guid2 = new WowGuid64(0x600000002B2D7C9);

            Assert.That(guid1.GetHashCode(), Is.EqualTo(guid15.GetHashCode()));
            Assert.That(guid1.GetHashCode(), Is.Not.EqualTo(guid2.GetHashCode()));
        }

        [Test, Ignore("Nothing to test")]
        public void TestToString()
        {
            // ...
        }
    }
}
