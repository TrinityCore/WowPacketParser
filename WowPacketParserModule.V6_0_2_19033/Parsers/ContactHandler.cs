using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class ContactHandler
    {
        [Parser(Opcode.CMSG_ADD_IGNORE)]
        [Parser(Opcode.CMSG_VOICE_ADD_IGNORE)]
        public static void HandleAddIgnoreOrMute(Packet packet)
        {
            var bits9 = packet.Translator.ReadBits(9);
            packet.Translator.ReadWoWString("Name", bits9);
        }

        [Parser(Opcode.CMSG_ADD_FRIEND)]
        public static void HandleAddFriend(Packet packet)
        {
            var bits16 = packet.Translator.ReadBits(9);
            var bits10 = packet.Translator.ReadBits(10);

            packet.Translator.ReadWoWString("Name", bits16);
            packet.Translator.ReadWoWString("Notes", bits10);
        }

        public static void ReadQualifiedGUID(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt32("VirtualRealmAddress", indexes);
            packet.Translator.ReadPackedGuid128("Guid", indexes);
        }

        [Parser(Opcode.CMSG_DEL_FRIEND)]
        [Parser(Opcode.CMSG_DEL_IGNORE)]
        [Parser(Opcode.CMSG_VOICE_DEL_IGNORE)]
        public static void HandleDeleteFriendOrIgnoreOrMute(Packet packet)
        {
            ReadQualifiedGUID(packet, "QualifiedGUID");
        }

        [Parser(Opcode.SMSG_FRIEND_STATUS)]
        public static void HandleFriendStatus(Packet packet)
        {
            packet.Translator.ReadByte("FriendResult");

            packet.Translator.ReadPackedGuid128("Guid");
            packet.Translator.ReadPackedGuid128("WowAccount");

            packet.Translator.ReadInt32("VirtualRealmAddress");

            packet.Translator.ReadByteE<ContactStatus>("Status");

            packet.Translator.ReadInt32<AreaId>("AreaID");
            packet.Translator.ReadInt32("Level");
            packet.Translator.ReadInt32E<Class>("ClassID");

            packet.Translator.ResetBitReader();

            var bits28 = packet.Translator.ReadBits(10);
            packet.Translator.ReadWoWString("Notes", bits28);
        }

        [Parser(Opcode.SMSG_CONTACT_LIST)]
        public static void HandleContactList(Packet packet)
        {
            packet.Translator.ReadInt32E<ContactListFlag>("List Flags");
            var bits6 = packet.Translator.ReadBits("ContactInfoCount", 8);

            for (var i = 0; i < bits6; i++)
            {
                packet.Translator.ReadPackedGuid128("Guid", i);
                packet.Translator.ReadPackedGuid128("WowAccount", i);

                packet.Translator.ReadInt32("VirtualRealmAddr", i);
                packet.Translator.ReadInt32("NativeRealmAddr", i);
                packet.Translator.ReadInt32("TypeFlags", i);

                packet.Translator.ReadByte("Status", i);

                packet.Translator.ReadInt32("AreaID", i);
                packet.Translator.ReadInt32("Level", i);
                packet.Translator.ReadInt32("ClassID", i);

                packet.Translator.ResetBitReader();

                var bits44 = packet.Translator.ReadBits(10);
                packet.Translator.ReadWoWString("Notes", bits44, i);
            }
        }

        [Parser(Opcode.CMSG_SEND_CONTACT_LIST)]
        public static void HandleSendContactList(Packet packet)
        {
            packet.Translator.ReadInt32("Flags");
        }

        [Parser(Opcode.CMSG_SET_CONTACT_NOTES)]
        public static void HandleSetContactNotes(Packet packet)
        {
            ReadQualifiedGUID(packet, "QualifiedGUID");
            var notesLength = packet.Translator.ReadBits(10);
            packet.Translator.ReadWoWString("Notes", notesLength);
        }
    }
}
