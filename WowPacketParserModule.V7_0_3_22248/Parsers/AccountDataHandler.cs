using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class AccountDataHandler
    {
        [Parser(Opcode.CMSG_SAVE_CLIENT_VARIABLES)]
        public static void HandleSaveClientVarables(Packet packet)
        {
            var varablesCount = packet.Translator.ReadUInt32("VarablesCount");

            for (var i = 0; i < varablesCount; ++i)
            {
                var variableNameLen = packet.Translator.ReadBits(6);
                var valueLen = packet.Translator.ReadBits(10);

                packet.Formatter.AppendItem($"[{ i.ToString() }] VariableName: \"{ packet.Translator.ReadWoWString((int)variableNameLen) }\" Value: \"{ packet.Translator.ReadWoWString((int)valueLen) }\"");
            }
        }

        [Parser(Opcode.CMSG_SAVE_ENABLED_ADDONS)]
        public static void HandleSaveEnabledAddons(Packet packet)
        {
            var enableAddonsCount = packet.Translator.ReadUInt32("EnableAddonsCount");

            for (var i = 0; i < enableAddonsCount; ++i)
            {
                packet.Translator.ResetBitReader();

                var addonNameLen = packet.Translator.ReadBits(7);
                var versionLen = packet.Translator.ReadBits(6);

                packet.Translator.ReadBit("Loaded", i);
                packet.Translator.ReadBit("Disabled", i);

                if (addonNameLen > 1)
                    packet.Translator.ReadCString("AddonName", i);
                if (versionLen > 1)
                    packet.Translator.ReadCString("Version", i);
            }
        }

        [Parser(Opcode.SMSG_CACHE_INFO)]
        public static void HandleCacheInfo(Packet packet)
        {
            var cacheInfoCount = packet.Translator.ReadUInt32("CacheInfoCount");

            packet.Translator.ResetBitReader();

            var signatureLen = packet.Translator.ReadBits(6);

            for (var i = 0; i < cacheInfoCount; ++i)
            {
                packet.Translator.ResetBitReader();

                var variableNameLen = packet.Translator.ReadBits(6);
                var valueLen = packet.Translator.ReadBits(6);

                packet.Formatter.AppendItem($"[{ i.ToString() }] VariableName: \"{ packet.Translator.ReadWoWString((int)variableNameLen) }\" Value: \"{ packet.Translator.ReadWoWString((int)valueLen) }\"");
            }

            packet.Translator.ReadWoWString("Signature", signatureLen);
        }
    }
}
