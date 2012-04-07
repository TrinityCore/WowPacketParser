using System;
using System.Collections.Generic;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects
{
    public sealed class Unit : WoWObject
    {
        public List<Aura> Auras;

        public List<List<Aura>> AddedAuras;

        public override bool IsTemporarySpawn()
        {
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
        //public int? MeleeTime2;
        public uint? RangedTime;
        public uint? Model;
        //public int? Model2;
        public uint? Mount;
        // public int? MinDamage;
        // public int? MaxDamage;
        public uint? Bytes1;
        public UnitDynamicFlags? DynamicFlags;
        public NPCFlags? NpcFlags;
        public EmoteType? EmoteState;
        public uint[] Resistances;
        public uint? ManaMod;
        public uint? HealthMod;
        public uint? Bytes2;
        //public int? MeleeAttackPower;
        //public int? MeleeDamageMultiplier;
        //public int? RangedAttackPower;
        //public int? RangedMinDamage;
        //public int? RangedMaxDamage;
        public float? BoundingRadius;
        public float? CombatReach;
        public float? HoverHeight;

        // Fields calculate with bytes0
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

            PowerType = (PowerType) ((Bytes0 & 0x7FFFFFFF) >> 24);
            Gender    = (Gender)    ((Bytes0 & 0x00FF0000) >> 16);
            Class     = (Class)     ((Bytes0 & 0x0000FF00) >> 08);
            Race      = (Race)      ((Bytes0 & 0x000000FF) >> 00);
        }

        public void LoadValuesFromUpdateFields()
        {
            Size          = UpdateFields.GetValue<ObjectField, float?>(ObjectField.OBJECT_FIELD_SCALE_X);
            Bytes0        = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_BYTES_0);
            MaxHealth     = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_MAXHEALTH);
            Level         = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_LEVEL);
            Faction       = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_FACTIONTEMPLATE);
            Equipment     = UpdateFields.GetArray<UnitField, uint>(UnitField.UNIT_VIRTUAL_ITEM_SLOT_ID1, 3);
            UnitFlags     = UpdateFields.GetEnum<UnitField, UnitFlags?>(UnitField.UNIT_FIELD_FLAGS);
            UnitFlags2    = UpdateFields.GetEnum<UnitField, UnitFlags2?>(UnitField.UNIT_FIELD_FLAGS_2);
            MeleeTime     = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_BASEATTACKTIME);
            RangedTime    = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_RANGEDATTACKTIME);
            Model         = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_DISPLAYID);
            Mount         = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_MOUNTDISPLAYID);
            Bytes1        = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_BYTES_1);
            DynamicFlags  = UpdateFields.GetEnum<UnitField, UnitDynamicFlags?>(UnitField.UNIT_DYNAMIC_FLAGS);
            NpcFlags      = UpdateFields.GetEnum<UnitField, NPCFlags?>(UnitField.UNIT_NPC_FLAGS);
            EmoteState    = UpdateFields.GetEnum<UnitField, EmoteType?>(UnitField.UNIT_NPC_EMOTESTATE);
            Resistances   = UpdateFields.GetArray<UnitField, uint>(UnitField.UNIT_FIELD_RESISTANCES_ARMOR, 7);
            ManaMod       = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_BASE_MANA);
            HealthMod     = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_BASE_HEALTH);
            Bytes2        = UpdateFields.GetValue<UnitField, uint?>(UnitField.UNIT_FIELD_BYTES_2);
            BoundingRadius= UpdateFields.GetValue<UnitField, float?>(UnitField.UNIT_FIELD_BOUNDINGRADIUS);
            CombatReach   = UpdateFields.GetValue<UnitField, float?>(UnitField.UNIT_FIELD_COMBATREACH);
            HoverHeight   = UpdateFields.GetValue<UnitField, float?>(UnitField.UNIT_FIELD_HOVERHEIGHT);

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
            UpdateField uf;
            if (dict.TryGetValue(Enums.Version.UpdateFields.GetUpdateField(updateField), out uf))
            {
                if (typeof(TK) == typeof(int?) || typeof(TK) == typeof(int) ||
                    typeof(TK) == typeof(uint?) || typeof(TK) == typeof(uint))
                    return (TK) (object) uf.UInt32Value;
                if (typeof (TK) == typeof (float?) || typeof (TK) == typeof (double?) ||
                    typeof(TK) == typeof(float) || typeof(TK) == typeof(double))
                    return (TK) (object) uf.SingleValue;
            }

            return default(TK);
        }

        /// <summary>
        /// Grabs N (consecutive) values from a dictionary of UpdateFields
        /// </summary>
        /// <typeparam name="T">The type of UpdateField (ObjectField, UnitField, ...)</typeparam>
        /// <typeparam name="TK">UInt or Float</typeparam>
        /// <param name="dict">The dictionary</param>
        /// <param name="firstUpdateField">The first update field of the sequence</param>
        /// <param name="count">Number of values to retrieve</param>
        /// <returns></returns>
        public static TK[] GetArray<T, TK>(this Dictionary<int, UpdateField> dict, T firstUpdateField, int count)
        {
            var result = new TK[count];

            for (var i = 0; i < count; i++)
            {
                UpdateField uf;
                if (dict.TryGetValue(Enums.Version.UpdateFields.GetUpdateField<T>(Convert.ToInt32(firstUpdateField) + i), out uf))
                    if (typeof(TK) == typeof(uint))
                        result[i] = (TK) (object) uf.UInt32Value;
                    else if (typeof(TK) == typeof(float))
                        result[i] = (TK)(object) uf.SingleValue;
                    else
                        return null;
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
                if (dict.TryGetValue(Enums.Version.UpdateFields.GetUpdateField(updateField), out uf))
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
