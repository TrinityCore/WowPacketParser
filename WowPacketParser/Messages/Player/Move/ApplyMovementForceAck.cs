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
            MovementHelper.ReadMovementAck(packet);
            MovementHelper.ReadMovementForce(packet, "MovementForce");
        }
    }
}
