using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class ChannelHandler
    {
        [Parser(Opcode.CMSG_JOIN_CHANNEL)]
        public static void HandleChannelJoin(Packet packet)
        {
            packet.ReadInt32("Channel Id");
            packet.ReadBit("CreateVoiceSession");
            packet.ReadBit("Internal");

            var channelLength = packet.ReadBits(7);
            var passwordLength = packet.ReadBits(7);

            packet.ResetBitReader();

            packet.ReadWoWString("ChannelName", channelLength);
            packet.ReadWoWString("Password", passwordLength);
        }

        [Parser(Opcode.SMSG_CHANNEL_NOTIFY_JOINED)]
        public static void HandleChannelNotify(Packet packet)
        {
            var bits544 = packet.ReadBits(7);
            var bits24 = packet.ReadBits(10);

            packet.ReadByte("ChannelFlags");
            packet.ReadInt32("ChatChannelID");
            packet.ReadInt64("InstanceID");

            packet.ReadWoWString("Channel", bits544);
            packet.ReadWoWString("ChannelWelcomeMsg", bits24);
        }
    }
}
