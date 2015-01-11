using System.Globalization;
using System.Threading;
using NUnit.Framework;
using WowPacketParser.Misc;

namespace WowPacketParser.Tests.Misc
{
    [TestFixture]
    public class QuaternionTest
    {
        [Test]
        public void TestConstructor()
        {
            var quat = new Quaternion(1.0f, 2.0f, 3.0f, 4.0f);
            Assert.IsNotNull(quat);
            Assert.AreEqual(1.0f, quat.X);
            Assert.AreEqual(2.0f, quat.Y);
            Assert.AreEqual(3.0f, quat.Z);
            Assert.AreEqual(4.0f, quat.W);
        }

        [Test]
        public void TestToString()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var quat = new Quaternion(1.1f, 2.0f, 0f, 4.0f);

            Assert.AreEqual("X: 1.1 Y: 2 Z: 0 W: 4", quat.ToString());
        }

        [Test]
        public void TestOperators()
        {
            var quat1 = new Quaternion(1.1f, 2.0f, 0f, 4.0f);
            var quat15 = new Quaternion(1.1f, 2.0f, 0f, 4.0f);
            var quat2 = new Quaternion(1.1f, 2.0f, 3.0f, 4.0f);

            Assert.IsTrue(quat1 == quat15);
#pragma warning disable 1718
            // ReSharper disable once EqualExpressionComparison
            Assert.IsTrue(quat1 == quat1);
#pragma warning restore 1718
            Assert.IsFalse(quat1 == quat2);
        }

        [Test]
        public void TestEquals()
        {
            var quat1 = new Quaternion(1.1f, 2.0f, 0f, 4.0f);
            var quat15 = new Quaternion(1.1f, 2.0f, 0f, 4.0f);
            var quat2 = new Quaternion(1.1f, 2.0f, 3.0f, 4.0f);

            Assert.IsTrue(quat1.Equals(quat15));
            Assert.IsTrue(quat1.Equals(quat1));
            Assert.IsFalse(quat1.Equals(quat2));
        }

        [Test]
        public void TestGetHashCode()
        {
            var quat1 = new Quaternion(1.1f, 2.0f, 0f, 4.0f);
            var quat15 = new Quaternion(1.1f, 2.0f, 0f, 4.0f);
            var quat2 = new Quaternion(1.1f, 2.0f, 0.0001f, 4.0f);

            Assert.AreEqual(quat1.GetHashCode(), quat15.GetHashCode());
            Assert.AreNotEqual(quat1.GetHashCode(), quat2.GetHashCode());
        }
    }
}
