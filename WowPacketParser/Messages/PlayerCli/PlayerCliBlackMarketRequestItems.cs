using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliBlackMarketRequestItems
    {
        public ulong NpcGUID;
        public UnixTime LastUpdateID;
    }
}
