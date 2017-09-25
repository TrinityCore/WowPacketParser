using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.Calendar
{
    public unsafe struct Complain
    {
        public ulong InviteID;
        public ulong EventID;
        public ulong InvitedByGUID;

        [Parser(Opcode.CMSG_CALENDAR_COMPLAIN, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleCalendarComplain(Packet packet)
        {
            packet.ReadInt64("Event ID");
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_CALENDAR_COMPLAIN, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleCalendarComplain434(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadInt64("Event ID");
            packet.ReadInt64("Invite ID");
        }
    }
}
