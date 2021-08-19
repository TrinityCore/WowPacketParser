using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_2_0_30898
{
    public class ItemEnchantment : IItemEnchantment
    {
        public int ID { get; set; }
        public uint Duration { get; set; }
        public short Charges { get; set; }
        public ushort Inactive { get; set; }
    }
}

