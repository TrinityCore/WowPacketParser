using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBattlePetJournal
    {
        public bool HasJournalLock;
        public List<ClientBattlePet> Pets;
        public short TrapLevel;
        public List<ClientPetBattleSlot> Slots;
    }
}
