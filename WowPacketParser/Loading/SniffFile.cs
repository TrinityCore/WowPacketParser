using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Saving;
using WowPacketParser.SQL;
using WowPacketParser.Store;

namespace WowPacketParser.Loading
{
    public class SniffFile
    {
        private string _fileName;
        private readonly string _originalFileName;
        private readonly Statistics _stats;
        private readonly DumpFormatType _dumpFormat;
        private readonly string _logPrefix;

        private readonly List<string> _withErrorHeaders = new List<string>();
        private readonly List<string> _skippedHeaders = new List<string>();
        private readonly List<string> _noStructureHeaders = new List<string>();

        public SniffFile(string fileName, DumpFormatType dumpFormat = DumpFormatType.Text, Tuple<int, int> number = null)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("fileName cannot be null, empty or whitespace.", "fileName");

            _stats = new Statistics();
            _fileName = fileName;
            _originalFileName = fileName;

            var extension = Path.GetExtension(_originalFileName);
            if (extension.ToLower() == ".gz")
                _originalFileName = _originalFileName.Remove(_originalFileName.Length - extension.Length);

            _dumpFormat = dumpFormat;

            if (number == null)
                _logPrefix = string.Format("[{0}]", Path.GetFileName(_originalFileName));
            else
                _logPrefix = string.Format("[{0}/{1} {2}]", number.Item1, number.Item2, Path.GetFileName(_originalFileName));
        }

        public void ProcessFile()
        {
            string tempFileName = null;

            try
            {
                tempFileName = ProcessFileImpl();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(_logPrefix + " " + ex.GetType());
                Trace.WriteLine(_logPrefix + " " + ex.Message);
                Trace.WriteLine(_logPrefix + " " + ex.StackTrace);
            }
            finally
            {
                if (tempFileName != null)
                {
                    File.Delete(tempFileName);
                    Trace.WriteLine(_logPrefix + " Deleted temporary file " + Path.GetFileName(tempFileName));
                }
            }
        }

        private string ProcessFileImpl()
        {
            string tempFile = null;
            var extension = Path.GetExtension(_fileName);
            if (extension != null && extension.ToLower() == ".gz")
            {
                tempFile = Decompress(new FileInfo(_fileName));
                _fileName = tempFile;
            }

            switch (_dumpFormat)
            {
                case DumpFormatType.StatisticsPreParse:
                {
                    var packets = ReadPackets();
                    if (packets.Count == 0)
                        break;

                    var firstPacket = packets.First();
                    var lastPacket = packets.Last();

                    // CSV format
                    Trace.WriteLine(String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8}",
                        _originalFileName,                                                 // - sniff file name
                        firstPacket.Time,                                                  // - time of first packet
                        lastPacket.Time,                                                   // - time of last packet
                        (lastPacket.Time - firstPacket.Time).TotalSeconds,                 // - sniff duration (seconds)
                        packets.Count,                                                     // - packet count
                        packets.AsParallel().Sum(packet => packet.Length),                 // - total packets size (bytes)
                        packets.AsParallel().Average(packet => packet.Length),             // - average packet size (bytes)
                        packets.AsParallel().Min(packet => packet.Length),                 // - smaller packet size (bytes)
                        packets.AsParallel().Max(packet => packet.Length)));               // - larger packet size (bytes)

                    break;
                }
                case DumpFormatType.SniffDataOnly:
                case DumpFormatType.SqlOnly:
                case DumpFormatType.Text:
                case DumpFormatType.HexOnly:
                {
                    var outFileName = Path.ChangeExtension(_originalFileName, null) + "_parsed.txt";

                    if (Utilities.FileIsInUse(outFileName) && Settings.DumpFormat != DumpFormatType.SqlOnly)
                    {
                        // If our dump format requires a .txt to be created,
                        // check if we can write to that .txt before starting parsing
                        Trace.WriteLine(string.Format("Save file {0} is in use, parsing will not be done.", outFileName));
                        break;
                    }

                    Store.Store.SQLEnabledFlags = Settings.SQLOutputFlag;
                    Store.Store.HotfixSQLEnabledFlags = Settings.HotfixSQLOutputFlag;

                    File.Delete(outFileName);

                    _stats.SetStartTime(DateTime.Now);

                    int threadCount = Settings.Threads;
                    if (threadCount == 0)
                        threadCount = Environment.ProcessorCount;

                    ThreadPool.SetMinThreads(threadCount + 2, 4);

                    var written = false;
                    using (var writer = (Settings.DumpFormatWithText() ? new StreamWriter(outFileName, true) : null))
                    {
                        var firstRead = true;
                        var firstWrite = true;

                        var reader = new Reader(_fileName, _originalFileName);

                        var pwp = new ParallelWorkProcessor<Packet>(() => // read
                        {
                            if (!reader.PacketReader.CanRead())
                                return Tuple.Create<Packet, bool>(null, true);

                            Packet packet;
                            var b = reader.TryRead(out packet);

                            if (firstRead)
                            {
                                Trace.WriteLine(string.Format("{0}: Parsing {1} of packets. Detected version {2}",
                                    _logPrefix, Utilities.BytesToString(reader.PacketReader.GetTotalSize()), ClientVersion.VersionString));

                                firstRead = false;
                            }

                            return Tuple.Create(packet, b);
                        }, packet => // parse
                        {
                            // Parse the packet, adding text to Writer and stuff to the stores
                            if (packet.Direction == Direction.BNClientToServer ||
                                packet.Direction == Direction.BNServerToClient)
                                Handler.ParseBattlenet(packet);
                            else
                                Handler.Parse(packet);

                            // Update statistics
                            _stats.AddByStatus(packet.Status);
                            return packet;
                        },
                        packet => // write
                        {
                            ShowPercentProgress("Processing...", reader.PacketReader.GetCurrentSize(), reader.PacketReader.GetTotalSize());

                            if (!packet.Status.HasAnyFlag(Settings.OutputFlag) || !packet.WriteToFile)
                            {
                                packet.ClosePacket();
                                return;
                            }

                            written = true;

                            if (firstWrite)
                            {
                                // ReSharper disable AccessToDisposedClosure
                                if (writer != null)
                                    writer.WriteLine(GetHeader(_originalFileName));
                                // ReSharper restore AccessToDisposedClosure

                                firstWrite = false;
                            }

                            // get packet header if necessary
                            if (Settings.LogPacketErrors)
                            {
                                switch (packet.Status)
                                {
                                    case ParsedStatus.WithErrors:
                                        _withErrorHeaders.Add(packet.GetHeader());
                                        break;
                                    case ParsedStatus.NotParsed:
                                        _skippedHeaders.Add(packet.GetHeader());
                                        break;
                                    case ParsedStatus.NoStructure:
                                        _noStructureHeaders.Add(packet.GetHeader());
                                        break;
                                }
                            }

// ReSharper disable AccessToDisposedClosure
                            if (writer != null)
                            {
                                // Write to file
                                writer.WriteLine(packet.Writer);
                                writer.Flush();
                            }
// ReSharper restore AccessToDisposedClosure

                            // Close Writer, Stream - Dispose
                            packet.ClosePacket();
                        }, threadCount);

                        pwp.WaitForFinished(Timeout.Infinite);

                        reader.PacketReader.Dispose();

                        _stats.SetEndTime(DateTime.Now);
                    }

                    if (written)
                        Trace.WriteLine(string.Format("{0}: Saved file to '{1}'", _logPrefix, outFileName));
                    else
                    {
                        Trace.WriteLine(string.Format("{0}: No file produced", _logPrefix));
                        File.Delete(outFileName);
                    }

                    Trace.WriteLine(string.Format("{0}: {1}", _logPrefix, _stats));

                    if (Settings.SQLOutputFlag != 0 || Settings.HotfixSQLOutputFlag != 0)
                        WriteSQLs();

                    if (Settings.LogPacketErrors)
                        WritePacketErrors();

                    GC.Collect(); // Force a GC collect after parsing a file. It seems to help.

                    break;
                }
                case DumpFormatType.Pkt:
                {
                    var packets = ReadPackets();
                    if (packets.Count == 0)
                        break;

                    if (Settings.FilterPacketsNum < 0)
                    {
                        int packetsPerSplit = Math.Abs(Settings.FilterPacketsNum);
                        int totalPackets = packets.Count;

                        int numberOfSplits = (int)Math.Ceiling((double)totalPackets/packetsPerSplit);

                        for (int i = 0; i < numberOfSplits; ++i)
                        {
                            var fileNamePart = _originalFileName + "_part_" + (i + 1) + ".pkt";

                            var packetsPart = packets.Take(packetsPerSplit).ToList();
                            packets.RemoveRange(0, packetsPart.Count);

                            BinaryDump(fileNamePart, packetsPart);
                        }
                    }
                    else
                    {
                        var fileNameExcerpt = Path.ChangeExtension(_originalFileName, null) + "_excerpt.pkt";
                        BinaryDump(fileNameExcerpt, packets);
                    }

                    break;
                }
                case DumpFormatType.PktSplit:
                {
                    var packets = ReadPackets();
                    if (packets.Count == 0)
                        break;

                    SplitBinaryDump(packets);
                    break;
                }
                case DumpFormatType.PktDirectionSplit:
                {
                    var packets = ReadPackets();
                    if (packets.Count == 0)
                        break;

                    DirectionSplitBinaryDump(packets);
                    break;
                }
                case DumpFormatType.PktSessionSplit:
                {
                    var packets = ReadPackets();
                    if (packets.Count == 0)
                        break;

                    SessionSplitBinaryDump(packets);
                    break;
                }
                case DumpFormatType.CompressSniff:
                {
                    if (extension == null || extension.ToLower() == ".gz")
                    {
                        Trace.WriteLine("Skipped compressing file {0}", _fileName);
                        break;
                    }

                    var fi = new FileInfo(_fileName);
                    Compress(fi);
                    break;
                }
                case DumpFormatType.SniffVersionSplit:
                {
                    var reader = new Reader(_fileName, _originalFileName);

                    if (ClientVersion.IsUndefined() && reader.PacketReader.CanRead())
                    {
                        Packet packet;
                        reader.TryRead(out packet);
                        packet.ClosePacket();
                    }

                    reader.PacketReader.Dispose();

                    string version = ClientVersion.IsUndefined() ? "unknown" : ClientVersion.VersionString;

                    string realFileName = _originalFileName + (_fileName != _originalFileName ? ".gz" : "");

                    string destPath = Path.Combine(Path.GetDirectoryName(realFileName), version,
                        Path.GetFileName(realFileName));

                    string destDir = Path.GetDirectoryName(destPath);
                    if (!Directory.Exists(destDir))
                        Directory.CreateDirectory(destDir);

                    File.Move(realFileName, destPath);

                    Trace.WriteLine("Moved " + realFileName + " to " + destPath);

                    break;
                }
                case DumpFormatType.ConnectionIndexes:
                {
                    var packets = ReadPackets();
                    if (packets.Count == 0)
                        break;

                    using (var writer = new StreamWriter(Path.ChangeExtension(_originalFileName, null) + "_connidx.txt"))
                    {
                        if (ClientVersion.Build <= ClientVersionBuild.V6_0_3_19342)
                            writer.WriteLine("# Warning: versions before 6.1 might not have proper ConnectionIndex values.");

                        IEnumerable<IGrouping<Tuple<int, Direction>, Packet>> groupsOpcode = packets
                            .GroupBy(packet => Tuple.Create(packet.Opcode, packet.Direction))
                            .OrderBy(grouping => grouping.Key.Item2);

                        foreach (IGrouping<Tuple<int, Direction>, Packet> groupOpcode in groupsOpcode)
                        {
                            List<IGrouping<int, Packet>> groups = groupOpcode
                                .GroupBy(packet => packet.ConnectionIndex)
                                .OrderBy(grouping => grouping.Key)
                                .ToList();

                            writer.Write("{0} {1,-50}: ", groupOpcode.Key.Item2, Opcodes.GetOpcodeName(groupOpcode.Key.Item1, groupOpcode.Key.Item2));

                            for (int i = 0; i < groups.Count; i++)
                            {
                                int idx = groups[i].Key;
                                writer.Write("{0} ({1}{2})", idx, (idx & 1) != 0 ? "INSTANCE" : "REALM", (idx & 2) != 0 ? "_NEW" : "");

                                if (i != groups.Count - 1)
                                    writer.Write(", ");
                            }

                            writer.WriteLine();
                        }
                    }

                    break;
                }
                default:
                {
                    Trace.WriteLine(string.Format("{0}: Dump format is none, nothing will be processed.", _logPrefix));
                    break;
                }
            }

            return tempFile;
        }

        public static string GetHeader(string fileName)
        {
            return "# TrinityCore - WowPacketParser" + Environment.NewLine +
                   "# File name: " + Path.GetFileName(fileName) + Environment.NewLine +
                   "# Detected build: " + ClientVersion.Build + Environment.NewLine +
                   "# Detected locale: " + BinaryPacketReader.GetClientLocale() + Environment.NewLine +
                   "# Parsing date: " + DateTime.Now.ToString(CultureInfo.InvariantCulture) + Environment.NewLine;
        }

        private static long _lastPercent;
        static void ShowPercentProgress(string message, long curr, long total)
        {
            var percent = (100 * curr) / total;
            if (percent == _lastPercent)
                return; // we only need to update if percentage changes otherwise we would be wasting precious resources

            _lastPercent = percent;

            Console.Write("\r{0} {1}% complete", message, percent);
            if (curr == total)
                Console.WriteLine();
        }

        public List<Packet> ReadPackets()
        {
            var packets = new List<Packet>();

            // stats.SetStartTime(DateTime.Now);

            Reader.Read(_fileName, _originalFileName, p =>
            {
                var packet = p.Item1;
                var currSize = p.Item2;
                var totalSize = p.Item3;

                ShowPercentProgress("Reading...", currSize, totalSize);
                packets.Add(packet);
            });

            return packets;

            // stats.SetEndTime(DateTime.Now);
            // Trace.WriteLine(string.Format("{0}: {1}", _logPrefix, _stats));
        }

        private void SplitBinaryDump(ICollection<Packet> packets)
        {
            Trace.WriteLine(string.Format("{0}: Splitting {1} packets to multiple files...", _logPrefix, packets.Count));
            SplitBinaryPacketWriter.Write(packets, Encoding.ASCII);
        }

        private void DirectionSplitBinaryDump(ICollection<Packet> packets)
        {
            Trace.WriteLine(string.Format("{0}: Splitting {1} packets to multiple files...", _logPrefix, packets.Count));
            SplitDirectionBinaryPacketWriter.Write(packets, Encoding.ASCII);
        }

        private void SessionSplitBinaryDump(ICollection<Packet> packets)
        {
            Trace.WriteLine(string.Format("{0}: Splitting {1} packets to multiple files...", _logPrefix, packets.Count));
            SplitSessionBinaryPacketWriter.Write(packets, Encoding.ASCII);
        }

        private void BinaryDump(string fileName, ICollection<Packet> packets)
        {
            Trace.WriteLine(string.Format("{0}: Copying {1} packets to .pkt format...", _logPrefix, packets.Count));
            BinaryPacketWriter.Write(SniffType.Pkt, fileName, Encoding.ASCII, packets);
        }

        private void WriteSQLs()
        {
            string sqlFileName;
            if (String.IsNullOrWhiteSpace(Settings.SQLFileName))
                sqlFileName = string.Format("{0}_{1}.sql",
                    Utilities.FormattedDateTimeForFiles(), Path.GetFileName(_originalFileName));
            else
                sqlFileName = Settings.SQLFileName;

            if (String.IsNullOrWhiteSpace(Settings.SQLFileName))
            {
                Builder.DumpSQL(string.Format("{0}: Dumping sql", _logPrefix), sqlFileName, GetHeader(_originalFileName));
                Storage.ClearContainers();
            }
        }

        private void WritePacketErrors()
        {
            if (_withErrorHeaders.Count == 0 && _skippedHeaders.Count == 0 && _noStructureHeaders.Count == 0)
                return;

            var fileName = Path.GetFileNameWithoutExtension(_originalFileName) + "_errors.txt";

            using (var file = new StreamWriter(fileName))
            {
                file.WriteLine(GetHeader(_originalFileName));

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
                    file.WriteLine();
                }

                if (_noStructureHeaders.Count != 0)
                {
                    file.WriteLine("- Packets without structure:");
                    foreach (var header in _noStructureHeaders)
                        file.WriteLine(header);
                }
            }
        }

        private void Compress(FileInfo fileToCompress)
        {
            using (var originalFileStream = fileToCompress.OpenRead())
            {
                using (var compressedFileStream = File.Create(fileToCompress.FullName + ".gz"))
                {
                    using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                    {
                        originalFileStream.CopyTo(compressionStream);
                        Console.WriteLine("{0} Compressed {1} from {2} to {3} bytes.",
                            _logPrefix, fileToCompress.Name, fileToCompress.Length, compressedFileStream.Length);
                    }
                }
            }
        }

        public string Decompress(FileInfo fileToDecompress)
        {
            using (var originalFileStream = fileToDecompress.OpenRead())
            {
                var newFileName = Path.GetTempFileName();

                using (var decompressedFileStream = File.Create(newFileName))
                {
                    using (var decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        Console.WriteLine("{0} Decompressed {1} to {2}", _logPrefix, fileToDecompress.Name, Path.GetFileName(newFileName));
                        return newFileName;
                    }
                }
            }
        }
    }
}
