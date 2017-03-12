using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGameObjectActivateAnimKit
    {
        public bool Maintain;
        public ulong ObjectGUID;
        public int AnimKitID;
    }
}
