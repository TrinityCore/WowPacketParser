using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class WorldStateHandler
    {
        [Parser(Opcode.SMSG_INIT_WORLD_STATES)]
        public static void HandleInitWorldStates(Packet packet)
        {
            packet.ReadInt32<MapId>("Map ID");
            packet.ReadInt32<ZoneId>("Zone Id");
            CoreParsers.WorldStateHandler.CurrentAreaId = packet.ReadInt32<AreaId>("Area Id");

            var numFields = packet.ReadBits("Field Count", 21);

            for (var i = 0; i < numFields; i++)
            {
                var val = packet.ReadInt32();
                var field = packet.ReadInt32();
                packet.AddValue("Field", field + " - Value: " + val, i);
            }
        }

        [Parser(Opcode.SMSG_UPDATE_WORLD_STATE)]
        public static void HandleUpdateWorldState(Packet packet)
        {
            packet.ReadBit("bit18");
            var val = packet.ReadInt32();
            var field = packet.ReadInt32();
            packet.AddValue("Field", field + " - Value: " + val);
        }
    }
}
