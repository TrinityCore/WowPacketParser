using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class AddonHandler
    {
        [Parser(Opcode.SMSG_ADDON_INFO)]
        public static void HandleServerAddonInfo(Packet packet)
        {
            var BannedAddonsCount = packet.ReadBits("Banned Addons Count", 18);
            var AddonsCount = packet.ReadBits("Addons Count", 23);
            uint[,] AddonsInfo = new uint[AddonsCount, 4];

            packet.ReadToEnd();
        }
    }
}
