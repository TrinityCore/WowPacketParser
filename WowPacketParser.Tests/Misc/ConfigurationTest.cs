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
            Assert.AreEqual("salsa",      _config.GetString("tequilla", string.Empty));
            Assert.AreEqual(string.Empty, _config.GetString("gringo", "test"));
            Assert.AreEqual("1",          _config.GetString("mariachi", string.Empty));
            Assert.AreEqual("test",       _config.GetString("si", "test"));
        }

        [Test]
        public void TestGetStringList()
        {
            Assert.AreEqual(new[] { "salsa" }, _config.GetStringList("tequilla", new[] { "test" }));
            Assert.AreEqual(new[] { "bacalao", "gasolina", "macarena" }, _config.GetStringList("enchilada", new[] { "test" }));
            Assert.AreEqual(new[] { "test" }, _config.GetStringList("si", new[] { "test" }));
        }

        [Test]
        public void TestGetBoolean()
        {
            Assert.AreEqual(true, _config.GetBoolean("ronaldo", false));
            Assert.AreEqual(false, _config.GetBoolean("nein", true));
            Assert.AreEqual(true, _config.GetBoolean("si", true));
            Assert.AreEqual(false, _config.GetBoolean("banderas", false));
        }

        [Test]
        public void TestGetInt()
        {
            Assert.AreEqual(3, _config.GetInt("banderas", 1));
            Assert.AreEqual(-1, _config.GetInt("si", -1));
            Assert.AreEqual(1, _config.GetInt("tequilla", 1));
        }

        [Test]
        public void TestGetEnum()
        {
            Assert.AreEqual(TestEnum.Porque, _config.GetEnum("mariachi", TestEnum.Corazon));
            Assert.AreEqual(TestEnum.Corazon, _config.GetEnum("si", TestEnum.Corazon));
            Assert.AreEqual(TestEnum.Uno | TestEnum.Porque, _config.GetEnum("banderas", TestEnum.Corazon));
        }
    }
}
