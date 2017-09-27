using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.SupportTicket
{
    public unsafe struct SubmitSuggestion
    {
        public CliSupportTicketHeader Header;
        public string Note;

        [Parser(Opcode.CMSG_SUPPORT_TICKET_SUBMIT_SUGGESTION)]
        public static void HandleSubmitSuggestion(Packet packet)
        {
            readCliSupportTicketHeader(packet, "Header");

            var noteLength = packet.ReadBits("NoteLength", 10);
            packet.ResetBitReader();
            packet.ReadWoWString("Note", noteLength);
        }
        private static void readCliSupportTicketHeader(Packet packet, params object[] idx)
        {
            packet.ReadInt32<MapId>("MapID", idx);
            packet.ReadVector3("Position", idx);
            packet.ReadSingle("Facing", idx);
        }
    }
}
