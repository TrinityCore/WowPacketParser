using NUnit.Framework;
using WowPacketParser.Misc;

namespace WowPacketParser.Tests.Misc
{
    [TestFixture]
    public class UpdateFieldTest
    {
        public static unsafe uint FloatToInt32Bits(float value) // ala BitConveter.DoubleToInt64Bits()
        {
            return *(uint*)&value;
        }

        public static unsafe float Int32ToFloatBits(uint value) // ala BitConveter.Int64BitsToDouble()
        {
            return *(float*)&value;
        }

        [Test]
        public void TestConstructor()
        {
            var updateField = new UpdateField(uint.MaxValue);

            Assert.AreEqual(uint.MaxValue, updateField.UInt32Value);
            Assert.AreEqual(Int32ToFloatBits(uint.MaxValue), updateField.FloatValue, float.Epsilon);

            updateField = new UpdateField(float.MaxValue);

            Assert.AreEqual(float.MaxValue, updateField.FloatValue);
            Assert.AreEqual(FloatToInt32Bits(float.MaxValue), updateField.UInt32Value);
        }

        [Test]
        public void TestEquals()
        {
            var uf1 = new UpdateField(uint.MaxValue);
            var uf15 = new UpdateField(uint.MaxValue);
            var uf2 = new UpdateField(float.MaxValue);

            Assert.IsTrue(uf1.Equals(uf15));
            Assert.IsFalse(uf1.Equals(uf2));
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.IsFalse(uf1.Equals(uint.MaxValue));
            Assert.IsTrue(uf1.Equals(uf1));
        }

        [Test]
        public void TestOperators()
        {
            var uf1 = new UpdateField(uint.MaxValue);
            var uf15 = new UpdateField(uint.MaxValue);
            var uf2 = new UpdateField(float.MaxValue);

            Assert.IsTrue(uf1 == uf15);
            Assert.IsFalse(uf1 != uf15);
            Assert.IsFalse(uf1 == uf2);
            Assert.IsTrue(uf1 != uf2);
#pragma warning disable 1718
            // ReSharper disable once EqualExpressionComparison
            Assert.IsTrue(uf1 == uf1);
#pragma warning restore 1718
        }
    }
}
