using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.SQL;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class WorldStateHandler
    {
        public static WorldState ReadWorldStateBlock(Packet packet, params object[] indexes)
        {
            var worldStateId = packet.ReadInt32();
            var val = packet.ReadInt32();
            var comment = "";
            SQLDatabase.WorldStateNames.TryGetValue(worldStateId, out comment);
            packet.AddValue("WorldStateID", $"{worldStateId} - Value: {val} - {comment}", indexes);
            return new WorldState()
            {
                Id = worldStateId,
                Value = val
            };
        }

        [Parser(Opcode.SMSG_INIT_WORLD_STATES)]
        public static void HandleInitWorldStates(Packet packet)
        {
            PacketInitWorldStates worldStatesPacket = packet.Holder.InitWorldStates = new PacketInitWorldStates();
            worldStatesPacket.MapId = packet.ReadInt32<MapId>("MapID");
            worldStatesPacket.ZoneId = CoreParsers.WorldStateHandler.CurrentZoneId = packet.ReadInt32<ZoneId>("AreaId");
            worldStatesPacket.AreaId = CoreParsers.WorldStateHandler.CurrentAreaId = packet.ReadInt32<AreaId>("SubareaID");

            var numFields = packet.ReadInt32("Field Count");
            for (var i = 0; i < numFields; i++)
                worldStatesPacket.WorldStates.Add(ReadWorldStateBlock(packet));
        }

        [Parser(Opcode.SMSG_UPDATE_WORLD_STATE)]
        public static void HandleUpdateWorldState(Packet packet)
        {
            PacketUpdateWorldState updateWorldState = packet.Holder.UpdateWorldState = new PacketUpdateWorldState();
            updateWorldState.WorldState = ReadWorldStateBlock(packet);
            updateWorldState.Hidden = packet.ReadBit("Hidden");
        }
    }
}
