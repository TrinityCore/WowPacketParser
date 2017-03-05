using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.SMSG_CHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            var text = new CreatureText();
            var senderGUIDBytes = new byte[8];
            var guildGUIDBytes = new byte[8];
            var receiverGUIDBytes = new byte[8];
            var groupGUIDBytes = new byte[8];

            var hasSender = !packet.Translator.ReadBit();
            var hasText = !packet.Translator.ReadBit();

            packet.Translator.ReadBit(); // fake bit

            packet.Translator.StartBitStream(groupGUIDBytes, 1, 0, 6, 7, 3, 4, 2, 5);

            var hasReceiver = !packet.Translator.ReadBit();
            var hasRealmId = !packet.Translator.ReadBit();

            packet.Translator.ReadBit(); // fake bit

            var bit1495 = packet.Translator.ReadBit();
            var bit11 = !packet.Translator.ReadBit();
            var hasChannel = !packet.Translator.ReadBit();

            packet.Translator.StartBitStream(guildGUIDBytes, 0, 7, 6, 4, 1, 3, 2, 5);

            var senderNameLen = 0u;
            if (hasSender)
                senderNameLen = packet.Translator.ReadBits(11);

            var textLen = 0u;
            if (hasText)
                textLen = packet.Translator.ReadBits(12);

            packet.Translator.ReadBit(); // fake bit

            var hasAchi = !packet.Translator.ReadBit();

            packet.Translator.StartBitStream(senderGUIDBytes, 4, 2, 7, 5, 1, 3, 0, 6);

            var bit148C = !packet.Translator.ReadBit();
            var hasPrefix = !packet.Translator.ReadBit();

            var prefixLen = 0u;
            if (hasPrefix)
                prefixLen = packet.Translator.ReadBits(5);

            var channelLen = 0u;
            if (hasChannel)
                channelLen = packet.Translator.ReadBits(7);

            var bit1494 = packet.Translator.ReadBit();
            var bit1490 = !packet.Translator.ReadBit();

            packet.Translator.ReadBit(); // fake bit

            var bits148C = 0u;

            if (bit148C)
                bits148C = packet.Translator.ReadBits(9);

            packet.Translator.StartBitStream(receiverGUIDBytes, 1, 5, 4, 6, 3, 2, 7, 0);

            var receiverNameLen = 0u;
            if (hasReceiver)
                receiverNameLen = packet.Translator.ReadBits(11);

            if (hasReceiver)
                text.ReceiverName = packet.Translator.ReadWoWString("Receiver Name", receiverNameLen);

            if (hasRealmId)
                packet.Translator.ReadInt32("Realm Id");

            packet.Translator.ParseBitStream(groupGUIDBytes, 2, 3, 1, 4, 0, 5, 6, 7);
            packet.Translator.ParseBitStream(receiverGUIDBytes, 2, 7, 5, 0, 3, 4, 1, 6);
            packet.Translator.ParseBitStream(senderGUIDBytes, 5, 7, 3, 1, 6, 2, 4, 0);
            packet.Translator.ParseBitStream(guildGUIDBytes, 5, 7, 4, 1, 2, 0, 6, 3);

            if (hasChannel)
                packet.Translator.ReadWoWString("Channel Name", channelLen);

            if (hasAchi)
                packet.Translator.ReadInt32<AchievementId>("Achievement Id");

            if (hasPrefix)
                packet.Translator.ReadWoWString("Addon Message Prefix", prefixLen);

            if (bit11)
                packet.Translator.ReadByte("Byte11");

            if (bit1490)
                packet.Translator.ReadSingle("Float1490");

            if (hasSender)
                text.SenderName = packet.Translator.ReadWoWString("Sender Name", senderNameLen);

            if (hasText)
                text.Text = packet.Translator.ReadWoWString("Text", textLen);

            text.Type = (ChatMessageType)packet.Translator.ReadByteE<ChatMessageTypeNew>("SlashCmd");

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

        [Parser(Opcode.SMSG_TEXT_EMOTE)]
        public static void HandleTextEmoteServer(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.Translator.ReadInt32E<EmoteType>("Emote ID");
            packet.Translator.ReadInt32E<EmoteTextType>("Text Emote ID");

            guid2[1] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 0);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_DEFENSE_MESSAGE)]
        public static void HandleDefenseMessage(Packet packet)
        {
            var len = packet.Translator.ReadBits(12);
            packet.Translator.ReadInt32<ZoneId>("Zone Id");
            packet.Translator.ReadWoWString("Message", len);
        }
    }
}
