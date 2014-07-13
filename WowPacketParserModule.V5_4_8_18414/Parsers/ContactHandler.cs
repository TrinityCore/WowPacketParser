using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class ContactHandler
    {
        [Parser(Opcode.CMSG_CONTACT_LIST)]
        public static void HandleContactListClient(Packet packet)
        {
            packet.ReadEnum<ContactListFlag>("List Flags?", TypeCode.Int32);
        }

        [Parser(Opcode.SMSG_CONTACT_LIST)]
        public static void HandleContactList(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_FRIEND_STATUS)]
        public static void HandleFriendStatus(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
