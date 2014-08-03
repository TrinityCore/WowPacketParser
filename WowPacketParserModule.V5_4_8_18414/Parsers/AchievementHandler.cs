using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA)]
        public static void HandleAllAchievementData(Packet packet)
        {
            var count = packet.ReadBits("count", 19);
            var guid = new byte[count][];
            var guid2 = new byte[count][];
            var cnt = new uint[count];
            for (var i = 0; i < count; i++)
            {
                guid[i] = new byte[8];
                guid2[i] = new byte[8];

                guid[i][0] = packet.ReadBit();
                guid2[i][7] = packet.ReadBit();

                cnt[i] = packet.ReadBits(4);

                guid2[i][4] = packet.ReadBit();
                guid[i][6] = packet.ReadBit();
                guid2[i][5] = packet.ReadBit();
                guid2[i][2] = packet.ReadBit();
                guid2[i][0] = packet.ReadBit();
                guid2[i][6] = packet.ReadBit();
                guid[i][5] = packet.ReadBit();
                guid[i][7] = packet.ReadBit();
                guid2[i][1] = packet.ReadBit();
                guid2[i][3] = packet.ReadBit();
                guid[i][4] = packet.ReadBit();
                guid[i][3] = packet.ReadBit();
                guid[i][2] = packet.ReadBit();
                guid[i][1] = packet.ReadBit();
            }
            for (var i = 0; i < count; i++)
            {
                packet.ParseBitStream(guid[i], 2);
                packet.ParseBitStream(guid2[i], 2, 4);
                packet.ParseBitStream(guid[i], 4);
                packet.ParseBitStream(guid2[i], 6, 0);
                packet.ReadInt32("unk276", i); // 276
                packet.ParseBitStream(guid2[i], 1, 7);
                packet.ReadPackedTime("Time", i);
                packet.ParseBitStream(guid[i], 7, 6, 5, 1);
                packet.ParseBitStream(guid2[i], 3);
                packet.ReadInt32("unk260", i); // 260
                packet.ParseBitStream(guid2[i], 5);
                packet.ParseBitStream(guid[i], 3, 0);
                packet.ReadInt32("Criteria ID", i); // 20
                packet.WriteGuid("Guid", guid[i], i);
                packet.WriteGuid("Guid2", guid2[i], i);
            }
        }
    }
}
