using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class TransmogrificationHandler
    {
        [Parser(Opcode.SMSG_ACCOUNT_TRANSMOG_UPDATE, ClientVersionBuild.V8_2_5_31921)]
        public static void HandleAccountTransmogUpdate(Packet packet)
        {
            packet.ReadBit("IsFullUpdate");
            packet.ReadBit("IsSetFavorite");
            var count = packet.ReadUInt32("FavoriteAppearancesCount");
            var newCount = packet.ReadUInt32("NewAppearancesCount");
            for (int i = 0; i < count; i++)
                packet.ReadUInt32("ItemModifiedAppearanceId", "FavoriteAppearances", i);
            for (int i = 0; i < newCount; i++)
                packet.ReadUInt32("ItemModifiedAppearanceId", "NewAppearances", i);
        }
    }
}
