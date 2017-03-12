using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellGo
    {
        public SpellCastLogData? LogData; // Optional
        public SpellCastData Cast;
    }
}
