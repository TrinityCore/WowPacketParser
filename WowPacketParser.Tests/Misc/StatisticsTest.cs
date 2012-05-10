using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
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
            Assert.Throws<ArgumentOutOfRangeException>(() => new Statistics(0));

            var stats = new Statistics(10);
            Assert.AreEqual(10, stats.TotalPacketCount);
            Assert.AreEqual(0, stats.CalculatedTotalPacketCount);
            Assert.AreEqual(0, stats.SucessPacketCount);
            Assert.AreEqual(0, stats.WithErrorsPacketCount);
            Assert.AreEqual(0, stats.NotParsedPacketCount);

            var stats2 = new Statistics();
            Assert.AreEqual(0, stats2.TotalPacketCount);
            Assert.AreEqual(0, stats2.CalculatedTotalPacketCount);
            Assert.AreEqual(0, stats2.SucessPacketCount);
            Assert.AreEqual(0, stats2.WithErrorsPacketCount);
            Assert.AreEqual(0, stats2.NotParsedPacketCount);
        }

        [Test]
        public void TestAdds()
        {
            var stats = new Statistics(6);
            stats.AddSucess();
            stats.AddSucess();
            stats.AddSucess();
            stats.AddWithErrors();
            stats.AddWithErrors();
            stats.AddNotParsed();

            Assert.AreEqual(3, stats.SucessPacketCount);
            Assert.AreEqual(2, stats.WithErrorsPacketCount);
            Assert.AreEqual(1, stats.NotParsedPacketCount);
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

            Assert.AreEqual(1, stats.SucessPacketCount);
            Assert.AreEqual(0, stats.WithErrorsPacketCount);
            Assert.AreEqual(1, stats.NotParsedPacketCount);
            Assert.AreEqual(2, stats.CalculatedTotalPacketCount);
        }

        [Test]
        public void TestPercentage()
        {
            var stats = new Statistics(6);
            stats.AddSucess();
            stats.AddSucess();
            stats.AddSucess();
            stats.AddWithErrors();
            stats.AddWithErrors();
            stats.AddNotParsed();

            Assert.AreEqual(3.0 / 6.0 * 100.0, stats.GetSucessPercentage(), 0.001);
            Assert.AreEqual(2.0 / 6.0 * 100.0, stats.GetWithErrorsPercentage(), 0.001);
            Assert.AreEqual(1.0 / 6.0 * 100.0, stats.GetNotParsedPercentage(), 0.001);
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
            stats.AddSucess();
            stats.AddSucess();
            stats.AddSucess();
            stats.AddWithErrors();
            stats.AddWithErrors();
            stats.AddNotParsed();

            var str = stats.ToString();

            Assert.IsNotNullOrEmpty(str);
        }

        [Test]
        public void TestBuildStats()
        {
            var packet1 = new Packet(new byte[] { 1, 2 }, 1, DateTime.Now, Direction.ClientToServer, 1, "test.bin");
            var packet2 = new Packet(new byte[] { 2, 1 }, 2, DateTime.Now.AddMinutes(1), Direction.ServerToClient, 2, "test.bin");

            packet1.Status = ParsedStatus.Success;
            packet2.Status = ParsedStatus.NotParsed;

            var packets = new List<Packet> {packet1, packet2};

            var stats = Statistics.BuildStats(packets);

            Assert.AreEqual(2, stats.TotalPacketCount);
            Assert.AreEqual(2, stats.CalculatedTotalPacketCount);
            Assert.AreEqual(1, stats.SucessPacketCount);
            Assert.AreEqual(0, stats.WithErrorsPacketCount);
            Assert.AreEqual(1, stats.NotParsedPacketCount);
            Assert.AreEqual(50, stats.GetSucessPercentage());
            Assert.AreEqual(0, stats.GetWithErrorsPercentage());
            Assert.AreEqual(50, stats.GetNotParsedPercentage());
        }
    }
}
