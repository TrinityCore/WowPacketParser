using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class WorldStateHandler
    {
        [Parser(Opcode.SMSG_INIT_WORLD_STATES)]
        public static void HandleInitWorldStates(Packet packet)
        {
            packet.ReadInt32<ZoneId>("Zone Id");
            CoreParsers.WorldStateHandler.CurrentAreaId = packet.ReadInt32<AreaId>("Area Id");
            packet.ReadInt32<MapId>("Map ID");

            var numFields = packet.ReadBits("Field Count", 21);
            for (var i = 0; i < numFields; i++)
                CoreParsers.WorldStateHandler.ReadWorldStateBlock(packet);
        }
    }
}
