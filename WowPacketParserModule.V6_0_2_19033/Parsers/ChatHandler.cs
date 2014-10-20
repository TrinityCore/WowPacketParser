using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.SMSG_MESSAGECHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            var text = new CreatureText();

            text.Type = (ChatMessageType)packet.ReadByte("Chat type");
            text.Language = packet.ReadEnum<Language>("Language", TypeCode.Byte);

            packet.ReadPackedGuid128("SenderGUID");
            packet.ReadPackedGuid128("SenderGuildGUID");
            packet.ReadPackedGuid128("WowAccountGUID");
            packet.ReadPackedGuid128("TargetGUID");
            packet.ReadUInt32("TargetVirtualAddress");
            packet.ReadUInt32("SenderVirtualAddress");
            packet.ReadPackedGuid128("PartyGUID");
            packet.ReadUInt32("AchievementID");
            packet.ReadSingle("DisplayTime");

            var bits24 = packet.ReadBits(11);
            var bits1121 = packet.ReadBits(11);
            var prefixLen = packet.ReadBits(5);
            var channelLen = packet.ReadBits(7);
            var textLen = packet.ReadBits(12);
            var bits1490 = packet.ReadBits(10);

            packet.ReadBit("Bit5304");
            packet.ReadBit("Bit5305");

            packet.ReadWoWString("Sender Name", bits24);
            packet.ReadWoWString("Receiver Name", bits1121);
            packet.ReadWoWString("Addon Message Prefix", prefixLen);
            packet.ReadWoWString("Channel Name", channelLen);

            text.Text = packet.ReadWoWString("Text", textLen);
        }
    }
}
