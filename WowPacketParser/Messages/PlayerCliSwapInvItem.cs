using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSwapInvItem
    {
        public InvUpdate Inv;
        public byte Slot2;
        public byte Slot1;
    }
}
