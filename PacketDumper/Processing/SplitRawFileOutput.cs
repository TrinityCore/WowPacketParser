using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using PacketParser.Enums.Version;
using PacketParser.Processing;
using PacketDumper.Processing.RawData;
using PacketDumper.Misc;
using PacketParser.DataStructures;

namespace PacketDumper.Processing
{
    public class SplitRawFileOutput : IPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return ProcessPacket; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return null; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        IBinaryPacketWriter packetWriter = null;
        private const string Folder = "split"; // might want to move to config later

        public bool Init(PacketFileProcessor file)
        {
            if (Settings.RawOutputType == "" || !Settings.SplitRawOutput)
                return false;

            Trace.WriteLine(string.Format("{0}: Splitting packets to multiple raw format({2}) files...", file.LogPrefix, Settings.RawOutputType));
            try
            {
                Directory.CreateDirectory(Folder); // not doing anything if it exists already
                packetWriter = BinaryPacketWriter.Get();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.GetType());
                Trace.WriteLine(ex.Message);
                Trace.WriteLine(ex.StackTrace);
                return false;
            }
            return true;
        }
        Dictionary<int, BinaryWriter> files = new Dictionary<int, BinaryWriter>();

        public void ProcessPacket(Packet packet)
        {
            if (packet.SubPacket)
                return;
            BinaryWriter a;
            if (files.TryGetValue(packet.Opcode, out a))
            {
                packetWriter.WritePacket(packet, a);
            }
            else
            {
                var fileName = Folder + "/" + Opcodes.GetOpcodeName(packet.Opcode) + "." + Settings.RawOutputType.ToString().ToLower();
                bool existed = !File.Exists(fileName);
                var fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write);
                var writer = new BinaryWriter(fileStream, Encoding.ASCII);
                if (!existed)
                    packetWriter.WriteHeader(writer);
                packetWriter.WritePacket(packet, writer);
                files.Add(packet.Opcode, writer);

            }
        }

        public void Finish()
        {
            foreach (var pair in files)
            {
                pair.Value.Close();
            }
        }
    }
}
