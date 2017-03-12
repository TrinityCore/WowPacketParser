using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveSetVehicleRecIdAck
    {
        public int VehicleRecID;
        public CliMovementAck Data;
    }
}
