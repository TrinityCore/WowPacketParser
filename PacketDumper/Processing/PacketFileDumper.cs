using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
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

            var reader = Reader.GetReader(FileName, Settings.PacketFileType);
            Trace.WriteLine(string.Format("{0}: Processing packets (type {1})...", LogPrefix, reader.ToString()));
            ClientVersion.SetVersion(GetClientVersion(reader));
            if (ClientVersion.Build == ClientVersionBuild.Zero)
                throw new Exception("Selected packet file type does not contain version info, you need to provide version in config!");
            Trace.WriteLine(string.Format("{0}: Assumed version: {1}", LogPrefix, ClientVersion.VersionString));

            try
            {
                var packetNum = 0;
                var packetCount = 0;

                // initialize processors
                InitProcessors();
                
                while (reader.CanRead())
                {
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

                    // finish if read packet count reached max
                    if (Settings.ReaderFilterPacketsNum > 0 && packetCount == Settings.ReaderFilterPacketsNum)
                        break;
                }
                // finalize processors
                foreach (var procs in Processors)
                {
                    procs.Value.Finish();
                }

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

        public override void InitProcessors()
        {
            base.InitProcessors();
            var procss = Utilities.GetClasses(typeof(IPacketProcessor));
            foreach (var p in procss)
            {
                if (p.IsAbstract || p.IsInterface)
                    continue;
                IPacketProcessor instance = (IPacketProcessor)Activator.CreateInstance(p);
                if (instance.Init(this))
                    Processors[p] = instance;
            }
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

        public void ProcessData(Packet data)
        {
            var itr = data.GetTreeEnumerator();
            while (itr.MoveNext())
            {
                foreach (var p in Processors)
                {
                    var proc = p.Value;
                    foreach (var i in itr.CurrentClosedNodes)
                    {
                        if (i.type == typeof(Packet))
                            proc.ProcessedPacket(i.obj as Packet);
                    }
                    if (itr.Type == typeof(Packet))
                    {
                        Packet packet = (Packet)itr.Current;
                        proc.ProcessPacket(packet);
                    }
                    proc.ProcessData(itr.Name, itr.Index, itr.Current, itr.Type);
                }
            }
            foreach (var proc in Processors)
            {
                foreach (var i in itr.CurrentClosedNodes)
                {
                    if (i.type == typeof(Packet))
                        proc.Value.ProcessedPacket(i.obj as Packet);
                }
            }
        }
    }
}
