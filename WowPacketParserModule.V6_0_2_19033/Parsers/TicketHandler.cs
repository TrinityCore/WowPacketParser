using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class TicketHandler
    {
        [Parser(Opcode.SMSG_GM_TICKET_CASE_STATUS)]
        public static void HandleGMTicketCaseStatus(Packet packet)
        {
            packet.ReadTime("OldestTicketTime");
            packet.ReadTime("UpdateTime");

            var int24 = packet.ReadInt32("CasesCount");
            for (int i = 0; i < int24; i++)
            {
                packet.ReadInt32("CaseID", i);
                packet.ReadInt32("CaseOpened", i);
                packet.ReadInt32("CaseStatus", i);
                packet.ReadInt16("CfgRealmID", i);
                packet.ReadInt64("CharacterID", i);
                packet.ReadInt32("WaitTimeOverrideMinutes", i);

                packet.ResetBitReader();
                var bits12 = packet.ReadBits(11);
                var bits262 = packet.ReadBits(10);

                packet.ReadWoWString("Url", bits12, i);
                packet.ReadWoWString("WaitTimeOverrideMessage", bits262, i);
            }
        }

        [Parser(Opcode.SMSG_GM_TICKET_SYSTEM_STATUS)]
        public static void HandleGMTicketSystemStatus(Packet packet)
        {
            packet.ReadInt32("Status");
        }

        [Parser(Opcode.SMSG_GM_TICKET_GET_TICKET_RESPONSE)]
        public static void HandleGMTicketGetTicketResponse(Packet packet)
        {
            packet.ReadInt32("Result");

            var bit3068 = packet.ReadBit("HasInfo");

            // ClientGMTicketInfo
            if (bit3068)
            {
                packet.ReadInt32("TicketID");
                packet.ReadByte("Category");
                packet.ReadTime("TicketOpenTime");
                packet.ReadTime("OldestTicketTime");
                packet.ReadTime("UpdateTime");
                packet.ReadByte("AssignedToGM");
                packet.ReadByte("OpenedByGM");
                packet.ReadInt32("WaitTimeOverrideMinutes");

                packet.ResetBitReader();

                var bits1 = packet.ReadBits(11);
                var bits2022 = packet.ReadBits(10);

                packet.ReadWoWString("TicketDescription", bits1);
                packet.ReadWoWString("WaitTimeOverrideMessage", bits2022);
            }
        }
    }
}
