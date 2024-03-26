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

            Assert.That(uint.MaxValue, Is.EqualTo(updateField.UInt32Value));
            Assert.That(Int32ToFloatBits(uint.MaxValue), Is.EqualTo(updateField.FloatValue).Within(float.Epsilon));

            updateField = new UpdateField(float.MaxValue);

            Assert.That(float.MaxValue, Is.EqualTo(updateField.FloatValue));
            Assert.That(FloatToInt32Bits(float.MaxValue), Is.EqualTo(updateField.UInt32Value));
        }

        [Test]
        public void TestEquals()
        {
            var uf1 = new UpdateField(uint.MaxValue);
            var uf15 = new UpdateField(uint.MaxValue);
            var uf2 = new UpdateField(float.MaxValue);

            Assert.That(uf1.Equals(uf15), Is.True);
            Assert.That(uf1.Equals(uf2), Is.False);
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.That(uf1.Equals(uint.MaxValue), Is.False);
            Assert.That(uf1.Equals(uf1), Is.True);
        }

        [Test]
        public void TestOperators()
        {
            var uf1 = new UpdateField(uint.MaxValue);
            var uf15 = new UpdateField(uint.MaxValue);
            var uf2 = new UpdateField(float.MaxValue);

            Assert.That(uf1 == uf15, Is.True);
            Assert.That(uf1 != uf15, Is.False);
            Assert.That(uf1 == uf2, Is.False);
            Assert.That(uf1 != uf2, Is.True);
#pragma warning disable 1718
            // ReSharper disable once EqualExpressionComparison
            Assert.That(uf1 == uf1, Is.True);
#pragma warning restore 1718
        }
    }
}
