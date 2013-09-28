using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;
using ChatMessageType540 = WowPacketParserModule.V5_4_0_17359.Enums.ChatMessageType;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.SMSG_DEFENSE_MESSAGE)]
        public static void HandleDefenseMessage(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Zone, "Zone Id");
            var len = packet.ReadBits(12);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHANNEL_LIST)]
        public static void HandleChannelList(Packet packet)
        {
            packet.ReadUInt32("Flags");
            packet.ReadBit();
            var length = packet.ReadBits("", 7);
            packet.ReadBit();
            packet.ReadBits("HasPassword", 7);

            packet.ReadWoWString("Channel Name", length);
        }

        [Parser(Opcode.SMSG_MESSAGECHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            var text = new CreatureText();

            var SenderGUID = new byte[8];
            var GuildGUID = new byte[8];
            var ReceiverGUID = new byte[8];
            var GroupGUID = new byte[8];

            var bit1495 = packet.ReadBit();

            var hasText = !packet.ReadBit();
            var hasAchi = !packet.ReadBit();
            var hasSender = !packet.ReadBit();

            packet.ReadBit(); // fake bit

            packet.StartBitStream(SenderGUID, 2, 4, 0, 6, 1, 3, 5, 7);

            packet.ReadBit(); // fake bit

            packet.StartBitStream(GroupGUID, 6, 0, 4, 1, 2, 3, 7, 5);

            var hasPrefix = !packet.ReadBit();
            var bit1494 = packet.ReadBit();
            var hasRealmId = !packet.ReadBit();
            var bit1490 = !packet.ReadBit();

            int senderName = 0;
            if (hasSender)
                senderName = (int)packet.ReadBits(11);

            packet.ReadBit(); // fake bit

            packet.StartBitStream(ReceiverGUID, 4, 0, 6, 7, 5, 1, 3, 2);

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

            packet.StartBitStream(GuildGUID, 0, 2, 1, 4, 6, 7, 5, 3);

            var hasChannel = !packet.ReadBit();

            int channelLen = 0;
            if (hasChannel)
            {
                channelLen = (int)packet.ReadBits(7);
                packet.ReadWoWString("Channel Name", channelLen);
            }

            if (hasSender)
                text.Comment = packet.ReadWoWString("Sender Name", senderName);

            packet.ParseBitStream(GroupGUID, 6, 7, 1, 2, 4, 3, 0, 5);

            packet.ParseBitStream(ReceiverGUID, 0, 4, 1, 3, 5, 7, 2, 6);

            text.Type = (ChatMessageType)packet.ReadEnum<ChatMessageType540>("Chat type", TypeCode.Byte);

            packet.ParseBitStream(SenderGUID, 7, 6, 5, 4, 0, 2, 1, 3);

            if (hasPrefix)
                packet.ReadWoWString("Addon Message Prefix", prefixLen);

            if (hasRealmId)
                packet.ReadInt32("Realm Id");

            packet.ParseBitStream(GuildGUID, 1, 0, 3, 7, 6, 5, 2, 4);

            if (hasReceiver)
                packet.ReadWoWString("Receiver Name", receiverLen);

            if (hasAchi)
                packet.ReadInt32("Achievement");

            if (hasLang)
                text.Language = packet.ReadEnum<Language>("Language", TypeCode.Byte);

            if (hasText)
                text.Text = packet.ReadWoWString("Text", textLen);

            if (bit1490)
                packet.ReadSingle("Float1490");

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
    }
}
