using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientGMCreateItemTarget
    {
        public ItemContext CreationContext;
        public int ItemID;
        public string Target;
        public ulong Guid;
    }
}
