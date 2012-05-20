using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.SQL;
using WowPacketParser.Saving;
using WowPacketParser.Store;

namespace WowPacketParser.Loading
{
    public class SniffFile
    {
        private readonly string _fileName;
        private readonly string _outFileName;
        private readonly Statistics _stats;
        private LinkedList<Packet> _packets;
        private readonly string _logPrefix;
        private readonly SQLOutputFlags _sqlOutput;

        public SniffFile(string fileName, Tuple<int, int> number = null, SQLOutputFlags sqlOutput = SQLOutputFlags.None)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("fileName cannot be null, empty or whitespace.", "fileName");

            _stats = new Statistics();
            _packets = null;
            _fileName = fileName;
            _sqlOutput = sqlOutput;

            _outFileName = Path.ChangeExtension(fileName, null) + "_parsed.txt";

            if (number == null)
                _logPrefix = string.Format("[{0}]", Path.GetFileName(fileName));
            else
                _logPrefix = string.Format("[{0}/{1} {2}]", number.Item1, number.Item2, Path.GetFileName(fileName));          
        }

        public void ProcessFile()
        {
            // reset data loaded from prev file
            Storage.ClearContainers();

            // read raw packet data from file
            ReadPackets();
            
            // dump raw packet data if needed
            WriteBinary();

            // parse packets, dump txt, sql etc
            ProcessPackets();

            // 
            WriteSQLs();
        }

        private void ReadPackets()
        {
            Trace.WriteLine(string.Format("{0}: Reading packets...", _logPrefix));
            _packets = new LinkedList<Packet>(Reader.Read(_fileName));
        }

        private string GetHeader()
        {
            return "# TrinityCore - WowPacketParser" + Environment.NewLine +
                   "# File name: " + Path.GetFileName(_fileName) + Environment.NewLine +
                   "# Detected build: " + ClientVersion.Build + Environment.NewLine +
                   "# Parsing date: " + DateTime.Now.ToString() +
                   Environment.NewLine;
        }

        private void ProcessPackets()
        {
            Trace.WriteLine(string.Format("{0}: Parsing {1} packets. Assumed version {2}",
                _logPrefix, _packets.Count, ClientVersion.VersionString));

            File.Delete(_outFileName);

            /*
                if (Utilities.FileIsInUse(_outFileName))
                {
                    // If our dump format requires a .txt to be created,
                    // check if we can write to that .txt before starting parsing
                    Trace.WriteLine(string.Format("Save file {0} is in use, parsing will not be done.", _outFileName));
                    return;
                }
            */

            using (var writer = new StreamWriter(_outFileName, true))
            {
                _stats.SetStartTime(DateTime.Now);
                foreach (var packet in _packets)
                {
                    // Parse the packet, read the data into StoreData tree
                    Handler.Parse(packet);

                    // Update statistics
                    _stats.AddByStatus(packet.Status);

                    // Write to file
                    writer.WriteLine(Handler.DumpAsText(packet));
                    writer.Flush();

                    // Close Writer, Stream - Dispose
                    packet.ClosePacket();
                }
                _stats.SetEndTime(DateTime.Now);
            }
            _packets.Clear();

            Trace.WriteLine(string.Format("{0}: Saved file to '{1}'", _logPrefix, _outFileName));
            Trace.WriteLine(string.Format("{0}: {1}", _logPrefix, _stats));
        }

        private void WriteBinary()
        {
            if (Settings.BinaryOutputType == "")
                return;
            try
            {
                IBinaryWriter packetWriter;
                switch (Settings.BinaryOutputType)
                {
                    case "kszor":
                        packetWriter = new KSZorBinaryPacketWriter();
                        break;
                    default:
                        throw new Exception("Unsuported binary packet writer type");
                }

                if (Settings.SplitBinaryOutput)
                {
                    Trace.WriteLine(string.Format("{0}: Splitting {1} packets to multiple binary({2}) files...", _logPrefix, _packets.Count, Settings.BinaryOutputType));

                    Directory.CreateDirectory(SplitBinaryOutput.Folder); // not doing anything if it exists already

                    foreach (var packet in _packets)
                    {
                        var fileName = SplitBinaryOutput.Folder + "/" + Enums.Version.Opcodes.GetOpcodeName(packet.Opcode) + "." + Settings.BinaryOutputType.ToString().ToLower();
                        try
                        {
                            using (SplitBinaryOutput.Locks.Lock(fileName))
                            {
                                bool existed = !File.Exists(fileName);
                                var fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.None);
                                using (var writer = new BinaryWriter(fileStream, Encoding.ASCII))
                                {
                                    if (!existed)
                                        packetWriter.WriteHeader(writer);
                                    packetWriter.WritePacket(packet, writer);
                                }
                            }
                        }
                        catch (TimeoutException)
                        {
                            Trace.WriteLine(string.Format("Timeout trying to write Opcode to {0} ignoring opcode", fileName));
                        }
                    }
                }
                else
                {
                    Trace.WriteLine(string.Format("{0}: Copying {1} packets to binary({2})...", _logPrefix, _packets.Count, Settings.BinaryOutputType));
                    var dumpFileName = Path.ChangeExtension(_fileName, null) + "_excerpt.pkt";

                    var fileStream = new FileStream(dumpFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                    using (var writer = new BinaryWriter(fileStream, Encoding.ASCII))
                    {
                        packetWriter.WriteHeader(writer);
                        foreach (var packet in _packets)
                        {
                            packetWriter.WritePacket(packet, writer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.GetType());
                Trace.WriteLine(ex.Message);
                Trace.WriteLine(ex.StackTrace);
            }
        }

        private void WriteSQLs()
        {
            if (_sqlOutput != SQLOutputFlags.None)
                return;
            string sqlFileName;
            if (String.IsNullOrWhiteSpace(Settings.SQLFileName))
                sqlFileName = string.Format("{0}_{1}.sql",
                    Utilities.FormattedDateTimeForFiles(), Path.GetFileName(_fileName));
            else
                sqlFileName = Settings.SQLFileName;

            Builder.DumpSQL(string.Format("{0}: Dumping sql", _logPrefix), sqlFileName, GetHeader());
        }
    }
}
