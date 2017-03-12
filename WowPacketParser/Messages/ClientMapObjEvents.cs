using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMapObjEvents
    {
        public uint UniqueID;
        public Data Events;
    }
}
