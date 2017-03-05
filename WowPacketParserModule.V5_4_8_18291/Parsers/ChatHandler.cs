using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.CMSG_CHAT_MESSAGE_GUILD)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_YELL)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_SAY)]
        public static void HandleClientChatMessage(Packet packet)
        {
            packet.Translator.ReadInt32E<Language>("Language");
            var len = packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_WHISPER)]
        public static void HandleClientChatMessageWhisper(Packet packet)
        {
            packet.Translator.ReadInt32E<Language>("Language");
            var msgLen = packet.Translator.ReadBits(8);
            var recvName = packet.Translator.ReadBits(9);
            packet.Translator.ReadWoWString("Message", msgLen);
            packet.Translator.ReadWoWString("Receivers Name", recvName);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_CHANNEL)]
        public static void HandleClientChatMessageChannel434(Packet packet)
        {
            packet.Translator.ReadInt32E<Language>("Language");
            var channelNameLen = packet.Translator.ReadBits(9);
            var msgLen = packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("Message", msgLen);
            packet.Translator.ReadWoWString("Channel Name", channelNameLen);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_DND)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_EMOTE)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_AFK)]
        public static void HandleMessageChatDND(Packet packet)
        {
            var len = packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_SEND_TEXT_EMOTE)]
        public static void HandleTextEmote(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadInt32E<EmoteTextType>("Text Emote ID");
            packet.Translator.ReadInt32E<EmoteType>("Emote ID");

            packet.Translator.StartBitStream(guid, 6, 7, 3, 2, 0, 5, 1, 4);
            packet.Translator.ParseBitStream(guid, 0, 5, 1, 4, 2, 3, 7, 6);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_TEXT_EMOTE)]
        public static void HandleTextEmoteServer(Packet packet)
        {
            var guid = new byte[8];
            var targetGuid = new byte[8];

            guid[1] = packet.Translator.ReadBit();
            targetGuid[7] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            targetGuid[5] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            targetGuid[6] = packet.Translator.ReadBit();
            targetGuid[2] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            targetGuid[0] = packet.Translator.ReadBit();
            targetGuid[1] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            targetGuid[3] = packet.Translator.ReadBit();
            targetGuid[4] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(targetGuid, 2);
            packet.Translator.ReadXORByte(targetGuid, 1);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(targetGuid, 7);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadInt32E<EmoteTextType>("Text Emote ID");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(targetGuid, 0);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(targetGuid, 6);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(targetGuid, 3);
            packet.Translator.ReadXORByte(targetGuid, 5);
            packet.Translator.ReadXORByte(targetGuid, 4);
            packet.Translator.ReadInt32E<EmoteType>("Emote ID");

            packet.Translator.WriteGuid("Guid", guid);
            packet.Translator.WriteGuid("TargetGuid", targetGuid);
        }

        [Parser(Opcode.SMSG_CHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            var text = new CreatureText();

            var senderGUIDBytes = new byte[8];
            var guildGUIDBytes = new byte[8];
            var receiverGUIDBytes = new byte[8];
            var groupGUIDBytes = new byte[8];

            var hasSender = !packet.Translator.ReadBit();

            packet.Translator.ReadBit("Show only in bubble"); // 0 Show in chat log, 1 for showing only in bubble

            var senderNameLen = 0u;
            if (hasSender)
                senderNameLen = packet.Translator.ReadBits(11);

            packet.Translator.ReadBit(); // fake bit
            var hasChannel = !packet.Translator.ReadBit();
            var bit1499 = packet.Translator.ReadBit();
            var bit1494 = !packet.Translator.ReadBit();
            var bit1490 = !packet.Translator.ReadBit();
            var hasRealmId1 = !packet.Translator.ReadBit();
            packet.Translator.StartBitStream(groupGUIDBytes, 0, 1, 5, 4, 3, 2, 6, 7);
            var bits1490 = 0u;
            if (bit1490)
                bits1490 = packet.Translator.ReadBits(9);
            packet.Translator.ReadBit(); // fake bit
            packet.Translator.StartBitStream(receiverGUIDBytes, 7, 6, 1, 4, 0, 2, 3, 5);
            packet.Translator.ReadBit(); // fake bit
            var hasLang = !packet.Translator.ReadBit();
            var hasPrefix = !packet.Translator.ReadBit();
            packet.Translator.StartBitStream(senderGUIDBytes, 0, 3, 7, 2, 1, 5, 4, 6);
            var hasAchi = !packet.Translator.ReadBit();
            var hasText = !packet.Translator.ReadBit();
            var channelLen = 0u;
            if (hasChannel)
                channelLen = packet.Translator.ReadBits(7);
            var textLen = 0u;
            if (hasText)
                textLen = packet.Translator.ReadBits(12);
            var hasReceiver = !packet.Translator.ReadBit();
            var prefixLen = 0u;
            if (hasPrefix)
                prefixLen = packet.Translator.ReadBits(5);
            var hasRealmId2 = !packet.Translator.ReadBit();
            var receiverLen = 0u;
            if (hasReceiver)
                receiverLen = packet.Translator.ReadBits(11);
            packet.Translator.ReadBit(); // fake bit ??????
            packet.Translator.StartBitStream(guildGUIDBytes, 2, 5, 7, 4, 0, 1, 3, 6);

            packet.Translator.ParseBitStream(guildGUIDBytes, 4, 5, 7, 3, 2, 6, 0, 1);
            if (hasChannel)
                packet.Translator.ReadWoWString("Channel Name", channelLen);
            if (hasPrefix)
                packet.Translator.ReadWoWString("Addon Message Prefix", prefixLen);

            if (bit1494)
                packet.Translator.ReadSingle("Float1494");

            packet.Translator.ParseBitStream(senderGUIDBytes, 4, 7, 1, 5, 0, 6, 2, 3);

            text.Type = (ChatMessageType)packet.Translator.ReadByteE<ChatMessageTypeNew>("SlashCmd");
            if (hasAchi)
                packet.Translator.ReadInt32<AchievementId>("Achievement Id");
            packet.Translator.ParseBitStream(groupGUIDBytes, 1, 3, 4, 6, 0, 2, 5, 7);
            packet.Translator.ParseBitStream(receiverGUIDBytes, 2, 5, 3, 6, 7, 4, 1, 0);
            if (hasLang)
                text.Language = packet.Translator.ReadByteE<Language>("Language");
            if (hasRealmId2)
                packet.Translator.ReadInt32("Realm Id 2");
            if (hasText)
                text.Text = packet.Translator.ReadWoWString("Text", textLen);
            if (hasReceiver)
                text.ReceiverName = packet.Translator.ReadWoWString("Receiver Name", receiverLen);
            if (hasSender)
                text.SenderName = packet.Translator.ReadWoWString("Sender Name", senderNameLen);
            if (hasRealmId1)
                packet.Translator.ReadInt32("Realm Id");

            text.SenderGUID = packet.Translator.WriteGuid("SenderGUID", senderGUIDBytes);
            text.ReceiverGUID = packet.Translator.WriteGuid("ReceiverGUID", receiverGUIDBytes);
            packet.Translator.WriteGuid("GuildGUID", guildGUIDBytes);
            packet.Translator.WriteGuid("GroupGUID", groupGUIDBytes);

            uint entry = 0;
            if (text.SenderGUID.GetObjectType() == ObjectType.Unit)
                entry = text.SenderGUID.GetEntry();
            else if (text.ReceiverGUID.GetObjectType() == ObjectType.Unit)
                entry = text.ReceiverGUID.GetEntry();

            if (entry != 0)
                Storage.CreatureTexts.Add(entry, text, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_CHAT_PLAYER_NOTFOUND)]
        public static void HandleChatPlayerNotFound(Packet packet)
        {
            var len = packet.Translator.ReadBits(9);
            packet.Translator.ReadWoWString("Name", len);
        }

        [Parser(Opcode.SMSG_DEFENSE_MESSAGE)]
        public static void HandleDefenseMessage(Packet packet)
        {
            packet.Translator.ReadUInt32<ZoneId>("Zone Id");
            uint length = packet.Translator.ReadBits("Message Length", 12);
            packet.Translator.ReadWoWString("Message", length);
        }

        [Parser(Opcode.SMSG_NOTIFICATION)]
        public static void HandleNotification(Packet packet)
        {
            var length = packet.Translator.ReadBits(12);
            packet.Translator.ReadWoWString("Message", length);
        }
    }
}
