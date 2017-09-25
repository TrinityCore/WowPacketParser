using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient.GM
{
    public unsafe struct CreateItemTarget
    {
        public ItemContext CreationContext;
        public int ItemID;
        public string Target;
        public ulong Guid;
    }
}
