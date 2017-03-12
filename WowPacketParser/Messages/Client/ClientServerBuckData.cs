using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientServerBuckData
    {
        public byte ClusterID;
        public int CaptureTime;
        public uint RequestID;
        public List<ServerBuckDataList> Data;
    }
}
