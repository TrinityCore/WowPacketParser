using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CreatureSoundData, HasIndexInData = false)]
    public class CreatureSoundDataEntry
    {
        public uint SoundExertionID { get; set; }
        public uint SoundExertionCriticalID { get; set; }
        public uint SoundInjuryID { get; set; }
        public uint SoundInjuryCriticalID { get; set; }
        public uint SoundInjuryCrushingBlowID { get; set; }
        public uint SoundDeathID { get; set; }
        public uint SoundStunID { get; set; }
        public uint SoundStandID { get; set; }
        public uint SoundFootstepID { get; set; }
        public uint SoundAggroID { get; set; }
        public uint SoundWingFlapID { get; set; }
        public uint SoundWingGlideID { get; set; }
        public uint SoundAlertID { get; set; }
        public uint SoundJumpStartID { get; set; }
        public uint SoundJumpEndID { get; set; }
        public uint SoundPetAttackID { get; set; }
        public uint SoundPetOrderID { get; set; }
        public uint SoundPetDismissID { get; set; }
        public uint LoopSoundID { get; set; }
        public uint BirthSoundID { get; set; }
        public uint SpellCastDirectedSoundID { get; set; }
        public uint SubmergeSoundID { get; set; }
        public uint SubmergedSoundID { get; set; }
        public uint WindupSoundID { get; set; }
        public uint WindupCriticalSoundID { get; set; }
        public uint ChargeSoundID { get; set; }
        public uint ChargeCriticalSoundID { get; set; }
        public uint BattleShoutSoundID { get; set; }
        public uint BattleShoutCriticalSoundID { get; set; }
        public uint TauntSoundID { get; set; }
        public uint CreatureSoundDataIDPet { get; set; }
        public float FidgetDelaySecondsMin { get; set; }
        public float FidgetDelaySecondsMax { get; set; }
        public byte CreatureImpactType { get; set; }
        public uint NPCSoundID { get; set; }
        [HotfixArray(5)]
        public uint[] SoundFidget { get; set; }
        [HotfixArray(4)]
        public uint[] CustomAttack { get; set; }
    }
}
