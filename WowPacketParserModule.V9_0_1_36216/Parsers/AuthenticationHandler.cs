using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class AuthenticationHandler
    {
        [Parser(Opcode.SMSG_ENTER_ENCRYPTED_MODE, ClientVersionBuild.V9_2_7_45114)]
        public static void HandleEnterEncryptedMode(Packet packet)
        {
            packet.ReadBytes("Signature (ED25519)", 64);
            packet.ReadBit("Enabled");
        }
    }
}
