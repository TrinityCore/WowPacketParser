using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class GuildFinderHandler
    {

        [Parser(Opcode.CMSG_LF_GUILD_BROWSE)]
        public static void HandleGuildFinderBrowse(Packet packet)
        {
            packet.Translator.ReadUInt32E<GuildFinderOptionsRoles>("Class Roles");
            packet.Translator.ReadUInt32E<GuildFinderOptionsAvailability>("Availability");
            packet.Translator.ReadUInt32E<GuildFinderOptionsInterest>("Guild Interests");
            packet.Translator.ReadUInt32("Player Level");
        }

        [Parser(Opcode.CMSG_LF_GUILD_SET_GUILD_POST, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildFinderSetGuildPost422(Packet packet)
        {
            packet.Translator.ReadBit("Join");
            packet.Translator.ReadUInt32E<GuildFinderOptionsAvailability>("Availability");
            packet.Translator.ReadUInt32E<GuildFinderOptionsRoles>("Class Roles");
            packet.Translator.ReadUInt32E<GuildFinderOptionsInterest>("Guild Interests");
            packet.Translator.ReadUInt32E<GuildFinderOptionsLevel>("Level");
            packet.Translator.ReadCString("Comment");
        }

        [Parser(Opcode.CMSG_LF_GUILD_SET_GUILD_POST, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildFinderSetGuildPost434(Packet packet)
        {
            packet.Translator.ReadUInt32E<GuildFinderOptionsLevel>("Level");
            packet.Translator.ReadUInt32E<GuildFinderOptionsAvailability>("Availability");
            packet.Translator.ReadUInt32E<GuildFinderOptionsInterest>("Guild Interests");
            packet.Translator.ReadUInt32E<GuildFinderOptionsRoles>("Class Roles");
            var length = packet.Translator.ReadBits(11);
            packet.Translator.ReadBit("Listed");
            packet.Translator.ReadWoWString("Comment", length);
        }

        [Parser(Opcode.SMSG_LF_GUILD_POST_UPDATED)]
        public static void HandleGuildFinderPostUpdated(Packet packet)
        {
            var b = packet.Translator.ReadBit("Unk Bit"); // Can set settings ?

            if (b != 0)
            {
                var length = packet.Translator.ReadBits(11);
                packet.Translator.ReadBit("Listed");
                // Flush bits
                packet.Translator.ReadUInt32E<GuildFinderOptionsLevel>("Level");
                packet.Translator.ReadWoWString("Comment", length);
                packet.Translator.ReadInt32("Unk Int32");
                packet.Translator.ReadUInt32E<GuildFinderOptionsAvailability>("Availability");
                packet.Translator.ReadUInt32E<GuildFinderOptionsRoles>("Class Roles");
                packet.Translator.ReadUInt32E<GuildFinderOptionsInterest>("Guild Interests");
            }
        }

        [Parser(Opcode.SMSG_LF_GUILD_COMMAND_RESULT)]
        public static void HandleGuildFinderCommandResult(Packet packet)
        {
            packet.Translator.ReadByte("Unk Byte"); // == 0 -> ERR_GUILD_INTERNAL
            packet.Translator.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_LF_GUILD_BROWSE_UPDATED, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandlerLFGuildBrowseUpdated422(Packet packet)
        {
            var count = packet.Translator.ReadInt32("Count");
            if (count == 0)
                return;
            var guids = new byte[count][];

            for (var i = 0; i < count; ++i)
                guids[i] = packet.Translator.StartBitStream(7, 4, 5, 0, 2, 6, 1, 3);

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("Guild Emblem Border Color", i);

                packet.Translator.ReadXORByte(guids[i], 4);

                packet.Translator.ReadCString("Guild Description", i);

                packet.Translator.ReadXORByte(guids[i], 6);

                packet.Translator.ReadInt32("Guild Emblem Texture File", i);
                packet.Translator.ReadInt32("Guild Level", i);

                packet.Translator.ReadXORByte(guids[i], 5);

                packet.Translator.ReadInt32("Unk 2", i);
                packet.Translator.ReadUInt32E<GuildFinderOptionsRoles>("Class Roles", i);
                packet.Translator.ReadCString("Guild Name", i);
                packet.Translator.ReadByte("Cached", i);

                packet.Translator.ReadXORByte(guids[i], 3);

                packet.Translator.ReadInt32("Achievement Points", i);

                packet.Translator.ReadXORByte(guids[i], 0);

                packet.Translator.ReadInt32("Guild Emblem Color", i);
                packet.Translator.ReadInt32("Guild Emblem Background Color", i);
                packet.Translator.ReadByte("Request Pending", i);
                packet.Translator.ReadUInt32E<GuildFinderOptionsInterest>("Guild Interests", i);
                packet.Translator.ReadUInt32E<GuildFinderOptionsAvailability>("Availability", i);

                packet.Translator.ReadXORByte(guids[i], 7);

                packet.Translator.ReadInt32("Number of members", i);

                packet.Translator.ReadXORByte(guids[i], 2);

                packet.Translator.ReadInt32("Unk 5", i);

                packet.Translator.ReadXORByte(guids[i], 1);

                packet.Translator.WriteGuid("Guild GUID", guids[i], i);
            }
        }

        [Parser(Opcode.SMSG_LF_GUILD_BROWSE_UPDATED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandlerLFGuildBrowseUpdated434(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 19);

            var guids = new byte[count][];
            var strlen = new uint[count][];

            for (int i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                strlen[i] = new uint[2];

                guids[i][7] = packet.Translator.ReadBit();
                guids[i][5] = packet.Translator.ReadBit();
                strlen[i][1] = packet.Translator.ReadBits(8);
                guids[i][0] = packet.Translator.ReadBit();
                strlen[i][0] = packet.Translator.ReadBits(11);
                guids[i][4] = packet.Translator.ReadBit();
                guids[i][1] = packet.Translator.ReadBit();
                guids[i][2] = packet.Translator.ReadBit();
                guids[i][6] = packet.Translator.ReadBit();
                guids[i][3] = packet.Translator.ReadBit();
            }

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("Tabard Emblem Color", i);
                packet.Translator.ReadInt32("Tabard Border Style", i); // Guessed from sniffs
                packet.Translator.ReadInt32("Tabard Icon", i);
                packet.Translator.ReadWoWString("Comment", strlen[i][0], i);
                packet.Translator.ReadBool("Cached", i);

                packet.Translator.ReadXORByte(guids[i], 5);

                packet.Translator.ReadUInt32E<GuildFinderOptionsInterest>("Guild Interests", i);

                packet.Translator.ReadXORByte(guids[i], 6);
                packet.Translator.ReadXORByte(guids[i], 4);

                packet.Translator.ReadInt32("Level", i);
                packet.Translator.ReadWoWString("Name", strlen[i][1], i);
                packet.Translator.ReadInt32("Achievement Points", i);

                packet.Translator.ReadXORByte(guids[i], 7);

                packet.Translator.ReadBool("Request Pending", i);

                packet.Translator.ReadXORByte(guids[i], 2);
                packet.Translator.ReadXORByte(guids[i], 0);

                packet.Translator.ReadUInt32E<GuildFinderOptionsAvailability>("Availability", i);

                packet.Translator.ReadXORByte(guids[i], 1);

                packet.Translator.ReadInt32("Tabard Background Color", i);
                packet.Translator.ReadInt32("Unk Int 2", i); // + 128 (Always 0 or 1)
                packet.Translator.ReadInt32("Tabard Border Color", i);
                packet.Translator.ReadUInt32E<GuildFinderOptionsRoles>("Class Roles", i);

                packet.Translator.ReadXORByte(guids[i], 3);

                packet.Translator.ReadInt32("Number of Members", i);

                packet.Translator.WriteGuid("Guild Guid", guids[i], i);
            }
        }

        [Parser(Opcode.CMSG_LF_GUILD_GET_RECRUITS)]
        public static void HandlerLFGuildGetRecruits(Packet packet)
        {
            packet.Translator.ReadTime("Unk Time");
        }

        [Parser(Opcode.SMSG_LF_GUILD_RECRUIT_LIST_UPDATED)] // 4.3.4
        public static void HandlerLFGuildRecruitListUpdated(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 20);

            var guids = new byte[count][];
            var strlen = new uint[count][];

            for (int i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                strlen[i] = new uint[2];

                strlen[i][0] = packet.Translator.ReadBits(11);
                guids[i][2] = packet.Translator.ReadBit();
                guids[i][4] = packet.Translator.ReadBit();
                guids[i][3] = packet.Translator.ReadBit();
                guids[i][7] = packet.Translator.ReadBit();
                guids[i][0] = packet.Translator.ReadBit();
                strlen[i][1] = packet.Translator.ReadBits(7);
                guids[i][5] = packet.Translator.ReadBit();
                guids[i][1] = packet.Translator.ReadBit();
                guids[i][6] = packet.Translator.ReadBit();
            }

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORByte(guids[i], 4);

                packet.Translator.ReadInt32("Unk Int32 1", i); // Is expired ?

                packet.Translator.ReadXORByte(guids[i], 3);
                packet.Translator.ReadXORByte(guids[i], 0);
                packet.Translator.ReadXORByte(guids[i], 1);

                packet.Translator.ReadInt32("Player level", i);

                packet.Translator.ReadXORByte(guids[i], 6);
                packet.Translator.ReadXORByte(guids[i], 7);
                packet.Translator.ReadXORByte(guids[i], 2);

                packet.Translator.ReadInt32("Time Since", i); // Time (in seconds) since the application was submitted.
                packet.Translator.ReadUInt32E<GuildFinderOptionsAvailability>("Availability", i);
                packet.Translator.ReadUInt32E<GuildFinderOptionsRoles>("Class Roles", i);
                packet.Translator.ReadUInt32E<GuildFinderOptionsInterest>("Guild Interests", i);
                packet.Translator.ReadInt32("Time Left", i); // Time (in seconds) until the application will expire.

                packet.Translator.ReadWoWString("Character Name", strlen[i][1], i);
                packet.Translator.ReadWoWString("Comment", strlen[i][0], i);

                packet.Translator.ReadUInt32E<Class>("Class", i);

                packet.Translator.ReadXORByte(guids[i], 5);

                packet.Translator.WriteGuid("Guid", guids[i], i);
            }

            packet.Translator.ReadTime("Unk Time");
        }

        [Parser(Opcode.SMSG_LF_GUILD_MEMBERSHIP_LIST_UPDATED)]
        public static void HandlerLFGuildMembershipListUpdated(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 20);

            var guids = new byte[count][];
            var strlen = new uint[count][];

            for (int i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                strlen[i] = new uint[2];

                guids[i][1] = packet.Translator.ReadBit();
                guids[i][0] = packet.Translator.ReadBit();
                guids[i][5] = packet.Translator.ReadBit();
                strlen[i][0] = packet.Translator.ReadBits(11);
                guids[i][3] = packet.Translator.ReadBit();
                guids[i][7] = packet.Translator.ReadBit();
                guids[i][4] = packet.Translator.ReadBit();
                guids[i][6] = packet.Translator.ReadBit();
                guids[i][2] = packet.Translator.ReadBit();
                strlen[i][1] = packet.Translator.ReadBits(8);
            }

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORByte(guids[i], 2);

                packet.Translator.ReadWoWString("Guild name", strlen[i][0], i);

                packet.Translator.ReadXORByte(guids[i], 5);

                packet.Translator.ReadWoWString("Request comment", strlen[i][1], i);
                packet.Translator.ReadUInt32E<GuildFinderOptionsAvailability>("Availability", i);
                packet.Translator.ReadInt32("Time Left", i);

                packet.Translator.ReadXORByte(guids[i], 0);
                packet.Translator.ReadXORByte(guids[i], 6);
                packet.Translator.ReadXORByte(guids[i], 3);
                packet.Translator.ReadXORByte(guids[i], 7);

                packet.Translator.ReadUInt32E<GuildFinderOptionsRoles>("Class Roles", i);

                packet.Translator.ReadXORByte(guids[i], 4);
                packet.Translator.ReadXORByte(guids[i], 1);

                packet.Translator.ReadInt32("Time Since", i);
                packet.Translator.ReadUInt32E<GuildFinderOptionsInterest>("Guild Interests", i);

                packet.Translator.WriteGuid("Guid", guids[i], i);
            }

            packet.Translator.ReadInt32("Left applications count");
        }

        [Parser(Opcode.CMSG_LF_GUILD_DECLINE_RECRUIT)] // 4.3.4
        public static void HandleLFGuildDeclineRecruit(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(1, 4, 5, 2, 6, 7, 0, 3);
            packet.Translator.ParseBitStream(guid, 5, 7, 2, 3, 4, 1, 0, 6);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_LF_GUILD_REMOVE_RECRUIT)] // 4.3.4
        public static void HandleLFGuildRemoveRecruit(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(0, 4, 3, 5, 7, 6, 2, 1);
            packet.Translator.ParseBitStream(guid, 4, 0, 3, 6, 5, 1, 2, 7);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_LF_GUILD_ADD_RECRUIT)]
        public static void HandleLFGuildAddRecruit(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadUInt32E<GuildFinderOptionsRoles>("Class Roles");
            packet.Translator.ReadUInt32E<GuildFinderOptionsInterest>("Guild Interests");
            packet.Translator.ReadUInt32E<GuildFinderOptionsAvailability>("Availability");

            guid[3] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            var comment = packet.Translator.ReadBits(11);
            guid[5] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            var player = packet.Translator.ReadBits(7);
            guid[2] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadWoWString("Comment", comment);
            packet.Translator.ReadWoWString("Player name", player);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 3);

            packet.Translator.WriteGuid("Guild GUID", guid);
        }

        [Parser(Opcode.CMSG_LF_GUILD_GET_GUILD_POST)]
        [Parser(Opcode.CMSG_LF_GUILD_GET_APPLICATIONS)]
        [Parser(Opcode.SMSG_LF_GUILD_APPLICANT_LIST_CHANGED)]
        [Parser(Opcode.SMSG_LF_GUILD_APPLICATIONS_LIST_CHANGED)]
        public static void HandlerLFGuildZeroLength(Packet packet)
        {
        }
    }
}
