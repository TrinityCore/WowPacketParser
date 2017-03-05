using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class WorldStateHandler
    {
        public static void ReadWorldStateBlock(Packet packet, params object[] indexes)
        {
            var field = packet.ReadInt32();
            var val = packet.ReadInt32();
            packet.AddValue("VariableID", field + " - Value: " + val, indexes);
        }

        [Parser(Opcode.SMSG_INIT_WORLD_STATES)]
        public static void HandleInitWorldStates(Packet packet)
        {
            packet.ReadInt32<MapId>("Map ID");
            packet.ReadInt32<ZoneId>("AreaID");
            CoreParsers.WorldStateHandler.CurrentAreaId = packet.ReadInt32<AreaId>("SubareaID");

            var numFields = packet.ReadInt32("Field Count");
            for (var i = 0; i < numFields; i++)
                ReadWorldStateBlock(packet);
        }

        [Parser(Opcode.SMSG_UPDATE_WORLD_STATE)]
        public static void HandleUpdateWorldState(Packet packet)
        {
            ReadWorldStateBlock(packet);
            packet.ReadBit("Hidden");
        }
    }
}
