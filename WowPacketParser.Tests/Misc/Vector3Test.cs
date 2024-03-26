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
            Assert.That(vect, Is.Not.Null);
            Assert.That(1.0f, Is.EqualTo(vect.X));
            Assert.That(2.0f, Is.EqualTo(vect.Y));
            Assert.That(3.0f, Is.EqualTo(vect.Z));
        }

        [Test]
        public void TestToString()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var quat = new Vector3(1.1f, 2.0f, 0f);

            Assert.That("X: 1.1 Y: 2 Z: 0", Is.EqualTo(quat.ToString()));
        }

        [Test]
        public void TestOperators()
        {
            var vect1 = new Vector3(1.1f, 2.0f, 0f);
            var vect15 = new Vector3(1.1f, 2.0f, 0f);
            var vect2 = new Vector3(1.1f, 2.0f, 3.0f);

            Assert.That(vect1 == vect15, Is.True);
#pragma warning disable 1718
            // ReSharper disable once EqualExpressionComparison
            Assert.That(vect1 == vect1, Is.True);
#pragma warning restore 1718
            Assert.That(vect1 == vect2, Is.False);
        }

        [Test]
        public void TestEquals()
        {
            var vect1 = new Vector3(1.1f, 2.0f, 0f);
            var vect15 = new Vector3(1.1f, 2.0f, 0f);
            var vect2 = new Vector3(1.1f, 2.0f, 3.0f);

            Assert.That(vect1.Equals(vect15), Is.True);
            Assert.That(vect1.Equals(vect1), Is.True);
            Assert.That(vect1.Equals(vect2), Is.False);
        }

        [Test]
        public void TestGetHashCode()
        {
            var vect1 = new Vector3(1.1f, 2.0f, 0f);
            var vect15 = new Vector3(1.1f, 2.0f, 0f);
            var vect2 = new Vector3(1.1f, 2.0f, 0.0001f);

            Assert.That(vect1.GetHashCode(), Is.EqualTo(vect15.GetHashCode()));
            Assert.That(vect1.GetHashCode(), Is.Not.EqualTo(vect2.GetHashCode()));
        }
    }
}
