using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliPetAction
    {
        public ulong PetGUID;
        public ulong TargetGUID;
        public Vector3 ActionPosition;
        public uint Action;
    }
}
