
namespace WowPacketParser.Store.Objects
{
    public record ItemEnchantData
    {
        public int ID;
        public uint Expiration;
        public uint Charges;
        public byte Slot;
    }
}
