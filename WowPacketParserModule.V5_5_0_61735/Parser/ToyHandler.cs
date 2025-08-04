using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class ToyHandler
    {
        [Parser(Opcode.SMSG_ACCOUNT_TOY_UPDATE)]
        public static void HandleAccountToyUpdate(Packet packet)
        {
            packet.ReadBit("IsFullUpdate");

            var itemIdCount = packet.ReadUInt32("ToyItemIDsCount");
            var isFavoriteCount = packet.ReadUInt32("ToyIsFavoriteCount");
            uint fanfareCount = packet.ReadUInt32("FanfareCount");

            for (int i = 0; i < itemIdCount; i++)
                packet.ReadInt32("ToyItemID", i);

            packet.ResetBitReader();

            for (int i = 0; i < isFavoriteCount; i++)
                packet.ReadBit("ToyIsFavorite", i);

            for (int i = 0; i < fanfareCount; i++)
                packet.ReadBit("HasFanfare", i);
        }
    }
}
