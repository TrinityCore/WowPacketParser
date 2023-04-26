using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class MovementHandler
    {
        [Parser(Opcode.SMSG_MOVE_SET_CAN_ADV_FLY)]
        [Parser(Opcode.SMSG_MOVE_UNSET_CAN_ADV_FLY)]
        public static void HandleMoveCanADVFly(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("SequenceIndex");
        }

        [Parser(Opcode.CMSG_MOVE_SET_ADV_FLY)]
        public static void HandleMoveSetADVFly(Packet packet)
        {
            var stats = Substructures.MovementHandler.ReadMovementStats(packet);
            packet.Holder.ClientMove = new() { Mover = stats.MoverGuid, Position = stats.PositionAsVector4 }; ;
        }

        [Parser(Opcode.CMSG_MOVE_SET_CAN_ADV_FLY_ACK)]
        public static void HandleMoveSetCanADVFlyACK(Packet packet)
        {
            var stats = V6_0_2_19033.Parsers.MovementHandler.ReadMovementAck(packet, "MovementAck");
            packet.Holder.ClientMove = new() { Mover = stats.MoverGuid, Position = stats.PositionAsVector4 };
        }
    }
}
