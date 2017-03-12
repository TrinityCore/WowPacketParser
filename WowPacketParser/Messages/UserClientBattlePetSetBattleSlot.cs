using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientBattlePetSetBattleSlot
    {
        public ulong BattlePetGUID;
        public byte SlotIndex;
    }
}
