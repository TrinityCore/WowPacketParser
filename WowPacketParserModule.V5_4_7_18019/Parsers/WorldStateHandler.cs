using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
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

            var numFields = packet.ReadInt16("Field Count");
            //packet.ReadByte("UnkB");
            /*for (var i = 0; i < numFields; i++)
            {
                var val = packet.ReadByte();
                packet.WriteLine("Field: {0} - Value: {1}", i, val);
            }*/
            packet.ReadToEnd();
        }
    }
}
