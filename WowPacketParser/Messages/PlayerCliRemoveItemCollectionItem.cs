using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliRemoveItemCollectionItem
    {
        public int ItemCollectionType;
        public int ItemID;
    }
}
