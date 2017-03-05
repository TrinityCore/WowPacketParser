using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_1_17538.Parsers
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

            var hasLang = !packet.Translator.ReadBit();
            var hasReceiver = !packet.Translator.ReadBit();

            packet.Translator.ReadBit(); // fake bit

            packet.Translator.StartBitStream(groupGUIDBytes, 3, 7, 2, 6, 0, 4, 5, 1);

            var hasAchi = !packet.Translator.ReadBit();
            var bit1495 = packet.Translator.ReadBit();
            var hasPrefix = !packet.Translator.ReadBit();

            packet.Translator.ReadBit(); // fake bit

            var bit43D = !packet.Translator.ReadBit();

            packet.Translator.StartBitStream(guildGUIDBytes, 1, 6, 0, 5, 2, 4, 7, 3);

            packet.Translator.ReadBit(); // fake bit

            packet.Translator.StartBitStream(receiverGUIDBytes, 6, 1, 3, 5, 4, 2, 7, 0);

            var receiverLen = 0u;
            if (hasReceiver)
                receiverLen = packet.Translator.ReadBits(11);

            var hasChannel = !packet.Translator.ReadBit();
            var bit1494 = packet.Translator.ReadBit();
            var bit1490 = !packet.Translator.ReadBit();
            var hasRealmId = !packet.Translator.ReadBit();

            var bits43D = 0u;
            if (bit43D)
                bits43D = packet.Translator.ReadBits(11);

            var channelLen = 0u;
            if (hasChannel)
                channelLen = packet.Translator.ReadBits(7);

            var bit8CF = !packet.Translator.ReadBit();

            var textLen = 0u;
            if (bit8CF)
                textLen = packet.Translator.ReadBits(12);

            packet.Translator.ReadBit(); // fake bit

            packet.Translator.StartBitStream(senderGUIDBytes, 4, 1, 3, 6, 2, 5, 0, 7);

            var bit148C = !packet.Translator.ReadBit();

            var bits148C = 0u;
            if (bit148C)
                bits148C = packet.Translator.ReadBits(9);

            // TODO: missing sender name

            if (hasPrefix)
            {
                var prefixLen = packet.Translator.ReadBits(5);
                packet.Translator.ReadWoWString("Addon Message Prefix", prefixLen);
            }

            packet.Translator.ParseBitStream(groupGUIDBytes, 4, 2, 7, 3, 6, 1, 5, 0);

            if (hasReceiver)
                text.ReceiverName = packet.Translator.ReadWoWString("Receiver Name", receiverLen);

            packet.Translator.ParseBitStream(receiverGUIDBytes, 7, 4, 1, 3, 0, 6, 5, 2);
            packet.Translator.ParseBitStream(guildGUIDBytes, 5, 7, 3, 0, 4, 6, 1, 2);

            if (hasLang)
                text.Language = packet.Translator.ReadByteE<Language>("Language");

            packet.Translator.ParseBitStream(senderGUIDBytes, 7, 4, 0, 6, 3, 2, 5, 1);

            if (hasChannel)
                packet.Translator.ReadWoWString("Channel Name", channelLen);

            text.Text = packet.Translator.ReadWoWString("Text", textLen);

            text.Type = (ChatMessageType)packet.Translator.ReadByteE<ChatMessageTypeNew>("SlashCmd");

            if (hasAchi)
                packet.Translator.ReadInt32<AchievementId>("Achievement Id");

            if (bit1490)
                packet.Translator.ReadSingle("Float1490");

            if (hasRealmId)
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
