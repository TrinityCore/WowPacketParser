using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Threading;
using Google.Protobuf.Collections;
using WowPacketParser.Enums;
using WowPacketParser.Proto;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParser.Misc
{
    public static class Extensions
    {
        /// <summary>
        /// Convert bool to byte
        /// </summary>
        /// <param name="value">A boolean</param>
        /// <returns>A byte</returns>
        public static byte ToByte(this bool value)
        {
            return (byte)(value ? 1 : 0);
        }

        /// <summary>
        /// Returns true if flag exists in value (&)
        /// </summary>
        /// <param name="value">An enum, int, ...</param>
        /// <param name="flag">An enum, int, ...</param>
        /// <returns>A boolean</returns>
        public static bool HasAnyFlag(this IConvertible value, IConvertible flag)
        {
            var uFlag = flag.ToUInt64(null);
            var uThis = value.ToUInt64(null);

            return (uThis & uFlag) != 0;
        }

        /// <summary>
        /// Returns true if bit is set in value (&)
        /// </summary>
        /// <param name="value">An enum, int, ...</param>
        /// <param name="bit">An int</param>
        /// <returns>A boolean</returns>
        public static bool HasAnyFlagBit(this IConvertible value, IConvertible bit)
        {
            var uBit = bit.ToInt32(null);

            Contract.Assert(uBit >= 0 && uBit <= 63);

            var uFlag = 1UL << uBit;
            var uThis = value.ToUInt64(null);

            return (uThis & uFlag) != 0;
        }

        /// <summary>
        /// Returns true if bit is set in value (&)
        /// </summary>
        /// <param name="value">An enum, int, ...</param>
        /// <param name="bit">An int</param>
        /// <returns>A boolean</returns>
        public static bool HasAnyFlagBit(this UInt128 value, IConvertible bit) // uint128 doesnt implement IConvertible
        {
            var uBit = bit.ToInt32(null);

            Contract.Assert(uBit >= 0 && uBit <= 127);

            var uFlag = ((UInt128)1) << uBit;
            var uThis = value;

            return (uThis & uFlag) != 0;
        }

        /// <summary>
        /// Return true if our string is a substring of any filter (case insensitive)
        /// </summary>
        /// <param name="value">String</param>
        /// <param name="filters">List of strings</param>
        /// <returns>A boolean</returns>
        public static bool MatchesFilters(this string value, IEnumerable<string> filters)
        {
            // Note: IndexOf returns -1 if string was not found
            return filters.Any(filter => value.IndexOf(filter, StringComparison.InvariantCultureIgnoreCase) != -1);
        }

        /// <summary>
        /// Shows our hex representation of a packet
        /// </summary>
        /// <param name="packet">A packet</param>
        public static void AsHex(this Packet packet)
        {
            packet.WriteLine(Utilities.ByteArrayToHexTable(packet.GetStream(0)));
        }

        /// <summary>
        /// <para>Define the culture of the thread as CultureInfo.InvariantCulture</para>
        /// <remarks>This is required since new threads will have the culture of the machine (to be changed in .NET 4.5)</remarks>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ParallelQuery<TSource> SetCulture<TSource>(this ParallelQuery<TSource> source)
        {
            SetCulture(CultureInfo.InvariantCulture);
            return source
                .Select(
                    item =>
                        {
                            SetCulture(CultureInfo.InvariantCulture);
                            return item;
                        });
        }

        private static void SetCulture(CultureInfo cultureInfo)
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
        }

        /// <summary>
        /// Converts a timespan in a string (hh:mm:ss.ms)
        /// </summary>
        /// <param name="span">A timespan</param>
        /// <returns>A string</returns>
        public static string ToFormattedString(this TimeSpan span)
        {
            return $"{span.Hours:00}:{span.Minutes:00}:{span.Seconds:00}.{span.Milliseconds:000}";
        }

        public static void Clear<T>(this ConcurrentBag<T> bag)
        {
            T t;
            while (bag.Count > 0)
                bag.TryTake(out t);
        }

        /// <summary>
        /// Compare two dictionaries
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionaries</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionaries</typeparam>
        /// <param name="first">First dictionary</param>
        /// <param name="second">Second dictionary</param>
        /// <returns>true if dictionaries are equal, false otherwise</returns>
        public static bool DictionaryEqual<TKey, TValue>(this IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second)
        {
            if (first == second) return true;
            if ((first == null) || (second == null)) return false;
            if (first.Count != second.Count) return false;

            var comparer = EqualityComparer<TValue>.Default;

            foreach (var kvp in first)
            {
                TValue secondValue;
                if (!second.TryGetValue(kvp.Key, out secondValue)) return false;
                if (!comparer.Equals(kvp.Value, secondValue)) return false;
            }
            return true;
        }

        /// <summary>
        /// Flattens an IEnumerable
        /// Example:
        /// [1, 2, [3, [4]], 5] -> [1, 2, 3, 4, 5]
        /// </summary>
        /// <typeparam name="T">Type of each object</typeparam>
        /// <param name="values">Input IEnumerable</param>
        /// <returns>Flatten result</returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> values)
        {
            foreach (var item in values)
            {
                if (!(item is IEnumerable<T>))
                    yield return item;
                var childs = item as IEnumerable<T>;
                if (childs == null) continue;
                foreach (var child in childs.Flatten())
                {
                    yield return child;
                }
            }
        }

        public static string GetExtension(this FileCompression value)
        {
            var attributes = (FileCompressionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(FileCompressionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Extension : "";
        }

        public static FileCompression ToFileCompressionEnum(this string str)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (FileCompression item in Enum.GetValues(typeof(FileCompression)))
            {
                var attributes = (FileCompressionAttribute[])item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(FileCompressionAttribute), false);
                if (attributes.Length > 0 && (attributes[0].Extension.Equals(str.ToLower())))
                    return item;
            }

            return FileCompression.None;
        }

        public static int BinarySearch<TKey, TValue>(this SortedList<TKey, TValue> sortedList, TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var array = sortedList.Keys;
            var comparer = sortedList.Comparer;
            var lo = 0;
            var hi = sortedList.Count - 1;
            while (lo <= hi)
            {
                var i = lo + ((hi - lo) >> 1);
                var order = comparer.Compare(array[i], key);
                if (order == 0)
                    return i;

                if (order < 0)
                    lo = i + 1;
                else
                    hi = i - 1;
            }

            return ~lo;
        }

        public static void Reserve<T>(this RepeatedField<T> field, int count) where T : new()
        {
            while (field.Count < count)
                field.Add(new T());
        }

        public static void UpdateData(this UpdateValuesObjectDataFields fields, IObjectData data)
        {
            fields.EntryID = data.EntryID;
            fields.Scale = data.Scale;
            fields.DynamicFlags = data.DynamicFlags;
        }
        
        public static void UpdateData(this UpdateValuesObjectDataFields fields, IGameObjectData data)
        {
            var go = fields.Gameobject ??= new();
            if (data.CreatedBy != null)
                go.CreatedBy = data.CreatedBy;
            go.Flags = data.Flags;
            if (data.ParentRotation.HasValue)
                go.ParentRotation = data.ParentRotation;
            go.FactionTemplate = data.FactionTemplate;
            go.State = data.State;
            go.TypeID = data.TypeID;
            go.PercentHealth = data.PercentHealth;
            go.DisplayID = data.DisplayID;
            go.ArtKit = data.ArtKit;
            go.Level = data.Level;
        }

        public static void UpdateData(this UpdateValuesObjectDataFields fields, IUnitData data)
        {
            var unit = fields.Unit ??= new();
            unit.DisplayID = data.DisplayID;
            unit.ClassId = data.ClassId;
            unit.Sex = data.Sex;
            unit.Health = data.Health;
            unit.MaxHealth = data.MaxHealth;
            unit.Level = data.Level;
            unit.ContentTuningID = data.ContentTuningID;
            unit.ScalingLevelMin = data.ScalingLevelMin;
            unit.ScalingLevelMax = data.ScalingLevelMax;
            unit.ScalingLevelDelta = data.ScalingLevelDelta;
            unit.FactionTemplate = data.FactionTemplate;
            unit.Flags = data.Flags;
            unit.Flags2 = data.Flags2;
            unit.Flags3 = data.Flags3;
            unit.RangedAttackRoundBaseTime = data.RangedAttackRoundBaseTime;
            unit.BoundingRadius = data.BoundingRadius;
            unit.CombatReach = data.CombatReach;
            unit.MountDisplayID = data.MountDisplayID;
            unit.StandState = data.StandState;
            unit.PetTalentPoints = data.PetTalentPoints;
            unit.VisFlags = data.VisFlags;
            unit.AnimTier = data.AnimTier;
            unit.CreatedBySpell = data.CreatedBySpell;
            unit.EmoteState = data.EmoteState;
            unit.SheatheState = data.SheatheState;
            unit.PvpFlags = data.PvpFlags;
            unit.PetFlags = data.PetFlags;
            unit.ShapeshiftForm = data.ShapeshiftForm;
            unit.HoverHeight = data.HoverHeight;
            unit.InteractSpellID = data.InteractSpellID;
            unit.StateSpellVisualID = data.StateSpellVisualID;
            unit.StateAnimID = data.StateAnimID;
            unit.StateAnimKitID = data.StateAnimKitID;
            unit.Race = data.Race;
            unit.DisplayPower = data.DisplayPower;
            unit.EffectiveLevel = data.EffectiveLevel;
            unit.AuraState = data.AuraState;
            unit.DisplayScale = data.DisplayScale;
            unit.CreatureFamily = data.CreatureFamily;
            unit.CreatureType = data.CreatureType;
            unit.NativeDisplayID = data.NativeDisplayID;
            unit.NativeXDisplayScale = data.NativeXDisplayScale;
            unit.BaseMana = data.BaseMana;
            unit.BaseHealth = data.BaseHealth;
            if (data.Charm != null)
                unit.Charm = data.Charm;
            if (data.Summon != null)
                unit.Summon = data.Summon;
            if (data.Critter != null)
                unit.Critter = data.Critter;
            if (data.CharmedBy != null)
                unit.CharmedBy = data.CharmedBy;
            if (data.DemonCreator != null)
                unit.DemonCreator = data.DemonCreator;
            if (data.LookAtControllerTarget != null)
                unit.LookAtControllerTarget = data.LookAtControllerTarget;
            if (data.Target != null)
                unit.Target = data.Target;
            if (data.SummonedBy != null)
                unit.SummonedBy = data.SummonedBy;
            if (data.CreatedBy != null)
                unit.CreatedBy = data.CreatedBy;
            unit.NpcFlags.Reserve(data.NpcFlags.Length);
            for (int i = 0; i < data.NpcFlags.Length; ++i)
                unit.NpcFlags[i] = data.NpcFlags[i].ToProto();
            
            unit.Power.Reserve(data.Power.Length);
            for (int i = 0; i < data.Power.Length; ++i)
                unit.Power[i] = data.Power[i].ToProto();
            
            unit.MaxPower.Reserve(data.MaxPower.Length);
            for (int i = 0; i < data.MaxPower.Length; ++i)
                unit.MaxPower[i] = data.MaxPower[i].ToProto();
            
            unit.AttackRoundBaseTime.Reserve(data.AttackRoundBaseTime.Length);
            for (int i = 0; i < data.AttackRoundBaseTime.Length; ++i)
                unit.AttackRoundBaseTime[i] = data.AttackRoundBaseTime[i].ToProto();
            
            unit.Resistances.Reserve(data.Resistances.Length);
            for (int i = 0; i < data.Resistances.Length; ++i)
                unit.Resistances[i] = data.Resistances[i].ToProto();
            
            unit.VirtualItems.Reserve(data.VirtualItems.Length);
            for (int i = 0; i < data.VirtualItems.Length; ++i)
                if (data.VirtualItems[i] != null)
                    unit.VirtualItems[i] = data.VirtualItems[i].ToProto();
        }
        
        public static UInt32ValueWrapper ToProto(this uint? value)
        {
            return new UInt32ValueWrapper() { Value = value };
        }
        
        public static Int32ValueWrapper ToProto(this int? value)
        {
            return new Int32ValueWrapper() { Value = value };
        }

        public static VisibleItemFields ToProto(this IVisibleItem item)
        {
            return new()
            {
                ItemID = item.ItemID ?? 0,
                ItemVisual = item.ItemVisual ?? 0,
                ItemAppearanceModID = item.ItemAppearanceModID ?? 0
            };
        }

        public static bool IsUniversalProtobufType(this DumpFormatType type)
        {
            return type is DumpFormatType.UniversalProto or DumpFormatType.UniversalProtoWithText
                or DumpFormatType.UniversalProtoWithSeparateText;
        }
    }
}
