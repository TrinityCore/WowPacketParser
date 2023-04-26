using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class MovementHandler
    {
        [Parser(Opcode.CMSG_MOVE_SET_ADV_FLYING_PITCHING_RATE_DOWN_ACK)]
        [Parser(Opcode.CMSG_MOVE_SET_ADV_FLYING_PITCHING_RATE_UP_ACK)]
        [Parser(Opcode.CMSG_MOVE_SET_ADV_FLYING_TURN_VELOCITY_THRESHOLD_ACK)]
        [Parser(Opcode.CMSG_MOVE_SET_ADV_FLYING_BANKING_RATE_ACK)]
        public static void HandleMovementSetADV(Packet packet)
        {
            var stats = V6_0_2_19033.Parsers.MovementHandler.ReadMovementAck(packet, "MovementAck");
            packet.Holder.ClientMove = new() { Mover = stats.MoverGuid, Position = stats.PositionAsVector4 };

            packet.ReadSingle("UnkFloat");
            packet.ReadSingle("UnkFloat");
        }
    }
}
