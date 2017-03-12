using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQueryGameObjectResponse
    {
        public uint GameObjectID;
        public bool Allow;
        public Data Stats;
    }
}
