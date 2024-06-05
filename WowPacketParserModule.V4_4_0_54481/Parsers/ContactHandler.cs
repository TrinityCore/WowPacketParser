using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
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

            packet.ReadByte("Status", index);

            packet.ReadUInt32<AreaId>("AreaID", index);
            packet.ReadUInt32("Level", index);
            packet.ReadUInt32("ClassID", index);

            packet.ResetBitReader();

            var notesLen = packet.ReadBits(10);
            packet.ReadBit("Mobile");
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
    }
}
