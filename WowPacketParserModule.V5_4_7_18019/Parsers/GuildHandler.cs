using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.CMSG_GUILD_ACCEPT)]
        [Parser(Opcode.CMSG_GUILD_DECLINE)]
        [Parser(Opcode.CMSG_GUILD_DISBAND)]
        [Parser(Opcode.CMSG_GUILD_LEAVE)]
        [Parser(Opcode.CMSG_GUILD_PERMISSIONS)]
        [Parser(Opcode.CMSG_GUILD_QUERY_NEWS)]
        [Parser(Opcode.CMSG_GUILD_ROSTER)]
        public static void HandleGuildAccept(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_GUILD_ADD_RANK)]
        public static void HandleGuildAddRank(Packet packet)
        {
            packet.ReadInt32("Rank");
            var len = packet.ReadBits("Len", 7);
            packet.ReadWoWString("Name", len);
        }

        [Parser(Opcode.CMSG_GUILD_BANK_BUY_TAB)]
        public static void HandleGuildBankBuyTab(Packet packet)
        {
            packet.ReadByte("TabID");

            var guid = packet.StartBitStream(7, 6, 1, 2, 5, 3, 0, 4);
            packet.ParseBitStream(guid, 0, 7, 3, 4, 1, 6, 5, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GUILD_BANKER_ACTIVATE)]
        public static void HandleGuildBankerActivate(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadByte("Full Slot List");
        }

        [Parser(Opcode.CMSG_GUILD_DEL_RANK)]
        public static void HandleGuildDelRank(Packet packet)
        {
            packet.ReadInt32("Rank");
        }

        [Parser(Opcode.CMSG_GUILD_DEMOTE)]
        public static void HandleGuildDemote(Packet packet)
        {
            var guid = packet.StartBitStream(1, 0, 7, 5, 3, 2, 4, 6);
            packet.ParseBitStream(guid, 3, 4, 1, 0, 7, 2, 5, 6);

            packet.WriteGuid("Guid Target", guid);
        }

        [Parser(Opcode.CMSG_GUILD_INVITE)]
        public static void HandleGuildInvite(Packet packet)
        {
            var len = packet.ReadBits("Len", 9);
            packet.ReadWoWString("Name", len);
        }

        [Parser(Opcode.CMSG_GUILD_MOTD)]
        public static void HandleGuildMotd(Packet packet)
        {
            var len = packet.ReadBits("Len", 10);
            packet.ReadWoWString("Motd", len);
        }

        [Parser(Opcode.CMSG_GUILD_NEWS_UPDATE_STICKY)]
        public static void HandleGuildNewsUpdateSticky(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("NewsID");

            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            packet.ReadBit("Sticky");
            guid[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            packet.ParseBitStream(guid, 3, 4, 7, 2, 5, 1, 6, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GUILD_PROMOTE)]
        public static void HandleGuildPromote(Packet packet)
        {
            var guid = packet.StartBitStream(4, 0, 3, 5, 7, 1, 2, 6);
            packet.ParseBitStream(guid, 7, 0, 5, 2, 3, 6, 4, 1);

            packet.WriteGuid("Guid Target", guid);
        }

        [Parser(Opcode.CMSG_GUILD_QUERY)]
        public static void HandleGuildQuery(Packet packet)
        {
            var guildGuid = new byte[8];
            var playerGuid = new byte[8];

            guildGuid[0] = packet.ReadBit();
            guildGuid[4] = packet.ReadBit();
            playerGuid[0] = packet.ReadBit();
            guildGuid[1] = packet.ReadBit();
            guildGuid[7] = packet.ReadBit();
            guildGuid[6] = packet.ReadBit();
            playerGuid[6] = packet.ReadBit();
            playerGuid[2] = packet.ReadBit();
            guildGuid[5] = packet.ReadBit();
            playerGuid[5] = packet.ReadBit();
            playerGuid[7] = packet.ReadBit();
            playerGuid[1] = packet.ReadBit();
            playerGuid[3] = packet.ReadBit();
            playerGuid[4] = packet.ReadBit();
            guildGuid[2] = packet.ReadBit();
            guildGuid[3] = packet.ReadBit();

            packet.ReadXORByte(guildGuid, 7);
            packet.ReadXORByte(playerGuid, 4);
            packet.ReadXORByte(playerGuid, 0);
            packet.ReadXORByte(playerGuid, 3);
            packet.ReadXORByte(playerGuid, 6);
            packet.ReadXORByte(playerGuid, 7);
            packet.ReadXORByte(guildGuid, 2);
            packet.ReadXORByte(guildGuid, 5);
            packet.ReadXORByte(guildGuid, 0);
            packet.ReadXORByte(guildGuid, 3);
            packet.ReadXORByte(playerGuid, 2);
            packet.ReadXORByte(playerGuid, 5);
            packet.ReadXORByte(guildGuid, 1);
            packet.ReadXORByte(playerGuid, 1);
            packet.ReadXORByte(guildGuid, 6);
            packet.ReadXORByte(guildGuid, 4);

            packet.WriteGuid("GuildGuid", guildGuid);
            packet.WriteGuid("PlayerGuid", playerGuid);
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_RANKS)]
        public static void HandleGuildQueryRanks(Packet packet)
        {
            var guid = packet.StartBitStream(2, 7, 0, 5, 6, 3, 4, 1);
            packet.ParseBitStream(guid, 6, 0, 1, 7, 2, 3, 4, 5);

            packet.WriteGuid("Guid Target", guid);
        }

        [Parser(Opcode.CMSG_GUILD_REMOVE)]
        public static void HandleGuildRemove(Packet packet)
        {
            var guid = packet.StartBitStream(3, 1, 6, 0, 7, 2, 5, 4);
            packet.ParseBitStream(guid, 2, 6, 0, 1, 4, 3, 5, 7);

            packet.WriteGuid("Guid Target", guid);
        }

        [Parser(Opcode.CMSG_GUILD_SET_GUILD_MASTER)]
        public static void HandleGuildSetGuildMaster(Packet packet)
        {
            var len = packet.ReadBits("Len", 9);
            packet.ReadWoWString("Name", len);
        }

        [Parser(Opcode.CMSG_GUILD_SET_RANK_PERMISSIONS)]
        public static void HandleGuildSetRankPerm(Packet packet)
        {
            packet.ReadInt32("OldRankID");
            for (var tabId = 0; tabId < 8; ++tabId)
            {
                packet.ReadInt32("BankRights", tabId);
                packet.ReadInt32("Slots", tabId);
            }
            packet.ReadInt32("MoneyPerDay");
            packet.ReadInt32("NewRights");
            packet.ReadInt32("NewRankId");
            packet.ReadInt32("OldRights");

            var len = packet.ReadBits("Len", 7);
            packet.ReadWoWString("Name", len);
        }

        [Parser(Opcode.CMSG_QUERY_GUILD_REWARDS)]
        public static void HandleQueryGuildRewards(Packet packet)
        {
            packet.ReadInt32("Unk");
        }

        [Parser(Opcode.CMSG_QUERY_GUILD_XP)]
        public static void HandleQueryGuildXp(Packet packet)
        {
            var guid = packet.StartBitStream(3, 2, 7, 5, 6, 0, 1, 4);
            packet.ParseBitStream(guid, 3, 7, 2, 1, 5, 4, 0, 6);

            packet.WriteGuid("Guid", guid);
        }
    }
}
