using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class ContactHandler
    {
        public static void ReadSingleContactBlock(ref Packet packet, bool onlineCheck)
        {
            var status = packet.ReadEnum<ContactStatus>("Status", TypeCode.Byte);

            if (onlineCheck && status == ContactStatus.Offline)
                return;

            packet.ReadEntryWithName<Int32>(StoreNameType.Area, "Area");
            packet.ReadInt32("Level");
            packet.ReadEnum<Class>("Class", TypeCode.Int32);
        }

        [Parser(Opcode.CMSG_CONTACT_LIST)]
        public static void HandleContactListClient(Packet packet)
        {
            packet.ReadEnum<ContactListFlag>("List Flags?", TypeCode.Int32);
        }

        [Parser(Opcode.SMSG_CONTACT_LIST)]
        public static void HandleContactList(Packet packet)
        {
            packet.ReadEnum<ContactListFlag>("List Flags", TypeCode.Int32);

            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                packet.ReadGuid("GUID");

                var flag = packet.ReadEnum<ContactEntryFlag>("Flags", TypeCode.Int32);

                packet.ReadCString("Note");

                if (!flag.HasAnyFlag(ContactEntryFlag.Friend))
                    continue;

                ReadSingleContactBlock(ref packet, true);
            }

            if (packet.CanRead())
                WardenHandler.ReadCheatCheckDecryptionBlock(ref packet);
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
                    ReadSingleContactBlock(ref packet, false);
                    break;
                }
                case ContactResult.Online:
                    ReadSingleContactBlock(ref packet, false);
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
