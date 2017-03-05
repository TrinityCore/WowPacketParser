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
            packet.Translator.ReadInt32<MapId>("Map ID");
            packet.Translator.ReadInt32<ZoneId>("Zone Id");
            CoreParsers.WorldStateHandler.CurrentAreaId = packet.Translator.ReadInt32<AreaId>("Area Id");

            var numFields = packet.Translator.ReadBits("Field Count", 21);

            for (var i = 0; i < numFields; i++)
            {
                var val = packet.Translator.ReadInt32();
                var field = packet.Translator.ReadInt32();
                packet.AddValue("Field", field + " - Value: " + val, i);
            }
        }

        [Parser(Opcode.SMSG_UPDATE_WORLD_STATE)]
        public static void HandleUpdateWorldState(Packet packet)
        {
            packet.Translator.ReadBit("bit18");
            var val = packet.Translator.ReadInt32();
            var field = packet.Translator.ReadInt32();
            packet.AddValue("Field", field + " - Value: " + val);
        }
    }
}
