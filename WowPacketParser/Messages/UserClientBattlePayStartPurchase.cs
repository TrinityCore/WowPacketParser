using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientBattlePayStartPurchase
    {
        public ulong TargetCharacter;
        public uint ProductID;
        public uint ClientToken;
    }
}
