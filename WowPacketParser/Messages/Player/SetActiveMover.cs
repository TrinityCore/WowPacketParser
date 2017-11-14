using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct SetActiveMover
    {
        public ulong ActiveMover;

        [Parser(Opcode.CMSG_SET_ACTIVE_MOVER, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        [Parser(Opcode.SMSG_MOUNT_SPECIAL_ANIM)]
        public static void HandleSetActiveMover(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_MOVER, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSetActiveMover430(Packet packet)
        {
            var guid = packet.StartBitStream(7, 2, 0, 4, 3, 5, 6, 1);
            packet.ParseBitStream(guid, 1, 3, 2, 6, 0, 5, 4, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_ACTIVE_MOVER, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMoveSetActiveMover430(Packet packet)
        {
            var guid = packet.StartBitStream(6, 2, 7, 0, 3, 5, 4, 1);
            packet.ParseBitStream(guid, 3, 5, 6, 7, 2, 0, 1, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_MOVER, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSetActiveMover434(Packet packet)
        {
            var guid = packet.StartBitStream(7, 2, 1, 0, 4, 5, 6, 3);
            packet.ParseBitStream(guid, 3, 2, 4, 0, 5, 1, 6, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MOVE_SET_ACTIVE_MOVER, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_3_0_16981)]
        public static void HandleMoveSetActiveMover434(Packet packet)
        {
            var guid = packet.StartBitStream(5, 7, 3, 6, 0, 4, 1, 2);
            packet.ParseBitStream(guid, 6, 2, 3, 0, 5, 7, 1, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_MOVER, ClientVersionBuild.V5_3_0_16981, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleSetActiveMover530(Packet packet)
        {
            packet.ReadBit("unk");

            var guid = packet.StartBitStream(6, 2, 3, 0, 4, 1, 7, 5);
            packet.ParseBitStream(guid, 5, 1, 0, 2, 6, 3, 7, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_MOVER, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleSetActiveMover547(Packet packet)
        {
            packet.ReadBit("unk");

            var guid = packet.StartBitStream(1, 3, 2, 6, 7, 5, 4, 0);
            packet.ParseBitStream(guid, 5, 1, 7, 2, 6, 3, 4, 0);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_MOVER, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleSetActiveMover602(Packet packet)
        {
            packet.ReadPackedGuid128("ActiveMover");
        }

        [Parser(Opcode.SMSG_MOVE_SET_ACTIVE_MOVER, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleMoveSetActiveMover602(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
        }
    }
}
