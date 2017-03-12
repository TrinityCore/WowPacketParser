using WowPacketParser.Messages.Player;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientQueryPlayerNameResponse
    {
        public ulong Player;
        public byte Result;
        public PlayerGuidLookupData Data;
    }
}
