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
        public uint? Faction;
        public uint[] Equipment;
        public UnitFlags? UnitFlags;
        public UnitFlags2? UnitFlags2;
        public uint? MeleeTime;
        public uint? RangedTime;
        public uint? Model;
        public uint? Mount;
        public uint? Bytes1;
        public UnitDynamicFlags? DynamicFlags;
        public NPCFlags? NpcFlags;
        public EmoteType? EmoteState;
        public uint? ManaMod;
        public uint? HealthMod;
        public uint? Bytes2;
        public float? BoundingRadius;
        public float? CombatReach;
        public float? HoverHeight;
        public uint? InteractSpellID;

        // Fields calculated with bytes0
        public PowerType? PowerType;
        public Gender? Gender;
        public Class? Class;
        public Race? Race;

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

        public override void LoadValuesFromUpdateFields()
        {
            Bytes0        = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_BYTES_0);
            Bytes1        = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_BYTES_1);
            Bytes2        = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_BYTES_2);
            Size          = UpdateFields.GetValue<ObjectField, float?>(ObjectField.OBJECT_FIELD_SCALE_X);
            MaxHealth     = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_MAXHEALTH);
            Level         = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_LEVEL);
            Faction       = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_FACTIONTEMPLATE);
            Equipment     = UpdateFields.GetArray<UnitField, uint>(UnitField.UNIT_VIRTUAL_ITEM_SLOT_ID, 3);
            UnitFlags     = UpdateFields.GetEnum<UnitField, UnitFlags?>(UnitField.UNIT_FIELD_FLAGS);
            UnitFlags2    = UpdateFields.GetEnum<UnitField, UnitFlags2?>(UnitField.UNIT_FIELD_FLAGS_2);
            MeleeTime     = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_BASEATTACKTIME);
            RangedTime    = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_RANGEDATTACKTIME);
            Model         = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_DISPLAYID);
            Mount         = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_MOUNTDISPLAYID);
            DynamicFlags  = UpdateFields.GetEnum<UnitField, UnitDynamicFlags?>(UnitField.UNIT_DYNAMIC_FLAGS);
            NpcFlags      = UpdateFields.GetEnum<UnitField, NPCFlags?>(UnitField.UNIT_NPC_FLAGS);
            EmoteState    = UpdateFields.GetEnum<UnitField, EmoteType?>(UnitField.UNIT_NPC_EMOTESTATE);
            //Resistances   = UpdateFields.GetArray<UnitField, uint>(UnitField.UNIT_FIELD_RESISTANCES_ARMOR, 7);
            ManaMod       = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_BASE_MANA);
            HealthMod     = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_BASE_HEALTH);
            BoundingRadius= UpdateFields.GetValue<UnitField, float?>(UnitField.UNIT_FIELD_BOUNDINGRADIUS);
            CombatReach   = UpdateFields.GetValue<UnitField, float?>(UnitField.UNIT_FIELD_COMBATREACH);
            HoverHeight   = UpdateFields.GetValue<UnitField, float?>(UnitField.UNIT_FIELD_HOVERHEIGHT);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_4_0_17359))
            {
                PowerType = UpdateFields.GetEnum<UnitField, PowerType?>(UnitField.UNIT_FIELD_DISPLAY_POWER);
                InteractSpellID = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_INTERACT_SPELLID);
            }

            ComputeBytes0();
        }
    }

    public static class Ext // TODO: Rename & move
    {
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
            var isInt = false;
            var type = typeof(TK);
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.UInt32:
                case TypeCode.Int32:
                    isInt = true;
                    break;
                case TypeCode.Single:
                case TypeCode.Double:
                    break;
                default:
                {
                    switch (Type.GetTypeCode(Nullable.GetUnderlyingType(type)))
                    {
                        case TypeCode.UInt32:
                        case TypeCode.Int32:
                            isInt = true;
                            break;
                        case TypeCode.Single:
                        case TypeCode.Double:
                            break;
                        default:
                            return default(TK);
                    }
                    break;
                }
            }

            UpdateField uf;
            if (dict.TryGetValue(UpdateFields.GetUpdateField(updateField), out uf))
            {
                if (isInt)
                    return (TK)(object)uf.UInt32Value;
                return (TK)(object)uf.SingleValue;
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
            var isInt = false;
            var type = typeof(TK);
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.UInt32:
                case TypeCode.Int32:
                    isInt = true;
                    break;
                case TypeCode.Single:
                case TypeCode.Double:
                    break;
                default:
                    {
                        switch (Type.GetTypeCode(Nullable.GetUnderlyingType(type)))
                        {
                            case TypeCode.UInt32:
                            case TypeCode.Int32:
                                isInt = true;
                                break;
                            case TypeCode.Single:
                            case TypeCode.Double:
                                break;
                            default:
                                return null;
                        }
                        break;
                    }
            }

            var result = new TK[count];
            UpdateField uf;
            if (isInt)
            {
                for (var i = 0; i < count; i++)
                    if (dict.TryGetValue(UpdateFields.GetUpdateField(firstUpdateField) + i, out uf))
                        result[i] = (TK)(object)uf.UInt32Value;
            }
            else
            {
                for (var i = 0; i < count; i++)
                    if (dict.TryGetValue(UpdateFields.GetUpdateField(firstUpdateField) + i, out uf))
                        result[i] = (TK)(object)uf.SingleValue;
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
