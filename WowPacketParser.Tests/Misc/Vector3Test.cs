using NUnit.Framework;
using System.Globalization;
using System.Threading;
using WowPacketParser.Misc;

namespace WowPacketParser.Tests.Misc
{
    [TestFixture]
    public class Vector3Test
    {
        [Test]
        public void TestConstructor()
        {
            var vect = new Vector3(1.0f, 2.0f, 3.0f);
            Assert.IsNotNull(vect);
            Assert.AreEqual(1.0f, vect.X);
            Assert.AreEqual(2.0f, vect.Y);
            Assert.AreEqual(3.0f, vect.Z);
        }

        [Test]
        public void TestToString()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var quat = new Vector3(1.1f, 2.0f, 0f);

            Assert.AreEqual("X: 1.1 Y: 2 Z: 0", quat.ToString());
        }

        [Test]
        public void TestOperators()
        {
            var vect1 = new Vector3(1.1f, 2.0f, 0f);
            var vect15 = new Vector3(1.1f, 2.0f, 0f);
            var vect2 = new Vector3(1.1f, 2.0f, 3.0f);

            Assert.IsTrue(vect1 == vect15);
#pragma warning disable 1718
            // ReSharper disable once EqualExpressionComparison
            Assert.IsTrue(vect1 == vect1);
#pragma warning restore 1718
            Assert.IsFalse(vect1 == vect2);
        }

        [Test]
        public void TestEquals()
        {
            var vect1 = new Vector3(1.1f, 2.0f, 0f);
            var vect15 = new Vector3(1.1f, 2.0f, 0f);
            var vect2 = new Vector3(1.1f, 2.0f, 3.0f);

            Assert.IsTrue(vect1.Equals(vect15));
            Assert.IsTrue(vect1.Equals(vect1));
            Assert.IsFalse(vect1.Equals(vect2));
        }

        [Test]
        public void TestGetHashCode()
        {
            var vect1 = new Vector3(1.1f, 2.0f, 0f);
            var vect15 = new Vector3(1.1f, 2.0f, 0f);
            var vect2 = new Vector3(1.1f, 2.0f, 0.0001f);

            Assert.AreEqual(vect1.GetHashCode(), vect15.GetHashCode());
            Assert.AreNotEqual(vect1.GetHashCode(), vect2.GetHashCode());
        }
    }
}
