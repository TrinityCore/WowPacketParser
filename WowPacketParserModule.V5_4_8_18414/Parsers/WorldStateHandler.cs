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
        public static int CurrentAreaId = -1;

        [Parser(Opcode.SMSG_INIT_WORLD_STATES)]
        public static void HandleInitWorldStates(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");
            packet.ReadEntryWithName<Int32>(StoreNameType.Zone, "Zone Id");
            CurrentAreaId = packet.ReadEntryWithName<Int32>(StoreNameType.Area, "Area Id");

            var numFields = packet.ReadBits("Field Count", 21);
            //packet.ReadByte("UnkB");
            /*for (var i = 0; i < numFields; i++)
            {
                var val = packet.ReadByte();
                packet.WriteLine("Field: {0} - Value: {1}", i, val);
            }*/
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_UPDATE_WORLD_STATE)]
        public static void HandleUpdateWorldState(Packet packet)
        {
            packet.ReadBit("Bit in Byte16");
            packet.ReadUInt32("Field");
            packet.ReadUInt32("Value");
        }

        [Parser(Opcode.SMSG_WORLD_STATE_UI_TIMER_UPDATE)]
        public static void HandleUpdateUITimer(Packet packet)
        {
            packet.ReadTime("Time");
        }
    }
}
