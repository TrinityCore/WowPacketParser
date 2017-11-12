using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.Guild
{
    public unsafe struct SetRankPermissions
    {
        public uint OldFlags;
        public int RankID;
        public int RankOrder;
        public string RankName;
        public uint Flags;
        public uint WithdrawGoldLimit;
        public fixed uint TabFlags[8];
        public fixed uint TabWithdrawItemLimit[8];

        [Parser(Opcode.CMSG_GUILD_SET_RANK_PERMISSIONS, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleGuildRank(Packet packet)
        {
            packet.ReadUInt32("Rank Id");
            packet.ReadUInt32E<GuildRankRightsFlag>("Rights");
            packet.ReadCString("Name");
            packet.ReadInt32("Money Per Day");
            for (var i = 0; i < 6; i++)
            {
                packet.ReadUInt32E<GuildBankRightsFlag>("Tab Rights", i);
                packet.ReadInt32("Tab Slots", i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_SET_RANK_PERMISSIONS, ClientVersionBuild.V4_0_6_13596, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildRank406(Packet packet)
        {
            for (var i = 0; i < 8; ++i)
                packet.ReadUInt32("Bank Slots", i);

            packet.ReadUInt32E<GuildRankRightsFlag>("Rights");

            packet.ReadUInt32("New Rank Id");
            packet.ReadUInt32("Old Rank Id");

            for (var i = 0; i < 8; ++i)
                packet.ReadUInt32E<GuildBankRightsFlag>("Tab Rights", i);

            packet.ReadGuid("Guild GUID");
            packet.ReadUInt32E<GuildRankRightsFlag>("Old Rights");

            packet.ReadInt32("Money Per Day");
            packet.ReadGuid("Player GUID");
            packet.ReadCString("Rank Name");
        }

        [Parser(Opcode.CMSG_GUILD_SET_RANK_PERMISSIONS, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleGuildRank434(Packet packet)
        {
            packet.ReadUInt32("Old Rank Id");
            packet.ReadUInt32E<GuildRankRightsFlag>("Old Rights");
            packet.ReadUInt32E<GuildRankRightsFlag>("New Rights");

            for (var i = 0; i < 8; ++i)
            {
                packet.ReadUInt32("Tab Slot", i);
                packet.ReadUInt32E<GuildBankRightsFlag>("Tab Rights", i);
            }

            packet.ReadUInt32("Money Per Day");
            packet.ReadUInt32("New Rank Id");
            var length = packet.ReadBits(7);
            packet.ReadWoWString("Rank Name", length);
        }

        [Parser(Opcode.CMSG_GUILD_SET_RANK_PERMISSIONS, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGuildRank547(Packet packet)
        {
            packet.ReadUInt32("Old Rank Id");

            for (var i = 0; i < 8; ++i)
            {
                packet.ReadUInt32E<GuildBankRightsFlag>("Tab Rights", i);
                packet.ReadUInt32("Tab Slot", i);
            }

            packet.ReadUInt32("Money Per Day");
            packet.ReadUInt32E<GuildRankRightsFlag>("New Rights");
            packet.ReadUInt32("New Rank Id");
            packet.ReadUInt32E<GuildRankRightsFlag>("Old Rights");

            var length = packet.ReadBits(7);
            packet.ReadWoWString("Rank Name", length);
        }

        [Parser(Opcode.CMSG_GUILD_SET_RANK_PERMISSIONS, ClientVersionBuild.V6_0_2_19033)]
        public static void HandlelGuildSetRankPermissions(Packet packet)
        {
            packet.ReadInt32("RankID");
            packet.ReadInt32("RankOrder");
            packet.ReadUInt32E<GuildRankRightsFlag>("Flags");
            packet.ReadUInt32E<GuildRankRightsFlag>("OldFlags");
            packet.ReadInt32("WithdrawGoldLimit");

            for (var i = 0; i < 8; ++i)
            {
                packet.ReadInt32E<GuildBankRightsFlag>("TabFlags", i);
                packet.ReadInt32("TabWithdrawItemLimit", i);
            }

            packet.ResetBitReader();
            var rankNameLen = packet.ReadBits(7);

            packet.ReadWoWString("RankName", rankNameLen);
        }
    }
}
