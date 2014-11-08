using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class WorldStateHandler
    {
        public static void ReadWorldStateBlock(ref Packet packet, params object[] indexes)
        {
            var field = packet.ReadInt32();
            var val = packet.ReadInt32();
            packet.AddValue("VariableID", field + " - Value: " + val, indexes);
        }

        [Parser(Opcode.SMSG_INIT_WORLD_STATES)]
        public static void HandleInitWorldStates(Packet packet)
        {
            packet.ReadEntry<Int32>(StoreNameType.Map, "Map ID");
            packet.ReadEntry<Int32>(StoreNameType.Zone, "AreaID");
            CoreParsers.WorldStateHandler.CurrentAreaId = packet.ReadEntry<Int32>(StoreNameType.Area, "SubareaID");

            var numFields = packet.ReadInt32("Field Count");
            for (var i = 0; i < numFields; i++)
                ReadWorldStateBlock(ref packet);
        }

        [Parser(Opcode.SMSG_UPDATE_WORLD_STATE)]
        public static void HandleUpdateWorldState(Packet packet)
        {
            ReadWorldStateBlock(ref packet);
            packet.ReadBit("Hidden");
        }
    }
}
