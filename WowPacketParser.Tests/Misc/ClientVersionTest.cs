using System;
using NUnit.Framework;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Tests.Misc
{
    [TestFixture]
    public class ClientVersionTest
    {
        [SetUp]
        public void Initialize()
        {
            ClientVersion.SetVersion(ClientVersionBuild.V3_3_3_11685);
        }

        [Test]
        public void TestAddedInVersion()
        {
            Assert.IsTrue(ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing));
            Assert.IsFalse(ClientVersion.AddedInVersion(ClientType.Cataclysm));

            Assert.IsTrue(ClientVersion.AddedInVersion(ClientVersionBuild.V2_4_3_8606));
            Assert.IsFalse(ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_5a_12340));
        }

        [Test]
        public void TestRemovedInVersion()
        {
            Assert.IsFalse(ClientVersion.RemovedInVersion(ClientType.WrathOfTheLichKing));
            Assert.IsTrue(ClientVersion.RemovedInVersion(ClientType.Cataclysm));

            Assert.IsFalse(ClientVersion.RemovedInVersion(ClientVersionBuild.V2_4_3_8606));
            Assert.IsTrue(ClientVersion.RemovedInVersion(ClientVersionBuild.V3_3_5a_12340));
        }

        [Test]
        public void TestIsUndefined()
        {
            Assert.IsFalse(ClientVersion.IsUndefined());

            ClientVersion.SetVersion(ClientVersionBuild.Zero);

            Assert.IsTrue(ClientVersion.IsUndefined());
        }

        [Test]
        public void TestBuildGetters()
        {
            Assert.AreEqual(ClientVersionBuild.V3_3_3_11685, ClientVersion.Build);
            Assert.AreEqual(11685, ClientVersion.BuildInt);
            Assert.AreEqual("V3_3_3_11685", ClientVersion.VersionString);
        }

        [Test]
        public void TestSetVersion()
        {
            ClientVersion.SetVersion(new DateTime(1991, 1, 1));
            Assert.IsTrue(ClientVersion.IsUndefined());

            ClientVersion.SetVersion(new DateTime(2025, 1, 1));
            Assert.IsFalse(ClientVersion.IsUndefined());

            ClientVersion.SetVersion(new DateTime(2010, 6, 29));
            Assert.AreEqual(ClientVersionBuild.V3_3_5a_12340, ClientVersion.Build);

            ClientVersion.SetVersion(new DateTime(2015, 7, 9));
            Assert.AreEqual(ClientVersionBuild.V6_2_0_20253, ClientVersion.Build);

            ClientVersion.SetVersion(ClientVersionBuild.V1_12_1_5875);
            Assert.AreEqual(ClientVersionBuild.V1_12_1_5875, ClientVersion.Build);

            ClientVersion.SetVersion(ClientVersionBuild.V6_2_0_20253);
            Assert.AreEqual(ClientVersionBuild.V6_2_0_20253, ClientVersion.Build);
        }
    }
}
