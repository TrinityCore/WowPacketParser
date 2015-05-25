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
            packet.ReadInt32E<Language>("Language");
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_WHISPER)]
        public static void HandleClientChatMessageWhisper(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var msgLen = packet.ReadBits(8);
            var recvName = packet.ReadBits(9);
            packet.ReadWoWString("Message", msgLen);
            packet.ReadWoWString("Receivers Name", recvName);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_CHANNEL)]
        public static void HandleClientChatMessageChannel434(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var channelNameLen = packet.ReadBits(9);
            var msgLen = packet.ReadBits(8);
            packet.ReadWoWString("Message", msgLen);
            packet.ReadWoWString("Channel Name", channelNameLen);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_DND)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_EMOTE)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_AFK)]
        public static void HandleMessageChatDND(Packet packet)
        {
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_SEND_TEXT_EMOTE)]
        public static void HandleTextEmote(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32E<EmoteTextType>("Text Emote ID");
            packet.ReadInt32E<EmoteType>("Emote ID");

            packet.StartBitStream(guid, 6, 7, 3, 2, 0, 5, 1, 4);
            packet.ParseBitStream(guid, 0, 5, 1, 4, 2, 3, 7, 6);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_TEXT_EMOTE)]
        public static void HandleTextEmoteServer(Packet packet)
        {
            var guid = new byte[8];
            var targetGuid = new byte[8];

            guid[1] = packet.ReadBit();
            targetGuid[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            targetGuid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            targetGuid[6] = packet.ReadBit();
            targetGuid[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            targetGuid[0] = packet.ReadBit();
            targetGuid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            targetGuid[3] = packet.ReadBit();
            targetGuid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[5] = packet.ReadBit();

            packet.ReadXORByte(targetGuid, 2);
            packet.ReadXORByte(targetGuid, 1);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(targetGuid, 7);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);
            packet.ReadInt32E<EmoteTextType>("Text Emote ID");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(targetGuid, 0);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(targetGuid, 6);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(targetGuid, 3);
            packet.ReadXORByte(targetGuid, 5);
            packet.ReadXORByte(targetGuid, 4);
            packet.ReadInt32E<EmoteType>("Emote ID");

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("TargetGuid", targetGuid);
        }

        [Parser(Opcode.SMSG_CHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            var text = new CreatureText();

            var senderGUIDBytes = new byte[8];
            var guildGUIDBytes = new byte[8];
            var receiverGUIDBytes = new byte[8];
            var groupGUIDBytes = new byte[8];

            var hasSender = !packet.ReadBit();

            packet.ReadBit("Show only in bubble"); // 0 Show in chat log, 1 for showing only in bubble

            var senderNameLen = 0u;
            if (hasSender)
                senderNameLen = packet.ReadBits(11);

            packet.ReadBit(); // fake bit
            var hasChannel = !packet.ReadBit();
            var bit1499 = packet.ReadBit();
            var bit1494 = !packet.ReadBit();
            var bit1490 = !packet.ReadBit();
            var hasRealmId1 = !packet.ReadBit();
            packet.StartBitStream(groupGUIDBytes, 0, 1, 5, 4, 3, 2, 6, 7);
            var bits1490 = 0u;
            if (bit1490)
                bits1490 = packet.ReadBits(9);
            packet.ReadBit(); // fake bit
            packet.StartBitStream(receiverGUIDBytes, 7, 6, 1, 4, 0, 2, 3, 5);
            packet.ReadBit(); // fake bit
            var hasLang = !packet.ReadBit();
            var hasPrefix = !packet.ReadBit();
            packet.StartBitStream(senderGUIDBytes, 0, 3, 7, 2, 1, 5, 4, 6);
            var hasAchi = !packet.ReadBit();
            var hasText = !packet.ReadBit();
            var channelLen = 0u;
            if (hasChannel)
                channelLen = packet.ReadBits(7);
            var textLen = 0u;
            if (hasText)
                textLen = packet.ReadBits(12);
            var hasReceiver = !packet.ReadBit();
            var prefixLen = 0u;
            if (hasPrefix)
                prefixLen = packet.ReadBits(5);
            var hasRealmId2 = !packet.ReadBit();
            var receiverLen = 0u;
            if (hasReceiver)
                receiverLen = packet.ReadBits(11);
            packet.ReadBit(); // fake bit ??????
            packet.StartBitStream(guildGUIDBytes, 2, 5, 7, 4, 0, 1, 3, 6);

            packet.ParseBitStream(guildGUIDBytes, 4, 5, 7, 3, 2, 6, 0, 1);
            if (hasChannel)
                packet.ReadWoWString("Channel Name", channelLen);
            if (hasPrefix)
                packet.ReadWoWString("Addon Message Prefix", prefixLen);

            if (bit1494)
                packet.ReadSingle("Float1494");

            packet.ParseBitStream(senderGUIDBytes, 4, 7, 1, 5, 0, 6, 2, 3);

            text.Type = (ChatMessageType)packet.ReadByteE<ChatMessageTypeNew>("SlashCmd");
            if (hasAchi)
                packet.ReadInt32<AchievementId>("Achievement Id");
            packet.ParseBitStream(groupGUIDBytes, 1, 3, 4, 6, 0, 2, 5, 7);
            packet.ParseBitStream(receiverGUIDBytes, 2, 5, 3, 6, 7, 4, 1, 0);
            if (hasLang)
                text.Language = packet.ReadByteE<Language>("Language");
            if (hasRealmId2)
                packet.ReadInt32("Realm Id 2");
            if (hasText)
                text.Text = packet.ReadWoWString("Text", textLen);
            if (hasReceiver)
                text.ReceiverName = packet.ReadWoWString("Receiver Name", receiverLen);
            if (hasSender)
                text.SenderName = packet.ReadWoWString("Sender Name", senderNameLen);
            if (hasRealmId1)
                packet.ReadInt32("Realm Id");

            text.SenderGUID = packet.WriteGuid("SenderGUID", senderGUIDBytes);
            text.ReceiverGUID = packet.WriteGuid("ReceiverGUID", receiverGUIDBytes);
            packet.WriteGuid("GuildGUID", guildGUIDBytes);
            packet.WriteGuid("GroupGUID", groupGUIDBytes);

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
            var len = packet.ReadBits(9);
            packet.ReadWoWString("Name", len);
        }

        [Parser(Opcode.SMSG_DEFENSE_MESSAGE)]
        public static void HandleDefenseMessage(Packet packet)
        {
            var message = new DefenseMessage();

            var zoneId = packet.ReadUInt32<ZoneId>("Zone Id");
            var length = packet.ReadBits("Message Length", 12);
            message.Text = packet.ReadWoWString("Message", length);

            Storage.DefenseMessages.Add(zoneId, message, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_NOTIFICATION)]
        public static void HandleNotification(Packet packet)
        {
            var length = packet.ReadBits(12);
            packet.ReadWoWString("Message", length);
        }
    }
}
