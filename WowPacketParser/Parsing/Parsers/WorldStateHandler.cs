using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class WorldStateHandler
    {
        [Parser(Opcode.SMSG_INIT_WORLD_STATES)]
        public static void HandleInitWorldStates(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");
            packet.ReadInt32("Zone ID");
            packet.ReadInt32("Area ID");

            var numFields = packet.ReadInt16("Field Count");
            for (var i = 0; i < numFields; i++)
                ReadWorldStateBlock(ref packet);
        }

        public static void ReadWorldStateBlock(ref Packet packet)
        {
            packet.ReadInt32("Field");
            packet.ReadInt32("Value");
        }

        [Parser(Opcode.SMSG_UPDATE_WORLD_STATE)]
        public static void HandleUpdateWorldState(Packet packet)
        {
            ReadWorldStateBlock(ref packet);
        }

        [Parser(Opcode.SMSG_WORLD_STATE_UI_TIMER_UPDATE)]
        public static void HandleUpdateUITimer(Packet packet)
        {
            packet.ReadTime("Time");
        }
    }
}
