using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Tests.Misc
{
    [TestFixture]
    public class StatisticsTest
    {
        [Test]
        public void TestConstructor()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentOutOfRangeException>(() => new Statistics(0));

            var stats = new Statistics(10);
            Assert.AreEqual(10, stats.TotalPacketCount);
            Assert.AreEqual(0, stats.CalculatedTotalPacketCount);
            Assert.AreEqual(0, stats.SuccessPacketCount);
            Assert.AreEqual(0, stats.WithErrorsPacketCount);
            Assert.AreEqual(0, stats.NotParsedPacketCount);
            Assert.AreEqual(0, stats.NoStructurePacketCount);

            var stats2 = new Statistics();
            Assert.AreEqual(0, stats2.TotalPacketCount);
            Assert.AreEqual(0, stats2.CalculatedTotalPacketCount);
            Assert.AreEqual(0, stats2.SuccessPacketCount);
            Assert.AreEqual(0, stats2.WithErrorsPacketCount);
            Assert.AreEqual(0, stats2.NotParsedPacketCount);
            Assert.AreEqual(0, stats2.NoStructurePacketCount);
        }

        [Test]
        public void TestAdds()
        {
            var stats = new Statistics(7);
            stats.AddSuccess();
            stats.AddSuccess();
            stats.AddSuccess();
            stats.AddWithErrors();
            stats.AddWithErrors();
            stats.AddNotParsed();
            stats.AddNoStructure();

            Assert.AreEqual(3, stats.SuccessPacketCount);
            Assert.AreEqual(2, stats.WithErrorsPacketCount);
            Assert.AreEqual(1, stats.NotParsedPacketCount);
            Assert.AreEqual(1, stats.NoStructurePacketCount);
            Assert.AreEqual(stats.TotalPacketCount, stats.CalculatedTotalPacketCount);
        }

        [Test]
        public void TestAddByStatus()
        {
            var packet1 = new Packet(new byte[] { 1, 2 }, 1, DateTime.Now, Direction.ClientToServer, 1, "test.bin");
            var packet2 = new Packet(new byte[] { 1, 2 }, 1, DateTime.Now, Direction.ClientToServer, 2, "test.bin");
            var packet3 = new Packet(new byte[] { 1, 2 }, 1, DateTime.Now, Direction.ClientToServer, 3, "test.bin");

            packet1.Status = ParsedStatus.Success;
            packet2.Status = ParsedStatus.NotParsed;

            var stats = new Statistics();
            stats.AddByStatus(packet1.Status);
            stats.AddByStatus(packet2.Status);
            stats.AddByStatus(packet3.Status);

            packet1.ClosePacket();
            packet2.ClosePacket();
            packet3.ClosePacket();

            Assert.AreEqual(1, stats.SuccessPacketCount);
            Assert.AreEqual(0, stats.WithErrorsPacketCount);
            Assert.AreEqual(0, stats.NoStructurePacketCount);
            Assert.AreEqual(1, stats.NotParsedPacketCount);
            Assert.AreEqual(2, stats.CalculatedTotalPacketCount);
        }

        [Test]
        public void TestPercentage()
        {
            var stats = new Statistics(7);
            stats.AddSuccess();
            stats.AddSuccess();
            stats.AddSuccess();
            stats.AddWithErrors();
            stats.AddWithErrors();
            stats.AddNotParsed();
            stats.AddNoStructure();
            stats.AddNoStructure();

            Assert.AreEqual(3.0 / 8.0 * 100.0, stats.GetSuccessPercentage(), 0.001);
            Assert.AreEqual(2.0 / 8.0 * 100.0, stats.GetWithErrorsPercentage(), 0.001);
            Assert.AreEqual(1.0 / 8.0 * 100.0, stats.GetNotParsedPercentage(), 0.001);
            Assert.AreEqual(2.0 / 8.0 * 100.0, stats.GetNoStructurePercentage(), 0.001);
        }

        [Test]
        public void TestTimes()
        {
            var stats = new Statistics(1);
            stats.SetStartTime(DateTime.Now);
            Thread.Sleep(1);
            stats.SetEndTime(DateTime.Now);

            var span = stats.GetParsingTime();

            Assert.Greater(span.Milliseconds, 0);
        }

        [Test]
        public void TestToString()
        {
            // What is there to test?

            var stats = new Statistics(6);
            stats.AddSuccess();
            stats.AddSuccess();
            stats.AddSuccess();
            stats.AddWithErrors();
            stats.AddWithErrors();
            stats.AddNotParsed();
            stats.AddNoStructure();

            var str = stats.ToString();

            Assert.That(str, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void TestBuildStats()
        {
            var packet1 = new Packet(new byte[] { 1, 2 }, 1, DateTime.Now, Direction.ClientToServer, 1, "test.bin");
            var packet2 = new Packet(new byte[] { 2, 1 }, 2, DateTime.Now.AddMinutes(1), Direction.ServerToClient, 2, "test.bin");
            var packet3 = new Packet(new byte[] { 2, 2 }, 3, DateTime.Now.AddMinutes(3), Direction.ServerToClient, 3, "test.bin");

            packet1.Status = ParsedStatus.Success;
            packet2.Status = ParsedStatus.NotParsed;
            packet3.Status = ParsedStatus.NoStructure;

            var packets = new List<Packet> {packet1, packet2, packet3};

            var stats = Statistics.BuildStats(packets);

            Assert.AreEqual(3, stats.TotalPacketCount);
            Assert.AreEqual(3, stats.CalculatedTotalPacketCount);
            Assert.AreEqual(1, stats.SuccessPacketCount);
            Assert.AreEqual(0, stats.WithErrorsPacketCount);
            Assert.AreEqual(1, stats.NotParsedPacketCount);
            Assert.AreEqual(33.3, stats.GetSuccessPercentage(), 0.1);
            Assert.AreEqual(0, stats.GetWithErrorsPercentage());
            Assert.AreEqual(33.3, stats.GetNotParsedPercentage(), 0.1);
            Assert.AreEqual(33.3, stats.GetNoStructurePercentage(), 0.1);
        }
    }
}
