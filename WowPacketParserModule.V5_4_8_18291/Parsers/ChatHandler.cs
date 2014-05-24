using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;
using ChatMessageType540 = WowPacketParserModule.V5_4_0_17359.Enums.ChatMessageType;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.CMSG_MESSAGECHAT_SAY)]
        public static void HandleClientChatMessage(Packet packet)
        {
            packet.ReadEnum<Language>("Language", TypeCode.Int32);
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_WHISPER)]
        public static void HandleClientChatMessageWhisper(Packet packet)
        {
            packet.ReadEnum<Language>("Language2", TypeCode.Int32);
            var msgLen = packet.ReadBits(8);
            var recvName = packet.ReadBits(8);
            packet.ReadBit("unk");

            packet.ReadWoWString("Receivers Name", recvName);
            packet.ReadWoWString("Message", msgLen);
        }
    }
}
