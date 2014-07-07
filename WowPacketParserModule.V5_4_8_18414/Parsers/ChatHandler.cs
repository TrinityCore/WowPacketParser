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
        [Parser(Opcode.CMSG_MESSAGECHAT_EMOTE)]
        public static void HandleClientChatMessageEmote(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                var len = packet.ReadBits(8);
                packet.ReadWoWString("Message", len);
            }
            else
            {
                packet.WriteLine("              : SMSG_UNK_103E");
                packet.ReadInt32("Int32");
                packet.ReadInt32("Int28");
                packet.ReadInt32("Int24");
                packet.ReadInt64("QW16");
            }
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_GUILD)]
        public static void HandleClientChatMessageGuild(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadEnum<Language>("Language", TypeCode.Int32);
                packet.ReadWoWString("Message", packet.ReadBits(8));
            }
            else
            {
                packet.WriteLine("              : SMSG_UNK_0CAE");
                var guid = packet.StartBitStream(4, 3, 0, 2, 1, 6, 5, 7);
                packet.ParseBitStream(guid, 1, 4, 5, 6, 2, 0, 3, 7);
                packet.WriteGuid("Guid", guid);
            }
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_PARTY)]
        public static void HandleMessageChatParty(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadEnum<Language>("Language", TypeCode.Int32);
                packet.ReadWoWString("Message", packet.ReadBits(8));
            }
            else
            {
                packet.WriteLine("              : SMSG_UNK_109A");
                var guid = packet.StartBitStream(6, 7, 2, 5, 3, 0, 1, 4);
                packet.ParseBitStream(guid, 2, 5, 6, 7, 1, 4, 3, 0);
                packet.WriteGuid("Guid", guid);
            }
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_SAY)]
        public static void HandleClientChatMessageSay(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadEnum<Language>("Language", TypeCode.Int32);
                packet.ReadWoWString("Message", packet.ReadBits(8));
            }
            else
            {
                packet.WriteLine("              : SMSG_PARTY_MEMBER_STATS");
                var guid = new byte[8];
                guid[0] = packet.ReadBit();
                guid[5] = packet.ReadBit();

                var byte28 = packet.ReadBit("byte28");

                guid[1] = packet.ReadBit();
                guid[4] = packet.ReadBit();

                var byte29 = packet.ReadBit("byte29");

                guid[6] = packet.ReadBit();
                guid[2] = packet.ReadBit();
                guid[7] = packet.ReadBit();
                guid[3] = packet.ReadBit();
                packet.ParseBitStream(guid, 3, 2, 6, 7, 5);

                var int24 = packet.ReadInt32("int24");

                packet.ParseBitStream(guid, 1, 4, 0);
                packet.WriteGuid("Guid", guid);

                var count = packet.ReadInt32("count");
                for (var i = 0; i < count; i++)
                {
                    packet.ReadByte("byte", i);
                }
                packet.ReadWoWString("str", count);
            }
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_WHISPER)]
        public static void HandleClientChatMessageWhisper(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadEnum<ChatMessageType>("Type", TypeCode.UInt32);
                var msgLen = packet.ReadBits(8);
                var recvName = packet.ReadBits(9);

                packet.ReadWoWString("Message", msgLen);
                packet.ReadWoWString("Receivers Name", recvName);
            }
            else
            {
                packet.WriteLine("              : SMSG_UNK_123E"); // sub_7334E3
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_YELL)]
        public static void HandleClientChatMessageYell(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadEnum<Language>("Language", TypeCode.Int32);
                packet.ReadWoWString("Message", packet.ReadBits(8));
            }
            else
            {
                packet.WriteLine("              : SMSG_UNK_04AA"); // sub_6B9D5D
                packet.ReadSingle("unk1");
                packet.ReadInt32("unk2");
            }
        }

        [Parser(Opcode.SMSG_MESSAGECHAT)] // Similar to SMSG_MESSAGECHAT
        public static void HandleMessageChat(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
