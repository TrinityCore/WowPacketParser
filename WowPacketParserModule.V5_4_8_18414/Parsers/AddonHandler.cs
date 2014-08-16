using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class AddonHandler
    {
        [Parser(Opcode.CMSG_ADDON_REGISTERED_PREFIXES)]
        public static void HandleCAddonRegisteredPrefixes(Packet packet)
        {
            var count = packet.ReadBits("count", 24);
            var len = new uint[count];
            for (var i = 0; i < count; i++)
                len[i] = packet.ReadBits(5);
            for (var i = 0; i < count; i++)
                packet.ReadWoWString("Addon", len[i], i);
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_ADDON_WHISPER)]
        public static void HandleMessageChatAddonWhisper(Packet packet)
        {
            var targetLen = packet.ReadBits(9);
            var messageLen = packet.ReadBits(8);
            var prefixLen = packet.ReadBits(5);
            packet.ReadWoWString("Target Name", targetLen);
            packet.ReadWoWString("Prefix", prefixLen);
            packet.ReadWoWString("Message", messageLen);
        }

        [Parser(Opcode.SMSG_ADDON_INFO)]
        public static void HandleServerAddonInfo(Packet packet)
        {
            var BannedAddonsCount = packet.ReadBits("Banned Addons Count", 18);
            var AddonsCount = packet.ReadBits("Addons Count", 23);
            uint[,] AddonsInfo = new uint[AddonsCount, 4];
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_UNREGISTER_ALL_ADDON_PREFIXES)]
        public static void HandleAddonNull(Packet packet)
        {
        }
    }
}
