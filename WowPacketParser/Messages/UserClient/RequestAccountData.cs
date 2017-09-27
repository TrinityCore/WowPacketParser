using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct RequestAccountData
    {
        public byte DataType;

        [Parser(Opcode.CMSG_REQUEST_ACCOUNT_DATA, ClientVersionBuild.Zero, ClientVersionBuild.V5_4_8_18291)]
        public static void HandleRequestAccountData(Packet packet)
        {
            packet.ReadInt32E<AccountDataType>("Data Type");
        }

        [Parser(Opcode.CMSG_REQUEST_ACCOUNT_DATA, ClientVersionBuild.V5_4_8_18291, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleRequestAccountData548(Packet packet)
        {
            packet.ReadBitsE<AccountDataType>("Data Type", 3);
        }

        [Parser(Opcode.CMSG_REQUEST_ACCOUNT_DATA, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleRequestAccountData602(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadBitsE<AccountDataType>("Data Type", 3);
        }
    }
}
