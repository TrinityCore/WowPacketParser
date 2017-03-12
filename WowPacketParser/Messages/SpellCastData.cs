using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct SpellCastData
    {
        public ulong CasterGUID;
        public ulong CasterUnit;
        public byte CastID;
        public int SpellID;
        public uint CastFlags;
        public uint CastFlagsEx;
        public uint CastTime;
        public List<ulong> HitTargets;
        public List<ulong> MissTargets;
        public List<SpellMissStatus> MissStatus;
        public SpellTargetData Target;
        public List<SpellPowerData> RemainingPower;
        public RuneData RemainingRunes; // Optional
        public MissileTrajectoryResult MissileTrajectory;
        public SpellAmmo Ammo;
        public ProjectileVisual ProjectileVisual; // Optional
        public byte DestLocSpellCastIndex;
        public List<Location> TargetPoints;
        public CreatureImmunities Immunities;
        public SpellHealPrediction Predict;
    }
}
