using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class ContactHandler
    {
        public static void ReadContactInfo(Packet packet, params object[] index)
        {
            packet.ReadPackedGuid128("Guid", index);
            packet.ReadPackedGuid128("WowAccount", index);

            packet.ReadUInt32("VirtualRealmAddr", index);
            packet.ReadUInt32("NativeRealmAddr", index);
            packet.ReadUInt32("TypeFlags", index);

            packet.ReadByteE<ContactStatus>("Status", index);

            packet.ReadUInt32<AreaId>("AreaID", index);
            packet.ReadUInt32("Level", index);
            packet.ReadByteE<Class>("ClassID", index);

            packet.ResetBitReader();

            var notesLen = packet.ReadBits(10);
            packet.ReadWoWString("Notes", notesLen);
        }

        [Parser(Opcode.SMSG_CONTACT_LIST)]
        public static void HandleContactList(Packet packet)
        {
            packet.ReadInt32E<ContactListFlag>("List Flags");
            var contactInfoCount = packet.ReadBits("ContactInfoCount", 8);

            for (var i = 0; i < contactInfoCount; i++)
                ReadContactInfo(packet, i);
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
            packet.ReadByteE<Class>("ClassID");

            packet.ResetBitReader();

            var notesLen = packet.ReadBits(10);
            packet.ReadWoWString("Notes", notesLen);
        }
    }
}
