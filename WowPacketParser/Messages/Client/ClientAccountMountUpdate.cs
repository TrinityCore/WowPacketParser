using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAccountMountUpdate
    {
        public List<int> MountSpellIDs;
        public List<bool> MountIsFavorite;
        public bool IsFullUpdate;
    }
}
