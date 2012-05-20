using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Guid = WowPacketParser.Misc.Guid;
using WowPacketParser.Misc;
using WowPacketParser.Loading;
using System.Diagnostics;
namespace WowPacketParser.Processing
{
    public class SplitRawFileOutput : IPacketProcessor
    {
        IBinaryPacketWriter packetWriter = null;
        public bool Init(SniffFile file)
        {
            if (Settings.RawOutputType == "" || !Settings.SplitRawOutput)
                return false;

            Trace.WriteLine(string.Format("{0}: Splitting packets to multiple raw format({2}) files...", file.LogPrefix, Settings.RawOutputType));
            try
            {
                Directory.CreateDirectory(SplitRawOutput.Folder); // not doing anything if it exists already
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
        public void ProcessPacket(Packet packet)
        {
            var fileName = SplitRawOutput.Folder + "/" + Enums.Version.Opcodes.GetOpcodeName(packet.Opcode) + "." + Settings.RawOutputType.ToString().ToLower();
            try
            {
                using (SplitRawOutput.Locks.Lock(fileName))
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
                Trace.WriteLine(string.Format("Timeout trying to write Opcode to {0} ignoring packet", fileName));
            }
        }
        public void Finish() {}
        public void ProcessData(string name, Object obj, Type t) {}
    }
}
