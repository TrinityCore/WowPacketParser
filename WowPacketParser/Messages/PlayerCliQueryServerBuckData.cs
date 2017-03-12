using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliQueryServerBuckData
    {
        public bool AllClusters;
        public byte ClusterID;
        public uint RequestID;
        public byte Mpid;
    }
}
