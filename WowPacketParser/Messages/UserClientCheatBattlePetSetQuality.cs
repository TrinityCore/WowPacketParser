using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientCheatBattlePetSetQuality
    {
        public ushort BreedQuality;
        public ulong BattlePetGUID;
        public bool AllPetsInJournal;
    }
}
