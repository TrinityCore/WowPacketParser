using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class GameShopHandler
    {
        [Parser(Opcode.CMSG_GAME_SHOP_QUERY)]
        public static void HandleClientGameShopQuery(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_GAME_SHOP_QUERY_RESPONSE)] // sub_74F354
        public static void HandleServerGameShopQueryresponse(Packet packet)
        {
            /*var count36 = packet.ReadBits("count36", 19);
            var count16 = packet.ReadBits("count16", 19);
            var count44 = new uint[count16];
            var unk5312 = new bool[count16];
            for (var i = 0; i < count16; i++)
            {
                packet.ReadBits("unk60", 2);
                count44[i] = packet.ReadBits("unk44", 20);
                for (var j = 0; j < count44[i]; j++)
                {
                    packet.ReadBits("unk276", 10, i, j); // 69*4
                    packet.ReadBits("unk4380", 13, i, j); // 1095*4
                    packet.ReadBit("unk156", i, j);
                    packet.ReadBit("unk148", i, j);
                    packet.ReadBit("unk164", i, j);
                    packet.ReadBit("unk5296", i, j);
                    packet.ReadBits("unk2328", 10, i, j);
                }
                unk5312[i] = packet.ReadBit("unk5312", i);
                if (unk5312[i])
                    packet.ReadBits("unk5308", 4, i);

            }*/
            packet.ReadToEnd();
        }
    }
}
