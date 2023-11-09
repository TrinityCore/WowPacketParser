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
            var nameLength = packet.ReadBits(9);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_5_40772))
                packet.ReadPackedGuid128("AccountGUID");
            packet.ReadWoWString("Name", nameLength);
        }

        [Parser(Opcode.CMSG_ADD_FRIEND)]
        public static void HandleAddFriend(Packet packet)
        {
            var nameLength = packet.ReadBits(9);
            uint notesLength = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_0_52038))
                notesLength = packet.ReadBits(9);
            else
                notesLength = packet.ReadBits(10);

            packet.ReadWoWString("Name", nameLength);
            packet.ReadWoWString("Notes", notesLength);
        }

        public static void ReadQualifiedGUID(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("VirtualRealmAddress", indexes);
            packet.ReadPackedGuid128("Guid", indexes);
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
            packet.ReadByte("FriendResult");

            packet.ReadPackedGuid128("Guid");
            packet.ReadPackedGuid128("WowAccount");

            packet.ReadInt32("VirtualRealmAddress");

            packet.ReadByteE<ContactStatus>("Status");

            packet.ReadInt32<AreaId>("AreaID");
            packet.ReadInt32("Level");
            packet.ReadInt32E<Class>("ClassID");

            packet.ResetBitReader();

            var notesLen = packet.ReadBits(10);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_0_30898))
                packet.ReadBit("Mobile");
            packet.ReadWoWString("Notes", notesLen);
        }

        public static void ReadContactInfo(Packet packet, params object[] index)
        {
            packet.ReadPackedGuid128("Guid", index);
            packet.ReadPackedGuid128("WowAccount", index);

            packet.ReadUInt32("VirtualRealmAddr", index);
            packet.ReadUInt32("NativeRealmAddr", index);
            packet.ReadUInt32("TypeFlags", index);

            packet.ReadByte("Status", index);

            packet.ReadUInt32<AreaId>("AreaID", index);
            packet.ReadUInt32("Level", index);
            packet.ReadUInt32("ClassID", index);

            packet.ResetBitReader();

            var notesLen = packet.ReadBits(10);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_0_30898))
                packet.ReadBit("Mobile");
            packet.ReadWoWString("Notes", notesLen);
        }

        [Parser(Opcode.SMSG_CONTACT_LIST)]
        public static void HandleContactList(Packet packet)
        {
            packet.ReadInt32E<ContactListFlag>("List Flags");
            var bits6 = packet.ReadBits("ContactInfoCount", 8);

            for (var i = 0; i < bits6; i++)
                ReadContactInfo(packet, i);
        }

        [Parser(Opcode.CMSG_SEND_CONTACT_LIST)]
        public static void HandleSendContactList(Packet packet)
        {
            packet.ReadInt32("Flags");
        }

        [Parser(Opcode.CMSG_SET_CONTACT_NOTES)]
        public static void HandleSetContactNotes(Packet packet)
        {
            ReadQualifiedGUID(packet, "QualifiedGUID");
            var notesLength = packet.ReadBits(10);
            packet.ReadWoWString("Notes", notesLength);
        }
    }
}
