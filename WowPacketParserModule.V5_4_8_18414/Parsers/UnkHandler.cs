using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParserModule.V5_4_8_18414.Enums;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class UnkHandler
    {
        [Parser(Opcode.CMSG_UNK_0002)]
        public static void HandleUnk0002(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadBit("unk");
            }
            else
            {
                packet.WriteLine("              : SMSG_UNK_0002");
                packet.ReadInt32("unk");
            }
        }

        [Parser(Opcode.SMSG_UNK_0CAE)]
        public static void HandleUnk0CAE(Packet packet)
        {
            var guid = packet.StartBitStream(4, 3, 0, 2, 1, 6, 5, 7);
            packet.ParseBitStream(guid, 1, 4, 5, 6, 2, 0, 3, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_0D51)]
        public static void HandleUnk0D51(Packet packet)
        {
            var count = packet.ReadBits("count", 22);
            for (var i = 0; i < count; i++)
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);
        }

        [Parser(Opcode.SMSG_UNK_103E)]
        public static void HandleSUnk103E(Packet packet)
        {
            packet.ReadInt32("Int32");
            packet.ReadInt32("Int28");
            packet.ReadInt32("Int24");
            packet.ReadInt64("QW16");
        }

        [Parser(Opcode.SMSG_UNK_109A)]
        public static void HandleSUnk109A(Packet packet)
        {
            var guid = packet.StartBitStream(6, 7, 2, 5, 3, 0, 1, 4);
            packet.ParseBitStream(guid, 2, 5, 6, 7, 1, 4, 3, 0);
            packet.WriteGuid("Guid", guid);
        }
    }
}
