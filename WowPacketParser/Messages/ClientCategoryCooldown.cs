using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCategoryCooldown
    {
        public List<CategoryCooldown> CategoryCooldowns;
    }
}
