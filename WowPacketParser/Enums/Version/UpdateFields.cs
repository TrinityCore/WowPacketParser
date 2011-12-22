using System;
using System.Collections.Generic;
using System.Reflection;
using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version
{
    public static partial class UpdateFields
    {
        private static readonly Assembly _assembly = Assembly.GetExecutingAssembly();

        private static string GetUpdateFieldDictionary(int build)
        {
            switch ((ClientVersionBuild)build)
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
            }

            return "V3_3_5_opcodes";
        }

        public static string GetUpdateFieldName(int updateField, string fieldType)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.{1}", GetUpdateFieldDictionary(ClientVersion.GetBuildInt()), fieldType);

            var newEnumType = _assembly.GetType(typeString);

            var enumName = Enum.GetName(newEnumType, updateField);

            if (!String.IsNullOrEmpty(enumName))
                return enumName;

            return updateField.ToString();
        }

        // I wonder if the next methods could be merged into one....

        public static int GetUpdateField(ObjectField objectField)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.ObjectField", GetUpdateFieldDictionary(ClientVersion.GetBuildInt()));

            var newEnumType = _assembly.GetType(typeString);

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == objectField.ToString())
                    return val;

            return (int)objectField;
        }

        public static int GetUpdateField(ItemField itemField)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.ItemField", GetUpdateFieldDictionary(ClientVersion.GetBuildInt()));

            var newEnumType = _assembly.GetType(typeString);

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == itemField.ToString())
                    return val;

            return (int)itemField;
        }

        public static int GetUpdateField(ContainerField containerField)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.ContainerField", GetUpdateFieldDictionary(ClientVersion.GetBuildInt()));

            var newEnumType = _assembly.GetType(typeString);

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == containerField.ToString())
                    return val;

            return (int)containerField;
        }

        public static int GetUpdateField(UnitField unitField)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.UnitField", GetUpdateFieldDictionary(ClientVersion.GetBuildInt()));

            var newEnumType = _assembly.GetType(typeString);

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == unitField.ToString())
                    return val;

            return (int)unitField;
        }

        public static int GetUpdateField(GameObjectField gameObjectField)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.GameObjectField", GetUpdateFieldDictionary(ClientVersion.GetBuildInt()));

            var newEnumType = _assembly.GetType(typeString);

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == gameObjectField.ToString())
                    return val;

            return (int)gameObjectField;
        }

        public static int GetUpdateField(DynamicObjectField dynamicObjectField)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.DynamicObjectField", GetUpdateFieldDictionary(ClientVersion.GetBuildInt()));

            var newEnumType = _assembly.GetType(typeString);

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == dynamicObjectField.ToString())
                    return val;

            return (int)dynamicObjectField;
        }

        public static int GetUpdateField(CorpseField corpseField)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.CorpseField", GetUpdateFieldDictionary(ClientVersion.GetBuildInt()));

            var newEnumType = _assembly.GetType(typeString);

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == corpseField.ToString())
                    return val;

            return (int)corpseField;
        }
    }
}
