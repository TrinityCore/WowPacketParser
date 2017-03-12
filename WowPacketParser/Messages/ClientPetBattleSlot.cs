using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPetBattleSlot
    {
        public ulong BattlePetGUID;
        public int CollarID;
        public byte SlotIndex;
        public bool Locked;
    }
}
