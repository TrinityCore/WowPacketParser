using System.Collections.Generic;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientBattlePetSetLevel
    {
        public List<ulong> BattlePetGUIDs;
        public ushort Level;
    }
}
