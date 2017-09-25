using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.LFGuild
{
    public unsafe struct Browse
    {
        public int CharacterLevel;
        public int Availability;
        public int ClassRoles;
        public int PlayStyle;

        [Parser(Opcode.CMSG_LF_GUILD_BROWSE)]
        public static void HandleGuildFinderBrowse(Packet packet)
        {
            packet.ReadUInt32E<GuildFinderOptionsRoles>("Class Roles");
            packet.ReadUInt32E<GuildFinderOptionsAvailability>("Availability");
            packet.ReadUInt32E<GuildFinderOptionsInterest>("Guild Interests");
            packet.ReadUInt32("Player Level");
        }
    }
}
