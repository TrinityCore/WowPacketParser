using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientUpdateCharacterFlags
    {
        public ulong Character;
        public uint? Flags3; // Optional
        public uint? Flags; // Optional
        public uint? Flags2; // Optional
    }
}
