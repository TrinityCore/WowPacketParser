using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.SQL;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class WorldStateHandler
    {
        public static void ReadWorldStateBlock(Packet packet, params object[] indexes)
        {
            var val = packet.ReadInt32();
            var worldStateId = packet.ReadInt32();
            var comment = "";
            SQLDatabase.WorldStateNames.TryGetValue(worldStateId, out comment);
            packet.AddValue("WorldStateID", $"{worldStateId} - Value: {val} - {comment}", indexes);
        }

        [Parser(Opcode.SMSG_INIT_WORLD_STATES)]
        public static void HandleInitWorldStates(Packet packet)
        {
            var numFields = packet.ReadBits("Field Count", 21);
            CoreParsers.WorldStateHandler.CurrentAreaId = packet.ReadInt32<AreaId>("Area Id");

            for (var i = 0; i < numFields; i++)
                ReadWorldStateBlock(packet, i);

            CoreParsers.WorldStateHandler.CurrentZoneId = packet.ReadInt32<ZoneId>("Zone Id");
            packet.ReadInt32<MapId>("Map ID");
        }

        [Parser(Opcode.SMSG_UPDATE_WORLD_STATE)]
        public static void HandleUpdateWorldState(Packet packet)
        {
            packet.ReadBit("Hidden");
            ReadWorldStateBlock(packet);
        }
    }
}
