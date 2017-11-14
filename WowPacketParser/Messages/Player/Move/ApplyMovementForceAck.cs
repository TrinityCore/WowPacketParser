using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Player.Move
{
    public unsafe struct ApplyMovementForceAck
    {
        public int TransportID;
        public Vector2 Direction;
        public uint MovementForceID;
        public CliMovementAck Data;

        [Parser(Opcode.CMSG_MOVE_APPLY_MOVEMENT_FORCE_ACK)]
        public static void HandleMoveApplyMovementForceAck(Packet packet)
        {
            ReadMovementAck(packet);
            ReadMovementForce(packet, "MovementForce");
        }

        public static void ReadMovementForce(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("ID", idx);
            packet.ReadVector3("Direction", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_2_19802)) // correct?
                packet.ReadVector3("TransportPosition", idx);
            packet.ReadInt32("TransportID", idx);
            packet.ReadSingle("Magnitude", idx);

            packet.ResetBitReader();

            packet.ReadBits("Type", 2, idx);
        }

        public static void ReadMovementAck(Packet packet)
        {
            ReadMovementStats(packet);
            packet.ReadInt32("AckIndex");
        }
    }
}
