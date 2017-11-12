using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.Guild
{
    public unsafe struct PromoteMember
    {
        public ulong Promotee;

        [Parser(Opcode.CMSG_GUILD_PROMOTE_MEMBER, ClientVersionBuild.Zero, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleGuildCreate(Packet packet)
        {
            packet.ReadCString("Name");
        }

        [Parser(Opcode.CMSG_GUILD_PROMOTE_MEMBER, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleGuildPromote(Packet packet)
        {
            var guid = packet.StartBitStream(4, 0, 3, 5, 7, 1, 2, 6);
            packet.ParseBitStream(guid, 7, 0, 5, 2, 3, 6, 4, 1);
            packet.WriteGuid("GUID", guid);
        }
    }
}
