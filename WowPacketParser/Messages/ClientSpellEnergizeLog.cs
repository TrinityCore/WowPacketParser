using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellEnergizeLog
    {
        public int SpellID;
        public ulong TargetGUID;
        public ulong CasterGUID;
        public SpellCastLogData LogData; // Optional
        public int Amount;
        public int Type;
    }
}
