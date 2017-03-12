using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ItemBonusInstanceData
    {
        public ItemContext Context;
        public ItemBonuses Bonuses;
    }
}
