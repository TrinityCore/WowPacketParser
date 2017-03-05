using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.SMSG_QUERY_GUILD_INFO_RESPONSE)]
        public static void HandleGuildQueryResponse(Packet packet)
        {
            var guild1 = new byte[8];
            var guild2 = new byte[8];

            int[] rankName = null;

            packet.Translator.StartBitStream(guild2, 6, 2, 0, 3, 4, 1, 5);

            var hasData = packet.Translator.ReadBit();

            guild1[0] = packet.Translator.ReadBit();

            var nameLen = (int)packet.Translator.ReadBits(7);
            var rankCount = (int)packet.Translator.ReadBits(21);

            if (hasData)
            {
                rankName = new int[rankCount];
                for (var j = 0; j < rankCount; j++)
                    rankName[j] = (int)packet.Translator.ReadBits(7);

                packet.Translator.StartBitStream(guild1, 1, 2, 5, 3, 7, 4, 6);
            }

            guild2[7] = packet.Translator.ReadBit();

            if (hasData)
            {
                packet.Translator.ReadInt32("Emblem Style");

                for (var j = 0; j < rankCount; j++)
                {
                    packet.Translator.ReadInt32("Rights Order", j);
                    packet.Translator.ReadWoWString("Rank Name", rankName[j], j);
                    packet.Translator.ReadInt32("Creation Order", j);
                }

                packet.Translator.ReadXORByte(guild1, 1);
                packet.Translator.ReadInt32("Realm Id");
                packet.Translator.ReadInt32("Emblem Color");
                packet.Translator.ReadInt32("Emblem Background Color");
                packet.Translator.ReadInt32("Emblem Border Style");
                packet.Translator.ReadXORByte(guild1, 0);
                packet.Translator.ReadInt32("Emblem Border Color");
                packet.Translator.ReadXORByte(guild1, 6);

                packet.Translator.ReadWoWString("Guild Name", nameLen);

                packet.Translator.ReadXORByte(guild1, 5);
                packet.Translator.ReadXORByte(guild1, 3);
                packet.Translator.ReadXORByte(guild1, 2);
                packet.Translator.ReadXORByte(guild1, 7);
                packet.Translator.ReadXORByte(guild1, 4);

                packet.Translator.WriteGuid("Guild1", guild1);
            }

            packet.Translator.ParseBitStream(guild2, 4, 1, 0, 3, 5, 7, 6, 2);

            packet.Translator.WriteGuid("Guild1", guild2);
        }

        [Parser(Opcode.SMSG_GUILD_NEWS_TEXT)]
        public static void HandleNewText(Packet packet)
        {
            packet.Translator.ReadWoWString("Text", (int)packet.Translator.ReadBits(10));
        }

        [Parser(Opcode.SMSG_GUILD_RANKS)]
        public static void HandleGuildRankServer434(Packet packet)
        {
            const int guildBankMaxTabs = 8;
            var count = packet.Translator.ReadBits("Count", 17);
            var length = new int[count];

            for (var i = 0; i < count; ++i)
                length[i] = (int)packet.Translator.ReadBits(7);

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("Gold Per Day", i);

                for (var j = 0; j < guildBankMaxTabs; ++j)
                {
                    packet.Translator.ReadInt32E<GuildBankRightsFlag>("Tab Rights", i, j);
                    packet.Translator.ReadInt32("Tab Slots", i, j);
                }

                packet.Translator.ReadInt32("Unk 1", i);
                packet.Translator.ReadWoWString("Name", length[i], i);
                packet.Translator.ReadInt32("Creation Order", i);
                packet.Translator.ReadInt32("Rights Order", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER)]
        public static void HandleGuildRoster(Packet packet)
        {
            packet.Translator.ReadPackedTime("Time");
            packet.Translator.ReadInt32("Accounts In Guild");
            packet.Translator.ReadInt32("Unk Uint32 4");
            packet.Translator.ReadInt32("Weekly Reputation Cap");

            var motdLength = packet.Translator.ReadBits(10);
            var size = packet.Translator.ReadBits(17);

            var guid = new byte[size][];
            var nameLength = new uint[size];
            var publicLength = new uint[size];
            var officerLength = new uint[size];

            for (var i = 0; i < size; ++i)
            {
                guid[i] = new byte[8];

                packet.Translator.ReadBit("Can SoR", i);
                packet.Translator.StartBitStream(guid[i], 0, 7, 2);
                packet.Translator.ReadBit("Has Authenticator", i);
                officerLength[i] = packet.Translator.ReadBits(8);
                guid[i][3] = packet.Translator.ReadBit();
                nameLength[i] = packet.Translator.ReadBits(6);
                packet.Translator.StartBitStream(guid[i], 6, 4, 1, 5);
                publicLength[i] = packet.Translator.ReadBits(8);
            }

            var infoLength = packet.Translator.ReadBits(11);

            for (var i = 0; i < size; ++i)
            {
                packet.Translator.ReadSingle("Last online", i);
                packet.Translator.ReadWoWString("Officer note", officerLength[i], i);
                packet.Translator.ReadXORByte(guid[i], 4);
                packet.Translator.ReadByte("Unk Byte", i);
                packet.Translator.ReadXORByte(guid[i], 0);
                packet.Translator.ReadInt32("Remaining guild week Rep", i);

                for (var j = 0; j < 2; ++j)
                {
                    var rank = packet.Translator.ReadUInt32();
                    var value = packet.Translator.ReadUInt32();
                    var id = packet.Translator.ReadUInt32();
                    packet.AddValue("Profession", string.Format("Id {0} - Value {1} - Rank {2}", id, value, rank), i, j);
                }

                packet.Translator.ReadInt32("Guild Reputation", i);

                var name = packet.Translator.ReadWoWString("Name", nameLength[i], i);

                packet.Translator.ReadXORByte(guid[i], 6);
                packet.Translator.ReadByte("Member Level", i);
                packet.Translator.ReadXORByte(guid[i], 5);
                packet.Translator.ReadXORByte(guid[i], 1);
                packet.Translator.ReadXORByte(guid[i], 3);
                packet.Translator.ReadInt32("Member Achievement Points", i);
                packet.Translator.ReadXORByte(guid[i], 7);
                packet.Translator.ReadByteE<GuildMemberFlag>("Member Flags", i);
                packet.Translator.ReadInt32("Member Rank", i);
                packet.Translator.ReadByteE<Class>("Member Class", i);
                packet.Translator.ReadXORByte(guid[i], 2);
                packet.Translator.ReadInt64("Unk 2", i);
                packet.Translator.ReadInt64("Week activity", i);
                packet.Translator.ReadInt32("Zone Id", i);
                packet.Translator.ReadInt32("Total activity", i);

                packet.Translator.ReadWoWString("Public note", publicLength[i], i);
                packet.Translator.WriteGuid("Guid", guid[i], i);
                StoreGetters.AddName(new WowGuid64(BitConverter.ToUInt64(guid[i], 0)), name);
            }

            packet.Translator.ReadWoWString("Guild Info", infoLength);
            packet.Translator.ReadWoWString("MOTD", motdLength);
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_PARTY_STATE)]
        public static void HandleGuildRequestPartyState(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 0, 1, 2, 6, 5, 7, 4);
            packet.Translator.ParseBitStream(guid, 4, 1, 6, 7, 2, 5, 0);

            packet.Translator.WriteGuid("Guild GUID", guid);
        }

        [Parser(Opcode.SMSG_GUILD_PARTY_STATE)]
        public static void HandleGuildPartyStateResponse(Packet packet)
        {
            packet.Translator.ReadSingle("Guild XP multiplier");
            packet.Translator.ReadInt32("Int10");
            packet.Translator.ReadInt32("Int14");
            packet.Translator.ReadBit("Is guild group");
        }

        [Parser(Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS)]
        public static void HandleGuildAchievementData(Packet packet)
        {
            var count = packet.Translator.ReadBits("Achievement count", 20);

            var guid = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                guid[i] = new byte[8];
                packet.Translator.StartBitStream(guid[i], 4, 6, 3, 7, 0, 2, 5, 1);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORByte(guid[i], 6);
                packet.Translator.ReadInt32<AchievementId>("Achievement Id", i);
                packet.Translator.ReadInt32("Unk 1", i);
                packet.Translator.ReadXORByte(guid[i], 3);
                packet.Translator.ReadXORByte(guid[i], 7);
                packet.Translator.ReadXORByte(guid[i], 1);
                packet.Translator.ReadXORByte(guid[i], 0);
                packet.Translator.ReadInt32("Unk 2", i);
                packet.Translator.ReadXORByte(guid[i], 5);
                packet.Translator.ReadXORByte(guid[i], 4);
                packet.Translator.ReadPackedTime("Date", i);
                packet.Translator.ReadXORByte(guid[i], 2);

                packet.Translator.WriteGuid("GUID", guid[i], i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_REPUTATION_WEEKLY_CAP)]
        public static void HandleGuildReputationWeeklyCap(Packet packet)
        {
            packet.Translator.ReadUInt32("Cap");
        }
    }
}
