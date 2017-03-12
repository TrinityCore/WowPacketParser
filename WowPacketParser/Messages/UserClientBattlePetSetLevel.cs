using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientBattlePetSetLevel
    {
        public List<ulong> BattlePetGUIDs;
        public ushort Level;
    }
}
