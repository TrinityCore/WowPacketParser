using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.Guild
{
    public unsafe struct InviteByName
    {
        public string Name;

        [Parser(Opcode.CMSG_GUILD_INVITE_BY_NAME)]
        public static void HandleGuildInviteByName(Packet packet)
        {
            var bits16 = packet.ReadBits(9);
            packet.ReadWoWString("Name", bits16);
        }
    }
}
