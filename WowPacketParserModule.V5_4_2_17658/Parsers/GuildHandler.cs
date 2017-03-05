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

            guildGUID[5] = packet.Translator.ReadBit();
            playerGUID[3] = packet.Translator.ReadBit();
            playerGUID[5] = packet.Translator.ReadBit();
            guildGUID[2] = packet.Translator.ReadBit();
            guildGUID[0] = packet.Translator.ReadBit();
            guildGUID[4] = packet.Translator.ReadBit();
            playerGUID[6] = packet.Translator.ReadBit();
            playerGUID[2] = packet.Translator.ReadBit();
            playerGUID[7] = packet.Translator.ReadBit();
            playerGUID[1] = packet.Translator.ReadBit();
            playerGUID[4] = packet.Translator.ReadBit();
            guildGUID[1] = packet.Translator.ReadBit();
            playerGUID[0] = packet.Translator.ReadBit();
            guildGUID[3] = packet.Translator.ReadBit();
            guildGUID[6] = packet.Translator.ReadBit();
            guildGUID[7] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(playerGUID, 6);
            packet.Translator.ReadXORByte(playerGUID, 3);
            packet.Translator.ReadXORByte(guildGUID, 1);
            packet.Translator.ReadXORByte(playerGUID, 0);
            packet.Translator.ReadXORByte(playerGUID, 7);
            packet.Translator.ReadXORByte(guildGUID, 0);
            packet.Translator.ReadXORByte(guildGUID, 5);
            packet.Translator.ReadXORByte(guildGUID, 2);
            packet.Translator.ReadXORByte(playerGUID, 1);
            packet.Translator.ReadXORByte(guildGUID, 3);
            packet.Translator.ReadXORByte(playerGUID, 2);
            packet.Translator.ReadXORByte(guildGUID, 7);
            packet.Translator.ReadXORByte(playerGUID, 4);
            packet.Translator.ReadXORByte(playerGUID, 5);
            packet.Translator.ReadXORByte(guildGUID, 6);
            packet.Translator.ReadXORByte(guildGUID, 4);

            packet.Translator.WriteGuid("PlayerGUID", playerGUID);
            packet.Translator.WriteGuid("GuildGUID", guildGUID);
        }

        [Parser(Opcode.SMSG_QUERY_GUILD_INFO_RESPONSE)]
        public static void HandleGuildQueryResponse(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[2] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();

            int nameLen = 0;
            int rankCount = 0;
            int[] rankName = null;
            var hasData = packet.Translator.ReadBit();
            if (hasData)
            {
                guid1[5] = packet.Translator.ReadBit();
                guid1[4] = packet.Translator.ReadBit();
                guid1[1] = packet.Translator.ReadBit();
                guid1[3] = packet.Translator.ReadBit();
                rankCount = (int)packet.Translator.ReadBits(21);
                guid1[0] = packet.Translator.ReadBit();

                rankName = new int[rankCount];
                for (var i = 0; i < rankCount; ++i)
                    rankName[i] = (int)packet.Translator.ReadBits(7);

                guid1[2] = packet.Translator.ReadBit();
                guid1[7] = packet.Translator.ReadBit();
                guid1[6] = packet.Translator.ReadBit();
                nameLen = (int)packet.Translator.ReadBits(7);
            }

            guid2[3] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();

            if (hasData)
            {
                packet.Translator.ReadXORByte(guid1, 0);
                packet.Translator.ReadXORByte(guid1, 1);
                packet.Translator.ReadXORByte(guid1, 5);
                packet.Translator.ReadInt32("Emblem Border Color");
                for (var j = 0; j < rankCount; j++)
                {
                    packet.Translator.ReadInt32("Rights Order", j);
                    packet.Translator.ReadInt32("Creation Order", j);
                    packet.Translator.ReadWoWString("Rank Name", rankName[j], j);
                }

                packet.Translator.ReadWoWString("Guild Name", nameLen);
                packet.Translator.ReadInt32("Realm Id");
                packet.Translator.ReadInt32("Emblem Background Color");
                packet.Translator.ReadXORByte(guid1, 7);
                packet.Translator.ReadXORByte(guid1, 6);
                packet.Translator.ReadXORByte(guid1, 2);
                packet.Translator.ReadInt32("Emblem Style");
                packet.Translator.ReadXORByte(guid1, 3);
                packet.Translator.ReadXORByte(guid1, 4);
                packet.Translator.ReadInt32("Emblem Border Style");
                packet.Translator.ReadInt32("Emblem Color");
                packet.Translator.WriteGuid("GuildGUID1", guid1);
            }

            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 7);

            packet.Translator.WriteGuid("GuildGUID2", guid2);
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER)]
        public static void HandleGuildRoster(Packet packet)
        {

            var motdLength = packet.Translator.ReadBits(10);
            var infoLength = packet.Translator.ReadBits(11);
            var size = packet.Translator.ReadBits(17);

            var guid = new byte[size][];
            var nameLength = new uint[size];
            var officerLength = new uint[size];
            var publicLength = new uint[size];

            for (var i = 0; i < size; ++i)
            {
                guid[i] = new byte[8];

                guid[i][1] = packet.Translator.ReadBit();
                guid[i][5] = packet.Translator.ReadBit();
                officerLength[i] = packet.Translator.ReadBits(8);
                packet.Translator.ReadBit("Can SoR", i);
                guid[i][4] = packet.Translator.ReadBit();
                guid[i][6] = packet.Translator.ReadBit();
                guid[i][7] = packet.Translator.ReadBit();
                guid[i][3] = packet.Translator.ReadBit();
                guid[i][2] = packet.Translator.ReadBit();
                nameLength[i] = packet.Translator.ReadBits(6);
                guid[i][0] = packet.Translator.ReadBit();
                publicLength[i] = packet.Translator.ReadBits(8);
                packet.Translator.ReadBit("Has Authenticator", i);
            }

            for (var i = 0; i < size; ++i)
            {
                packet.Translator.ReadByteE<Gender>("Gender", i);
                packet.Translator.ReadByteE<Class>("Member Class", i);
                packet.Translator.ReadXORByte(guid[i], 6);
                packet.Translator.ReadByte("Member Level", i);
                packet.Translator.ReadXORByte(guid[i], 0);

                for (var j = 0; j < 2; ++j)
                {
                    var value = packet.Translator.ReadUInt32();
                    var rank = packet.Translator.ReadUInt32();
                    var id = packet.Translator.ReadUInt32();

                    packet.AddValue("Profession", string.Format("Id {0} - Value {1} - Rank {2}", id, value, rank), i, j);
                }

                var name = packet.Translator.ReadWoWString("Name", nameLength[i], i);
                packet.Translator.ReadInt32("RealmId", i);
                packet.Translator.ReadSingle("Last online", i);
                packet.Translator.ReadInt64("Unk 2", i);
                packet.Translator.ReadInt64("Week activity", i);
                packet.Translator.ReadInt32("Zone Id", i);
                packet.Translator.ReadXORByte(guid[i], 5);
                packet.Translator.ReadXORByte(guid[i], 7);
                packet.Translator.ReadXORByte(guid[i], 4);
                packet.Translator.ReadWoWString("Officer note", officerLength[i], i);
                packet.Translator.ReadInt32("Int218", i);
                packet.Translator.ReadXORByte(guid[i], 2);
                packet.Translator.ReadInt32("Member Rank", i);
                packet.Translator.ReadXORByte(guid[i], 1);
                packet.Translator.ReadByteE<GuildMemberFlag>("Member Flags", i);
                packet.Translator.ReadInt32("Guild Reputation", i);
                packet.Translator.ReadWoWString("Public note", publicLength[i], i);
                packet.Translator.ReadXORByte(guid[i], 3);
                packet.Translator.ReadInt32("Member Achievement Points", i);
                packet.Translator.WriteGuid("Guid", guid[i], i);
                StoreGetters.AddName(new WowGuid64(BitConverter.ToUInt64(guid[i], 0)), name);
            }

            packet.Translator.ReadPackedTime("Time");
            packet.Translator.ReadWoWString("MOTD", motdLength);
            packet.Translator.ReadWoWString("Guild Info", infoLength);
            packet.Translator.ReadInt32("Unk Uint32 4");
            packet.Translator.ReadInt32("Weekly Reputation Cap");
            packet.Translator.ReadInt32("Accounts In Guild");
        }

        [Parser(Opcode.SMSG_GUILD_RANKS)]
        public static void HandleGuildRankServer(Packet packet)
        {
            const int guildBankMaxTabs = 8;
            var count = packet.Translator.ReadBits("Count", 17);
            var length = new uint[count];

            for (var i = 0; i < count; ++i)
                length[i] = packet.Translator.ReadBits(7);

            for (var i = 0; i < count; ++i)
            {
                for (var j = 0; j < guildBankMaxTabs; ++j)
                {
                    packet.Translator.ReadInt32("Tab Slots", i, j);
                    packet.Translator.ReadInt32E<GuildBankRightsFlag>("Tab Rights", i, j);
                }

                packet.Translator.ReadInt32("Gold Per Day", i);
                packet.Translator.ReadInt32("Unk 1", i);
                packet.Translator.ReadInt32("Rights Order", i);
                packet.Translator.ReadWoWString("Name", length[i], i);
                packet.Translator.ReadInt32("Creation Order", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_NEWS_TEXT)]
        public static void HandleNewText(Packet packet)
        {
            packet.Translator.ReadWoWString("Text", (int)packet.Translator.ReadBits(10));
        }

        [Parser(Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS)]
        public static void HandleGuildAchievementData(Packet packet)
        {
            var count = packet.Translator.ReadBits("Achievement count", 20);

            var guid = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                guid[i] = new byte[8];
                packet.Translator.StartBitStream(guid[i], 7, 5, 3, 4, 0, 6, 2, 1);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32<AchievementId>("Achievement Id", i);

                packet.Translator.ReadXORByte(guid[i], 6);
                packet.Translator.ReadXORByte(guid[i], 3);

                packet.Translator.ReadInt32("Unk 1", i);

                packet.Translator.ReadXORByte(guid[i], 0);
                packet.Translator.ReadXORByte(guid[i], 1);

                packet.Translator.ReadPackedTime("Time", i);

                packet.Translator.ReadXORByte(guid[i], 4);

                packet.Translator.ReadInt32("Unk 2", i);

                packet.Translator.ReadXORByte(guid[i], 7);
                packet.Translator.ReadXORByte(guid[i], 4);
                packet.Translator.ReadXORByte(guid[i], 5);
                packet.Translator.WriteGuid("Guid", guid[i], i);
            }
        }
    }
}
