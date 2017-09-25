using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct Complaint
    {
        public ComplaintOffender Offender;
        public byte ComplaintType;
        public ulong EventGuid;
        public ulong InviteGuid;
        public uint MailID;
        public ComplaintChat Chat;

        [Parser(Opcode.CMSG_COMPLAINT, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleComplain(Packet packet)
        {
            bool fromChat = packet.ReadBool("From Chat"); // false = from mail
            packet.ReadGuid("Guid");
            packet.ReadInt32E<Language>("Language");
            packet.ReadInt32E<ChatMessageType>("Type");
            packet.ReadInt32("Channel ID");

            if (fromChat)
            {
                packet.ReadTime("Time ago");
                packet.ReadCString("Complain");
            }
        }


        [Parser(Opcode.CMSG_COMPLAINT, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleComplain602(Packet packet)
        {
            var result = packet.ReadByte("ComplaintType");

            readComplaintOffender(packet, "Offender");

            switch (result)
            {
                case 0: // Mail
                    packet.ReadInt32("MailID");
                    break;
                case 1: // Chat
                    readComplaintChat(packet, "Chat");
                    break;
                case 2: // Calendar
                    // Order guessed
                    packet.ReadInt64("EventGuid");
                    packet.ReadInt64("InviteGuid");
                    break;
            }
        }

        private static void readComplaintChat(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("Command");
            packet.ReadInt32("ChannelID");

            packet.ResetBitReader();

            var len = packet.ReadBits(12);
            packet.ReadWoWString("MessageLog", len);
        }

        private static void readComplaintOffender(Packet packet, params object[] indexes)
        {
            packet.ReadPackedGuid128("PlayerGuid", indexes);
            packet.ReadInt32("RealmAddress", indexes);
            packet.ReadInt32("TimeSinceOffence", indexes);
        }

    }
}
