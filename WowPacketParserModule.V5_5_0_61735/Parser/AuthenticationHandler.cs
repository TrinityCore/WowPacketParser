using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class AuthenticationHandler
    {
        [Parser(Opcode.SMSG_AUTH_CHALLENGE)]
        public static void HandleServerAuthChallenge(Packet packet)
        {
            for (uint i = 0; i < 8; ++i)
                packet.ReadUInt32("DosChallenge", i);
            packet.ReadBytes("Challenge", 32);
            packet.ReadByte("DosZeroBits");
        }

        [Parser(Opcode.CMSG_AUTH_CONTINUED_SESSION)]
        public static void HandleRedirectAuthProof(Packet packet)
        {
            packet.ReadInt64("DosResponse");
            packet.ReadInt64("Key");
            packet.ReadBytes("LocalChallenge", 32);
            packet.ReadBytes("Digest", 24);
        }

        [Parser(Opcode.CMSG_AUTH_SESSION)]
        public static void HandleAuthSession(Packet packet)
        {
            packet.ReadUInt64("DosResponse");
            packet.ReadUInt32("RegionID");
            packet.ReadUInt32("BattlegroupID");
            packet.ReadUInt32("RealmID");
            packet.ReadBytes("LocalChallenge", 32);
            packet.ReadBytes("Digest", 24);
            packet.ReadBit("UseIPv6");

            var realmJoinTicketSize = packet.ReadInt32();
            packet.ReadBytes("RealmJoinTicket", realmJoinTicketSize);
        }

        [Parser(Opcode.SMSG_ENTER_ENCRYPTED_MODE)]
        public static void HandleEnterEncryptedMode(Packet packet)
        {
            packet.ReadInt32("RegionGroup");
            packet.ReadBytes("Signature (ED25519)", 64);
            packet.ReadBit("Enabled");
        }

        [Parser(Opcode.SMSG_SUSPEND_COMMS)]
        public static void HandleSuspendCommsPackets(Packet packet)
        {
            packet.ReadInt32("Serial");
        }

        [Parser(Opcode.SMSG_CONNECT_TO)]
        public static void HandleRedirectClient(Packet packet)
        {
            packet.ReadBytes("Where (RSA encrypted)", 256);

            AddressType type = packet.ReadByteE<AddressType>("Type");
            switch (type)
            {
                case AddressType.IPv4:
                    packet.ReadIPAddress("Address");
                    break;
                case AddressType.IPv6:
                    packet.ReadIPv6Address("Address");
                    break;
                case AddressType.NamedSocket:
                    packet.ReadWoWString("Address", 128);
                    break;
                default:
                    break;
            }

            packet.ReadUInt16("Port");
            packet.ReadUInt32E<ConnectToSerial>("Serial");
            packet.ReadByte("Con");
            packet.ReadUInt64("Key");
        }

        [Parser(Opcode.SMSG_RESUME_COMMS)]
        public static void HandleZeroLengthPackets(Packet packet)
        {
        }
    }
}
