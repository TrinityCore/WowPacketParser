using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.SMSG_POWER_UPDATE)]
        public static void HandlePowerUpdate(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 4, 6, 7, 5, 2, 3, 0, 1);

            var count = packet.ReadBits("Count", 21);

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);

            for (var i = 0; i < count; i++)
            {
                packet.ReadEnum<PowerType>("Power type", TypeCode.Byte); // Actually powertype for class
                packet.ReadInt32("Value");
            }

            packet.ReadXORByte(guid, 6);

            packet.WriteGuid("Guid", guid);
        }        
    }
}
