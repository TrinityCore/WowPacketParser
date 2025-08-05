using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class MailHandler
    {
        [Parser(Opcode.SMSG_MAIL_COMMAND_RESULT)]
        public static void HandleMailCommandResult(Packet packet)
        {
            packet.ReadUInt64("MailID");
            packet.ReadInt32E<MailActionType>("Command");
            packet.ReadInt32E<MailErrorType>("ErrorCode");
            packet.ReadInt32("BagResult");
            packet.ReadUInt64("AttachID");
            packet.ReadInt32("QtyInInventory");
        }

        [Parser(Opcode.SMSG_NOTIFY_RECEIVED_MAIL)]
        public static void HandleNotifyReceivedMail(Packet packet)
        {
            packet.ReadSingle("Delay");
        }
    }
}
