using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliChangeBagSlotFlag
    {
        public bool On;
        public uint FlagToChange;
        public uint BagIndex;
    }
}
