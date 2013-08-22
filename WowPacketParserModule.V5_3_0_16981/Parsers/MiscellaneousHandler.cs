using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.CMSG_SET_ACTION_BUTTON)]
        public static void HandleSetActionButton(Packet packet)
        {
            packet.ReadByte("Slot Id");
            var actionId = packet.StartBitStream(0, 4, 7, 2, 5, 3, 1, 6);
            packet.ParseBitStream(actionId, 7, 3, 0, 2, 1, 4, 5, 6);
            packet.WriteLine("Action Id: {0}", BitConverter.ToUInt32(actionId, 0));
        }

        [Parser(Opcode.CMSG_SET_SELECTION)]
        public static void HandleSetSelection(Packet packet)
        {
            var guid = packet.StartBitStream(3, 5, 6, 4, 2, 7, 1, 2);
            packet.ParseBitStream(guid, 7, 6, 4, 0, 3, 1, 2, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_REALM_SPLIT)]
        public static void HandleServerRealmSplit(Packet packet)
        {
            var len = packet.ReadBits(7);
            packet.ReadWoWString("Split Date", len);
            packet.ReadUInt32("Client State");
            packet.ReadUInt32("Split State");
        }
    }
}
