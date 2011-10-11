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
                case ContactResult.FriendAddedOffline:
                    packet.ReadCString("Note");
                    break;
                case ContactResult.FriendAddedOnline:
                {
                    packet.ReadCString("Note");
                    ReadSingleContactBlock(packet, false);
                    break;
                }
                case ContactResult.Online:
                    ReadSingleContactBlock(packet, false);
                    break;
                case ContactResult.Unknown2:
                    packet.ReadByte("Unk byte 1");
                    break;
                case ContactResult.Unknown3:
                    packet.ReadInt32("Unk int");
                    break;
            }
        }
    }
}
