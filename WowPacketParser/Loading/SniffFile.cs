using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
        private readonly DumpFormatType _dumpFormat;
        private readonly string _logPrefix;
        private readonly bool _splitOutput;
        private readonly SQLOutputFlags _sqlOutput;

        private readonly LinkedList<string> _withErrorHeaders = new LinkedList<string>();
        private readonly LinkedList<string> _skippedHeaders = new LinkedList<string>();

        public SniffFile(string fileName, DumpFormatType dumpFormat = DumpFormatType.Text, bool splitOutput = false, Tuple<int, int> number = null, SQLOutputFlags sqlOutput = SQLOutputFlags.None)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("fileName cannot be null, empty or whitespace.", "fileName");

            _stats = new Statistics();
            _packets = null;
            _fileName = fileName;
            _splitOutput = splitOutput;
            _dumpFormat = dumpFormat;
            _sqlOutput = sqlOutput;

            _outFileName = Path.ChangeExtension(fileName, null) + "_parsed.txt";

            if (number == null)
                _logPrefix = string.Format("[{0}]", Path.GetFileName(fileName));
            else
                _logPrefix = string.Format("[{0}/{1} {2}]", number.Item1, number.Item2, Path.GetFileName(fileName));
        }

        public void ProcessFile()
        {
            switch (_dumpFormat)
            {
                case DumpFormatType.Text:
                {
                    if (Utilities.FileIsInUse(_outFileName))
                    {
                        // If our dump format requires a .txt to be created,
                        // check if we can write to that .txt before starting parsing
                        Trace.WriteLine(string.Format("Save file {0} is in use, parsing will not be done.", _outFileName));
                        return;
                    }

                    Store.Store.Flags = _sqlOutput;

                    if (!ReadPackets())
                        return;

                    ParsePackets();

                    if (_sqlOutput != SQLOutputFlags.None)
                        WriteSQLs();

                    if (Settings.LogPacketErrors)
                        WritePacketErrors();

                    GC.Collect(); // Force a GC collect after parsing a file. It seems to help.

                    break;
                }
                case DumpFormatType.Pkt:
                {
                    if (!ReadPackets())
                        return;

                    if (_splitOutput)
                        SplitBinaryDump();
                    else
                        BinaryDump();

                    break;
                }
                default:
                {
                    Trace.WriteLine(string.Format("{0}: Dump format is none, nothing will be processed.", _logPrefix));
                    break;
                }
            }
        }

        private bool ReadPackets()
        {
            Trace.WriteLine(string.Format("{0}: Reading packets...", _logPrefix));
            try
            {
                _packets = (LinkedList<Packet>) Reader.Read(_fileName);
                return true;
            }
            catch (IOException ex)
            {
                Trace.WriteLine(ex.Message);
                Trace.WriteLine("Skipped.");
                return false;
            }
        }

        private string GetHeader()
        {
            return "# TrinityCore - WowPacketParser" + Environment.NewLine +
                   "# File name: " + Path.GetFileName(_fileName) + Environment.NewLine +
                   "# Detected build: " + ClientVersion.Build + Environment.NewLine +
                   "# Parsing date: " + DateTime.Now.ToString(CultureInfo.InvariantCulture) +
                   Environment.NewLine;
        }

        private void ParsePackets()
        {
            var packetCount = _packets.Count;

            File.Delete(_outFileName);

            if (packetCount == 0)
            {
                Trace.WriteLine(string.Format("{0}: Skipped output, packet count is 0.", _logPrefix));
                return;
            }

            Trace.WriteLine(string.Format("{0}: Parsing {1} packets. Assumed version {2}",
                    _logPrefix, packetCount, ClientVersion.VersionString));

            using (var writer = new StreamWriter(_outFileName, true))
            {
                var i = 1;

                writer.WriteLine(GetHeader());

                _stats.SetStartTime(DateTime.Now);
                foreach (var packet in _packets)
                {
                    ShowPercentProgress("Processing...", i++, packetCount);

                    // Parse the packet, adding text to Writer and stuff to the stores
                    Handler.Parse(packet);

                    // Update statistics
                    _stats.AddByStatus(packet.Status);

                    // get packet header if necessary
                    if (Settings.LogPacketErrors)
                    {
                        if (packet.Status == ParsedStatus.WithErrors)
                            _withErrorHeaders.AddLast(packet.GetHeader());
                        else if (packet.Status == ParsedStatus.NotParsed)
                            _skippedHeaders.AddLast(packet.GetHeader());
                    }

                    // Write to file
                    writer.WriteLine(packet.Writer);
                    writer.Flush();

                    // Close Writer, Stream - Dispose
                    packet.ClosePacket();
                }
                _stats.SetEndTime(DateTime.Now);
            }

            _packets.Clear();
            _packets = null;
            Trace.WriteLine(string.Format("{0}: Saved file to '{1}'", _logPrefix, _outFileName));
            Trace.WriteLine(string.Format("{0}: {1}", _logPrefix, _stats));
        }

        private static int _lastPercent;
        static void ShowPercentProgress(string message, int currElementIndex, int totalElementCount)
        {
            var percent = (100 * currElementIndex) / totalElementCount;
            if (percent == _lastPercent)
                return; // we only need to update if percentage changes otherwise we would be wasting precious resources

            _lastPercent = percent;

            Console.Write("\r{0} {1}% complete", message, percent);
            if (currElementIndex == totalElementCount)
                Console.WriteLine();
        }

        private void SplitBinaryDump()
        {
            Trace.WriteLine(string.Format("{0}: Splitting {1} packets to multiple files...", _logPrefix, _packets.Count));

            try
            {
                SplitBinaryPacketWriter.Write(_packets, Encoding.ASCII);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.GetType());
                Trace.WriteLine(ex.Message);
                Trace.WriteLine(ex.StackTrace);
            }
        }

        private void BinaryDump()
        {
            Trace.WriteLine(string.Format("{0}: Copying {1} packets to .pkt format...", _logPrefix, _packets.Count));
            var dumpFileName = Path.ChangeExtension(_fileName, null) + "_excerpt.pkt";

            try
            {
                BinaryPacketWriter.Write(SniffType.Pkt, dumpFileName, Encoding.ASCII, _packets);
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
            string sqlFileName;
            if (String.IsNullOrWhiteSpace(Settings.SQLFileName))
                sqlFileName = string.Format("{0}_{1}.sql",
                    Utilities.FormattedDateTimeForFiles(), Path.GetFileName(_fileName));
            else
                sqlFileName = Settings.SQLFileName;

            Builder.DumpSQL(string.Format("{0}: Dumping sql", _logPrefix), sqlFileName, GetHeader());
            Storage.ClearContainers();
        }

        private void WritePacketErrors()
        {
            if (_withErrorHeaders.Count == 0 && _skippedHeaders.Count == 0)
                return;

            var fileName = Path.GetFileNameWithoutExtension(_fileName) + "_errors.txt";

            using (var file = new StreamWriter(fileName))
            {
                file.WriteLine(GetHeader());

                if (_withErrorHeaders.Count != 0)
                {
                    file.WriteLine("- Packets with errors:");
                    foreach (var header in _withErrorHeaders)
                        file.WriteLine(header);
                    file.WriteLine();
                }

                if (_skippedHeaders.Count != 0)
                {
                    file.WriteLine("- Packets not parsed:");
                    foreach (var header in _skippedHeaders)
                        file.WriteLine(header);
                }
            }
        }
    }
}
