using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_1_36216
{
    public class ItemEnchantment : IItemEnchantment
    {
        public int ID { get; set; }
        public uint Duration { get; set; }
        public short Charges { get; set; }
        public ushort Inactive { get; set; }
    }
}

