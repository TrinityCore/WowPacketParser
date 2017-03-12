using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellLogMissEntry
    {
        public ulong Victim;
        public byte MissReason;
        public ClientSpellLogMissDebug? Debug; // Optional
    }
}
