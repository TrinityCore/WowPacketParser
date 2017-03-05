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
            using (var packet2 = packet.Inflate(packet.ReadInt32()))
            {
                HandleGuildRoster434(packet2);
            }
        }

        [Parser(Opcode.CMSG_GUILD_INVITE)]
        public static void HandleGuildInviteByName(Packet packet)
        {
            var count = packet.ReadBits("String Length", 7);
            packet.ReadWoWString("Name", count);
        }

        [Parser(Opcode.SMSG_GUILD_RANKS)]
        public static void HandleGuildRankServer434(Packet packet)
        {
            const int guildBankMaxTabs = 8;
            var count = packet.ReadBits("Count", 18);
            var length = new int[count];
            for (var i = 0; i < count; ++i)
                length[i] = (int)packet.ReadBits(7);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Creation Order", i);
                for (var j = 0; j < guildBankMaxTabs; ++j)
                {
                    packet.ReadInt32("Tab Slots", i, j);
                    packet.ReadInt32E<GuildBankRightsFlag>("Tab Rights", i, j);
                }
                packet.ReadInt32("Gold Per Day", i);
                packet.ReadInt32E<GuildRankRightsFlag>("Rights", i);
                packet.ReadWoWString("Name", length[i], i);
                packet.ReadInt32("Rights Order", i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_DELETE_RANK)]
        public static void HandleGuildDelRank434(Packet packet)
        {
            packet.ReadUInt32("Rank Id");
        }

        [Parser(Opcode.CMSG_GUILD_SWITCH_RANK)]
        public static void HandleGuildSwitchRank434(Packet packet)
        {
            packet.ReadUInt32("Rank Id");
            packet.ReadBit("Direction");
        }

        [Parser(Opcode.SMSG_GUILD_SEND_RANK_CHANGE)]
        public static void HandleGuildRanksUpdate(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid1[7] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            packet.ReadBit("Promote?");
            guid1[5] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid1[4] = packet.ReadBit();

            packet.ReadInt32("Rank Index");

            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 4);
            packet.WriteGuid("Guid 1", guid1);
            packet.WriteGuid("Guid 2", guid2);
        }

        [Parser(Opcode.CMSG_GUILD_SET_RANK_PERMISSIONS)]
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

        [Parser(Opcode.SMSG_GUILD_PARTY_STATE)]
        public static void HandleGuildPartyStateResponse434(Packet packet)
        {
            packet.ReadBit("Is guild group");
            packet.ReadSingle("Guild XP multiplier");
            packet.ReadUInt32("Current guild members");
            packet.ReadUInt32("Needed guild members");
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_PARTY_STATE)]
        public static void HandleGuildUpdatePartyState434(Packet packet)
        {
            var guid = packet.StartBitStream(0, 6, 7, 3, 5, 1, 2, 4);
            packet.ParseBitStream(guid, 6, 3, 2, 1, 5, 0, 7, 4);
            packet.WriteGuid("Guild Guid", guid);
        }

        [Parser(Opcode.CMSG_GUILD_ASSIGN_MEMBER_RANK)]
        public static void HandleGuildAssignMemberRank(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.ReadUInt32("Rank");

            guid1[1] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[5] = packet.ReadBit();

            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 7);

            packet.WriteGuid("GUID 1", guid1);
            packet.WriteGuid("GUID 2", guid2);
        }

        [Parser(Opcode.CMSG_GUILD_GET_ROSTER)]
        public static void HandleGuildRosterRequest434(Packet packet)
        {
            // The client does not write these 2 guids properly.
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            guid2[2] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid1[7] = packet.ReadBit();

            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 6);
            // packet.WriteGuid("Guid1", guid1);
            // packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER)]
        public static void HandleGuildRoster434(Packet packet)
        {
            var motdLength = packet.ReadBits(11);
            var size = packet.ReadBits(18);

            var guid = new byte[size][];
            var nameLength = new uint[size];
            var publicLength = new uint[size];
            var officerLength = new uint[size];

            for (var i = 0; i < size; ++i)
            {
                guid[i] = new byte[8];
                guid[i][3] = packet.ReadBit();
                guid[i][4] = packet.ReadBit();
                packet.ReadBit("Has Authenticator", i);
                packet.ReadBit("Can SoR", i);
                publicLength[i] = packet.ReadBits(8);
                officerLength[i] = packet.ReadBits(8);
                guid[i][0] = packet.ReadBit();
                nameLength[i] = packet.ReadBits(7);
                guid[i][1] = packet.ReadBit();
                guid[i][2] = packet.ReadBit();
                guid[i][6] = packet.ReadBit();
                guid[i][5] = packet.ReadBit();
                guid[i][7] = packet.ReadBit();
            }
            var infoLength = packet.ReadBits(12);

            for (var i = 0; i < size; ++i)
            {
                packet.ReadByteE<Class>("Member Class", i);
                packet.ReadInt32("Guild Reputation", i);

                packet.ReadXORByte(guid[i], 0);

                packet.ReadUInt64("Week activity", i);
                packet.ReadUInt32("Member Rank", i);
                packet.ReadUInt32("Member Achievement Points", i);
                for (var j = 0; j < 2; ++j)
                {
                    var rank = packet.ReadUInt32();
                    var value = packet.ReadUInt32();
                    var id = packet.ReadUInt32();
                    packet.AddValue("Profession", string.Format("Id {0} - Value {1} - Rank {2}", id, value, rank), i, j);
                }

                packet.ReadXORByte(guid[i], 2);

                packet.ReadByteE<GuildMemberFlag>("Member Flags", i);
                packet.ReadInt32<ZoneId>("Zone Id", i);
                packet.ReadUInt64("Total activity", i);

                packet.ReadXORByte(guid[i], 7);

                packet.ReadUInt32("Remaining guild week Rep", i);
                packet.ReadWoWString("Public note", publicLength[i], i);

                if (guid[i][3] != 0)
                    guid[i][3] ^= packet.ReadByte();

                packet.ReadByte("Member Level", i);
                packet.ReadInt32("Unk 2", i);

                packet.ReadXORByte(guid[i], 5);
                packet.ReadXORByte(guid[i], 4);

                packet.ReadByte("Unk Byte", i);

                packet.ReadXORByte(guid[i], 1);

                packet.ReadSingle("Last online", i);
                packet.ReadWoWString("Officer note", officerLength[i], i);

                if (guid[i][6] != 0)
                    guid[i][6] ^= packet.ReadByte();

                var name = packet.ReadWoWString("Name", nameLength[i], i);
                packet.WriteGuid("Guid", guid[i], i);
                StoreGetters.AddName(new WowGuid64(BitConverter.ToUInt64(guid[i], 0)), name);
            }

            packet.ReadWoWString("Guild Info", infoLength);
            packet.ReadWoWString("MOTD", motdLength);
            packet.ReadUInt32("Accounts In Guild");
            packet.ReadUInt32("Weekly Reputation Cap");
            packet.ReadPackedTime("CreationTime");
            packet.ReadUInt32("Unk Uint32 4");
        }

        [Parser(Opcode.CMSG_GUILD_OFFICER_REMOVE_MEMBER)]
        public static void HandleGuildRemove434(Packet packet)
        {
            var guid = packet.StartBitStream(6, 5, 4, 0, 1, 3, 7, 2);
            packet.ParseBitStream(guid, 2, 6, 5, 7, 1, 4, 3, 0);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_GUILD_INVITE)]
        public static void HandleGuildInvite434(Packet packet)
        {
            var newGuildGuid = new byte[8];
            var oldGuildGuid = new byte[8];

            packet.ReadInt32("Guild Level");
            packet.ReadInt32("Border Style");
            packet.ReadInt32("Border Color");
            packet.ReadInt32("Emblem Texture");
            packet.ReadInt32("Emblem Background Color");
            packet.ReadInt32("Emblem Color");

            newGuildGuid[3] = packet.ReadBit();
            newGuildGuid[2] = packet.ReadBit();

            var oldGuildNameLength = packet.ReadBits(8);

            newGuildGuid[1] = packet.ReadBit();
            oldGuildGuid[6] = packet.ReadBit();
            oldGuildGuid[4] = packet.ReadBit();
            oldGuildGuid[1] = packet.ReadBit();
            oldGuildGuid[5] = packet.ReadBit();
            oldGuildGuid[7] = packet.ReadBit();
            oldGuildGuid[2] = packet.ReadBit();
            newGuildGuid[7] = packet.ReadBit();
            newGuildGuid[0] = packet.ReadBit();
            newGuildGuid[6] = packet.ReadBit();

            var newGuildNameLength = packet.ReadBits(8);

            oldGuildGuid[3] = packet.ReadBit();
            oldGuildGuid[0] = packet.ReadBit();
            newGuildGuid[5] = packet.ReadBit();

            var inviterNameLength = packet.ReadBits(7);

            newGuildGuid[4] = packet.ReadBit();

            packet.ReadXORByte(newGuildGuid, 1);
            packet.ReadXORByte(oldGuildGuid, 3);
            packet.ReadXORByte(newGuildGuid, 6);
            packet.ReadXORByte(oldGuildGuid, 2);
            packet.ReadXORByte(oldGuildGuid, 1);
            packet.ReadXORByte(newGuildGuid, 0);

            packet.ReadWoWString("Old Guild Name", oldGuildNameLength);

            packet.ReadXORByte(newGuildGuid, 7);
            packet.ReadXORByte(newGuildGuid, 2);

            packet.ReadWoWString("Inviter Name", inviterNameLength);

            packet.ReadXORByte(oldGuildGuid, 7);
            packet.ReadXORByte(oldGuildGuid, 6);
            packet.ReadXORByte(oldGuildGuid, 5);
            packet.ReadXORByte(oldGuildGuid, 0);
            packet.ReadXORByte(newGuildGuid, 4);

            packet.ReadWoWString("New Guild Name", newGuildNameLength);

            packet.ReadXORByte(newGuildGuid, 5);
            packet.ReadXORByte(newGuildGuid, 3);
            packet.ReadXORByte(oldGuildGuid, 4);

            packet.WriteGuid("New Guild Guid", newGuildGuid);
            packet.WriteGuid("Old Guild Guid", oldGuildGuid);
        }

        [Parser(Opcode.CMSG_GUILD_MOTD)]
        public static void HandleGuildMOTD434(Packet packet)
        {
            packet.ReadWoWString("MOTD", (int)packet.ReadBits(11));
        }

        [Parser(Opcode.CMSG_GUILD_INFO_TEXT)]
        public static void HandleGuildInfo434(Packet packet)
        {
            packet.ReadWoWString("Text", (int)packet.ReadBits(12));
        }

        [Parser(Opcode.CMSG_GUILD_CHANGE_NAME_REQUEST)]
        public static void HandleGuildNameChange(Packet packet)
        {
            packet.ReadWoWString("New Name", (int)packet.ReadBits(8));
        }

        [Parser(Opcode.CMSG_GUILD_SET_NOTE)]
        public static void HandleGuildSetNote434(Packet packet)
        {
            var guid = new byte[8];
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            packet.ReadBit("Public");
            guid[6] = packet.ReadBit();
            var len = packet.ReadBits("Note Length", 8);
            guid[2] = packet.ReadBit();

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadWoWString("Note", len);
            packet.ReadXORByte(guid, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_GUILD_BANK_QUERY_RESULTS)]
        public static void HandleGuildBankList434(Packet packet)
        {
            packet.ReadBit("Unk");
            var count = packet.ReadBits("Item count", 20);
            var count2 = packet.ReadBits("Tab count", 22);

            var icons = new uint[count2];
            var texts = new uint[count2];
            var enchants = new uint[count];

            for (var i = 0; i < count; ++i)
                enchants[i] = packet.ReadBits(24); // Number of Enchantments ?

            for (var i = 0; i < count2; ++i)
            {
                icons[i] = packet.ReadBits(9);
                texts[i] = packet.ReadBits(7);
            }

            for (var i = 0; i < count2; ++i)
            {
                packet.ReadWoWString("Icon", icons[i], i);
                packet.ReadUInt32("Index", i);
                packet.ReadWoWString("Text", texts[i], i);
            }

            packet.ReadUInt64("Money");

            for (var i = 0; i < count; ++i)
            {
                for (var j = 0; j < enchants[i]; ++j)
                {
                    packet.ReadUInt32("Enchantment Slot Id?", i, j);
                    packet.ReadUInt32("Enchantment Id?", i, j);
                }
                packet.ReadUInt32("Unk UInt32 1", i); // Only seen 0
                packet.ReadUInt32("Unk UInt32 2", i); // Only seen 0
                packet.ReadUInt32("Unk UInt32 3", i); // Only seen 0
                packet.ReadUInt32("Stack Count", i);
                packet.ReadUInt32("Slot Id", i);
                packet.ReadUInt32E<UnknownFlags>("Unk mask", i);
                packet.ReadInt32<ItemId>("Item Entry", i);
                packet.ReadInt32("Random Item Property Id", i);
                packet.ReadUInt32("Spell Charges", i);
                packet.ReadUInt32("Item Suffix Factor", i);
            }
            packet.ReadUInt32("Tab");
            packet.ReadInt32("Remaining Withdraw");
        }

        [Parser(Opcode.CMSG_GUILD_REQUEST_MAX_DAILY_XP)]
        public static void HandleGuildRequestMaxDailyXP434(Packet packet)
        {
            var guid = packet.StartBitStream(0, 3, 5, 1, 4, 6, 7, 2);
            packet.ParseBitStream(guid, 7, 4, 3, 5, 1, 2, 6, 0);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_XP)]
        public static void HandleRequestGuildXP(Packet packet)
        {
            var guid = packet.StartBitStream(2, 1, 0, 5, 4, 7, 6, 3);
            packet.ParseBitStream(guid, 7, 2, 3, 6, 1, 5, 0, 4);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_NEWS)]
        public static void HandleGuildQueryNews434(Packet packet)
        {
            var guid = packet.StartBitStream(4, 2, 6, 3, 5, 0, 1, 7);
            packet.ParseBitStream(guid, 4, 1, 5, 6, 0, 3, 7, 2);
            packet.WriteGuid("GUID", guid);
        }


        [Parser(Opcode.CMSG_GUILD_GET_RANKS)]
        public static void HandleGuildRanks434(Packet packet)
        {
            var guid = packet.StartBitStream(2, 3, 0, 6, 4, 7, 5, 1);
            packet.ParseBitStream(guid, 3, 4, 5, 7, 1, 0, 6, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_GUILD_NEWS_UPDATE)]
        public static void HandleGuildNewsUpdate434(Packet packet)
        {
            var size = packet.ReadBits("Size", 21);

            var guidOut = new byte[size][];
            var guidIn = new byte[size][][];
            var count = new uint[size];

            for (int i = 0; i < size; ++i)
            {
                count[i] = packet.ReadBits("Count", 26);

                guidOut[i] = new byte[8];
                guidOut[i][7] = packet.ReadBit(); // 55

                guidIn[i] = new byte[count[i]][];
                for (int j = 0; j < count[i]; ++j)
                    guidIn[i][j] = packet.StartBitStream(7, 1, 5, 3, 4, 6, 0, 2);

                guidOut[i][0] = packet.ReadBit(); // 48
                guidOut[i][6] = packet.ReadBit();
                guidOut[i][5] = packet.ReadBit();
                guidOut[i][4] = packet.ReadBit();
                guidOut[i][3] = packet.ReadBit();
                guidOut[i][1] = packet.ReadBit();
                guidOut[i][2] = packet.ReadBit();
            }

            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < count[i]; ++j)
                {
                    packet.ParseBitStream(guidIn[i][j], 0, 1, 4, 7, 5, 6, 3, 2);
                    packet.WriteGuid("Guid", guidIn[i][j], i, j);
                }

                packet.ReadXORByte(guidOut[i], 5);

                packet.ReadInt32("Flag", i); // not 0 for playerachievements and raidencounters, 1 - sticky
                packet.ReadInt32("Entry (item/achiev/encounter)", i);
                packet.ReadInt32("Unk Int32 2", i); // always 0

                packet.ReadXORByte(guidOut[i], 7);
                packet.ReadXORByte(guidOut[i], 6);
                packet.ReadXORByte(guidOut[i], 2);
                packet.ReadXORByte(guidOut[i], 3);
                packet.ReadXORByte(guidOut[i], 0);
                packet.ReadXORByte(guidOut[i], 4);
                packet.ReadXORByte(guidOut[i], 1);

                packet.ReadInt32("News Id", i);
                packet.ReadInt32E<GuildNewsType>("News Type", i);
                packet.ReadPackedTime("Time", i);

                packet.WriteGuid("Guid", guidOut[i], i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_REWARD_LIST)]
        public static void HandleGuildRewardsList434(Packet packet)
        {
            var size = packet.ReadBits("Size", 21);

            for (var i = 0; i < size; ++i)
            {
                packet.ReadUInt32E<ReputationRank>("Faction Standing", i);
                packet.ReadUInt32E<RaceMask>("Race mask", i);
                packet.ReadUInt32<ItemId>("Item Id", i);
                packet.ReadUInt64("Price", i);
                packet.ReadUInt32("Unk UInt32", i);
                packet.ReadInt32<AchievementId>("Achievement Id", i);
            }

            packet.ReadTime("Time");
        }

        [Parser(Opcode.SMSG_GUILD_BANK_TEXT_QUERY_RESULT)]
        public static void HandleGuildQueryBankText434(Packet packet)
        {
            packet.ReadUInt32("Tab Id");
            packet.ReadWoWString("Text", packet.ReadBits(14));
        }

        [Parser(Opcode.CMSG_GUILD_BANK_SET_TAB_TEXT)]
        public static void HandleGuildSetBankText434(Packet packet)
        {
            packet.ReadUInt32("Tab Id");
            packet.ReadWoWString("Tab Text", packet.ReadBits(14));
        }

        [Parser(Opcode.SMSG_GUILD_EVENT_LOG_QUERY_RESULTS)]
        public static void HandleGuildEventLogQuery434(Packet packet)
        {
            var count = packet.ReadBits(23);
            var guid1 = new byte[count][];
            var guid2 = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                guid1[i] = new byte[8];
                guid2[i] = new byte[8];

                guid1[i][2] = packet.ReadBit();
                guid1[i][4] = packet.ReadBit();
                guid2[i][7] = packet.ReadBit();
                guid2[i][6] = packet.ReadBit();
                guid1[i][3] = packet.ReadBit();
                guid2[i][3] = packet.ReadBit();
                guid2[i][5] = packet.ReadBit();
                guid1[i][7] = packet.ReadBit();
                guid1[i][5] = packet.ReadBit();
                guid1[i][0] = packet.ReadBit();
                guid2[i][4] = packet.ReadBit();
                guid2[i][2] = packet.ReadBit();
                guid2[i][0] = packet.ReadBit();
                guid2[i][1] = packet.ReadBit();
                guid1[i][1] = packet.ReadBit();
                guid1[i][6] = packet.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guid2[i], 3);
                packet.ReadXORByte(guid2[i], 2);
                packet.ReadXORByte(guid2[i], 5);

                packet.ReadByte("Rank", i);

                packet.ReadXORByte(guid2[i], 4);
                packet.ReadXORByte(guid1[i], 0);
                packet.ReadXORByte(guid1[i], 4);

                packet.ReadUInt32("Time ago", i);

                packet.ReadXORByte(guid1[i], 7);
                packet.ReadXORByte(guid1[i], 3);
                packet.ReadXORByte(guid2[i], 0);
                packet.ReadXORByte(guid2[i], 6);
                packet.ReadXORByte(guid2[i], 7);
                packet.ReadXORByte(guid1[i], 5);

                packet.ReadByteE<GuildEventLogType>("Type", i);

                packet.ReadXORByte(guid2[i], 1);
                packet.ReadXORByte(guid1[i], 2);
                packet.ReadXORByte(guid1[i], 6);
                packet.ReadXORByte(guid1[i], 1);

                packet.WriteGuid("GUID1", guid1[i], i);
                packet.WriteGuid("GUID2", guid2[i], i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_BANK_LOG_QUERY_RESULTS)]
        public static void HandleGuildBankLogQueryResult434(Packet packet)
        {
            var hasWeekCashflow = packet.ReadBit("Has Cash flow Perk");
            var size = packet.ReadBits("Size", 23);
            var hasMoney = new byte[size];
            var tabChanged = new byte[size];
            var stackCount = new byte[size];
            var hasItem = new byte[size];
            var guid = new byte[size][];
            for (var i = 0; i < size; i++)
            {
                guid[i] = new byte[8];
                hasMoney[i] = packet.ReadBit();
                guid[i][4] = packet.ReadBit();
                guid[i][1] = packet.ReadBit();
                hasItem[i] = packet.ReadBit();
                stackCount[i] = packet.ReadBit();
                guid[i][2] = packet.ReadBit();
                guid[i][5] = packet.ReadBit();
                guid[i][3] = packet.ReadBit();
                guid[i][6] = packet.ReadBit();
                guid[i][0] = packet.ReadBit();
                tabChanged[i] = packet.ReadBit(); //unk
                guid[i][7] = packet.ReadBit();
            }
            for (var i = 0; i < size; i++)
            {
                packet.ReadXORByte(guid[i], 6);
                packet.ReadXORByte(guid[i], 1);
                packet.ReadXORByte(guid[i], 5);
                if (stackCount[i] != 0)
                    packet.ReadUInt32("Stack count", i);
                packet.ReadByteE<GuildBankEventLogType>("Bank Log Event Type", i);
                packet.ReadXORByte(guid[i], 2);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadXORByte(guid[i], 0);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadXORByte(guid[i], 3);

                if (hasItem[i] != 0)
                    packet.ReadUInt32("Item Entry", i);

                packet.ReadInt32("Time", i);

                if (hasMoney[i] != 0)
                    packet.ReadInt64("Money", i);

                if (tabChanged[i] != 0)
                    packet.ReadByte("Dest tab id", i);

                packet.WriteGuid("Guid", guid[i], i);
            }
            packet.ReadUInt32("Tab Id");

            if (hasWeekCashflow)
                packet.ReadInt64("Week Cash Flow Contribution");
        }

        [Parser(Opcode.SMSG_GUILD_CRITERIA_DATA)]
        public static void HandleGuildCriteriaData434(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);
            var counter = new byte[count][];
            var guid = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                counter[i] = new byte[8];
                guid[i] = new byte[8];

                counter[i][4] = packet.ReadBit();
                counter[i][1] = packet.ReadBit();
                guid[i][2] = packet.ReadBit();
                counter[i][3] = packet.ReadBit();
                guid[i][1] = packet.ReadBit();
                counter[i][5] = packet.ReadBit();
                counter[i][0] = packet.ReadBit();
                guid[i][3] = packet.ReadBit();
                counter[i][2] = packet.ReadBit();
                guid[i][7] = packet.ReadBit();
                guid[i][5] = packet.ReadBit();
                guid[i][0] = packet.ReadBit();
                counter[i][6] = packet.ReadBit();
                guid[i][6] = packet.ReadBit();
                counter[i][7] = packet.ReadBit();
                guid[i][4] = packet.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guid[i], 5);

                packet.ReadTime("Unk time 1", i);

                packet.ReadXORByte(counter[i], 3);
                packet.ReadXORByte(counter[i], 7);

                packet.ReadTime("Unk time 2", i);

                packet.ReadXORByte(counter[i], 6);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadXORByte(guid[i], 1);
                packet.ReadXORByte(counter[i], 4);
                packet.ReadXORByte(guid[i], 3);
                packet.ReadXORByte(counter[i], 0);
                packet.ReadXORByte(guid[i], 2);
                packet.ReadXORByte(counter[i], 1);
                packet.ReadXORByte(guid[i], 6);

                packet.ReadTime("Criteria Date", i);
                packet.ReadUInt32("Criteria id", i);

                packet.ReadXORByte(counter[i], 5);

                packet.ReadUInt32("Unk", i);

                packet.ReadXORByte(guid[i], 7);
                packet.ReadXORByte(counter[i], 2);
                packet.ReadXORByte(guid[i], 0);

                packet.WriteGuid("Criteria GUID", guid[i], i);
                packet.AddValue("Criteria counter", BitConverter.ToUInt64(counter[i], 0), i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_MEMBERS_FOR_RECIPE)]
        public static void HandleGuildQueryMembersForRecipe(Packet packet)
        {
            packet.ReadInt32<SpellId>("Spell ID");
            packet.ReadUInt32("Skill ID");
            packet.ReadUInt32("Skill Value");

            var guid = packet.StartBitStream(4, 1, 0, 3, 6, 7, 5, 2);
            packet.ParseBitStream(guid, 1, 6, 5, 0, 3, 7, 2, 4);
            packet.WriteGuid("Guild GUID", guid);
        }

        [Parser(Opcode.SMSG_GUILD_MEMBERS_FOR_RECIPE)]
        public static void HandleGuildMembersForRecipe(Packet packet)
        {
            var count = packet.ReadBits("Count", 26);
            var guid = new byte[count][];

            for (int i = 0; i < count; ++i)
                guid[i] = packet.StartBitStream(2, 3, 1, 6, 0, 7, 4, 5);

            for (int i = 0; i < count; ++i)
            {
                packet.ParseBitStream(guid[i], 1, 5, 6, 7, 2, 3, 0, 4);
                packet.WriteGuid("Player GUID", guid[i], i);
            }

            packet.ReadInt32<SpellId>("Spell ID");
            packet.ReadUInt32("Skill ID");
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_MEMBER_RECIPES)]
        public static void HandleGuildQueryMemberRecipes(Packet packet)
        {
            var guildGuid = new byte[8];
            var guid = new byte[8];

            packet.ReadInt32("Skill ID");

            guid[2] = packet.ReadBit();
            guildGuid[1] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guildGuid[0] = packet.ReadBit();
            guildGuid[6] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guildGuid[4] = packet.ReadBit();
            guildGuid[3] = packet.ReadBit();
            guildGuid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guildGuid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guildGuid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guildGuid, 4);
            packet.ReadXORByte(guildGuid, 2);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guildGuid, 7);
            packet.ReadXORByte(guildGuid, 3);
            packet.ReadXORByte(guildGuid, 1);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guildGuid, 0);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guildGuid, 5);
            packet.ReadXORByte(guildGuid, 6);
            packet.ReadXORByte(guid, 5);

            packet.WriteGuid("Guild GUID", guildGuid);
            packet.WriteGuid("Player GUID", guid);
        }

        [Parser(Opcode.SMSG_GUILD_MEMBER_RECIPES)]
        public static void HandleGuildMemberRecipes(Packet packet)
        {
            var guid = packet.StartBitStream(0, 3, 7, 4, 6, 2, 1, 5);

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);

            packet.ReadInt32("Skill Value");
            packet.ReadBytes("Skill Bits", 300);

            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);

            packet.ReadInt32("Skill ID");
            packet.ReadInt32("Tradeskill Level");

            packet.WriteGuid("Player GUID", guid);
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_RECIPES)]
        public static void HandleGuildQueryRecipes(Packet packet)
        {
            var guid = packet.StartBitStream(5, 6, 1, 4, 2, 7, 0, 3);
            packet.ParseBitStream(guid, 3, 1, 0, 5, 4, 2, 6, 7);
            packet.WriteGuid("Guild GUID", guid);
        }

        [Parser(Opcode.SMSG_GUILD_KNOWN_RECIPES)]
        public static void HandleGuildRecipes(Packet packet)
        {
            var count = packet.ReadBits("Count", 16);

            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt32("Skill Id", i);
                packet.ReadBytes("Skill Bits", 300, i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_CHALLENGE_UPDATE)]
        public static void HandleGuildChallengeUpdated(Packet packet)
        {
            for (int i = 0; i < 4; ++i)
                packet.ReadInt32("Guild Experience Reward", i);

            for (int i = 0; i < 4; ++i)
                packet.ReadInt32("Completion Gold Reward", i);

            for (int i = 0; i < 4; ++i)
                packet.ReadInt32("Total Count", i);

            for (int i = 0; i < 4; ++i)
                packet.ReadInt32("Gold Reward Unk 2", i); // requires perk Cash Flow?

            for (int i = 0; i < 4; ++i)
                packet.ReadInt32("Current Count", i);
        }

        [Parser(Opcode.SMSG_GUILD_CHALLENGE_COMPLETED)]
        public static void HandleGuildChallengeCompleted(Packet packet)
        {
            packet.ReadInt32("Index"); // not confirmed
            packet.ReadInt32("Gold Reward");
            packet.ReadInt32("Current Count");
            packet.ReadInt32("Guild Experience Reward");
            packet.ReadInt32("Total Count");
        }

        [Parser(Opcode.SMSG_GUILD_REPUTATION_REACTION_CHANGED)]
        public static void HandleGuildReputationReactionChanged(Packet packet)
        {
            var guid = packet.StartBitStream(1, 6, 2, 4, 0, 3, 7, 5);
            packet.ParseBitStream(guid, 4, 6, 5, 7, 2, 0, 3, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GUILD_ADD_RANK)]
        public static void HandleGuildAddRank(Packet packet)
        {
            packet.ReadUInt32("Rank ID");
            var count = packet.ReadBits(7);
            packet.ReadWoWString("Name", count);
        }

        [Parser(Opcode.CMSG_GUILD_ACHIEVEMENT_MEMBERS)]
        public static void HandleGuildAchievementMembers(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];

            packet.ReadInt32("Achievement Id");

            guid[0] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid2[1] = packet.ReadBit();

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);

            packet.WriteGuid("Guild Guid", guid);
            packet.WriteGuid("Player Guid", guid2);

        }

        [Parser(Opcode.SMSG_GUILD_ACHIEVEMENT_MEMBERS)]
        public static void HandleGuildAchievementMembersResponse(Packet packet)
        {
            var guid = new byte[8];


            guid[3] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();

            var count = packet.ReadBits("Player Count", 26);
            var guid2 = new byte[count][];
            for (var i = 0; i < count; i++)
                guid2[i] = packet.StartBitStream(3, 1, 4, 5, 7, 0, 6, 2);

            guid[2] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            packet.ReadXORByte(guid, 5);

            for (var i = 0; i < count; i++)
            {
                packet.ParseBitStream(guid2[i], 1, 5, 7, 0, 6, 4, 3, 2);
                packet.WriteGuid("Player Guid", guid2[i], i);
            }

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);

            packet.ReadInt32<AchievementId>("Achievement Id");

            packet.ReadXORByte(guid, 1);

            packet.WriteGuid("Guild Guid", guid);

        }

        [Parser(Opcode.SMSG_GUILD_ACHIEVEMENT_EARNED)]
        public static void HandleGuildAchievementEarned(Packet packet)
        {
            var guid = packet.StartBitStream(3, 1, 0, 7, 4, 6, 2, 5);

            packet.ReadXORByte(guid, 2);

            packet.ReadPackedTime("Time");

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 3);

            packet.ReadInt32<AchievementId>("Achievement Id");

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);

            packet.WriteGuid("Guild Guid", guid);
        }
    }
}
