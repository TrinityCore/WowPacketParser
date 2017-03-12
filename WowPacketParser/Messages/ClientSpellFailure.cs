using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellFailure
    {
        public ulong CasterUnit;
        public int SpellID;
        public byte Reason;
        public byte CastID;
    }
}
