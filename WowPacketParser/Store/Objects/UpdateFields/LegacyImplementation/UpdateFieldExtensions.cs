using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields.LegacyImplementation
{
    public static class UpdateFieldExtensions
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
            throw new ArgumentException($"Type must be one of int, uint, float or its nullable counterpart but was {type.Name}");
        }

        /// <summary>
        /// Grabs a value from a dictionary of UpdateFields
        /// </summary>
        /// <typeparam name="T">The type of UpdateField (ObjectField, UnitField, ...)</typeparam>
        /// <typeparam name="TK">The type of the value (int, uint or float and their nullable counterparts)</typeparam>
        /// <param name="dict">The dictionary</param>
        /// <param name="updateField">The update field we want</param>
        /// <returns></returns>
        public static TK GetValue<T, TK>(this Dictionary<int, UpdateField> dict, T updateField) where T: Enum
        {
            UpdateField uf;
            if (dict.TryGetValue(Enums.Version.UpdateFields.GetUpdateField(updateField), out uf))
            {
                var type = GetTypeCodeOfReturnValue<TK>();
                switch (type)
                {
                    case TypeCode.UInt32:
                        return (TK)(object)uf.UInt32Value;
                    case TypeCode.Int32:
                        return (TK)(object)(int)uf.UInt32Value;
                    case TypeCode.Single:
                        return (TK)(object)uf.FloatValue;
                    case TypeCode.Double:
                        return (TK)(object)(double)uf.FloatValue;
                    default:
                        break;
                }
            }

            return default(TK);
        }

        /// <summary>
        /// Grabs a value list from a dictionary of dynamic UpdateFields
        /// </summary>
        /// <typeparam name="T">The type of UpdateField (UnitDynamicField, ...)</typeparam>
        /// <typeparam name="TK">The type of the value (int, uint or float and their nullable counterparts)</typeparam>
        /// <param name="dict">The dictionary</param>
        /// <param name="updateField">The update field we want</param>
        /// <returns></returns>
        public static IEnumerable<TK> GetValue<T, TK>(this Dictionary<int, List<UpdateField>> dict, T updateField) where T: Enum
        {
            List<UpdateField> ufs;
            if (dict.TryGetValue(Enums.Version.UpdateFields.GetUpdateField(updateField), out ufs))
            {
                var type = GetTypeCodeOfReturnValue<TK>();
                switch (type)
                {
                    case TypeCode.UInt32:
                        return ufs.Select(uf => (TK)(object)uf.UInt32Value);
                    case TypeCode.Int32:
                        return ufs.Select(uf => (TK)(object)(int)uf.UInt32Value);
                    case TypeCode.Single:
                        return ufs.Select(uf => (TK)(object)uf.FloatValue);
                    case TypeCode.Double:
                        return ufs.Select(uf => (TK)(object)(double)uf.FloatValue);
                    default:
                        break;
                }
            }

            return Enumerable.Empty<TK>();
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
        public static TK[] GetArray<T, TK>(this Dictionary<int, UpdateField> dict, T firstUpdateField, int count) where T : Enum
        {
            var result = new TK[count];
            var type = GetTypeCodeOfReturnValue<TK>();
            for (var i = 0; i < count; i++)
            {
                UpdateField uf;
                var updateField = Enums.Version.UpdateFields.GetUpdateField(firstUpdateField);
                if (updateField == -1)
                    continue;
                if (dict.TryGetValue(updateField + i, out uf))
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
                            result[i] = (TK)(object)uf.FloatValue;
                            break;
                        case TypeCode.Double:
                            result[i] = (TK)(object)(double)uf.FloatValue;
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
        public static TK GetEnum<T, TK>(this Dictionary<int, UpdateField> dict, T updateField) where T : Enum
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
