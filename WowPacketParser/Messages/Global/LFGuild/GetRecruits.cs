using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.LFGuild
{
    public unsafe struct GetRecruits
    {
        public UnixTime LastUpdate;

        [Parser(Opcode.CMSG_LF_GUILD_GET_RECRUITS)]
        public static void HandlerLFGuildGetRecruits(Packet packet)
        {
            packet.ReadTime("Unk Time");
        }
    }
}
