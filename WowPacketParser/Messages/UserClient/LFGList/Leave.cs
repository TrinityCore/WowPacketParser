using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.LFGList
{
    public unsafe struct Leave
    {
        public CliRideTicket Ticket;
        
        [Parser(Opcode.CMSG_LFG_LIST_LEAVE)]
        public static void HandleLFGListLeave(Packet packet)
        {
            readCliRideTicket(packet, "RideTicket");
        }

        public static void readCliRideTicket(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("RequesterGuid", idx);
            packet.ReadInt32("Id", idx);
            packet.ReadInt32("Type", idx);
            packet.ReadTime("Time", idx);
        }
    }
}
