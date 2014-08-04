using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using TradeStatus548 = WowPacketParserModule.V5_4_8_18414.Enums.TradeStatus;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class TradeHandler
    {
        [Parser(Opcode.SMSG_GAME_STORE_INGAME_BUY_FAILED)]
        public static void HandleGameStoreIngameBuyFailed(Packet packet)
        {
            var count = packet.ReadBits("count", 19);
            var unk44 = new uint[count];
            for (var i = 0; i < count; i++)
                unk44[i] = packet.ReadBits("unk44", 8, i);
            for (var i = 0; i < count; i++)
            {
                packet.ReadInt64("unk24", i);
                packet.ReadInt32("unk54", i);
                var unk88 = packet.ReadInt32("unk88", i);
                packet.ReadWoWString("str", unk88, i);
                packet.ReadInt32("unk72", i);
            }
            packet.ReadInt32("unk16");
        }
    }
}
