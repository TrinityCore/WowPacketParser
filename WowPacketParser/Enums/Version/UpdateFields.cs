using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version
{
    public static class UpdateFields
    {
        private static readonly Dictionary<Type, BiDictionary<string, int>> UpdateFieldDictionaries = LoadUFDictionaries();

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

            return dicts;
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
                if (UpdateFieldDictionaries[typeof(T)].ContainsValue(field))
                    return UpdateFieldDictionaries[typeof (T)][field];

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
                default:
                {
                    return "V3_3_5a_12340";
                }
            }
        }
    }
}
