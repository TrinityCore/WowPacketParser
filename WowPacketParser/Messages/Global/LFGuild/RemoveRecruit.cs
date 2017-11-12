using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.LFGuild
{
    public unsafe struct RemoveRecruit
    {
        public ulong GuildGUID;

        [Parser(Opcode.CMSG_LF_GUILD_REMOVE_RECRUIT)] // 4.3.4
        public static void HandleLFGuildRemoveRecruit(Packet packet)
        {
            var guid = packet.StartBitStream(0, 4, 3, 5, 7, 6, 2, 1);
            packet.ParseBitStream(guid, 4, 0, 3, 6, 5, 1, 2, 7);
            packet.WriteGuid("Guid", guid);
        }
    }
}
