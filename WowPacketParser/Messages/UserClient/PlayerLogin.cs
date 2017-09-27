using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Messages.Submessages;
using System;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct PlayerLogin
    {
        public CliClientSettings ClientSettings;
        public static WowGuid PlayerGUID;

        public static object CoreParsers { get; private set; }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandlePlayerLogin(Packet packet)
        {
            var guid = packet.ReadGuid("GUID");
            PlayerGUID = guid;
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandlePlayerLogin422(Packet packet)
        {
            var guid = packet.StartBitStream(0, 4, 7, 1, 3, 2, 5, 6);
            packet.ParseBitStream(guid, 5, 0, 3, 4, 7, 2, 6, 1);
            packet.WriteGuid("Guid", guid);
            PlayerGUID = new WowGuid64(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_3_15354)]
        public static void HandlePlayerLogin430(Packet packet)
        {
            var guid = packet.StartBitStream(0, 5, 3, 4, 7, 6, 2, 1);
            packet.ParseBitStream(guid, 4, 1, 7, 2, 6, 5, 3, 0);

            packet.WriteGuid("Guid", guid);
            PlayerGUID = new WowGuid64(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V4_3_3_15354, ClientVersionBuild.V4_3_4_15595)]
        public static void HandlePlayerLogin433(Packet packet)
        {
            var guid = packet.StartBitStream(6, 7, 4, 5, 0, 1, 3, 2);
            packet.ParseBitStream(guid, 1, 4, 7, 2, 3, 6, 0, 5);
            packet.WriteGuid("Guid", guid);
            PlayerGUID = new WowGuid64(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_1_0_16309)]
        public static void HandlePlayerLogin434(Packet packet)
        {
            var guid = packet.StartBitStream(2, 3, 0, 6, 4, 5, 1, 7);
            packet.ParseBitStream(guid, 2, 7, 0, 3, 5, 6, 1, 4);
            packet.WriteGuid("Guid", guid);
            PlayerGUID = new WowGuid64(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V5_1_0_16309, ClientVersionBuild.V5_3_0_16981)]
        public static void HandlePlayerLogin510(Packet packet)
        {
            var guid = packet.StartBitStream(1, 5, 0, 2, 7, 6, 3, 4);
            packet.ParseBitStream(guid, 6, 4, 3, 5, 0, 2, 7, 1);
            packet.WriteGuid("Guid", guid);
            packet.ReadSingle("Unk Float");
            PlayerGUID = new WowGuid64(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V5_3_0_16981, ClientVersionBuild.V5_4_0_17359)]
        public static void HandlePlayerLogin530(Packet packet)
        {
            packet.ReadSingle("Unk Float");
            var guid = packet.StartBitStream(3, 4, 0, 6, 7, 1, 2, 5);
            packet.ParseBitStream(guid, 0, 3, 7, 6, 1, 2, 4, 5);
            PlayerGUID = new WowGuid64(BitConverter.ToUInt64(guid, 0));
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V5_4_0_17359, ClientVersionBuild.V5_4_1_17538)]
        public static void HandlePlayerLogin540(Packet packet)
        {
            packet.ReadSingle("Unk Float");
            var guid = packet.StartBitStream(2, 3, 7, 4, 0, 1, 5, 6);
            packet.ParseBitStream(guid, 0, 1, 3, 4, 7, 6, 2, 5);
            PlayerGUID = new WowGuid64(BitConverter.ToUInt64(guid, 0));
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V5_4_1_17538, ClientVersionBuild.V5_4_2_17658)]
        public static void HandlePlayerLogin541(Packet packet)
        {
            packet.ReadSingle("Unk Float");
            var guid = packet.StartBitStream(6, 7, 1, 5, 2, 4, 3, 0);
            packet.ParseBitStream(guid, 7, 6, 0, 1, 4, 3, 2, 5);
            PlayerGUID = new WowGuid64(BitConverter.ToUInt64(guid, 0));
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V5_4_2_17658, ClientVersionBuild.V5_4_7_17898)]
        public static void HandlePlayerLogin542(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadSingle("Unk Float");

            packet.StartBitStream(guid, 7, 2, 5, 4, 3, 0, 6, 1);
            packet.ParseBitStream(guid, 7, 1, 5, 0, 3, 6, 2, 4);

            PlayerGUID = new WowGuid64(BitConverter.ToUInt64(guid, 0));
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_8_18291)]
        public static void HandlePlayerLogin547(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadSingle("Unk Float");

            packet.StartBitStream(guid, 7, 6, 0, 4, 5, 2, 3, 1);
            packet.ParseBitStream(guid, 5, 0, 1, 6, 7, 2, 3, 4);

            PlayerGUID = new WowGuid64(BitConverter.ToUInt64(guid, 0));
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V5_4_8_18291, ClientVersionBuild.V6_0_2_19033)]
        public static void HandlePlayerLogin548(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadSingle("Unk Float");

            packet.StartBitStream(guid, 1, 4, 7, 3, 2, 6, 5, 0);
            packet.ParseBitStream(guid, 5, 1, 0, 6, 2, 4, 7, 3);

            PlayerGUID = new WowGuid64(BitConverter.ToUInt64(guid, 0));
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN)]
        public static void HandlePlayerLogin602(Packet packet)
        {
            var guid = packet.ReadPackedGuid128("Guid");
            readClientSettings(packet, "ClientSettings");
            PlayerGUID = guid;
        }

        private static void readClientSettings(Packet packet, params object[] idx)
        {
            packet.ReadSingle("FarClip", idx);
        }

    }
}
