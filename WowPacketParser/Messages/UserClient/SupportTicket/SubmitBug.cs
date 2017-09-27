using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.SupportTicket
{
    public unsafe struct SubmitBug
    {
        public string Note;
        public CliSupportTicketHeader Header;

        [Parser(Opcode.CMSG_SUPPORT_TICKET_SUBMIT_BUG, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleSubmitBug(Packet packet)
        {
            var length = packet.ReadBits(12);
            var pos = new Vector4();

            packet.ReadWoWString("Text", length);
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            packet.ReadInt32("Map ID");
            pos.O = packet.ReadSingle();
            packet.AddValue("Position", pos);
        }
        
        [Parser(Opcode.CMSG_SUPPORT_TICKET_SUBMIT_BUG, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleSubmitBug(Packet packet)
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
