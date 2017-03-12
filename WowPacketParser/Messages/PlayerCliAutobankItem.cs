using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliAutobankItem
    {
        public byte PackSlot;
        public InvUpdate Inv;
        public byte Slot;
    }
}
