using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMapObjEvents
    {
        public uint UniqueID;
        public Data Events;
    }
}
