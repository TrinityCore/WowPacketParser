using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class MailHandler
    {
        [Parser(Opcode.SMSG_SEND_MAIL_RESULT)]
        public static void HandleSendMailResult(Packet packet)
        {
            packet.ReadUInt32("MailID");
            packet.ReadEnum<MailActionType>("Command", TypeCode.UInt32);
            packet.ReadEnum<MailErrorType>("ErrorCode", TypeCode.UInt32);
            packet.ReadUInt32("BagResult");
            packet.ReadUInt32("AttachID");
            packet.ReadUInt32("QtyInInventory");
        }
    }
}