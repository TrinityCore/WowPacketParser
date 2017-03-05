using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class ContactHandler
    {
        public static void ReadSingleContactBlock(Packet packet, bool onlineCheck)
        {
            var status = packet.Translator.ReadByteE<ContactStatus>("Status");

            if (onlineCheck && status == ContactStatus.Offline)
                return;

            packet.Translator.ReadInt32<AreaId>("Area");
            packet.Translator.ReadInt32("Level");
            packet.Translator.ReadInt32E<Class>("Class");
        }

        [Parser(Opcode.CMSG_CONTACT_LIST)]
        public static void HandleContactListClient(Packet packet)
        {
            packet.Translator.ReadInt32E<ContactListFlag>("List Flags?");
        }

        [Parser(Opcode.SMSG_CONTACT_LIST)]
        public static void HandleContactList(Packet packet)
        {
            packet.Translator.ReadInt32E<ContactListFlag>("List Flags");

            var count = packet.Translator.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadGuid("GUID");

                var flag = packet.Translator.ReadInt32E<ContactEntryFlag>("Flags");

                packet.Translator.ReadCString("Note");

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
            var result = packet.Translator.ReadByteE<ContactResult>("Result");

            packet.Translator.ReadGuid("GUID");

            switch (result)
            {
                case ContactResult.FriendAddedOffline:
                    packet.Translator.ReadCString("Note");
                    break;
                case ContactResult.FriendAddedOnline:
                {
                    packet.Translator.ReadCString("Note");
                    ReadSingleContactBlock(packet, false);
                    break;
                }
                case ContactResult.Online:
                    ReadSingleContactBlock(packet, false);
                    break;
                case ContactResult.Unknown2:
                    packet.Translator.ReadByte("Unk byte 1");
                    break;
                case ContactResult.Unknown3:
                    packet.Translator.ReadInt32("Unk int");
                    break;
            }
        }
    }
}
