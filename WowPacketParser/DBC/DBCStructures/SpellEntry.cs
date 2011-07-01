using System;
using System.Runtime.InteropServices;

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
        private readonly uint[] _SpellName; // SpellEntry.SpellName
        public uint SpellNameFlag;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = DBCStore.DBC.MaxDBCLocale)]
        private readonly uint[] _Rank; // SpellEntry.Rank
        public uint RankFlags;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = DBCStore.DBC.MaxDBCLocale)]
        private readonly uint[] _Description; // SpellEntry.Description
        public uint DescriptionFlag;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = DBCStore.DBC.MaxDBCLocale)]
        private readonly uint[] _ToolTip; // SpellEntry.ToolTip
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

        public string SpellName
        {
            get
            {
                var name = string.Empty;
                var a = DBCStore.DBC.SpellStrings.TryGetValue(_SpellName[0], out name);
                System.Console.WriteLine(a ? "FOUND NAME" : "NOT FOUND NAME");
                return name;
            }
        }

        public string Rank
        {
            get
            {
                var rank = string.Empty;
                DBCStore.DBC.SpellStrings.TryGetValue(_Rank[0], out rank);
                return rank;
            }
        }

        public string Description
        {
            get
            {
                var description = string.Empty;
                DBCStore.DBC.SpellStrings.TryGetValue(_Description[0], out description);
                return description;
            }
        }

        public string ToolTip
        {
            get
            {
                var toolTip = string.Empty;
                DBCStore.DBC.SpellStrings.TryGetValue(_ToolTip[0], out toolTip);
                return toolTip;
            }
        }
    }
}
