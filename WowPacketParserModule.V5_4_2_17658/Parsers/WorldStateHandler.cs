using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class WorldStateHandler
    {
        [Parser(Opcode.SMSG_INIT_WORLD_STATES)]
        public static void HandleInitWorldStates(Packet packet)
        {
            var numFields = packet.Translator.ReadBits("Field Count", 21);
            CoreParsers.WorldStateHandler.CurrentAreaId = packet.Translator.ReadInt32<AreaId>("Area Id");

            for (var i = 0; i < numFields; i++)
            {
                var val = packet.Translator.ReadInt32();
                var field = packet.Translator.ReadInt32();
                packet.AddValue("Field", field + " - Value: " + val, i);
            }

            packet.Translator.ReadInt32<ZoneId>("Zone Id");
            packet.Translator.ReadInt32<MapId>("Map ID");
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
