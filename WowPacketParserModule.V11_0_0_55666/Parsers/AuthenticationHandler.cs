using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
{
    public static class AuthenticationHandler
    {
        [Parser(Opcode.SMSG_AUTH_CHALLENGE)]
        public static void HandleServerAuthChallenge(Packet packet)
        {
            for (uint i = 0; i < 8; ++i)
                packet.ReadUInt32("DosChallenge", i);
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V11_0_7_58123))
                packet.ReadBytes("Challenge", 16);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_7_58123))
                packet.ReadBytes("Challenge", 32);
            packet.ReadByte("DosZeroBits");
        }

        [Parser(Opcode.CMSG_AUTH_CONTINUED_SESSION)]
        public static void HandleRedirectAuthProof(Packet packet)
        {
            packet.ReadInt64("DosResponse");
            packet.ReadInt64("Key");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V11_0_7_58123))
                packet.ReadBytes("LocalChallenge", 16);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_7_58123))
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
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V11_0_7_58123))
                packet.ReadBytes("LocalChallenge", 16);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_7_58123))
                packet.ReadBytes("LocalChallenge", 32);
            packet.ReadBytes("Digest", 24);
            packet.ReadBit("UseIPv6");

            var realmJoinTicketSize = packet.ReadInt32();
            packet.ReadBytes("RealmJoinTicket", realmJoinTicketSize);
        }
    }
}
