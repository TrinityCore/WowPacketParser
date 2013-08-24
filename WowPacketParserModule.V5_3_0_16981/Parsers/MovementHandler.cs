using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class MovementHandler
    {
        [Parser(Opcode.SMSG_BINDPOINTUPDATE)]
        public static void HandleBindPointUpdate(Packet packet)
        {
            packet.ReadSingle("Position Z");
            packet.ReadSingle("Position Y");
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map Id");
            packet.ReadEntryWithName<Int32>(StoreNameType.Zone, "Zone Id");
            packet.ReadSingle("Position X");
        }

        [Parser(Opcode.SMSG_LOGIN_SETTIMESPEED)]
        public static void HandleLoginSetTimeSpeed(Packet packet)
        {
            packet.ReadPackedTime("Game Time");
            packet.ReadUInt32("bit5");
            packet.ReadUInt32("bit7");
            packet.ReadUInt32("bit6");
            packet.ReadSingle("Game Speed");
        }
    }
}
