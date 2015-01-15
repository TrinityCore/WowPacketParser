using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using NUnit.Framework;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Tests.Misc
{
    [TestFixture]
    public class UtilitiesTest
    {
        [Test]
        public void TestGetDateTimeFromUnixTime()
        {
            Assert.AreEqual(new DateTime(1970, 1, 1), Utilities.GetDateTimeFromUnixTime(0));
            Assert.AreEqual(new DateTime(1970, 1, 1).AddSeconds(-1), Utilities.GetDateTimeFromUnixTime(-1));
            Assert.AreEqual(new DateTime(2012, 5, 15, 10, 10, 10), Utilities.GetDateTimeFromUnixTime(1337076610));
        }

        [Test]
        public void TestGetUnixtimeFromDateTime()
        {
            Assert.AreEqual(0, Utilities.GetUnixTimeFromDateTime(new DateTime(1970, 1, 1)));
            Assert.AreEqual(-1, Utilities.GetUnixTimeFromDateTime(new DateTime(1970, 1, 1).AddSeconds(-1)));
            Assert.AreEqual(1337076610, Utilities.GetUnixTimeFromDateTime(new DateTime(2012, 5, 15, 10, 10, 10)));
        }

        [Test]
        public void TestHexStringToBinary()
        {
            Assert.AreEqual(new byte[0], Utilities.HexStringToBinary(string.Empty));
            Assert.AreEqual(new byte[] {1, 2, 3, 4}, Utilities.HexStringToBinary("01020304"));
            Assert.AreEqual(new byte[] {255, 0}, Utilities.HexStringToBinary("FF00"));

            Assert.Throws<ArgumentOutOfRangeException>(() => Utilities.HexStringToBinary("B"));
        }

        [Test]
        public void TestByteArrayToHexString()
        {
            Assert.AreEqual(string.Empty, Utilities.ByteArrayToHexString(new byte[0]));
            Assert.AreEqual("01020304", Utilities.ByteArrayToHexString(new byte[] { 1, 2, 3, 4 }));
            Assert.AreEqual("FF00", Utilities.ByteArrayToHexString(new byte[] { 255, 0 }));
        }

        [Test]
        public void TestGetDateTimeFromGameTime()
        {
            var dateTime = Utilities.GetDateTimeFromGameTime(168938967);
            Assert.AreEqual(23, dateTime.Minute);
            Assert.AreEqual(23, dateTime.Hour);
            Assert.AreEqual(8, dateTime.Day);
            Assert.AreEqual(2, dateTime.Month);
            Assert.AreEqual(2010, dateTime.Year);

            dateTime = Utilities.GetDateTimeFromGameTime(0);
            Assert.AreEqual(0, dateTime.Minute);
            Assert.AreEqual(0, dateTime.Hour);
            Assert.AreEqual(1, dateTime.Day);
            Assert.AreEqual(1, dateTime.Month);
            Assert.AreEqual(2000, dateTime.Year);
        }

        [Test]
        public void TestObjectTypeToStore()
        {
            Assert.AreEqual(StoreNameType.Item, Utilities.ObjectTypeToStore(ObjectType.Item));
            Assert.AreEqual(StoreNameType.Unit, Utilities.ObjectTypeToStore(ObjectType.Unit));
            Assert.AreEqual(StoreNameType.None, Utilities.ObjectTypeToStore(ObjectType.DynamicObject));
        }

        [Test]
        public void TestFormattedDateTimeForFiles()
        {
            var str = Utilities.FormattedDateTimeForFiles();
            var now = DateTime.Now;

            Assert.IsTrue(str.All(c => !Path.GetInvalidFileNameChars().Contains(c)));
            StringAssert.Contains(now.Year.ToString(CultureInfo.InvariantCulture), str);
            StringAssert.Contains(now.Month.ToString(CultureInfo.InvariantCulture), str);
            StringAssert.Contains(now.Day.ToString(CultureInfo.InvariantCulture), str);
            StringAssert.Contains(now.Hour.ToString(CultureInfo.InvariantCulture), str);
            StringAssert.Contains(now.Month.ToString(CultureInfo.InvariantCulture), str);
            StringAssert.Contains(now.Second.ToString(CultureInfo.InvariantCulture), str);
        }

        [Test]
        public void TestGetFiles()
        {
            var empty = new List<string>();
            Assert.IsFalse(Utilities.GetFiles(ref empty));
            Assert.IsTrue(empty.Count == 0);

            var files = new List<string> { "a", "WowPacketParser.Tests.dll", "b" };
            Assert.IsTrue(Utilities.GetFiles(ref files));
            Assert.IsTrue(files.Contains("WowPacketParser.Tests.dll"));
            Assert.IsFalse(files.Contains("a"));
            Assert.IsFalse(files.Contains("b"));

            var any = new List<string> { "*" };
            Assert.IsTrue(Utilities.GetFiles(ref any));
            Assert.IsTrue(any.Count > 0);
        }

        [Test]
        public void EqualValues()
        {
            Assert.IsTrue(Utilities.EqualValues(1, 1));

            Assert.IsTrue(Utilities.EqualValues(1.0f, 1.000001f));
            Assert.IsFalse(Utilities.EqualValues(1.0f, 1.1f));

            Assert.IsTrue(Utilities.EqualValues(1.0, 1.000001));
            Assert.IsFalse(Utilities.EqualValues(1.0, 1.1));

            Assert.IsFalse(Utilities.EqualValues(1.000001f, 1));
            Assert.IsTrue(Utilities.EqualValues(1.000001, 1));

            Assert.IsTrue(Utilities.EqualValues("Air", InhabitType.Air));
            Assert.IsTrue(Utilities.EqualValues(4, InhabitType.Air));

            Assert.IsTrue(Utilities.EqualValues(DBNull.Value, string.Empty));
            Assert.IsTrue(Utilities.EqualValues(string.Empty, DBNull.Value));

            Assert.IsFalse(Utilities.EqualValues(false, true));
            Assert.IsTrue(Utilities.EqualValues(false, 0));
        }

        [AttributeUsage(AttributeTargets.Field)]
        private class FieldTestAttribute : Attribute { }

        [AttributeUsage(AttributeTargets.Field)]
        private class FieldDummyTestAttribute : Attribute { }

        private class TestFoo
        {
#pragma warning disable 0649
#pragma warning disable 169
            [FieldTest]
            public int Bar;

            [FieldDummyTest]
            public int Foo;

            [FieldTest]
            public double Baz;
#pragma warning disable 169
#pragma warning restore 0649
        }

        private class TestNone { }

        [Test]
        public void TestGetFieldsAndAttribute()
        {
            var a = Utilities.GetFieldsAndAttribute<TestNone, FieldTestAttribute>();
            var b = Utilities.GetFieldsAndAttribute<TestFoo, FieldTestAttribute>();

            Assert.IsNull(a);
            Assert.IsNotNull(b);

            Assert.AreEqual(2, b.Count);

            CollectionAssert.Contains(b.Select(tuple => tuple.Item1), typeof(TestFoo).GetField("Bar"));
            CollectionAssert.Contains(b.Select(tuple => tuple.Item1), typeof(TestFoo).GetField("Baz"));
            CollectionAssert.DoesNotContain(b.Select(tuple => tuple.Item1), typeof(TestFoo).GetField("Foo"));

            CollectionAssert.Contains(b.Select(tuple => tuple.Item2.GetType()), typeof(FieldTestAttribute));
        }

        [Test]
        public void TestFileIsInUse()
        {
            Assert.IsFalse(Utilities.FileIsInUse("Blablabla"));
            Assert.IsFalse(Utilities.FileIsInUse("WowPacketParser.Tests.dll"));

            using (File.Create("test.test"))
            {
                Assert.IsTrue(Utilities.FileIsInUse("test.test"));
            }

            File.Delete("test.test");
        }

    }
}
