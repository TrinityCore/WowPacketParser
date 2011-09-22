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
            Console.WriteLine("Map ID: " + Extensions.MapLine(packet.ReadInt32()));

            packet.ReadInt32("Zone ID");

            packet.ReadInt32("Area ID");

            var numFields = packet.ReadInt16("Field Count");

            for (var i = 0; i < numFields; i++)
                ReadWorldStateBlock(packet);
        }

        public static void ReadWorldStateBlock(Packet packet)
        {
            var fieldId = packet.ReadInt32();
            Console.WriteLine("Field: " + fieldId);

            var fieldVal = packet.ReadInt32();
            Console.WriteLine("Value: " + fieldVal);
        }

        [Parser(Opcode.SMSG_UPDATE_WORLD_STATE)]
        public static void HandleUpdateWorldState(Packet packet)
        {
            ReadWorldStateBlock(packet);
        }

        [Parser(Opcode.SMSG_WORLD_STATE_UI_TIMER_UPDATE)]
        public static void HandleUpdateUITimer(Packet packet)
        {
            var time = packet.ReadTime();
            Console.WriteLine("Time: " + time);
        }
    }
}
