using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.Guild
{
    public unsafe struct SetGuildMaster
    {
        public string NewMasterName;

        [Parser(Opcode.CMSG_GUILD_SET_GUILD_MASTER, ClientVersionBuild.Zero, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleGuildSetGuildMaster(Packet packet)
        {
            var nameLength = packet.ReadBits(7);
            packet.ReadBit("Is Dethroned"); // Most probably related to guild finder inactivity
            packet.ReadWoWString("New GuildMaster name", nameLength);
        }

        [Parser(Opcode.CMSG_GUILD_SET_GUILD_MASTER, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleGuildSetGuildMaster547(Packet packet)
        {
            var nameLength = packet.ReadBits(9);
            packet.ReadWoWString("New GuildMaster name", nameLength);
        }
    }
}
