using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientBattlePetSetCollar
    {
        public int CollarItemID;
        public byte SlotIndex;
    }
}
