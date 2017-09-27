using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserRouterClient
{
    public unsafe struct AuthContinuedSession
    {
        public ulong Key;
        public ulong DosResponse;
        public fixed byte Digest[20];

        [Parser(Opcode.CMSG_AUTH_CONTINUED_SESSION, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleRedirectionAuthProof(Packet packet)
        {
            packet.ReadCString("Account");
            packet.ReadInt64("Unk Int64"); // Key or DosResponse
            packet.ReadBytes("Proof SHA-1 Hash", 20);
        }

        [Parser(Opcode.CMSG_AUTH_CONTINUED_SESSION, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleRedirectionAuthProof422(Packet packet)
        {
            var bytes = new byte[20];
            bytes[0] = packet.ReadByte();
            bytes[12] = packet.ReadByte();
            bytes[3] = packet.ReadByte();
            bytes[17] = packet.ReadByte();
            bytes[11] = packet.ReadByte();
            bytes[13] = packet.ReadByte();
            bytes[5] = packet.ReadByte();
            bytes[9] = packet.ReadByte();
            bytes[6] = packet.ReadByte();
            bytes[19] = packet.ReadByte();
            bytes[15] = packet.ReadByte();
            bytes[18] = packet.ReadByte();
            bytes[8] = packet.ReadByte();
            packet.ReadInt64("Unk long 1"); // Key or DosResponse
            bytes[2] = packet.ReadByte();
            bytes[1] = packet.ReadByte();
            packet.ReadInt64("Unk long 2"); // Key or DosResponse
            bytes[7] = packet.ReadByte();
            bytes[4] = packet.ReadByte();
            bytes[16] = packet.ReadByte();
            bytes[14] = packet.ReadByte();
            bytes[10] = packet.ReadByte();
            packet.AddValue("Proof RSA Hash", Utilities.ByteArrayToHexString(bytes));
        }

        [Parser(Opcode.CMSG_AUTH_CONTINUED_SESSION, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_4_0_17359)]
        public static void HandleRedirectionAuthProof434(Packet packet)
        {
            var bytes = new byte[20];
            packet.ReadUInt64("Unk Long");
            packet.ReadUInt64("+ 4");
            bytes[5] = packet.ReadByte();
            bytes[2] = packet.ReadByte();
            bytes[6] = packet.ReadByte();
            bytes[10] = packet.ReadByte();
            bytes[8] = packet.ReadByte();
            bytes[17] = packet.ReadByte();
            bytes[11] = packet.ReadByte();
            bytes[15] = packet.ReadByte();
            bytes[7] = packet.ReadByte();
            bytes[1] = packet.ReadByte();
            bytes[4] = packet.ReadByte();
            bytes[16] = packet.ReadByte();
            bytes[0] = packet.ReadByte();
            bytes[12] = packet.ReadByte();
            bytes[14] = packet.ReadByte();
            bytes[13] = packet.ReadByte();
            bytes[18] = packet.ReadByte();
            bytes[9] = packet.ReadByte();
            bytes[19] = packet.ReadByte();
            bytes[3] = packet.ReadByte();
            packet.AddValue("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(bytes));
        }

        [Parser(Opcode.CMSG_AUTH_CONTINUED_SESSION, ClientVersionBuild.V5_4_0_17359, ClientVersionBuild.V5_4_2_17658)]
        public static void HandleRedirectAuthProof540(Packet packet)
        {
            var sha = new byte[20];
            packet.ReadInt64("Int64 Unk1"); // Key or DosResponse
            packet.ReadInt64("Int64 Unk2"); // Key or DosResponse
            sha[0] = packet.ReadByte();
            sha[12] = packet.ReadByte();
            sha[1] = packet.ReadByte();
            sha[4] = packet.ReadByte();
            sha[5] = packet.ReadByte();
            sha[7] = packet.ReadByte();
            sha[18] = packet.ReadByte();
            sha[2] = packet.ReadByte();
            sha[19] = packet.ReadByte();
            sha[8] = packet.ReadByte();
            sha[11] = packet.ReadByte();
            sha[13] = packet.ReadByte();
            sha[14] = packet.ReadByte();
            sha[9] = packet.ReadByte();
            sha[6] = packet.ReadByte();
            sha[3] = packet.ReadByte();
            sha[10] = packet.ReadByte();
            sha[15] = packet.ReadByte();
            sha[17] = packet.ReadByte();
            sha[16] = packet.ReadByte();
            packet.AddValue("SHA-1 Hash", sha);
        }

        [Parser(Opcode.CMSG_AUTH_CONTINUED_SESSION, ClientVersionBuild.V5_4_2_17658, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleRedirectAuthProof542(Packet packet)
        {
            var sha = new byte[20];
            packet.ReadInt64("Int64 Unk1"); // Key or DosResponse
            packet.ReadInt64("Int64 Unk2"); // Key or DosResponse

            sha[10] = packet.ReadByte();
            sha[17] = packet.ReadByte();
            sha[1] = packet.ReadByte();
            sha[4] = packet.ReadByte();
            sha[0] = packet.ReadByte();
            sha[8] = packet.ReadByte();
            sha[2] = packet.ReadByte();
            sha[11] = packet.ReadByte();
            sha[15] = packet.ReadByte();
            sha[6] = packet.ReadByte();
            sha[13] = packet.ReadByte();
            sha[12] = packet.ReadByte();
            sha[3] = packet.ReadByte();
            sha[5] = packet.ReadByte();
            sha[14] = packet.ReadByte();
            sha[19] = packet.ReadByte();
            sha[18] = packet.ReadByte();
            sha[16] = packet.ReadByte();
            sha[9] = packet.ReadByte();
            sha[7] = packet.ReadByte();

            packet.AddValue("SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.CMSG_AUTH_CONTINUED_SESSION, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleRedirectAuthProof547(Packet packet)
        {
            var sha = new byte[20];
            packet.ReadInt64("Int64 Unk1"); // Key or DosResponse
            packet.ReadInt64("Int64 Unk2"); // Key or DosResponse
            sha[1] = packet.ReadByte();
            sha[14] = packet.ReadByte();
            sha[9] = packet.ReadByte();
            sha[18] = packet.ReadByte();
            sha[17] = packet.ReadByte();
            sha[8] = packet.ReadByte();
            sha[6] = packet.ReadByte();
            sha[10] = packet.ReadByte();
            sha[3] = packet.ReadByte();
            sha[16] = packet.ReadByte();
            sha[4] = packet.ReadByte();
            sha[0] = packet.ReadByte();
            sha[15] = packet.ReadByte();
            sha[2] = packet.ReadByte();
            sha[19] = packet.ReadByte();
            sha[12] = packet.ReadByte();
            sha[13] = packet.ReadByte();
            sha[5] = packet.ReadByte();
            sha[11] = packet.ReadByte();
            sha[7] = packet.ReadByte();

            packet.AddValue("SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.CMSG_AUTH_CONTINUED_SESSION, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleRedirectAuthProof(Packet packet)
        {
            packet.ReadInt64("DosResponse");
            packet.ReadInt64("Key");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_4_21315))
            {
                packet.ReadBytes("LocalChallenge", 16);
                packet.ReadBytes("Digest", 24);
            }
            else
                packet.ReadBytes("Digest", 20);
        }
    }
}
