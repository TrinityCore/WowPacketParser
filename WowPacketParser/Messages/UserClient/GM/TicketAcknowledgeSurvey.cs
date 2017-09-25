using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.GM
{
    public unsafe struct TicketAcknowledgeSurvey
    {
        public int CaseID;
        
        [Parser(Opcode.CMSG_GM_TICKET_ACKNOWLEDGE_SURVEY)]
        public static void HandleGMTicketAcknowledgeSurvey(Packet packet)
        {
            packet.ReadInt32("CaseID");
        }
    }
}
