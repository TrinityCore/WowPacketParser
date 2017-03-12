using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientSetPlayerDeclinedNames
    {
        public ulong Player;
        public string[/*5*/] DeclinedName;
    }
}
