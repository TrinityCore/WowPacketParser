using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class ChatHandler
    {
        public static void ReadChatAddonMessageParams(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();
            var prefixLen = packet.ReadBits(5);
            var textLen = packet.ReadBits(8);
            packet.ReadBit("IsLogged", indexes);

            packet.ReadInt32("Type", indexes);
            packet.ReadWoWString("Prefix", prefixLen, indexes);
            packet.ReadWoWString("Text", textLen, indexes);
        }

        [Parser(Opcode.SMSG_CHAT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleServerChatMessage(Packet packet)
        {
            PacketChat chatPacket = packet.Holder.Chat = new PacketChat();
            var text = new CreatureText
            {
                Type = (ChatMessageType)packet.ReadByteE<ChatMessageTypeNew>("SlashCmd"),
                Language = packet.ReadUInt32E<Language>("Language"),
                SenderGUID = packet.ReadPackedGuid128("SenderGUID")
            };

            packet.ReadPackedGuid128("SenderGuildGUID");
            packet.ReadPackedGuid128("WowAccountGUID");
            text.ReceiverGUID = packet.ReadPackedGuid128("TargetGUID");
            packet.ReadUInt32("TargetVirtualAddress");
            packet.ReadUInt32("SenderVirtualAddress");
            packet.ReadInt32("AchievementID");

            uint chatFlags = packet.ReadUInt16("ChatFlags");

            packet.ReadSingle("DisplayTime");
            packet.ReadInt32<SpellId>("SpellID");

            var senderNameLen = packet.ReadBits(11);
            var receiverNameLen = packet.ReadBits(11);
            var prefixLen = packet.ReadBits(5);
            var channelLen = packet.ReadBits(7);
            var textLen = packet.ReadBits(12);

            packet.ReadBit("HideChatLog");
            packet.ReadBit("FakeSenderName");
            bool unk801bit = packet.ReadBit("Unk801_Bit");
            bool hasChannelGuid = packet.ReadBit("HasChannelGUID");

            text.SenderName = packet.ReadWoWString("Sender Name", senderNameLen);
            text.ReceiverName = packet.ReadWoWString("Receiver Name", receiverNameLen);
            packet.ReadWoWString("Addon Message Prefix", prefixLen);
            packet.ReadWoWString("Channel Name", channelLen);

            chatPacket.Text = text.Text = packet.ReadWoWString("Text", textLen);
            chatPacket.Sender = text.SenderGUID.ToUniversalGuid();
            chatPacket.Target = text.ReceiverGUID.ToUniversalGuid();
            chatPacket.Language = (int) text.Language;
            chatPacket.Type = (int) text.Type;
            chatPacket.Flags = chatFlags;

            if (unk801bit)
                packet.ReadUInt32("Unk801");

            if (hasChannelGuid)
                packet.ReadPackedGuid128("ChannelGUID");

            uint entry = 0;
            if (text.SenderGUID.GetObjectType() == ObjectType.Unit)
                entry = text.SenderGUID.GetEntry();
            else if (text.ReceiverGUID.GetObjectType() == ObjectType.Unit)
                entry = text.ReceiverGUID.GetEntry();

            if (entry != 0)
                Storage.CreatureTexts.Add(entry, text, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_DND, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_EMOTE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_AFK, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleMessageChat(Packet packet)
        {
            var len = packet.ReadBits(11);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_CHANNEL, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleChatAddonMessageChannel(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            packet.ReadPackedGuid128("ChannelGUID");
            var channelNameLen = packet.ReadBits(9);
            var msgLen = packet.ReadBits(11);

            packet.ReadWoWString("Target", channelNameLen);
            packet.ReadWoWString("Text", msgLen);
        }

        [Parser(Opcode.SMSG_EMOTE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleEmote(Packet packet)
        {
            PacketEmote packetEmote = packet.Holder.Emote = new PacketEmote();
            var guid = packet.ReadPackedGuid128("GUID");
            var emote = packet.ReadInt32E<EmoteType>("Emote ID");
            var count = packet.ReadUInt32("SpellVisualKitCount");
            packet.ReadInt32("SequenceVariation");

            for (var i = 0; i < count; ++i)
                packet.ReadUInt32("SpellVisualKitID", i);

            if (guid.GetObjectType() == ObjectType.Unit)
                Storage.Emotes.Add(guid, emote, packet.TimeSpan);

            packetEmote.Emote = (int)emote;
            packetEmote.Sender = guid.ToUniversalGuid();
        }

        [Parser(Opcode.SMSG_CHAT_PLAYER_AMBIGUOUS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleChatPlayerAmbiguous(Packet packet)
        {
            packet.ResetBitReader();
            var len = packet.ReadBits(9);
            packet.ReadWoWString("Name", len);
        }

        [Parser(Opcode.SMSG_CHAT_PLAYER_NOTFOUND, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleChatPlayerNotFound(Packet packet)
        {
            packet.ResetBitReader();
            var len = packet.ReadBits(9);
            packet.ReadWoWString("Name", len);
        }

        [Parser(Opcode.SMSG_CHAT_RESTRICTED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleChatRestricted(Packet packet)
        {
            packet.ReadInt32E<ChatRestrictionType>("Restriction");
        }

        [Parser(Opcode.SMSG_CHAT_SERVER_MESSAGE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleServerMessage(Packet packet)
        {
            packet.ReadInt32("MessageID");

            packet.ResetBitReader();
            var len = packet.ReadBits(11);
            packet.ReadWoWString("StringParam", len);
        }

        [Parser(Opcode.SMSG_DEFENSE_MESSAGE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleDefenseMessage(Packet packet)
        {
            packet.ReadInt32<ZoneId>("ZoneID");
            packet.ResetBitReader();
            var len = packet.ReadBits(12);
            packet.ReadWoWString("MessageText", len);
        }

        [Parser(Opcode.SMSG_PRINT_NOTIFICATION, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePrintNotification(Packet packet)
        {
            var notifyTextLen = packet.ReadBits(12);

            packet.ReadWoWString("NotifyText", notifyTextLen);
        }

        [Parser(Opcode.SMSG_TEXT_EMOTE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleTextEmoteServer(Packet packet)
        {
            packet.ReadPackedGuid128("SourceGUID");
            packet.ReadPackedGuid128("SourceAccountGUID");
            packet.ReadInt32E<EmoteTextType>("EmoteID");
            packet.ReadInt32("SoundIndex");
            packet.ReadPackedGuid128("TargetGUID");
        }

        [Parser(Opcode.SMSG_UPDATE_AADC_STATUS_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleUpdateAadcStatusResponse(Packet packet)
        {
            packet.ResetBitReader();
            packet.ReadBit("Success");
            packet.ReadBit("ChatDisabled");
        }

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAddonMessage(Packet packet)
        {
            ReadChatAddonMessageParams(packet);
        }

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_TARGETED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleChatAddonMessageTargeted(Packet packet)
        {
            ReadChatAddonMessageParams(packet, "Params");

            packet.ReadPackedGuid128("ChannelGUID");
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadUInt32("PlayerVirtualRealmAddress");

            var playerNameLen = packet.ReadBits(7);
            var channelNameLen = packet.ReadBits(7);

            if (playerNameLen > 1)
                packet.ReadDynamicString("PlayerName", playerNameLen);

            if (channelNameLen > 1)
                packet.ReadDynamicString("ChannelName", channelNameLen);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_GUILD, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_OFFICER, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_YELL, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleClientChatMessage(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var len = packet.ReadBits(11);
            packet.ReadWoWString("Text", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_INSTANCE_CHAT, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_PARTY, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_RAID, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_RAID_WARNING, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_SAY, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleClientChatMessageInstance(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var len = packet.ReadBits(11);
            packet.ReadBit("IsSecure");
            packet.ReadWoWString("Text", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_WHISPER, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleClientChatMessageWhisper(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            packet.ReadPackedGuid128("TargetGUID");
            packet.ReadUInt32("TargetVirtualRealmAddress");

            var targetLen = packet.ReadBits(7);
            var textLen = packet.ReadBits(11);

            if (targetLen > 1)
                packet.ReadDynamicString("Target", targetLen);

            if (textLen > 1)
                packet.ReadDynamicString("Text", textLen);
        }

        [Parser(Opcode.CMSG_CHAT_REPORT_IGNORED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleChatIgnored(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            packet.ReadByte("Reason");
        }

        [Parser(Opcode.CMSG_SEND_TEXT_EMOTE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSendTextEmote(Packet packet)
        {
            packet.ReadPackedGuid128("Target");
            packet.ReadInt32E<EmoteTextType>("EmoteID");
            packet.ReadInt32("SoundIndex");
            var count = packet.ReadUInt32("SpellVisualKitCount");
            packet.ReadInt32("SequenceVariation");

            for (var i = 0; i < count; ++i)
                packet.ReadInt32("SpellVisualKitID", i);
        }

        [Parser(Opcode.CMSG_UPDATE_AADC_STATUS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleUpdateAADCStatus(Packet packet)
        {
            packet.ReadBit("ChatDisabled");
        }

        [Parser(Opcode.CMSG_EMOTE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleChatNull(Packet packet)
        {
        }
    }
}
