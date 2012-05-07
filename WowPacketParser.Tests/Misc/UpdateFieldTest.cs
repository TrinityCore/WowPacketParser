using WowPacketParser.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WowPacketParser.Tests
{
    [TestClass]
    public class UpdateFieldTest
    {
        [TestMethod]
        public void UpdateFieldConstructorTest()
        {
            var updateField = new UpdateField(uint.MaxValue, float.MaxValue);

            Assert.AreEqual(uint.MaxValue, updateField.UInt32Value);
            Assert.AreEqual(float.MaxValue, updateField.SingleValue, float.Epsilon);
        }
    }
}
