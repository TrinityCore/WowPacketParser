using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
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
