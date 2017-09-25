using System.Collections.Generic;

namespace WowPacketParser.Messages.UserClient.BattlePet
{
    public unsafe struct SetLevel
    {
        public List<ulong> BattlePetGUIDs;
        public ushort Level;
    }
}
