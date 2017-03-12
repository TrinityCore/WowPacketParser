using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPetBattleSlotUpdates
    {
        public bool NewSlotUnlocked;
        public bool AutoSlotted;
        public List<ClientPetBattleSlot> Slots;
    }
}
