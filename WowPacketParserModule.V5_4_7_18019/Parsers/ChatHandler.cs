using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;
using ChatMessageType547 = WowPacketParserModule.V5_4_7_18019.Enums.ChatMessageType;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.CMSG_LFG_GET_STATUS)]
        public static void HandleLFGGetStatus(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_LFG_JOIN)]
        public static void HandleLFGJoin(Packet packet)
        {
            packet.ReadByte("unk1");
            packet.ReadInt32("unk2");
            packet.ReadInt32("unk3");
            packet.ReadInt32("unk4");
            packet.ReadInt32("Roles");
            var numDungeons = packet.ReadBits("Num Dungeons", 22);
            var commentLen = packet.ReadBits("CommentLen", 8);
            packet.ReadBit("unk5");
            packet.ReadWoWString("Comment", commentLen);
            for (var i = 0; i < numDungeons; ++i)
            {
                packet.ReadInt32("Dungeon", i);
            }
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_AFK)]
        public static void HandleMessageChatAfk(Packet packet)
        {
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Away Message", len);
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_WHISPER)]
        public static void HandleClientChatMessageWhisper434(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadEnum<ChatMessageType>("Type", TypeCode.UInt32);
                var recvName = packet.ReadBits(10);
                var msgLen = packet.ReadBits(9);

                packet.ReadWoWString("Receivers Name", recvName);
                packet.ReadWoWString("Message", msgLen);
            }
            else
            {
                packet.WriteLine("              : SMSG_???");
                //packet.Opcode = (int)Opcode.CMSG_MOUNTSPECIAL_ANIM;
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_RAID)]
        [Parser(Opcode.CMSG_MESSAGECHAT_YELL)]
        public static void HandleClientChatMessageYell(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadToEnd();
            }
            else
            {
                packet.WriteLine("              : SMSG_???");
                //packet.Opcode = (int)Opcode.CMSG_MOUNTSPECIAL_ANIM;
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.CMSG_TEXT_EMOTE)]
        public static void HandleTextEmote(Packet packet)
        {
            packet.ReadInt32("TextEmote");
            packet.ReadInt32("EmoteNum");
            packet.ReadPackedGuid("Guid");
        }

        [Parser(Opcode.SMSG_TEXT_EMOTE)]
        public static void HandleTextEmoteServer(Packet packet)
        {
            packet.AsHex();
            packet.ReadToEnd();
        }

    }
}
