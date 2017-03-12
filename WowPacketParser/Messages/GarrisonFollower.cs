using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GarrisonFollower
    {
        public ulong DbID;
        public int GarrFollowerID;
        public int CreatureID;
        public int GarrGivenNameID;
        public int GarrFamilyNameID;
        public int Gender;
        public int Spec;
        public int Race;
        public int Quality;
        public int FollowerLevel;
        public int ItemLevelWeapon;
        public int ItemLevelArmor;
        public int Xp;
        public int CurrentBuildingID;
        public int CurrentMissionID;
        public List<int> AbilityID;
    }
}
