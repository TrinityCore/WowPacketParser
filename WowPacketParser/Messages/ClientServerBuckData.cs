using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientServerBuckData
    {
        public byte ClusterID;
        public int CaptureTime;
        public uint RequestID;
        public List<ServerBuckDataList> Data;
    }
}
