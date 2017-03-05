using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.SMSG_CHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            var text = new CreatureText();
            var groupGUIDBytes = new byte[8];
            var guildGUIDBytes = new byte[8];
            var receiverGUIDBytes = new byte[8];
            var senderGUIDBytes = new byte[8];

            var bit1316 = !packet.Translator.ReadBit();
            packet.Translator.ReadBit("bit5269");
            packet.Translator.ReadBit(); // fake bit
            var hasSender = !packet.Translator.ReadBit();
            packet.Translator.StartBitStream(groupGUIDBytes, 6, 1, 7, 5, 4, 3, 2, 0);
            packet.Translator.ReadBit(); // fake bit
            var hasChannel = !packet.Translator.ReadBit();
            packet.Translator.StartBitStream(receiverGUIDBytes, 1, 5, 7, 4, 2, 0, 6, 3);
            packet.Translator.ReadBit("bit5268");
            var hasReceiver = !packet.Translator.ReadBit();

            int receiverLen = 0;
            if (hasReceiver)
                receiverLen = (int)packet.Translator.ReadBits(11);

            packet.Translator.ReadBit(); // fake bit
            packet.Translator.StartBitStream(guildGUIDBytes, 0, 6, 1, 5, 7, 3, 4, 2);
            var hasText = !packet.Translator.ReadBit();
            var hasPrefix = !packet.Translator.ReadBit();

            int senderName = 0;
            if (hasSender)
                senderName = (int)packet.Translator.ReadBits(11);

            int textLen = 0;
            if (hasText)
                textLen = (int)packet.Translator.ReadBits(12);

            var hasConstTime = !packet.Translator.ReadBit();
            var hasAchi = !packet.Translator.ReadBit();
            packet.Translator.ReadBit(); // fake bit
            packet.Translator.StartBitStream(senderGUIDBytes, 5, 4, 1, 0, 6, 2, 7, 3);

            int channelLen = 0;
            if (hasChannel)
                channelLen = (int)packet.Translator.ReadBits(7);

            var bit2630 = !packet.Translator.ReadBit();
            if (bit2630)
                packet.Translator.ReadBitsE<ChatTag>("Chat Tag", 9);

            var hasLang = !packet.Translator.ReadBit();
            int prefixLen = 0;
            if (hasPrefix)
                prefixLen = (int)packet.Translator.ReadBits(5);

            if (hasPrefix)
                packet.Translator.ReadWoWString("Addon Message Prefix", prefixLen);

            packet.Translator.ParseBitStream(guildGUIDBytes, 3, 1, 5, 4, 6, 2, 0, 7);
            packet.Translator.ParseBitStream(receiverGUIDBytes, 7, 4, 2, 3, 1, 5, 6, 0);
            packet.Translator.ParseBitStream(senderGUIDBytes, 5, 0, 7, 4, 3, 2, 1, 6);
            packet.Translator.ParseBitStream(groupGUIDBytes, 3, 5, 2, 6, 4, 0, 1, 7);

            text.SenderGUID = packet.Translator.WriteGuid("SenderGUID", senderGUIDBytes);
            text.ReceiverGUID = packet.Translator.WriteGuid("ReceiverGUID", receiverGUIDBytes);
            packet.Translator.WriteGuid("GroupGUID", groupGUIDBytes);
            packet.Translator.WriteGuid("GuildGUID", guildGUIDBytes);


            if (hasAchi)
                packet.Translator.ReadInt32<AchievementId>("Achievement Id");

            if (hasReceiver)
                text.ReceiverName = packet.Translator.ReadWoWString("Receiver Name", receiverLen);

            text.Type = (ChatMessageType)packet.Translator.ReadByteE<ChatMessageTypeNew>("Chat type");
            if (hasText)
                text.Text = packet.Translator.ReadWoWString("Text", textLen);

            if (hasConstTime)
                packet.Translator.ReadInt32("Constant time");

            if (bit1316)
                packet.Translator.ReadSingle("float1316");

            if (hasChannel)
                packet.Translator.ReadWoWString("Channel Name", channelLen);

            if (hasSender)
                text.SenderName = packet.Translator.ReadWoWString("Sender Name", senderName);

            if (hasLang)
                text.Language = packet.Translator.ReadByteE<Language>("Language");

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

            packet.Translator.ReadInt32E<EmoteTextType>("Text Emote ID");
            packet.Translator.ReadInt32E<EmoteType>("Emote ID");

            packet.Translator.StartBitStream(guid, 3, 4, 5, 1, 6, 2, 0, 7);
            packet.Translator.ParseBitStream(guid, 6, 7, 4, 5, 2, 1, 3, 0);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_TEXT_EMOTE)]
        public static void HandleTextEmoteServer(Packet packet)
        {
            var guid = new byte[8];

            guid[4] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            var bits7 = packet.Translator.ReadBits(7);
            guid[7] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();

            packet.Translator.ReadInt32E<EmoteType>("Emote ID");
            packet.Translator.ReadWoWString("Name", bits7);
            packet.Translator.ReadInt32E<EmoteTextType>("Text Emote ID");

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_LIST)]
        public static void HandleChannelList(Packet packet)
        {
            packet.Translator.ReadUInt32("Flags");
            var password = packet.Translator.ReadBits(7);
            packet.Translator.ReadBit();
            var length = packet.Translator.ReadBits(7);
            packet.Translator.ReadBit();

            packet.Translator.ReadWoWString("Password", password);
            packet.Translator.ReadWoWString("Channel Name", length);
        }

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
            packet.Translator.ReadInt32E<Language>("Language");
            var len = packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_AFK)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_DND)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_EMOTE)]
        public static void HandleClientChatMessage2(Packet packet)
        {
            var len = packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_CHANNEL)]
        public static void HandleClientChatMessageChannel(Packet packet)
        {
            packet.Translator.ReadInt32E<Language>("Language");
            var msgLen = packet.Translator.ReadBits(8);
            var channelNameLen = packet.Translator.ReadBits(9);

            packet.Translator.ReadWoWString("Message", msgLen);
            packet.Translator.ReadWoWString("Channel Name", channelNameLen);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_WHISPER)]
        public static void HandleClientChatMessageWhisper(Packet packet)
        {
            packet.Translator.ReadInt32E<Language>("Language");
            var recvName = packet.Translator.ReadBits(9);
            var msgLen = packet.Translator.ReadBits(8);

            packet.Translator.ReadWoWString("Message", msgLen);
            packet.Translator.ReadWoWString("Receivers Name", recvName);
        }

        [Parser(Opcode.SMSG_DEFENSE_MESSAGE)]
        public static void HandleDefenseMessage(Packet packet)
        {
            packet.Translator.ReadInt32<ZoneId>("Zone Id");
            packet.Translator.ReadBits("Message Length?", 12);
            packet.Translator.ReadCString("Message");
        }
    }
}
