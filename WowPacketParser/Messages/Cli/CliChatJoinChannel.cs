using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliChatJoinChannel
    {
        public string Password;
        public string ChannelName;
        public bool CreateVoiceSession;
        public int ChatChannelId;
        public bool Internal;

        [Parser(Opcode.CMSG_CHAT_JOIN_CHANNEL, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleChannelJoin(Packet packet)
        {
            packet.ReadInt32("Channel Id");
            packet.ReadBool("Has Voice");
            packet.ReadBool("Joined by zone update");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                packet.ReadCString("Channel Pass");
                packet.ReadCString("Channel Name");
            }
            else
            {
                packet.ReadCString("Channel Name");
                packet.ReadCString("Channel Pass");
            }
        }

        [Parser(Opcode.CMSG_CHAT_JOIN_CHANNEL, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleChannelJoin434(Packet packet)
        {
            packet.ReadInt32("Channel Id");
            packet.ReadBit("Has Voice");
            packet.ReadBit("Joined by zone update");
            var channelLength = packet.ReadBits(8);
            var passwordLength = packet.ReadBits(8);
            packet.ReadWoWString("Channel Name", channelLength);
            packet.ReadWoWString("Channel Pass", passwordLength);
        }

        [Parser(Opcode.CMSG_CHAT_JOIN_CHANNEL, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleChannelJoin547(Packet packet)
        {
            packet.ReadInt32("Channel Id");
            var passwordLength = packet.ReadBits(7);
            packet.ReadBit("Joined by zone update");
            var channelLength = packet.ReadBits(7);
            packet.ReadBit("Has Voice");
            packet.ReadWoWString("Channel Pass", passwordLength);
            packet.ReadWoWString("Channel Name", channelLength);
        }

        [Parser(Opcode.CMSG_CHAT_JOIN_CHANNEL, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleChannelJoin602(Packet packet)
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
    }
}
