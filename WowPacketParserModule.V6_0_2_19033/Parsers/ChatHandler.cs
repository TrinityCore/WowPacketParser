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
        [Parser(Opcode.CMSG_MESSAGECHAT_ADDON_GUILD)]
        [Parser(Opcode.CMSG_MESSAGECHAT_ADDON_OFFICER)]
        [Parser(Opcode.CMSG_MESSAGECHAT_ADDON_PARTY)]
        [Parser(Opcode.CMSG_MESSAGECHAT_ADDON_RAID)]
        public static void HandleAddonMessage(Packet packet)
        {
            var prefixLen = packet.ReadBits(5);
            var testLen = packet.ReadBits(8);

            packet.ReadWoWString("Prefix", prefixLen);
            packet.ReadWoWString("Text", testLen);
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_ADDON_WHISPER)]
        public static void HandleAddonMessageWhisper(Packet packet)
        {
            var targetLen = packet.ReadBits(9);
            var prefixLen = packet.ReadBits(5);
            var testLen = packet.ReadBits(8);

            packet.ReadWoWString("Target", targetLen);
            packet.ReadWoWString("Prefix", prefixLen);
            packet.ReadWoWString("Text", testLen);
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_GUILD)]
        [Parser(Opcode.CMSG_MESSAGECHAT_YELL)]
        [Parser(Opcode.CMSG_MESSAGECHAT_SAY)]
        [Parser(Opcode.CMSG_MESSAGECHAT_PARTY)]
        [Parser(Opcode.CMSG_MESSAGECHAT_INSTANCE)]
        public static void HandleClientChatMessage(Packet packet)
        {
            packet.ReadEnum<Language>("Language", TypeCode.Int32);
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Text", len);
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_CHANNEL)]
        public static void HandleClientChatMessageChannel434(Packet packet)
        {
            packet.ReadEnum<Language>("Language", TypeCode.Int32);
            var channelNameLen = packet.ReadBits(9);
            var msgLen = packet.ReadBits(8);
            packet.ResetBitReader();
            packet.ReadWoWString("Channel Name", channelNameLen);
            packet.ReadWoWString("Message", msgLen);
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_WHISPER)]
        public static void HandleClientChatMessageWhisper(Packet packet)
        {
            packet.ReadEnum<Language>("Language", TypeCode.Int32);
            var recvName = packet.ReadBits(9);
            var msgLen = packet.ReadBits(8);

            packet.ReadWoWString("Target", recvName);
            packet.ReadWoWString("Text", msgLen);
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_DND)]
        [Parser(Opcode.CMSG_MESSAGECHAT_EMOTE)]
        [Parser(Opcode.CMSG_MESSAGECHAT_AFK)]
        public static void HandleMessageChatDND(Packet packet)
        {
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.SMSG_MESSAGECHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            var text = new CreatureText
            {
                Type = (ChatMessageType) packet.ReadByte("Chat type"),
                Language = packet.ReadEnum<Language>("Language", TypeCode.Byte),
                SenderGUID = packet.ReadPackedGuid128("SenderGUID")
            };

            packet.ReadPackedGuid128("SenderGuildGUID");
            packet.ReadPackedGuid128("WowAccountGUID");
            text.ReceiverGUID = packet.ReadPackedGuid128("TargetGUID");
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
            packet.ReadBits("ChatFlags", 10);

            packet.ReadBit("HideChatLog");
            packet.ReadBit("FakeSenderName");

            text.SenderName = packet.ReadWoWString("Sender Name", bits24);
            text.ReceiverName = packet.ReadWoWString("Receiver Name", bits1121);
            packet.ReadWoWString("Addon Message Prefix", prefixLen);
            packet.ReadWoWString("Channel Name", channelLen);

            text.Text = packet.ReadWoWString("Text", textLen);

            uint entry = 0;
            if (text.SenderGUID.GetObjectType() == ObjectType.Unit)
                entry = text.SenderGUID.GetEntry();
            else if (text.ReceiverGUID.GetObjectType() == ObjectType.Unit)
                entry = text.ReceiverGUID.GetEntry();

            if (entry != 0)
                Storage.CreatureTexts.Add(entry, text, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_SERVER_MESSAGE)]
        public static void HandleServerMessage(Packet packet)
        {
            packet.ReadInt32("MessageID");

            packet.ResetBitReader();

            var bits20 = packet.ReadBits(11);
            packet.ReadWoWString("StringParam", bits20);
        }

        [Parser(Opcode.SMSG_EMOTE)]
        public static void HandleEmote(Packet packet)
        {
            var guid = packet.ReadPackedGuid128("GUID");
            var emote = packet.ReadEnum<EmoteType>("Emote ID", TypeCode.Int32);

            if (guid.GetObjectType() == ObjectType.Unit)
                Storage.Emotes.Add(guid, emote, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_TEXT_EMOTE)]
        public static void HandleTextEmote(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");

            packet.ReadEnum<EmoteType>("Emote ID", TypeCode.Int32);
            packet.ReadEnum<EmoteTextType>("Text Emote ID", TypeCode.Int32);
        }

        [Parser(Opcode.SMSG_TEXT_EMOTE)]
        public static void HandleTextEmoteServer(Packet packet)
        {
            packet.ReadPackedGuid128("SourceGUID");
            packet.ReadPackedGuid128("WowAccountGUID");
            packet.ReadEnum<EmoteType>("EmoteID", TypeCode.Int32);
            packet.ReadEnum<EmoteTextType>("SoundIndex", TypeCode.Int32);
            packet.ReadPackedGuid128("TargetGUID");
        }

        [Parser(Opcode.SMSG_DEFENSE_MESSAGE)]
        public static void HandleDefenseMessage(Packet packet)
        {
            packet.ReadEntry<Int32>(StoreNameType.Zone, "ZoneID");
            var len = packet.ReadBits(12);
            packet.ReadWoWString("MessageText", len);
        }
    }
}
