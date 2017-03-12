using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetBankBagSlotAutosortDisabled
    {
        public bool Disable;
        public uint BagIndex;
    }
}
