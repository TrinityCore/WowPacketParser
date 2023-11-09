using MySql.Data.MySqlClient;
using Sigil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using WowPacketParser.Misc;

namespace WowPacketParser.Hotfix
{
    public abstract class IHotfixSerializer<T> where T : class, new()
    {
        // Filter for typeof(Packet).GetMethod to skip the generic variant of ReadXXX used for enums
        private class NonGenericBinder : Binder
        {
            public static NonGenericBinder Instance { get; } = new NonGenericBinder();

            public override FieldInfo BindToField(BindingFlags bindingAttr, FieldInfo[] match, object value, CultureInfo culture)
            {
                return null;
            }

            public override MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match, ref object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] names, out object state)
            {
                state = null;
                return null;
            }

            public override object ChangeType(object value, Type type, CultureInfo culture)
            {
                return null;
            }

            public override void ReorderArgumentArray(ref object[] args, object state)
            {
            }

            public override MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[] modifiers)
            {
                return match.Where(m => !m.IsGenericMethod).SingleOrDefault();
            }

            public override PropertyInfo SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, Type returnType, Type[] indexes, ParameterModifier[] modifiers)
            {
                return null;
            }
        }

        // ReSharper disable once StaticMemberInGenericType
        protected static Dictionary<TypeCode, MethodInfo> _binaryReaders { get; } = new Dictionary<TypeCode, MethodInfo>
        {
            { TypeCode.SByte, typeof(Packet).GetMethod("ReadSByte", BindingFlags.Public | BindingFlags.Instance, NonGenericBinder.Instance, new [] { typeof(string), typeof(object[]) }, null) },
            { TypeCode.Byte, typeof(Packet).GetMethod("ReadByte", BindingFlags.Public | BindingFlags.Instance, NonGenericBinder.Instance, new [] { typeof(string), typeof(object[]) }, null) },
            { TypeCode.Int16, typeof(Packet).GetMethod("ReadInt16", BindingFlags.Public | BindingFlags.Instance, NonGenericBinder.Instance, new [] { typeof(string), typeof(object[]) }, null) },
            { TypeCode.UInt16, typeof(Packet).GetMethod("ReadUInt16", BindingFlags.Public | BindingFlags.Instance, NonGenericBinder.Instance, new [] { typeof(string), typeof(object[]) }, null) },
            { TypeCode.Int32, typeof(Packet).GetMethod("ReadInt32", BindingFlags.Public | BindingFlags.Instance, NonGenericBinder.Instance, new [] { typeof(string), typeof(object[]) }, null) },
            { TypeCode.UInt32, typeof(Packet).GetMethod("ReadUInt32", BindingFlags.Public | BindingFlags.Instance, NonGenericBinder.Instance, new [] { typeof(string), typeof(object[]) }, null) },
            { TypeCode.Int64, typeof(Packet).GetMethod("ReadInt64", BindingFlags.Public | BindingFlags.Instance, NonGenericBinder.Instance, new [] { typeof(string), typeof(object[]) }, null) },
            { TypeCode.UInt64, typeof(Packet).GetMethod("ReadUInt64", BindingFlags.Public | BindingFlags.Instance, NonGenericBinder.Instance, new [] { typeof(string), typeof(object[]) }, null) },
            { TypeCode.Single, typeof(Packet).GetMethod("ReadSingle", BindingFlags.Public | BindingFlags.Instance, NonGenericBinder.Instance, new [] { typeof(string), typeof(object[]) }, null) },
            { TypeCode.Double, typeof(Packet).GetMethod("ReadDouble", BindingFlags.Public | BindingFlags.Instance, NonGenericBinder.Instance, new [] { typeof(string), typeof(object[]) }, null) },
            { TypeCode.String, typeof(Packet).GetMethod("ReadCString", BindingFlags.Public | BindingFlags.Instance, NonGenericBinder.Instance, new [] { typeof(string), typeof(object[]) }, null) }
        };

        protected Func<Packet, T> _deserializer;
        private Action<T, StringBuilder /* hotfixBuilder */, StringBuilder /* localeBuilder */> _serializer;

        #region Static helpers for SQL serialization
        // ReSharper disable StaticMemberInGenericType
        private static MethodInfo stringReplace = typeof(string).GetMethod("Replace", new[] { typeof(string), typeof(string) });
        private static MethodInfo stringFormat = typeof(string).GetMethod("Format", new[] { typeof(string), typeof(object) });
        private static MethodInfo stringBuilderAppend = typeof(StringBuilder).GetMethod("Append", new[] { typeof(string) });
        private static MethodInfo stringEscape = typeof(MySqlHelper).GetMethod("EscapeString", new[] { typeof(string) });
        // ReSharper restore StaticMemberInGenericType

        private static void SerializeValue(PropertyInfo propInfo,
            Emit<Action<T, StringBuilder, StringBuilder>> serializationEmitter)
        {
            var isString = propInfo.PropertyType == typeof(string);

            using (var stringLocal = serializationEmitter.DeclareLocal<string>())
            {
                serializationEmitter.LoadConstant(isString ? @"'{0}', " : "{0}, ");
                serializationEmitter.LoadArgument(0); // instance
                serializationEmitter.CallVirtual(propInfo.GetGetMethod());
                if (isString)
                {
                    serializationEmitter.Call(stringEscape);
                    serializationEmitter.LoadConstant(Environment.NewLine);
                    serializationEmitter.LoadConstant(@"\n");
                    serializationEmitter.CallVirtual(stringReplace);
                }
                else
                    serializationEmitter.Box(propInfo.PropertyType);
                serializationEmitter.Call(stringFormat);
                serializationEmitter.StoreLocal(stringLocal);

                // Append to hotfix builder
                serializationEmitter.LoadArgument(1); // hotfixBuilder
                serializationEmitter.LoadLocal(stringLocal);
                serializationEmitter.Call(stringBuilderAppend);
                serializationEmitter.Pop();

                if (isString)
                {
                    // Append to locale builder if (localeBuilder != null)
                    var skipLocaleBuilderMark = serializationEmitter.DefineLabel();
                    serializationEmitter.LoadArgument(2); // instanceBuilder
                    serializationEmitter.LoadNull();
                    serializationEmitter.CompareEqual();
                    serializationEmitter.BranchIfTrue(skipLocaleBuilderMark);
                    serializationEmitter.LoadArgument(2); // instanceBuilder
                    serializationEmitter.LoadLocal(stringLocal);
                    serializationEmitter.Call(stringBuilderAppend);
                    serializationEmitter.Pop();
                    serializationEmitter.MarkLabel(skipLocaleBuilderMark);
                }
            }
        }

        private static void SerializeValueArray(PropertyInfo propInfo,
            Emit<Action<T, StringBuilder, StringBuilder>> serializationEmitter)
        {
            var loopBodyLabel = serializationEmitter.DefineLabel();
            var loopConditionLabel = serializationEmitter.DefineLabel();

            var elementType = propInfo.PropertyType.GetElementType();
            var isString = elementType == typeof(string);

            using (var stringLocal = serializationEmitter.DeclareLocal<string>())
            using (var iterationLocal = serializationEmitter.DeclareLocal<int>())
            {
                serializationEmitter.LoadConstant(0);
                serializationEmitter.StoreLocal(iterationLocal);
                serializationEmitter.Branch(loopConditionLabel);
                serializationEmitter.MarkLabel(loopBodyLabel);

                serializationEmitter.LoadConstant(isString ? @"'{0}', " : "{0}, ");
                serializationEmitter.LoadArgument(0); // instance
                serializationEmitter.CallVirtual(propInfo.GetGetMethod());
                serializationEmitter.LoadLocal(iterationLocal);
                serializationEmitter.LoadElement(elementType);
                if (isString)
                {
                    serializationEmitter.Call(stringEscape);
                    serializationEmitter.LoadConstant(Environment.NewLine);
                    serializationEmitter.LoadConstant(@"\n");
                    serializationEmitter.CallVirtual(stringReplace);
                }
                else
                    serializationEmitter.Box(elementType);
                serializationEmitter.Call(stringFormat);
                serializationEmitter.StoreLocal(stringLocal);

                // Append to hotfix builder
                serializationEmitter.LoadArgument(1); // hotfixBuilder
                serializationEmitter.LoadLocal(stringLocal);
                serializationEmitter.Call(stringBuilderAppend);
                serializationEmitter.Pop();

                if (isString)
                {
                    // Append to locale builder if (localeBuilder != null)
                    var localeBuilderMarker = serializationEmitter.DefineLabel();
                    serializationEmitter.LoadArgument(2); // instanceBuilder
                    serializationEmitter.LoadNull();
                    serializationEmitter.CompareEqual();
                    serializationEmitter.BranchIfTrue(localeBuilderMarker);
                    serializationEmitter.LoadArgument(2); // instanceBuilder
                    serializationEmitter.LoadLocal(stringLocal);
                    serializationEmitter.Call(stringBuilderAppend);
                    serializationEmitter.Pop();
                    serializationEmitter.MarkLabel(localeBuilderMarker);
                }

                serializationEmitter.LoadLocal(iterationLocal);
                serializationEmitter.LoadConstant(1);
                serializationEmitter.Add();
                serializationEmitter.StoreLocal(iterationLocal);

                serializationEmitter.MarkLabel(loopConditionLabel);
                serializationEmitter.LoadLocal(iterationLocal);
                serializationEmitter.LoadArgument(0); // instance
                serializationEmitter.CallVirtual(propInfo.GetGetMethod());
                serializationEmitter.LoadLength(elementType);
                serializationEmitter.Convert<int>();
                serializationEmitter.CompareLessThan();
                serializationEmitter.BranchIfTrue(loopBodyLabel);
            }
        }
        #endregion

        public void GenerateSerializer()
        {
            try
            {
                // var asmName = new AssemblyName("Serializer_" + typeof(T).Name);
                // var asm = AppDomain.CurrentDomain.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.Save);
                // var mod = asm.DefineDynamicModule(asmName.Name, asmName.Name + ".dll");
                // var typeBuilder = mod.DefineType("MyType", TypeAttributes.Public | TypeAttributes.Abstract);

                var serializationEmitter = Emit<Action<T, StringBuilder, StringBuilder>>.NewDynamicMethod();
                // var serializationEmitter = Emit<Action<T, StringBuilder, StringBuilder>>.BuildMethod(typeBuilder, "Build", MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard);

                foreach (var propInfo in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).
                    Where(propInfo => propInfo.GetGetMethod() != null && propInfo.GetSetMethod() != null && ShouldRead(propInfo)))
                {
                    if (propInfo.PropertyType.IsArray)
                        SerializeValueArray(propInfo, serializationEmitter);
                    else
                        SerializeValue(propInfo, serializationEmitter);
                }
                serializationEmitter.Return();
                _serializer = serializationEmitter.CreateDelegate();

                // serializationEmitter.CreateMethod();
                // typeBuilder.CreateType();
                // asm.Save(asmName.Name + ".dll");
            }
            catch (SigilVerificationException sve)
            {
                Console.WriteLine(sve);
            }
        }

        public abstract void GenerateDeserializer();

        protected static bool ShouldRead(MemberInfo propInfo)
        {
            var removedAttr = propInfo.GetCustomAttributes<HotfixVersionAttribute>();

            foreach (var attr in removedAttr)
            {
                if (attr.RemovedInVersion)
                {
                    if (!ClientVersion.RemovedInVersion(attr.Build))
                        return false;
                }
                else if (!ClientVersion.AddedInVersion(attr.Build))
                    return false;
            }

            return true;
        }

        public T Deserialize(Packet packet) => _deserializer(packet);

        public string[] PosLetters = new string[]{ "X", "Y", "Z" };
        public void SerializeStore(HotfixStore<T> store, StringBuilder hotfixBuilder, StringBuilder localeBuilder)
        {
            if (store.Records.Count == 0)
                return;

            bool isLocale = false;
            var hotfixStructureAttribute = typeof (T).GetCustomAttribute<HotfixStructureAttribute>();
            Debug.Assert(hotfixStructureAttribute != null);

            var propertiesInfos = typeof (T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            // Synch with TC
            var normalizedTableHashString = hotfixStructureAttribute.Hash.ToString()
                .Replace("GameObject", "Gameobject")
                .Replace("PvP", "Pvp")
                .Replace("PVP", "Pvp")
                .Replace("QuestXP", "QuestXp")
                .Replace("WMO", "Wmo")
                .Replace("AddOn", "Addon")
                .Replace("LFG", "Lfg")
                .Replace("_", "");

            var tableName = Regex.Replace(normalizedTableHashString, @"(?<=[a-z])([A-Z])|(?<=[A-Z])([A-Z][a-z])",
                @"_$1$2", RegexOptions.Compiled).ToLower();

            if (localeBuilder != null && propertiesInfos.Any(
                propInfo => propInfo.PropertyType == typeof (string) || propInfo.PropertyType == typeof (string[])))
            {
                switch (tableName)
                {
                    case "achievement":
                    case "achievement_category":
                    case "adventure_journal":
                    case "adventure_map_poi":
                    case "area_table":
                    case "artifact_appearance":
                    case "artifact_appearance_set":
                    case "artifact":
                    case "auction_house":
                    case "azerite_essence":
                    case "azerite_essence_power":
                    case "barber_shop_style":
                    case "battlemaster_list":
                    case "battle_pet_ability":
                    case "battle_pet_species":
                    case "broadcast_text":
                    case "char_titles":
                    case "chat_channels":
                    case "chr_classes":
                    case "chr_customization_choice":
                    case "chr_customization_option":
                    case "chr_races":
                    case "chr_specialization":
                    case "creature_family":
                    case "creature_type":
                    case "criteria_tree":
                    case "currency_container":
                    case "currency_types":
                    case "difficulty":
                    case "dungeon_encounter":
                    case "faction":
                    case "friendship_rep_reaction":
                    case "friendship_reputation":
                    case "gameobjects":
                    case "garr_ability":
                    case "garr_building":
                    case "garr_class_spec":
                    case "garr_follower":
                    case "garr_mission":
                    case "heirloom":
                    case "item_bag_family":
                    case "item_class":
                    case "item_limit_category":
                    case "item_name_description":
                    case "item_search_name":
                    case "item_set":
                    case "item_sparse":
                    case "journal_encounter":
                    case "journal_encounter_section":
                    case "journal_instance":
                    case "journal_tier":
                    case "keystone_affix":
                    case "languages":
                    case "lfg_dungeons":
                    case "mail_template":
                    case "map_challenge_mode":
                    case "map_difficulty":
                    case "map":
                    case "mount":
                    case "player_condition":
                    case "prestige_level_info":
                    case "pvp_talent":
                    case "pvp_tier":
                    case "quest_sort":
                    case "scenario":
                    case "scenario_step":
                    case "server_messages":
                    case "skill_line":
                    case "specialization_spells":
                    case "spell_category":
                    case "spell_focus_object":
                    case "spell_item_enchantment":
                    case "spell_name":
                    case "spell_range":
                    case "spell_shapeshift_form":
                    case "talent":
                    case "taxi_nodes":
                    case "totem_category":
                    case "toy":
                    case "transmog_set_group":
                    case "transmog_set":
                    case "ui_map":
                    case "unit_power_bar":
                    case "wmo_area_table":
                        break;
                    default:
                        return;
                }

                if (tableName == "broadcast_text")
                {
                    var remainingCountDelete = store.Records.Count - 1;

                    localeBuilder.Append($"DELETE FROM `{tableName}_locale` WHERE `locale` = '{ClientLocale.PacketLocale}' AND `ID` IN (");
                    foreach (var kv in store.Records)
                    {
                        localeBuilder?.Append($"{kv.Key}");
                        localeBuilder?.Append(remainingCountDelete > 0 ? $", " : $");" + Environment.NewLine);
                        --remainingCountDelete;
                    }
                }
                else
                    localeBuilder.AppendLine($"DELETE FROM `{tableName}_locale` WHERE `locale` = '{ClientLocale.PacketLocale}' AND `VerifiedBuild`>0;");
                localeBuilder.Append($"INSERT INTO `{tableName}_locale` (");
                localeBuilder.Append("`ID`, ");
                localeBuilder.Append("`locale`, ");
                isLocale = true;
            }

            if (tableName == "broadcast_text")
            {
                var remainingCountDelete = store.Records.Count - 1;

                hotfixBuilder.Append($"DELETE FROM `{tableName}` WHERE `VerifiedBuild`>0 AND `ID` IN (");
                foreach (var kv in store.Records)
                {
                    hotfixBuilder.Append($"{kv.Key}");
                    hotfixBuilder.Append(remainingCountDelete > 0 ? $", " : $");" + Environment.NewLine);
                    --remainingCountDelete;
                }
            }
            else
                hotfixBuilder.AppendLine($"DELETE FROM `{tableName}` WHERE `VerifiedBuild`>0;");

            hotfixBuilder.Append($"INSERT INTO `{tableName}` (");
            if (!hotfixStructureAttribute.HasIndexInData)
                hotfixBuilder.Append("`ID`, ");

            foreach (var propInfo in propertiesInfos)
            {
                if (!ShouldRead(propInfo))
                    continue;

                if (propInfo.PropertyType.IsArray)
                {
                    var isString = propInfo.PropertyType.GetElementType() == typeof (string);

                    var arraySizeAttribute = propInfo.GetCustomAttribute<HotfixArrayAttribute>();
                    for (var i = 0; i < arraySizeAttribute.Size; ++i)
                    {
                        if (arraySizeAttribute.IsPosition)
                        {
                            hotfixBuilder.Append($"`{propInfo.Name}{ PosLetters[i] }`, ");
                            if (localeBuilder != null && isString)
                                localeBuilder.Append($"{propInfo.Name}{ PosLetters[i] }_lang`, ");
                        }
                        else
                        {
                            hotfixBuilder.Append($"`{propInfo.Name}{ i + 1 }`, ");
                            if (localeBuilder != null && isString)
                                localeBuilder.Append($"{propInfo.Name}{ i + 1 }_lang`, ");
                        }
                    }
                }
                else
                {
                    hotfixBuilder.Append($"`{propInfo.Name}`, ");
                    if (localeBuilder != null && propInfo.PropertyType == typeof (string))
                        localeBuilder.Append($"`{propInfo.Name}_lang`, ");
                }
            }

            hotfixBuilder.AppendLine("`VerifiedBuild`) VALUES");
            if (isLocale)
                localeBuilder?.AppendLine("`VerifiedBuild`) VALUES");

            var remainingCount = store.Records.Count - 1;

            foreach (var kv in store.Records)
            {
                hotfixBuilder.Append("(");
                if (isLocale)
                    localeBuilder?.Append("(");

                if (!hotfixStructureAttribute.HasIndexInData)
                    hotfixBuilder.Append($"{kv.Key}, ");

                if (isLocale)
                    localeBuilder?.Append($"{kv.Key}, ");

                if (isLocale)
                    localeBuilder?.Append($"'{ClientLocale.PacketLocale}', ");
                _serializer((T)kv.Value, hotfixBuilder, localeBuilder);

                hotfixBuilder.AppendLine(remainingCount > 0 ? $"{ClientVersion.BuildInt})," : $"{ClientVersion.BuildInt});");
                if (isLocale)
                    localeBuilder?.AppendLine(remainingCount > 0 ? $"{ClientVersion.BuildInt})," : $"{ClientVersion.BuildInt});");
                --remainingCount;
            }

            hotfixBuilder.AppendLine();
            if (isLocale)
                localeBuilder?.AppendLine();
        }
    }
}
