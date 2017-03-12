using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientUpdateObject
    {
        public uint NumObjUpdates;
        public ushort MapID;
        public Data Data;
    }
}
