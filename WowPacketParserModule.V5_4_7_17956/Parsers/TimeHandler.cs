using System;
using System.Collections;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_7_17956.Parsers
{
    public static class TimeHandler
    {
        public static void HandleGameSpeedSet(Packet packet)
        {
            packet.ReadSingle("New Game Speed");
        }

        public static void HandleGameTimeSet(Packet packet)
        {
            packet.ReadPackedTime("Time 1");
            packet.ReadPackedTime("New Time");
            packet.ReadInt32("Unk dword24");
            packet.ReadInt32("Unk dword20");
        }

        public static void HandleGameTimeUpdate(Packet packet)
        {
            packet.ReadInt32("Unk dword16");
            packet.ReadInt32("Unk dword28");
            packet.ReadPackedTime("Time 1");
            packet.ReadPackedTime("New Time");
        }

        public static void HandleLoginSetTimeSpeed(Packet packet)
        {
            packet.ReadPackedTime("Server Current Time");
            packet.ReadSingle("Game Speed");
            packet.ReadInt32("Unk dword32");
            packet.ReadInt32("Unk dword24");
            packet.ReadPackedTime("Time 1");
        }

        public static void HandleServerTime(Packet packet)
        {
            packet.ReadPackedTime("Server game time");
            packet.ReadUInt32("Server last tick");
        }
    }
}