using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class ContactHandler
    {
        public static void ReadSingleContactBlock(Packet packet, bool onlineCheck)
        {
            var status = packet.ReadByteE<ContactStatus>("Status");

            if (onlineCheck && status == ContactStatus.Offline)
                return;

            packet.ReadInt32<AreaId>("Area");
            packet.ReadInt32("Level");
            packet.ReadInt32E<Class>("Class");
        }

        [Parser(Opcode.CMSG_CONTACT_LIST)]
        public static void HandleContactListClient(Packet packet)
        {
            packet.ReadInt32E<ContactListFlag>("List Flags?");
        }

        [Parser(Opcode.SMSG_CONTACT_LIST)]
        public static void HandleContactList(Packet packet)
        {
            packet.ReadInt32E<ContactListFlag>("List Flags");

            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                packet.ReadGuid("GUID");

                var flag = packet.ReadInt32E<ContactEntryFlag>("Flags");

                packet.ReadCString("Note");

                if (!flag.HasAnyFlag(ContactEntryFlag.Friend))
                    continue;

                ReadSingleContactBlock(packet, true);
            }

            if (packet.CanRead())
                WardenHandler.ReadCheatCheckDecryptionBlock(packet);
        }

        [Parser(Opcode.SMSG_FRIEND_STATUS)]
        public static void HandleFriendStatus(Packet packet)
        {
            var result = packet.ReadByteE<ContactResult>("Result");

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
