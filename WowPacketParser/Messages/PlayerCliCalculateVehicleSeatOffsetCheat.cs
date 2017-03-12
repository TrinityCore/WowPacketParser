using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliCalculateVehicleSeatOffsetCheat
    {
        public ulong PassengerGUID;
        public Vector3 PassengerRawPos;
    }
}
