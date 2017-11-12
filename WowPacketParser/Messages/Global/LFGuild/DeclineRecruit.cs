using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.LFGuild
{
    public unsafe struct DeclineRecruit
    {
        public ulong RecruitGUID;

        [Parser(Opcode.CMSG_LF_GUILD_DECLINE_RECRUIT)] // 4.3.4
        public static void HandleLFGuildDeclineRecruit(Packet packet)
        {
            var guid = packet.StartBitStream(1, 4, 5, 2, 6, 7, 0, 3);
            packet.ParseBitStream(guid, 5, 7, 2, 3, 4, 1, 0, 6);
            packet.WriteGuid("Guid", guid);
        }
    }
}
