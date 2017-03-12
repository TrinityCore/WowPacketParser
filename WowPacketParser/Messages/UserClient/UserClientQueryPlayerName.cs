using WowPacketParser.Messages.Player;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientQueryPlayerName
    {
        public ulong Player;
        public PlayerGuidLookupHint Hint;
    }
}
