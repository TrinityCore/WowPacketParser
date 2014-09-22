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

        [Parser(Opcode.CMSG_SETSHEATHED)]
        public static void HandleSetSheathed(Packet packet)
        {
            packet.ReadEnum<SheathState>("Sheath", TypeCode.Int32);
            packet.ReadBit("hasData");
        }

        [Parser(Opcode.SMSG_CONTACT_LIST)]
        public static void HandleContactList(Packet packet)
        {
            packet.ReadEnum<ContactListFlag>("List Flags", TypeCode.Int32);
            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                packet.ReadGuid("GUID");
                packet.ReadInt32("Realm Id");
                packet.ReadInt32("Realm Id");
                var flag = packet.ReadEnum<ContactEntryFlag>("Flags", TypeCode.Int32);
                packet.ReadCString("Note");

                if (!flag.HasAnyFlag(ContactEntryFlag.Friend))
                    continue;

                var status = packet.ReadEnum<ContactStatus>("Status", TypeCode.Byte);
                if (status == 0) // required any flag
                    continue;

                packet.ReadEntry<Int32>(StoreNameType.Area, "Area");
                packet.ReadInt32("Level");
                packet.ReadEnum<Class>("Class", TypeCode.Int32);
            }
        }

        [Parser(Opcode.SMSG_FRIEND_STATUS)]
        public static void HandleFriendStatus(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
