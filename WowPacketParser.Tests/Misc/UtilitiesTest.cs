using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
            Assert.That(new DateTime(1970, 1, 1), Is.EqualTo(Utilities.GetDateTimeFromUnixTime(0)));
            Assert.That(new DateTime(1970, 1, 1).AddSeconds(-1), Is.EqualTo(Utilities.GetDateTimeFromUnixTime(-1)));
            Assert.That(new DateTime(2012, 5, 15, 10, 10, 10), Is.EqualTo(Utilities.GetDateTimeFromUnixTime(1337076610)));
        }

        [Test]
        public void TestGetUnixtimeFromDateTime()
        {
            Assert.That(0, Is.EqualTo(Utilities.GetUnixTimeFromDateTime(new DateTime(1970, 1, 1))));
            Assert.That(-1, Is.EqualTo(Utilities.GetUnixTimeFromDateTime(new DateTime(1970, 1, 1).AddSeconds(-1))));
            Assert.That(1337076610, Is.EqualTo(Utilities.GetUnixTimeFromDateTime(new DateTime(2012, 5, 15, 10, 10, 10))));
        }

        [Test]
        public void TestGetDateTimeFromGameTime()
        {
            var dateTime = Utilities.GetDateTimeFromGameTime(168938967);
            Assert.That(23, Is.EqualTo(dateTime.Minute));
            Assert.That(23, Is.EqualTo(dateTime.Hour));
            Assert.That(8, Is.EqualTo(dateTime.Day));
            Assert.That(2, Is.EqualTo(dateTime.Month));
            Assert.That(2010, Is.EqualTo(dateTime.Year));

            dateTime = Utilities.GetDateTimeFromGameTime(0);
            Assert.That(0, Is.EqualTo(dateTime.Minute));
            Assert.That(0, Is.EqualTo(dateTime.Hour));
            Assert.That(1, Is.EqualTo(dateTime.Day));
            Assert.That(1, Is.EqualTo(dateTime.Month));
            Assert.That(2000, Is.EqualTo(dateTime.Year));
        }

        [Test]
        public void TestObjectTypeToStore()
        {
            Assert.That(StoreNameType.Item, Is.EqualTo(Utilities.ObjectTypeToStore(ObjectType.Item)));
            Assert.That(StoreNameType.Unit, Is.EqualTo(Utilities.ObjectTypeToStore(ObjectType.Unit)));
            Assert.That(StoreNameType.None, Is.EqualTo(Utilities.ObjectTypeToStore(ObjectType.DynamicObject)));
        }

        [Test]
        public void TestFormattedDateTimeForFiles()
        {
            var str = Utilities.FormattedDateTimeForFiles();
            var now = DateTime.Now;

            Assert.That(str.All(c => !Path.GetInvalidFileNameChars().Contains(c)), Is.True);
            Assert.That(str, Does.Contain(now.Year.ToString(CultureInfo.InvariantCulture)));
            Assert.That(str, Does.Contain(now.Month.ToString(CultureInfo.InvariantCulture)));
            Assert.That(str, Does.Contain(now.Day.ToString(CultureInfo.InvariantCulture)));
            Assert.That(str, Does.Contain(now.Hour.ToString(CultureInfo.InvariantCulture)));
            Assert.That(str, Does.Contain(now.Month.ToString(CultureInfo.InvariantCulture)));
            Assert.That(str, Does.Contain(now.Second.ToString(CultureInfo.InvariantCulture)));
        }

        [Test]
        public void TestGetFiles()
        {
            var empty = new List<string>();
            Assert.That(Utilities.GetFiles(ref empty), Is.False);
            Assert.That(empty.Count == 0, Is.True);

            var files = new List<string> { "a", "WowPacketParser.Tests.dll", "b" };
            Assert.That(Utilities.GetFiles(ref files), Is.True);
            Assert.That(files.Contains("WowPacketParser.Tests.dll"), Is.True);
            Assert.That(files.Contains("a"), Is.False);
            Assert.That(files.Contains("b"), Is.False);

            var any = new List<string> { "*" };
            Assert.That(Utilities.GetFiles(ref any), Is.True);
            Assert.That(any.Count > 0, Is.True);
        }

        [Test]
        public void EqualValues()
        {
            Assert.That(Utilities.EqualValues(1, 1), Is.True);

            Assert.That(Utilities.EqualValues(1.0f, 1.000001f), Is.True);
            Assert.That(Utilities.EqualValues(1.0f, 1.1f), Is.False);

            Assert.That(Utilities.EqualValues(1.0, 1.000001), Is.True);
            Assert.That(Utilities.EqualValues(1.0, 1.1), Is.False);

            Assert.That(Utilities.EqualValues(1.000001f, 1), Is.False);
            Assert.That(Utilities.EqualValues(1.000001, 1), Is.True);

            Assert.That(Utilities.EqualValues("Air", InhabitType.Air), Is.True);
            Assert.That(Utilities.EqualValues(4, InhabitType.Air), Is.True);

            Assert.That(Utilities.EqualValues(DBNull.Value, string.Empty), Is.True);
            Assert.That(Utilities.EqualValues(string.Empty, DBNull.Value), Is.True);

            Assert.That(Utilities.EqualValues(false, true), Is.False);
            Assert.That(Utilities.EqualValues(false, 0), Is.True);
        }

        [AttributeUsage(AttributeTargets.Field)]
        private class FieldTestAttribute : Attribute, IAttribute { }

        [AttributeUsage(AttributeTargets.Field)]
        private class FieldDummyTestAttribute : Attribute, IAttribute { }

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
            var a = Utilities.GetFieldsAndAttributes<TestNone, FieldTestAttribute>();
            var b = Utilities.GetFieldsAndAttributes<TestFoo, FieldTestAttribute>();

            Assert.That(a, Is.Null);
            Assert.That(b, Is.Not.Null);

            Assert.That(2, Is.EqualTo(b.Count));

            Assert.That(b.Select(tuple => tuple.Key), Does.Contain(typeof(TestFoo).GetField("Bar")));
            Assert.That(b.Select(tuple => tuple.Key), Does.Contain(typeof(TestFoo).GetField("Baz")));
            Assert.That(b.Select(tuple => tuple.Key), Does.Not.Contain(typeof(TestFoo).GetField("Foo")));

            foreach (var pair in b)
                Assert.That(pair.Value.Select(attr => attr.GetType()), Does.Contain(typeof(FieldTestAttribute)));

        }

        [Test]
        public void TestFileIsInUse()
        {
            Assert.That(Utilities.FileIsInUse("Blablabla"), Is.False);

            using (File.Create("test.test"))
            {
                Assert.That(Utilities.FileIsInUse("test.test"), Is.True);
            }

            File.Delete("test.test");
        }

    }
}
