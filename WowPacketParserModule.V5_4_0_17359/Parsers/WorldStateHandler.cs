using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class WorldStateHandler
    {
        [Parser(Opcode.SMSG_INIT_WORLD_STATES)]
        public static void HandleInitWorldStates(Packet packet)
        {
            PacketInitWorldStates worldStatesPacket = packet.Holder.InitWorldStates = new PacketInitWorldStates();
            worldStatesPacket.ZoneId = CoreParsers.WorldStateHandler.CurrentZoneId = packet.ReadInt32<ZoneId>("Zone Id");
            worldStatesPacket.AreaId = CoreParsers.WorldStateHandler.CurrentAreaId = packet.ReadInt32<AreaId>("Area Id");
            worldStatesPacket.MapId = packet.ReadInt32<MapId>("Map ID");

            var numFields = packet.ReadBits("Field Count", 21);
            for (var i = 0; i < numFields; i++)
                worldStatesPacket.WorldStates.Add(CoreParsers.WorldStateHandler.ReadWorldStateBlock(packet));
        }
    }
}
