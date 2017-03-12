using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct VirtualRealmNameInfo
    {
        public bool IsLocal;
        public string RealmNameActual;
        public string RealmNameNormalized;
    }
}
