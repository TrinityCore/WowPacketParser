using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_3_4_15595.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.SMSG_COMPRESSED_GUILD_ROSTER)]
        public static void HandleCompressedGuildRoster(Packet packet)
        {
            using (var packet2 = packet.Inflate(packet.Translator.ReadInt32()))
            {
                HandleGuildRoster434(packet2);
            }
        }

        [Parser(Opcode.CMSG_GUILD_INVITE)]
        public static void HandleGuildInviteByName(Packet packet)
        {
            var count = packet.Translator.ReadBits("String Length", 7);
            packet.Translator.ReadWoWString("Name", count);
        }

        [Parser(Opcode.SMSG_GUILD_RANKS)]
        public static void HandleGuildRankServer434(Packet packet)
        {
            const int guildBankMaxTabs = 8;
            var count = packet.Translator.ReadBits("Count", 18);
            var length = new int[count];
            for (var i = 0; i < count; ++i)
                length[i] = (int)packet.Translator.ReadBits(7);

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("Creation Order", i);
                for (var j = 0; j < guildBankMaxTabs; ++j)
                {
                    packet.Translator.ReadInt32("Tab Slots", i, j);
                    packet.Translator.ReadInt32E<GuildBankRightsFlag>("Tab Rights", i, j);
                }
                packet.Translator.ReadInt32("Gold Per Day", i);
                packet.Translator.ReadInt32E<GuildRankRightsFlag>("Rights", i);
                packet.Translator.ReadWoWString("Name", length[i], i);
                packet.Translator.ReadInt32("Rights Order", i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_DELETE_RANK)]
        public static void HandleGuildDelRank434(Packet packet)
        {
            packet.Translator.ReadUInt32("Rank Id");
        }

        [Parser(Opcode.CMSG_GUILD_SWITCH_RANK)]
        public static void HandleGuildSwitchRank434(Packet packet)
        {
            packet.Translator.ReadUInt32("Rank Id");
            packet.Translator.ReadBit("Direction");
        }

        [Parser(Opcode.SMSG_GUILD_SEND_RANK_CHANGE)]
        public static void HandleGuildRanksUpdate(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid1[7] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Promote?");
            guid1[5] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();

            packet.Translator.ReadInt32("Rank Index");

            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.WriteGuid("Guid 1", guid1);
            packet.Translator.WriteGuid("Guid 2", guid2);
        }

        [Parser(Opcode.CMSG_GUILD_SET_RANK_PERMISSIONS)]
        public static void HandleGuildRank434(Packet packet)
        {
            packet.Translator.ReadUInt32("Old Rank Id");
            packet.Translator.ReadUInt32E<GuildRankRightsFlag>("Old Rights");
            packet.Translator.ReadUInt32E<GuildRankRightsFlag>("New Rights");

            for (var i = 0; i < 8; ++i)
            {
                packet.Translator.ReadUInt32("Tab Slot", i);
                packet.Translator.ReadUInt32E<GuildBankRightsFlag>("Tab Rights", i);
            }

            packet.Translator.ReadUInt32("Money Per Day");
            packet.Translator.ReadUInt32("New Rank Id");
            var length = packet.Translator.ReadBits(7);
            packet.Translator.ReadWoWString("Rank Name", length);
        }

        [Parser(Opcode.SMSG_GUILD_PARTY_STATE)]
        public static void HandleGuildPartyStateResponse434(Packet packet)
        {
            packet.Translator.ReadBit("Is guild group");
            packet.Translator.ReadSingle("Guild XP multiplier");
            packet.Translator.ReadUInt32("Current guild members");
            packet.Translator.ReadUInt32("Needed guild members");
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_PARTY_STATE)]
        public static void HandleGuildUpdatePartyState434(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(0, 6, 7, 3, 5, 1, 2, 4);
            packet.Translator.ParseBitStream(guid, 6, 3, 2, 1, 5, 0, 7, 4);
            packet.Translator.WriteGuid("Guild Guid", guid);
        }

        [Parser(Opcode.CMSG_GUILD_ASSIGN_MEMBER_RANK)]
        public static void HandleGuildAssignMemberRank(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.Translator.ReadUInt32("Rank");

            guid1[1] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid2, 7);

            packet.Translator.WriteGuid("GUID 1", guid1);
            packet.Translator.WriteGuid("GUID 2", guid2);
        }

        [Parser(Opcode.CMSG_GUILD_GET_ROSTER)]
        public static void HandleGuildRosterRequest434(Packet packet)
        {
            // The client does not write these 2 guids properly.
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            guid2[2] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid1, 6);
            // packet.Translator.WriteGuid("Guid1", guid1);
            // packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER)]
        public static void HandleGuildRoster434(Packet packet)
        {
            var motdLength = packet.Translator.ReadBits(11);
            var size = packet.Translator.ReadBits(18);

            var guid = new byte[size][];
            var nameLength = new uint[size];
            var publicLength = new uint[size];
            var officerLength = new uint[size];

            for (var i = 0; i < size; ++i)
            {
                guid[i] = new byte[8];
                guid[i][3] = packet.Translator.ReadBit();
                guid[i][4] = packet.Translator.ReadBit();
                packet.Translator.ReadBit("Has Authenticator", i);
                packet.Translator.ReadBit("Can SoR", i);
                publicLength[i] = packet.Translator.ReadBits(8);
                officerLength[i] = packet.Translator.ReadBits(8);
                guid[i][0] = packet.Translator.ReadBit();
                nameLength[i] = packet.Translator.ReadBits(7);
                guid[i][1] = packet.Translator.ReadBit();
                guid[i][2] = packet.Translator.ReadBit();
                guid[i][6] = packet.Translator.ReadBit();
                guid[i][5] = packet.Translator.ReadBit();
                guid[i][7] = packet.Translator.ReadBit();
            }
            var infoLength = packet.Translator.ReadBits(12);

            for (var i = 0; i < size; ++i)
            {
                packet.Translator.ReadByteE<Class>("Member Class", i);
                packet.Translator.ReadInt32("Guild Reputation", i);

                packet.Translator.ReadXORByte(guid[i], 0);

                packet.Translator.ReadUInt64("Week activity", i);
                packet.Translator.ReadUInt32("Member Rank", i);
                packet.Translator.ReadUInt32("Member Achievement Points", i);
                for (var j = 0; j < 2; ++j)
                {
                    var rank = packet.Translator.ReadUInt32();
                    var value = packet.Translator.ReadUInt32();
                    var id = packet.Translator.ReadUInt32();
                    packet.AddValue("Profession", string.Format("Id {0} - Value {1} - Rank {2}", id, value, rank), i, j);
                }

                packet.Translator.ReadXORByte(guid[i], 2);

                packet.Translator.ReadByteE<GuildMemberFlag>("Member Flags", i);
                packet.Translator.ReadInt32<ZoneId>("Zone Id", i);
                packet.Translator.ReadUInt64("Total activity", i);

                packet.Translator.ReadXORByte(guid[i], 7);

                packet.Translator.ReadUInt32("Remaining guild week Rep", i);
                packet.Translator.ReadWoWString("Public note", publicLength[i], i);

                if (guid[i][3] != 0)
                    guid[i][3] ^= packet.Translator.ReadByte();

                packet.Translator.ReadByte("Member Level", i);
                packet.Translator.ReadInt32("Unk 2", i);

                packet.Translator.ReadXORByte(guid[i], 5);
                packet.Translator.ReadXORByte(guid[i], 4);

                packet.Translator.ReadByte("Unk Byte", i);

                packet.Translator.ReadXORByte(guid[i], 1);

                packet.Translator.ReadSingle("Last online", i);
                packet.Translator.ReadWoWString("Officer note", officerLength[i], i);

                if (guid[i][6] != 0)
                    guid[i][6] ^= packet.Translator.ReadByte();

                var name = packet.Translator.ReadWoWString("Name", nameLength[i], i);
                packet.Translator.WriteGuid("Guid", guid[i], i);
                StoreGetters.AddName(new WowGuid64(BitConverter.ToUInt64(guid[i], 0)), name);
            }

            packet.Translator.ReadWoWString("Guild Info", infoLength);
            packet.Translator.ReadWoWString("MOTD", motdLength);
            packet.Translator.ReadUInt32("Accounts In Guild");
            packet.Translator.ReadUInt32("Weekly Reputation Cap");
            packet.Translator.ReadPackedTime("CreationTime");
            packet.Translator.ReadUInt32("Unk Uint32 4");
        }

        [Parser(Opcode.CMSG_GUILD_OFFICER_REMOVE_MEMBER)]
        public static void HandleGuildRemove434(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(6, 5, 4, 0, 1, 3, 7, 2);
            packet.Translator.ParseBitStream(guid, 2, 6, 5, 7, 1, 4, 3, 0);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_GUILD_INVITE)]
        public static void HandleGuildInvite434(Packet packet)
        {
            var newGuildGuid = new byte[8];
            var oldGuildGuid = new byte[8];

            packet.Translator.ReadInt32("Guild Level");
            packet.Translator.ReadInt32("Border Style");
            packet.Translator.ReadInt32("Border Color");
            packet.Translator.ReadInt32("Emblem Texture");
            packet.Translator.ReadInt32("Emblem Background Color");
            packet.Translator.ReadInt32("Emblem Color");

            newGuildGuid[3] = packet.Translator.ReadBit();
            newGuildGuid[2] = packet.Translator.ReadBit();

            var oldGuildNameLength = packet.Translator.ReadBits(8);

            newGuildGuid[1] = packet.Translator.ReadBit();
            oldGuildGuid[6] = packet.Translator.ReadBit();
            oldGuildGuid[4] = packet.Translator.ReadBit();
            oldGuildGuid[1] = packet.Translator.ReadBit();
            oldGuildGuid[5] = packet.Translator.ReadBit();
            oldGuildGuid[7] = packet.Translator.ReadBit();
            oldGuildGuid[2] = packet.Translator.ReadBit();
            newGuildGuid[7] = packet.Translator.ReadBit();
            newGuildGuid[0] = packet.Translator.ReadBit();
            newGuildGuid[6] = packet.Translator.ReadBit();

            var newGuildNameLength = packet.Translator.ReadBits(8);

            oldGuildGuid[3] = packet.Translator.ReadBit();
            oldGuildGuid[0] = packet.Translator.ReadBit();
            newGuildGuid[5] = packet.Translator.ReadBit();

            var inviterNameLength = packet.Translator.ReadBits(7);

            newGuildGuid[4] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(newGuildGuid, 1);
            packet.Translator.ReadXORByte(oldGuildGuid, 3);
            packet.Translator.ReadXORByte(newGuildGuid, 6);
            packet.Translator.ReadXORByte(oldGuildGuid, 2);
            packet.Translator.ReadXORByte(oldGuildGuid, 1);
            packet.Translator.ReadXORByte(newGuildGuid, 0);

            packet.Translator.ReadWoWString("Old Guild Name", oldGuildNameLength);

            packet.Translator.ReadXORByte(newGuildGuid, 7);
            packet.Translator.ReadXORByte(newGuildGuid, 2);

            packet.Translator.ReadWoWString("Inviter Name", inviterNameLength);

            packet.Translator.ReadXORByte(oldGuildGuid, 7);
            packet.Translator.ReadXORByte(oldGuildGuid, 6);
            packet.Translator.ReadXORByte(oldGuildGuid, 5);
            packet.Translator.ReadXORByte(oldGuildGuid, 0);
            packet.Translator.ReadXORByte(newGuildGuid, 4);

            packet.Translator.ReadWoWString("New Guild Name", newGuildNameLength);

            packet.Translator.ReadXORByte(newGuildGuid, 5);
            packet.Translator.ReadXORByte(newGuildGuid, 3);
            packet.Translator.ReadXORByte(oldGuildGuid, 4);

            packet.Translator.WriteGuid("New Guild Guid", newGuildGuid);
            packet.Translator.WriteGuid("Old Guild Guid", oldGuildGuid);
        }

        [Parser(Opcode.CMSG_GUILD_MOTD)]
        public static void HandleGuildMOTD434(Packet packet)
        {
            packet.Translator.ReadWoWString("MOTD", (int)packet.Translator.ReadBits(11));
        }

        [Parser(Opcode.CMSG_GUILD_INFO_TEXT)]
        public static void HandleGuildInfo434(Packet packet)
        {
            packet.Translator.ReadWoWString("Text", (int)packet.Translator.ReadBits(12));
        }

        [Parser(Opcode.CMSG_GUILD_CHANGE_NAME_REQUEST)]
        public static void HandleGuildNameChange(Packet packet)
        {
            packet.Translator.ReadWoWString("New Name", (int)packet.Translator.ReadBits(8));
        }

        [Parser(Opcode.CMSG_GUILD_SET_NOTE)]
        public static void HandleGuildSetNote434(Packet packet)
        {
            var guid = new byte[8];
            guid[1] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Public");
            guid[6] = packet.Translator.ReadBit();
            var len = packet.Translator.ReadBits("Note Length", 8);
            guid[2] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadWoWString("Note", len);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_GUILD_BANK_QUERY_RESULTS)]
        public static void HandleGuildBankList434(Packet packet)
        {
            packet.Translator.ReadBit("Unk");
            var count = packet.Translator.ReadBits("Item count", 20);
            var count2 = packet.Translator.ReadBits("Tab count", 22);

            var icons = new uint[count2];
            var texts = new uint[count2];
            var enchants = new uint[count];

            for (var i = 0; i < count; ++i)
                enchants[i] = packet.Translator.ReadBits(24); // Number of Enchantments ?

            for (var i = 0; i < count2; ++i)
            {
                icons[i] = packet.Translator.ReadBits(9);
                texts[i] = packet.Translator.ReadBits(7);
            }

            for (var i = 0; i < count2; ++i)
            {
                packet.Translator.ReadWoWString("Icon", icons[i], i);
                packet.Translator.ReadUInt32("Index", i);
                packet.Translator.ReadWoWString("Text", texts[i], i);
            }

            packet.Translator.ReadUInt64("Money");

            for (var i = 0; i < count; ++i)
            {
                for (var j = 0; j < enchants[i]; ++j)
                {
                    packet.Translator.ReadUInt32("Enchantment Slot Id?", i, j);
                    packet.Translator.ReadUInt32("Enchantment Id?", i, j);
                }
                packet.Translator.ReadUInt32("Unk UInt32 1", i); // Only seen 0
                packet.Translator.ReadUInt32("Unk UInt32 2", i); // Only seen 0
                packet.Translator.ReadUInt32("Unk UInt32 3", i); // Only seen 0
                packet.Translator.ReadUInt32("Stack Count", i);
                packet.Translator.ReadUInt32("Slot Id", i);
                packet.Translator.ReadUInt32E<UnknownFlags>("Unk mask", i);
                packet.Translator.ReadInt32<ItemId>("Item Entry", i);
                packet.Translator.ReadInt32("Random Item Property Id", i);
                packet.Translator.ReadUInt32("Spell Charges", i);
                packet.Translator.ReadUInt32("Item Suffix Factor", i);
            }
            packet.Translator.ReadUInt32("Tab");
            packet.Translator.ReadInt32("Remaining Withdraw");
        }

        [Parser(Opcode.CMSG_GUILD_REQUEST_MAX_DAILY_XP)]
        public static void HandleGuildRequestMaxDailyXP434(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(0, 3, 5, 1, 4, 6, 7, 2);
            packet.Translator.ParseBitStream(guid, 7, 4, 3, 5, 1, 2, 6, 0);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_XP)]
        public static void HandleRequestGuildXP(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(2, 1, 0, 5, 4, 7, 6, 3);
            packet.Translator.ParseBitStream(guid, 7, 2, 3, 6, 1, 5, 0, 4);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_NEWS)]
        public static void HandleGuildQueryNews434(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(4, 2, 6, 3, 5, 0, 1, 7);
            packet.Translator.ParseBitStream(guid, 4, 1, 5, 6, 0, 3, 7, 2);
            packet.Translator.WriteGuid("GUID", guid);
        }


        [Parser(Opcode.CMSG_GUILD_GET_RANKS)]
        public static void HandleGuildRanks434(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(2, 3, 0, 6, 4, 7, 5, 1);
            packet.Translator.ParseBitStream(guid, 3, 4, 5, 7, 1, 0, 6, 2);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_GUILD_NEWS_UPDATE)]
        public static void HandleGuildNewsUpdate434(Packet packet)
        {
            var size = packet.Translator.ReadBits("Size", 21);

            var guidOut = new byte[size][];
            var guidIn = new byte[size][][];
            var count = new uint[size];

            for (int i = 0; i < size; ++i)
            {
                count[i] = packet.Translator.ReadBits("Count", 26);

                guidOut[i] = new byte[8];
                guidOut[i][7] = packet.Translator.ReadBit(); // 55

                guidIn[i] = new byte[count[i]][];
                for (int j = 0; j < count[i]; ++j)
                    guidIn[i][j] = packet.Translator.StartBitStream(7, 1, 5, 3, 4, 6, 0, 2);

                guidOut[i][0] = packet.Translator.ReadBit(); // 48
                guidOut[i][6] = packet.Translator.ReadBit();
                guidOut[i][5] = packet.Translator.ReadBit();
                guidOut[i][4] = packet.Translator.ReadBit();
                guidOut[i][3] = packet.Translator.ReadBit();
                guidOut[i][1] = packet.Translator.ReadBit();
                guidOut[i][2] = packet.Translator.ReadBit();
            }

            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < count[i]; ++j)
                {
                    packet.Translator.ParseBitStream(guidIn[i][j], 0, 1, 4, 7, 5, 6, 3, 2);
                    packet.Translator.WriteGuid("Guid", guidIn[i][j], i, j);
                }

                packet.Translator.ReadXORByte(guidOut[i], 5);

                packet.Translator.ReadInt32("Flag", i); // not 0 for playerachievements and raidencounters, 1 - sticky
                packet.Translator.ReadInt32("Entry (item/achiev/encounter)", i);
                packet.Translator.ReadInt32("Unk Int32 2", i); // always 0

                packet.Translator.ReadXORByte(guidOut[i], 7);
                packet.Translator.ReadXORByte(guidOut[i], 6);
                packet.Translator.ReadXORByte(guidOut[i], 2);
                packet.Translator.ReadXORByte(guidOut[i], 3);
                packet.Translator.ReadXORByte(guidOut[i], 0);
                packet.Translator.ReadXORByte(guidOut[i], 4);
                packet.Translator.ReadXORByte(guidOut[i], 1);

                packet.Translator.ReadInt32("News Id", i);
                packet.Translator.ReadInt32E<GuildNewsType>("News Type", i);
                packet.Translator.ReadPackedTime("Time", i);

                packet.Translator.WriteGuid("Guid", guidOut[i], i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_REWARD_LIST)]
        public static void HandleGuildRewardsList434(Packet packet)
        {
            var size = packet.Translator.ReadBits("Size", 21);

            for (var i = 0; i < size; ++i)
            {
                packet.Translator.ReadUInt32E<ReputationRank>("Faction Standing", i);
                packet.Translator.ReadUInt32E<RaceMask>("Race mask", i);
                packet.Translator.ReadUInt32<ItemId>("Item Id", i);
                packet.Translator.ReadUInt64("Price", i);
                packet.Translator.ReadUInt32("Unk UInt32", i);
                packet.Translator.ReadInt32<AchievementId>("Achievement Id", i);
            }

            packet.Translator.ReadTime("Time");
        }

        [Parser(Opcode.SMSG_GUILD_BANK_TEXT_QUERY_RESULT)]
        public static void HandleGuildQueryBankText434(Packet packet)
        {
            packet.Translator.ReadUInt32("Tab Id");
            packet.Translator.ReadWoWString("Text", packet.Translator.ReadBits(14));
        }

        [Parser(Opcode.CMSG_GUILD_BANK_SET_TAB_TEXT)]
        public static void HandleGuildSetBankText434(Packet packet)
        {
            packet.Translator.ReadUInt32("Tab Id");
            packet.Translator.ReadWoWString("Tab Text", packet.Translator.ReadBits(14));
        }

        [Parser(Opcode.SMSG_GUILD_EVENT_LOG_QUERY_RESULTS)]
        public static void HandleGuildEventLogQuery434(Packet packet)
        {
            var count = packet.Translator.ReadBits(23);
            var guid1 = new byte[count][];
            var guid2 = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                guid1[i] = new byte[8];
                guid2[i] = new byte[8];

                guid1[i][2] = packet.Translator.ReadBit();
                guid1[i][4] = packet.Translator.ReadBit();
                guid2[i][7] = packet.Translator.ReadBit();
                guid2[i][6] = packet.Translator.ReadBit();
                guid1[i][3] = packet.Translator.ReadBit();
                guid2[i][3] = packet.Translator.ReadBit();
                guid2[i][5] = packet.Translator.ReadBit();
                guid1[i][7] = packet.Translator.ReadBit();
                guid1[i][5] = packet.Translator.ReadBit();
                guid1[i][0] = packet.Translator.ReadBit();
                guid2[i][4] = packet.Translator.ReadBit();
                guid2[i][2] = packet.Translator.ReadBit();
                guid2[i][0] = packet.Translator.ReadBit();
                guid2[i][1] = packet.Translator.ReadBit();
                guid1[i][1] = packet.Translator.ReadBit();
                guid1[i][6] = packet.Translator.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORByte(guid2[i], 3);
                packet.Translator.ReadXORByte(guid2[i], 2);
                packet.Translator.ReadXORByte(guid2[i], 5);

                packet.Translator.ReadByte("Rank", i);

                packet.Translator.ReadXORByte(guid2[i], 4);
                packet.Translator.ReadXORByte(guid1[i], 0);
                packet.Translator.ReadXORByte(guid1[i], 4);

                packet.Translator.ReadUInt32("Time ago", i);

                packet.Translator.ReadXORByte(guid1[i], 7);
                packet.Translator.ReadXORByte(guid1[i], 3);
                packet.Translator.ReadXORByte(guid2[i], 0);
                packet.Translator.ReadXORByte(guid2[i], 6);
                packet.Translator.ReadXORByte(guid2[i], 7);
                packet.Translator.ReadXORByte(guid1[i], 5);

                packet.Translator.ReadByteE<GuildEventLogType>("Type", i);

                packet.Translator.ReadXORByte(guid2[i], 1);
                packet.Translator.ReadXORByte(guid1[i], 2);
                packet.Translator.ReadXORByte(guid1[i], 6);
                packet.Translator.ReadXORByte(guid1[i], 1);

                packet.Translator.WriteGuid("GUID1", guid1[i], i);
                packet.Translator.WriteGuid("GUID2", guid2[i], i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_BANK_LOG_QUERY_RESULTS)]
        public static void HandleGuildBankLogQueryResult434(Packet packet)
        {
            var hasWeekCashflow = packet.Translator.ReadBit("Has Cash flow Perk");
            var size = packet.Translator.ReadBits("Size", 23);
            var hasMoney = new byte[size];
            var tabChanged = new byte[size];
            var stackCount = new byte[size];
            var hasItem = new byte[size];
            var guid = new byte[size][];
            for (var i = 0; i < size; i++)
            {
                guid[i] = new byte[8];
                hasMoney[i] = packet.Translator.ReadBit();
                guid[i][4] = packet.Translator.ReadBit();
                guid[i][1] = packet.Translator.ReadBit();
                hasItem[i] = packet.Translator.ReadBit();
                stackCount[i] = packet.Translator.ReadBit();
                guid[i][2] = packet.Translator.ReadBit();
                guid[i][5] = packet.Translator.ReadBit();
                guid[i][3] = packet.Translator.ReadBit();
                guid[i][6] = packet.Translator.ReadBit();
                guid[i][0] = packet.Translator.ReadBit();
                tabChanged[i] = packet.Translator.ReadBit(); //unk
                guid[i][7] = packet.Translator.ReadBit();
            }
            for (var i = 0; i < size; i++)
            {
                packet.Translator.ReadXORByte(guid[i], 6);
                packet.Translator.ReadXORByte(guid[i], 1);
                packet.Translator.ReadXORByte(guid[i], 5);
                if (stackCount[i] != 0)
                    packet.Translator.ReadUInt32("Stack count", i);
                packet.Translator.ReadByteE<GuildBankEventLogType>("Bank Log Event Type", i);
                packet.Translator.ReadXORByte(guid[i], 2);
                packet.Translator.ReadXORByte(guid[i], 4);
                packet.Translator.ReadXORByte(guid[i], 0);
                packet.Translator.ReadXORByte(guid[i], 7);
                packet.Translator.ReadXORByte(guid[i], 3);

                if (hasItem[i] != 0)
                    packet.Translator.ReadUInt32("Item Entry", i);

                packet.Translator.ReadInt32("Time", i);

                if (hasMoney[i] != 0)
                    packet.Translator.ReadInt64("Money", i);

                if (tabChanged[i] != 0)
                    packet.Translator.ReadByte("Dest tab id", i);

                packet.Translator.WriteGuid("Guid", guid[i], i);
            }
            packet.Translator.ReadUInt32("Tab Id");

            if (hasWeekCashflow)
                packet.Translator.ReadInt64("Week Cash Flow Contribution");
        }

        [Parser(Opcode.SMSG_GUILD_CRITERIA_DATA)]
        public static void HandleGuildCriteriaData434(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 21);
            var counter = new byte[count][];
            var guid = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                counter[i] = new byte[8];
                guid[i] = new byte[8];

                counter[i][4] = packet.Translator.ReadBit();
                counter[i][1] = packet.Translator.ReadBit();
                guid[i][2] = packet.Translator.ReadBit();
                counter[i][3] = packet.Translator.ReadBit();
                guid[i][1] = packet.Translator.ReadBit();
                counter[i][5] = packet.Translator.ReadBit();
                counter[i][0] = packet.Translator.ReadBit();
                guid[i][3] = packet.Translator.ReadBit();
                counter[i][2] = packet.Translator.ReadBit();
                guid[i][7] = packet.Translator.ReadBit();
                guid[i][5] = packet.Translator.ReadBit();
                guid[i][0] = packet.Translator.ReadBit();
                counter[i][6] = packet.Translator.ReadBit();
                guid[i][6] = packet.Translator.ReadBit();
                counter[i][7] = packet.Translator.ReadBit();
                guid[i][4] = packet.Translator.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORByte(guid[i], 5);

                packet.Translator.ReadTime("Unk time 1", i);

                packet.Translator.ReadXORByte(counter[i], 3);
                packet.Translator.ReadXORByte(counter[i], 7);

                packet.Translator.ReadTime("Unk time 2", i);

                packet.Translator.ReadXORByte(counter[i], 6);
                packet.Translator.ReadXORByte(guid[i], 4);
                packet.Translator.ReadXORByte(guid[i], 1);
                packet.Translator.ReadXORByte(counter[i], 4);
                packet.Translator.ReadXORByte(guid[i], 3);
                packet.Translator.ReadXORByte(counter[i], 0);
                packet.Translator.ReadXORByte(guid[i], 2);
                packet.Translator.ReadXORByte(counter[i], 1);
                packet.Translator.ReadXORByte(guid[i], 6);

                packet.Translator.ReadTime("Criteria Date", i);
                packet.Translator.ReadUInt32("Criteria id", i);

                packet.Translator.ReadXORByte(counter[i], 5);

                packet.Translator.ReadUInt32("Unk", i);

                packet.Translator.ReadXORByte(guid[i], 7);
                packet.Translator.ReadXORByte(counter[i], 2);
                packet.Translator.ReadXORByte(guid[i], 0);

                packet.Translator.WriteGuid("Criteria GUID", guid[i], i);
                packet.AddValue("Criteria counter", BitConverter.ToUInt64(counter[i], 0), i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_MEMBERS_FOR_RECIPE)]
        public static void HandleGuildQueryMembersForRecipe(Packet packet)
        {
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadUInt32("Skill ID");
            packet.Translator.ReadUInt32("Skill Value");

            var guid = packet.Translator.StartBitStream(4, 1, 0, 3, 6, 7, 5, 2);
            packet.Translator.ParseBitStream(guid, 1, 6, 5, 0, 3, 7, 2, 4);
            packet.Translator.WriteGuid("Guild GUID", guid);
        }

        [Parser(Opcode.SMSG_GUILD_MEMBERS_FOR_RECIPE)]
        public static void HandleGuildMembersForRecipe(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 26);
            var guid = new byte[count][];

            for (int i = 0; i < count; ++i)
                guid[i] = packet.Translator.StartBitStream(2, 3, 1, 6, 0, 7, 4, 5);

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ParseBitStream(guid[i], 1, 5, 6, 7, 2, 3, 0, 4);
                packet.Translator.WriteGuid("Player GUID", guid[i], i);
            }

            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadUInt32("Skill ID");
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_MEMBER_RECIPES)]
        public static void HandleGuildQueryMemberRecipes(Packet packet)
        {
            var guildGuid = new byte[8];
            var guid = new byte[8];

            packet.Translator.ReadInt32("Skill ID");

            guid[2] = packet.Translator.ReadBit();
            guildGuid[1] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guildGuid[0] = packet.Translator.ReadBit();
            guildGuid[6] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guildGuid[4] = packet.Translator.ReadBit();
            guildGuid[3] = packet.Translator.ReadBit();
            guildGuid[7] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guildGuid[5] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guildGuid[2] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guildGuid, 4);
            packet.Translator.ReadXORByte(guildGuid, 2);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guildGuid, 7);
            packet.Translator.ReadXORByte(guildGuid, 3);
            packet.Translator.ReadXORByte(guildGuid, 1);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guildGuid, 0);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guildGuid, 5);
            packet.Translator.ReadXORByte(guildGuid, 6);
            packet.Translator.ReadXORByte(guid, 5);

            packet.Translator.WriteGuid("Guild GUID", guildGuid);
            packet.Translator.WriteGuid("Player GUID", guid);
        }

        [Parser(Opcode.SMSG_GUILD_MEMBER_RECIPES)]
        public static void HandleGuildMemberRecipes(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(0, 3, 7, 4, 6, 2, 1, 5);

            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);

            packet.Translator.ReadInt32("Skill Value");
            packet.Translator.ReadBytes("Skill Bits", 300);

            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 2);

            packet.Translator.ReadInt32("Skill ID");
            packet.Translator.ReadInt32("Tradeskill Level");

            packet.Translator.WriteGuid("Player GUID", guid);
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_RECIPES)]
        public static void HandleGuildQueryRecipes(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(5, 6, 1, 4, 2, 7, 0, 3);
            packet.Translator.ParseBitStream(guid, 3, 1, 0, 5, 4, 2, 6, 7);
            packet.Translator.WriteGuid("Guild GUID", guid);
        }

        [Parser(Opcode.SMSG_GUILD_KNOWN_RECIPES)]
        public static void HandleGuildRecipes(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 16);

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("Skill Id", i);
                packet.Translator.ReadBytes("Skill Bits", 300, i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_CHALLENGE_UPDATE)]
        public static void HandleGuildChallengeUpdated(Packet packet)
        {
            for (int i = 0; i < 4; ++i)
                packet.Translator.ReadInt32("Guild Experience Reward", i);

            for (int i = 0; i < 4; ++i)
                packet.Translator.ReadInt32("Completion Gold Reward", i);

            for (int i = 0; i < 4; ++i)
                packet.Translator.ReadInt32("Total Count", i);

            for (int i = 0; i < 4; ++i)
                packet.Translator.ReadInt32("Gold Reward Unk 2", i); // requires perk Cash Flow?

            for (int i = 0; i < 4; ++i)
                packet.Translator.ReadInt32("Current Count", i);
        }

        [Parser(Opcode.SMSG_GUILD_CHALLENGE_COMPLETED)]
        public static void HandleGuildChallengeCompleted(Packet packet)
        {
            packet.Translator.ReadInt32("Index"); // not confirmed
            packet.Translator.ReadInt32("Gold Reward");
            packet.Translator.ReadInt32("Current Count");
            packet.Translator.ReadInt32("Guild Experience Reward");
            packet.Translator.ReadInt32("Total Count");
        }

        [Parser(Opcode.SMSG_GUILD_REPUTATION_REACTION_CHANGED)]
        public static void HandleGuildReputationReactionChanged(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(1, 6, 2, 4, 0, 3, 7, 5);
            packet.Translator.ParseBitStream(guid, 4, 6, 5, 7, 2, 0, 3, 1);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GUILD_ADD_RANK)]
        public static void HandleGuildAddRank(Packet packet)
        {
            packet.Translator.ReadUInt32("Rank ID");
            var count = packet.Translator.ReadBits(7);
            packet.Translator.ReadWoWString("Name", count);
        }

        [Parser(Opcode.CMSG_GUILD_ACHIEVEMENT_MEMBERS)]
        public static void HandleGuildAchievementMembers(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];

            packet.Translator.ReadInt32("Achievement Id");

            guid[0] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 6);

            packet.Translator.WriteGuid("Guild Guid", guid);
            packet.Translator.WriteGuid("Player Guid", guid2);

        }

        [Parser(Opcode.SMSG_GUILD_ACHIEVEMENT_MEMBERS)]
        public static void HandleGuildAchievementMembersResponse(Packet packet)
        {
            var guid = new byte[8];


            guid[3] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();

            var count = packet.Translator.ReadBits("Player Count", 26);
            var guid2 = new byte[count][];
            for (var i = 0; i < count; i++)
                guid2[i] = packet.Translator.StartBitStream(3, 1, 4, 5, 7, 0, 6, 2);

            guid[2] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 5);

            for (var i = 0; i < count; i++)
            {
                packet.Translator.ParseBitStream(guid2[i], 1, 5, 7, 0, 6, 4, 3, 2);
                packet.Translator.WriteGuid("Player Guid", guid2[i], i);
            }

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.ReadInt32<AchievementId>("Achievement Id");

            packet.Translator.ReadXORByte(guid, 1);

            packet.Translator.WriteGuid("Guild Guid", guid);

        }

        [Parser(Opcode.SMSG_GUILD_ACHIEVEMENT_EARNED)]
        public static void HandleGuildAchievementEarned(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(3, 1, 0, 7, 4, 6, 2, 5);

            packet.Translator.ReadXORByte(guid, 2);

            packet.Translator.ReadPackedTime("Time");

            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 3);

            packet.Translator.ReadInt32<AchievementId>("Achievement Id");

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 6);

            packet.Translator.WriteGuid("Guild Guid", guid);
        }
    }
}
