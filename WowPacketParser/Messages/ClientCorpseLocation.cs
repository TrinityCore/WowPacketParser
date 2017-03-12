using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCorpseLocation
    {
        public ulong Transport;
        public Vector3 Position;
        public int ActualMapID;
        public bool Valid;
        public int MapID;
    }
}
