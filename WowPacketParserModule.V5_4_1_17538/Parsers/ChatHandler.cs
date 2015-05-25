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

            var hasLang = !packet.ReadBit();
            var hasReceiver = !packet.ReadBit();

            packet.ReadBit(); // fake bit

            packet.StartBitStream(groupGUIDBytes, 3, 7, 2, 6, 0, 4, 5, 1);

            var hasAchi = !packet.ReadBit();
            var bit1495 = packet.ReadBit();
            var hasPrefix = !packet.ReadBit();

            packet.ReadBit(); // fake bit

            var bit43D = !packet.ReadBit();

            packet.StartBitStream(guildGUIDBytes, 1, 6, 0, 5, 2, 4, 7, 3);

            packet.ReadBit(); // fake bit

            packet.StartBitStream(receiverGUIDBytes, 6, 1, 3, 5, 4, 2, 7, 0);

            var receiverLen = 0u;
            if (hasReceiver)
                receiverLen = packet.ReadBits(11);

            var hasChannel = !packet.ReadBit();
            var bit1494 = packet.ReadBit();
            var bit1490 = !packet.ReadBit();
            var hasRealmId = !packet.ReadBit();

            var bits43D = 0u;
            if (bit43D)
                bits43D = packet.ReadBits(11);

            var channelLen = 0u;
            if (hasChannel)
                channelLen = packet.ReadBits(7);

            var bit8CF = !packet.ReadBit();

            var textLen = 0u;
            if (bit8CF)
                textLen = packet.ReadBits(12);

            packet.ReadBit(); // fake bit

            packet.StartBitStream(senderGUIDBytes, 4, 1, 3, 6, 2, 5, 0, 7);

            var bit148C = !packet.ReadBit();

            var bits148C = 0u;
            if (bit148C)
                bits148C = packet.ReadBits(9);

            // TODO: missing sender name

            if (hasPrefix)
            {
                var prefixLen = packet.ReadBits(5);
                packet.ReadWoWString("Addon Message Prefix", prefixLen);
            }

            packet.ParseBitStream(groupGUIDBytes, 4, 2, 7, 3, 6, 1, 5, 0);

            if (hasReceiver)
                text.ReceiverName = packet.ReadWoWString("Receiver Name", receiverLen);

            packet.ParseBitStream(receiverGUIDBytes, 7, 4, 1, 3, 0, 6, 5, 2);
            packet.ParseBitStream(guildGUIDBytes, 5, 7, 3, 0, 4, 6, 1, 2);

            if (hasLang)
                text.Language = packet.ReadByteE<Language>("Language");

            packet.ParseBitStream(senderGUIDBytes, 7, 4, 0, 6, 3, 2, 5, 1);

            if (hasChannel)
                packet.ReadWoWString("Channel Name", channelLen);

            text.Text = packet.ReadWoWString("Text", textLen);

            text.Type = (ChatMessageType)packet.ReadByteE<ChatMessageTypeNew>("SlashCmd");

            if (hasAchi)
                packet.ReadInt32<AchievementId>("Achievement Id");

            if (bit1490)
                packet.ReadSingle("Float1490");

            if (hasRealmId)
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
