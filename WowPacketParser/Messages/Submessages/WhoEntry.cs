using WowPacketParser.Messages.Player;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct WhoEntry
    {
        public PlayerGuidLookupData PlayerData;
        public ulong GuildGUID;
        public uint GuildVirtualRealmAddress;
        public string GuildName;
        public int AreaID;
        public bool IsGM;
    }
}
