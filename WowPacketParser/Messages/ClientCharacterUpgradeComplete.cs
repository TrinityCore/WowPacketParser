using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCharacterUpgradeComplete
    {
        public List<int> Items;
        public ulong CharacterGUID;
    }
}
