using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;
using ChatMessageType548 = WowPacketParserModule.V5_4_8_18414.Enums.ChatMessageType;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.CMSG_MESSAGECHAT_SAY)]
        public static void HandleClientChatMessageSay(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadEnum<Language>("Language", TypeCode.Int32);
                packet.ReadWoWString("Message", packet.ReadBits(9));
            }
            else
            {
                packet.WriteLine("              : SMSG_PARTY_MEMBER_STATS");
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.SMSG_MESSAGECHAT)] // Similar to SMSG_MESSAGECHAT
        public static void HandleMessageChat(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
