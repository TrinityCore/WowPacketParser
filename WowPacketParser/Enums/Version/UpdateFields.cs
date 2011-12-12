using System;
using System.Collections.Generic;
using System.Reflection;
using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version
{
    public static partial class UpdateFields
    {
        private static readonly Assembly _assembly = Assembly.GetExecutingAssembly();
        private static readonly string _defaultVersion = "WowPacketParser.Enums.Version.V3_3_5a_12340";

        public static string GetUpdateFieldName(int updateField, string fieldType)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.{1}", ClientVersion.GetVersionString(), fieldType);

            var enumName = Enum.GetName(_assembly.GetType(typeString), updateField);

            if (!String.IsNullOrEmpty(enumName))
                return enumName;

            return updateField.ToString();
        }

        // I wonder if the next methods could be merged into one....

        public static int GetUpdateField(ObjectField objectField)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.ObjectField", ClientVersion.GetVersionString());

            var newEnumType = _assembly.GetType(typeString);

            if (newEnumType == null)
                newEnumType = _assembly.GetType(_defaultVersion + ".ObjectField");

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == objectField.ToString())
                    return val;

            return (int)objectField;
        }

        public static int GetUpdateField(ItemField itemField)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.ItemField", ClientVersion.GetVersionString());

            var newEnumType = _assembly.GetType(typeString);

            if (newEnumType == null)
                newEnumType = _assembly.GetType(_defaultVersion + ".ItemField");

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == itemField.ToString())
                    return val;

            return (int)itemField;
        }

        public static int GetUpdateField(ContainerField containerField)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.ContainerField", ClientVersion.GetVersionString());

            var newEnumType = _assembly.GetType(typeString);

            if (newEnumType == null)
                newEnumType = _assembly.GetType(_defaultVersion + ".ContainerField");

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == containerField.ToString())
                    return val;

            return (int)containerField;
        }

        public static int GetUpdateField(UnitField unitField)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.UnitField", ClientVersion.GetVersionString());

            var newEnumType = _assembly.GetType(typeString);

            if (newEnumType == null)
                newEnumType = _assembly.GetType(_defaultVersion + ".UnitField");

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == unitField.ToString())
                    return val;

            return (int)unitField;
        }

        public static int GetUpdateField(GameObjectField gameObjectField)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.GameObjectField", ClientVersion.GetVersionString());

            var newEnumType = _assembly.GetType(typeString);

            if (newEnumType == null)
                newEnumType = _assembly.GetType(_defaultVersion + ".GameObjectField");

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == gameObjectField.ToString())
                    return val;

            return (int)gameObjectField;
        }

        public static int GetUpdateField(DynamicObjectField dynamicObjectField)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.DynamicObjectField", ClientVersion.GetVersionString());

            var newEnumType = _assembly.GetType(typeString);

            if (newEnumType == null)
                newEnumType = _assembly.GetType(_defaultVersion + ".DynamicObjectField");

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == dynamicObjectField.ToString())
                    return val;

            return (int)dynamicObjectField;
        }

        public static int GetUpdateField(CorpseField corpseField)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.CorpseField", ClientVersion.GetVersionString());

            var newEnumType = _assembly.GetType(typeString);

            if (newEnumType == null)
                newEnumType = _assembly.GetType(_defaultVersion + ".CorpseField");

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == corpseField.ToString())
                    return val;

            return (int)corpseField;
        }
    }
}
