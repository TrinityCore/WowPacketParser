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
            packet.ReadInt32<ZoneId>("Zone Id");
            var len = packet.ReadBits(12);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_LIST)]
        public static void HandleChannelList(Packet packet)
        {
            packet.ReadUInt32("Flags");
            packet.ReadBit();
            var length = packet.ReadBits("", 7);
            packet.ReadBit();
            packet.ReadBits("HasPassword", 7);

            packet.ReadWoWString("Channel Name", length);
        }

        [Parser(Opcode.SMSG_CHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            var text = new CreatureText();

            var senderGUIDBytes = new byte[8];
            var guildGUIDBytes = new byte[8];
            var receiverGUIDBytes = new byte[8];
            var groupGUIDBytes = new byte[8];

            var bit1495 = packet.ReadBit();

            var hasText = !packet.ReadBit();
            var hasAchi = !packet.ReadBit();
            var hasSender = !packet.ReadBit();

            packet.ReadBit(); // fake bit

            packet.StartBitStream(senderGUIDBytes, 2, 4, 0, 6, 1, 3, 5, 7);

            packet.ReadBit(); // fake bit

            packet.StartBitStream(groupGUIDBytes, 6, 0, 4, 1, 2, 3, 7, 5);

            var hasPrefix = !packet.ReadBit();
            var bit1494 = packet.ReadBit();
            var hasRealmId = !packet.ReadBit();
            var bit1490 = !packet.ReadBit();

            int senderName = 0;
            if (hasSender)
                senderName = (int)packet.ReadBits(11);

            packet.ReadBit(); // fake bit

            packet.StartBitStream(receiverGUIDBytes, 4, 0, 6, 7, 5, 1, 3, 2);

            int prefixLen = 0;
            if (hasPrefix)
                prefixLen = (int)packet.ReadBits(5);

            var hasReceiver = !packet.ReadBit();
            var bit148C = !packet.ReadBit();

            int textLen = 0;
            if (hasText)
                textLen = (int)packet.ReadBits(12);

            var hasLang = !packet.ReadBit();

            int countBits148C = 0;
            if (bit148C)
                countBits148C = (int)packet.ReadBits(9);

            packet.ReadBit(); // fake bit

            int receiverLen = 0;
            if (hasReceiver)
                receiverLen = (int)packet.ReadBits(11);

            packet.StartBitStream(guildGUIDBytes, 0, 2, 1, 4, 6, 7, 5, 3);

            var hasChannel = !packet.ReadBit();

            if (hasChannel)
            {
                var channelLen = (int)packet.ReadBits(7);
                packet.ReadWoWString("Channel Name", channelLen);
            }

            if (hasSender)
                text.SenderName = packet.ReadWoWString("Sender Name", senderName);

            packet.ParseBitStream(groupGUIDBytes, 6, 7, 1, 2, 4, 3, 0, 5);

            packet.ParseBitStream(receiverGUIDBytes, 0, 4, 1, 3, 5, 7, 2, 6);

            text.Type = (ChatMessageType)packet.ReadByteE<ChatMessageTypeNew>("SlashCmd");

            packet.ParseBitStream(senderGUIDBytes, 7, 6, 5, 4, 0, 2, 1, 3);

            if (hasPrefix)
                packet.ReadWoWString("Addon Message Prefix", prefixLen);

            if (hasRealmId)
                packet.ReadInt32("Realm Id");

            packet.ParseBitStream(guildGUIDBytes, 1, 0, 3, 7, 6, 5, 2, 4);

            if (hasReceiver)
                text.ReceiverName = packet.ReadWoWString("Receiver Name", receiverLen);

            if (hasAchi)
                packet.ReadInt32<AchievementId>("Achievement Id");

            if (hasLang)
                text.Language = packet.ReadByteE<Language>("Language");

            if (hasText)
                text.Text = packet.ReadWoWString("Text", textLen);

            if (bit1490)
                packet.ReadSingle("Float1490");

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

        [Parser(Opcode.SMSG_TEXT_EMOTE)]
        public static void HandleTextEmoteServer(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[0] = packet.ReadBit();
            packet.StartBitStream(guid1, 3, 4);
            packet.StartBitStream(guid2, 6, 7, 3);
            packet.StartBitStream(guid1, 6, 7);
            packet.StartBitStream(guid2, 5, 2, 1);
            guid1[0] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            packet.StartBitStream(guid1, 1, 5, 2);

            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 6);

            packet.ReadInt32E<EmoteType>("Emote ID");

            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 4);

            packet.ReadInt32E<EmoteTextType>("Text Emote ID");

            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 3);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
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
            packet.ReadInt32E<Language>("Language");
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Message", len);
        }
    }
}
