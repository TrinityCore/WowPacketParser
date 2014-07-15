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

        [Parser(Opcode.SMSG_MESSAGECHAT)] // sub_70A096
        public static void HandleMessageChat(Packet packet)
        {
            var text = new CreatureText();
            uint entry = 0;

            //text.Language = packet.ReadEnum<Language>("Language", TypeCode.Int32);

            var unk64 = packet.ReadBit("unk64*4") ? 0x80000000 : 0;
            packet.ReadBit("unk5272");
            if ((unk64 & 0x80000000)==0)
                unk64 = packet.ReadBits("unk64*4", 11);

            packet.ReadBit("!unk48*4");
            // unk52*4 = 0;
            var unk2131 = packet.ReadBit("unk2131*4") ? 0x80000000 : 0u;
            packet.ReadBit("unk5273*4");
            var has1494 = !packet.ReadBit("!unk1494h"); // dword ptr [esi+1494h] = ("has unk1494h") ? 0 : ds:dword_D26EA8
            var unk5264 = packet.ReadBit("!unk5264*2") ? 0u : 1u;
            var unk56 = !packet.ReadBit("!unk56*4");

            var guid4 = packet.StartBitStream(0, 1, 5, 4, 3, 2, 6, 7);

            if (unk5264 > 0)
                unk5264 = packet.ReadBits("unk5264*2", 9);

            packet.ReadBit("!unk40*4");
            // unk44*4 = 0
            var guid3 = packet.StartBitStream(7, 6, 1, 4, 0, 2, 3, 5);
            packet.ReadBit("!unk24*4");
            // unk28*4 = 0
            var unk17 = !packet.ReadBit("!unk17");
            var unk2114 = packet.ReadBit("unk2114*4") ? 0x80000000 : 0u;

            var guid = packet.StartBitStream(0, 3, 7, 2, 1, 5, 4, 6);

            var unk5260 = packet.ReadBit("!unk5260*4") ? 0u : 1u;
            var unk2259 = packet.ReadBit("has unk2259") ? 0x80000000 : 0u;

            if ((unk2131 & 0x80000000) == 0)
                unk2131 = packet.ReadBits("unk2131*4", 7);

            if ((unk2259 & 0x80000000) == 0)
                unk2259 = packet.ReadBits("unk2259*4", 12);

            var unk1089 = packet.ReadBit("unk1089*4") ? 0x80000000 : 0u;

            if ((unk2114 & 0x80000000) == 0)
                unk2114 = packet.ReadBits("unk2114*4", 5);

            var unk60 = !packet.ReadBit("!unk60*4");

            if ((unk1089 & 0x80000000) == 0)
                unk1089 = packet.ReadBits("unk1089*4", 11);

            packet.ReadBit("!unk32*4");
            // unk36 = 0

            var guid2 = packet.StartBitStream(2, 5, 7, 4, 0, 1, 3, 6);
            packet.ParseBitStream(guid2, 4, 5, 7, 3, 2, 6, 0, 1);
            packet.WriteGuid("Guid2", guid2);

            if ((unk2131 & 0x80000000) == 0)
                packet.ReadWoWString("str2131", unk2131);

            if ((unk2114 & 0x80000000) == 0)
                packet.ReadWoWString("str2114", unk2114);

            if (has1494)
                packet.ReadSingle("unk1494");

            packet.ParseBitStream(guid, 4, 7, 1, 5, 0, 6, 2, 3);
            packet.WriteGuid("Sender", guid);

            var unitGuid = new Guid(BitConverter.ToUInt64(guid, 0));

            if (unitGuid.GetObjectType() == ObjectType.Unit)
                entry = unitGuid.GetEntry();

            text.Type = packet.ReadEnum<ChatMessageType>("Type", TypeCode.Byte);

            if (unk5260 > 0)
            {
                unk5260 = packet.ReadUInt32("unk5260");
            }

            packet.ParseBitStream(guid4, 1, 3, 4, 6, 0, 2, 5, 7);
            packet.WriteGuid("Guid4", guid4);

            packet.ParseBitStream(guid3, 2, 5, 3, 6, 7, 4, 1, 0);
            packet.WriteGuid("Target", guid3);

            if (unk17)
                packet.ReadByte("unk17");

            if (unk60)
                packet.ReadInt32("unk60");

            if ((unk2259 & 0x80000000) == 0)
                text.Text = packet.ReadWoWString("Message", unk2259);

            if ((unk1089 & 0x80000000) == 0)
                text.Text += " -> " + packet.ReadWoWString("Target", unk1089);

            if ((unk64 & 0x80000000) == 0)
                text.Comment = packet.ReadWoWString("Sender", unk64);

            if (unk56)
                packet.ReadInt32("unk56");

            if (entry != 0)
                Storage.CreatureTexts.Add(entry, text, packet.TimeSpan);
        }
    }
}
