using System;
using System.Collections.Generic;
using System.Reflection;
using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version
{
    public static partial class UpdateFields
    {
        private static readonly Assembly _assembly = Assembly.GetExecutingAssembly();

        public static string GetUpdateFieldName(int updateField, string fieldType)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.{1}", ClientVersion.Build, fieldType);

            var enumName = _assembly.GetType(typeString).GetEnumName(updateField);

            if (!String.IsNullOrEmpty(enumName))
                return enumName;

            return updateField.ToString();
        }

        // I wonder if the next methods could be merged into one....

        public static int GetUpdateField(ObjectField objectField)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.ObjectField", ClientVersion.Build);

            var newEnumType = _assembly.GetType(typeString);

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == objectField.ToString())
                    return val;

            return (int)objectField;
        }

        public static int GetUpdateField(ItemField itemField)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.ItemField", ClientVersion.Build);

            var newEnumType = _assembly.GetType(typeString);

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == itemField.ToString())
                    return val;

            return (int)itemField;
        }

        public static int GetUpdateField(ContainerField containerField)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.ContainerField", ClientVersion.Build);

            var newEnumType = _assembly.GetType(typeString);

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == containerField.ToString())
                    return val;

            return (int)containerField;
        }

        public static int GetUpdateField(UnitField unitField)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.UnitField", ClientVersion.Build);

            var newEnumType = _assembly.GetType(typeString);

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == unitField.ToString())
                    return val;

            return (int)unitField;
        }

        public static int GetUpdateField(GameObjectField gameObjectField)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.GameObjectField", ClientVersion.Build);

            var newEnumType = _assembly.GetType(typeString);

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == gameObjectField.ToString())
                    return val;

            return (int)gameObjectField;
        }

        public static int GetUpdateField(DynamicObjectField dynamicObjectField)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.DynamicObjectField", ClientVersion.Build);

            var newEnumType = _assembly.GetType(typeString);

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == dynamicObjectField.ToString())
                    return val;

            return (int)dynamicObjectField;
        }

        public static int GetUpdateField(CorpseField corpseField)
        {
            var typeString = string.Format("WowPacketParser.Enums.Version.{0}.CorpseField", ClientVersion.Build);

            var newEnumType = _assembly.GetType(typeString);

            foreach (int val in Enum.GetValues(newEnumType))
                if (Enum.GetName(newEnumType, val) == corpseField.ToString())
                    return val;

            return (int)corpseField;
        }
    }
}
