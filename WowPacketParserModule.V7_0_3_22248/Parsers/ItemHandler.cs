using WowPacketParser.Misc;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class ItemHandler
    {
        public static void ReadItemGemInstanceData(Packet packet, params object[] idx)
        {
            packet.ReadByte("Slot", idx);
            V6_0_2_19033.Parsers.ItemHandler.ReadItemInstance(packet, "Item", idx);
        }
    }
}
