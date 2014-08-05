using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class MailHandler
    {
        [Parser(Opcode.CMSG_GET_MAIL_LIST)]
        public static void HandleGetMailList(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_MAIL_CREATE_TEXT_ITEM)]
        public static void HandleMailCreate(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_MAIL_DELETE)]
        public static void HandleMailDelete(Packet packet)
        {
            packet.ReadUInt32("Template Id");
            packet.ReadUInt32("Mail Id");
        }

        [Parser(Opcode.CMSG_MAIL_MARK_AS_READ)]
        public static void HandleMarkMail(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_MAIL_RETURN_TO_SENDER)]
        public static void HandleMailReturnToSender(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_MAIL_TAKE_ITEM)]
        public static void HandleMailTakeItem(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_MAIL_TAKE_MONEY)]
        public static void HandleTakeMoney(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_SEND_MAIL)]
        public static void HandleSendMail(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.MSG_QUERY_NEXT_MAIL_TIME)]
        public static void HandleNullMail(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_MAIL_LIST_RESULT)]
        public static void HandleSMailListResult(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_RECEIVED_MAIL)]
        public static void HandleSReceivedMail(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SEND_MAIL_RESULT)]
        public static void HandleSSendMailResult(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SHOW_MAILBOX)]
        public static void HandleSShowMailbox(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
