using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct AccountObjectSetCheats
    {
        public bool AutoBattle;
        public AccountobjectSetCheats Type;
        public sbyte SlotLockCheat;
    }
}
