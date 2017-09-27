using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct VoiceSessionEnable
    {
        public bool EnableVoiceChat;
        public bool EnableMicrophone;

        [Parser(Opcode.CMSG_VOICE_SESSION_ENABLE, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleVoiceSessionEnable(Packet packet)
        {
            packet.ReadBool("Voice Enabled");
            packet.ReadBool("Microphone Enabled");
        }

        [Parser(Opcode.CMSG_VOICE_SESSION_ENABLE, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleVoiceSessionEnable602(Packet packet)
        {
            packet.ReadBit("EnableVoiceChat");
            packet.ReadBit("EnableMicrophone");
        }
    }
}
