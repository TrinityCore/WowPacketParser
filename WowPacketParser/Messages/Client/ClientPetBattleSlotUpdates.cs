using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPetBattleSlotUpdates
    {
        public bool NewSlotUnlocked;
        public bool AutoSlotted;
        public List<ClientPetBattleSlot> Slots;
    }
}
