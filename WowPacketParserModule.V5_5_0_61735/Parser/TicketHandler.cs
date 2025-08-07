using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class TicketHandler
    {
        [Parser(Opcode.SMSG_GM_TICKET_SYSTEM_STATUS)]
        public static void HandleGMTicketSystemStatus(Packet packet)
        {
            packet.ReadInt32("Status");
        }

        [Parser(Opcode.SMSG_GM_TICKET_CASE_STATUS)]
        public static void HandleGMTicketCaseStatus(Packet packet)
        {
            var casesCount = packet.ReadInt32("CasesCount");
            for (int i = 0; i < casesCount; i++)
            {
                packet.ReadInt32("CaseID", i);
                packet.ReadTime64("CaseOpened", i);
                packet.ReadInt32("CaseStatus", i);
                packet.ReadInt16("CfgRealmID", i);
                packet.ReadInt64("CharacterID", i);
                packet.ReadInt32("WaitTimeOverrideMinutes", i);

                packet.ResetBitReader();
                var urlLen = packet.ReadBits(11);
                var overrideMsgLen = packet.ReadBits(10);
                var titleLen = packet.ReadBits(24);
                var descriptionLen = packet.ReadBits(24);

                packet.ReadWoWString("Url", urlLen, i);
                packet.ReadWoWString("WaitTimeOverrideMessage", overrideMsgLen, i);
                packet.ReadDynamicString("Title", titleLen, i);
                packet.ReadDynamicString("Description", descriptionLen, i);
            }
        }

        [Parser(Opcode.SMSG_COMPLAINT_RESULT)]
        public static void HandleComplaintResult(Packet packet)
        {
            packet.ReadUInt32("ComplaintType");
            packet.ReadByte("Result");
        }
    }
}
