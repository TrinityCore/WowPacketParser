using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliChatRegisterAddonPrefixes
    {
        public List<CliStructAddonPrefix> Prefixes;

        [Parser(Opcode.CMSG_CHAT_REGISTER_ADDON_PREFIXES)]
        public static void HandleChatRegisterAddonPrefixes602(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; ++i)
            {
                var lengths = (int)packet.ReadBits(5);
                packet.ResetBitReader();
                packet.ReadWoWString("Addon", lengths, i);
            }
        }
    }
}
