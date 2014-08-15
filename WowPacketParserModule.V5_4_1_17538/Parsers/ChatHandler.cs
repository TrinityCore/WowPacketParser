using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using ChatMessageType540 = WowPacketParserModule.V5_4_0_17359.Enums.ChatMessageType;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_1_17538.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.SMSG_MESSAGECHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            var text = new CreatureText();

            var SenderGUID = new byte[8];
            var GuildGUID = new byte[8];
            var ReceiverGUID = new byte[8];
            var GroupGUID = new byte[8];

            var hasLang = !packet.ReadBit();
            var hasReceiver = !packet.ReadBit();

            packet.ReadBit(); // fake bit

            packet.StartBitStream(GroupGUID, 3, 7, 2, 6, 0, 4, 5, 1);

            var hasAchi = !packet.ReadBit();
            var bit1495 = packet.ReadBit();
            var hasPrefix = !packet.ReadBit();

            packet.ReadBit(); // fake bit

            var bit43D = !packet.ReadBit();

            packet.StartBitStream(GuildGUID, 1, 6, 0, 5, 2, 4, 7, 3);

            packet.ReadBit(); // fake bit

            packet.StartBitStream(ReceiverGUID, 6, 1, 3, 5, 4, 2, 7, 0);

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

            packet.StartBitStream(SenderGUID, 4, 1, 3, 6, 2, 5, 0, 7);

            var bit148C = !packet.ReadBit();

            var bits148C = 0u;
            if (bit148C)
                bits148C = packet.ReadBits(9);

            var prefixLen = 0u;
            if (hasPrefix)
            {
                prefixLen = packet.ReadBits(5);
                packet.ReadWoWString("Addon Message Prefix", prefixLen);
            }

            packet.ParseBitStream(GroupGUID, 4, 2, 7, 3, 6, 1, 5, 0);

            if (hasReceiver)
                packet.ReadWoWString("Receiver Name", receiverLen);

            packet.ParseBitStream(ReceiverGUID, 7, 4, 1, 3, 0, 6, 5, 2);
            packet.ParseBitStream(GuildGUID, 5, 7, 3, 0, 4, 6, 1, 2);

            if (hasLang)
                text.Language = packet.ReadEnum<Language>("Language", TypeCode.Byte);

            packet.ParseBitStream(SenderGUID, 7, 4, 0, 6, 3, 2, 5, 1);

            if (hasChannel)
                packet.ReadWoWString("Channel Name", channelLen);

            text.Text = packet.ReadWoWString("Text", textLen);

            text.Type = (ChatMessageType)packet.ReadEnum<ChatMessageType540>("Chat type", TypeCode.Byte);

            if (hasAchi)
                packet.ReadInt32("Achievement");

            if (bit1490)
                packet.ReadSingle("Float1490");

            if (hasRealmId)
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
    }
}
