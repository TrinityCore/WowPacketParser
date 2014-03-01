using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.CMSG_CHAR_DELETE)]
        public static void HandleClientCharDelete(Packet packet)
        {
            var playerGuid = new byte[8];

            packet.StartBitStream(playerGuid, 6, 4, 5, 1, 7, 3, 2, 0);
            packet.ParseBitStream(playerGuid, 1, 2, 3, 4, 0, 7, 6, 5);

            packet.WriteGuid("GUID", playerGuid);
        }

        [Parser(Opcode.SMSG_INIT_CURRENCY)]
        public static void HandleInitCurrency(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);
            if (count == 0)
                return;

            var hasWeekCount = new bool[count];
            var hasWeekCap = new bool[count];
            var hasSeasonTotal = new bool[count];
            var flags = new uint[count];

            for (var i = 0; i < count; ++i)
            {
                flags[i] = packet.ReadBits(5);          // 20h
                hasWeekCap[i] = packet.ReadBit();
                hasSeasonTotal[i] = packet.ReadBit();
                hasWeekCount[i] = packet.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {
                packet.WriteLine("[{0}] Flags {1}", i, flags[i]); // 20h
                packet.ReadUInt32("Currency count", i);
                packet.ReadUInt32("Currency id", i);

                if (hasWeekCap[i]) // 14h
                    packet.ReadUInt32("Weekly cap", i);

                if (hasWeekCount[i]) // 1Ch
                    packet.ReadUInt32("Weekly count", i);

                if (hasSeasonTotal[i]) // 0Ch
                    packet.ReadUInt32("Season total earned", i);
            }
        }

        [Parser(Opcode.CMSG_PLAYED_TIME)]
        public static void HandlePlayedTime(Packet packet)
        {
            packet.ReadBoolean("Print in chat");
        }
    }
}
