using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct SetContactNotes
    {
        public QualifiedGUID Player;
        public string Notes;

        [Parser(Opcode.CMSG_SET_CONTACT_NOTES, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_1903)]
        public static void HandleSetContactNotes(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadCString("Name");
        }

        [Parser(Opcode.CMSG_SET_CONTACT_NOTES, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleSetContactNotes602(Packet packet)
        {
            readQualifiedGUID(packet, "QualifiedGUID");
            var notesLength = packet.ReadBits(10);
            packet.ReadWoWString("Notes", notesLength);
        }

        private static void readQualifiedGUID(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("VirtualRealmAddress", indexes);
            packet.ReadPackedGuid128("Guid", indexes);
        }
    }
}
