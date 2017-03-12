using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientLoadingScreenNotify
    {
        public int MapID;
        public bool Showing;
    }
}
