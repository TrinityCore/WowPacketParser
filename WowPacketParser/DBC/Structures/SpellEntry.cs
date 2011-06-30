using System.Runtime.InteropServices;

namespace WowPacketParser.DBC.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SpellEntry
    {
        public uint ID;
        public uint Category;
        public uint DispelType;
        public uint Mechanic;
        public uint Attributes;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public uint[] AttributesEx;
        public ulong ShapeshiftMask;
        public ulong ShapeshiftExclude;
        public uint Targets;
        public uint TargetCreatureType;
        public uint RequiresSpellFocus;
        public uint FacingCasterFlags;
        public uint CasterAuraState;
        public uint TargetAuraState;
        public uint ExcludeCasterAuraState;
        public uint ExcludeTargetAuraState;
        public uint CasterAuraSpell;
        public uint TargetAuraSpell;
        public uint ExcludeCasterAuraSpell;
        public uint ExcludeTargetAuraSpell;
        public uint CastingTimeIndex;
        public uint RecoveryTime;
        public uint CategoryRecoveryTime;
        public uint InterruptFlags;
        public uint AuraInterruptFlags;
        public uint ChannelInterruptFlags;
        public uint ProcTypeMask;
        public uint ProcChance;
        public uint ProcCharges;
        public uint MaxLevel;
        public uint BaseLevel;
        public uint SpellLevel;
        public uint DurationIndex;
        public int PowerType;
        public uint ManaCost;
        public uint ManaCostPerLevel;
        public uint ManaPerSecond;
        public uint ManaPerSecondPerLevel;
        public uint RangeIndex;
        public float Speed;
        public uint ModalNextSpell;
        public uint CumulativeAura;
        public uint Tote1;
        public uint Tote2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public int[] Reagent;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public int[] ReagentCount;
        public int EquippedItemClass;
        public int EquippedItemSubclass;
        public int EquippedItemInvTypes;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] Effect;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public int[] EffectDieSides;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] EffectRealPointsPerLevel;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public int[] EffectBasePoints_1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] EffectMechanic;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] ImplicitTargetA;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] ImplicitTargetB;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] EffectRadiusIndex;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] EffectAura;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] EffectAuraPeriod;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] EffectAmplitude;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] EffectChainTargets;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] EffectItemType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public int[] EffectMiscValue;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public int[] EffectMiscValueB;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] EffectTriggerSpell;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] EffectPointsPerCombo;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] EffectSpellClassMaskA;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] EffectSpellClassMaskB;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] EffectSpellClassMaskC;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint SpellVisualID;
        public uint SpellIconID;
        public uint ActiveIconID;
        public uint SpellPriority;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public string[] NameLang;
        public uint NameFlag;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public string[] NameSubtextLang;
        public uint NameSubtextFlag;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public string[] DescriptionLang;
        public uint Description_flag;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public string[] AuraDescriptionLang;
        public uint AuraDescription_flag;
        public uint ManaCostPct;
        public uint StartRecoveryCategory;
        public uint StartRecoveryTime;
        public uint MaxTargetLevel;
        public uint SpellClassSet;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] SpellClassMask;
        public uint MaxTargets;
        public uint DefenseType;
        public uint PreventionType;
        public uint StanceBarOrder;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] EffectChainAmplitude;
        public uint MinFactionID;
        public uint MinReputation;
        public uint RequiredAuraVision;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] RequiredTotemCategoryID;
        public int RequiredAreasID;
        public uint SchoolMask;
        public uint RuneCostID;
        public uint SpellMissileID;
        public int PowerDisplayID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] Unk1;
        public uint SpellDescriptionVariableID;
        public uint SpellDifficultyID;
    }
}
