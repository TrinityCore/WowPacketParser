using WowPacketParser.Misc;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientMinimapPing
    {
        public Vector2 Position;
        public byte PartyIndex;
    }
}
