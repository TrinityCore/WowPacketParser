using NUnit.Framework;
using WowPacketParser.Misc;

namespace WowPacketParser.Tests.Misc
{
    [TestFixture]
    public class UpdateFieldTest
    {
        [Test]
        public void UpdateFieldConstructorTest()
        {
            var updateField = new UpdateField(uint.MaxValue, float.MaxValue);

            Assert.AreEqual(uint.MaxValue, updateField.UInt32Value);
            Assert.AreEqual(float.MaxValue, updateField.SingleValue, float.Epsilon);
        }
    }
}
