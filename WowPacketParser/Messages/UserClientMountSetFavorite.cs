using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientMountSetFavorite
    {
        public int MountSpellID;
        public bool IsFavorite;
    }
}
