using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct SetActiveVoiceChannel
    {
        public byte ChannelType;
        public string ChannelName;

        [Parser(Opcode.CMSG_SET_ACTIVE_VOICE_CHANNEL)]
        public static void HandleSetActiveVoiceChannel(Packet packet)
        {
            packet.ReadByte("ChannelType");
            var bits108 = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", bits108);
        }
    }
}
