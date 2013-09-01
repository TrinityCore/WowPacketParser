using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class WorldStateHandler
    {
        [Parser(Opcode.SMSG_INIT_WORLD_STATES)]
        public static void HandleInitWorldStates(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");
            packet.ReadEntryWithName<Int32>(StoreNameType.Zone, "Zone Id");
            CoreParsers.WorldStateHandler.CurrentAreaId = packet.ReadEntryWithName<Int32>(StoreNameType.Area, "Area Id");

            var numFields = packet.ReadBits("Field Count", 21);
            for (var i = 0; i < numFields; i++)
                CoreParsers.WorldStateHandler.ReadWorldStateBlock(ref packet);
        }
    }
}
