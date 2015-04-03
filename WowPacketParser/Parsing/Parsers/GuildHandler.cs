using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class GuildHandler
    {
        private static void ReadEmblemInfo(Packet packet)
        {
            packet.ReadInt32("Emblem Style");
            packet.ReadInt32("Emblem Color");
            packet.ReadInt32("Emblem Border Style");
            packet.ReadInt32("Emblem Border Color");
            packet.ReadInt32("Emblem Background Color");
        }

        [Parser(Opcode.CMSG_GUILD_GET_ROSTER, ClientVersionBuild.V4_0_6_13596, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleGuildRequestRoster406(Packet packet)
        {
            packet.ReadGuid("Guild GUID");
            packet.ReadGuid("Player GUID");
        }

        [Parser(Opcode.CMSG_GUILD_GET_ROSTER, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleGuildRosterC422(Packet packet)
        {
            var guid = packet.StartBitStream(7, 3, 2, 6, 5, 4, 1, 0);
            packet.ParseBitStream(guid, 7, 4, 5, 0, 1, 2, 6, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GUILD_GET_ROSTER, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleGuildRoster430(Packet packet)
        {
            // Seems to have some previous formula, processed GUIDS does not fit any know guid
            // ToDo: Fix this.
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            guid2[5] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid1[1] = packet.ReadBit();

            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 7);
            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.CMSG_GUILD_GET_ROSTER, ClientVersionBuild.V4_3_3_15354, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildRosterRequest433(Packet packet)
        {
            // Seems to have some previous formula, processed GUIDS does not fit any know guid
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            guid2[6] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid1[4] = packet.ReadBit();

            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 6);
            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleGuildRosterResponse(Packet packet)
        {
            var size = packet.ReadUInt32("Number Of Members");
            packet.ReadCString("MOTD");
            packet.ReadCString("Info");

            var numFields = packet.ReadInt32("Number Of Ranks");
            for (var i = 0; i < numFields; i++)
            {
                packet.ReadUInt32E<GuildRankRightsFlag>("Rights", i);
                packet.ReadInt32("Money Per Day", i);

                for (var j = 0; j < 6; j++)
                {
                    packet.ReadUInt32E<GuildBankRightsFlag>("Tab Rights", i, j);
                    packet.ReadInt32("Tab Slots", i, j);
                }
            }

            for (var i = 0; i < size; i++)
            {
                var guid = packet.ReadGuid("GUID", i);
                var online = packet.ReadBool("Online", i);
                var name = packet.ReadCString("Name", i);
                StoreGetters.AddName(guid, name);
                packet.ReadUInt32("Rank Id", i);
                packet.ReadByte("Level", i);
                packet.ReadByte("Class", i);
                packet.ReadByte("Unk", i);
                packet.ReadInt32<ZoneId>("Zone Id", i);

                if (!online)
                    packet.ReadUInt32("Last Online", i);

                packet.ReadCString("Public Note", i);
                packet.ReadCString("Officer Note", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER, ClientVersionBuild.V4_0_6_13596, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleGuildRoster406(Packet packet)
        {
            packet.ReadCString("Guild MOTD");
            var size = packet.ReadUInt32("Members size");

            var chars = new char[size];
            for (var i = 0; i < size; ++i)
                chars[i] = packet.ReadBit() ? '1' : '0';
            var bits = new string(chars);

            packet.AddValue("Unk bits", bits);

            for (var i = 0; i < size; ++i)
                packet.ReadCString("Public Note", i);

            for (var i = 0; i < size; ++i)
                packet.ReadUInt64("Week activity", i);

            packet.ReadCString("Guild Info");

            for (var i = 0; i < size; ++i)
                packet.ReadByteE<GuildMemberFlag>("Member Flags", i);

            for (var i = 0; i < size; ++i)
                packet.ReadInt32<ZoneId>("Zone Id", i);

            for (var i = 0; i < size; ++i)
                packet.ReadUInt32("Member Achievement Points", i);

            for (var i = 0; i < size; ++i)
                packet.ReadCString("Officer Note", i);

            for (var i = 0; i < size; ++i)
                packet.ReadUInt64("Total activity", i);

            for (var i = 0; i < size; ++i)
                packet.ReadByte("Unk Byte", i); // Related to class (spec?)

            for (var i = 0; i < size; ++i)
                packet.ReadGuid("Member GUID", i);

            for (var i = 0; i < size; ++i)
                packet.ReadByteE<Class>("Member Class", i);

            for (var i = 0; i < size; ++i)
                packet.ReadCString("Member Name", i);

            for (var i = 0; i < size; ++i)
                packet.ReadInt32("Unk", i);

            for (var i = 0; i < size; ++i)
                packet.ReadUInt32("Member Rank", i);

            for (var i = 0; i < size; ++i)
                packet.ReadInt32("Guild reputation", i);

            for (var i = 0; i < size; ++i)
                packet.ReadByte("Member Level", i);

            for (var i = 0; i < size; ++i)
                for (var j = 0; j < 2; ++j)
                {
                    var value = packet.ReadUInt32();
                    var id = packet.ReadUInt32();
                    var rank = packet.ReadUInt32();
                    packet.AddValue("Profession", string.Format("Id {0} - Value {1} - Rank {2}", id, value, rank), i, j);
                }

            for (var i = 0; i < size; ++i)
                packet.ReadUInt32("Remaining guild Rep", i);

            for (var i = 0; i < size; ++i)
                packet.ReadSingle("Last online", i);
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildRoster422(Packet packet)
        {
            // packet.AsHex(); // FIXME
        }

        [Parser(Opcode.SMSG_COMPRESSED_GUILD_ROSTER)]
        public static void HandleCompressedGuildRoster(Packet packet)
        {
            using (var packet2 = packet.Inflate(packet.ReadInt32()))
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                    HandleGuildRoster422(packet2);
                else
                    HandleGuildRoster406(packet2);
            }
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_PARTY_STATE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleGuildUpdatePartyState(Packet packet)
        {
            packet.ReadGuid("Guild GUID");
            packet.ReadGuid("Player GUID");
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_PARTY_STATE, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleGuildUpdatePartyState430(Packet packet)
        {
            var guid = packet.StartBitStream(6, 5, 2, 4, 1, 3, 7, 0);
            packet.ParseBitStream(guid, 1, 3, 5, 0, 4, 2, 6, 7);
            packet.WriteGuid("Guild Guid", guid);
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_PARTY_STATE, ClientVersionBuild.V4_3_3_15354, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildUpdatePartyState433(Packet packet)
        {
            var guid = packet.StartBitStream(0, 3, 2, 5, 6, 1, 4, 7);
            packet.ParseBitStream(guid, 2, 5, 3, 0, 1, 4, 7, 6);
            packet.WriteGuid("Guild Guid", guid);
        }

        [Parser(Opcode.SMSG_GUILD_PARTY_STATE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleGuildUpdatePartyStateResponse(Packet packet)
        {
            packet.ReadByte("Unk byte");
            packet.ReadUInt32("Unk UInt32 1");
            packet.ReadUInt32("Unk UInt32 2");
            packet.ReadUInt32("Unk UInt32 3");
        }

        [Parser(Opcode.SMSG_GUILD_PARTY_STATE, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildUpdatePartyStateResponse430(Packet packet)
        {
            packet.ReadSingle("Guild XP multiplier");
            packet.ReadUInt32("Current guild members"); // TODO: Check this.
            packet.ReadUInt32("Needed guild members");  // TODO: Check this.
            packet.ReadBit("Is guild group");
        }

        [Parser(Opcode.CMSG_QUERY_GUILD_INFO)]
        public static void HandleGuildQuery(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596)) // Not sure when it was changed
            {
                packet.ReadGuid("Guild GUID");
                packet.ReadGuid("Player GUID");
            }
            else
                packet.ReadUInt32("Guild Id");
        }

        [Parser(Opcode.SMSG_QUERY_GUILD_INFO_RESPONSE)]
        public static void HandleGuildQueryResponse(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596)) // Not sure when it was changed
                packet.ReadGuid("Guild GUID");
            else
                packet.ReadUInt32("Guild Id");

            packet.ReadCString("Guild Name");
            for (var i = 0; i < 10; i++)
                packet.ReadCString("Rank Name", i);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596)) // Not sure when it was changed
            {
                for (var i = 0; i < 10; i++)
                    packet.ReadUInt32("Creation Order", i);

                for (var i = 0; i < 10; i++)
                    packet.ReadUInt32("Rights Order", i);
            }

            ReadEmblemInfo(packet);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.ReadUInt32("Ranks");
        }

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

        [Parser(Opcode.CMSG_GUILD_SET_GUILD_MASTER)]
        public static void HandleGuildSetGuildMaster(Packet packet)
        {
            var nameLength = packet.ReadBits(7);
            packet.ReadBit("Is Dethroned"); // Most probably related to guild finder inactivity
            packet.ReadWoWString("New GuildMaster name", nameLength);
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

        [Parser(Opcode.CMSG_GUILD_SWITCH_RANK, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildSwitchRank(Packet packet)
        {
            packet.ReadBit("Direction");
            packet.ReadGuid("Player GUID");
            packet.ReadUInt32("Rank Id");
        }

        [Parser(Opcode.SMSG_GUILD_RANKS, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleGuildRankServer(Packet packet)
        {
            const int guildBankMaxTabs = 8;

            var count = packet.ReadUInt32("Rank Count");
            for (var i = 0; i < count; i++)
            {
                packet.ReadUInt32("Creation Order", i);
                packet.ReadUInt32("Rights Order", i);
                packet.ReadCString("Name", i);
                packet.ReadInt32E<GuildRankRightsFlag>("Rights", i);

                for (int j = 0; j < guildBankMaxTabs; j++)
                    packet.ReadInt32E<GuildBankRightsFlag>("Tab Rights", i, j);

                for (int j = 0; j < guildBankMaxTabs; j++)
                    packet.ReadInt32("Tab Slots", i, j);

                packet.ReadInt32("Gold Per Day", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_RANKS, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildRankServer430(Packet packet)
        {
            const int guildBankMaxTabs = 8;
            var count = packet.ReadBits("Count", 18);
            var length = new int[count];
            for (var i = 0; i < count; ++i)
                length[i] = (int)packet.ReadBits(7);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadWoWString("Name", length[i], i);
                packet.ReadInt32("Creation Order", i);
                for (var j = 0; j < guildBankMaxTabs; ++j)
                {
                    packet.ReadInt32("Tab Slots", i, j);
                    packet.ReadInt32E<GuildBankRightsFlag>("Tab Rights", i, j);
                }
                packet.ReadInt32("Rights order", i);
                packet.ReadInt32E<GuildRankRightsFlag>("Rights", i);
                packet.ReadInt32("Gold Per Day", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_RANKS, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleGuildRankServer422(Packet packet)
        {
            const int guildBankMaxTabs = 8;

            var count = packet.ReadUInt32("Rank Count");
            for (int i = 0; i < count; i++)
            {
                packet.ReadCString("Name", i);
                packet.ReadInt32("Creation Order", i);

                for (int j = 0; j < guildBankMaxTabs; j++)
                    packet.ReadInt32E<GuildBankRightsFlag>("Tab Rights", i, j);

                packet.ReadInt32("Gold Per Day", i);

                for (int j = 0; j < guildBankMaxTabs; j++)
                    packet.ReadInt32("Tab Slots", i, j);

                packet.ReadInt32("Rights Order", i);
                packet.ReadInt32E<GuildRankRightsFlag>("Rights", i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_CREATE)]
        [Parser(Opcode.CMSG_GUILD_INVITE)]
        [Parser(Opcode.CMSG_GUILD_PROMOTE_MEMBER)]
        [Parser(Opcode.CMSG_GUILD_DEMOTE_MEMBER)]
        [Parser(Opcode.CMSG_GUILD_OFFICER_REMOVE_MEMBER, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        [Parser(Opcode.CMSG_GUILD_LEADER)]
        [Parser(Opcode.CMSG_GUILD_ADD_RANK, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildCreate(Packet packet)
        {
            packet.ReadCString("Name");
        }

        [Parser(Opcode.CMSG_GUILD_OFFICER_REMOVE_MEMBER, ClientVersionBuild.V4_0_6_13596, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildRemove406(Packet packet)
        {
            packet.ReadGuid("Target GUID");
            packet.ReadGuid("Removee GUID");
        }

        [Parser(Opcode.SMSG_GUILD_INVITE, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleGuildInvite(Packet packet)
        {
            packet.ReadCString("Invitee Name");
            packet.ReadCString("Guild Name");
        }

        [Parser(Opcode.SMSG_GUILD_INVITE, ClientVersionBuild.V4_0_6_13596, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildInvite406(Packet packet)
        {
            packet.ReadUInt32("Emblem Background Color");
            packet.ReadUInt32("Emblem Border Style");
            packet.ReadUInt32("Emblem Color");
            packet.ReadUInt32("Emblem Texture File");
            packet.ReadGuid("Old Guild GUID");
            packet.ReadCString("Old Guild Name");
            packet.ReadCString("Inviter Name");
            packet.ReadUInt32("Guild Level");
            packet.ReadUInt32("Emblem Border Color");
            packet.ReadGuid("Guild GUID");
            packet.ReadCString("Guild Name");
        }


        [Parser(Opcode.SMSG_GUILD_INFO)]
        public static void HandleGuildInfo(Packet packet)
        {
            packet.ReadCString("Name");
            packet.ReadPackedTime("Creation Date");
            packet.ReadUInt32("Number of Players");
            packet.ReadUInt32("Number of Accounts");
        }

        [Parser(Opcode.CMSG_GUILD_MOTD, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleGuildMOTD(Packet packet)
        {
            packet.ReadCString("MOTD");
        }

        [Parser(Opcode.CMSG_GUILD_MOTD, ClientVersionBuild.V4_0_6_13596, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildMOTD406(Packet packet)
        {
            packet.ReadGuid("Guild GUID");
            packet.ReadGuid("Player GUID");
            packet.ReadCString("MOTD");
        }

        [Parser(Opcode.CMSG_GUILD_INFO_TEXT, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleSetGuildInfo(Packet packet)
        {
            packet.ReadCString("Text");
        }

        [Parser(Opcode.CMSG_GUILD_INFO_TEXT, ClientVersionBuild.V4_0_6_13596, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildInfo406(Packet packet)
        {
            packet.ReadGuid("Player GUID");
            packet.ReadGuid("Guild GUID");
            packet.ReadCString("Text");
        }

        [Parser(Opcode.SMSG_GUILD_EVENT)]
        public static void HandleGuildEvent(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                packet.ReadByteE<GuildEventType442>("Event Type");
            else
                packet.ReadByteE<GuildEventType>("Event Type");

            var size = packet.ReadByte("Param Count");
            for (var i = 0; i < size; i++)
                packet.ReadCString("Param", i);

            if (packet.CanRead()) // FIXME 4 5 6 16 17 (GuildEventType changed for 4.2.2)
                packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_GUILD_COMMAND_RESULT)]
        [Parser(Opcode.SMSG_GUILD_COMMAND_RESULT_2, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildCommandResult(Packet packet)
        {
            packet.ReadUInt32E<GuildCommandType>("Command Type");
            packet.ReadCString("Param");
            packet.ReadUInt32E<GuildCommandError>("Command Result");
        }

        [Parser(Opcode.MSG_SAVE_GUILD_EMBLEM)]
        public static void HandleGuildEmblem(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadGuid("GUID");
                ReadEmblemInfo(packet);
            }
            else
                packet.ReadUInt32E<GuildEmblemError>("Result");
        }

        [Parser(Opcode.CMSG_GUILD_SET_PUBLIC_NOTE)]
        public static void HandleGuildSetPublicNote(Packet packet)
        {
            packet.ReadCString("Player Name");
            packet.ReadCString("Public Note");
        }

        [Parser(Opcode.CMSG_GUILD_SET_OFFICER_NOTE)]
        public static void HandleGuildSetOfficerNote(Packet packet)
        {
            packet.ReadCString("Player Name");
            packet.ReadCString("Officer Note");
        }

        [Parser(Opcode.CMSG_GUILD_SET_NOTE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildSetNote(Packet packet)
        {
            packet.ReadBit("Public");
            packet.ReadGuid("Target GUID");
            packet.ReadGuid("Issuer GUID");
            packet.ReadGuid("Guild GUID");
            packet.ReadCString("Note");
        }

        [Parser(Opcode.CMSG_GUILD_BANK_ACTIVATE)]
        public static void HandleGuildBankerActivate(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadBool("Full Slot List");
        }

        [Parser(Opcode.CMSG_GUILD_BANK_QUERY_TAB)]
        public static void HandleGuildBankQueryTab(Packet packet)
        {
            packet.ReadGuid("GUID");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_2_14545)
                || ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595))
                packet.ReadByte("Tab Id");
            packet.ReadBool("Full Slot List"); // false = only slots updated in last operation are shown. True = all slots updated
        }

        [Parser(Opcode.SMSG_GUILD_BANK_QUERY_RESULTS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildBankList(Packet packet)
        {
            packet.ReadUInt64("Money");
            var tabId = packet.ReadByte("Tab Id");
            packet.ReadInt32("Remaining Withdraw");
            if (packet.ReadBool("Full Slot List") && tabId == 0)
            {
                var size = packet.ReadByte("Number of Tabs");
                for (var i = 0; i < size; i++)
                {
                    packet.ReadCString("Tab Name", i);
                    packet.ReadCString("Tab Icon", i);
                }
            }

            var slots = packet.ReadByte("Number of Slots");
            for (var i = 0; i < slots; i++)
            {
                packet.ReadByte("Slot Id", i);
                var entry = packet.ReadInt32<ItemId>("Item Entry", i);
                if (entry > 0)
                {
                    packet.ReadUInt32E<UnknownFlags>("Unk mask", i);
                    var ramdonEnchant = packet.ReadInt32("Random Item Property Id", i);
                    if (ramdonEnchant != 0)
                        packet.ReadUInt32("Item Suffix Factor", i);
                    packet.ReadUInt32("Stack Count", i);
                    packet.ReadUInt32("Unk Uint32 2", i); // Only seen 0
                    packet.ReadByte("Spell Charges", i);
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                    {
                        packet.ReadInt32("Unk 1 Int32", i);
                        packet.ReadInt32("Unk 2 Int32", i);
                    }

                    var enchantment = packet.ReadByte("Number of Enchantments", i);
                    for (var j = 0; j < enchantment; j++)
                    {
                        packet.ReadByte("Enchantment Slot Id", i, j);
                        packet.ReadUInt32("Enchantment Id", i, j);
                    }
                }
            }
        }

        [Parser(Opcode.CMSG_GUILD_BANK_SWAP_ITEMS)]
        public static void HandleGuildBankSwapItems(Packet packet)
        {
            packet.ReadGuid("GUID");
            var bankToBank = packet.ReadBool("BankToBank");
            if (bankToBank)
            {
                packet.ReadByte("Dest Tab Id");
                packet.ReadByte("Dest Slot Id");
                packet.ReadInt32<ItemId>("Dest Item Entry");
                packet.ReadByte("Tab Id");
                packet.ReadByte("Slot Id");
                packet.ReadInt32<ItemId>("Item Entry");
                packet.ReadByte("Unk Byte 1");
                packet.ReadUInt32("Amount");
            }
            else
            {
                packet.ReadByte("Tab Id");
                packet.ReadByte("Slot Id");
                packet.ReadInt32<ItemId>("Item Entry");
                var autostore = packet.ReadBool("Autostore");
                if (autostore)
                {
                    packet.ReadUInt32("Autostore Count");
                    packet.ReadBool("From Bank To Player");
                    packet.ReadUInt32("Unk Uint32 2");
                }
                else
                {
                    packet.ReadByte("Bag");
                    packet.ReadByte("Slot");
                    packet.ReadBool("From Bank To Player");
                    packet.ReadUInt32("Amount");
                }
            }
        }

        [Parser(Opcode.CMSG_GUILD_BANK_BUY_TAB)]
        public static void HandleGuildBankBuyTab(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByte("Tab Id");
        }

        [Parser(Opcode.CMSG_GUILD_BANK_UPDATE_TAB)]
        public static void HandleGuildBankUpdateTab(Packet packet)
        {
            packet.ReadGuid("Banker");
            packet.ReadByte("BankTab");
            packet.ReadCString("Name");
            packet.ReadCString("Icon");
        }

        [Parser(Opcode.CMSG_GUILD_GET_RANKS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        [Parser(Opcode.CMSG_GUILD_QUERY_NEWS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_GUILD_REQUEST_MAX_DAILY_XP, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        [Parser(Opcode.CMSG_REQUEST_GUILD_XP, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        [Parser(Opcode.CMSG_GUILD_QUERY_TRADESKILL)]
        public static void HandleGuildRequestMulti(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_GUILD_REQUEST_MAX_DAILY_XP, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildRequestMaxDailyXP430(Packet packet)
        {
            var guid = packet.StartBitStream(6, 5, 2, 3, 0, 4, 7, 1);
            packet.ParseBitStream(guid, 1, 6, 4, 5, 3, 7, 0, 2);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_XP, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleRequestGuildXP430(Packet packet)
        {
            var guid = packet.StartBitStream(2, 4, 5, 6, 1, 0, 3, 7);
            packet.ParseBitStream(guid, 0, 1, 4, 3, 2, 6, 7, 5);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_GUILD_REQUEST_MAX_DAILY_XP, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleGuildRequestMaxDailyXP510(Packet packet)
        {
            var guid = packet.StartBitStream(5, 3, 6, 4, 7, 2, 1, 0);
            packet.ParseBitStream(guid, 4, 7, 5, 6, 0, 1, 2, 3);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_GUILD_GET_RANKS, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleGuildRanks43(Packet packet)
        {
            var guid = packet.StartBitStream(7, 2, 0, 4, 6, 5, 1, 3);
            packet.ParseBitStream(guid, 7, 5, 2, 6, 1, 4, 0, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GUILD_GET_RANKS, ClientVersionBuild.V4_3_3_15354, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildRanks433(Packet packet)
        {
            var guid = packet.StartBitStream(2, 6, 1, 0, 5, 3, 7, 4);
            packet.ParseBitStream(guid, 3, 6, 5, 4, 0, 7, 2, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_GUILD_XP)]
        public static void HandleGuildXP(Packet packet)
        {
            packet.ReadUInt64("Member Total XP");
            packet.ReadUInt64("Guild XP for next Level");
            packet.ReadUInt64("Guild XP Today");
            packet.ReadUInt64("Member Weekly XP");
            packet.ReadUInt64("Guild Current XP");
        }

        [Parser(Opcode.SMSG_GUILD_NEWS_UPDATE, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleGuildNewsUpdate(Packet packet)
        {
            var size = packet.ReadUInt32("Size");

            for (var i = 0; i < size; ++i)
            {
                var unk1 = packet.ReadUInt32("Unk count", i);
                for (var j = 0; j < unk1; ++j)
                    packet.ReadUInt64("Unk uint64", i, j);
            }

            for (var i = 0; i < size; ++i)
                packet.ReadPackedTime("Time", i);

            for (var i = 0; i < size; ++i)
                packet.ReadGuid("Player GUID", i);

            for (var i = 0; i < size; ++i)
            {
                packet.ReadUInt32("Item/Achievement", i);
                packet.ReadUInt32("Unk", i);
            }

            for (var i = 0; i < size; ++i)
                packet.ReadInt32E<GuildNewsType>("News Type", i);

            for (var i = 0; i < size; ++i)
                packet.ReadUInt32("Guild/Player news", i);

            for (var i = 0; i < size; ++i)
                packet.ReadUInt32("Unk UInt32 4", i);
        }

        [Parser(Opcode.SMSG_GUILD_NEWS_UPDATE, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildNewsUpdate422(Packet packet)
        {
            var size = packet.ReadUInt32("Size");
            for (var i = 0; i < size; ++i)
                packet.ReadUInt32("Guild/Player news", i);

            for (var i = 0; i < size; ++i)
                packet.ReadGuid("Player GUID", i);

            for (var i = 0; i < size; ++i)
            {
                var unk1 = packet.ReadUInt32("Unk count", i);
                for (var j = 0; j < unk1; ++j)
                    packet.ReadUInt64("Unk uint64", i, j);
            }

            for (var i = 0; i < size; ++i)
                packet.ReadInt32E<GuildNewsType>("News Type", i);

            for (var i = 0; i < size; ++i)
            {
                packet.ReadUInt32("Item/Achievement", i);
                packet.ReadUInt32("Unk", i);
            }

            for (var i = 0; i < size; ++i)
                packet.ReadUInt32("Unk UInt32 4", i);

            for (var i = 0; i < size; ++i)
                packet.ReadPackedTime("Time", i);
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_REWARDS_LIST)]
        public static void HandleRequestGuildRewardsList(Packet packet)
        {
            packet.ReadTime("CurrentVersion");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_3_4_15595))
                packet.ReadGuid("Player GUID");
        }

        [Parser(Opcode.SMSG_GUILD_REWARD_LIST, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildRewardsList(Packet packet)
        {
            packet.ReadUInt32("Guild Id");
            var size = packet.ReadUInt32("Size");

            for (var i = 0; i < size; ++i)
                packet.ReadUInt32("Unk UInt32 1", i);

            for (var i = 0; i < size; ++i)
                packet.ReadUInt32("Unk UInt32 2", i);

            for (var i = 0; i < size; ++i)
                packet.ReadUInt64("Price", i);

            for (var i = 0; i < size; ++i)
                packet.ReadInt32<AchievementId>("Achievement Id", i);

            for (var i = 0; i < size; ++i)
                packet.ReadUInt32E<ReputationRank>("Faction Standing", i);

            for (var i = 0; i < size; ++i)
                packet.ReadUInt32<ItemId>("Item Id", i);
        }

        [Parser(Opcode.CMSG_GUILD_BANK_DEPOSIT_MONEY)]
        [Parser(Opcode.CMSG_GUILD_BANK_WITHDRAW_MONEY)]
        public static void HandleGuildBankDepositMoney(Packet packet)
        {
            packet.ReadGuid("GUID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                packet.ReadUInt64("Money");
            else
                packet.ReadUInt32("Money");
        }

        [Parser(Opcode.CMSG_GUILD_BANK_TEXT_QUERY, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleQueryGuildBankText(Packet packet)
        {
            packet.ReadUInt32("Tab Id");
        }

        [Parser(Opcode.MSG_QUERY_GUILD_BANK_TEXT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildQueryBankText(Packet packet)
        {
            packet.ReadByte("Tab Id");
            if (packet.Direction == Direction.ServerToClient)
                packet.ReadCString("Text");
        }

        [Parser(Opcode.CMSG_GUILD_BANK_SET_TAB_TEXT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildSetBankText(Packet packet)
        {
            packet.ReadByte("Tab Id");
            packet.ReadCString("Tab Text");
        }

        [Parser(Opcode.MSG_GUILD_PERMISSIONS)]
        public static void HandleGuildPermissions(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
                return;

            packet.ReadUInt32("Rank Id");
            packet.ReadUInt32E<GuildRankRightsFlag>("Rights");
            packet.ReadInt32("Remaining Money");
            packet.ReadByte("Tab size");
            var tabSize = ClientVersion.AddedInVersion(ClientType.Cataclysm) ? 8 : 6;
            for (var i = 0; i < tabSize; i++)
            {
                packet.ReadInt32E<GuildBankRightsFlag>("Tab Rights", i);
                packet.ReadInt32("Tab Slots", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_PERMISSIONS_QUERY_RESULTS)]
        public static void HandleGuildPermissionsQueryResult(Packet packet)
        {
            packet.ReadUInt32("Rank Id");
            packet.ReadInt32("Purchased Tab size");
            packet.ReadUInt32E<GuildRankRightsFlag>("Rights");
            packet.ReadInt32("Remaining Money");
            packet.ReadBits("Tab size", 23);
            for (var i = 0; i < 8; i++)
            {
                packet.ReadInt32E<GuildBankRightsFlag>("Tab Rights", i);
                packet.ReadInt32("Tab Slots", i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_BANK_REMAINING_WITHDRAW_MONEY_QUERY)]
        [Parser(Opcode.MSG_GUILD_BANK_MONEY_WITHDRAWN)]
        public static void HandleGuildBankMoneyWithdrawn(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                    packet.ReadInt64("Remaining Money");
                else
                    packet.ReadInt32("Remaining Money");
            }
        }

        [Parser(Opcode.SMSG_GUILD_BANK_REMAINING_WITHDRAW_MONEY)]
        public static void HandleGuildBankMoneyWithdrawnResponse(Packet packet)
        {
            packet.ReadInt64("Remaining Money");
        }

        [Parser(Opcode.MSG_GUILD_EVENT_LOG_QUERY)]
        public static void HandleGuildEventLogQuery(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
                return;

            var size = packet.ReadByte("Log size");
            for (var i = 0; i < size; i++)
            {
                var type = packet.ReadByteE<GuildEventLogType>("Type");
                packet.ReadGuid("GUID");
                if (type != GuildEventLogType.JoinGuild && type != GuildEventLogType.LeaveGuild)
                    packet.ReadGuid("GUID 2");
                if (type == GuildEventLogType.PromotePlayer || type == GuildEventLogType.DemotePlayer)
                    packet.ReadByte("Rank");
                packet.ReadUInt32("Time Ago");
            }
        }

        [Parser(Opcode.MSG_GUILD_BANK_LOG_QUERY, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildBankLogQueryResult(Packet packet)
        {
            packet.ReadByte("Tab Id");
            if (packet.Direction == Direction.ServerToClient)
            {
                var size = packet.ReadByte("Size");
                for (var i = 0; i < size; i++)
                {
                    var type = packet.ReadByteE<GuildBankEventLogType>("Bank Log Event Type", i);
                    packet.ReadGuid("GUID", i);
                    if (type == GuildBankEventLogType.BuySlot)
                        packet.ReadUInt32("Cost", i);
                    else
                    {
                        if (type == GuildBankEventLogType.DepositMoney ||
                            type == GuildBankEventLogType.WithdrawMoney ||
                            type == GuildBankEventLogType.RepairMoney)
                            packet.ReadUInt32("Money", i);
                        else
                        {
                            packet.ReadUInt32("Item Entry", i);
                            packet.ReadUInt32("Stack Count", i);
                            if (type == GuildBankEventLogType.MoveItem ||
                                type == GuildBankEventLogType.MoveItem2)
                                packet.ReadByte("Tab Id", i);
                        }
                    }
                    packet.ReadUInt32("Time", i);
                }
            }
        }

        [Parser(Opcode.CMSG_GUILD_BANK_LOG_QUERY, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleGuildBankLogQuery(Packet packet)
        {
            packet.ReadUInt32("Tab Id");
        }

        [Parser(Opcode.SMSG_GUILD_DECLINE)]
        public static void HandleGuildDecline(Packet packet)
        {
            packet.ReadCString("Reason");
            packet.ReadBool("Auto decline");
        }

        [Parser(Opcode.SMSG_PETITION_SHOW_LIST)]
        public static void HandlePetitionShowList(Packet packet)
        {
            packet.ReadGuid("GUID");
            var counter = packet.ReadByte("Counter");
            for (var i = 0; i < counter; i++)
            {
                packet.ReadUInt32("Index");
                packet.ReadUInt32("Charter Entry");
                packet.ReadUInt32("Charter Display");
                packet.ReadUInt32("Charter Cost");
                packet.ReadUInt32("Unk Uint32 1");
                packet.ReadUInt32("Required signs");
            }
        }

        [Parser(Opcode.CMSG_PETITION_BUY)]
        public static void HandlePetitionBuy(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Unk UInt32 1");
            packet.ReadUInt64("Unk UInt64 1");
            packet.ReadCString("Name");
            packet.ReadCString("Text");
            packet.ReadUInt32("Unk UInt32 2");
            packet.ReadUInt32("Unk UInt32 3");
            packet.ReadUInt32("Unk UInt32 4");
            packet.ReadUInt32("Unk UInt32 5");
            packet.ReadUInt32("Unk UInt32 6");
            packet.ReadUInt32("Unk UInt32 7");
            packet.ReadUInt32("Unk UInt32 8");
            packet.ReadUInt16("Unk UInt16 1");
            packet.ReadUInt32("Unk UInt32 9");
            packet.ReadUInt32("Unk UInt32 10");
            packet.ReadUInt32("Unk UInt32 11");

            for (var i = 0; i < 10; i++)
                packet.ReadCString("Unk String", i);

            packet.ReadUInt32("Client Index");
            packet.ReadUInt32("Unk UInt32 12");
        }

        [Parser(Opcode.CMSG_PETITION_SHOW_SIGNATURES)]
        public static void HandlePetitionShowSignatures(Packet packet)
        {
            packet.ReadGuid("Petition GUID");
        }

        [Parser(Opcode.SMSG_PETITION_SHOW_SIGNATURES)]
        public static void HandlePetitionShowSignaturesServer(Packet packet)
        {
            packet.ReadGuid("Petition GUID");
            packet.ReadGuid("Owner GUID");
            packet.ReadUInt32("Guild/Team GUID");
            var counter = packet.ReadByte("Sign count");
            for (var i = 0; i < counter; i++)
            {
                packet.ReadGuid("Player GUID", i);
                packet.ReadUInt32("Unk UInt32 1", i);
            }
        }

        [Parser(Opcode.CMSG_PETITION_SIGN)]
        public static void HandlePetitionSign(Packet packet)
        {
            packet.ReadGuid("Petition GUID");
            packet.ReadByte("Unk Byte 1");
        }

        [Parser(Opcode.SMSG_PETITION_SIGN_RESULTS)]
        public static void HandlePetitionSignResult(Packet packet)
        {
            packet.ReadGuid("Petition GUID");
            packet.ReadGuid("Player GUID");
            packet.ReadUInt32E<PetitionResultType>("Petition Result");
        }

        [Parser(Opcode.MSG_PETITION_DECLINE)]
        public static void HandlePetitionDecline(Packet packet)
        {
            packet.ReadGuid(packet.Direction == Direction.ClientToServer ? "Petition GUID" : "Player GUID");
        }

        [Parser(Opcode.CMSG_OFFER_PETITION)]
        public static void HandlePetitionOffer(Packet packet)
        {
            packet.ReadUInt32("Unk UInt3 1");
            packet.ReadGuid("Petition GUID");
            packet.ReadGuid("Target GUID");
        }

        [Parser(Opcode.CMSG_TURN_IN_PETITION)]
        public static void HandlePetitionTurnIn(Packet packet)
        {
            packet.ReadGuid("Petition GUID");
        }

        [Parser(Opcode.SMSG_TURN_IN_PETITION_RESULT)]
        public static void HandlePetitionTurnInResults(Packet packet)
        {
            packet.ReadUInt32E<PetitionResultType>("Result");
        }

        [Parser(Opcode.CMSG_PETITION_QUERY)]
        public static void HandlePetitionQuery(Packet packet)
        {
            packet.ReadUInt32("Guild/Team GUID");
            packet.ReadGuid("Petition GUID");
        }

        [Parser(Opcode.SMSG_PETITION_QUERY_RESPONSE)]
        public static void HandlePetitionQueryResponse(Packet packet)
        {
            packet.ReadUInt32("Guild/Team GUID");
            packet.ReadGuid("Owner GUID");
            packet.ReadCString("Name");
            packet.ReadCString("Text");
            packet.ReadUInt32("Signs Needed");
            packet.ReadUInt32("Signs Needed");
            packet.ReadUInt32("Unk UInt32 4");
            packet.ReadUInt32("Unk UInt32 5");
            packet.ReadUInt32("Unk UInt32 6");
            packet.ReadUInt32("Unk UInt32 7");
            packet.ReadUInt32("Unk UInt32 8");
            packet.ReadUInt16("Unk UInt16 1");
            packet.ReadUInt32("Unk UInt32 (Level?)");
            packet.ReadUInt32("Unk UInt32 (Level?)");
            packet.ReadUInt32("Unk UInt32 11");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                for (var i = 0; i < 10; i++)
                    packet.ReadCString("Unk String", i);

            packet.ReadUInt32("Client Index");
            packet.ReadUInt32("Petition Type (0: Guild / 1: Arena)");
        }

        [Parser(Opcode.MSG_PETITION_RENAME)]
        public static void HandlePetitionRename(Packet packet)
        {
            packet.ReadGuid("Petition GUID");
            packet.ReadCString("New Name");
        }

        [Parser(Opcode.SMSG_OFFER_PETITION_ERROR)]
        public static void HandlePetitionError(Packet packet)
        {
            packet.ReadGuid("Petition GUID");
        }

        [Parser(Opcode.SMSG_GUILD_REPUTATION_WEEKLY_CAP)]
        public static void HandleGuildReputationWeeklyCap(Packet packet)
        {
            packet.ReadUInt32("Cap");
        }

        [Parser(Opcode.CMSG_GUILD_SET_ACHIEVEMENT_TRACKING)]
        public static void HandleGuildSetAchievementTracking(Packet packet)
        {
            var count = packet.ReadBits("Count", 24);
            for (var i = 0; i < count; ++i)
                packet.ReadInt32<AchievementId>("Achievement Id", i);
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_RECIPES, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleQueryGuildRecipes510(Packet packet)
        {
            var guid = packet.StartBitStream(1, 4, 3, 5, 0, 6, 2, 7);
            packet.ParseBitStream(guid, 5, 3, 1, 4, 0, 7, 6, 2);
            packet.WriteGuid("Guild Guid", guid);
        }

        [Parser(Opcode.SMSG_GUILD_MAX_DAILY_XP)]
        [Parser(Opcode.SMSG_GUILD_XP_GAIN)]
        public static void HandleGuildXPResponse(Packet packet)
        {
            packet.ReadInt64("XP");
        }

        [Parser(Opcode.CMSG_GUILD_SET_FOCUSED_ACHIEVEMENT)]
        public static void HandleGuildAchievementProgressQuery(Packet packet)
        {
            packet.ReadInt32<AchievementId>("Achievement Id");
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER_UPDATE)]
        public static void HandleGuildRosterUpdate(Packet packet)
        {
            var count = packet.ReadBits("Count", 18);
            var guids = new byte[count][];
            var strlen = new uint[count][];
            var param = new bool[count][];

            for (int i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                strlen[i] = new uint[3];
                param[i] = new bool[2];

                strlen[i][2] = packet.ReadBits(7);
                guids[i][5] = packet.ReadBit();
                guids[i][4] = packet.ReadBit();
                guids[i][0] = packet.ReadBit();
                guids[i][7] = packet.ReadBit();
                guids[i][1] = packet.ReadBit();
                guids[i][3] = packet.ReadBit();
                strlen[i][0] = packet.ReadBits(8);
                guids[i][6] = packet.ReadBit();
                param[i][0] = packet.ReadBit();
                guids[i][2] = packet.ReadBit();
                param[i][1] = packet.ReadBit();
                strlen[i][1] = packet.ReadBits(8);
            }

            for (int i = 0; i < count; ++i)
            {
                packet.ReadByte("unk Byte 359", i); // 0 or 1
                packet.ReadUInt32("Remaining guild week Rep", i);

                packet.ReadXORByte(guids[i], 1);

                packet.ReadInt64("Total activity", i);

                packet.ReadXORByte(guids[i], 2);

                for (int j = 0; j < 2; ++j)
                {
                    var rank = packet.ReadUInt32();
                    var id = packet.ReadUInt32();
                    var value = packet.ReadUInt32();
                    packet.AddValue("Profession", string.Format("Id {0} - Value {1} - Rank {2}", id, value, rank), i, j);
                }

                packet.ReadXORByte(guids[i], 0);
                packet.ReadXORByte(guids[i], 6);
                packet.ReadXORByte(guids[i], 7);

                packet.ReadInt32("Rank", i);
                packet.ReadWoWString("Public Comment", strlen[i][0], i);
                packet.ReadWoWString("Officers Comment", strlen[i][1], i);

                packet.ReadXORByte(guids[i], 4);
                packet.ReadXORByte(guids[i], 5);

                packet.ReadInt32("Guild Reputation", i);
                packet.ReadInt32("Member Achievement Points", i);
                packet.ReadSingle("Last Online", i);
                packet.ReadInt64("Week activity", i);
                packet.ReadByte("Level", i);
                packet.ReadByteE<Class>("Class", i);

                packet.ReadXORByte(guids[i], 3);

                packet.ReadByte("unk Byte 356", i); // 0
                packet.ReadInt32<ZoneId>("Zone Id", i);
                packet.ReadWoWString("Character Name", strlen[i][2], i);
                packet.ReadInt32("unk Int32 44", i);
                packet.AddValue("Can SoR", param[i][0], i);
                packet.AddValue("Has Authenticator", param[i][1], i);

                packet.WriteGuid("Player Guid", guids[i], i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_MEMBER_UPDATE_NOTE)]
        public static void HandleGuildMemberUpdateNote(Packet packet)
        {
            var guid = new byte[8];

            guid[7] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var len = packet.ReadBits("Note Text Length", 8);
            guid[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            packet.ReadBit("Public");
            guid[1] = packet.ReadBit();

            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);

            packet.ReadWoWString("Note", len);

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);

            packet.WriteGuid("Guid", guid);

        }

        [Parser(Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleGuildAchievementData(Packet packet)
        {
            var cnt = packet.ReadUInt32("Count");
            for (var i = 0; i < cnt; ++i)
                packet.ReadPackedTime("Date", i);

            for (var i = 0; i < cnt; ++i)
                packet.ReadUInt32("Achievement Id", i);
        }

        [Parser(Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleGuildAchievementData430(Packet packet)
        {
            var count = packet.ReadBits("Count", 23);
            for (var i = 0; i < count; ++i)
            {
                packet.ReadPackedTime("Date", i);
                packet.ReadInt32<AchievementId>("Achievement Id", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_CRITERIA_DATA, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildCriteriaData(Packet packet)
        {
            var criterias = packet.ReadUInt32("Criterias");

            for (var i = 0; i < criterias; ++i)
                packet.ReadGuid("Player GUID", i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadUInt32("Criteria Timer 1", i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadUInt32("Criteria Timer 2", i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadUInt64("Counter", i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadPackedTime("Criteria Time", i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadUInt32("Criteria Id", i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadUInt32("Flag", i);
        }

        [Parser(Opcode.SMSG_GUILD_MEMBER_DAILY_RESET)]
        [Parser(Opcode.CMSG_GUILD_REQUEST_CHALLENGE_UPDATE)]
        [Parser(Opcode.CMSG_GUILD_GET_ROSTER, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        [Parser(Opcode.CMSG_GUILD_ACCEPT)]
        [Parser(Opcode.CMSG_GUILD_DECLINE_INVITATION)]
        [Parser(Opcode.CMSG_GUILD_INFO)]
        [Parser(Opcode.CMSG_GUILD_LEAVE)]
        [Parser(Opcode.CMSG_GUILD_DISBAND)]
        [Parser(Opcode.CMSG_GUILD_DELETE_RANK, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_GUILD_EVENT_LOG_QUERY)]
        [Parser(Opcode.SMSG_GUILD_CANCEL)] // Fires GUILD_INVITE_CANCEL
        [Parser(Opcode.SMSG_GUILD_INVITE_CANCEL)]
        [Parser(Opcode.CMSG_GUILD_AUTO_DECLINE_INVITATION)] // 4.3.4, sent if player has PLAYER_FLAGS_AUTO_DECLINE_GUILD
        [Parser(Opcode.CMSG_GUILD_PERMISSIONS_QUERY)]
        public static void HandleGuildNull(Packet packet)
        {
            // Just to have guild opcodes together
        }
    }
}
