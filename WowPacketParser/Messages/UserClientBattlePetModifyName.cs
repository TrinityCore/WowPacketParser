using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientBattlePetModifyName
    {
        public string Name;
        public DeclinedBattlePetNames? DeclinedNames; // Optional
        public ulong BattlePetGUID;
    }
}
