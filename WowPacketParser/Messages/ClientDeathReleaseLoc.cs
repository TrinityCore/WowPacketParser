using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDeathReleaseLoc
    {
        public Vector3 Loc;
        public int MapID;
    }
}
