using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.PacketStructures;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Saving;
using WowPacketParser.SQL;
using WowPacketParser.Store;

namespace WowPacketParser.Loading
{
    public class SniffFile
    {
        private string _fileName;
        private string _tempName;
        private FileCompression _compression;
        private SniffType _sniffType;

        private readonly Statistics _stats;
        private readonly DumpFormatType _dumpFormat;
        private readonly string _logPrefix;

        private readonly List<string> _withErrorHeaders = new List<string>();
        private readonly List<string> _skippedHeaders = new List<string>();
        private readonly List<string> _noStructureHeaders = new List<string>();

        public SniffFile(string fileName, DumpFormatType dumpFormat = DumpFormatType.Text, Tuple<int, int> number = null)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("fileName cannot be null, empty or whitespace.", nameof(fileName));

            _stats = new Statistics();

            FileName = fileName;
            _dumpFormat = dumpFormat;

            _logPrefix = number == null ? $"[{Path.GetFileName(FileName)}]" : $"[{number.Item1}/{number.Item2} {Path.GetFileName(FileName)}]";
        }

        private string FileName
        {
            get { return _fileName; }
            set
            {
                var extension = Path.GetExtension(value);
                if (extension == null)
                    throw new IOException($"Invalid file type {_fileName}");

                _compression = extension.ToFileCompressionEnum();

                _fileName = _compression != FileCompression.None ? value.Remove(value.Length - extension.Length) : value;

                extension = Path.GetExtension(_fileName);
                if (extension == null)
                    throw new IOException($"Invalid file type {_fileName}");

                switch (extension.ToLower())
                {
                    case ".bin":
                        _sniffType = SniffType.Bin;
                        break;
                    case ".pkt":
                        _sniffType = SniffType.Pkt;
                        break;
                    case ".sqlite":
                        _sniffType = SniffType.Sqlite;
                        break;
                    default:
                        throw new IOException($"Invalid file type {_fileName}");
                }
            }
        }

        public Packets ProcessFile()
        {

            try
            {
                return ProcessFileImpl();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(_logPrefix + " " + ex.GetType());
                Trace.WriteLine(_logPrefix + " " + ex.Message);
                Trace.WriteLine(_logPrefix + " " + ex.StackTrace);
                return null;
            }
            finally
            {
                if (_tempName != null)
                {
                    File.Delete(_tempName);
                    Trace.WriteLine(_logPrefix + " Deleted temporary file " + Path.GetFileName(_tempName));
                }
            }
        }

        private Packets ProcessFileImpl()
        {
            if (_compression != FileCompression.None)
                _tempName = Decompress();

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
                    // ReSharper disable once UseStringInterpolation
                    Trace.WriteLine(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8}",
                        FileName,                                                          // - sniff file name
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
                case DumpFormatType.UniversalProto:
                case DumpFormatType.UniversalProtoWithText:
                case DumpFormatType.UniversalProtoWithSeparateText:
                {
                    var outFileName = Path.ChangeExtension(FileName, null) + "_parsed.txt";
                    var outProtoFileName = Path.ChangeExtension(FileName, null) + "_parsed.dat";
                    FileStream protoOutputStream = null;

                    if (Settings.DumpFormatWithTextToFile())
                    {
                        if (Utilities.FileIsInUse(outFileName) && Settings.DumpFormat != DumpFormatType.SqlOnly)
                        {
                            // If our dump format requires a .txt to be created,
                            // check if we can write to that .txt before starting parsing
                            Trace.WriteLine($"Save file {outFileName} is in use, parsing will not be done.");
                            break;
                        }
                        File.Delete(outFileName);
                    }

                    if (_dumpFormat.IsUniversalProtobufType())
                    {
                        if (Utilities.FileIsInUse(outProtoFileName))
                        {
                            Trace.WriteLine($"Save file {outProtoFileName} is in use, parsing will not be done.");
                            break;
                        }
                        File.Delete(outProtoFileName);
                        protoOutputStream = File.Create(outProtoFileName);
                    }

                    Store.Store.SQLEnabledFlags = Settings.SQLOutputFlag;
                    bool movementEnabled = Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_movement);

                    _stats.SetStartTime(DateTime.Now);

                    var threadCount = Settings.Threads;
                    if (threadCount == 0)
                        threadCount = Environment.ProcessorCount;

                    ThreadPool.SetMinThreads(threadCount + 2, 4);

                    var written = false;

                    Packets packets = new() { Version = StructureVersion.ProtobufStructureVersion, DumpType = (uint)Settings.DumpFormat };
                    using (var writer = (Settings.DumpFormatWithTextToFile() ? new StreamWriter(outFileName, true) : null))
                    {
                        var firstRead = true;
                        var firstWrite = true;

                        var reader = _compression != FileCompression.None ? new Reader(_tempName, _sniffType) : new Reader(FileName, _sniffType);

                        var pwp = new ParallelWorkProcessor<Packet>(() => // read
                        {
                            if (!reader.PacketReader.CanRead())
                                return Tuple.Create<Packet, bool>(null, true);

                            Packet packet;
                            var b = reader.TryRead(out packet);

                            if (firstRead)
                            {
                                Trace.WriteLine(
                                    $"{_logPrefix}: Parsing {Utilities.BytesToString(reader.PacketReader.GetTotalSize())} of packets. Detected version {ClientVersion.VersionString}");
                                packets.GameVersion = (ulong)ClientVersion.Build;
                                firstRead = false;
                            }

                            return Tuple.Create(packet, b);
                        }, packet => // parse
                        {
                            // Parse the packet, adding text to Writer and stuff to the stores
                            if (packet.Direction == Direction.BNClientToServer ||
                                packet.Direction == Direction.BNServerToClient)
                                BattlenetHandler.ParseBattlenet(packet);
                            else
                                Handler.Parse(packet);

                            // Update statistics
                            _stats.AddByStatus(packet.Status);
                            return packet;
                        },
                        packet => // write
                        {
                            if (!Console.IsOutputRedirected)
                                ShowPercentProgress("Processing...", reader.PacketReader.GetCurrentSize(), reader.PacketReader.GetTotalSize());
                            else
                                Console.WriteLine(reader.PacketReader.GetCurrentSize() * 1.0 / reader.PacketReader.GetTotalSize());

                            if (!packet.Status.HasAnyFlag(Settings.OutputFlag) || !packet.WriteToFile)
                            {
                                packet.ClosePacket();
                                return;
                            }

                            written = true;

                            if (firstWrite)
                            {
                                // ReSharper disable AccessToDisposedClosure
                                writer?.WriteLine(GetHeader(FileName));
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
                                var startOffset = writer.BaseStream.Position;
                                writer.WriteLine(packet.Writer);
                                writer.Flush();

                                if (_dumpFormat is DumpFormatType.UniversalProtoWithSeparateText)
                                {
                                    packet.Holder.BaseData.TextStartOffset = startOffset;
                                    packet.Holder.BaseData.TextLength = (int)(writer.BaseStream.Position - startOffset);
                                }
                            }
// ReSharper restore AccessToDisposedClosure

                            if (_dumpFormat is DumpFormatType.UniversalProtoWithText)
                                packet.Holder.BaseData.StringData = packet.Writer.ToString();

                            // Close Writer, Stream - Dispose
                            packet.ClosePacket();

                            if (_dumpFormat.IsUniversalProtobufType() || movementEnabled || HotfixSettings.Instance.ShouldLog())
                            {
                                if (_dumpFormat.IsUniversalProtobufType() || HotfixSettings.Instance.ShouldLog())
                                    packets.Packets_.Add(packet.Holder);
                                else if (movementEnabled && packet.Holder.MonsterMove != null)
                                    packets.Packets_.Add(packet.Holder);
                            }
                        }, threadCount);

                        pwp.WaitForFinished(Timeout.Infinite);

                        reader.PacketReader.Dispose();

                        if (protoOutputStream != null)
                        {
                            packets.WriteTo(protoOutputStream);
                            protoOutputStream.Close();
                        }

                        _stats.SetEndTime(DateTime.Now);
                    }

                    if (Settings.DumpFormatWithTextToFile())
                    {
                        if (written)
                            Trace.WriteLine($"{_logPrefix}: Saved file to '{outFileName}'");
                        else
                        {
                            Trace.WriteLine($"{_logPrefix}: No file produced");
                            File.Delete(outFileName);
                        }
                    }

                    Trace.WriteLine($"{_logPrefix}: {_stats}");

                    if (Settings.SQLOutputFlag != 0 || HotfixSettings.Instance.ShouldLog())
                        WriteSQLs(packets);

                    if (Settings.LogPacketErrors)
                        WritePacketErrors();

                    GC.Collect(); // Force a GC collect after parsing a file. It seems to help.

                    return packets;
                }
                case DumpFormatType.Pkt:
                {
                    var packets = ReadPackets();
                    if (packets.Count == 0)
                        break;

                    if (Settings.FilterPacketsNum < 0)
                    {
                        var packetsPerSplit = Math.Abs(Settings.FilterPacketsNum);
                        var totalPackets = packets.Count;

                        var numberOfSplits = (int)Math.Ceiling((double)totalPackets / packetsPerSplit);

                        for (var i = 0; i < numberOfSplits; ++i)
                        {
                            var fileNamePart = FileName + "_part_" + (i + 1) + ".pkt";

                            var packetsPart = packets.Take(packetsPerSplit).ToList();
                            packets.RemoveRange(0, packetsPart.Count);

                            BinaryDump(fileNamePart, packetsPart);
                        }
                    }
                    else
                    {
                        var fileNameExcerpt = Path.ChangeExtension(FileName, null) + "_excerpt.pkt";
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
                    if (_compression != FileCompression.None)
                    {
                        Trace.WriteLine($"Skipped compressing file {FileName}");
                        break;
                    }

                    Compress();
                    break;
                }
                case DumpFormatType.SniffVersionSplit:
                {
                    var reader = _compression != FileCompression.None ? new Reader(_tempName, _sniffType) : new Reader(FileName, _sniffType);

                    if (ClientVersion.IsUndefined() && reader.PacketReader.CanRead())
                    {
                        Packet packet;
                        reader.TryRead(out packet);
                        packet.ClosePacket();
                    }

                    reader.PacketReader.Dispose();

                    var version = ClientVersion.IsUndefined() ? "unknown" : ClientVersion.VersionString;

                    var realFileName = GetCompressedFileName();

                    var destPath = Path.Combine(Path.GetDirectoryName(realFileName), version,
                        Path.GetFileName(realFileName));

                    var destDir = Path.GetDirectoryName(destPath);
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

                    using (var writer = new StreamWriter(Path.ChangeExtension(FileName, null) + "_connidx.txt"))
                    {
                        if (ClientVersion.Build <= ClientVersionBuild.V6_0_3_19342)
                            writer.WriteLine("# Warning: versions before 6.1 might not have proper ConnectionIndex values.");

                        IEnumerable<IGrouping<Tuple<int, Direction>, Packet>> groupsOpcode = packets
                            .GroupBy(packet => Tuple.Create(packet.Opcode, packet.Direction))
                            .OrderBy(grouping => grouping.Key.Item2);

                        foreach (var groupOpcode in groupsOpcode)
                        {
                            var groups = groupOpcode
                                .GroupBy(packet => packet.ConnectionIndex)
                                .OrderBy(grouping => grouping.Key)
                                .ToList();

                            writer.Write("{0} {1,-50}: ", groupOpcode.Key.Item2, Opcodes.GetOpcodeName(groupOpcode.Key.Item1, groupOpcode.Key.Item2));

                            for (var i = 0; i < groups.Count; i++)
                            {
                                var idx = groups[i].Key;
                                writer.Write("{0} ({1}{2})", idx, (idx & 1) != 0 ? "INSTANCE" : "REALM", (idx & 2) != 0 ? "_NEW" : "");

                                if (i != groups.Count - 1)
                                    writer.Write(", ");
                            }

                            writer.WriteLine();
                        }
                    }

                    break;
                }
                case DumpFormatType.Fusion:
                {
                    var packets = ReadPackets();
                    if (packets.Count == 0)
                        break;

                    FusionDump(packets);
                    break;
                }
                default:
                {
                    Trace.WriteLine($"{_logPrefix}: Dump format is none, nothing will be processed.");
                    break;
                }
            }

            return null;
        }

        public static string GetHeader(string fileName)
        {
            return "# TrinityCore - WowPacketParser" + Environment.NewLine +
                   "# File name: " + Path.GetFileName(fileName) + Environment.NewLine +
                   "# Detected build: " + ClientVersion.Build + Environment.NewLine +
                   "# Detected locale: " + ClientLocale.ClientLocaleString + Environment.NewLine +
                   "# Targeted database: " + Settings.TargetedDatabase + Environment.NewLine +
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

            var fileName = FileName;
            if (_compression != FileCompression.None)
                fileName = _tempName;

            Reader.Read(fileName, _sniffType, p =>
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

        private void FusionDump(ICollection<Packet> packets)
        {
            Trace.WriteLine($"{_logPrefix}: Merge {packets.Count} packets to a file...");
            FusionBinaryPacketWriter.Write(packets);
        }

        private void SplitBinaryDump(ICollection<Packet> packets)
        {
            Trace.WriteLine($"{_logPrefix}: Splitting {packets.Count} packets to multiple files...");
            SplitBinaryPacketWriter.Write(packets);
        }

        private void DirectionSplitBinaryDump(ICollection<Packet> packets)
        {
            Trace.WriteLine($"{_logPrefix}: Splitting {packets.Count} packets to multiple files...");
            SplitDirectionBinaryPacketWriter.Write(packets);
        }

        private void SessionSplitBinaryDump(ICollection<Packet> packets)
        {
            Trace.WriteLine($"{_logPrefix}: Splitting {packets.Count} packets to multiple files...");
            SplitSessionBinaryPacketWriter.Write(packets);
        }

        private void BinaryDump(string fileName, ICollection<Packet> packets)
        {
            Trace.WriteLine($"{_logPrefix}: Copying {packets.Count} packets to .pkt format...");
            BinaryPacketWriter.Write(fileName, FileMode.Create, packets);
        }

        private void WriteSQLs(Packets packets)
        {
            var sqlFileName = string.IsNullOrWhiteSpace(Settings.SQLFileName) ? $"{Utilities.FormattedDateTimeForFiles()}_{Path.GetFileName(FileName)}.sql" : Settings.SQLFileName;

            if (!string.IsNullOrWhiteSpace(Settings.SQLFileName))
                return;

            Builder.DumpSQL(new []{packets}, $"{_logPrefix}: Dumping sql", sqlFileName, GetHeader(FileName));
            Storage.ClearContainers();
        }

        private void WritePacketErrors()
        {
            if (_withErrorHeaders.Count == 0 && _skippedHeaders.Count == 0 && _noStructureHeaders.Count == 0)
                return;

            var fileName = Path.GetFileNameWithoutExtension(FileName) + "_errors.txt";

            using (var file = new StreamWriter(fileName))
            {
                file.WriteLine(GetHeader(FileName));

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

        private void Compress()
        {
            var fileToCompress = new FileInfo(FileName);
            _compression = FileCompression.GZip;

            using (var originalFileStream = fileToCompress.OpenRead())
            {
                using (var compressedFileStream = File.Create(GetCompressedFileName()))
                {
                    using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress, true))
                    {
                        originalFileStream.CopyTo(compressionStream);
                    }

                    Trace.WriteLine($"{_logPrefix} Compressed {fileToCompress.Name} from {fileToCompress.Length} to {compressedFileStream.Length} bytes.");
                }
            }
        }

        public string Decompress()
        {
            var fileToDecompress = new FileInfo(GetCompressedFileName());

            using (var originalFileStream = fileToDecompress.OpenRead())
            {
                var newFileName = Path.GetTempFileName();

                using (var decompressedFileStream = File.Create(newFileName))
                {
                    switch (_compression)
                    {
                        case FileCompression.GZip:
                            using (var decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                            {
                                decompressionStream.CopyTo(decompressedFileStream);
                            }
                            break;
                        default:
                            throw new NotImplementedException($"Invalid decompression method for {fileToDecompress.Name}");
                    }
                }

                Trace.WriteLine($"{_logPrefix} Decompressed {fileToDecompress.Name} to {Path.GetFileName(newFileName)}");
                return newFileName;
            }
        }

        private string GetCompressedFileName()
        {
            return FileName + _compression.GetExtension();
        }
    }
}
