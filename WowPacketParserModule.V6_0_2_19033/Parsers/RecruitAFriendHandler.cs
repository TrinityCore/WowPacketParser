using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class RecruitAFriendHandler
    {

        [Parser(Opcode.SMSG_RECRUIT_A_FRIEND_RESPONSE)]
        public static void HandleRecruitAFriendResponse(Packet packet)
        {
            packet.ReadBits("Result", 3)
                ;
        }
    }
}
