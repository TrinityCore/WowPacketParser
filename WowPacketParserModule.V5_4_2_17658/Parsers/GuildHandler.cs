using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.CMSG_QUERY_GUILD_INFO)]
        public static void HandleGuildQuery(Packet packet)
        {
            var playerGUID = new byte[8];
            var guildGUID = new byte[8];

            guildGUID[5] = packet.ReadBit();
            playerGUID[3] = packet.ReadBit();
            playerGUID[5] = packet.ReadBit();
            guildGUID[2] = packet.ReadBit();
            guildGUID[0] = packet.ReadBit();
            guildGUID[4] = packet.ReadBit();
            playerGUID[6] = packet.ReadBit();
            playerGUID[2] = packet.ReadBit();
            playerGUID[7] = packet.ReadBit();
            playerGUID[1] = packet.ReadBit();
            playerGUID[4] = packet.ReadBit();
            guildGUID[1] = packet.ReadBit();
            playerGUID[0] = packet.ReadBit();
            guildGUID[3] = packet.ReadBit();
            guildGUID[6] = packet.ReadBit();
            guildGUID[7] = packet.ReadBit();

            packet.ReadXORByte(playerGUID, 6);
            packet.ReadXORByte(playerGUID, 3);
            packet.ReadXORByte(guildGUID, 1);
            packet.ReadXORByte(playerGUID, 0);
            packet.ReadXORByte(playerGUID, 7);
            packet.ReadXORByte(guildGUID, 0);
            packet.ReadXORByte(guildGUID, 5);
            packet.ReadXORByte(guildGUID, 2);
            packet.ReadXORByte(playerGUID, 1);
            packet.ReadXORByte(guildGUID, 3);
            packet.ReadXORByte(playerGUID, 2);
            packet.ReadXORByte(guildGUID, 7);
            packet.ReadXORByte(playerGUID, 4);
            packet.ReadXORByte(playerGUID, 5);
            packet.ReadXORByte(guildGUID, 6);
            packet.ReadXORByte(guildGUID, 4);

            packet.WriteGuid("PlayerGUID", playerGUID);
            packet.WriteGuid("GuildGUID", guildGUID);
        }

        [Parser(Opcode.SMSG_QUERY_GUILD_INFO_RESPONSE)]
        public static void HandleGuildQueryResponse(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[2] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[1] = packet.ReadBit();

            int nameLen = 0;
            int rankCount = 0;
            int[] rankName = null;
            var hasData = packet.ReadBit();
            if (hasData)
            {
                guid1[5] = packet.ReadBit();
                guid1[4] = packet.ReadBit();
                guid1[1] = packet.ReadBit();
                guid1[3] = packet.ReadBit();
                rankCount = (int)packet.ReadBits(21);
                guid1[0] = packet.ReadBit();

                rankName = new int[rankCount];
                for (var i = 0; i < rankCount; ++i)
                    rankName[i] = (int)packet.ReadBits(7);

                guid1[2] = packet.ReadBit();
                guid1[7] = packet.ReadBit();
                guid1[6] = packet.ReadBit();
                nameLen = (int)packet.ReadBits(7);
            }

            guid2[3] = packet.ReadBit();
            guid2[7] = packet.ReadBit();

            if (hasData)
            {
                packet.ReadXORByte(guid1, 0);
                packet.ReadXORByte(guid1, 1);
                packet.ReadXORByte(guid1, 5);
                packet.ReadInt32("Emblem Border Color");
                for (var j = 0; j < rankCount; j++)
                {
                    packet.ReadInt32("Rights Order", j);
                    packet.ReadInt32("Creation Order", j);
                    packet.ReadWoWString("Rank Name", rankName[j], j);
                }

                packet.ReadWoWString("Guild Name", nameLen);
                packet.ReadInt32("Realm Id");
                packet.ReadInt32("Emblem Background Color");
                packet.ReadXORByte(guid1, 7);
                packet.ReadXORByte(guid1, 6);
                packet.ReadXORByte(guid1, 2);
                packet.ReadInt32("Emblem Style");
                packet.ReadXORByte(guid1, 3);
                packet.ReadXORByte(guid1, 4);
                packet.ReadInt32("Emblem Border Style");
                packet.ReadInt32("Emblem Color");
                packet.WriteGuid("GuildGUID1", guid1);
            }

            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 7);

            packet.WriteGuid("GuildGUID2", guid2);
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER)]
        public static void HandleGuildRoster(Packet packet)
        {

            var motdLength = packet.ReadBits(10);
            var infoLength = packet.ReadBits(11);
            var size = packet.ReadBits(17);

            var guid = new byte[size][];
            var nameLength = new uint[size];
            var officerLength = new uint[size];
            var publicLength = new uint[size];

            for (var i = 0; i < size; ++i)
            {
                guid[i] = new byte[8];

                guid[i][1] = packet.ReadBit();
                guid[i][5] = packet.ReadBit();
                officerLength[i] = packet.ReadBits(8);
                packet.ReadBit("Can SoR", i);
                guid[i][4] = packet.ReadBit();
                guid[i][6] = packet.ReadBit();
                guid[i][7] = packet.ReadBit();
                guid[i][3] = packet.ReadBit();
                guid[i][2] = packet.ReadBit();
                nameLength[i] = packet.ReadBits(6);
                guid[i][0] = packet.ReadBit();
                publicLength[i] = packet.ReadBits(8);
                packet.ReadBit("Has Authenticator", i);
            }

            for (var i = 0; i < size; ++i)
            {
                packet.ReadByteE<Gender>("Gender", i);
                packet.ReadByteE<Class>("Member Class", i);
                packet.ReadXORByte(guid[i], 6);
                packet.ReadByte("Member Level", i);
                packet.ReadXORByte(guid[i], 0);

                for (var j = 0; j < 2; ++j)
                {
                    var value = packet.ReadUInt32();
                    var rank = packet.ReadUInt32();
                    var id = packet.ReadUInt32();

                    packet.AddValue("Profession", string.Format("Id {0} - Value {1} - Rank {2}", id, value, rank), i, j);
                }

                var name = packet.ReadWoWString("Name", nameLength[i], i);
                packet.ReadInt32("RealmId", i);
                packet.ReadSingle("Last online", i);
                packet.ReadInt64("Unk 2", i);
                packet.ReadInt64("Week activity", i);
                packet.ReadInt32("Zone Id", i);
                packet.ReadXORByte(guid[i], 5);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadWoWString("Officer note", officerLength[i], i);
                packet.ReadInt32("Int218", i);
                packet.ReadXORByte(guid[i], 2);
                packet.ReadInt32("Member Rank", i);
                packet.ReadXORByte(guid[i], 1);
                packet.ReadByteE<GuildMemberFlag>("Member Flags", i);
                packet.ReadInt32("Guild Reputation", i);
                packet.ReadWoWString("Public note", publicLength[i], i);
                packet.ReadXORByte(guid[i], 3);
                packet.ReadInt32("Member Achievement Points", i);
                packet.WriteGuid("Guid", guid[i], i);
                StoreGetters.AddName(new WowGuid64(BitConverter.ToUInt64(guid[i], 0)), name);
            }

            packet.ReadPackedTime("Time");
            packet.ReadWoWString("MOTD", motdLength);
            packet.ReadWoWString("Guild Info", infoLength);
            packet.ReadInt32("Unk Uint32 4");
            packet.ReadInt32("Weekly Reputation Cap");
            packet.ReadInt32("Accounts In Guild");
        }

        [Parser(Opcode.SMSG_GUILD_RANKS)]
        public static void HandleGuildRankServer(Packet packet)
        {
            const int guildBankMaxTabs = 8;
            var count = packet.ReadBits("Count", 17);
            var length = new uint[count];

            for (var i = 0; i < count; ++i)
                length[i] = packet.ReadBits(7);

            for (var i = 0; i < count; ++i)
            {
                for (var j = 0; j < guildBankMaxTabs; ++j)
                {
                    packet.ReadInt32("Tab Slots", i, j);
                    packet.ReadInt32E<GuildBankRightsFlag>("Tab Rights", i, j);
                }

                packet.ReadInt32("Gold Per Day", i);
                packet.ReadInt32("Unk 1", i);
                packet.ReadInt32("Rights Order", i);
                packet.ReadWoWString("Name", length[i], i);
                packet.ReadInt32("Creation Order", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_NEWS_TEXT)]
        public static void HandleNewText(Packet packet)
        {
            packet.ReadWoWString("Text", (int)packet.ReadBits(10));
        }

        [Parser(Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS)]
        public static void HandleGuildAchievementData(Packet packet)
        {
            var count = packet.ReadBits("Achievement count", 20);

            var guid = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                guid[i] = new byte[8];
                packet.StartBitStream(guid[i], 7, 5, 3, 4, 0, 6, 2, 1);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32<AchievementId>("Achievement Id", i);

                packet.ReadXORByte(guid[i], 6);
                packet.ReadXORByte(guid[i], 3);

                packet.ReadInt32("Unk 1", i);

                packet.ReadXORByte(guid[i], 0);
                packet.ReadXORByte(guid[i], 1);

                packet.ReadPackedTime("Time", i);

                packet.ReadXORByte(guid[i], 4);

                packet.ReadInt32("Unk 2", i);

                packet.ReadXORByte(guid[i], 7);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadXORByte(guid[i], 5);
                packet.WriteGuid("Guid", guid[i], i);
            }
        }
    }
}
