using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
namespace WowPacketParser.Processing
{
    public interface IPacketProcessor
    {
        bool Init(SniffFile file);
        void ProcessPacket(Packet packet);
        void ProcessedPacket(Packet packet);
        void Finish();
        void ProcessData(string name, int? index, Object obj, Type t);
    }
}
