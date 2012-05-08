using NUnit.Framework;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using System;

namespace WowPacketParser.Tests.Misc
{
    [TestFixture]
    public class ExtensionsTest
    {
        [Test]
        public void TestAsHex()
        {
            var packet = new Packet(new byte[0], 1, new DateTime(2012, 1, 1), Direction.ClientToServer, 1, "Test");
            Extensions.AsHex(packet);

            var actual = packet.Writer.ToString();
            const string expected = "|-------------------------------------------------|---------------------------------|\r\n| 00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F | 0 1 2 3 4 5 6 7 8 9 A B C D E F |\r\n|-------------------------------------------------|---------------------------------|\r\n|-------------------------------------------------|---------------------------------|\r\n";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestHasAnyFlag()
        {
            Assert.IsTrue(Extensions.HasAnyFlag(0x30, 0x10));
            Assert.IsFalse(Extensions.HasAnyFlag(0x20, 0x10));
            Assert.IsTrue(Extensions.HasAnyFlag(InhabitType.Anywhere, InhabitType.Water));
        }

        /*
        [TestMethod]
        public void TestMatchesFilters()
        {
            string value = string.Empty; // TODO: Initialize to an appropriate value
            IEnumerable<string> filters = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Extensions.MatchesFilters(value, filters);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SetCulture
        ///</summary>
        public void SetCultureTestHelper<TSource>()
        {
            ParallelQuery<TSource> source = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TSource> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TSource> actual;
            actual = Extensions.SetCulture<TSource>(source);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SetCultureTest()
        {
            SetCultureTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for SetCulture
        ///</summary>
        [TestMethod()]
        [DeploymentItem("WowPacketParser.exe")]
        public void SetCultureTest1()
        {
            CultureInfo cultureInfo = null; // TODO: Initialize to an appropriate value
            Extensions_Accessor.SetCulture(cultureInfo);
        }

        /// <summary>
        ///A test for ToByte
        ///</summary>
        [TestMethod()]
        public void ToByteTest()
        {
            bool value = false; // TODO: Initialize to an appropriate value
            byte expected = 0; // TODO: Initialize to an appropriate value
            byte actual;
            actual = Extensions.ToByte(value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToFormattedString
        ///</summary>
        [TestMethod()]
        public void ToFormattedStringTest()
        {
            TimeSpan span = new TimeSpan(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = Extensions.ToFormattedString(span);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToTuple
        ///</summary>
        [TestMethod()]
        public void ToTupleTest()
        {
            IList<object> col = null; // TODO: Initialize to an appropriate value
            int count = 0; // TODO: Initialize to an appropriate value
            object expected = null; // TODO: Initialize to an appropriate value
            object actual;
            actual = Extensions.ToTuple(col, count);
            Assert.AreEqual(expected, actual);
        }
         * */
    }
}
