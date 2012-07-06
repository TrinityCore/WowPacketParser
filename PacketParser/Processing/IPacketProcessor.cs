using System;
using PacketParser.DataStructures;

namespace PacketParser.Processing
{
    public interface IPacketProcessor
    {
        bool Init(PacketFileProcessor file);
        void ProcessPacket(Packet packet);
        void ProcessedPacket(Packet packet);
        void Finish();
        void ProcessData(string name, int? index, Object obj, Type t);
    }
}
