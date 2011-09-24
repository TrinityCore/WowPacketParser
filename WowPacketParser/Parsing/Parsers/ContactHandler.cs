using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class ContactHandler
    {
        public static void ReadSingleContactBlock(Packet packet, bool onlineCheck)
        {
            var status = packet.ReadEnum<ContactStatus>("Status", TypeCode.Byte);

            if (onlineCheck && status != ContactStatus.Online)
                return;

            packet.ReadInt32("Area ID");
            packet.ReadInt32("Level");

            packet.ReadEnum<Class>("Class", TypeCode.Int32);
        }

        [Parser(Opcode.SMSG_CONTACT_LIST)]
        public static void HandleContactList(Packet packet)
        {
            packet.ReadEnum<ContactListFlag>("List Flags", TypeCode.Int32);

            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                packet.ReadGuid("GUID");

                var cflags = packet.ReadEnum<ContactEntryFlag>("Flags", TypeCode.Int32);

                packet.ReadCString("Note");

                if (!cflags.HasFlag(ContactEntryFlag.Friend))
                    continue;

                ReadSingleContactBlock(packet, true);
            }

            if (!packet.IsRead())
                WardenHandler.ReadCheatCheckDecryptionBlock(packet);
        }

        [Parser(Opcode.SMSG_FRIEND_STATUS)]
        public static void HandleFriendStatus(Packet packet)
        {
            var result = packet.ReadEnum<ContactResult>("Result", TypeCode.Byte);

            packet.ReadGuid("GUID");

            switch (result)
            {
                case ContactResult.FriendAddedOnline:
                case ContactResult.FriendAddedOffline:
                case ContactResult.Online:
                {
                    if (result != ContactResult.Online)
                        packet.ReadCString("Note");

                    ReadSingleContactBlock(packet, false);
                    break;
                }
            }
        }
    }
}
