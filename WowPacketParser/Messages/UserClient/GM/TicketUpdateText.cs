using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.GM
{
    public unsafe struct TicketUpdateText
    {
        public string Description;

        [Parser(Opcode.CMSG_GM_TICKET_UPDATE_TEXT, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGMTicketUpdatetext(Packet packet)
        {
            packet.ReadCString("New Ticket Text");
        }

        [Parser(Opcode.CMSG_GM_TICKET_UPDATE_TEXT, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGMTicketUpdatetext602(Packet packet)
        {
            var length = packet.ReadBits("DescriptionLength", 11);
            packet.ResetBitReader();

            packet.ReadWoWString("Description", length);
        }
    }
}
