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
            var prefixLen = packet.ReadBits(5);
            var testLen = packet.ReadBits(8);

            packet.ReadWoWString("Prefix", prefixLen);
            packet.ReadWoWString("Text", testLen);
        }

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_WHISPER)]
        public static void HandleAddonMessageWhisper(Packet packet)
        {
            var targetLen = packet.ReadBits(9);
            var prefixLen = packet.ReadBits(5);
            var testLen = packet.ReadBits(8);

            packet.ReadWoWString("Target", targetLen);
            packet.ReadWoWString("Prefix", prefixLen);
            packet.ReadWoWString("Text", testLen);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_GUILD)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_YELL)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_SAY)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_PARTY)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_INSTANCE_CHAT)]
        public static void HandleClientChatMessage(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Text", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_CHANNEL)]
        public static void HandleClientChatMessageChannel434(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var channelNameLen = packet.ReadBits(9);
            var msgLen = packet.ReadBits(8);
            packet.ResetBitReader();
            packet.ReadWoWString("Channel Name", channelNameLen);
            packet.ReadWoWString("Message", msgLen);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_WHISPER)]
        public static void HandleClientChatMessageWhisper(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var recvName = packet.ReadBits(9);
            var msgLen = packet.ReadBits(8);

            packet.ReadWoWString("Target", recvName);
            packet.ReadWoWString("Text", msgLen);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_DND)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_EMOTE)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_AFK)]
        public static void HandleMessageChatDND(Packet packet)
        {
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.SMSG_CHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            var text = new CreatureText
            {
                Type = (ChatMessageType)packet.ReadByteE<ChatMessageTypeNew>("SlashCmd"),
                Language = packet.ReadByteE<Language>("Language"),
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
            packet.ReadBits("ChatFlags", ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_2_19802) ? 11 : 10);

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

        [Parser(Opcode.SMSG_CHAT_SERVER_MESSAGE)]
        public static void HandleServerMessage(Packet packet)
        {
            packet.ReadInt32("MessageID");

            packet.ResetBitReader();

            var bits20 = packet.ReadBits(11);
            packet.ReadWoWString("StringParam", bits20);
        }

        [Parser(Opcode.CMSG_EMOTE)]
        public static void HandleEmoteClient(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_EMOTE)]
        public static void HandleEmote(Packet packet)
        {
            var guid = packet.ReadPackedGuid128("GUID");
            var emote = packet.ReadInt32E<EmoteType>("Emote ID");

            if (guid.GetObjectType() == ObjectType.Unit)
                Storage.Emotes.Add(guid, emote, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_SEND_TEXT_EMOTE, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_0_3_19103)]
        public static void HandleTextEmote602(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");

            packet.ReadInt32E<EmoteType>("Emote ID");
            packet.ReadInt32E<EmoteTextType>("Text Emote ID");
        }

        [Parser(Opcode.CMSG_SEND_TEXT_EMOTE, ClientVersionBuild.V6_0_3_19103)]
        public static void HandleTextEmote603(Packet packet)
        {
            packet.ReadPackedGuid128("TargetGUID");
            packet.ReadInt32E<EmoteTextType>("Emote ID");
            packet.ReadInt32("SoundIndex");
        }

        [Parser(Opcode.SMSG_TEXT_EMOTE)]
        public static void HandleTextEmoteServer(Packet packet)
        {
            packet.ReadPackedGuid128("SourceGUID");
            packet.ReadPackedGuid128("WowAccountGUID");
            packet.ReadInt32E<EmoteTextType>("EmoteID");
            packet.ReadInt32("SoundIndex");
            packet.ReadPackedGuid128("TargetGUID");
        }

        [Parser(Opcode.SMSG_DEFENSE_MESSAGE)]
        public static void HandleDefenseMessage(Packet packet)
        {
            packet.ReadInt32<ZoneId>("ZoneID");
            var len = packet.ReadBits(12);
            packet.ReadWoWString("MessageText", len);
        }

        [Parser(Opcode.SMSG_CHAT_PLAYER_NOTFOUND)]
        public static void HandleChatPlayerNotFound(Packet packet)
        {
            var bits16 = packet.ReadBits(9);
            packet.ReadWoWString("Name", bits16);
        }
    }
}
