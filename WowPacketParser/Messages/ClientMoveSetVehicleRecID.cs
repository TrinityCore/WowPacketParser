using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveSetVehicleRecID
    {
        public ulong MoverGUID;
        public uint SequenceIndex;
        public int VehicleRecID;
    }
}
