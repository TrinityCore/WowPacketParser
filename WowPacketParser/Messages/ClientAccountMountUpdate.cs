using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAccountMountUpdate
    {
        public List<int> MountSpellIDs;
        public List<bool> MountIsFavorite;
        public bool IsFullUpdate;
    }
}
