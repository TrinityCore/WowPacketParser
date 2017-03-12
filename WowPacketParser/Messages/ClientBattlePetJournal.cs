using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlePetJournal
    {
        public bool HasJournalLock;
        public List<ClientBattlePet> Pets;
        public short TrapLevel;
        public List<ClientPetBattleSlot> Slots;
    }
}
