using NUnit.Framework;
using System.Globalization;
using System.Threading;
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
            Assert.That(quat, Is.Not.Null);
            Assert.That(1.0f, Is.EqualTo(quat.X));
            Assert.That(2.0f, Is.EqualTo(quat.Y));
            Assert.That(3.0f, Is.EqualTo(quat.Z));
            Assert.That(4.0f, Is.EqualTo(quat.W));
        }

        [Test]
        public void TestToString()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var quat = new Quaternion(1.1f, 2.0f, 0f, 4.0f);

            Assert.That("X: 1.1 Y: 2 Z: 0 W: 4", Is.EqualTo(quat.ToString()));
        }

        [Test]
        public void TestOperators()
        {
            var quat1 = new Quaternion(1.1f, 2.0f, 0f, 4.0f);
            var quat15 = new Quaternion(1.1f, 2.0f, 0f, 4.0f);
            var quat2 = new Quaternion(1.1f, 2.0f, 3.0f, 4.0f);

            Assert.That(quat1 == quat15, Is.True);
#pragma warning disable 1718
            // ReSharper disable once EqualExpressionComparison
            Assert.That(quat1 == quat1, Is.True);
#pragma warning restore 1718
            Assert.That(quat1 == quat2, Is.False);
        }

        [Test]
        public void TestEquals()
        {
            var quat1 = new Quaternion(1.1f, 2.0f, 0f, 4.0f);
            var quat15 = new Quaternion(1.1f, 2.0f, 0f, 4.0f);
            var quat2 = new Quaternion(1.1f, 2.0f, 3.0f, 4.0f);

            Assert.That(quat1.Equals(quat15), Is.True);
            Assert.That(quat1.Equals(quat1), Is.True);
            Assert.That(quat1.Equals(quat2), Is.False);
        }

        [Test]
        public void TestGetHashCode()
        {
            var quat1 = new Quaternion(1.1f, 2.0f, 0f, 4.0f);
            var quat15 = new Quaternion(1.1f, 2.0f, 0f, 4.0f);
            var quat2 = new Quaternion(1.1f, 2.0f, 0.0001f, 4.0f);

            Assert.That(quat1.GetHashCode(), Is.EqualTo(quat15.GetHashCode()));
            Assert.That(quat1.GetHashCode(), Is.Not.EqualTo(quat2.GetHashCode()));
        }
    }
}
