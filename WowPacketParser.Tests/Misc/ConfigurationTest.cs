using NUnit.Framework;
using System;
using System.Configuration;
using Configuration = WowPacketParser.Misc.Configuration;

namespace WowPacketParser.Tests.Misc
{
    [TestFixture]
    public class ConfigurationTest
    {
        [Flags]
        private enum TestEnum
        {
            Corazon = 0,
            Porque = 1,
            Uno = 2
        }

        private Configuration _config;

        [SetUp]
        public void Initialize()
        {
            var configCollection = new KeyValueConfigurationCollection
            {
                {"tequilla", "salsa"},
                {"gringo", ""},
                {"enchilada", "bacalao,gasolina,macarena"},
                {"mariachi", "1"},
                {"banderas", "3"},
                {"ronaldo", "true"},
                {"nein", "False"}
            };

            _config = new Configuration(configCollection);
        }

        [TearDown]
        public void Cleanup()
        {
            _config = null;
        }

        [Test]
        public void TestGetString()
        {
            Assert.That("salsa",      Is.EqualTo(_config.GetString("tequilla", string.Empty)));
            Assert.That(string.Empty, Is.EqualTo(_config.GetString("gringo", "test")));
            Assert.That("1",          Is.EqualTo(_config.GetString("mariachi", string.Empty)));
            Assert.That("test",       Is.EqualTo(_config.GetString("si", "test")));
        }

        [Test]
        public void TestGetStringList()
        {
            Assert.That(new[] { "salsa" }, Is.EqualTo(_config.GetStringList("tequilla", new[] { "test" })));
            Assert.That(new[] { "bacalao", "gasolina", "macarena" }, Is.EqualTo(_config.GetStringList("enchilada", new[] { "test" })));
            Assert.That(new[] { "test" }, Is.EqualTo(_config.GetStringList("si", new[] { "test" })));
        }

        [Test]
        public void TestGetBoolean()
        {
            Assert.That(true, Is.EqualTo(_config.GetBoolean("ronaldo", false)));
            Assert.That(false, Is.EqualTo(_config.GetBoolean("nein", true)));
            Assert.That(true, Is.EqualTo(_config.GetBoolean("si", true)));
            Assert.That(false, Is.EqualTo(_config.GetBoolean("banderas", false)));
        }

        [Test]
        public void TestGetInt()
        {
            Assert.That(3, Is.EqualTo(_config.GetInt("banderas", 1)));
            Assert.That(-1, Is.EqualTo(_config.GetInt("si", -1)));
            Assert.That(1, Is.EqualTo(_config.GetInt("tequilla", 1)));
        }

        [Test]
        public void TestGetEnum()
        {
            Assert.That(TestEnum.Porque, Is.EqualTo(_config.GetEnum("mariachi", TestEnum.Corazon)));
            Assert.That(TestEnum.Corazon, Is.EqualTo(_config.GetEnum("si", TestEnum.Corazon)));
            Assert.That(TestEnum.Uno | TestEnum.Porque, Is.EqualTo(_config.GetEnum("banderas", TestEnum.Corazon)));
        }
    }
}
