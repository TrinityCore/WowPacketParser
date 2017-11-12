using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.Guild
{
    public unsafe struct DeleteRank
    {
        public int RankOrder;


        [Parser(Opcode.CMSG_GUILD_DELETE_RANK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildDelRank434(Packet packet)
        {
            packet.ReadUInt32("Rank Id");
        }
    }
}
