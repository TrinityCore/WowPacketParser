using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
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
