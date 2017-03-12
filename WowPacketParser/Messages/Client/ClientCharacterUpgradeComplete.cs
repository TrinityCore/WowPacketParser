using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCharacterUpgradeComplete
    {
        public List<int> Items;
        public ulong CharacterGUID;
    }
}
