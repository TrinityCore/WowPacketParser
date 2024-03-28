using NUnit.Framework;
using System.Globalization;
using System.Threading;
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
            Assert.That(vect, Is.Not.Null);
            Assert.That(1.0f, Is.EqualTo(vect.X));
            Assert.That(2.0f, Is.EqualTo(vect.Y));
        }

        [Test]
        public void TestToString()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var quat = new Vector2(1.1f, 2.0f);

            Assert.That("X: 1.1 Y: 2", Is.EqualTo(quat.ToString()));
        }

        [Test]
        public void TestOperators()
        {
            var vect1 = new Vector2(1.1f, 2.0f);
            var vect15 = new Vector2(1.1f, 2.0f);
            var vect2 = new Vector2(1.1f, 2.00001f);

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
            var vect1 = new Vector2(1.1f, 2.0f);
            var vect15 = new Vector2(1.1f, 2.0f);
            var vect2 = new Vector2(1.1f, 2.0001f);

            Assert.That(vect1.Equals(vect15), Is.True);
            Assert.That(vect1.Equals(vect1), Is.True);
            Assert.That(vect1.Equals(vect2), Is.False);
        }

        [Test]
        public void TestGetHashCode()
        {
            var vect1 = new Vector2(1.1f, 0.0f);
            var vect15 = new Vector2(1.1f, 0.0f);
            var vect2 = new Vector2(1.1f, 0.0001f);

            Assert.That(vect1.GetHashCode(), Is.EqualTo(vect15.GetHashCode()));
            Assert.That(vect1.GetHashCode(), Is.Not.EqualTo(vect2.GetHashCode()));
        }
    }
}
