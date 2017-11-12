using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class GuildFinderHandler
    {
        [Parser(Opcode.SMSG_LF_GUILD_POST_UPDATED)]
        public static void HandleGuildFinderPostUpdated(Packet packet)
        {
            var b = packet.ReadBit("Unk Bit"); // Can set settings ?

            if (b != 0)
            {
                var length = packet.ReadBits(11);
                packet.ReadBit("Listed");
                // Flush bits
                packet.ReadUInt32E<GuildFinderOptionsLevel>("Level");
                packet.ReadWoWString("Comment", length);
                packet.ReadInt32("Unk Int32");
                packet.ReadUInt32E<GuildFinderOptionsAvailability>("Availability");
                packet.ReadUInt32E<GuildFinderOptionsRoles>("Class Roles");
                packet.ReadUInt32E<GuildFinderOptionsInterest>("Guild Interests");
            }
        }

        [Parser(Opcode.SMSG_LF_GUILD_COMMAND_RESULT)]
        public static void HandleGuildFinderCommandResult(Packet packet)
        {
            packet.ReadByte("Unk Byte"); // == 0 -> ERR_GUILD_INTERNAL
            packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_LF_GUILD_BROWSE_UPDATED, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandlerLFGuildBrowseUpdated422(Packet packet)
        {
            var count = packet.ReadInt32("Count");
            if (count == 0)
                return;
            var guids = new byte[count][];

            for (var i = 0; i < count; ++i)
                guids[i] = packet.StartBitStream(7, 4, 5, 0, 2, 6, 1, 3);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Guild Emblem Border Color", i);

                packet.ReadXORByte(guids[i], 4);

                packet.ReadCString("Guild Description", i);

                packet.ReadXORByte(guids[i], 6);

                packet.ReadInt32("Guild Emblem Texture File", i);
                packet.ReadInt32("Guild Level", i);

                packet.ReadXORByte(guids[i], 5);

                packet.ReadInt32("Unk 2", i);
                packet.ReadUInt32E<GuildFinderOptionsRoles>("Class Roles", i);
                packet.ReadCString("Guild Name", i);
                packet.ReadByte("Cached", i);

                packet.ReadXORByte(guids[i], 3);

                packet.ReadInt32("Achievement Points", i);

                packet.ReadXORByte(guids[i], 0);

                packet.ReadInt32("Guild Emblem Color", i);
                packet.ReadInt32("Guild Emblem Background Color", i);
                packet.ReadByte("Request Pending", i);
                packet.ReadUInt32E<GuildFinderOptionsInterest>("Guild Interests", i);
                packet.ReadUInt32E<GuildFinderOptionsAvailability>("Availability", i);

                packet.ReadXORByte(guids[i], 7);

                packet.ReadInt32("Number of members", i);

                packet.ReadXORByte(guids[i], 2);

                packet.ReadInt32("Unk 5", i);

                packet.ReadXORByte(guids[i], 1);

                packet.WriteGuid("Guild GUID", guids[i], i);
            }
        }

        [Parser(Opcode.SMSG_LF_GUILD_BROWSE_UPDATED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandlerLFGuildBrowseUpdated434(Packet packet)
        {
            var count = packet.ReadBits("Count", 19);

            var guids = new byte[count][];
            var strlen = new uint[count][];

            for (int i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                strlen[i] = new uint[2];

                guids[i][7] = packet.ReadBit();
                guids[i][5] = packet.ReadBit();
                strlen[i][1] = packet.ReadBits(8);
                guids[i][0] = packet.ReadBit();
                strlen[i][0] = packet.ReadBits(11);
                guids[i][4] = packet.ReadBit();
                guids[i][1] = packet.ReadBit();
                guids[i][2] = packet.ReadBit();
                guids[i][6] = packet.ReadBit();
                guids[i][3] = packet.ReadBit();
            }

            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt32("Tabard Emblem Color", i);
                packet.ReadInt32("Tabard Border Style", i); // Guessed from sniffs
                packet.ReadInt32("Tabard Icon", i);
                packet.ReadWoWString("Comment", strlen[i][0], i);
                packet.ReadBool("Cached", i);

                packet.ReadXORByte(guids[i], 5);

                packet.ReadUInt32E<GuildFinderOptionsInterest>("Guild Interests", i);

                packet.ReadXORByte(guids[i], 6);
                packet.ReadXORByte(guids[i], 4);

                packet.ReadInt32("Level", i);
                packet.ReadWoWString("Name", strlen[i][1], i);
                packet.ReadInt32("Achievement Points", i);

                packet.ReadXORByte(guids[i], 7);

                packet.ReadBool("Request Pending", i);

                packet.ReadXORByte(guids[i], 2);
                packet.ReadXORByte(guids[i], 0);

                packet.ReadUInt32E<GuildFinderOptionsAvailability>("Availability", i);

                packet.ReadXORByte(guids[i], 1);

                packet.ReadInt32("Tabard Background Color", i);
                packet.ReadInt32("Unk Int 2", i); // + 128 (Always 0 or 1)
                packet.ReadInt32("Tabard Border Color", i);
                packet.ReadUInt32E<GuildFinderOptionsRoles>("Class Roles", i);

                packet.ReadXORByte(guids[i], 3);

                packet.ReadInt32("Number of Members", i);

                packet.WriteGuid("Guild Guid", guids[i], i);
            }
        }

        [Parser(Opcode.SMSG_LF_GUILD_RECRUIT_LIST_UPDATED)] // 4.3.4
        public static void HandlerLFGuildRecruitListUpdated(Packet packet)
        {
            var count = packet.ReadBits("Count", 20);

            var guids = new byte[count][];
            var strlen = new uint[count][];

            for (int i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                strlen[i] = new uint[2];

                strlen[i][0] = packet.ReadBits(11);
                guids[i][2] = packet.ReadBit();
                guids[i][4] = packet.ReadBit();
                guids[i][3] = packet.ReadBit();
                guids[i][7] = packet.ReadBit();
                guids[i][0] = packet.ReadBit();
                strlen[i][1] = packet.ReadBits(7);
                guids[i][5] = packet.ReadBit();
                guids[i][1] = packet.ReadBit();
                guids[i][6] = packet.ReadBit();
            }

            for (int i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guids[i], 4);

                packet.ReadInt32("Unk Int32 1", i); // Is expired ?

                packet.ReadXORByte(guids[i], 3);
                packet.ReadXORByte(guids[i], 0);
                packet.ReadXORByte(guids[i], 1);

                packet.ReadInt32("Player level", i);

                packet.ReadXORByte(guids[i], 6);
                packet.ReadXORByte(guids[i], 7);
                packet.ReadXORByte(guids[i], 2);

                packet.ReadInt32("Time Since", i); // Time (in seconds) since the application was submitted.
                packet.ReadUInt32E<GuildFinderOptionsAvailability>("Availability", i);
                packet.ReadUInt32E<GuildFinderOptionsRoles>("Class Roles", i);
                packet.ReadUInt32E<GuildFinderOptionsInterest>("Guild Interests", i);
                packet.ReadInt32("Time Left", i); // Time (in seconds) until the application will expire.

                packet.ReadWoWString("Character Name", strlen[i][1], i);
                packet.ReadWoWString("Comment", strlen[i][0], i);

                packet.ReadUInt32E<Class>("Class", i);

                packet.ReadXORByte(guids[i], 5);

                packet.WriteGuid("Guid", guids[i], i);
            }

            packet.ReadTime("Unk Time");
        }

        [Parser(Opcode.SMSG_LF_GUILD_MEMBERSHIP_LIST_UPDATED)]
        public static void HandlerLFGuildMembershipListUpdated(Packet packet)
        {
            var count = packet.ReadBits("Count", 20);

            var guids = new byte[count][];
            var strlen = new uint[count][];

            for (int i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                strlen[i] = new uint[2];

                guids[i][1] = packet.ReadBit();
                guids[i][0] = packet.ReadBit();
                guids[i][5] = packet.ReadBit();
                strlen[i][0] = packet.ReadBits(11);
                guids[i][3] = packet.ReadBit();
                guids[i][7] = packet.ReadBit();
                guids[i][4] = packet.ReadBit();
                guids[i][6] = packet.ReadBit();
                guids[i][2] = packet.ReadBit();
                strlen[i][1] = packet.ReadBits(8);
            }

            for (int i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guids[i], 2);

                packet.ReadWoWString("Guild name", strlen[i][0], i);

                packet.ReadXORByte(guids[i], 5);

                packet.ReadWoWString("Request comment", strlen[i][1], i);
                packet.ReadUInt32E<GuildFinderOptionsAvailability>("Availability", i);
                packet.ReadInt32("Time Left", i);

                packet.ReadXORByte(guids[i], 0);
                packet.ReadXORByte(guids[i], 6);
                packet.ReadXORByte(guids[i], 3);
                packet.ReadXORByte(guids[i], 7);

                packet.ReadUInt32E<GuildFinderOptionsRoles>("Class Roles", i);

                packet.ReadXORByte(guids[i], 4);
                packet.ReadXORByte(guids[i], 1);

                packet.ReadInt32("Time Since", i);
                packet.ReadUInt32E<GuildFinderOptionsInterest>("Guild Interests", i);

                packet.WriteGuid("Guid", guids[i], i);
            }

            packet.ReadInt32("Left applications count");
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
