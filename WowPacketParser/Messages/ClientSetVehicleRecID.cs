using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSetVehicleRecID
    {
        public ulong VehicleGUID;
        public int VehicleRecID;
    }
}
