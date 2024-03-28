using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
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

            Assert.That(expected, Is.EqualTo(actual));
            packet.ClosePacket();
        }

        [Test]
        public void TestHasAnyFlag()
        {
            Assert.That(0x30.HasAnyFlag(0x10), Is.True);
            Assert.That(0x20.HasAnyFlag(0x10), Is.False);
            Assert.That(InhabitType.Anywhere.HasAnyFlag(InhabitType.Water), Is.True);
        }

        [Test]
        public void TestMatchesFilters()
        {
            var list = new List<string> {"Foo", "Bar", "FooBar"};

            Assert.That("bar".MatchesFilters(list), Is.True);
            Assert.That("baz".MatchesFilters(list), Is.False);

            var list2 = new List<string> { " Foo", " Bar ", "FooBar " };
            for (var i = 0; i < list2.Count; i++)
                list2[i] = list2[i].Trim();


            Assert.That("bar".MatchesFilters(list2), Is.True);
            Assert.That("Foo".MatchesFilters(list2), Is.True);
            Assert.That("FooBaz".MatchesFilters(list2), Is.True); // matches Foo
            Assert.That("baz".MatchesFilters(list2), Is.False);
        }

        [Test]
        public void TestToByte()
        {
            Assert.That(1, Is.EqualTo(true.ToByte()));
            Assert.That(0, Is.EqualTo(false.ToByte()));
        }

        [Test]
        public void TestToFormattedString()
        {
            // 42 hours = 1 day + 18 hours
            Assert.That("18:42:42.042", Is.EqualTo(new TimeSpan(0, 42, 42, 42, 42).ToFormattedString()));
            Assert.That("00:00:00.000", Is.EqualTo(new TimeSpan(0, 0, 0, 0, 0).ToFormattedString()));
        }

        [Test]
        public void TestSetCulture()
        {
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CurrentCulture;

            var dummy = new List<int> {1, 2, 3};
            dummy.AsParallel().SetCulture();

            Assert.That(CultureInfo.InvariantCulture, Is.EqualTo(Thread.CurrentThread.CurrentCulture));

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        [Test]
        public void TestFlattenIEnumerable()
        {
            object[] arr = { 1, 2, new object[] { 3, new object[] {4} }, 5 };

            Assert.That(new object[] { 1, 2, 3, 4, 5 }, Is.EqualTo(arr.Flatten()));

            var list = new List<object> { "a", new[] {"b", "c"}, 2 };

            Assert.That(new object[] { "a", "b", "c", 2}, Is.EqualTo(list.Flatten()));

            int[] simple = {1, 2, 3};
            Assert.That(new[] { 1, 2, 3 }, Is.EqualTo(simple.Flatten()));

            int[] empty = {};
            Assert.That(new int[] {}, Is.EqualTo(empty.Flatten()));
        }
    }
}
