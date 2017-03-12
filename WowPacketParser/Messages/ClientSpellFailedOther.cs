using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellFailedOther
    {
        public ulong CasterUnit;
        public int SpellID;
        public byte Reason;
        public byte CastID;
    }
}
