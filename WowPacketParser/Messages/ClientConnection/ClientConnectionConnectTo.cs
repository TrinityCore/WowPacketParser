using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.ClientConnection
{
    public unsafe struct ClientConnectionConnectTo
    {
        public ulong Key;
        public uint Serial;
        public fixed byte Where[0x100]; // (RSA encrypted)
        public byte Con; // 1 == Connecting to world server

        [Parser(Opcode.SMSG_CONNECT_TO, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleRedirectClient(Packet packet)
        {
            packet.ReadIPAddress("IP Address");
            packet.ReadUInt16("Port");
            packet.ReadInt32("Token");
            packet.ReadBytes("Address SHA-1 Hash", 20);
        }

        [Parser(Opcode.SMSG_CONNECT_TO, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleRedirectClient406(Packet packet)
        {
            packet.ReadUInt64("Key");
            packet.ReadBytes("Where", 0x100);
            packet.ReadByte("Con");
            packet.ReadUInt32("Serial");
        }

        [Parser(Opcode.SMSG_CONNECT_TO, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleRedirectClient422(Packet packet)
        {
            packet.ReadBytes("Where", 0x100);
            packet.ReadByte("Con");
            packet.ReadUInt32("Serial");
            packet.ReadUInt64("Key");
        }

        [Parser(Opcode.SMSG_CONNECT_TO, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_4_0_17359)]
        public static void HandleRedirectClient434(Packet packet)
        {
            packet.ReadUInt64("Key");
            packet.ReadUInt32("Serial");
            packet.ReadBytes("Where", 0x100);
            packet.ReadByte("Con");
        }

        [Parser(Opcode.SMSG_CONNECT_TO, ClientVersionBuild.V5_4_0_17359, ClientVersionBuild.V5_4_2_17658)]
        public static void HandleRedirectClient540(Packet packet)
        {
            packet.ReadUInt64("Key");
            packet.ReadByte("Con");
            packet.ReadUInt32("Serial");
            packet.ReadBytes("Where", 0x100);
        }

        [Parser(Opcode.SMSG_CONNECT_TO, ClientVersionBuild.V5_4_2_17658, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleRedirectClient542(Packet packet)
        {
            packet.ReadBytes("Where", 0x100);
            packet.ReadByte("Con");
            packet.ReadUInt32("Serial");
            packet.ReadUInt64("Key");
        }

        [Parser(Opcode.SMSG_CONNECT_TO, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleRedirectClient547(Packet packet)
        {
            packet.ReadUInt64("Key");
            packet.ReadBytes("Where", 0x100);
            packet.ReadByte("Con");
            packet.ReadUInt32("Serial");
        }

        [Parser(Opcode.SMSG_CONNECT_TO, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleRedirectClient602(Packet packet)
        {
            packet.ReadUInt64("Key");
            packet.ReadUInt32("Serial");
            packet.ReadBytes("Where", 0x100);
            packet.ReadByte("Con");
        }
    }
}
