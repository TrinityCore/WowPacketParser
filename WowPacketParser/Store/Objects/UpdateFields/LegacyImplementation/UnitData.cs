using System.Collections.Generic;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields.LegacyImplementation
{
    public class UnitData : IUnitData
    {
        private WoWObject Object { get; }
        private Dictionary<int, UpdateField> UpdateFields => Object.UpdateFields;

        public UnitData(WoWObject obj)
        {
            Object = obj;
        }

        private WowGuid GetGuidValue(UnitField field)
        {
            if (!ClientVersion.AddedInVersion(ClientType.WarlordsOfDraenor))
            {
                var parts = UpdateFields.GetArray<UnitField, uint>(field, 2);
                return new WowGuid64(Utilities.MAKE_PAIR64(parts[0], parts[1]));
            }
            else
            {
                var parts = UpdateFields.GetArray<UnitField, uint>(field, 4);
                return new WowGuid128(Utilities.MAKE_PAIR64(parts[0], parts[1]), Utilities.MAKE_PAIR64(parts[2], parts[3]));
            }
        }

        public int? DisplayID => UpdateFields.GetValue<UnitField, int?>(UnitField.UNIT_FIELD_DISPLAYID);

        public uint?[] NpcFlags
        {
            get
            {
                if (ClientVersion.AddedInVersion(ClientType.Legion))
                    return UpdateFields.GetArray<UnitField, uint?>(UnitField.UNIT_NPC_FLAGS, 2);
                else
                    return new uint?[] { UpdateFields.GetValue<UnitField, uint>(UnitField.UNIT_NPC_FLAGS), 0 };
            }
        }

        public int?[] Resistances => UpdateFields.GetArray<UnitField, int?>(UnitField.UNIT_FIELD_RESISTANCES, 7);

        public WowGuid SummonedBy => GetGuidValue(UnitField.UNIT_FIELD_SUMMONEDBY);

        public WowGuid CreatedBy => GetGuidValue(UnitField.UNIT_FIELD_CREATEDBY);

        public byte? ClassId => (byte)((UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_BYTES_0).GetValueOrDefault((uint)Class.Warrior << 8) >> 8) & 0xFF);

        public byte? Sex => (byte)(ClientVersion.AddedInVersion(ClientVersionBuild.V5_4_0_17359)
                ? ((UpdateFields.GetValue<UnitField, uint>(UnitField.UNIT_FIELD_BYTES_0) >> 24) & 0xFF)
                : ((UpdateFields.GetValue<UnitField, uint>(UnitField.UNIT_FIELD_BYTES_0) >> 16) & 0xFF));

        public byte? Race => UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_BYTES_0).HasValue ?
            (byte)(UpdateFields.GetValue<UnitField, uint>(UnitField.UNIT_FIELD_BYTES_0) & 0xFF) : null;

        public byte? DisplayPower => UpdateFields.GetValue<UnitField, byte?>(UnitField.UNIT_FIELD_DISPLAY_POWER);

        public long? Health => UpdateFields.GetValue<UnitField, int>(UnitField.UNIT_FIELD_HEALTH);

        public long? MaxHealth => UpdateFields.GetValue<UnitField, int>(UnitField.UNIT_FIELD_MAXHEALTH);

        public int?[] Power => new int?[] { UpdateFields.GetValue<UnitField, int>(UnitField.UNIT_FIELD_POWER) };

        public int?[] MaxPower => new int?[] { UpdateFields.GetValue<UnitField, int>(UnitField.UNIT_FIELD_MAXPOWER) };

        public int? Level => UpdateFields.GetValue<UnitField, int?>(UnitField.UNIT_FIELD_LEVEL).GetValueOrDefault(1);

        public int? ContentTuningID => ClientVersion.AddedInVersion(ClientType.BattleForAzeroth)
                ? UpdateFields.GetValue<UnitField, int>(UnitField.UNIT_FIELD_CONTENT_TUNING_ID)
                : UpdateFields.GetValue<UnitField, int>(UnitField.UNIT_FIELD_SANDBOX_SCALING_ID);

        public int? ScalingLevelMin => UpdateFields.GetValue<UnitField, int>(UnitField.UNIT_FIELD_SCALING_LEVEL_MIN);

        public int? ScalingLevelMax => UpdateFields.GetValue<UnitField, int>(UnitField.UNIT_FIELD_SCALING_LEVEL_MAX);

        public int? ScalingLevelDelta => UpdateFields.GetValue<UnitField, int>(UnitField.UNIT_FIELD_SCALING_LEVEL_DELTA);

        public int? FactionTemplate => UpdateFields.GetValue<UnitField, int>(UnitField.UNIT_FIELD_FACTIONTEMPLATE);

        public IVisibleItem[] VirtualItems
        {
            get
            {
                if (ClientVersion.AddedInVersion(ClientType.Legion))
                {
                    var raw = UpdateFields.GetArray<UnitField, uint>(UnitField.UNIT_VIRTUAL_ITEM_SLOT_ID, 6);
                    var items = new VisibleItem[3];
                    for (var i = 0; i < 3; ++i)
                    {
                        items[i] = new VisibleItem
                        {
                            ItemID = (int)raw[i * 2],
                            ItemAppearanceModID = (ushort)(raw[i * 2 + 1] & 0xFFFF),
                            ItemVisual = (ushort)((raw[i * 2 + 1] >> 16) & 0xFFFF)
                        };
                    }
                    return items;
                }
                else
                    return UpdateFields.GetArray<UnitField, int>(UnitField.UNIT_VIRTUAL_ITEM_SLOT_ID, 3)
                        .Select(rawId => new VisibleItem { ItemID = rawId }).ToArray();
            }
        }

        public uint? Flags => UpdateFields.GetValue<UnitField, uint>(UnitField.UNIT_FIELD_FLAGS);

        public uint? Flags2 => UpdateFields.GetValue<UnitField, uint>(UnitField.UNIT_FIELD_FLAGS_2);

        public uint? Flags3 => UpdateFields.GetValue<UnitField, uint>(UnitField.UNIT_FIELD_FLAGS_3);

        public uint?[] AttackRoundBaseTime => UpdateFields.GetArray<UnitField, uint?>(UnitField.UNIT_FIELD_BASEATTACKTIME, 2)
            .Select(maybeAttackTime => (uint?)maybeAttackTime.GetValueOrDefault(2000)).ToArray();

        public uint? RangedAttackRoundBaseTime => UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_RANGEDATTACKTIME).GetValueOrDefault(2000);

        public float? BoundingRadius => UpdateFields.GetValue<UnitField, float?>(UnitField.UNIT_FIELD_BOUNDINGRADIUS).GetValueOrDefault(0.306f);

        public float? CombatReach => UpdateFields.GetValue<UnitField, float?>(UnitField.UNIT_FIELD_COMBATREACH).GetValueOrDefault(1.5f);

        public int? MountDisplayID => UpdateFields.GetValue<UnitField, int>(UnitField.UNIT_FIELD_MOUNTDISPLAYID);

        public byte? StandState => (byte)(UpdateFields.GetValue<UnitField, uint>(UnitField.UNIT_FIELD_BYTES_1) & 0xFF);

        public byte? PetTalentPoints => (byte)((UpdateFields.GetValue<UnitField, uint>(UnitField.UNIT_FIELD_BYTES_1) >> 8) & 0xFF);

        public byte? VisFlags => (byte)((UpdateFields.GetValue<UnitField, uint>(UnitField.UNIT_FIELD_BYTES_1) >> 16) & 0xFF);

        public byte? AnimTier => (byte)((UpdateFields.GetValue<UnitField, uint>(UnitField.UNIT_FIELD_BYTES_1) >> 24) & 0xFF);

        public int? CreatedBySpell => UpdateFields.GetValue<UnitField, int>(UnitField.UNIT_CREATED_BY_SPELL);

        public int? EmoteState => UpdateFields.GetValue<UnitField, int>(UnitField.UNIT_NPC_EMOTESTATE);

        public byte? SheatheState => (byte)(UpdateFields.GetValue<UnitField, uint>(UnitField.UNIT_FIELD_BYTES_2) & 0xFF);

        public byte? PvpFlags => (byte)((UpdateFields.GetValue<UnitField, uint>(UnitField.UNIT_FIELD_BYTES_2) >> 8) & 0xFF);

        public byte? PetFlags => (byte)((UpdateFields.GetValue<UnitField, uint>(UnitField.UNIT_FIELD_BYTES_2) >> 16) & 0xFF);

        public byte? ShapeshiftForm => (byte)((UpdateFields.GetValue<UnitField, uint>(UnitField.UNIT_FIELD_BYTES_2) >> 24) & 0xFF);

        public float? HoverHeight => UpdateFields.GetValue<UnitField, float?>(UnitField.UNIT_FIELD_HOVERHEIGHT).GetValueOrDefault(1.0f);

        public int? InteractSpellID => UpdateFields.GetValue<UnitField, int?>(UnitField.UNIT_FIELD_INTERACT_SPELLID);
        public uint? StateSpellVisualID => UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_STATE_SPELL_VISUAL_ID);
        public uint? StateAnimID => UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_STATE_ANIM_ID);
        public uint? StateAnimKitID => UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_STATE_ANIM_KIT_ID);
        public uint? StateWorldEffectsQuestObjectiveID => null;
        public uint?[] StateWorldEffectIDs => UpdateFields.GetValue<UnitField, uint?[]>(UnitField.UNIT_FIELD_STATE_WORLD_EFFECT_ID);
        public WowGuid Charm => GetGuidValue(UnitField.UNIT_FIELD_CHARM);
        public WowGuid Summon => GetGuidValue(UnitField.UNIT_FIELD_SUMMON);
        public WowGuid Critter => GetGuidValue(UnitField.UNIT_FIELD_CRITTER);
        public WowGuid CharmedBy => GetGuidValue(UnitField.UNIT_FIELD_CHARMEDBY);
        public WowGuid DemonCreator => GetGuidValue(UnitField.UNIT_FIELD_DEMON_CREATOR);
        public WowGuid LookAtControllerTarget => GetGuidValue(UnitField.UNIT_FIELD_LOOK_AT_CONTROLLER_TARGET);
        public WowGuid Target => GetGuidValue(UnitField.UNIT_FIELD_TARGET);
        public int? EffectiveLevel => UpdateFields.GetValue<UnitField, int?>(UnitField.UNIT_FIELD_EFFECTIVE_LEVEL);
        public uint? AuraState => UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_AURASTATE);
        public float? DisplayScale => UpdateFields.GetValue<UnitField, float?>(UnitField.UNIT_FIELD_DISPLAY_SCALE);
        public int? CreatureFamily => null;
        public int? CreatureType => null;
        public int? NativeDisplayID => UpdateFields.GetValue<UnitField, int?>(UnitField.UNIT_FIELD_NATIVEDISPLAYID);
        public float? NativeXDisplayScale => UpdateFields.GetValue<UnitField, float?>(UnitField.UNIT_FIELD_NATIVE_X_DISPLAY_SCALE);
        public int? BaseMana => UpdateFields.GetValue<UnitField, int?>(UnitField.UNIT_FIELD_BASE_MANA);
        public int? BaseHealth => UpdateFields.GetValue<UnitField, int?>(UnitField.UNIT_FIELD_BASE_HEALTH);

        public class VisibleItem : IMutableVisibleItem
        {
            public int? ItemID { get; set; }
            public ushort? ItemAppearanceModID { get; set; }
            public ushort? ItemVisual { get; set; }
        }
    }
}
