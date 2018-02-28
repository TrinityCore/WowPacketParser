using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects
{
    public sealed class Unit : WoWObject
    {
        public List<Aura> Auras;

        public BlockingCollection<List<Aura>> AddedAuras = new BlockingCollection<List<Aura>>();

        public override bool IsTemporarySpawn()
        {
            if (ForceTemporarySpawn)
                return true;

            // If our unit got any of the following update fields set,
            // it's probably a temporary spawn
            UpdateField uf;
            if (UpdateFields.TryGetValue(Enums.Version.UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_SUMMONEDBY), out uf) ||
                UpdateFields.TryGetValue(Enums.Version.UpdateFields.GetUpdateField(UnitField.UNIT_CREATED_BY_SPELL), out uf) ||
                UpdateFields.TryGetValue(Enums.Version.UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_CREATEDBY), out uf))
                return uf.UInt32Value != 0;

            return false;
        }

        public uint GossipId;

        // Fields from UPDATE_FIELDS
        public float? Size;
        public uint? Bytes0;
        public uint? MaxHealth;
        public uint? Level;
        public uint? ScalingMinLevel;
        public uint? ScalingMaxLevel;
        public int? ScalingDelta;
        public uint? Faction;
        public uint[] EquipmentItemId;
        public ushort[] EquipmentAppearanceModId;
        public ushort[] EquipmentItemVisual;
        public UnitFlags? UnitFlags;
        public UnitFlags2? UnitFlags2;
        public UnitFlags3? UnitFlags3;
        public uint? MeleeTime;
        public uint? RangedTime;
        public uint? Model;
        public uint? Mount;
        public uint? Bytes1;
        public ushort? AIAnimKit;
        public ushort? MovementAnimKit;
        public ushort? MeleeAnimKit;
        public UnitDynamicFlags? DynamicFlags;
        public UnitDynamicFlagsWOD? DynamicFlagsWod;
        public NPCFlags? NpcFlags;
        public EmoteType? EmoteState;
        public uint? ManaMod;
        public uint? HealthMod;
        public uint? Bytes2;
        public float? BoundingRadius;
        public float? CombatReach;
        public float? HoverHeight;
        public uint? InteractSpellID;
        public short[] Resistances;

        // Fields calculated with bytes0
        public PowerType? PowerType;
        public Gender? Gender;
        public Class? Class;
        public Race? Race;

        private uint[] EquipmentRaw;

        // Must be called AFTER LoadValuesFromUpdateFields
        private void ComputeBytes0()
        {
            if (Bytes0 == null || Bytes0 == 0)
            {
                PowerType = null;
                Gender = null;
                Class = null;
                Race = null;
                return;
            }

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V5_4_0_17359))
            {
                PowerType = (PowerType)((Bytes0 & 0x7FFFFFFF) >> 24);
                Gender = (Gender)((Bytes0 & 0x00FF0000) >> 16);
            }
            else
            {
                Gender = (Gender)((Bytes0 & 0xFF000000) >> 24);
            }
            Class     = (Class)     ((Bytes0 & 0x0000FF00) >>  8);
            Race      = (Race)      ((Bytes0 & 0x000000FF) >>  0);
        }

        private void ComputeEquipment()
        {
            if (EquipmentRaw == null)
            {
                EquipmentItemId = null;
                EquipmentAppearanceModId = null;
                EquipmentItemVisual = null;
                return;
            }

            if (ClientVersion.AddedInVersion(ClientType.Legion))
            {
                EquipmentItemId = new uint[3] { EquipmentRaw[0], EquipmentRaw[2], EquipmentRaw[4] };
                EquipmentAppearanceModId = new ushort[3] { (ushort)(EquipmentRaw[1] & 0xFFFF), (ushort)(EquipmentRaw[3] & 0xFFFF), (ushort)(EquipmentRaw[5] & 0xFFFF) };
                EquipmentItemVisual = new ushort[3] { (ushort)((EquipmentRaw[1] >> 16) & 0xFFFF), (ushort)((EquipmentRaw[3] >> 16) & 0xFFFF), (ushort)((EquipmentRaw[5] >> 16) & 0xFFFF) };
            }
            else
                EquipmentItemId = EquipmentRaw;
        }

        public override void LoadValuesFromUpdateFields()
        {
            Bytes0        = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_BYTES_0);
            Bytes1        = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_BYTES_1);
            Bytes2        = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_BYTES_2);
            Size          = UpdateFields.GetValue<ObjectField, float?>(ObjectField.OBJECT_FIELD_SCALE_X);
            MaxHealth     = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_MAXHEALTH);
            Level         = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_LEVEL);
            Faction       = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_FACTIONTEMPLATE);
            if (ClientVersion.AddedInVersion(ClientType.Legion))
            {
                ScalingMinLevel = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_SCALING_LEVEL_MIN);
                ScalingMaxLevel = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_SCALING_LEVEL_MAX);
                ScalingDelta = UpdateFields.GetValue<UnitField, int>(UnitField.UNIT_FIELD_SCALING_LEVEL_DELTA); ;
                EquipmentRaw = UpdateFields.GetArray<UnitField, uint>(UnitField.UNIT_VIRTUAL_ITEM_SLOT_ID, 6);
            }
            else
                EquipmentRaw = UpdateFields.GetArray<UnitField, uint>(UnitField.UNIT_VIRTUAL_ITEM_SLOT_ID, 3);

            UnitFlags     = UpdateFields.GetEnum<UnitField, UnitFlags?>(UnitField.UNIT_FIELD_FLAGS);
            UnitFlags2    = UpdateFields.GetEnum<UnitField, UnitFlags2?>(UnitField.UNIT_FIELD_FLAGS_2);
            if (ClientVersion.AddedInVersion(ClientType.Legion))
                UnitFlags3    = UpdateFields.GetEnum<UnitField, UnitFlags3?>(UnitField.UNIT_FIELD_FLAGS_3);

            MeleeTime     = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_BASEATTACKTIME);
            RangedTime    = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_RANGEDATTACKTIME);
            Model         = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_DISPLAYID);
            Mount         = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_MOUNTDISPLAYID);
            if (ClientVersion.AddedInVersion(ClientType.WarlordsOfDraenor))
                DynamicFlagsWod = UpdateFields.GetEnum<ObjectField, UnitDynamicFlagsWOD?>(ObjectField.OBJECT_DYNAMIC_FLAGS);
            else
                DynamicFlags  = UpdateFields.GetEnum<UnitField, UnitDynamicFlags?>(UnitField.UNIT_DYNAMIC_FLAGS);

            if (ClientVersion.AddedInVersion(ClientType.Legion))
            {
                // @TODO TEMPORARY HACK
                // For read NpcFlags as ulong
                uint[] tempNpcFlags = UpdateFields.GetArray<UnitField, uint>(UnitField.UNIT_NPC_FLAGS, 2);
                NpcFlags = (NPCFlags)Utilities.MAKE_PAIR64(tempNpcFlags[0], tempNpcFlags[1]);
            }
            else
                NpcFlags = UpdateFields.GetEnum<UnitField, NPCFlags?>(UnitField.UNIT_NPC_FLAGS);

            EmoteState    = UpdateFields.GetEnum<UnitField, EmoteType?>(UnitField.UNIT_NPC_EMOTESTATE);
            Resistances   = UpdateFields.GetArray<UnitField, short>(UnitField.UNIT_FIELD_RESISTANCES, 7);
            ManaMod       = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_BASE_MANA);
            HealthMod     = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_BASE_HEALTH);
            BoundingRadius= UpdateFields.GetValue<UnitField, float?>(UnitField.UNIT_FIELD_BOUNDINGRADIUS);
            CombatReach   = UpdateFields.GetValue<UnitField, float?>(UnitField.UNIT_FIELD_COMBATREACH);
            HoverHeight = UpdateFields.GetValue<UnitField, float?>(UnitField.UNIT_FIELD_HOVERHEIGHT);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_4_0_17359))
            {
                PowerType = UpdateFields.GetEnum<UnitField, PowerType?>(UnitField.UNIT_FIELD_DISPLAY_POWER);
                InteractSpellID = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_INTERACT_SPELLID);
            }

            ComputeBytes0();
            ComputeEquipment();
        }
    }

    public static class Ext // TODO: Rename & move
    {
        private static TypeCode GetTypeCodeOfReturnValue<TK>()
        {
            var type = typeof(TK);
            var typeCode = Type.GetTypeCode(type);
            switch (typeCode)
            {
                case TypeCode.UInt32:
                case TypeCode.Int32:
                case TypeCode.Single:
                case TypeCode.Double:
                    return typeCode;
                default:
                {
                    typeCode = Type.GetTypeCode(Nullable.GetUnderlyingType(type));
                    switch (typeCode)
                    {
                        case TypeCode.UInt32:
                        case TypeCode.Int32:
                        case TypeCode.Single:
                        case TypeCode.Double:
                            return typeCode;
                        default:
                            break;
                    }
                    break;
                }
            }
            return TypeCode.Empty;
        }

        /// <summary>
        /// Grabs a value from a dictionary of UpdateFields
        /// </summary>
        /// <typeparam name="T">The type of UpdateField (ObjectField, UnitField, ...)</typeparam>
        /// <typeparam name="TK">The type of the value (int, uint or float and their nullable counterparts)</typeparam>
        /// <param name="dict">The dictionary</param>
        /// <param name="updateField">The update field we want</param>
        /// <returns></returns>
        public static TK GetValue<T, TK>(this Dictionary<int, UpdateField> dict, T updateField)
        {
            UpdateField uf;
            if (dict.TryGetValue(UpdateFields.GetUpdateField(updateField), out uf))
            {
                var type = GetTypeCodeOfReturnValue<TK>();
                switch (type)
                {
                    case TypeCode.UInt32:
                        return (TK)(object)uf.UInt32Value;
                    case TypeCode.Int32:
                        return (TK)(object)(int)uf.UInt32Value;
                    case TypeCode.Single:
                        return (TK)(object)uf.SingleValue;
                    case TypeCode.Double:
                        return (TK)(object)(double)uf.SingleValue;
                    default:
                        break;
                }
            }

            return default(TK);
        }

        /// <summary>
        /// Grabs N (consecutive) values from a dictionary of UpdateFields
        /// </summary>
        /// <typeparam name="T">The type of UpdateField (ObjectField, UnitField, ...)</typeparam>
        /// <typeparam name="TK">The type of the value (int, uint or float and their nullable counterparts)</typeparam>
        /// <param name="dict">The dictionary</param>
        /// <param name="firstUpdateField">The first update field of the sequence</param>
        /// <param name="count">Number of values to retrieve</param>
        /// <returns></returns>
        public static TK[] GetArray<T, TK>(this Dictionary<int, UpdateField> dict, T firstUpdateField, int count)
        {
            var result = new TK[count];
            var type = GetTypeCodeOfReturnValue<TK>();
            for (var i = 0; i < count; i++)
            {
                UpdateField uf;
                if (dict.TryGetValue(UpdateFields.GetUpdateField(firstUpdateField) + i, out uf))
                {
                    switch (type)
                    {
                        case TypeCode.UInt32:
                            result[i] = (TK)(object)uf.UInt32Value;
                            break;
                        case TypeCode.Int32:
                            result[i] = (TK)(object)(int)uf.UInt32Value;
                            break;
                        case TypeCode.Single:
                            result[i] = (TK)(object)uf.SingleValue;
                            break;
                        case TypeCode.Double:
                            result[i] = (TK)(object)(double)uf.SingleValue;
                            break;
                        default:
                            break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Grabs a value from a dictionary of UpdateFields and converts it to an enum val
        /// </summary>
        /// <typeparam name="T">The type of UpdateField (ObjectField, UnitField, ...)</typeparam>
        /// <typeparam name="TK">The type of the value (a NULLABLE enum)</typeparam>
        /// <param name="dict">The dictionary</param>
        /// <param name="updateField">The update field we want</param>
        /// <returns></returns>
        public static TK GetEnum<T, TK>(this Dictionary<int, UpdateField> dict, T updateField)
        {
            // typeof (TK) is a nullable type (ObjectField?)
            // typeof (TK).GetGenericArguments()[0] is the non nullable equivalent (ObjectField)
            // we need to convert our int from UpdateFields to the enum type

            try
            {
                UpdateField uf;
                if (dict.TryGetValue(UpdateFields.GetUpdateField(updateField), out uf))
                    return (TK)Enum.Parse(typeof(TK).GetGenericArguments()[0], uf.UInt32Value.ToString(CultureInfo.InvariantCulture));
            }
            catch (OverflowException) // Data wrongly parsed can result in very wtfy values
            {
                return default(TK);
            }

            return default(TK);
        }
    }
}
