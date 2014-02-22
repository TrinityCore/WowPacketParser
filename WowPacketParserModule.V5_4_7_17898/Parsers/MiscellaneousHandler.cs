using System;
using System.Text;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.SMSG_WEATHER)]
        public static void HandleWeatherStatus(Packet packet)
        {
            packet.ReadSingle("Grade");
            packet.ReadEnum<WeatherState>("State", TypeCode.Int32);
            packet.ReadBit("Unk Bit"); // Type
        }
        
        [Parser(Opcode.SMSG_WEEKLY_SPELL_USAGE)]
        public static void HandleWeeklySpellUsage(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);

            for (int i = 0; i < count; ++i)
            {
                packet.ReadByte("Unk Int8");
                packet.ReadInt32("Unk Int32");
            }
        }
    }
}
