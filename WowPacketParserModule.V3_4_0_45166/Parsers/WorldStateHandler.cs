using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.SQL;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class WorldStateHandler
    {
        public static void ReadWorldStateBlock(Packet packet, params object[] indexes)
        {
            var worldStateId = packet.ReadInt32();
            var val = packet.ReadInt32();
            var comment = "";
            SQLDatabase.WorldStateNames.TryGetValue(worldStateId, out comment);
            packet.AddValue("WorldStateID", $"{worldStateId} - Value: {val} - {comment}", indexes);
        }

        [Parser(Opcode.SMSG_SERVER_TIME_OFFSET, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleUITimer(Packet packet)
        {
            packet.ReadTime64("Time");
        }

        [Parser(Opcode.SMSG_INIT_WORLD_STATES, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleInitWorldStates(Packet packet)
        {
            packet.ReadInt32<MapId>("MapID");
            CoreParsers.WorldStateHandler.CurrentZoneId = packet.ReadInt32<ZoneId>("AreaId");
            CoreParsers.WorldStateHandler.CurrentAreaId = packet.ReadInt32<AreaId>("SubareaID");

            var numFields = packet.ReadInt32("Field Count");
            for (var i = 0; i < numFields; i++)
                ReadWorldStateBlock(packet);
        }

        [Parser(Opcode.SMSG_UPDATE_WORLD_STATE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleUpdateWorldState(Packet packet)
        {
            ReadWorldStateBlock(packet);
            packet.ReadBit("Hidden");
        }
    }
}
