using System;
using System.Collections;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class InstanceHandler
    {
        [Parser(Opcode.CMSG_SET_DUNGEON_DIFFICULTY)]
        [Parser(Opcode.CMSG_SET_RAID_DIFFICULTY)]
        [Parser(Opcode.SMSG_PLAYER_DIFFICULTY_CHANGE)]
        public static void HandleSetDifficulty(Packet packet)
        {
            packet.ReadEnum<MapDifficulty>("Difficulty", TypeCode.Int32);
        }
    }
}
