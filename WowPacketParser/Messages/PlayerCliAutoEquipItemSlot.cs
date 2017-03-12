using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliAutoEquipItemSlot
    {
        public ulong Item;
        public byte ItemDstSlot;
        public InvUpdate Inv;
    }
}
