using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.CMSG_GUILD_QUERY)]
        public static void HandleGuildQuery(Packet packet)
        {
            packet.ReadPackedGuid128("Guild Guid");
            packet.ReadPackedGuid128("Player Guid");
        }

        [Parser(Opcode.SMSG_GUILD_QUERY_RESPONSE)]
        public static void HandleGuildQueryResponse(Packet packet)
        {
            packet.ReadPackedGuid128("Guild Guid");

            var hasData = packet.ReadBit();
            if (hasData)
            {
                packet.ReadPackedGuid128("Guild Guid");
                packet.ReadInt32("VirtualRealmAddress");
                var rankCount = packet.ReadInt32("RankCount");
                packet.ReadInt32("EmblemStyle");
                packet.ReadInt32("EmblemColor");
                packet.ReadInt32("BorderStyle");
                packet.ReadInt32("BorderColor");
                packet.ReadInt32("BackgroundColor");

                for (var i = 0; i < rankCount; i++)
                {
                    packet.ReadInt32("RankID", i);
                    packet.ReadInt32("RankOrder", i);

                    packet.ResetBitReader();
                    var rankNameLen = packet.ReadBits(7);
                    packet.ReadWoWString("Rank Name", rankNameLen, i);
                }

                packet.ResetBitReader();
                var nameLen = packet.ReadBits(7);
                packet.ReadWoWString("Guild Name", nameLen);
            }
        }

        [Parser(Opcode.SMSG_GUILD_MOTD)]
        public static void HandleNewText(Packet packet)
        {
            packet.ReadWoWString("MotdText", (int)packet.ReadBits(10));
        }

        [Parser(Opcode.CMSG_GUILD_BANK_BUY_TAB)]
        public static void HandleGuildBankBuyTab(Packet packet)
        {
            packet.ReadPackedGuid128("Banker");
            packet.ReadByte("BankTab");
        }

        [Parser(Opcode.SMSG_GUILD_RANK)]
        public static void HandleGuildRankServer434(Packet packet)
        {
            var count = packet.ReadUInt32("Count");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("RankID", i);
                packet.ReadInt32("RankOrder", i);
                packet.ReadInt32("Flags", i);
                packet.ReadInt32("WithdrawGoldLimit", i);

                for (var j = 0; j < 8; ++j)
                {
                    packet.ReadEnum<GuildBankRightsFlag>("TabFlags", TypeCode.Int32, i, j);
                    packet.ReadInt32("TabWithdrawItemLimit", i, j);
                }

                packet.ResetBitReader();
                var bits8 = (int)packet.ReadBits(7);
                packet.ReadWoWString("RankName", bits8, i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER)]
        public static void HandleGuildRoster(Packet packet)
        {
            packet.ReadUInt32("NumAccounts");
            packet.ReadPackedTime("CreateDate");
            packet.ReadUInt32("MaxWeeklyRep");
            var int20 = packet.ReadUInt32("MemberDataCount");

            for (var i = 0; i < int20; ++i)
            {
                packet.ReadPackedGuid128("Guid", i);

                packet.ReadUInt32("RankID", i);
                packet.ReadUInt32("AreaID", i);
                packet.ReadUInt32("PersonalAchievementPoints", i);
                packet.ReadUInt32("GuildReputation", i);

                packet.ReadSingle("LastSave", i);

                for (var j = 0; j < 2; ++j)
                {
                    packet.ReadUInt32("DbID", i, j);
                    packet.ReadUInt32("Rank", i, j);
                    packet.ReadUInt32("Step", i, j);
                }

                packet.ReadUInt32("VirtualRealmAddress", i);

                packet.ReadEnum<GuildMemberFlag>("Status", TypeCode.Byte, i);
                packet.ReadByte("Level", i);
                packet.ReadEnum<Class>("ClassID", TypeCode.Byte, i);
                packet.ReadEnum<Gender>("Gender", TypeCode.Byte, i);

                packet.ResetBitReader();

                var bits36 = packet.ReadBits(6);
                var bits92 = packet.ReadBits(8);
                var bits221 = packet.ReadBits(8);

                packet.ReadBit("Authenticated", i);
                packet.ReadBit("SorEligible", i);

                packet.ReadWoWString("Name", bits36, i);
                packet.ReadWoWString("Note", bits92, i);
                packet.ReadWoWString("OfficerNote", bits221, i);
            }

            packet.ResetBitReader();
            var bits2037= packet.ReadBits(10);
            var bits9 = packet.ReadBits(11);

            packet.ReadWoWString("WelcomeText", bits2037);
            packet.ReadWoWString("InfoText", bits9);
        }

        [Parser(Opcode.SMSG_GUILD_EVENT_PRESENCE_CHANGE)]
        public static void HandleGuildEventPresenceChange(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");

            packet.ReadInt32("VirtualRealmAddress");

            var bits38 = packet.ReadBits(6);
            packet.ReadBit("LoggedOn");
            packet.ReadBit("Mobile");

            packet.ReadWoWString("Name", bits38);
        }

        [Parser(Opcode.SMSG_GUILD_RECIPES)]
        public static void HandleGuildAchievementData(Packet packet)
        {
            var count = packet.ReadInt32("Criteria count");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Skill Id", i);
                packet.ReadBytes("Skill Bits", 300, i);
            }
        }
    }
}
