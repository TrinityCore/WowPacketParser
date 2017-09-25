using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.LFGuild
{
    public unsafe struct SetGuildPost
    {
        public string Comment;
        public int Availability;
        public bool Active;
        public int PlayStyle;
        public int ClassRoles;
        public int LevelRange;
        
        [Parser(Opcode.CMSG_LF_GUILD_SET_GUILD_POST, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildFinderSetGuildPost422(Packet packet)
        {
            packet.ReadBit("Join");
            packet.ReadUInt32E<GuildFinderOptionsAvailability>("Availability");
            packet.ReadUInt32E<GuildFinderOptionsRoles>("Class Roles");
            packet.ReadUInt32E<GuildFinderOptionsInterest>("Guild Interests");
            packet.ReadUInt32E<GuildFinderOptionsLevel>("Level");
            packet.ReadCString("Comment");
        }

        [Parser(Opcode.CMSG_LF_GUILD_SET_GUILD_POST, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildFinderSetGuildPost434(Packet packet)
        {
            packet.ReadUInt32E<GuildFinderOptionsLevel>("Level");
            packet.ReadUInt32E<GuildFinderOptionsAvailability>("Availability");
            packet.ReadUInt32E<GuildFinderOptionsInterest>("Guild Interests");
            packet.ReadUInt32E<GuildFinderOptionsRoles>("Class Roles");
            var length = packet.ReadBits(11);
            packet.ReadBit("Listed");
            packet.ReadWoWString("Comment", length);
        }

        [Parser(Opcode.CMSG_LF_GUILD_SET_GUILD_POST, ClientVersionBuild.V6_1_2_19802)]
        public static void HandleGuildFinderSetGuildPost612(Packet packet)
        {
            packet.ReadUInt32E<GuildFinderOptionsInterest>("Guild Interests"); // ok
            packet.ReadUInt32E<GuildFinderOptionsAvailability>("Availability"); // ok
            packet.ReadUInt32E<GuildFinderOptionsRoles>("Class Roles"); // ok
            packet.ReadUInt32E<GuildFinderOptionsLevel>("Level");
            packet.ReadBit("Listed");
            packet.ReadWoWString("Comment", packet.ReadBits(10));
        }
    }
}
