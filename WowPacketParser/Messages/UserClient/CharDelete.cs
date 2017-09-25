using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct CharDelete
    {
        public ulong Guid;
        
        [Parser(Opcode.CMSG_CHAR_DELETE, ClientVersionBuild.Zero)]
        public static void HandleClientCharDelete(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_CHAR_DELETE, ClientVersionBuild.V5_3_0_16981, ClientVersionBuild.V5_4_1_17538)]
        public static void HandleClientCharDelete530(Packet packet)
        {
            var playerGuid = new byte[8];

            playerGuid[2] = packet.ReadBit();
            playerGuid[1] = packet.ReadBit();
            playerGuid[5] = packet.ReadBit();
            playerGuid[7] = packet.ReadBit();
            playerGuid[6] = packet.ReadBit();

            packet.ReadBit();

            playerGuid[3] = packet.ReadBit();
            playerGuid[0] = packet.ReadBit();
            playerGuid[4] = packet.ReadBit();

            packet.ReadXORByte(playerGuid, 1);
            packet.ReadXORByte(playerGuid, 3);
            packet.ReadXORByte(playerGuid, 4);
            packet.ReadXORByte(playerGuid, 0);
            packet.ReadXORByte(playerGuid, 7);
            packet.ReadXORByte(playerGuid, 2);
            packet.ReadXORByte(playerGuid, 5);
            packet.ReadXORByte(playerGuid, 6);

            packet.WriteGuid("GUID", playerGuid);
        }

        [Parser(Opcode.CMSG_CHAR_DELETE, ClientVersionBuild.V5_4_1_17538, ClientVersionBuild.V5_4_2_17658)]
        public static void HandleClientCharDelete541(Packet packet)
        {
            var playerGuid = new byte[8];

            playerGuid[1] = packet.ReadBit();
            playerGuid[4] = packet.ReadBit();
            playerGuid[7] = packet.ReadBit();
            playerGuid[5] = packet.ReadBit();
            playerGuid[3] = packet.ReadBit();
            playerGuid[2] = packet.ReadBit();
            playerGuid[0] = packet.ReadBit();
            playerGuid[6] = packet.ReadBit();

            packet.ParseBitStream(playerGuid, 2, 0, 4, 1, 5, 3, 7, 6);

            var guid = new WowGuid64(BitConverter.ToUInt64(playerGuid, 0));
            packet.WriteGuid("GUID", playerGuid);
        }

        [Parser(Opcode.CMSG_CHAR_DELETE, ClientVersionBuild.V5_4_2_17658, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleClientCharDelete542(Packet packet)
        {
            var playerGuid = new byte[8];

            packet.StartBitStream(playerGuid, 7, 0, 1, 3, 5, 2, 4, 6);
            packet.ParseBitStream(playerGuid, 6, 7, 5, 0, 4, 2, 3, 1);

            packet.WriteGuid("GUID", playerGuid);
        }

        [Parser(Opcode.CMSG_CHAR_DELETE, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleClientCharDelete547(Packet packet)
        {
            var playerGuid = new byte[8];

            packet.StartBitStream(playerGuid, 6, 4, 5, 1, 7, 3, 2, 0);
            packet.ParseBitStream(playerGuid, 1, 2, 3, 4, 0, 7, 6, 5);

            packet.WriteGuid("GUID", playerGuid);
        }


        [Parser(Opcode.CMSG_CHAR_DELETE, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleClientCharDelete602(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

    }
}
