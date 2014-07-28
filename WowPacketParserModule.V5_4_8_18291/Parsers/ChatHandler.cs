using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;
using ChatMessageType540 = WowPacketParserModule.V5_4_0_17359.Enums.ChatMessageType;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.CMSG_MESSAGECHAT_GUILD)]
        [Parser(Opcode.CMSG_MESSAGECHAT_YELL)]
        [Parser(Opcode.CMSG_MESSAGECHAT_SAY)]
        public static void HandleClientChatMessage(Packet packet)
        {
            packet.ReadEnum<Language>("Language", TypeCode.Int32);
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_WHISPER)]
        public static void HandleClientChatMessageWhisper(Packet packet)
        {
            packet.ReadEnum<Language>("Language", TypeCode.Int32);
            var msgLen = packet.ReadBits(8);
            var recvName = packet.ReadBits(9);
            packet.ReadWoWString("Message", msgLen);
            packet.ReadWoWString("Receivers Name", recvName);
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_CHANNEL)]
        public static void HandleClientChatMessageChannel434(Packet packet)
        {
            packet.ReadEnum<Language>("Language", TypeCode.Int32);
            var channelNameLen = packet.ReadBits(9);
            var msgLen = packet.ReadBits(8);
            packet.ReadWoWString("Message", msgLen);
            packet.ReadWoWString("Channel Name", channelNameLen);
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_DND)]
        [Parser(Opcode.CMSG_MESSAGECHAT_EMOTE)]
        [Parser(Opcode.CMSG_MESSAGECHAT_AFK)]
        public static void HandleMessageChatDND(Packet packet)
        {
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_TEXT_EMOTE)]
        public static void HandleTextEmote(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadEnum<EmoteTextType>("Text Emote ID", TypeCode.Int32);
            packet.ReadEnum<EmoteType>("Emote ID", TypeCode.Int32);

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
            packet.ReadEnum<EmoteTextType>("Text Emote ID", TypeCode.Int32);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(targetGuid, 0);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(targetGuid, 6);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(targetGuid, 3);
            packet.ReadXORByte(targetGuid, 5);
            packet.ReadXORByte(targetGuid, 4);
            packet.ReadEnum<EmoteType>("Emote ID", TypeCode.Int32);

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("TargetGuid", targetGuid);
        }

        [Parser(Opcode.SMSG_MESSAGECHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            var text = new CreatureText();

            var SenderGUID = new byte[8];
            var GuildGUID = new byte[8];
            var ReceiverGUID = new byte[8];
            var GroupGUID = new byte[8];

            var hasSender = !packet.ReadBit();

            var bubble = packet.ReadBit(); // 0 Show in chat log, 1 for showing only in bubble
            if (bubble)
                packet.WriteLine("Show only in bubble");

            var senderNameLen = 0u;
            if (hasSender)
                senderNameLen = packet.ReadBits(11);

            packet.ReadBit(); // fake bit
            var hasChannel = !packet.ReadBit();
            var bit1499 = packet.ReadBit();
            var bit1494 = !packet.ReadBit();
            var bit1490 = !packet.ReadBit();
            var hasRealmId1 = !packet.ReadBit();
            packet.StartBitStream(GroupGUID, 0, 1, 5, 4, 3, 2, 6, 7);
            var bits1490 = 0u;
            if (bit1490)
                bits1490 = packet.ReadBits(9);
            packet.ReadBit(); // fake bit
            packet.StartBitStream(ReceiverGUID, 7, 6, 1, 4, 0, 2, 3, 5);
            packet.ReadBit(); // fake bit
            var hasLang = !packet.ReadBit();
            var hasPrefix = !packet.ReadBit();
            packet.StartBitStream(SenderGUID, 0, 3, 7, 2, 1, 5, 4, 6);
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
            packet.StartBitStream(GuildGUID, 2, 5, 7, 4, 0, 1, 3, 6);

            packet.ParseBitStream(GuildGUID, 4, 5, 7, 3, 2, 6, 0, 1);
            if (hasChannel)
                packet.ReadWoWString("Channel Name", channelLen);
            if (hasPrefix)
                packet.ReadWoWString("Addon Message Prefix", prefixLen);

            if (bit1494)
                packet.ReadSingle("Float1494");

            packet.ParseBitStream(SenderGUID, 4, 7, 1, 5, 0, 6, 2, 3);

            text.Type = (ChatMessageType)packet.ReadEnum<ChatMessageType540>("Chat type", TypeCode.Byte);
            if (hasAchi)
                packet.ReadInt32("Achievement");
            packet.ParseBitStream(GroupGUID, 1, 3, 4, 6, 0, 2, 5, 7);
            packet.ParseBitStream(ReceiverGUID, 2, 5, 3, 6, 7, 4, 1, 0);
            if (hasLang)
                text.Language = packet.ReadEnum<Language>("Language", TypeCode.Byte);
            if (hasRealmId2)
                packet.ReadInt32("Realm Id 2");
            if (hasText)
                text.Text = packet.ReadWoWString("Text", textLen);
            if (hasReceiver)
                packet.ReadWoWString("Receiver Name", receiverLen);
            if (hasSender)
                text.Comment = packet.ReadWoWString("Sender Name", senderNameLen);
            if (hasRealmId1)
                packet.ReadInt32("Realm Id");

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

        [Parser(Opcode.SMSG_CHAT_PLAYER_NOT_FOUND)]
        public static void HandleChatPlayerNotFound(Packet packet)
        {
            var len = packet.ReadBits(9);
            packet.ReadWoWString("Name", len);
        }

        [Parser(Opcode.SMSG_DEFENSE_MESSAGE)]
        public static void HandleDefenseMessage(Packet packet)
        {
            var message = new DefenseMessage();

            uint zoneId = (uint)packet.ReadEntryWithName<UInt32>(StoreNameType.Zone, "Zone Id");
            var length = packet.ReadBits("Message Length", 12);
            message.text = packet.ReadWoWString("Message", length);

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
