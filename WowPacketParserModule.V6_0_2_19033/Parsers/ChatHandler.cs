using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_GUILD)]
        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_OFFICER)]
        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_PARTY)]
        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_RAID)]
        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_INSTANCE_CHAT)]
        public static void HandleAddonMessage(Packet packet)
        {
            var prefixLen = packet.Translator.ReadBits(5);
            var testLen = packet.Translator.ReadBits(8);

            packet.Translator.ReadWoWString("Prefix", prefixLen);
            packet.Translator.ReadWoWString("Text", testLen);
        }

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_WHISPER)]
        public static void HandleAddonMessageWhisper(Packet packet)
        {
            var targetLen = packet.Translator.ReadBits(9);
            var prefixLen = packet.Translator.ReadBits(5);
            var testLen = packet.Translator.ReadBits(8);

            packet.Translator.ReadWoWString("Target", targetLen);
            packet.Translator.ReadWoWString("Prefix", prefixLen);
            packet.Translator.ReadWoWString("Text", testLen);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_GUILD)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_YELL)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_SAY)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_PARTY)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_INSTANCE_CHAT)]
        public static void HandleClientChatMessage(Packet packet)
        {
            packet.Translator.ReadInt32E<Language>("Language");
            var len = packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("Text", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_CHANNEL)]
        public static void HandleClientChatMessageChannel434(Packet packet)
        {
            packet.Translator.ReadInt32E<Language>("Language");
            var channelNameLen = packet.Translator.ReadBits(9);
            var msgLen = packet.Translator.ReadBits(8);
            packet.Translator.ResetBitReader();
            packet.Translator.ReadWoWString("Channel Name", channelNameLen);
            packet.Translator.ReadWoWString("Message", msgLen);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_WHISPER)]
        public static void HandleClientChatMessageWhisper(Packet packet)
        {
            packet.Translator.ReadInt32E<Language>("Language");
            var recvName = packet.Translator.ReadBits(9);
            var msgLen = packet.Translator.ReadBits(8);

            packet.Translator.ReadWoWString("Target", recvName);
            packet.Translator.ReadWoWString("Text", msgLen);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_DND)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_EMOTE)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_AFK)]
        public static void HandleMessageChatDND(Packet packet)
        {
            var len = packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("Message", len);
        }

        [Parser(Opcode.SMSG_CHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            var text = new CreatureText
            {
                Type = (ChatMessageType)packet.Translator.ReadByteE<ChatMessageTypeNew>("SlashCmd"),
                Language = packet.Translator.ReadByteE<Language>("Language"),
                SenderGUID = packet.Translator.ReadPackedGuid128("SenderGUID")
            };

            packet.Translator.ReadPackedGuid128("SenderGuildGUID");
            packet.Translator.ReadPackedGuid128("WowAccountGUID");
            text.ReceiverGUID = packet.Translator.ReadPackedGuid128("TargetGUID");
            packet.Translator.ReadUInt32("TargetVirtualAddress");
            packet.Translator.ReadUInt32("SenderVirtualAddress");
            packet.Translator.ReadPackedGuid128("PartyGUID");
            packet.Translator.ReadUInt32("AchievementID");
            packet.Translator.ReadSingle("DisplayTime");

            var bits24 = packet.Translator.ReadBits(11);
            var bits1121 = packet.Translator.ReadBits(11);
            var prefixLen = packet.Translator.ReadBits(5);
            var channelLen = packet.Translator.ReadBits(7);
            var textLen = packet.Translator.ReadBits(12);
            packet.Translator.ReadBits("ChatFlags", ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_2_19802) ? 11 : 10);

            packet.Translator.ReadBit("HideChatLog");
            packet.Translator.ReadBit("FakeSenderName");

            text.SenderName = packet.Translator.ReadWoWString("Sender Name", bits24);
            text.ReceiverName = packet.Translator.ReadWoWString("Receiver Name", bits1121);
            packet.Translator.ReadWoWString("Addon Message Prefix", prefixLen);
            packet.Translator.ReadWoWString("Channel Name", channelLen);

            text.Text = packet.Translator.ReadWoWString("Text", textLen);

            uint entry = 0;
            if (text.SenderGUID.GetObjectType() == ObjectType.Unit)
                entry = text.SenderGUID.GetEntry();
            else if (text.ReceiverGUID.GetObjectType() == ObjectType.Unit)
                entry = text.ReceiverGUID.GetEntry();

            if (entry != 0)
                Storage.CreatureTexts.Add(entry, text, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_CHAT_SERVER_MESSAGE)]
        public static void HandleServerMessage(Packet packet)
        {
            packet.Translator.ReadInt32("MessageID");

            packet.Translator.ResetBitReader();

            var bits20 = packet.Translator.ReadBits(11);
            packet.Translator.ReadWoWString("StringParam", bits20);
        }

        [Parser(Opcode.CMSG_EMOTE)]
        public static void HandleEmoteClient(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_EMOTE)]
        public static void HandleEmote(Packet packet)
        {
            var guid = packet.Translator.ReadPackedGuid128("GUID");
            var emote = packet.Translator.ReadInt32E<EmoteType>("Emote ID");

            if (guid.GetObjectType() == ObjectType.Unit)
                Storage.Emotes.Add(guid, emote, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_SEND_TEXT_EMOTE, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_0_3_19103)]
        public static void HandleTextEmote602(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");

            packet.Translator.ReadInt32E<EmoteType>("Emote ID");
            packet.Translator.ReadInt32E<EmoteTextType>("Text Emote ID");
        }

        [Parser(Opcode.CMSG_SEND_TEXT_EMOTE, ClientVersionBuild.V6_0_3_19103)]
        public static void HandleTextEmote603(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("TargetGUID");
            packet.Translator.ReadInt32E<EmoteTextType>("Emote ID");
            packet.Translator.ReadInt32("SoundIndex");
        }

        [Parser(Opcode.SMSG_TEXT_EMOTE)]
        public static void HandleTextEmoteServer(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("SourceGUID");
            packet.Translator.ReadPackedGuid128("WowAccountGUID");
            packet.Translator.ReadInt32E<EmoteTextType>("EmoteID");
            packet.Translator.ReadInt32("SoundIndex");
            packet.Translator.ReadPackedGuid128("TargetGUID");
        }

        [Parser(Opcode.SMSG_DEFENSE_MESSAGE)]
        public static void HandleDefenseMessage(Packet packet)
        {
            packet.Translator.ReadInt32<ZoneId>("ZoneID");
            var len = packet.Translator.ReadBits(12);
            packet.Translator.ReadWoWString("MessageText", len);
        }

        [Parser(Opcode.SMSG_CHAT_PLAYER_NOTFOUND)]
        public static void HandleChatPlayerNotFound(Packet packet)
        {
            var bits16 = packet.Translator.ReadBits(9);
            packet.Translator.ReadWoWString("Name", bits16);
        }

        [Parser(Opcode.SMSG_PRINT_NOTIFICATION)]
        public static void HandlePrintNotification(Packet packet)
        {
            var notifyTextLen = packet.ReadBits(12);

            packet.ReadWoWString("NotifyText", notifyTextLen);
        }
    }
}
