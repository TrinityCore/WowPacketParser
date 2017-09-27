using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct MailReturnToSender
    {
        public ulong SenderGUID;
        public int MailID;

        [Parser(Opcode.CMSG_MAIL_RETURN_TO_SENDER, ClientVersionBuild.Zero, ClientVersionBuild.V5_4_8_18291)]
        public static void HandleMailReturnToSender(Packet packet)
        {
            packet.ReadGuid("Mailbox GUID");
            packet.ReadUInt32("Mail Id");
            packet.ReadGuid("Sender GUID");
        }

        [Parser(Opcode.CMSG_MAIL_RETURN_TO_SENDER, ClientVersionBuild.V5_4_8_18291, ClientVersionBuild.V5_4_8_18291)]
        public static void HandleMailReturnToSender548(Packet packet)
        {
            packet.ReadUInt32("Mail Id");

            var guid = new byte[8];

            packet.StartBitStream(guid, 2, 0, 4, 6, 3, 1, 7, 5);
            packet.ParseBitStream(guid, 5, 6, 2, 0, 3, 1, 4, 7);
            packet.WriteGuid("Mailbox Guid", guid);
        }

        [Parser(Opcode.CMSG_MAIL_RETURN_TO_SENDER, ClientVersionBuild.V5_4_8_18291, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleMailReturnToSender602(Packet packet)
        {
            packet.ReadInt32("MailID");
            packet.ReadPackedGuid128("SenderGUID");
        }
    }
}
