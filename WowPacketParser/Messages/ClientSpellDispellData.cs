using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellDispellData
    {
        public int SpellID;
        public bool Harmful;
        public int? Rolled; // Optional
        public int? Needed; // Optional
    }
}
