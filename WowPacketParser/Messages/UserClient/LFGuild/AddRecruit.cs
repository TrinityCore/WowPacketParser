using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.LFGuild
{
    public unsafe struct AddRecruit
    {
        public ulong GuildGUID;
        public int Availability;
        public int ClassRoles;
        public int PlayStyle;
        public string Comment;

        [Parser(Opcode.CMSG_LF_GUILD_ADD_RECRUIT)]
        public static void HandleLFGuildAddRecruit(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadUInt32E<GuildFinderOptionsRoles>("Class Roles");
            packet.ReadUInt32E<GuildFinderOptionsInterest>("Guild Interests");
            packet.ReadUInt32E<GuildFinderOptionsAvailability>("Availability");

            guid[3] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var comment = packet.ReadBits(11);
            guid[5] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var player = packet.ReadBits(7);
            guid[2] = packet.ReadBit();

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadWoWString("Comment", comment);
            packet.ReadWoWString("Player name", player);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 3);

            packet.WriteGuid("Guild GUID", guid);
        }
    }
}
