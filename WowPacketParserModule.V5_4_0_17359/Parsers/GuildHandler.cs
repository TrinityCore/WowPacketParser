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

            packet.StartBitStream(guild2, 6, 2, 0, 3, 4, 1, 5);

            var hasData = packet.ReadBit();

            guild1[0] = packet.ReadBit();

            var nameLen = (int)packet.ReadBits(7);
            var rankCount = (int)packet.ReadBits(21);

            if (hasData)
            {
                rankName = new int[rankCount];
                for (var j = 0; j < rankCount; j++)
                    rankName[j] = (int)packet.ReadBits(7);

                packet.StartBitStream(guild1, 1, 2, 5, 3, 7, 4, 6);
            }

            guild2[7] = packet.ReadBit();

            if (hasData)
            {
                packet.ReadInt32("Emblem Style");

                for (var j = 0; j < rankCount; j++)
                {
                    packet.ReadInt32("Rights Order", j);
                    packet.ReadWoWString("Rank Name", rankName[j], j);
                    packet.ReadInt32("Creation Order", j);
                }

                packet.ReadXORByte(guild1, 1);
                packet.ReadInt32("Realm Id");
                packet.ReadInt32("Emblem Color");
                packet.ReadInt32("Emblem Background Color");
                packet.ReadInt32("Emblem Border Style");
                packet.ReadXORByte(guild1, 0);
                packet.ReadInt32("Emblem Border Color");
                packet.ReadXORByte(guild1, 6);

                packet.ReadWoWString("Guild Name", nameLen);

                packet.ReadXORByte(guild1, 5);
                packet.ReadXORByte(guild1, 3);
                packet.ReadXORByte(guild1, 2);
                packet.ReadXORByte(guild1, 7);
                packet.ReadXORByte(guild1, 4);

                packet.WriteGuid("Guild1", guild1);
            }

            packet.ParseBitStream(guild2, 4, 1, 0, 3, 5, 7, 6, 2);

            packet.WriteGuid("Guild1", guild2);
        }

        [Parser(Opcode.SMSG_GUILD_NEWS_TEXT)]
        public static void HandleNewText(Packet packet)
        {
            packet.ReadWoWString("Text", (int)packet.ReadBits(10));
        }

        [Parser(Opcode.SMSG_GUILD_RANKS)]
        public static void HandleGuildRankServer434(Packet packet)
        {
            const int guildBankMaxTabs = 8;
            var count = packet.ReadBits("Count", 17);
            var length = new int[count];

            for (var i = 0; i < count; ++i)
                length[i] = (int)packet.ReadBits(7);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Gold Per Day", i);

                for (var j = 0; j < guildBankMaxTabs; ++j)
                {
                    packet.ReadInt32E<GuildBankRightsFlag>("Tab Rights", i, j);
                    packet.ReadInt32("Tab Slots", i, j);
                }

                packet.ReadInt32("Unk 1", i);
                packet.ReadWoWString("Name", length[i], i);
                packet.ReadInt32("Creation Order", i);
                packet.ReadInt32("Rights Order", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER)]
        public static void HandleGuildRoster(Packet packet)
        {
            packet.ReadPackedTime("Time");
            packet.ReadInt32("Accounts In Guild");
            packet.ReadInt32("Unk Uint32 4");
            packet.ReadInt32("Weekly Reputation Cap");

            var motdLength = packet.ReadBits(10);
            var size = packet.ReadBits(17);

            var guid = new byte[size][];
            var nameLength = new uint[size];
            var publicLength = new uint[size];
            var officerLength = new uint[size];

            for (var i = 0; i < size; ++i)
            {
                guid[i] = new byte[8];

                packet.ReadBit("Can SoR", i);
                packet.StartBitStream(guid[i], 0, 7, 2);
                packet.ReadBit("Has Authenticator", i);
                officerLength[i] = packet.ReadBits(8);
                guid[i][3] = packet.ReadBit();
                nameLength[i] = packet.ReadBits(6);
                packet.StartBitStream(guid[i], 6, 4, 1, 5);
                publicLength[i] = packet.ReadBits(8);
            }

            var infoLength = packet.ReadBits(11);

            for (var i = 0; i < size; ++i)
            {
                packet.ReadSingle("Last online", i);
                packet.ReadWoWString("Officer note", officerLength[i], i);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadByte("Unk Byte", i);
                packet.ReadXORByte(guid[i], 0);
                packet.ReadInt32("Remaining guild week Rep", i);

                for (var j = 0; j < 2; ++j)
                {
                    var rank = packet.ReadUInt32();
                    var value = packet.ReadUInt32();
                    var id = packet.ReadUInt32();
                    packet.AddValue("Profession", string.Format("Id {0} - Value {1} - Rank {2}", id, value, rank), i, j);
                }

                packet.ReadInt32("Guild Reputation", i);

                var name = packet.ReadWoWString("Name", nameLength[i], i);

                packet.ReadXORByte(guid[i], 6);
                packet.ReadByte("Member Level", i);
                packet.ReadXORByte(guid[i], 5);
                packet.ReadXORByte(guid[i], 1);
                packet.ReadXORByte(guid[i], 3);
                packet.ReadInt32("Member Achievement Points", i);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadByteE<GuildMemberFlag>("Member Flags", i);
                packet.ReadInt32("Member Rank", i);
                packet.ReadByteE<Class>("Member Class", i);
                packet.ReadXORByte(guid[i], 2);
                packet.ReadInt64("Unk 2", i);
                packet.ReadInt64("Week activity", i);
                packet.ReadInt32("Zone Id", i);
                packet.ReadInt32("Total activity", i);

                packet.ReadWoWString("Public note", publicLength[i], i);
                packet.WriteGuid("Guid", guid[i], i);
                StoreGetters.AddName(new WowGuid64(BitConverter.ToUInt64(guid[i], 0)), name);
            }

            packet.ReadWoWString("Guild Info", infoLength);
            packet.ReadWoWString("MOTD", motdLength);
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_PARTY_STATE)]
        public static void HandleGuildRequestPartyState(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 1, 2, 6, 5, 7, 4);
            packet.ParseBitStream(guid, 4, 1, 6, 7, 2, 5, 0);

            packet.WriteGuid("Guild GUID", guid);
        }

        [Parser(Opcode.SMSG_GUILD_PARTY_STATE)]
        public static void HandleGuildPartyStateResponse(Packet packet)
        {
            packet.ReadSingle("Guild XP multiplier");
            packet.ReadInt32("Int10");
            packet.ReadInt32("Int14");
            packet.ReadBit("Is guild group");
        }

        [Parser(Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS)]
        public static void HandleGuildAchievementData(Packet packet)
        {
            var count = packet.ReadBits("Achievement count", 20);

            var guid = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                guid[i] = new byte[8];
                packet.StartBitStream(guid[i], 4, 6, 3, 7, 0, 2, 5, 1);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guid[i], 6);
                packet.ReadInt32<AchievementId>("Achievement Id", i);
                packet.ReadInt32("Unk 1", i);
                packet.ReadXORByte(guid[i], 3);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadXORByte(guid[i], 1);
                packet.ReadXORByte(guid[i], 0);
                packet.ReadInt32("Unk 2", i);
                packet.ReadXORByte(guid[i], 5);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadPackedTime("Date", i);
                packet.ReadXORByte(guid[i], 2);

                packet.WriteGuid("GUID", guid[i], i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_REPUTATION_WEEKLY_CAP)]
        public static void HandleGuildReputationWeeklyCap(Packet packet)
        {
            packet.ReadUInt32("Cap");
        }
    }
}
