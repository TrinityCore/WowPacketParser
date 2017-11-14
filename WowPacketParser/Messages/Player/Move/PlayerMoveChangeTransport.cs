using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Messages.Player.Move;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveChangeTransport
    {
        public CliMovementStatus Status;[Parser(Opcode.CMSG_MOVE_CHANGE_TRANSPORT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_MOVE_NOT_ACTIVE_MOVER, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_DISMISS_CONTROLLED_VEHICLE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMovementMessages(Packet packet)
        {
            WowGuid guid;
            if ((ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192) ||
                packet.Direction == Direction.ServerToClient) && ClientVersion.Build != ClientVersionBuild.V4_2_2_14545)
                guid = packet.ReadPackedGuid("Guid");
            else
                guid = new WowGuid64();

            MovementHelper.ReadMovementInfo(packet, guid);

            if (packet.Opcode != Opcodes.GetOpcode(Opcode.MSG_MOVE_KNOCK_BACK, Direction.Bidirectional))
                return;

            packet.ReadSingle("Sin Angle");
            packet.ReadSingle("Cos Angle");
            packet.ReadSingle("Speed");
            packet.ReadSingle("Velocity");
        }
    }
}
