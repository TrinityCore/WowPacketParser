using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.SMSG_GUILD_QUERY_RESPONSE)]
        public static void HandleGuildQueryResponse(Packet packet)
        {
            var Guild1 = new byte[8];
            var Guild2 = new byte[8];

            int nameLen = 0;
            int rankCount = 0;
            int[] rankName = null;

            packet.StartBitStream(Guild2, 6, 2, 0, 3, 4, 1, 5);

            var hasData = packet.ReadBit();

            Guild1[0] = packet.ReadBit();

            nameLen = (int)packet.ReadBits(7);
            rankCount = (int)packet.ReadBits(21);

            if (hasData)
            {
                rankName = new int[rankCount];
                for (var j = 0; j < rankCount; j++)
                    rankName[j] = (int)packet.ReadBits(7);

                packet.StartBitStream(Guild1, 1, 2, 5, 3, 7, 4, 6);
            }

            Guild2[7] = packet.ReadBit();

            if (hasData)
            {
                packet.ReadInt32("Emblem Style");

                for (var j = 0; j < rankCount; j++)
                {
                    packet.ReadInt32("Rights Order", j);
                    packet.ReadWoWString("Rank Name", rankName[j], j);
                    packet.ReadInt32("Creation Order", j);
                }

                packet.ReadXORByte(Guild1, 1);
                packet.ReadInt32("Realm Id");
                packet.ReadInt32("Emblem Color");
                packet.ReadInt32("Emblem Background Color");
                packet.ReadInt32("Emblem Border Style");
                packet.ReadXORByte(Guild1, 0);
                packet.ReadInt32("Emblem Border Color");
                packet.ReadXORByte(Guild1, 6);

                packet.ReadWoWString("Guild Name", nameLen);

                packet.ReadXORByte(Guild1, 5);
                packet.ReadXORByte(Guild1, 3);
                packet.ReadXORByte(Guild1, 2);
                packet.ReadXORByte(Guild1, 7);
                packet.ReadXORByte(Guild1, 4);

                packet.WriteGuid("Guild1", Guild1);
            }

            packet.ParseBitStream(Guild2, 4, 1, 0, 3, 5, 7, 6, 2);

            packet.WriteGuid("Guild1", Guild2);
        }

        [Parser(Opcode.SMSG_GUILD_NEWS_TEXT)]
        public static void HandleNewText(Packet packet)
        {
            packet.ReadWoWString("Text", (int)packet.ReadBits(10));
        }

        [Parser(Opcode.SMSG_GUILD_RANK)]
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
                    packet.ReadEnum<GuildBankRightsFlag>("Tab Rights", TypeCode.Int32, i, j);
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
                    packet.WriteLine("[{0}][{1}] Profession: Id {2} - Value {3} - Rank {4}", i, j, id, value, rank);
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
                packet.ReadEnum<GuildMemberFlag>("Member Flags", TypeCode.Byte, i);
                packet.ReadInt32("Member Rank", i);
                packet.ReadEnum<Class>("Member Class", TypeCode.Byte, i);
                packet.ReadXORByte(guid[i], 2);
                packet.ReadInt64("Unk 2", i);
                packet.ReadInt64("Week activity", i);
                packet.ReadInt32("Zone Id", i);
                packet.ReadInt32("Total activity", i);

                packet.ReadWoWString("Public note", publicLength[i], i);
                packet.WriteGuid("Guid", guid[i], i);
                StoreGetters.AddName(new Guid(BitConverter.ToUInt64(guid[i], 0)), name);
            }

            packet.ReadWoWString("Guild Info", infoLength);
            packet.ReadWoWString("MOTD", motdLength);
        }

        [Parser(Opcode.CMSG_GUILD_REQUEST_PARTY_STATE)]
        public static void HandleGuildRequestPartyState(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 1, 2, 6, 5, 7, 4);
            packet.ParseBitStream(guid, 4, 1, 6, 7, 2, 5, 0);

            packet.WriteGuid("Guild GUID", guid);
        }
    }
}
