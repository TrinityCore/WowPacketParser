
namespace WowPacketParser.Store.Objects
{
    public record ItemEnchantData
    {
        public int ID;
        public uint Expiration;
        public int Charges;
        public byte Slot;
    }
}
