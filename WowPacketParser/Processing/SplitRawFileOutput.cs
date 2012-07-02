using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Guid = WowPacketParser.Misc.Guid;
using WowPacketParser.Misc;
using WowPacketParser.Loading;
using System.Diagnostics;
using WowPacketParser.Enums.Version;

namespace WowPacketParser.Processing
{
    public class SplitRawFileOutput : IPacketProcessor
    {
        IBinaryPacketWriter packetWriter = null;
        private const string Folder = "split"; // might want to move to config later

        public bool Init(SniffFile file)
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
        public void ProcessedPacket(Packet packet) { }
        public void ProcessData(string name, int? index, Object obj, Type t) { }
    }
}
