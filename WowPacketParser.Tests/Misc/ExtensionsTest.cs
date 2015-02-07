using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Tests.Misc
{
    [TestFixture]
    public class ExtensionsTest
    {
        [Test]
        public void TestAsHex()
        {
            var bytes = new byte[] {0xB, 0xA, 0xD, 0xC, 0x0, 0xD, 0xE, 66, 65, 68, 67, 79, 68, 69, 0, 0, 0x42};

            var packet = new Packet(bytes, 1, new DateTime(2012, 1, 1), Direction.ClientToServer, 1, "Test");
            packet.AsHex();

            var actual = packet.Writer.ToString();

            var expected =
            "|-------------------------------------------------|---------------------------------|" + Environment.NewLine +
            "| 00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F | 0 1 2 3 4 5 6 7 8 9 A B C D E F |" + Environment.NewLine +
            "|-------------------------------------------------|---------------------------------|" + Environment.NewLine +
            "| 0B 0A 0D 0C 00 0D 0E 42 41 44 43 4F 44 45 00 00 | . . . . . . . B A D C O D E . . |" + Environment.NewLine +
            "| 42                                              | B                               |" + Environment.NewLine +
            "|-------------------------------------------------|---------------------------------|" + Environment.NewLine;

            Assert.AreEqual(expected, actual);
            packet.ClosePacket();
        }

        [Test]
        public void TestHasAnyFlag()
        {
            Assert.IsTrue(0x30.HasAnyFlag(0x10));
            Assert.IsFalse(0x20.HasAnyFlag(0x10));
            Assert.IsTrue(InhabitType.Anywhere.HasAnyFlag(InhabitType.Water));
        }

        [Test]
        public void TestMatchesFilters()
        {
            var list = new List<string> {"Foo", "Bar", "FooBar"};

            Assert.IsTrue("bar".MatchesFilters(list));
            Assert.IsFalse("baz".MatchesFilters(list));

            var list2 = new List<string> { " Foo", " Bar ", "FooBar " };
            for (var i = 0; i < list2.Count; i++)
                list2[i] = list2[i].Trim();


            Assert.IsTrue("bar".MatchesFilters(list2));
            Assert.IsTrue("Foo".MatchesFilters(list2));
            Assert.IsTrue("FooBaz".MatchesFilters(list2)); // matches Foo
            Assert.IsFalse("baz".MatchesFilters(list2));
        }

        [Test]
        public void TestToByte()
        {
            Assert.AreEqual(1, true.ToByte());
            Assert.AreEqual(0, false.ToByte());
        }

        [Test]
        public void TestToFormattedString()
        {
            // 42 hours = 1 day + 18 hours
            Assert.AreEqual("18:42:42.042", new TimeSpan(0, 42, 42, 42, 42).ToFormattedString());
            Assert.AreEqual("00:00:00.000", new TimeSpan(0, 0, 0, 0, 0).ToFormattedString());
        }

        [Test]
        public void TestSetCulture()
        {
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CurrentCulture;

            var dummy = new List<int> {1, 2, 3};
            dummy.AsParallel().SetCulture();

            Assert.AreEqual(CultureInfo.InvariantCulture, Thread.CurrentThread.CurrentCulture);

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        [Test]
        public void TestFlattenIEnumerable()
        {
            object[] arr = { 1, 2, new object[] { 3, new object[] {4} }, 5 };

            Assert.AreEqual(new object[] { 1, 2, 3, 4, 5 }, arr.Flatten());

            var list = new List<object> { "a", new[] {"b", "c"}, 2 };

            Assert.AreEqual(new object[] { "a", "b", "c", 2}, list.Flatten());

            int[] simple = {1, 2, 3};
            Assert.AreEqual(new[] { 1, 2, 3 }, simple.Flatten());

            int[] empty = {};
            Assert.AreEqual(new int[] {}, empty.Flatten());
        }
    }
}
