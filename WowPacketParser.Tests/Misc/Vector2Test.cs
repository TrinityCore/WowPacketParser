using System.Globalization;
using System.Threading;
using NUnit.Framework;
using WowPacketParser.Misc;

namespace WowPacketParser.Tests.Misc
{
    [TestFixture]
    public class Vector2Test
    {
        [Test]
        public void TestConstructor()
        {
            var vect = new Vector2(1.0f, 2.0f);
            Assert.IsNotNull(vect);
            Assert.AreEqual(1.0f, vect.X);
            Assert.AreEqual(2.0f, vect.Y);
        }

        [Test]
        public void TestToString()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var quat = new Vector2(1.1f, 2.0f);

            Assert.AreEqual("X: 1.1 Y: 2", quat.ToString());
        }

        [Test]
        public void TestOperators()
        {
            var vect1 = new Vector2(1.1f, 2.0f);
            var vect15 = new Vector2(1.1f, 2.0f);
            var vect2 = new Vector2(1.1f, 2.00001f);

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
            var vect1 = new Vector2(1.1f, 2.0f);
            var vect15 = new Vector2(1.1f, 2.0f);
            var vect2 = new Vector2(1.1f, 2.0001f);

            Assert.IsTrue(vect1.Equals(vect15));
            Assert.IsTrue(vect1.Equals(vect1));
            Assert.IsFalse(vect1.Equals(vect2));
        }

        [Test]
        public void TestGetHashCode()
        {
            var vect1 = new Vector2(1.1f, 0.0f);
            var vect15 = new Vector2(1.1f, 0.0f);
            var vect2 = new Vector2(1.1f, 0.0001f);

            Assert.AreEqual(vect1.GetHashCode(), vect15.GetHashCode());
            Assert.AreNotEqual(vect1.GetHashCode(), vect2.GetHashCode());
        }
    }
}
