using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerMoveSetVehicleRecIdAck
    {
        public int VehicleRecID;
        public CliMovementAck Data;
    }
}
