using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using PacketParser.Processing;
using PacketParser.Misc;
using PacketDumper.Misc;
using PacketParser.Loading;
using PacketParser.DataStructures;
using PacketParser.Parsing;

namespace PacketDumper.Processing
{
    public class PacketFileDumper : PacketFileProcessor
    {
        private readonly Statistics _stats;

        private const int minPacketsForProgressUpdate = 600;

        public PacketFileDumper(string fileName, Tuple<int, int> number = null) : base(fileName, number)
        {
            _stats = new Statistics();
        }

        public ClientVersionBuild GetClientVersion(IPacketReader reader)
        {
            // default selection, when version not provided in config
            if (Settings.ClientBuild == ClientVersionBuild.Zero)
            {
                // use version info from file
                return reader.GetBuild();
            }
            return Settings.ClientBuild;
        }

        public void Process()
        {
            Current = this;

            _stats.SetStartTime(DateTime.Now);

            var type = Settings.PacketFileType;
            if (String.IsNullOrEmpty(type))
                type = Reader.GetReaderTypeByFileName(FileName);
            var reader = Reader.GetReader(type, FileName);
            Trace.WriteLine(string.Format("{0}: Processing packets (type {1})...", LogPrefix, reader.ToString()));
            ClientVersion.SetVersion(GetClientVersion(reader));
            if (ClientVersion.Build == ClientVersionBuild.Zero)
                throw new Exception("Selected packet file type does not contain version info, you need to provide version in config!");
            Trace.WriteLine(string.Format("{0}: Assumed version: {1}", LogPrefix, ClientVersion.VersionString));

            try
            {
                var packetNum = 0;
                var packetCount = 0;

                uint oldPct = 0;
                ShowPercentProgressMessage("Processing...", oldPct);
                // initialize processors
                InitProcessors();

                uint progressCheckPackets = 0;
                while (reader.CanRead())
                {
                    ++progressCheckPackets;
                    var packet = reader.Read(packetNum, FileName);

                    // read error
                    if (packet == null)
                        continue;

                    ++packetNum;

                    // finish if read packet number reached max
                    if (Settings.ReaderFilterPacketNumHigh != 0 && packetNum > Settings.ReaderFilterPacketNumHigh)
                        break;

                    // skip packets if they were filtered out
                    if (packetNum < Settings.ReaderFilterPacketNumLow)
                        continue;

                    // check for filters
                    if (!CheckReadFilters(packet.Opcode))
                        continue;

                    ProcessPacket(packet);

                    ++packetCount;

                    if (progressCheckPackets >= minPacketsForProgressUpdate)
                    {
                        var newPct = reader.GetProgress();
                        if (newPct != oldPct)
                        {
                            ShowPercentProgressMessage("Processing...", newPct);
                            oldPct = newPct;
                        }
                        progressCheckPackets = 0;
                    }

                    // finish if read packet count reached max
                    if (Settings.ReaderFilterPacketsNum > 0 && packetCount == Settings.ReaderFilterPacketsNum)
                        break;
                }
                if (oldPct != 100)
                    ShowPercentProgressMessage("Processing...", 100);

                _stats.SetEndTime(DateTime.Now);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("File {0} could not be parsed", FileName);
                Trace.WriteLine(ex.Data);
                Trace.WriteLine(ex.GetType());
                Trace.WriteLine(ex.Message);
                Trace.WriteLine(ex.StackTrace);
            }
            finally
            {
                reader.Dispose();
                Trace.WriteLine(string.Format("{0}: {1}", LogPrefix, _stats));
                Current = null;
            }
        }

        private static void ShowPercentProgressMessage(string message, uint percent)
        {
            Console.Write("\r{0} {1}% complete", message, percent);
            if (percent == 100)
                Console.WriteLine();
        }

        private bool CheckReadFilters(int opc)
        {
            var opcodeName = Opcodes.GetOpcodeName(opc);
            if (Settings.ReaderFilterOpcode.Length > 0)
                if (!opcodeName.MatchesFilters(Settings.ReaderFilterOpcode))
                    return false;
            // check for ignore filters
            if (Settings.ReaderFilterIgnoreOpcode.Length > 0)
                if (opcodeName.MatchesFilters(Settings.ReaderFilterIgnoreOpcode))
                    return false;

            return true;
        }

        private void ProcessPacket(Packet packet)
        {
            // Parse the packet, read the data into StoreData tree
            Handler.Parse(packet);

            // Update statistics
            _stats.AddByStatus(packet.Status);

            ProcessData(packet);

            // Close Writer, Stream - Dispose
            packet.ClosePacket();
        }
    }
}
