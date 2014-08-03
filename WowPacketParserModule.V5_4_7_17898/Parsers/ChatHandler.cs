using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;
using ChatMessageType540 = WowPacketParserModule.V5_4_7_17898.Enums.ChatMessageType;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.CMSG_MESSAGECHAT_GUILD)]
        [Parser(Opcode.CMSG_MESSAGECHAT_INSTANCE)]
        [Parser(Opcode.CMSG_MESSAGECHAT_OFFICER)]
        [Parser(Opcode.CMSG_MESSAGECHAT_PARTY)]
        [Parser(Opcode.CMSG_MESSAGECHAT_RAID)]
        [Parser(Opcode.CMSG_MESSAGECHAT_RAID_WARNING)]
        [Parser(Opcode.CMSG_MESSAGECHAT_SAY)]
        [Parser(Opcode.CMSG_MESSAGECHAT_YELL)]
        public static void HandleClientChatMessage(Packet packet)
        {
            packet.ReadEnum<Language>("Language", TypeCode.Int32);
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_DND)]
        [Parser(Opcode.CMSG_MESSAGECHAT_EMOTE)]
        [Parser(Opcode.CMSG_MESSAGECHAT_AFK)]
        public static void HandleMessageChatDND(Packet packet)
        {
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_WHISPER)]
        public static void HandleClientChatMessageWhisper(Packet packet)
        {
            packet.ReadEnum<Language>("Language", TypeCode.Int32);
            var msgLen = packet.ReadBits(9);
            var recvName = packet.ReadBits(8);

            packet.ReadWoWString("Receivers Name", recvName);
            packet.ReadWoWString("Message", msgLen);
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_CHANNEL)]
        public static void HandleClientChatMessageChannel434(Packet packet)
        {
            packet.ReadEnum<Language>("Language", TypeCode.Int32);
            var msgLen = packet.ReadBits(8);
            var channelNameLen = packet.ReadBits(9);

            packet.ReadWoWString("Channel Name", channelNameLen);
            packet.ReadWoWString("Message", msgLen);
        }

        [Parser(Opcode.SMSG_MESSAGECHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            var text = new CreatureText();

            var SenderGUID = new byte[8];
            var GuildGUID = new byte[8];
            var ReceiverGUID = new byte[8];
            var GroupGUID = new byte[8];

            packet.ReadBit(); // fake bit
            packet.ReadBit(); // fake bit

            packet.StartBitStream(GuildGUID, 4, 5, 1, 0, 2, 6, 7, 3);

            var bit1490 = !packet.ReadBit();
            var hasLang = !packet.ReadBit();

            packet.StartBitStream(SenderGUID, 2, 7, 0, 3, 4, 6, 1, 5);

            var bubble = packet.ReadBit(); // 0 Show in chat log, 1 for showing only in bubble
            if (bubble)
                packet.WriteLine("Show only in bubble");
            var hasAchi = !packet.ReadBit();
            var hasReceiver = !packet.ReadBit();
            var hasSender = !packet.ReadBit();
            var hasText = !packet.ReadBit();

            packet.ReadBit(); // fake bit

            packet.StartBitStream(ReceiverGUID, 5, 7, 6, 4, 3, 2, 1, 0);

            var hasRealmId1 = !packet.ReadBit();

            var receiverLen = 0u;
            if (hasReceiver)
                receiverLen = packet.ReadBits(11);

            var senderNameLen = 0u;
            if (hasSender)
                senderNameLen = packet.ReadBits(11);

            packet.ReadBit(); // fake bit

            packet.StartBitStream(GroupGUID, 5, 2, 6, 1, 7, 3, 0, 4);

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

            packet.ParseBitStream(GuildGUID, 7, 2, 1, 4, 6, 5, 3, 0);
            packet.ParseBitStream(GroupGUID, 5, 3, 2, 4, 1, 0, 7, 6);

            text.Type = (ChatMessageType)packet.ReadEnum<ChatMessageType540>("Chat type", TypeCode.Byte);

            if (hasRealmId1)
                packet.ReadInt32("Realm Id");

            if (hasPrefix)
                packet.ReadWoWString("Addon Message Prefix", prefixLen);

            if (bit1494)
                packet.ReadSingle("Float1494");

            packet.ParseBitStream(ReceiverGUID, 4, 2, 3, 0, 6, 7, 5, 1);
            packet.ParseBitStream(SenderGUID, 6, 1, 0, 2, 4, 5, 7, 3);


            if (hasAchi)
                packet.ReadInt32("Achievement");

            if (hasReceiver)
                packet.ReadWoWString("Receiver Name", receiverLen);

            if (hasText)
                text.Text = packet.ReadWoWString("Text", textLen);

            if (hasSender)
                text.Comment = packet.ReadWoWString("Sender Name", senderNameLen);

            if (hasLang)
                text.Language = packet.ReadEnum<Language>("Language", TypeCode.Byte);

            if (hasChannel)
                packet.ReadWoWString("Channel Name", channelLen);

            if (hasRealmId2)
                packet.ReadInt32("Realm Id 2");

            packet.WriteGuid("SenderGUID", SenderGUID);
            packet.WriteGuid("ReceiverGUID", ReceiverGUID);
            packet.WriteGuid("GuildGUID", GuildGUID);
            packet.WriteGuid("GroupGUID", GroupGUID);

            uint entry = 0;
            var guid = new Guid(BitConverter.ToUInt64(SenderGUID, 0));
            if (guid.GetObjectType() == ObjectType.Unit)
                entry = guid.GetEntry();

            if (entry != 0)
                Storage.CreatureTexts.Add(entry, text, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_TEXT_EMOTE)]
        public static void HandleTextEmote(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadEnum<EmoteTextType>("Text Emote ID", TypeCode.Int32);
            packet.ReadEnum<EmoteType>("Emote ID", TypeCode.Int32);

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
            packet.ReadEnum<EmoteType>("Emote ID", TypeCode.Int32);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 2);
            packet.ReadEnum<EmoteTextType>("Text Emote ID", TypeCode.Int32);
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
            packet.ReadEntryWithName<Int32>(StoreNameType.Zone, "Zone Id");
            packet.ReadWoWString("Message", len);
        }
    }
}
