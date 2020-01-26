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

            Assert.IsNotNull(bit);
            Assert.IsFalse(bit);
        }

        [Test]
        public void TestOperators()
        {
            Bit bit = true;
            Assert.IsTrue(bit);
            Assert.IsTrue(1 == bit);

            bit = false;
            Assert.IsFalse(bit);
            Assert.IsTrue(0 == bit);

            bit = 1;
            Assert.IsTrue(bit);

            bit = 0;
            Assert.IsFalse(bit);
        }

        [Test]
        public void TestToString()
        {
            Bit bit = false;
            Assert.AreEqual("False", bit.ToString());

            bit = true;
            Assert.AreEqual("True", bit.ToString());
        }
    }
}
