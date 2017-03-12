using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerMoveChangeVehicleSeats
    {
        public ulong DstVehicle;
        public CliMovementStatus Status;
        public byte DstSeatIndex;
    }
}
