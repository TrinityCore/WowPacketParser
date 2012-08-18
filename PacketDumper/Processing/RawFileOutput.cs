using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using PacketDumper.Processing.RawData;
using PacketParser.Processing;
using PacketDumper.Misc;
using PacketParser.DataStructures;

namespace PacketDumper.Processing
{
    public class RawFileOutput : IPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return ProcessPacket; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return null; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        BinaryWriter writer = null;
        IBinaryPacketWriter packetWriter = null;
        string _logPrefix;
        public bool Init(PacketFileProcessor file)
        {
            _logPrefix = file.LogPrefix;
            var fileName = file.LogPrefix;
            if (Settings.RawOutputType == "" || Settings.SplitRawOutput)
                return false;
            try
            {
                Trace.WriteLine(string.Format("{0}: Copying packets to raw format({1})...", _logPrefix, Settings.RawOutputType));

                packetWriter = BinaryPacketWriter.Get();
                var dumpFileName = Path.ChangeExtension(fileName, null) + "_rawdump." + Settings.RawOutputType;

                var fileStream = new FileStream(dumpFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                writer = new BinaryWriter(fileStream, Encoding.ASCII);

                packetWriter.WriteHeader(writer);

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
            if (packet.SubPacket)
                return;
            packetWriter.WritePacket(packet, writer);
        }
        public void Finish()
        {
            if (writer != null)
                writer.Close();
        }
    }
}
