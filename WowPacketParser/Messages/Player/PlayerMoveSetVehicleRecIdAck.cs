using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveSetVehicleRecIdAck
    {
        public int VehicleRecID;
        public CliMovementAck Data;
    }
}
