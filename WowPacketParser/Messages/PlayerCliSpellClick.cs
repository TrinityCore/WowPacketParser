using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSpellClick
    {
        public ulong SpellClickUnitGUID;
        public bool TryAutoDismount;
    }
}
