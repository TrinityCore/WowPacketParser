using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version
{
    public static class UpdateFields
    {
        private static Dictionary<Type, BiDictionary<string, int>> LoadDefaultUFDictionaries()
        {
            var dicts = new Dictionary<Type, BiDictionary<string, int>>();

            LoadUFDictionariesInto(dicts, Assembly.GetExecutingAssembly(), ClientVersionBuild.Zero);

            return dicts;
        }

        // TEMPORARY HACK
        // This is needed because currently opcode handlers are only registersd if version is matching
        // so we need to clear them and reinitialize default handlers
        // This will be obsolete when all version specific stuff is moved to their own modules
        public static void ResetUFDictionaries()
        {
            UpdateFieldDictionaries.Clear();
        }

        public static bool LoadUFDictionaries(Assembly asm, ClientVersionBuild build)
        {
            return LoadUFDictionariesInto(UpdateFieldDictionaries, asm, build);
        }

        private static readonly Dictionary<Type, BiDictionary<string, int>> UpdateFieldDictionaries = LoadDefaultUFDictionaries();

        private static bool LoadUFDictionariesInto(Dictionary<Type, BiDictionary<string, int>> dicts, Assembly asm, ClientVersionBuild build)
        {
            Type[] enumTypes =
            {
                typeof(ObjectField), typeof(ItemField), typeof(ContainerField), typeof(UnitField),
                typeof(PlayerField), typeof(GameObjectField), typeof(DynamicObjectField),
                typeof(CorpseField), typeof(AreaTriggerField), typeof(SceneObjectField), typeof(ConversationField),
                typeof(ObjectDynamicField), typeof(ItemDynamicField), typeof(ContainerDynamicField), typeof(UnitDynamicField),
                typeof(PlayerDynamicField), typeof(GameObjectDynamicField), typeof(DynamicObjectDynamicField),
                typeof(CorpseDynamicField), typeof(AreaTriggerDynamicField), typeof(SceneObjectDynamicField), typeof(ConversationDynamicField)
            };

            var loaded = false;
            foreach (var enumType in enumTypes)
            {
                var vTypeString = string.Format("WowPacketParserModule.{0}.Enums.{1}", GetUpdateFieldDictionaryBuildName(build), enumType.Name);
                var vEnumType = asm.GetType(vTypeString);
                if (vEnumType == null)
                {
                    vTypeString = string.Format("WowPacketParser.Enums.Version.{0}.{1}", GetUpdateFieldDictionaryBuildName(build), enumType.Name);
                    vEnumType = Assembly.GetExecutingAssembly().GetType(vTypeString);
                    if (vEnumType == null)
                        continue;   // versions prior to 4.3.0 do not have AreaTriggerField
                }

                var vValues = Enum.GetValues(vEnumType);
                var vNames = Enum.GetNames(vEnumType);

                var result = new BiDictionary<string, int>();

                for (var i = 0; i < vValues.Length; ++i)
                    result.Add(vNames[i], (int)vValues.GetValue(i));

                dicts.Add(enumType, result);
                loaded = true;
            }

            return loaded;
        }

        public static int GetUpdateField<T>(T field)
        {
            if (UpdateFieldDictionaries.ContainsKey(typeof(T)))
                if (UpdateFieldDictionaries[typeof(T)].ContainsKey(field.ToString()))
                    return UpdateFieldDictionaries[typeof(T)][field.ToString()];

            return Convert.ToInt32(field);
        }

        public static int GetUpdateField<T>(int field)
        {
            if (UpdateFieldDictionaries.ContainsKey(typeof(T)))
                if (UpdateFieldDictionaries[typeof(T)].ContainsKey(field.ToString(CultureInfo.InvariantCulture)))
                    return UpdateFieldDictionaries[typeof(T)][field.ToString(CultureInfo.InvariantCulture)];

            return field;
        }

        public static string GetUpdateFieldName<T>(int field)
        {
            if (UpdateFieldDictionaries.ContainsKey(typeof(T)))
            {
                int diff = 0;
                do
                {
                    if (UpdateFieldDictionaries[typeof(T)].ContainsValue(field - diff))
                        return UpdateFieldDictionaries[typeof(T)][field - diff] + (diff > 0 ? " + " + diff.ToString() : "");
                    diff++;
                }
                while (diff < field);
            }

            return field.ToString(CultureInfo.InvariantCulture);
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
                case ClientVersionBuild.V4_3_0a_15050:
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
                case ClientVersionBuild.V5_0_4_16016:
                {
                    return "V5_0_4_16016";
                }
                case ClientVersionBuild.V5_0_5_16048:
                case ClientVersionBuild.V5_0_5a_16057:
                case ClientVersionBuild.V5_0_5b_16135:
                {
                    return "V5_0_5_16048";
                }
                case ClientVersionBuild.V5_1_0_16309:
                case ClientVersionBuild.V5_1_0a_16357:
                {
                    return "V5_1_0_16309";
                }
                case ClientVersionBuild.V5_2_0_16650:
                case ClientVersionBuild.V5_2_0_16669:
                case ClientVersionBuild.V5_2_0_16683:
                case ClientVersionBuild.V5_2_0_16685:
                case ClientVersionBuild.V5_2_0_16701:
                case ClientVersionBuild.V5_2_0_16709:
                case ClientVersionBuild.V5_2_0_16716:
                case ClientVersionBuild.V5_2_0_16733:
                case ClientVersionBuild.V5_2_0_16769:
                case ClientVersionBuild.V5_2_0_16826:
                {
                    return "V5_2_0_16650";
                }
                case ClientVersionBuild.V5_3_0_16981:
                case ClientVersionBuild.V5_3_0_16983:
                case ClientVersionBuild.V5_3_0_16992:
                case ClientVersionBuild.V5_3_0_17055:
                case ClientVersionBuild.V5_3_0_17116:
                case ClientVersionBuild.V5_3_0_17128:
                {
                    return "V5_3_0_16981";
                }
                case ClientVersionBuild.V5_4_0_17359:
                case ClientVersionBuild.V5_4_0_17371:
                case ClientVersionBuild.V5_4_0_17399:
                {
                    return "V5_4_0_17359";
                }
                case ClientVersionBuild.V5_4_1_17538:
                {
                    return "V5_4_1_17538";
                }
                case ClientVersionBuild.V5_4_2_17658:
                case ClientVersionBuild.V5_4_2_17688:
                {
                    return "V5_4_2_17688";
                }
                case ClientVersionBuild.V5_4_7_17898:
                case ClientVersionBuild.V5_4_7_17930:
                case ClientVersionBuild.V5_4_7_17956:
                case ClientVersionBuild.V5_4_7_18019:
                {
                    return "V5_4_7_17898";
                }
                case ClientVersionBuild.V5_4_8_18291:
                case ClientVersionBuild.V5_4_8_18414:
                {
                    return "V5_4_8_18291";
                }
                case ClientVersionBuild.V6_0_2_19033:
                case ClientVersionBuild.V6_0_2_19034:
                {
                    return "V6_0_2_19033";
                }
                case ClientVersionBuild.V6_0_3_19103:
                case ClientVersionBuild.V6_0_3_19116:
                case ClientVersionBuild.V6_0_3_19243:
                case ClientVersionBuild.V6_0_3_19342:
                {
                    return "V6_0_3_19103";
                }
                case ClientVersionBuild.V6_1_0_19678:
                case ClientVersionBuild.V6_1_0_19702:
                case ClientVersionBuild.V6_1_2_19802:
                case ClientVersionBuild.V6_1_2_19831:
                case ClientVersionBuild.V6_1_2_19865:
                {
                    return "V6_1_2_19802";
                }
                case ClientVersionBuild.V6_2_0_20173:
                case ClientVersionBuild.V6_2_0_20182:
                case ClientVersionBuild.V6_2_0_20201:
                case ClientVersionBuild.V6_2_0_20216:
                case ClientVersionBuild.V6_2_0_20253:
                case ClientVersionBuild.V6_2_0_20338:
                {
                    return "V6_2_0_20173";
                }
                default:
                {
                    return "V3_3_5a_12340";
                }
            }
        }
    }
}
