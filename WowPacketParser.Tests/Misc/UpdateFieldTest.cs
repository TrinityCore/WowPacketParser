using NUnit.Framework;
using WowPacketParser.Misc;

namespace WowPacketParser.Tests.Misc
{
    [TestFixture]
    public class UpdateFieldTest
    {
        [Test]
        public void TestConstructor()
        {
            var updateField = new UpdateField(uint.MaxValue, float.MaxValue);

            Assert.AreEqual(uint.MaxValue, updateField.UInt32Value);
            Assert.AreEqual(float.MaxValue, updateField.SingleValue, float.Epsilon);
        }

        [Test]
        public void TestEquals()
        {
            var uf1 = new UpdateField(uint.MaxValue, float.MaxValue);
            var uf15 = new UpdateField(uint.MaxValue, float.MaxValue);
            var uf2 = new UpdateField(1, float.MaxValue);

            Assert.IsTrue(uf1.Equals(uf15));
            Assert.IsFalse(uf1.Equals(uf2));
            Assert.IsFalse(uf1.Equals(uint.MaxValue));
            Assert.IsTrue(uf1.Equals(uf1));
        }

        [Test]
        public void TestOperators()
        {
            var uf1 = new UpdateField(uint.MaxValue, float.MaxValue);
            var uf15 = new UpdateField(uint.MaxValue, float.MaxValue);
            var uf2 = new UpdateField(1, float.MaxValue);

            Assert.IsTrue(uf1 == uf15);
            Assert.IsFalse(uf1 == uf2);
#pragma warning disable 1718
            Assert.IsTrue(uf1 == uf1);
#pragma warning restore 1718
        }
    }
}
