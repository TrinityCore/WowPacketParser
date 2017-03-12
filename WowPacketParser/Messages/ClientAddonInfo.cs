using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAddonInfo
    {
        public List<BannedAddonInfo> BannedAddons;
        public List<AddonInfo> Addons;
    }
}
