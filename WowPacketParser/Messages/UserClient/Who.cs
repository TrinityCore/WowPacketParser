using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct Who
    {
        public WhoRequest Request;
        public List<int> Areas;

        [Parser(Opcode.CMSG_WHO, ClientVersionBuild.Zero, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleWhoRequest(Packet packet)
        {
            packet.ReadInt32("Min Level");
            packet.ReadInt32("Max Level");
            packet.ReadCString("Player Name");
            packet.ReadCString("Guild Name");
            packet.ReadInt32("RaceMask");
            packet.ReadInt32("ClassMask");

            var zones = packet.ReadUInt32("Zones count");
            for (var i = 0; i < zones; ++i)
                packet.ReadUInt32<ZoneId>("Zone Id");

            var patterns = packet.ReadUInt32("Pattern count");
            for (var i = 0; i < patterns; ++i)
                packet.ReadCString("Pattern", i);
        }

        [Parser(Opcode.CMSG_WHO, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_8_18291)]
        public static void HandleWhoRequest547(Packet packet)
        {
            packet.ReadInt32("RaceMask");
            packet.ReadInt32("Max Level");
            packet.ReadInt32("Min Level");
            packet.ReadInt32("ClassMask");
            var guildNameLen = packet.ReadBits(7);
            packet.ReadBit("bit2C6");
            var patterns = packet.ReadBits(3);
            packet.ReadBit("bit2C5");
            var zones = packet.ReadBits(4);

            var bits2B8 = new uint[patterns];

            for (var i = 0; i < patterns; ++i)
                bits2B8[i] = packet.ReadBits(7);

            var bits73 = packet.ReadBits(9);
            var PlayerNameLen = packet.ReadBits(6);
            packet.ReadBit("bit2C4");
            var bit2D4 = packet.ReadBit();
            var bits1AB = packet.ReadBits(9);

            packet.ReadWoWString("string73", bits73);

            for (var i = 0; i < zones; ++i)
                packet.ReadInt32<ZoneId>("Zone Id");

            packet.ReadWoWString("Guild Name", guildNameLen);
            packet.ReadWoWString("string1AB", bits1AB);
            packet.ReadWoWString("Player Name", PlayerNameLen);

            for (var i = 0; i < patterns; ++i)
                packet.ReadWoWString("Pattern", bits2B8[i], i);

            if (bit2D4)
            {
                packet.ReadInt32("Int2C8");
                packet.ReadInt32("Int2D0");
                packet.ReadInt32("Int2CC");
            }
        }


        [Parser(Opcode.CMSG_WHO, ClientVersionBuild.V5_4_8_18291, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleWhoRequest548(Packet packet)
        {
            packet.ReadInt32("ClassMask");
            packet.ReadInt32("RaceMask");
            packet.ReadInt32("Max Level");
            packet.ReadInt32("Min Level");


            packet.ReadBit("bit2C4");
            packet.ReadBit("bit2C6");
            var bit2D4 = packet.ReadBit();
            var bits1AB = packet.ReadBits(9);
            packet.ReadBit("bit2C5");
            var PlayerNameLen = packet.ReadBits(6);
            var zones = packet.ReadBits(4);
            var bits73 = packet.ReadBits(9);
            var guildNameLen = packet.ReadBits(7);
            var patterns = packet.ReadBits(3);

            var bits2B8 = new uint[patterns];
            for (var i = 0; i < patterns; ++i)
                bits2B8[i] = packet.ReadBits(7);

            for (var i = 0; i < patterns; ++i)
                packet.ReadWoWString("Pattern", bits2B8[i], i);

            packet.ReadWoWString("string1AB", bits1AB);

            for (var i = 0; i < zones; ++i)
                packet.ReadInt32<ZoneId>("Zone Id");

            packet.ReadWoWString("Player Name", PlayerNameLen);

            packet.ReadWoWString("string73", bits73);

            packet.ReadWoWString("Guild Name", guildNameLen);

            if (bit2D4)
            {
                packet.ReadInt32("Int2C8");
                packet.ReadInt32("Int2D0");
                packet.ReadInt32("Int2CC");
            }
        }

        [Parser(Opcode.CMSG_WHO, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleWhoRequest602(Packet packet)
        {
            var bits728 = packet.ReadBits("WhoWordCount", 4);

            packet.ReadInt32("MinLevel");
            packet.ReadInt32("MaxLevel");
            packet.ReadInt32("RaceFilter");
            packet.ReadInt32("ClassFilter");

            packet.ResetBitReader();

            var bits2 = packet.ReadBits(6);
            var bits57 = packet.ReadBits(9);
            var bits314 = packet.ReadBits(7);
            var bits411 = packet.ReadBits(9);
            var bit169 = packet.ReadBits(3);

            packet.ReadBit("ShowEnemies");
            packet.ReadBit("ShowArenaPlayers");
            packet.ReadBit("ExactName");
            var bit708 = packet.ReadBit("HasServerInfo");

            packet.ReadWoWString("Name", bits2);
            packet.ReadWoWString("VirtualRealmName", bits57);
            packet.ReadWoWString("Guild", bits314);
            packet.ReadWoWString("GuildVirtualRealmName", bits411);

            for (var i = 0; i < bit169; ++i)
            {
                packet.ResetBitReader();
                var bits0 = packet.ReadBits(7);
                packet.ReadWoWString("Word", bits0, i);
            }

            // WhoRequestServerInfo
            if (bit708)
            {
                packet.ReadInt32("FactionGroup");
                packet.ReadInt32("Locale");
                packet.ReadInt32("RequesterVirtualRealmAddress");
            }

            for (var i = 0; i < bits728; ++i)
                packet.ReadUInt32<AreaId>("Area", i);
        }
    }
}
