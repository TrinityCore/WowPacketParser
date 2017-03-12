using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientDisenchantCredit
    {
        public ulong Disenchanter;
        public ItemInstance Item;
    }
}
