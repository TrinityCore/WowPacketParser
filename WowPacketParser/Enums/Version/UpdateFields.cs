using System;
using System.Globalization;
using System.Reflection;
using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version
{
    public static class UpdateFields
    {
        private static readonly Assembly _assembly = Assembly.GetExecutingAssembly();

        private static string GetUpdateFieldDictionary(ClientVersionBuild build)
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
                case ClientVersionBuild.V4_2_0_14333:
                case ClientVersionBuild.V4_2_0a_14480:
                {
                    return "V4_0_6_13596";
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
                case ClientVersionBuild.V4_3_3_15354:
                {
                    return "V4_3_2_15211";
                }
            }

            return "V3_3_5_opcodes";
        }

        public static string GetUpdateFieldName<T>(int field)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.{1}", GetUpdateFieldDictionary(ClientVersion.GetBuild()), typeof(T).Name);

            var newEnumType = _assembly.GetType(typeString);

            var enumName = Enum.GetName(newEnumType, field);

            if (!String.IsNullOrEmpty(enumName))
                return enumName;

            return field.ToString(CultureInfo.InvariantCulture);
        }

        public static int GetUpdateField<T>(T field)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.{1}", GetUpdateFieldDictionary(ClientVersion.GetBuild()), typeof(T).Name);

            var newEnumType = _assembly.GetType(typeString);

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == field.ToString())
                    return val;

            return Convert.ToInt32(field);
        }
    }
}
