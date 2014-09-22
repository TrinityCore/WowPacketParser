using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParserModule.V5_4_8_18414.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class WorldStateHandler
    {
        [Parser(Opcode.SMSG_INIT_WORLD_STATES)]
        public static void HandleInitWorldStates(Packet packet)
        {
            packet.ReadEntry<Int32>(StoreNameType.Map, "Map ID");
            CoreParsers.WorldStateHandler.CurrentAreaId = packet.ReadEntry<Int32>(StoreNameType.Area, "Area Id");
            packet.ReadEntry<Int32>(StoreNameType.Zone, "Zone Id");


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

        [Parser(Opcode.SMSG_WORLD_STATE_UI_TIMER_UPDATE)]
        public static void HandleUpdateUITimer(Packet packet)
        {
            packet.ReadTime("Time");
        }
    }
}
