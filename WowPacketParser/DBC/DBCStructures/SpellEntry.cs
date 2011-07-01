using System.Runtime.InteropServices;
using WowPacketParser.DBC.DBCStore;

namespace WowPacketParser.DBC.DBCStructures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SpellEntry
    {
        public uint ID;
        public uint Category;
        public uint DispelType;
        public uint Mechanic;
        public uint Attributes;
        public uint AttributesEx;
        public uint AttributesEx2;
        public uint AttributesEx3;
        public uint AttributesEx4;
        public uint AttributesEx5;
        public uint AttributesEx6;
        public uint AttributesEx7;
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
        public uint PowerType;
        public uint ManaCost;
        public uint ManaCostPerLevel;
        public uint ManaPerSecond;
        public uint ManaPerSecondPerLevel;
        public uint RangeIndex;
        public float Speed;
        public uint ModalNextSpell;
        public uint CumulativeAura;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] Totem;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public int[] Reagent;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public uint[] ReagentCount;
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
        public int[] EffectBasePoints;
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
        public uint[] SpellVisualID;
        public uint SpellIconID;
        public uint ActiveIconID;
        public uint SpellPriority;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = DBCStore.DBC.MaxDBCLocale)]
        private readonly uint[] _SpellName;
        public uint SpellNameFlag;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = DBCStore.DBC.MaxDBCLocale)]
        private readonly uint[] _Rank;
        public uint RankFlags;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = DBCStore.DBC.MaxDBCLocale)]
        private readonly uint[] _Description;
        public uint DescriptionFlag;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = DBCStore.DBC.MaxDBCLocale)]
        private readonly uint[] _ToolTip;
        public uint AuraDescriptionFlag;
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
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] RequiredTotemCategoryID;
        public int RequiredAreasID;
        public uint SchoolMask;
        public uint RuneCostID;
        public uint SpellMissileID;
        public uint PowerDisplayID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] Unk1;
        public uint SpellDescriptionVariableID;
        public uint SpellDifficultyID;

        /*
        uint m_ID;
        uint m_category;
        uint m_dispelType;
        uint m_mechanic;
        uint m_attributes;
        uint m_attributesEx;
        uint m_attributesExB;
        uint m_attributesExC;
        uint m_attributesExD;
        uint m_attributesExE;
        uint m_attributesExF;
        uint m_attributesExG;
        ulong m_shapeshiftMask;
        ulong m_shapeshiftExclude;
        uint m_targets;
        uint m_targetCreatureType;
        uint m_requiresSpellFocus;
        uint m_facingCasterFlags;
        uint m_casterAuraState;
        uint m_targetAuraState;
        uint m_excludeCasterAuraState;
        uint m_excludeTargetAuraState;
        uint m_casterAuraSpell ;
        uint m_targetAuraSpell;
        uint m_excludeCasterAuraSpell;
        uint m_excludeTargetAuraSpell;
        uint m_castingTimeIndex;
        uint m_recoveryTime;
        uint m_categoryRecoveryTime;
        uint m_interruptFlags;
        uint m_auraInterruptFlags;
        uint m_channelInterruptFlags;
        uint m_procTypeMask;
        uint m_procChance;
        uint m_procCharges;
        uint m_maxLevel;
        uint m_baseLevel;
        uint m_spellLevel;
        uint m_durationIndex;
        int m_powerType;
        uint m_manaCost;
        uint m_manaCostPerLevel;
        uint m_manaPerSecond;
        uint m_manaPerSecondPerLevel;
        uint m_rangeIndex;
        float m_speed;
        uint m_modalNextSpell;
        uint m_cumulativeAura;
        uint m_totem_1;
        uint m_totem_2;
        int m_reagent_1;
        int m_reagent_2;
        int m_reagent_3;
        int m_reagent_4;
        int m_reagent_5;
        int m_reagent_6;
        int m_reagent_7;
        int m_reagent_8;
        int m_reagentCount_1;
        int m_reagentCount_2;
        int m_reagentCount_3;
        int m_reagentCount_4;
        int m_reagentCount_5;
        int m_reagentCount_6;
        int m_reagentCount_7;
        int m_reagentCount_8;
        int m_equippedItemClass;
        int m_equippedItemSubclass;
        int m_equippedItemInvTypes;
        uint m_effect_1;
        uint m_effect_2;
        uint m_effect_3;
        int m_effectDieSides_1;
        int m_effectDieSides_2;
        int m_effectDieSides_3;
        float m_effectRealPointsPerLevel_1;
        float m_effectRealPointsPerLevel_2;
        float m_effectRealPointsPerLevel_3;
        int m_effectBasePoints_1;
        int m_effectBasePoints_2;
        int m_effectBasePoints_3;
        uint m_effectMechanic_1;
        uint m_effectMechanic_2;
        uint m_effectMechanic_3;
        uint m_implicitTargetA_1;
        uint m_implicitTargetA_2;
        uint m_implicitTargetA_3;
        uint m_implicitTargetB_1;
        uint m_implicitTargetB_2;
        uint m_implicitTargetB_3;
        uint m_effectRadiusIndex_1;
        uint m_effectRadiusIndex_2;
        uint m_effectRadiusIndex_3;
        uint m_effectAura_1;
        uint m_effectAura_2;
        uint m_effectAura_3;
        uint m_effectAuraPeriod_1;
        uint m_effectAuraPeriod_2;
        uint m_effectAuraPeriod_3;
        float m_effectAmplitude_1;
        float m_effectAmplitude_2;
        float m_effectAmplitude_3;
        uint m_effectChainTargets_1;
        uint m_effectChainTargets_2;
        uint m_effectChainTargets_3;
        uint m_effectItemType_1;
        uint m_effectItemType_2;
        uint m_effectItemType_3;
        int m_effectMiscValue_1;
        int m_effectMiscValue_2;
        int m_effectMiscValue_3;
        int m_effectMiscValueB_1;
        int m_effectMiscValueB_2;
        int m_effectMiscValueB_3;
        uint m_effectTriggerSpell_1;
        uint m_effectTriggerSpell_2;
        uint m_effectTriggerSpell_3;
        float m_effectPointsPerCombo_1;
        float m_effectPointsPerCombo_2;
        float m_effectPointsPerCombo_3;
        uint m_effectSpellClassMaskA_1;
        uint m_effectSpellClassMaskA_2;
        uint m_effectSpellClassMaskA_3;
        uint m_effectSpellClassMaskB_1;
        uint m_effectSpellClassMaskB_2;
        uint m_effectSpellClassMaskB_3;
        uint m_effectSpellClassMaskC_1;
        uint m_effectSpellClassMaskC_2;
        uint m_effectSpellClassMaskC_3;
        uint m_spellVisualID_1;
        uint m_spellVisualID_2;
        uint m_spellIconID;
        uint m_activeIconID;
        uint m_spellPriority;
        string m_name_lang_1;
        string m_name_lang_2;
        string m_name_lang_3;
        string m_name_lang_4;
        string m_name_lang_5;
        string m_name_lang_6;
        string m_name_lang_7;
        string m_name_lang_8;
        string m_name_lang_9;
        string m_name_lang_10;
        string m_name_lang_11;
        string m_name_lang_12;
        string m_name_lang_13;
        string m_name_lang_14;
        string m_name_lang_15;
        string m_name_lang_16;
        uint m_name_flag;
        string m_nameSubtext_lang_1;
        string m_nameSubtext_lang_2;
        string m_nameSubtext_lang_3;
        string m_nameSubtext_lang_4;
        string m_nameSubtext_lang_5;
        string m_nameSubtext_lang_6;
        string m_nameSubtext_lang_7;
        string m_nameSubtext_lang_8;
        string m_nameSubtext_lang_9;
        string m_nameSubtext_lang_10;
        string m_nameSubtext_lang_11;
        string m_nameSubtext_lang_12;
        string m_nameSubtext_lang_13;
        string m_nameSubtext_lang_14;
        string m_nameSubtext_lang_15;
        string m_nameSubtext_lang_16;
        uint m_nameSubtext_flag;
        string m_description_lang_1;
        string m_description_lang_2;
        string m_description_lang_3;
        string m_description_lang_4;
        string m_description_lang_5;
        string m_description_lang_6;
        string m_description_lang_7;
        string m_description_lang_8;
        string m_description_lang_9;
        string m_description_lang_10;
        string m_description_lang_11;
        string m_description_lang_12;
        string m_description_lang_13;
        string m_description_lang_14;
        string m_description_lang_15;
        string m_description_lang_16;
        uint m_description_flag;
        string m_auraDescription_lang_1;
        string m_auraDescription_lang_2;
        string m_auraDescription_lang_3;
        string m_auraDescription_lang_4;
        string m_auraDescription_lang_5;
        string m_auraDescription_lang_6;
        string m_auraDescription_lang_7;
        string m_auraDescription_lang_8;
        string m_auraDescription_lang_9;
        string m_auraDescription_lang_10;
        string m_auraDescription_lang_11;
        string m_auraDescription_lang_12;
        string m_auraDescription_lang_13;
        string m_auraDescription_lang_14;
        string m_auraDescription_lang_15;
        string m_auraDescription_lang_16;
        uint m_auraDescription_flag;
        uint m_manaCostPct;
        uint m_startRecoveryCategory;
        uint m_startRecoveryTime;
        uint m_maxTargetLevel;
        uint m_spellClassSet;
        uint m_spellClassMask_1;
        uint m_spellClassMask_2;
        uint m_spellClassMask_3;
        uint m_maxTargets;
        uint m_defenseType;
        uint m_preventionType;
        uint m_stanceBarOrder;
        float m_effectChainAmplitude_1;
        float m_effectChainAmplitude_2;
        float m_effectChainAmplitude_3;
        uint m_minFactionID;
        uint m_minReputation;
        uint m_requiredAuraVision;
        uint m_requiredTotemCategoryID_1;
        uint m_requiredTotemCategoryID_2;
        int m_requiredAreasID;
        uint m_schoolMask;
        uint m_runeCostID;
        uint m_spellMissileID;
        int m_powerDisplayID;
        float m_unk1_1;
        float m_unk1_2;
        float m_unk1_3;
        uint m_spellDescriptionVariableID;
        uint m_spellDifficultyID;
         * */
    }
}
