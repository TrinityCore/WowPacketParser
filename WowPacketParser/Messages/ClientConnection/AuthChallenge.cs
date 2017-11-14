using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.ClientConnection 
{
    public unsafe struct AuthChallenge
    {
        public uint Challenge;
        public fixed uint DosChallenge[8];
        public byte DosZeroBits;

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_1a_13205)]
        public static void HandleServerAuthChallenge(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                packet.ReadInt32("Shuffle Count");

            packet.ReadUInt32("Challenge");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
            {
                var stateCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685) ? 8 : 4;
                for (var i = 0; i < stateCount; i++)
                    packet.ReadInt32("DosChallenge", i);
            }
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.V4_0_1a_13205, ClientVersionBuild.V4_0_3_13329)]
        public static void HandleServerAuthChallenge401(Packet packet)
        {
            packet.ReadUInt32("DosChallenge", 2);
            packet.ReadUInt32("DosChallenge", 4);
            packet.ReadByte("DosZeroBits");
            packet.ReadUInt32("Challenge");
            packet.ReadUInt32("DosChallenge", 6);
            packet.ReadUInt32("DosChallenge", 5);
            packet.ReadUInt32("DosChallenge", 0);
            packet.ReadUInt32("DosChallenge", 1);
            packet.ReadUInt32("DosChallenge", 7);
            packet.ReadUInt32("DosChallenge", 3);
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.V4_0_3_13329, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleServerAuthChallenge403(Packet packet)
        {
            packet.ReadUInt32("DosChallenge", 4);
            packet.ReadUInt32("DosChallenge", 7);
            packet.ReadUInt32("Challenge");
            packet.ReadUInt32("DosChallenge", 0);
            packet.ReadByte("DosZeroBits");
            packet.ReadUInt32("DosChallenge", 6);
            packet.ReadUInt32("DosChallenge", 4);
            packet.ReadUInt32("DosChallenge", 2);
            packet.ReadUInt32("DosChallenge", 5);
            packet.ReadUInt32("DosChallenge", 1);
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleServerAuthChallenge422(Packet packet)
        {
            packet.ReadUInt32("DosChallenge", 0);
            packet.ReadUInt32("DosChallenge", 1);
            packet.ReadUInt32("DosChallenge", 2);
            packet.ReadUInt32("DosChallenge", 3);
            packet.ReadUInt32("Challenge");
            packet.ReadByte("DosZeroBits");
            packet.ReadUInt32("DosChallenge", 4);
            packet.ReadUInt32("DosChallenge", 5);
            packet.ReadUInt32("DosChallenge", 6);
            packet.ReadUInt32("DosChallenge", 7);
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_0_5_16048)]
        public static void HandleServerAuthChallenge434(Packet packet)
        {
            for (uint i = 0; i < 8; ++i)
                packet.ReadUInt32("DosChallenge", i);
            packet.ReadInt32("Challenge");
            packet.ReadByte("DosZeroBits");
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.V5_0_5_16048, ClientVersionBuild.V5_3_0_16981)]
        public static void HandleServerAuthChallenge505(Packet packet)
        {
            packet.ReadUInt32("Challenge");
            for (uint i = 0; i < 8; ++i)
                packet.ReadUInt32("DosChallenge", i);
            packet.ReadByte("DosZeroBits");
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.V5_3_0_16981, ClientVersionBuild.V5_4_0_17359)]
        public static void HandleServerAuthChallenge530(Packet packet)
        {
            for (uint i = 0; i < 8; ++i)
                packet.ReadUInt32("DosChallenge", i);
            packet.ReadUInt32("Challenge");
            packet.ReadByte("DosZeroBits");
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.V5_4_0_17359, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleServerAuthChallenge540(Packet packet)
        {
            for (uint i = 0; i < 8; ++i)
                packet.ReadUInt32("DosChallenge", i);
            packet.ReadByte("DosZeroBits");
            packet.ReadUInt32("Challenge");
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleServerAuthChallenge541(Packet packet)
        {
            packet.ReadByte("DosZeroBits");
            for (uint i = 0; i < 8; ++i)
                packet.ReadUInt32("DosChallenge", i);
            packet.ReadUInt32("Challenge");
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleServerAuthChallenge6(Packet packet)
        {
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_4_21315))
                packet.ReadUInt32("Challenge");
            for (uint i = 0; i < 8; ++i)
                packet.ReadUInt32("DosChallenge", i);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_4_21315))
                packet.ReadBytes("Challenge", 16);
            packet.ReadByte("DosZeroBits");
        }
    }
}
