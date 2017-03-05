using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.SMSG_DEFENSE_MESSAGE)]
        public static void HandleDefenseMessage(Packet packet)
        {
            packet.Translator.ReadInt32<ZoneId>("Zone Id");
            var len = packet.Translator.ReadBits(12);
            packet.Translator.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_LIST)]
        public static void HandleChannelList(Packet packet)
        {
            packet.Translator.ReadUInt32("Flags");
            packet.Translator.ReadBit();
            var length = packet.Translator.ReadBits("", 7);
            packet.Translator.ReadBit();
            packet.Translator.ReadBits("HasPassword", 7);

            packet.Translator.ReadWoWString("Channel Name", length);
        }

        [Parser(Opcode.SMSG_CHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            var text = new CreatureText();

            var senderGUIDBytes = new byte[8];
            var guildGUIDBytes = new byte[8];
            var receiverGUIDBytes = new byte[8];
            var groupGUIDBytes = new byte[8];

            var bit1495 = packet.Translator.ReadBit();

            var hasText = !packet.Translator.ReadBit();
            var hasAchi = !packet.Translator.ReadBit();
            var hasSender = !packet.Translator.ReadBit();

            packet.Translator.ReadBit(); // fake bit

            packet.Translator.StartBitStream(senderGUIDBytes, 2, 4, 0, 6, 1, 3, 5, 7);

            packet.Translator.ReadBit(); // fake bit

            packet.Translator.StartBitStream(groupGUIDBytes, 6, 0, 4, 1, 2, 3, 7, 5);

            var hasPrefix = !packet.Translator.ReadBit();
            var bit1494 = packet.Translator.ReadBit();
            var hasRealmId = !packet.Translator.ReadBit();
            var bit1490 = !packet.Translator.ReadBit();

            int senderName = 0;
            if (hasSender)
                senderName = (int)packet.Translator.ReadBits(11);

            packet.Translator.ReadBit(); // fake bit

            packet.Translator.StartBitStream(receiverGUIDBytes, 4, 0, 6, 7, 5, 1, 3, 2);

            int prefixLen = 0;
            if (hasPrefix)
                prefixLen = (int)packet.Translator.ReadBits(5);

            var hasReceiver = !packet.Translator.ReadBit();
            var bit148C = !packet.Translator.ReadBit();

            int textLen = 0;
            if (hasText)
                textLen = (int)packet.Translator.ReadBits(12);

            var hasLang = !packet.Translator.ReadBit();

            int countBits148C = 0;
            if (bit148C)
                countBits148C = (int)packet.Translator.ReadBits(9);

            packet.Translator.ReadBit(); // fake bit

            int receiverLen = 0;
            if (hasReceiver)
                receiverLen = (int)packet.Translator.ReadBits(11);

            packet.Translator.StartBitStream(guildGUIDBytes, 0, 2, 1, 4, 6, 7, 5, 3);

            var hasChannel = !packet.Translator.ReadBit();

            if (hasChannel)
            {
                var channelLen = (int)packet.Translator.ReadBits(7);
                packet.Translator.ReadWoWString("Channel Name", channelLen);
            }

            if (hasSender)
                text.SenderName = packet.Translator.ReadWoWString("Sender Name", senderName);

            packet.Translator.ParseBitStream(groupGUIDBytes, 6, 7, 1, 2, 4, 3, 0, 5);

            packet.Translator.ParseBitStream(receiverGUIDBytes, 0, 4, 1, 3, 5, 7, 2, 6);

            text.Type = (ChatMessageType)packet.Translator.ReadByteE<ChatMessageTypeNew>("SlashCmd");

            packet.Translator.ParseBitStream(senderGUIDBytes, 7, 6, 5, 4, 0, 2, 1, 3);

            if (hasPrefix)
                packet.Translator.ReadWoWString("Addon Message Prefix", prefixLen);

            if (hasRealmId)
                packet.Translator.ReadInt32("Realm Id");

            packet.Translator.ParseBitStream(guildGUIDBytes, 1, 0, 3, 7, 6, 5, 2, 4);

            if (hasReceiver)
                text.ReceiverName = packet.Translator.ReadWoWString("Receiver Name", receiverLen);

            if (hasAchi)
                packet.Translator.ReadInt32<AchievementId>("Achievement Id");

            if (hasLang)
                text.Language = packet.Translator.ReadByteE<Language>("Language");

            if (hasText)
                text.Text = packet.Translator.ReadWoWString("Text", textLen);

            if (bit1490)
                packet.Translator.ReadSingle("Float1490");

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

        [Parser(Opcode.SMSG_TEXT_EMOTE)]
        public static void HandleTextEmoteServer(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[0] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 3, 4);
            packet.Translator.StartBitStream(guid2, 6, 7, 3);
            packet.Translator.StartBitStream(guid1, 6, 7);
            packet.Translator.StartBitStream(guid2, 5, 2, 1);
            guid1[0] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 1, 5, 2);

            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid1, 6);

            packet.Translator.ReadInt32E<EmoteType>("Emote ID");

            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 4);

            packet.Translator.ReadInt32E<EmoteTextType>("Text Emote ID");

            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 3);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
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
    }
}
