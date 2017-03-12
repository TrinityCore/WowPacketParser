using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Cli
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
