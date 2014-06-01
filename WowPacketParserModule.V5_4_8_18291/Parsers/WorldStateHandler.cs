using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class WorldStateHandler
    {
        [Parser(Opcode.SMSG_INIT_WORLD_STATES)]
        public static void HandleInitWorldStates(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");
            CoreParsers.WorldStateHandler.CurrentAreaId = packet.ReadEntryWithName<Int32>(StoreNameType.Area, "Area Id");
            packet.ReadEntryWithName<Int32>(StoreNameType.Zone, "Zone Id");

            var numFields = packet.ReadBits("Field Count", 21);

            for (var i = 0; i < numFields; i++)
            {
                var val = packet.ReadInt32();
                var field = packet.ReadInt32();
                packet.WriteLine("[{0}] - Field: {1} - Value: {2}", i, field, val);
            }
        }
    }
}
