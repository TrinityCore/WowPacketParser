using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class AccountDataHandler
    {
        [Parser(Opcode.CMSG_REPORT_CLIENT_VARIABLES)]
        public static void HandleSaveClientVarables(Packet packet)
        {
            var varablesCount = packet.ReadUInt32("VarablesCount");

            for (var i = 0; i < varablesCount; ++i)
            {
                var variableNameLen = packet.ReadBits(6);
                var valueLen = packet.ReadBits(10);

                packet.WriteLine($"[{ i.ToString() }] VariableName: \"{ packet.ReadWoWString((int)variableNameLen) }\" Value: \"{ packet.ReadWoWString((int)valueLen) }\"");
            }
        }

        [Parser(Opcode.CMSG_REPORT_ENABLED_ADDONS)]
        public static void HandleSaveEnabledAddons(Packet packet)
        {
            var enableAddonsCount = packet.ReadUInt32("EnableAddonsCount");

            for (var i = 0; i < enableAddonsCount; ++i)
            {
                packet.ResetBitReader();

                var addonNameLen = packet.ReadBits(7);
                var versionLen = packet.ReadBits(6);

                packet.ReadBit("Loaded", i);
                packet.ReadBit("Disabled", i);

                if (addonNameLen > 1)
                    packet.ReadCString("AddonName", i);
                if (versionLen > 1)
                    packet.ReadCString("Version", i);
            }
        }

        [Parser(Opcode.SMSG_CACHE_INFO)]
        public static void HandleCacheInfo(Packet packet)
        {
            var cacheInfoCount = packet.ReadUInt32("CacheInfoCount");

            packet.ResetBitReader();

            var signatureLen = packet.ReadBits(6);

            for (var i = 0; i < cacheInfoCount; ++i)
            {
                packet.ResetBitReader();

                var variableNameLen = packet.ReadBits(6);
                var valueLen = packet.ReadBits(6);

                packet.WriteLine($"[{ i.ToString() }] VariableName: \"{ packet.ReadWoWString((int)variableNameLen) }\" Value: \"{ packet.ReadWoWString((int)valueLen) }\"");
            }

            packet.ReadWoWString("Signature", signatureLen);
        }
    }
}
