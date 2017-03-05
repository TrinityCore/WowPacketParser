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
                packet.ReadInt32("Blacklisted MapId", i);

            guid[1] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            packet.ReadBit("As Group");
            guid[4] = packet.ReadBit();
            var hasRoleMask = !packet.ReadBit("!HasRoleMask");
            guid[6] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            packet.ParseBitStream(guid, 7, 2, 4, 5, 0, 6, 3, 1);

            if (hasRoleMask)
                packet.ReadByte("RoleMask");

            packet.WriteGuid("Guid", guid);
        }
    }
}
