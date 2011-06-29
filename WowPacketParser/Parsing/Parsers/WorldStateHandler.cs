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
            var mapId = packet.ReadInt32();
            Console.WriteLine("Map ID: " + mapId);

            var zoneId = packet.ReadInt32();
            Console.WriteLine("Zone ID: " + zoneId);

            var areaId = packet.ReadInt32();
            Console.WriteLine("Area ID: " + areaId);

            var numFields = packet.ReadInt16();
            Console.WriteLine("Field Count: " + numFields);

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
