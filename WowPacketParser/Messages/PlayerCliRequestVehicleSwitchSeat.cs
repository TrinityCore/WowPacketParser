using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliRequestVehicleSwitchSeat
    {
        public ulong Vehicle;
        public byte SeatIndex;
    }
}
