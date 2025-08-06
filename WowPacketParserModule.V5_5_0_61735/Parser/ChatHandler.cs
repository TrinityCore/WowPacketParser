using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.SMSG_CHAT)]
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
            var chatFlags = packet.ReadUInt16("ChatFlags");

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

        [Parser(Opcode.SMSG_CHAT_PLAYER_AMBIGUOUS)]
        public static void HandleChatPlayerAmbiguous(Packet packet)
        {
            packet.ResetBitReader();
            var len = packet.ReadBits(9);
            packet.ReadWoWString("Name", len);
        }

        [Parser(Opcode.SMSG_CHAT_NOT_IN_PARTY)]
        public static void HandleChatNotInParty(Packet packet)
        {
            packet.ReadInt32E<ChatMessageTypeNew>("MessageType");
        }

        [Parser(Opcode.SMSG_CHAT_RESTRICTED)]
        public static void HandleChatRestricted(Packet packet)
        {
            packet.ReadInt32E<ChatRestrictionType>("Restriction");
        }

        [Parser(Opcode.SMSG_DEFENSE_MESSAGE)]
        public static void HandleDefenseMessage(Packet packet)
        {
            packet.ReadInt32<ZoneId>("ZoneID");
            packet.ResetBitReader();
            var len = packet.ReadBits(12);
            packet.ReadWoWString("MessageText", len);
        }

        [Parser(Opcode.SMSG_CHAT_PLAYER_NOTFOUND)]
        public static void HandleChatPlayerNotFound(Packet packet)
        {
            packet.ResetBitReader();
            var len = packet.ReadBits(9);
            packet.ReadWoWString("Name", len);
        }

        [Parser(Opcode.SMSG_CHAT_SERVER_MESSAGE)]
        public static void HandleServerMessage(Packet packet)
        {
            packet.ReadInt32("MessageID");

            packet.ResetBitReader();
            var len = packet.ReadBits(11);
            packet.ReadWoWString("StringParam", len);
        }

        [Parser(Opcode.SMSG_CHAT_CAN_LOCAL_WHISPER_TARGET_RESPONSE)]
        public static void HandleChatCanLocalWhisperTargetResponse(Packet packet)
        {
            packet.ReadPackedGuid128("WhisperTarget");
            packet.ReadInt32E<ChatWhisperTargetStatus>("Status");
        }

        [Parser(Opcode.SMSG_PRINT_NOTIFICATION)]
        public static void HandlePrintNotification(Packet packet)
        {
            var notifyTextLen = packet.ReadBits(12);

            packet.ReadWoWString("NotifyText", notifyTextLen);
        }

        [Parser(Opcode.SMSG_TEXT_EMOTE)]
        public static void HandleTextEmoteServer(Packet packet)
        {
            packet.ReadPackedGuid128("SourceGUID");
            packet.ReadPackedGuid128("SourceAccountGUID");
            packet.ReadInt32E<EmoteTextType>("EmoteID");
            packet.ReadInt32("SoundIndex");
            packet.ReadPackedGuid128("TargetGUID");
        }
    }
}
