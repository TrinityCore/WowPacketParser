using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliAutoEquipItem
    {
        public byte Slot;
        public InvUpdate Inv;
        public byte PackSlot;
    }
}
