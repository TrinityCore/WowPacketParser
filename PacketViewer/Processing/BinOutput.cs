using System;
using PacketParser.Processing;
using PacketParser.DataStructures;

namespace PacketViewer.Processing
{
    public class BinOutput : IPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return new Type[] { typeof(TextBuilder) }; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return null; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return ProcessedPacket; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        public bool Init(PacketFileProcessor proc)
        {
            return true;
        }

        public void Finish()
        {
        }

        public void ProcessedPacket(Packet packet)
        {
        }
    }
}