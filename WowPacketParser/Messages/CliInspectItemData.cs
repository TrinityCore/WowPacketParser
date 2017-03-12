using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliInspectItemData
    {
        public ulong CreatorGUID;
        public ItemInstance Item;
        public byte Index;
        public bool Usable;
        public List<CliInspectEnchantData> Enchants;
    }
}
