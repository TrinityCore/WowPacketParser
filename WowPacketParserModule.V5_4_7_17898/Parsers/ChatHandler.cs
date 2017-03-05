using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.CMSG_CHAT_MESSAGE_GUILD)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_INSTANCE_CHAT)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_OFFICER)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_PARTY)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_RAID)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_RAID_WARNING)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_SAY)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_YELL)]
        public static void HandleClientChatMessage(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_DND)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_EMOTE)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_AFK)]
        public static void HandleMessageChatDND(Packet packet)
        {
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_WHISPER)]
        public static void HandleClientChatMessageWhisper(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var msgLen = packet.ReadBits(9);
            var recvName = packet.ReadBits(8);

            packet.ReadWoWString("Receivers Name", recvName);
            packet.ReadWoWString("Message", msgLen);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_CHANNEL)]
        public static void HandleClientChatMessageChannel434(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var msgLen = packet.ReadBits(8);
            var channelNameLen = packet.ReadBits(9);

            packet.ReadWoWString("Channel Name", channelNameLen);
            packet.ReadWoWString("Message", msgLen);
        }

        [Parser(Opcode.SMSG_CHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            var text = new CreatureText();

            var senderGUIDBytes = new byte[8];
            var guildGUIDBytes = new byte[8];
            var receiverGUIDBytes = new byte[8];
            var groupGUIDBytes = new byte[8];

            packet.ReadBit(); // fake bit
            packet.ReadBit(); // fake bit

            packet.StartBitStream(guildGUIDBytes, 4, 5, 1, 0, 2, 6, 7, 3);

            var bit1490 = !packet.ReadBit();
            var hasLang = !packet.ReadBit();

            packet.StartBitStream(senderGUIDBytes, 2, 7, 0, 3, 4, 6, 1, 5);

            packet.ReadBit("Show only in bubble"); // 0 Show in chat log, 1 for showing only in bubble
            var hasAchi = !packet.ReadBit();
            var hasReceiver = !packet.ReadBit();
            var hasSender = !packet.ReadBit();
            var hasText = !packet.ReadBit();

            packet.ReadBit(); // fake bit

            packet.StartBitStream(receiverGUIDBytes, 5, 7, 6, 4, 3, 2, 1, 0);

            var hasRealmId1 = !packet.ReadBit();

            var receiverLen = 0u;
            if (hasReceiver)
                receiverLen = packet.ReadBits(11);

            var senderNameLen = 0u;
            if (hasSender)
                senderNameLen = packet.ReadBits(11);

            packet.ReadBit(); // fake bit

            packet.StartBitStream(groupGUIDBytes, 5, 2, 6, 1, 7, 3, 0, 4);

            var bit1494 = !packet.ReadBit();

            var bits1490 = 0u;
            if (bit1490)
                bits1490 = packet.ReadBits(9);

            var textLen = 0u;
            if (hasText)
                textLen = packet.ReadBits(12);

            var bit1499 = packet.ReadBit();
            var hasPrefix = !packet.ReadBit();
            var hasChannel = !packet.ReadBit();

            var prefixLen = 0u;
            if (hasPrefix)
                prefixLen = packet.ReadBits(5);

            var channelLen = 0u;
            if (hasChannel)
                channelLen = packet.ReadBits(7);

            var hasRealmId2 = !packet.ReadBit();

            packet.ParseBitStream(guildGUIDBytes, 7, 2, 1, 4, 6, 5, 3, 0);
            packet.ParseBitStream(groupGUIDBytes, 5, 3, 2, 4, 1, 0, 7, 6);

            text.Type = (ChatMessageType)packet.ReadByteE<ChatMessageTypeNew>("SlashCmd");

            if (hasRealmId1)
                packet.ReadInt32("Realm Id");

            if (hasPrefix)
                packet.ReadWoWString("Addon Message Prefix", prefixLen);

            if (bit1494)
                packet.ReadSingle("Float1494");

            packet.ParseBitStream(receiverGUIDBytes, 4, 2, 3, 0, 6, 7, 5, 1);
            packet.ParseBitStream(senderGUIDBytes, 6, 1, 0, 2, 4, 5, 7, 3);


            if (hasAchi)
                packet.ReadInt32<AchievementId>("Achievement Id");

            if (hasReceiver)
                text.ReceiverName = packet.ReadWoWString("Receiver Name", receiverLen);

            if (hasText)
                text.Text = packet.ReadWoWString("Text", textLen);

            if (hasSender)
                text.SenderName = packet.ReadWoWString("Sender Name", senderNameLen);

            if (hasLang)
                text.Language = packet.ReadByteE<Language>("Language");

            if (hasChannel)
                packet.ReadWoWString("Channel Name", channelLen);

            if (hasRealmId2)
                packet.ReadInt32("Realm Id 2");

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

        [Parser(Opcode.CMSG_SEND_TEXT_EMOTE)]
        public static void HandleTextEmote(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32E<EmoteTextType>("Text Emote ID");
            packet.ReadInt32E<EmoteType>("Emote ID");

            packet.StartBitStream(guid, 2, 3, 0, 7, 4, 6, 5, 1);
            packet.ParseBitStream(guid, 0, 6, 5, 7, 3, 4, 1, 2);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_TEXT_EMOTE)]
        public static void HandleTextEmoteServer(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid1[2] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 6);
            packet.ReadInt32E<EmoteType>("Emote ID");
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 2);
            packet.ReadInt32E<EmoteTextType>("Text Emote ID");
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 5);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_DEFENSE_MESSAGE)]
        public static void HandleDefenseMessage(Packet packet)
        {
            var len = packet.ReadBits(12);
            packet.ReadInt32<ZoneId>("Zone Id");
            packet.ReadWoWString("Message", len);
        }
    }
}
