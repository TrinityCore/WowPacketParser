using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.WowGuid;
using ChatMessageType548 = WowPacketParserModule.V5_4_8_18414.Enums.ChatMessageType;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.CMSG_CHAT_IGNORED)]
        public static void HandleClientChatIgnored(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_CHANNEL)]
        public static void HandleClientChatChannel(Packet packet)
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

        [Parser(Opcode.CMSG_MESSAGECHAT_GUILD)]
        public static void HandleClientChatMessageGuild(Packet packet)
        {
            packet.ReadEnum<Language>("Language", TypeCode.Int32);
            packet.ReadWoWString("Message", packet.ReadBits(8));
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_OFFICER)]
        public static void HandleClientChatMessageOfficer(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_PARTY)]
        public static void HandleMessageChatParty(Packet packet)
        {
            packet.ReadEnum<Language>("Language", TypeCode.Int32);
            packet.ReadWoWString("Message", packet.ReadBits(8));
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_RAID)]
        public static void HandleClientChatMessageRaid(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_RAID_WARNING)]
        public static void HandleClientChatMessageRaidWarning(Packet packet)
        {
            packet.ReadEnum<Language>("Language", TypeCode.Int32);
            packet.ReadWoWString("Message", packet.ReadBits(8));
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_SAY)]
        public static void HandleClientChatMessageSay(Packet packet)
        {
                packet.ReadEnum<Language>("Language", TypeCode.Int32);
                packet.ReadWoWString("Message", packet.ReadBits(8));
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

        [Parser(Opcode.CMSG_MESSAGECHAT_YELL)]
        public static void HandleClientChatMessageYell(Packet packet)
        {
            packet.ReadEnum<Language>("Language", TypeCode.Int32);
            packet.ReadWoWString("Message", packet.ReadBits(8));
        }

        [Parser(Opcode.SMSG_CHAT_PLAYER_NOT_FOUND)]
        public static void HandleChatPlayerNotFound(Packet packet)
        {
            packet.ReadWoWString("Name", packet.ReadBits(9));
        }

        [Parser(Opcode.SMSG_MESSAGECHAT)] // sub_70A096
        public static void HandleMessageChat(Packet packet)
        {
            var text = new CreatureText();
            uint entry = 0;

            //text.Language = packet.ReadEnum<Language>("Language", TypeCode.Int32);

            var hasSenderName = !packet.ReadBit("!hasSenderName");
            packet.ReadBit("unk5272");
            var SenderLen = 0u;
            if (hasSenderName)
                SenderLen = packet.ReadBits("SenderLen", 11);

            packet.ReadBit("!unk48*4");
            // unk52*4 = 0;
            var hasChannelName = !packet.ReadBit("!hasChannelName");
            packet.ReadBit("unk5273*4");
            var has1494 = !packet.ReadBit("!unk1494h"); // dword ptr [esi+1494h] = ("has unk1494h") ? 0 : ds:dword_D26EA8
            var chatTag = !packet.ReadBit("!chatTag"); // 5264*2
            var hasRealmID1 = !packet.ReadBit("!hasRealmID1"); // 56

            var guid4 = packet.StartBitStream(0, 1, 5, 4, 3, 2, 6, 7);

            if (chatTag)
                packet.ReadBits("chatTag", 9); // 5264*2

            packet.ReadBit("!unk40*4");
            // unk44*4 = 0
            var guid3 = packet.StartBitStream(7, 6, 1, 4, 0, 2, 3, 5);
            packet.ReadBit("!unk24*4");
            // unk28*4 = 0
            var hasLanguage = !packet.ReadBit("!hasLanguage"); // 17
            var hasPrefix = !packet.ReadBit("!hasPrefix");

            var guid = packet.StartBitStream(0, 3, 7, 2, 1, 5, 4, 6);

            var hasAchievementID = !packet.ReadBit("!hasAchievementID"); // 5260*4
            var hasMessage = !packet.ReadBit("!hasMessage"); // 2259

            var ChannelLen = 0u;
            if (hasChannelName)
                ChannelLen = packet.ReadBits("ChannelLen", 7); // 2134

            var MessageLen = 0u;
            if (hasMessage)
                MessageLen = packet.ReadBits("MessageLen", 12);

            var hasReceiverName = !packet.ReadBit("!hasReceiverName"); // 1089

            var PrefixLen = 0u;
            if (hasPrefix)
                PrefixLen = packet.ReadBits("PrefixLen", 5); // 2114*4

            var hasRealmID2 = !packet.ReadBit("!hasRealmID2"); // 60

            var ReceiverLen = 0u;
            if (hasReceiverName)
                ReceiverLen = packet.ReadBits("ReceiverLen", 11);

            packet.ReadBit("!unk32*4");
            // unk36 = 0

            var guid2 = packet.StartBitStream(2, 5, 7, 4, 0, 1, 3, 6);
            packet.ParseBitStream(guid2, 4, 5, 7, 3, 2, 6, 0, 1);
            packet.WriteGuid("Guid2", guid2);

            if (hasChannelName)
                packet.ReadWoWString("ChannelName", ChannelLen);

            if (hasPrefix)
                packet.ReadWoWString("Prefix", PrefixLen);

            if (has1494)
                packet.ReadSingle("unk1494");

            packet.ParseBitStream(guid, 4, 7, 1, 5, 0, 6, 2, 3);
            packet.WriteGuid("Sender", guid);

            var unitGuid = new Guid(BitConverter.ToUInt64(guid, 0));

            if (unitGuid.GetObjectType() == ObjectType.Unit)
                entry = unitGuid.GetEntry();

            text.Type = packet.ReadEnum<ChatMessageType>("Type", TypeCode.Byte);

            if (hasAchievementID)
                packet.ReadUInt32("AchievementID");

            packet.ParseBitStream(guid4, 1, 3, 4, 6, 0, 2, 5, 7);
            packet.WriteGuid("Guid4", guid4);

            packet.ParseBitStream(guid3, 2, 5, 3, 6, 7, 4, 1, 0);
            packet.WriteGuid("Target", guid3);

            if (hasLanguage)
                packet.ReadByte("Language");

            if (hasRealmID2)
                packet.ReadInt32("RealmID2"); // 60

            if (hasMessage)
                text.Text = packet.ReadWoWString("Message", MessageLen);

            if (hasReceiverName)
                text.Text += " -> " + packet.ReadWoWString("Receiver", ReceiverLen);

            if (hasSenderName)
                text.Comment = packet.ReadWoWString("Sender", SenderLen);

            if (hasRealmID1)
                packet.ReadInt32("RealmID1"); // 56

            if (entry != 0)
                Storage.CreatureTexts.Add(entry, text, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_NOTIFICATION)]
        public static void HandleNotification(Packet packet)
        {
            var length = packet.ReadBits(12);
            packet.ReadWoWString("Message", length);
        }

        [Parser(Opcode.SMSG_ZONE_UNDER_ATTACK)]
        public static void HandleZoneUnderAttack(Packet packet)
        {
            packet.ReadEntry<UInt32>(StoreNameType.Zone, "Zone Id");
        }
    }
}
