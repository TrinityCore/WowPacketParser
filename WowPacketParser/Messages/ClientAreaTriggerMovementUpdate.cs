using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAreaTriggerMovementUpdate
    {
        public Vector3 Start;
        public Vector3 End;
    }
}
