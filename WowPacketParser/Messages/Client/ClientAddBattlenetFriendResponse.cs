using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAddBattlenetFriendResponse // FIXME: No handlers
    {
        public ushort? BattlenetError; // Optional
        public ulong ClientToken;
        public addbattlenetfrienderror Result;
    }
}
