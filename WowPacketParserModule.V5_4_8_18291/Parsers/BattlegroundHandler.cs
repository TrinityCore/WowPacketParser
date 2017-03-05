using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class BattlegroundHandler
    {
        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN)]
        public static void HandleBattlemasterJoin434(Packet packet)
        {
            var guid = new byte[8];

            for (var i = 0; i < 2; ++i)
                packet.Translator.ReadInt32("Blacklisted MapId", i);

            guid[1] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("As Group");
            guid[4] = packet.Translator.ReadBit();
            var hasRoleMask = !packet.Translator.ReadBit("!HasRoleMask");
            guid[6] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            packet.Translator.ParseBitStream(guid, 7, 2, 4, 5, 0, 6, 3, 1);

            if (hasRoleMask)
                packet.Translator.ReadByte("RoleMask");

            packet.Translator.WriteGuid("Guid", guid);
        }
    }
}
