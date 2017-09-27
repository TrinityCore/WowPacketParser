using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct VoiceAddIgnore
    {
        public string OffenderName;

        [Parser(Opcode.CMSG_VOICE_ADD_IGNORE)]
        public static void HandleAddVoiceIgnore(Packet packet)
        {
            packet.ReadCString("Name");
        }
    }
}
