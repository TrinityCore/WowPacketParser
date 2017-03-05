using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class GuildHandler
    {
        private static void ReadEmblemInfo(Packet packet)
        {
            packet.Translator.ReadInt32("Emblem Style");
            packet.Translator.ReadInt32("Emblem Color");
            packet.Translator.ReadInt32("Emblem Border Style");
            packet.Translator.ReadInt32("Emblem Border Color");
            packet.Translator.ReadInt32("Emblem Background Color");
        }

        [Parser(Opcode.CMSG_GUILD_GET_ROSTER, ClientVersionBuild.V4_0_6_13596, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleGuildRequestRoster406(Packet packet)
        {
            packet.Translator.ReadGuid("Guild GUID");
            packet.Translator.ReadGuid("Player GUID");
        }

        [Parser(Opcode.CMSG_GUILD_GET_ROSTER, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleGuildRosterC422(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(7, 3, 2, 6, 5, 4, 1, 0);
            packet.Translator.ParseBitStream(guid, 7, 4, 5, 0, 1, 2, 6, 3);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GUILD_GET_ROSTER, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleGuildRoster430(Packet packet)
        {
            // Seems to have some previous formula, processed GUIDS does not fit any know guid
            // ToDo: Fix this.
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            guid2[5] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.CMSG_GUILD_GET_ROSTER, ClientVersionBuild.V4_3_3_15354, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildRosterRequest433(Packet packet)
        {
            // Seems to have some previous formula, processed GUIDS does not fit any know guid
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            guid2[6] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleGuildRosterResponse(Packet packet)
        {
            var size = packet.Translator.ReadUInt32("Number Of Members");
            packet.Translator.ReadCString("MOTD");
            packet.Translator.ReadCString("Info");

            var numFields = packet.Translator.ReadInt32("Number Of Ranks");
            for (var i = 0; i < numFields; i++)
            {
                packet.Translator.ReadUInt32E<GuildRankRightsFlag>("Rights", i);
                packet.Translator.ReadInt32("Money Per Day", i);

                for (var j = 0; j < 6; j++)
                {
                    packet.Translator.ReadUInt32E<GuildBankRightsFlag>("Tab Rights", i, j);
                    packet.Translator.ReadInt32("Tab Slots", i, j);
                }
            }

            for (var i = 0; i < size; i++)
            {
                var guid = packet.Translator.ReadGuid("GUID", i);
                var online = packet.Translator.ReadBool("Online", i);
                var name = packet.Translator.ReadCString("Name", i);
                StoreGetters.AddName(guid, name);
                packet.Translator.ReadUInt32("Rank Id", i);
                packet.Translator.ReadByte("Level", i);
                packet.Translator.ReadByte("Class", i);
                packet.Translator.ReadByte("Unk", i);
                packet.Translator.ReadInt32<ZoneId>("Zone Id", i);

                if (!online)
                    packet.Translator.ReadUInt32("Last Online", i);

                packet.Translator.ReadCString("Public Note", i);
                packet.Translator.ReadCString("Officer Note", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER, ClientVersionBuild.V4_0_6_13596, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleGuildRoster406(Packet packet)
        {
            packet.Translator.ReadCString("Guild MOTD");
            var size = packet.Translator.ReadUInt32("Members size");

            var chars = new char[size];
            for (var i = 0; i < size; ++i)
                chars[i] = packet.Translator.ReadBit() ? '1' : '0';
            var bits = new string(chars);

            packet.AddValue("Unk bits", bits);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadCString("Public Note", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadUInt64("Week activity", i);

            packet.Translator.ReadCString("Guild Info");

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadByteE<GuildMemberFlag>("Member Flags", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadInt32<ZoneId>("Zone Id", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadUInt32("Member Achievement Points", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadCString("Officer Note", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadUInt64("Total activity", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadByte("Unk Byte", i); // Related to class (spec?)

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadGuid("Member GUID", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadByteE<Class>("Member Class", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadCString("Member Name", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadInt32("Unk", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadUInt32("Member Rank", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadInt32("Guild reputation", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadByte("Member Level", i);

            for (var i = 0; i < size; ++i)
                for (var j = 0; j < 2; ++j)
                {
                    var value = packet.Translator.ReadUInt32();
                    var id = packet.Translator.ReadUInt32();
                    var rank = packet.Translator.ReadUInt32();
                    packet.AddValue("Profession", $"Id {id} - Value {value} - Rank {rank}", i, j);
                }

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadUInt32("Remaining guild Rep", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadSingle("Last online", i);
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildRoster422(Packet packet)
        {
            // packet.AsHex(); // FIXME
        }

        [Parser(Opcode.SMSG_COMPRESSED_GUILD_ROSTER)]
        public static void HandleCompressedGuildRoster(Packet packet)
        {
            using (var packet2 = packet.Inflate(packet.Translator.ReadInt32()))
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
            packet.Translator.ReadGuid("Guild GUID");
            packet.Translator.ReadGuid("Player GUID");
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_PARTY_STATE, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleGuildUpdatePartyState430(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(6, 5, 2, 4, 1, 3, 7, 0);
            packet.Translator.ParseBitStream(guid, 1, 3, 5, 0, 4, 2, 6, 7);
            packet.Translator.WriteGuid("Guild Guid", guid);
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_PARTY_STATE, ClientVersionBuild.V4_3_3_15354, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildUpdatePartyState433(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(0, 3, 2, 5, 6, 1, 4, 7);
            packet.Translator.ParseBitStream(guid, 2, 5, 3, 0, 1, 4, 7, 6);
            packet.Translator.WriteGuid("Guild Guid", guid);
        }

        [Parser(Opcode.SMSG_GUILD_PARTY_STATE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleGuildUpdatePartyStateResponse(Packet packet)
        {
            packet.Translator.ReadByte("Unk byte");
            packet.Translator.ReadUInt32("Unk UInt32 1");
            packet.Translator.ReadUInt32("Unk UInt32 2");
            packet.Translator.ReadUInt32("Unk UInt32 3");
        }

        [Parser(Opcode.SMSG_GUILD_PARTY_STATE, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildUpdatePartyStateResponse430(Packet packet)
        {
            packet.Translator.ReadSingle("Guild XP multiplier");
            packet.Translator.ReadUInt32("Current guild members"); // TODO: Check this.
            packet.Translator.ReadUInt32("Needed guild members");  // TODO: Check this.
            packet.Translator.ReadBit("Is guild group");
        }

        [Parser(Opcode.CMSG_QUERY_GUILD_INFO)]
        public static void HandleGuildQuery(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596)) // Not sure when it was changed
            {
                packet.Translator.ReadGuid("Guild GUID");
                packet.Translator.ReadGuid("Player GUID");
            }
            else
                packet.Translator.ReadUInt32("Guild Id");
        }

        [Parser(Opcode.SMSG_QUERY_GUILD_INFO_RESPONSE)]
        public static void HandleGuildQueryResponse(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596)) // Not sure when it was changed
                packet.Translator.ReadGuid("Guild GUID");
            else
                packet.Translator.ReadUInt32("Guild Id");

            packet.Translator.ReadCString("Guild Name");
            for (var i = 0; i < 10; i++)
                packet.Translator.ReadCString("Rank Name", i);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596)) // Not sure when it was changed
            {
                for (var i = 0; i < 10; i++)
                    packet.Translator.ReadUInt32("Creation Order", i);

                for (var i = 0; i < 10; i++)
                    packet.Translator.ReadUInt32("Rights Order", i);
            }

            ReadEmblemInfo(packet);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.Translator.ReadUInt32("Ranks");
        }

        [Parser(Opcode.CMSG_GUILD_SET_RANK_PERMISSIONS, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleGuildRank(Packet packet)
        {
            packet.Translator.ReadUInt32("Rank Id");
            packet.Translator.ReadUInt32E<GuildRankRightsFlag>("Rights");
            packet.Translator.ReadCString("Name");
            packet.Translator.ReadInt32("Money Per Day");
            for (var i = 0; i < 6; i++)
            {
                packet.Translator.ReadUInt32E<GuildBankRightsFlag>("Tab Rights", i);
                packet.Translator.ReadInt32("Tab Slots", i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_SET_GUILD_MASTER)]
        public static void HandleGuildSetGuildMaster(Packet packet)
        {
            var nameLength = packet.Translator.ReadBits(7);
            packet.Translator.ReadBit("Is Dethroned"); // Most probably related to guild finder inactivity
            packet.Translator.ReadWoWString("New GuildMaster name", nameLength);
        }

        [Parser(Opcode.CMSG_GUILD_SET_RANK_PERMISSIONS, ClientVersionBuild.V4_0_6_13596, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildRank406(Packet packet)
        {
            for (var i = 0; i < 8; ++i)
                packet.Translator.ReadUInt32("Bank Slots", i);

            packet.Translator.ReadUInt32E<GuildRankRightsFlag>("Rights");

            packet.Translator.ReadUInt32("New Rank Id");
            packet.Translator.ReadUInt32("Old Rank Id");

            for (var i = 0; i < 8; ++i)
                packet.Translator.ReadUInt32E<GuildBankRightsFlag>("Tab Rights", i);

            packet.Translator.ReadGuid("Guild GUID");
            packet.Translator.ReadUInt32E<GuildRankRightsFlag>("Old Rights");

            packet.Translator.ReadInt32("Money Per Day");
            packet.Translator.ReadGuid("Player GUID");
            packet.Translator.ReadCString("Rank Name");
        }

        [Parser(Opcode.CMSG_GUILD_SWITCH_RANK, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildSwitchRank(Packet packet)
        {
            packet.Translator.ReadBit("Direction");
            packet.Translator.ReadGuid("Player GUID");
            packet.Translator.ReadUInt32("Rank Id");
        }

        [Parser(Opcode.SMSG_GUILD_RANKS, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleGuildRankServer(Packet packet)
        {
            const int guildBankMaxTabs = 8;

            var count = packet.Translator.ReadUInt32("Rank Count");
            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadUInt32("Creation Order", i);
                packet.Translator.ReadUInt32("Rights Order", i);
                packet.Translator.ReadCString("Name", i);
                packet.Translator.ReadInt32E<GuildRankRightsFlag>("Rights", i);

                for (int j = 0; j < guildBankMaxTabs; j++)
                    packet.Translator.ReadInt32E<GuildBankRightsFlag>("Tab Rights", i, j);

                for (int j = 0; j < guildBankMaxTabs; j++)
                    packet.Translator.ReadInt32("Tab Slots", i, j);

                packet.Translator.ReadInt32("Gold Per Day", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_RANKS, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildRankServer430(Packet packet)
        {
            const int guildBankMaxTabs = 8;
            var count = packet.Translator.ReadBits("Count", 18);
            var length = new int[count];
            for (var i = 0; i < count; ++i)
                length[i] = (int)packet.Translator.ReadBits(7);

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadWoWString("Name", length[i], i);
                packet.Translator.ReadInt32("Creation Order", i);
                for (var j = 0; j < guildBankMaxTabs; ++j)
                {
                    packet.Translator.ReadInt32("Tab Slots", i, j);
                    packet.Translator.ReadInt32E<GuildBankRightsFlag>("Tab Rights", i, j);
                }
                packet.Translator.ReadInt32("Rights order", i);
                packet.Translator.ReadInt32E<GuildRankRightsFlag>("Rights", i);
                packet.Translator.ReadInt32("Gold Per Day", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_RANKS, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleGuildRankServer422(Packet packet)
        {
            const int guildBankMaxTabs = 8;

            var count = packet.Translator.ReadUInt32("Rank Count");
            for (int i = 0; i < count; i++)
            {
                packet.Translator.ReadCString("Name", i);
                packet.Translator.ReadInt32("Creation Order", i);

                for (int j = 0; j < guildBankMaxTabs; j++)
                    packet.Translator.ReadInt32E<GuildBankRightsFlag>("Tab Rights", i, j);

                packet.Translator.ReadInt32("Gold Per Day", i);

                for (int j = 0; j < guildBankMaxTabs; j++)
                    packet.Translator.ReadInt32("Tab Slots", i, j);

                packet.Translator.ReadInt32("Rights Order", i);
                packet.Translator.ReadInt32E<GuildRankRightsFlag>("Rights", i);
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
            packet.Translator.ReadCString("Name");
        }

        [Parser(Opcode.CMSG_GUILD_OFFICER_REMOVE_MEMBER, ClientVersionBuild.V4_0_6_13596, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildRemove406(Packet packet)
        {
            packet.Translator.ReadGuid("Target GUID");
            packet.Translator.ReadGuid("Removee GUID");
        }

        [Parser(Opcode.SMSG_GUILD_INVITE, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleGuildInvite(Packet packet)
        {
            packet.Translator.ReadCString("Invitee Name");
            packet.Translator.ReadCString("Guild Name");
        }

        [Parser(Opcode.SMSG_GUILD_INVITE, ClientVersionBuild.V4_0_6_13596, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildInvite406(Packet packet)
        {
            packet.Translator.ReadUInt32("Emblem Background Color");
            packet.Translator.ReadUInt32("Emblem Border Style");
            packet.Translator.ReadUInt32("Emblem Color");
            packet.Translator.ReadUInt32("Emblem Texture File");
            packet.Translator.ReadGuid("Old Guild GUID");
            packet.Translator.ReadCString("Old Guild Name");
            packet.Translator.ReadCString("Inviter Name");
            packet.Translator.ReadUInt32("Guild Level");
            packet.Translator.ReadUInt32("Emblem Border Color");
            packet.Translator.ReadGuid("Guild GUID");
            packet.Translator.ReadCString("Guild Name");
        }


        [Parser(Opcode.SMSG_GUILD_INFO)]
        public static void HandleGuildInfo(Packet packet)
        {
            packet.Translator.ReadCString("Name");
            packet.Translator.ReadPackedTime("Creation Date");
            packet.Translator.ReadUInt32("Number of Players");
            packet.Translator.ReadUInt32("Number of Accounts");
        }

        [Parser(Opcode.CMSG_GUILD_MOTD, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleGuildMOTD(Packet packet)
        {
            packet.Translator.ReadCString("MOTD");
        }

        [Parser(Opcode.CMSG_GUILD_MOTD, ClientVersionBuild.V4_0_6_13596, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildMOTD406(Packet packet)
        {
            packet.Translator.ReadGuid("Guild GUID");
            packet.Translator.ReadGuid("Player GUID");
            packet.Translator.ReadCString("MOTD");
        }

        [Parser(Opcode.CMSG_GUILD_INFO_TEXT, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleSetGuildInfo(Packet packet)
        {
            packet.Translator.ReadCString("Text");
        }

        [Parser(Opcode.CMSG_GUILD_INFO_TEXT, ClientVersionBuild.V4_0_6_13596, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildInfo406(Packet packet)
        {
            packet.Translator.ReadGuid("Player GUID");
            packet.Translator.ReadGuid("Guild GUID");
            packet.Translator.ReadCString("Text");
        }

        [Parser(Opcode.SMSG_GUILD_EVENT)]
        public static void HandleGuildEvent(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                packet.Translator.ReadByteE<GuildEventType442>("Event Type");
            else
                packet.Translator.ReadByteE<GuildEventType>("Event Type");

            var size = packet.Translator.ReadByte("Param Count");
            for (var i = 0; i < size; i++)
                packet.Translator.ReadCString("Param", i);

            if (packet.CanRead()) // FIXME 4 5 6 16 17 (GuildEventType changed for 4.2.2)
                packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_GUILD_COMMAND_RESULT)]
        [Parser(Opcode.SMSG_GUILD_COMMAND_RESULT_2, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildCommandResult(Packet packet)
        {
            packet.Translator.ReadUInt32E<GuildCommandType>("Command Type");
            packet.Translator.ReadCString("Param");
            packet.Translator.ReadUInt32E<GuildCommandError>("Command Result");
        }

        [Parser(Opcode.MSG_SAVE_GUILD_EMBLEM)]
        public static void HandleGuildEmblem(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.Translator.ReadGuid("GUID");
                ReadEmblemInfo(packet);
            }
            else
                packet.Translator.ReadUInt32E<GuildEmblemError>("Result");
        }

        [Parser(Opcode.CMSG_GUILD_SET_PUBLIC_NOTE)]
        public static void HandleGuildSetPublicNote(Packet packet)
        {
            packet.Translator.ReadCString("Player Name");
            packet.Translator.ReadCString("Public Note");
        }

        [Parser(Opcode.CMSG_GUILD_SET_OFFICER_NOTE)]
        public static void HandleGuildSetOfficerNote(Packet packet)
        {
            packet.Translator.ReadCString("Player Name");
            packet.Translator.ReadCString("Officer Note");
        }

        [Parser(Opcode.CMSG_GUILD_SET_NOTE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildSetNote(Packet packet)
        {
            packet.Translator.ReadBit("Public");
            packet.Translator.ReadGuid("Target GUID");
            packet.Translator.ReadGuid("Issuer GUID");
            packet.Translator.ReadGuid("Guild GUID");
            packet.Translator.ReadCString("Note");
        }

        [Parser(Opcode.CMSG_GUILD_BANK_ACTIVATE)]
        public static void HandleGuildBankerActivate(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadBool("Full Slot List");
        }

        [Parser(Opcode.CMSG_GUILD_BANK_QUERY_TAB)]
        public static void HandleGuildBankQueryTab(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_2_14545)
                || ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595))
                packet.Translator.ReadByte("Tab Id");
            packet.Translator.ReadBool("Full Slot List"); // false = only slots updated in last operation are shown. True = all slots updated
        }

        [Parser(Opcode.SMSG_GUILD_BANK_QUERY_RESULTS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildBankList(Packet packet)
        {
            packet.Translator.ReadUInt64("Money");
            var tabId = packet.Translator.ReadByte("Tab Id");
            packet.Translator.ReadInt32("Remaining Withdraw");
            if (packet.Translator.ReadBool("Full Slot List") && tabId == 0)
            {
                var size = packet.Translator.ReadByte("Number of Tabs");
                for (var i = 0; i < size; i++)
                {
                    packet.Translator.ReadCString("Tab Name", i);
                    packet.Translator.ReadCString("Tab Icon", i);
                }
            }

            var slots = packet.Translator.ReadByte("Number of Slots");
            for (var i = 0; i < slots; i++)
            {
                packet.Translator.ReadByte("Slot Id", i);
                var entry = packet.Translator.ReadInt32<ItemId>("Item Entry", i);
                if (entry > 0)
                {
                    packet.Translator.ReadUInt32E<UnknownFlags>("Unk mask", i);
                    var ramdonEnchant = packet.Translator.ReadInt32("Random Item Property Id", i);
                    if (ramdonEnchant != 0)
                        packet.Translator.ReadUInt32("Item Suffix Factor", i);
                    packet.Translator.ReadUInt32("Stack Count", i);
                    packet.Translator.ReadUInt32("Unk Uint32 2", i); // Only seen 0
                    packet.Translator.ReadByte("Spell Charges", i);
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                    {
                        packet.Translator.ReadInt32("Unk 1 Int32", i);
                        packet.Translator.ReadInt32("Unk 2 Int32", i);
                    }

                    var enchantment = packet.Translator.ReadByte("Number of Enchantments", i);
                    for (var j = 0; j < enchantment; j++)
                    {
                        packet.Translator.ReadByte("Enchantment Slot Id", i, j);
                        packet.Translator.ReadUInt32("Enchantment Id", i, j);
                    }
                }
            }
        }

        [Parser(Opcode.CMSG_GUILD_BANK_SWAP_ITEMS)]
        public static void HandleGuildBankSwapItems(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            var bankToBank = packet.Translator.ReadBool("BankToBank");
            if (bankToBank)
            {
                packet.Translator.ReadByte("Dest Tab Id");
                packet.Translator.ReadByte("Dest Slot Id");
                packet.Translator.ReadInt32<ItemId>("Dest Item Entry");
                packet.Translator.ReadByte("Tab Id");
                packet.Translator.ReadByte("Slot Id");
                packet.Translator.ReadInt32<ItemId>("Item Entry");
                packet.Translator.ReadByte("Unk Byte 1");
                packet.Translator.ReadUInt32("Amount");
            }
            else
            {
                packet.Translator.ReadByte("Tab Id");
                packet.Translator.ReadByte("Slot Id");
                packet.Translator.ReadInt32<ItemId>("Item Entry");
                var autostore = packet.Translator.ReadBool("Autostore");
                if (autostore)
                {
                    packet.Translator.ReadUInt32("Autostore Count");
                    packet.Translator.ReadBool("From Bank To Player");
                    packet.Translator.ReadUInt32("Unk Uint32 2");
                }
                else
                {
                    packet.Translator.ReadByte("Bag");
                    packet.Translator.ReadByte("Slot");
                    packet.Translator.ReadBool("From Bank To Player");
                    packet.Translator.ReadUInt32("Amount");
                }
            }
        }

        [Parser(Opcode.CMSG_GUILD_BANK_BUY_TAB)]
        public static void HandleGuildBankBuyTab(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadByte("Tab Id");
        }

        [Parser(Opcode.CMSG_GUILD_BANK_UPDATE_TAB)]
        public static void HandleGuildBankUpdateTab(Packet packet)
        {
            packet.Translator.ReadGuid("Banker");
            packet.Translator.ReadByte("BankTab");
            packet.Translator.ReadCString("Name");
            packet.Translator.ReadCString("Icon");
        }

        [Parser(Opcode.CMSG_GUILD_GET_RANKS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        [Parser(Opcode.CMSG_GUILD_QUERY_NEWS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_GUILD_REQUEST_MAX_DAILY_XP, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        [Parser(Opcode.CMSG_REQUEST_GUILD_XP, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        [Parser(Opcode.CMSG_GUILD_QUERY_TRADESKILL)]
        public static void HandleGuildRequestMulti(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_GUILD_REQUEST_MAX_DAILY_XP, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildRequestMaxDailyXP430(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(6, 5, 2, 3, 0, 4, 7, 1);
            packet.Translator.ParseBitStream(guid, 1, 6, 4, 5, 3, 7, 0, 2);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_XP, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleRequestGuildXP430(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(2, 4, 5, 6, 1, 0, 3, 7);
            packet.Translator.ParseBitStream(guid, 0, 1, 4, 3, 2, 6, 7, 5);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_GUILD_REQUEST_MAX_DAILY_XP, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleGuildRequestMaxDailyXP510(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(5, 3, 6, 4, 7, 2, 1, 0);
            packet.Translator.ParseBitStream(guid, 4, 7, 5, 6, 0, 1, 2, 3);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_GUILD_GET_RANKS, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleGuildRanks43(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(7, 2, 0, 4, 6, 5, 1, 3);
            packet.Translator.ParseBitStream(guid, 7, 5, 2, 6, 1, 4, 0, 3);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GUILD_GET_RANKS, ClientVersionBuild.V4_3_3_15354, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildRanks433(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(2, 6, 1, 0, 5, 3, 7, 4);
            packet.Translator.ParseBitStream(guid, 3, 6, 5, 4, 0, 7, 2, 1);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_GUILD_XP)]
        public static void HandleGuildXP(Packet packet)
        {
            packet.Translator.ReadUInt64("Member Total XP");
            packet.Translator.ReadUInt64("Guild XP for next Level");
            packet.Translator.ReadUInt64("Guild XP Today");
            packet.Translator.ReadUInt64("Member Weekly XP");
            packet.Translator.ReadUInt64("Guild Current XP");
        }

        [Parser(Opcode.SMSG_GUILD_NEWS_UPDATE, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleGuildNewsUpdate(Packet packet)
        {
            var size = packet.Translator.ReadUInt32("Size");

            for (var i = 0; i < size; ++i)
            {
                var unk1 = packet.Translator.ReadUInt32("Unk count", i);
                for (var j = 0; j < unk1; ++j)
                    packet.Translator.ReadUInt64("Unk uint64", i, j);
            }

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadPackedTime("Time", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadGuid("Player GUID", i);

            for (var i = 0; i < size; ++i)
            {
                packet.Translator.ReadUInt32("Item/Achievement", i);
                packet.Translator.ReadUInt32("Unk", i);
            }

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadInt32E<GuildNewsType>("News Type", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadUInt32("Guild/Player news", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadUInt32("Unk UInt32 4", i);
        }

        [Parser(Opcode.SMSG_GUILD_NEWS_UPDATE, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildNewsUpdate422(Packet packet)
        {
            var size = packet.Translator.ReadUInt32("Size");
            for (var i = 0; i < size; ++i)
                packet.Translator.ReadUInt32("Guild/Player news", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadGuid("Player GUID", i);

            for (var i = 0; i < size; ++i)
            {
                var unk1 = packet.Translator.ReadUInt32("Unk count", i);
                for (var j = 0; j < unk1; ++j)
                    packet.Translator.ReadUInt64("Unk uint64", i, j);
            }

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadInt32E<GuildNewsType>("News Type", i);

            for (var i = 0; i < size; ++i)
            {
                packet.Translator.ReadUInt32("Item/Achievement", i);
                packet.Translator.ReadUInt32("Unk", i);
            }

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadUInt32("Unk UInt32 4", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadPackedTime("Time", i);
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_REWARDS_LIST)]
        public static void HandleRequestGuildRewardsList(Packet packet)
        {
            packet.Translator.ReadTime("CurrentVersion");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_3_4_15595))
                packet.Translator.ReadGuid("Player GUID");
        }

        [Parser(Opcode.SMSG_GUILD_REWARD_LIST, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildRewardsList(Packet packet)
        {
            packet.Translator.ReadUInt32("Guild Id");
            var size = packet.Translator.ReadUInt32("Size");

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadUInt32("Unk UInt32 1", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadUInt32("Unk UInt32 2", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadUInt64("Price", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadInt32<AchievementId>("Achievement Id", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadUInt32E<ReputationRank>("Faction Standing", i);

            for (var i = 0; i < size; ++i)
                packet.Translator.ReadUInt32<ItemId>("Item Id", i);
        }

        [Parser(Opcode.CMSG_GUILD_BANK_DEPOSIT_MONEY)]
        [Parser(Opcode.CMSG_GUILD_BANK_WITHDRAW_MONEY)]
        public static void HandleGuildBankDepositMoney(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                packet.Translator.ReadUInt64("Money");
            else
                packet.Translator.ReadUInt32("Money");
        }

        [Parser(Opcode.CMSG_GUILD_BANK_TEXT_QUERY, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleQueryGuildBankText(Packet packet)
        {
            packet.Translator.ReadUInt32("Tab Id");
        }

        [Parser(Opcode.MSG_QUERY_GUILD_BANK_TEXT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildQueryBankText(Packet packet)
        {
            packet.Translator.ReadByte("Tab Id");
            if (packet.Direction == Direction.ServerToClient)
                packet.Translator.ReadCString("Text");
        }

        [Parser(Opcode.CMSG_GUILD_BANK_SET_TAB_TEXT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildSetBankText(Packet packet)
        {
            packet.Translator.ReadByte("Tab Id");
            packet.Translator.ReadCString("Tab Text");
        }

        [Parser(Opcode.MSG_GUILD_PERMISSIONS)]
        public static void HandleGuildPermissions(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
                return;

            packet.Translator.ReadUInt32("Rank Id");
            packet.Translator.ReadUInt32E<GuildRankRightsFlag>("Rights");
            packet.Translator.ReadInt32("Remaining Money");
            packet.Translator.ReadByte("Tab size");
            var tabSize = ClientVersion.AddedInVersion(ClientType.Cataclysm) ? 8 : 6;
            for (var i = 0; i < tabSize; i++)
            {
                packet.Translator.ReadInt32E<GuildBankRightsFlag>("Tab Rights", i);
                packet.Translator.ReadInt32("Tab Slots", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_PERMISSIONS_QUERY_RESULTS)]
        public static void HandleGuildPermissionsQueryResult(Packet packet)
        {
            packet.Translator.ReadUInt32("Rank Id");
            packet.Translator.ReadInt32("Purchased Tab size");
            packet.Translator.ReadUInt32E<GuildRankRightsFlag>("Rights");
            packet.Translator.ReadInt32("Remaining Money");
            packet.Translator.ReadBits("Tab size", 23);
            for (var i = 0; i < 8; i++)
            {
                packet.Translator.ReadInt32E<GuildBankRightsFlag>("Tab Rights", i);
                packet.Translator.ReadInt32("Tab Slots", i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_BANK_REMAINING_WITHDRAW_MONEY_QUERY)]
        [Parser(Opcode.MSG_GUILD_BANK_MONEY_WITHDRAWN)]
        public static void HandleGuildBankMoneyWithdrawn(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                    packet.Translator.ReadInt64("Remaining Money");
                else
                    packet.Translator.ReadInt32("Remaining Money");
            }
        }

        [Parser(Opcode.SMSG_GUILD_BANK_REMAINING_WITHDRAW_MONEY)]
        public static void HandleGuildBankMoneyWithdrawnResponse(Packet packet)
        {
            packet.Translator.ReadInt64("Remaining Money");
        }

        [Parser(Opcode.MSG_GUILD_EVENT_LOG_QUERY)]
        public static void HandleGuildEventLogQuery(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
                return;

            var size = packet.Translator.ReadByte("Log size");
            for (var i = 0; i < size; i++)
            {
                var type = packet.Translator.ReadByteE<GuildEventLogType>("Type");
                packet.Translator.ReadGuid("GUID");
                if (type != GuildEventLogType.JoinGuild && type != GuildEventLogType.LeaveGuild)
                    packet.Translator.ReadGuid("GUID 2");
                if (type == GuildEventLogType.PromotePlayer || type == GuildEventLogType.DemotePlayer)
                    packet.Translator.ReadByte("Rank");
                packet.Translator.ReadUInt32("Time Ago");
            }
        }

        [Parser(Opcode.MSG_GUILD_BANK_LOG_QUERY, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildBankLogQueryResult(Packet packet)
        {
            packet.Translator.ReadByte("Tab Id");
            if (packet.Direction == Direction.ServerToClient)
            {
                var size = packet.Translator.ReadByte("Size");
                for (var i = 0; i < size; i++)
                {
                    var type = packet.Translator.ReadByteE<GuildBankEventLogType>("Bank Log Event Type", i);
                    packet.Translator.ReadGuid("GUID", i);
                    if (type == GuildBankEventLogType.BuySlot)
                        packet.Translator.ReadUInt32("Cost", i);
                    else
                    {
                        if (type == GuildBankEventLogType.DepositMoney ||
                            type == GuildBankEventLogType.WithdrawMoney ||
                            type == GuildBankEventLogType.RepairMoney)
                            packet.Translator.ReadUInt32("Money", i);
                        else
                        {
                            packet.Translator.ReadUInt32("Item Entry", i);
                            packet.Translator.ReadUInt32("Stack Count", i);
                            if (type == GuildBankEventLogType.MoveItem ||
                                type == GuildBankEventLogType.MoveItem2)
                                packet.Translator.ReadByte("Tab Id", i);
                        }
                    }
                    packet.Translator.ReadUInt32("Time", i);
                }
            }
        }

        [Parser(Opcode.CMSG_GUILD_BANK_LOG_QUERY, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleGuildBankLogQuery(Packet packet)
        {
            packet.Translator.ReadUInt32("Tab Id");
        }

        [Parser(Opcode.SMSG_GUILD_DECLINE)]
        public static void HandleGuildDecline(Packet packet)
        {
            packet.Translator.ReadCString("Reason");
            packet.Translator.ReadBool("Auto decline");
        }

        [Parser(Opcode.SMSG_PETITION_SHOW_LIST)]
        public static void HandlePetitionShowList(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            var counter = packet.Translator.ReadByte("Counter");
            for (var i = 0; i < counter; i++)
            {
                packet.Translator.ReadUInt32("Index");
                packet.Translator.ReadUInt32("Charter Entry");
                packet.Translator.ReadUInt32("Charter Display");
                packet.Translator.ReadUInt32("Charter Cost");
                packet.Translator.ReadUInt32("Unk Uint32 1");
                packet.Translator.ReadUInt32("Required signs");
            }
        }

        [Parser(Opcode.CMSG_PETITION_BUY)]
        public static void HandlePetitionBuy(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadUInt32("Unk UInt32 1");
            packet.Translator.ReadUInt64("Unk UInt64 1");
            packet.Translator.ReadCString("Name");
            packet.Translator.ReadCString("Text");
            packet.Translator.ReadUInt32("Unk UInt32 2");
            packet.Translator.ReadUInt32("Unk UInt32 3");
            packet.Translator.ReadUInt32("Unk UInt32 4");
            packet.Translator.ReadUInt32("Unk UInt32 5");
            packet.Translator.ReadUInt32("Unk UInt32 6");
            packet.Translator.ReadUInt32("Unk UInt32 7");
            packet.Translator.ReadUInt32("Unk UInt32 8");
            packet.Translator.ReadUInt16("Unk UInt16 1");
            packet.Translator.ReadUInt32("Unk UInt32 9");
            packet.Translator.ReadUInt32("Unk UInt32 10");
            packet.Translator.ReadUInt32("Unk UInt32 11");

            for (var i = 0; i < 10; i++)
                packet.Translator.ReadCString("Unk String", i);

            packet.Translator.ReadUInt32("Client Index");
            packet.Translator.ReadUInt32("Unk UInt32 12");
        }

        [Parser(Opcode.CMSG_PETITION_SHOW_SIGNATURES)]
        public static void HandlePetitionShowSignatures(Packet packet)
        {
            packet.Translator.ReadGuid("Petition GUID");
        }

        [Parser(Opcode.SMSG_PETITION_SHOW_SIGNATURES)]
        public static void HandlePetitionShowSignaturesServer(Packet packet)
        {
            packet.Translator.ReadGuid("Petition GUID");
            packet.Translator.ReadGuid("Owner GUID");
            packet.Translator.ReadUInt32("Guild/Team GUID");
            var counter = packet.Translator.ReadByte("Sign count");
            for (var i = 0; i < counter; i++)
            {
                packet.Translator.ReadGuid("Player GUID", i);
                packet.Translator.ReadUInt32("Unk UInt32 1", i);
            }
        }

        [Parser(Opcode.CMSG_PETITION_SIGN)]
        public static void HandlePetitionSign(Packet packet)
        {
            packet.Translator.ReadGuid("Petition GUID");
            packet.Translator.ReadByte("Unk Byte 1");
        }

        [Parser(Opcode.SMSG_PETITION_SIGN_RESULTS)]
        public static void HandlePetitionSignResult(Packet packet)
        {
            packet.Translator.ReadGuid("Petition GUID");
            packet.Translator.ReadGuid("Player GUID");
            packet.Translator.ReadUInt32E<PetitionResultType>("Petition Result");
        }

        [Parser(Opcode.MSG_PETITION_DECLINE)]
        public static void HandlePetitionDecline(Packet packet)
        {
            packet.Translator.ReadGuid(packet.Direction == Direction.ClientToServer ? "Petition GUID" : "Player GUID");
        }

        [Parser(Opcode.CMSG_OFFER_PETITION)]
        public static void HandlePetitionOffer(Packet packet)
        {
            packet.Translator.ReadUInt32("Unk UInt3 1");
            packet.Translator.ReadGuid("Petition GUID");
            packet.Translator.ReadGuid("Target GUID");
        }

        [Parser(Opcode.CMSG_TURN_IN_PETITION)]
        public static void HandlePetitionTurnIn(Packet packet)
        {
            packet.Translator.ReadGuid("Petition GUID");
        }

        [Parser(Opcode.SMSG_TURN_IN_PETITION_RESULT)]
        public static void HandlePetitionTurnInResults(Packet packet)
        {
            packet.Translator.ReadUInt32E<PetitionResultType>("Result");
        }

        [Parser(Opcode.CMSG_PETITION_QUERY)]
        public static void HandlePetitionQuery(Packet packet)
        {
            packet.Translator.ReadUInt32("Guild/Team GUID");
            packet.Translator.ReadGuid("Petition GUID");
        }

        [Parser(Opcode.SMSG_PETITION_QUERY_RESPONSE)]
        public static void HandlePetitionQueryResponse(Packet packet)
        {
            packet.Translator.ReadUInt32("Guild/Team GUID");
            packet.Translator.ReadGuid("Owner GUID");
            packet.Translator.ReadCString("Name");
            packet.Translator.ReadCString("Text");
            packet.Translator.ReadUInt32("Signs Needed");
            packet.Translator.ReadUInt32("Signs Needed");
            packet.Translator.ReadUInt32("Unk UInt32 4");
            packet.Translator.ReadUInt32("Unk UInt32 5");
            packet.Translator.ReadUInt32("Unk UInt32 6");
            packet.Translator.ReadUInt32("Unk UInt32 7");
            packet.Translator.ReadUInt32("Unk UInt32 8");
            packet.Translator.ReadUInt16("Unk UInt16 1");
            packet.Translator.ReadUInt32("Unk UInt32 (Level?)");
            packet.Translator.ReadUInt32("Unk UInt32 (Level?)");
            packet.Translator.ReadUInt32("Unk UInt32 11");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                for (var i = 0; i < 10; i++)
                    packet.Translator.ReadCString("Unk String", i);

            packet.Translator.ReadUInt32("Client Index");
            packet.Translator.ReadUInt32("Petition Type (0: Guild / 1: Arena)");
        }

        [Parser(Opcode.MSG_PETITION_RENAME)]
        public static void HandlePetitionRename(Packet packet)
        {
            packet.Translator.ReadGuid("Petition GUID");
            packet.Translator.ReadCString("New Name");
        }

        [Parser(Opcode.SMSG_OFFER_PETITION_ERROR)]
        public static void HandlePetitionError(Packet packet)
        {
            packet.Translator.ReadGuid("Petition GUID");
        }

        [Parser(Opcode.SMSG_GUILD_REPUTATION_WEEKLY_CAP)]
        public static void HandleGuildReputationWeeklyCap(Packet packet)
        {
            packet.Translator.ReadUInt32("Cap");
        }

        [Parser(Opcode.CMSG_GUILD_SET_ACHIEVEMENT_TRACKING)]
        public static void HandleGuildSetAchievementTracking(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 24);
            for (var i = 0; i < count; ++i)
                packet.Translator.ReadInt32<AchievementId>("Achievement Id", i);
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_RECIPES, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleQueryGuildRecipes510(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(1, 4, 3, 5, 0, 6, 2, 7);
            packet.Translator.ParseBitStream(guid, 5, 3, 1, 4, 0, 7, 6, 2);
            packet.Translator.WriteGuid("Guild Guid", guid);
        }

        [Parser(Opcode.SMSG_GUILD_MAX_DAILY_XP)]
        [Parser(Opcode.SMSG_GUILD_XP_GAIN)]
        public static void HandleGuildXPResponse(Packet packet)
        {
            packet.Translator.ReadInt64("XP");
        }

        [Parser(Opcode.CMSG_GUILD_SET_FOCUSED_ACHIEVEMENT)]
        public static void HandleGuildAchievementProgressQuery(Packet packet)
        {
            packet.Translator.ReadInt32<AchievementId>("Achievement Id");
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER_UPDATE)]
        public static void HandleGuildRosterUpdate(Packet packet)
        {
            uint count = packet.Translator.ReadBits("Count", 18);
            var guids = new byte[count][];
            var strlen = new uint[count][];
            var param = new bool[count][];

            for (int i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                strlen[i] = new uint[3];
                param[i] = new bool[2];

                strlen[i][2] = packet.Translator.ReadBits(7);
                guids[i][5] = packet.Translator.ReadBit();
                guids[i][4] = packet.Translator.ReadBit();
                guids[i][0] = packet.Translator.ReadBit();
                guids[i][7] = packet.Translator.ReadBit();
                guids[i][1] = packet.Translator.ReadBit();
                guids[i][3] = packet.Translator.ReadBit();
                strlen[i][0] = packet.Translator.ReadBits(8);
                guids[i][6] = packet.Translator.ReadBit();
                param[i][0] = packet.Translator.ReadBit();
                guids[i][2] = packet.Translator.ReadBit();
                param[i][1] = packet.Translator.ReadBit();
                strlen[i][1] = packet.Translator.ReadBits(8);
            }

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadByte("unk Byte 359", i); // 0 or 1
                packet.Translator.ReadUInt32("Remaining guild week Rep", i);

                packet.Translator.ReadXORByte(guids[i], 1);

                packet.Translator.ReadInt64("Total activity", i);

                packet.Translator.ReadXORByte(guids[i], 2);

                for (int j = 0; j < 2; ++j)
                {
                    uint rank = packet.Translator.ReadUInt32();
                    uint id = packet.Translator.ReadUInt32();
                    uint value = packet.Translator.ReadUInt32();
                    packet.AddValue("Profession", $"Id {id} - Value {value} - Rank {rank}", i, j);
                }

                packet.Translator.ReadXORByte(guids[i], 0);
                packet.Translator.ReadXORByte(guids[i], 6);
                packet.Translator.ReadXORByte(guids[i], 7);

                packet.Translator.ReadInt32("Rank", i);
                packet.Translator.ReadWoWString("Public Comment", strlen[i][0], i);
                packet.Translator.ReadWoWString("Officers Comment", strlen[i][1], i);

                packet.Translator.ReadXORByte(guids[i], 4);
                packet.Translator.ReadXORByte(guids[i], 5);

                packet.Translator.ReadInt32("Guild Reputation", i);
                packet.Translator.ReadInt32("Member Achievement Points", i);
                packet.Translator.ReadSingle("Last Online", i);
                packet.Translator.ReadInt64("Week activity", i);
                packet.Translator.ReadByte("Level", i);
                packet.Translator.ReadByteE<Class>("Class", i);

                packet.Translator.ReadXORByte(guids[i], 3);

                packet.Translator.ReadByte("unk Byte 356", i); // 0
                packet.Translator.ReadInt32<ZoneId>("Zone Id", i);
                packet.Translator.ReadWoWString("Character Name", strlen[i][2], i);
                packet.Translator.ReadInt32("unk Int32 44", i);
                packet.AddValue("Can SoR", param[i][0], i);
                packet.AddValue("Has Authenticator", param[i][1], i);

                packet.Translator.WriteGuid("Player Guid", guids[i], i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_MEMBER_UPDATE_NOTE)]
        public static void HandleGuildMemberUpdateNote(Packet packet)
        {
            var guid = new byte[8];

            guid[7] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var len = packet.Translator.ReadBits("Note Text Length", 8);
            guid[5] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Public");
            guid[1] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 5);

            packet.Translator.ReadWoWString("Note", len);

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 4);

            packet.Translator.WriteGuid("Guid", guid);

        }

        [Parser(Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleGuildAchievementData(Packet packet)
        {
            var cnt = packet.Translator.ReadUInt32("Count");
            for (var i = 0; i < cnt; ++i)
                packet.Translator.ReadPackedTime("Date", i);

            for (var i = 0; i < cnt; ++i)
                packet.Translator.ReadUInt32("Achievement Id", i);
        }

        [Parser(Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleGuildAchievementData430(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 23);
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadPackedTime("Date", i);
                packet.Translator.ReadInt32<AchievementId>("Achievement Id", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_CRITERIA_DATA, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildCriteriaData(Packet packet)
        {
            var criterias = packet.Translator.ReadUInt32("Criterias");

            for (var i = 0; i < criterias; ++i)
                packet.Translator.ReadGuid("Player GUID", i);

            for (var i = 0; i < criterias; ++i)
                packet.Translator.ReadUInt32("Criteria Timer 1", i);

            for (var i = 0; i < criterias; ++i)
                packet.Translator.ReadUInt32("Criteria Timer 2", i);

            for (var i = 0; i < criterias; ++i)
                packet.Translator.ReadUInt64("Counter", i);

            for (var i = 0; i < criterias; ++i)
                packet.Translator.ReadPackedTime("Criteria Time", i);

            for (var i = 0; i < criterias; ++i)
                packet.Translator.ReadUInt32("Criteria Id", i);

            for (var i = 0; i < criterias; ++i)
                packet.Translator.ReadUInt32("Flag", i);
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
