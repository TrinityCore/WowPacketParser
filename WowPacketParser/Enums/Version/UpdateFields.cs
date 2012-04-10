using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using WowPacketParser.Misc;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.Enums.Version
{
    public static class UpdateFields
    {
        private static readonly Dictionary<Type, BiDictionary<string, int>> UpdateFieldDictionaries = LoadUFDictionaries();

        public static readonly Dictionary<Type, int> UpdateFieldMaxOffsets;

        private static Dictionary<Type, BiDictionary<string, int>> LoadUFDictionaries()
        {
            Type[] enumTypes = {
                               typeof (ObjectField), typeof (ItemField), typeof (ContainerField), typeof (UnitField),
                               typeof (PlayerField), typeof (GameObjectField), typeof (DynamicObjectField),
                               typeof (CorpseField), typeof (AreaTriggerField)
                           };

            var dicts = new Dictionary<Type, BiDictionary<string, int>>();

            foreach (var enumType in enumTypes)
            {
                var vTypeString = string.Format("WowPacketParser.Enums.Version.{0}.{1}", GetUpdateFieldDictionaryBuildName(ClientVersion.Build), enumType.Name);
                var vEnumType = Assembly.GetExecutingAssembly().GetType(vTypeString);
                if (vEnumType == null)
                    continue;   // versions prior to 4.3.0 do not have AreaTriggerField

                var vValues = Enum.GetValues(vEnumType);
                var vNames = Enum.GetNames(vEnumType);

                var result = new BiDictionary<string, int>();

                for (var i = 0; i < vValues.Length; ++i)
                    result.Add(vNames[i], (int)vValues.GetValue(i));

                dicts.Add(enumType, result);
            }

            UpdateFieldMaxOffsets.Add(typeof(ObjectField), (int)GetUpdateFieldOffset(ObjectField.OBJECT_END));
            UpdateFieldMaxOffsets.Add(typeof(ItemField), (int)GetUpdateFieldOffset(ItemField.ITEM_END));
            UpdateFieldMaxOffsets.Add(typeof(ContainerField), (int)GetUpdateFieldOffset(ContainerField.CONTAINER_END));
            UpdateFieldMaxOffsets.Add(typeof(UnitField), (int)GetUpdateFieldOffset(UnitField.UNIT_END));
            UpdateFieldMaxOffsets.Add(typeof(PlayerField), (int)GetUpdateFieldOffset(PlayerField.PLAYER_END));
            UpdateFieldMaxOffsets.Add(typeof(GameObjectField), (int)GetUpdateFieldOffset(GameObjectField.GAMEOBJECT_END));
            UpdateFieldMaxOffsets.Add(typeof(DynamicObjectField), (int)GetUpdateFieldOffset(DynamicObjectField.DYNAMICOBJECT_END));
            UpdateFieldMaxOffsets.Add(typeof(CorpseField), (int)GetUpdateFieldOffset(CorpseField.CORPSE_END));
            return dicts;
        }

        // returns update field offset by generic - crossversion enum
        public static int? GetUpdateFieldOffset<T>(T field)
        {
            if (UpdateFieldDictionaries.ContainsKey(typeof(T)))
                if (UpdateFieldDictionaries[typeof(T)].ContainsKey(field.ToString()))
                    return UpdateFieldDictionaries[typeof(T)][field.ToString()];

            return null;
        }

        // returns update field name by offset
        public static string GetUpdateFieldName(int fieldOffset, Type t)
        {
            if (UpdateFieldDictionaries.ContainsKey(t))
            {
                foreach (var pair in UpdateFieldDictionaries[t])
                {
                    if (pair.Value == fieldOffset)
                        return pair.Key;
                }
            }

            return null;
        }

        public static string GetUpdateFieldName<T>(int fieldOffset)
        {
            return GetUpdateFieldName(fieldOffset, typeof(T));
        }

        public static Type GetUpdateFieldEnumByOffset(Int32 offset, ObjectType type)
        {
            switch (type)
            {
                case ObjectType.Object:
                    return typeof(ObjectField);
                case ObjectType.Item:
                    {
                        int max;
                        Enums.Version.UpdateFields.UpdateFieldMaxOffsets.TryGetValue(typeof(ObjectField), out max);
                        if (offset < max)
                            goto case ObjectType.Object;
                        return typeof(ItemField);
                    }
                case ObjectType.Container:
                    {
                        int max;
                        Enums.Version.UpdateFields.UpdateFieldMaxOffsets.TryGetValue(typeof(ItemField), out max);
                        if (offset < max)
                            goto case ObjectType.Item;
                        return typeof(ContainerField);
                    }
                case ObjectType.Unit:
                    {
                        int max;
                        Enums.Version.UpdateFields.UpdateFieldMaxOffsets.TryGetValue(typeof(ObjectField), out max);
                        if (offset < max)
                            goto case ObjectType.Object;
                        return typeof(UnitField);
                    }
                case ObjectType.Player:
                    {
                        int max;
                        Enums.Version.UpdateFields.UpdateFieldMaxOffsets.TryGetValue(typeof(UnitField), out max);
                        if (offset < max)
                            goto case ObjectType.Unit;
                        return typeof(PlayerField);
                    }
                case ObjectType.GameObject:
                    {
                        int max;
                        Enums.Version.UpdateFields.UpdateFieldMaxOffsets.TryGetValue(typeof(ObjectField), out max);
                        if (offset < max)
                            goto case ObjectType.Object;
                        return typeof(GameObjectField);
                    }
                case ObjectType.DynamicObject:
                    {
                        int max;
                        Enums.Version.UpdateFields.UpdateFieldMaxOffsets.TryGetValue(typeof(ObjectField), out max);
                        if (offset < max)
                            goto case ObjectType.Object;
                        return typeof(DynamicObjectField);
                    }
                case ObjectType.Corpse:
                    {
                        int max;
                        Enums.Version.UpdateFields.UpdateFieldMaxOffsets.TryGetValue(typeof(ObjectField), out max);
                        if (offset < max)
                            goto case ObjectType.Object;
                        return typeof(CorpseField);
                    }
                default:
                    return typeof(Object);
            }
        }

        public static string GetUpdateFieldNameByOffset(Int32 offset, ObjectType type)
        {
            return GetUpdateFieldName(offset, GetUpdateFieldEnumByOffset(offset, type));
        }

        public static Type GetUpdateFieldType<T>(T field)
        {
            if (typeof(T) == typeof(ObjectField))
            {
                switch (Utilities.GetEnumForValue<ObjectField, T>(field))
                {
                    case ObjectField.OBJECT_FIELD_GUID:
                        return typeof(Guid);
                    case ObjectField.OBJECT_FIELD_SCALE_X:
                        return typeof(float);
                    default:
                        return typeof(UInt32);
                }
            }
            else if (typeof(T) == typeof(ItemField))
            {
                switch (Utilities.GetEnumForValue<ItemField, T>(field))
                {
                    case ItemField.ITEM_FIELD_OWNER:
                    case ItemField.ITEM_FIELD_CONTAINED:
                    case ItemField.ITEM_FIELD_CREATOR:
                    case ItemField.ITEM_FIELD_GIFTCREATOR:
                        return typeof(Guid);
                    case ItemField.ITEM_FIELD_FLAGS:
                        return typeof(UInt32);
                    default:
                        return typeof(Int32);
                }
            }
            else if (typeof(T) == typeof(ContainerField))
            {
                switch (Utilities.GetEnumForValue<ContainerField, T>(field))
                {
                    default:
                    {
                        string str = field.ToString();
                        if (str.Contains("CONTAINER_FIELD_SLOT"))
                            return typeof(Guid);
                        return typeof(Int32);
                    }
                }
            }
            else if (typeof(T) == typeof(UnitField))
            {
                switch (Utilities.GetEnumForValue<UnitField, T>(field))
                {
                    case UnitField.UNIT_FIELD_CHARM:
                    case UnitField.UNIT_FIELD_SUMMON:
                    case UnitField.UNIT_FIELD_CRITTER:
                    case UnitField.UNIT_FIELD_CHARMEDBY:
                    case UnitField.UNIT_FIELD_SUMMONEDBY:
                    case UnitField.UNIT_FIELD_CREATEDBY:
                    case UnitField.UNIT_FIELD_TARGET:
                    case UnitField.UNIT_FIELD_CHANNEL_OBJECT:
                        return typeof(Guid);
                    case UnitField.UNIT_CHANNEL_SPELL:
                    case UnitField.UNIT_CREATED_BY_SPELL:
                        return typeof(UInt32);
                    case UnitField.UNIT_FIELD_BYTES_0:
                    case UnitField.UNIT_FIELD_BYTES_1:
                    case UnitField.UNIT_FIELD_BYTES_2:
                    case UnitField.UNIT_FIELD_FLAGS:
                    case UnitField.UNIT_FIELD_FLAGS_2:
                    case UnitField.UNIT_FIELD_AURASTATE:
                    case UnitField.UNIT_DYNAMIC_FLAGS:
                    case UnitField.UNIT_NPC_FLAGS:
                        return typeof(UInt32);
                    case UnitField.UNIT_FIELD_BOUNDINGRADIUS:
                    case UnitField.UNIT_FIELD_COMBATREACH:
                    case UnitField.UNIT_FIELD_MINDAMAGE:
                    case UnitField.UNIT_FIELD_MAXDAMAGE:
                    case UnitField.UNIT_FIELD_MINOFFHANDDAMAGE:
                    case UnitField.UNIT_FIELD_MAXOFFHANDDAMAGE:
                    case UnitField.UNIT_MOD_CAST_SPEED:
                    case UnitField.UNIT_FIELD_ATTACK_POWER_MULTIPLIER:
                    case UnitField.UNIT_FIELD_MINRANGEDDAMAGE:
                    case UnitField.UNIT_FIELD_MAXRANGEDDAMAGE:
                    case UnitField.UNIT_FIELD_MAXHEALTHMODIFIER:
                    case UnitField.UNIT_FIELD_HOVERHEIGHT:
                    case UnitField.UNIT_FIELD_POWER_COST_MULTIPLIER:
                    case UnitField.UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER:
                    case UnitField.UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER:
                        return typeof(Single);
                    default:
                        return typeof(Int32);
                }
            }
            else if (typeof(T) == typeof(PlayerField))
            {
                switch (Utilities.GetEnumForValue<PlayerField, T>(field))
                {
                    case PlayerField.PLAYER_DUEL_ARBITER:
                    case PlayerField.PLAYER_FARSIGHT:
                        return typeof(Guid);
                    case PlayerField.PLAYER__FIELD_KNOWN_TITLES:
                    case PlayerField.PLAYER__FIELD_KNOWN_TITLES1:
                    case PlayerField.PLAYER__FIELD_KNOWN_TITLES2:
                    case PlayerField.PLAYER_FIELD_KNOWN_CURRENCIES:
                        return typeof(ulong);
                    case PlayerField.PLAYER_BYTES:
                    case PlayerField.PLAYER_BYTES_2:
                    case PlayerField.PLAYER_BYTES_3:
                    case PlayerField.PLAYER_FLAGS:
                    case PlayerField.PLAYER_FIELD_BYTES:
                    case PlayerField.PLAYER_FIELD_BYTES2:
                        return typeof(UInt32);
                    case PlayerField.PLAYER_BLOCK_PERCENTAGE:
                    case PlayerField.PLAYER_DODGE_PERCENTAGE:
                    case PlayerField.PLAYER_PARRY_PERCENTAGE:
                    case PlayerField.PLAYER_CRIT_PERCENTAGE:
                    case PlayerField.PLAYER_RANGED_CRIT_PERCENTAGE:
                    case PlayerField.PLAYER_OFFHAND_CRIT_PERCENTAGE:
                    case PlayerField.PLAYER_SPELL_CRIT_PERCENTAGE1:
                    case PlayerField.PLAYER_SHIELD_BLOCK_CRIT_PERCENTAGE:
                    case PlayerField.PLAYER_FIELD_MOD_HEALING_PCT:
                    case PlayerField.PLAYER_FIELD_MOD_HEALING_DONE_PCT:
                        return typeof(Single);
                    default:
                    {
                        string str = field.ToString();
                        if (str.Contains("PLAYER_FIELD_PACK_SLOT"))
                            return typeof(Guid);
                        else if (str.Contains("PLAYER_FIELD_BANK_SLOT"))
                            return typeof(Guid);
                        else if (str.Contains("PLAYER_FIELD_BANKBAG_SLOT"))
                            return typeof(Guid);
                        else if (str.Contains("PLAYER_FIELD_VENDORBUYBACK_SLOT"))
                            return typeof(Guid);
                        else if (str.Contains("PLAYER_FIELD_KEYRING_SLOT"))
                            return typeof(Guid);
                        else if (str.Contains("PLAYER_FIELD_CURRENCYTOKEN_SLOT"))
                            return typeof(Guid);
                        else if (str.Contains("PLAYER_RUNE_REGEN"))
                            return typeof(Single);
                        return typeof(Int32);
                    }
                }
            }
            else if (typeof(T) == typeof(GameObjectField))
            {
                switch (Utilities.GetEnumForValue<GameObjectField, T>(field))
                {
                    case GameObjectField.GAMEOBJECT_FIELD_CREATED_BY:
                        return typeof(Guid);
                    case GameObjectField.GAMEOBJECT_PARENTROTATION:
                        return typeof(Single);
                    case GameObjectField.GAMEOBJECT_DYNAMIC:
                    case GameObjectField.GAMEOBJECT_FLAGS:
                    case GameObjectField.GAMEOBJECT_DISPLAYID:
                    case GameObjectField.GAMEOBJECT_BYTES_1:
                        return typeof(UInt32);
                    default:
                        return typeof(Int32);
                }

            }
            else if (typeof(T) == typeof(DynamicObjectField))
            {
                switch (Utilities.GetEnumForValue<DynamicObjectField, T>(field))
                {
                    case DynamicObjectField.DYNAMICOBJECT_CASTER:
                        return typeof(Guid);
                    case DynamicObjectField.DYNAMICOBJECT_RADIUS:
                        return typeof(Single);
                    case DynamicObjectField.DYNAMICOBJECT_BYTES:
                    case DynamicObjectField.DYNAMICOBJECT_SPELLID:
                    case DynamicObjectField.DYNAMICOBJECT_CASTTIME:
                        return typeof(UInt32);
                    default:
                        return typeof(Int32);
                }
            }
            else if (typeof(T) == typeof(CorpseField))
            {
                switch (Utilities.GetEnumForValue<CorpseField, T>(field))
                {
                    case CorpseField.CORPSE_FIELD_OWNER:
                        return typeof(Guid);
                    case CorpseField.CORPSE_FIELD_PARTY:
                        return typeof(ulong);
                    case CorpseField.CORPSE_FIELD_BYTES_1:
                    case CorpseField.CORPSE_FIELD_BYTES_2:
                    case CorpseField.CORPSE_FIELD_FLAGS:
                    case CorpseField.CORPSE_FIELD_DYNAMIC_FLAGS:
                        return typeof(UInt32);
                    default:
                        return typeof(Int32);
                }
            }
            return typeof(int);
        }

        private static string GetUpdateFieldDictionaryBuildName(ClientVersionBuild build)
        {
            switch (build)
            {
                case ClientVersionBuild.V2_4_3_8606:
                case ClientVersionBuild.V3_0_2_9056:
                case ClientVersionBuild.V3_0_3_9183:
                case ClientVersionBuild.V3_0_8_9464:
                case ClientVersionBuild.V3_0_8a_9506:
                case ClientVersionBuild.V3_0_9_9551:
                case ClientVersionBuild.V3_1_0_9767:
                case ClientVersionBuild.V3_1_1_9806:
                case ClientVersionBuild.V3_1_1a_9835:
                case ClientVersionBuild.V3_1_2_9901:
                case ClientVersionBuild.V3_1_3_9947:
                case ClientVersionBuild.V3_2_0_10192:
                case ClientVersionBuild.V3_2_0a_10314:
                case ClientVersionBuild.V3_2_2_10482:
                case ClientVersionBuild.V3_2_2a_10505:
                case ClientVersionBuild.V3_3_0_10958:
                case ClientVersionBuild.V3_3_0a_11159:
                {
                    return "V3_3_0_10958";
                }
                case ClientVersionBuild.V3_3_3_11685:
                case ClientVersionBuild.V3_3_3a_11723:
                case ClientVersionBuild.V3_3_5a_12340:
                {
                    return "V3_3_5a_12340";
                }
                case ClientVersionBuild.V4_0_3_13329:
                {
                    return "V4_0_3_13329";
                }
                case ClientVersionBuild.V4_0_6_13596:
                case ClientVersionBuild.V4_0_6a_13623:
                case ClientVersionBuild.V4_1_0_13914:
                case ClientVersionBuild.V4_1_0a_14007:
                {
                    return "V4_0_6_13596";
                }
                case ClientVersionBuild.V4_2_0_14333:
                case ClientVersionBuild.V4_2_0a_14480:
                {
                    return "V4_2_0_14480";
                }
                case ClientVersionBuild.V4_2_2_14545:
                {
                    return "V4_2_2_14545";
                }
                case ClientVersionBuild.V4_3_0_15005:
                case ClientVersionBuild.V4_3_0_15050:
                {
                    return "V4_3_0_15005";
                }
                case ClientVersionBuild.V4_3_2_15211:
                {
                    return "V4_3_2_15211";
                }
                case ClientVersionBuild.V4_3_3_15354:
                {
                    return "V4_3_3_15354";
                }
                case ClientVersionBuild.V4_3_4_15595:
                {
                    return "V4_3_4_15595";
                }
                default:
                {
                    return "V3_3_5a_12340";
                }
            }
        }
    }
}
