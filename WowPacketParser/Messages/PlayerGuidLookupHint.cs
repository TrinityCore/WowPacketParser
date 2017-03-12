using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerGuidLookupHint
    {
        public uint VirtualRealmAddress; // Optional
        public uint NativeRealmAddress; // Optional
    }
}
