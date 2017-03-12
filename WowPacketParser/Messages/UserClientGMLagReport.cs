using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientGMLagReport
    {
        public int MapID;
        public int LagKind;
        public Vector3 Loc;
    }
}
