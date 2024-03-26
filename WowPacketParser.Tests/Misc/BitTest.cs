using NUnit.Framework;
using WowPacketParser.Misc;

namespace WowPacketParser.Tests.Misc
{
    [TestFixture]
    public class BitTest
    {
        [Test]
        public void TestConstructor()
        {
            var bit = new Bit();

            Assert.That(bit, Is.Not.Null);
            Assert.That<bool>(bit, Is.False);
        }

        [Test]
        public void TestOperators()
        {
            Bit bit = true;
            Assert.That<bool>(bit, Is.True);
            Assert.That(1 == bit, Is.True);

            bit = false;
            Assert.That<bool>(bit, Is.False);
            Assert.That(0 == bit, Is.True);

            bit = 1;
            Assert.That<bool>(bit, Is.True);

            bit = 0;
            Assert.That<bool>(bit, Is.False);
        }

        [Test]
        public void TestToString()
        {
            Bit bit = false;
            Assert.That("False", Is.EqualTo(bit.ToString()));

            bit = true;
            Assert.That("True", Is.EqualTo(bit.ToString()));
        }
    }
}
