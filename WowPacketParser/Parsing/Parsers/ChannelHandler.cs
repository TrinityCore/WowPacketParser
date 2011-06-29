using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class ChannelHandler
    {
        [Parser(Opcode.CMSG_CHANNEL_VOICE_ON)]
        [Parser(Opcode.CMSG_CHANNEL_VOICE_OFF)]
        public static void HandleChannelSetVoice(Packet packet)
        {
            var chanName = packet.ReadCString();
            Console.WriteLine("Channel Name: " + chanName);
        }

        [Parser(Opcode.CMSG_CHANNEL_SILENCE_VOICE)]
        [Parser(Opcode.CMSG_CHANNEL_UNSILENCE_VOICE)]
        [Parser(Opcode.CMSG_CHANNEL_SILENCE_ALL)]
        [Parser(Opcode.CMSG_CHANNEL_UNSILENCE_ALL)]
        public static void HandleChannelSilencing(Packet packet)
        {
            var chanName = packet.ReadCString();
            Console.WriteLine("Channel Name: " + chanName);

            var playerName = packet.ReadCString();
            Console.WriteLine("Player Name: " + playerName);
        }
    }
}
