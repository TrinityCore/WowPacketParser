using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
{
    public static class MovementHandler
    {
        [Parser(Opcode.SMSG_MOVE_SET_CAN_DRIVE)]
        public static void HandleMoveSetCanDrive(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("SequenceIndex");
            packet.ReadInt32("DriveCapabilityID");
        }
    }
}
