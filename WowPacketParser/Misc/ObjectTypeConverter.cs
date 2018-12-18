using System;
using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public static class ObjectTypeConverter
    {
        private static readonly Dictionary<ObjectTypeLegacy, ObjectType> ConvDictLegacy = new Dictionary<ObjectTypeLegacy, ObjectType>
        {
            { ObjectTypeLegacy.Object,                 ObjectType.Object },
            { ObjectTypeLegacy.Item,                   ObjectType.Item },
            { ObjectTypeLegacy.Container,              ObjectType.Container },
            { ObjectTypeLegacy.Unit,                   ObjectType.Unit },
            { ObjectTypeLegacy.Player,                 ObjectType.Player },
            { ObjectTypeLegacy.GameObject,             ObjectType.GameObject },
            { ObjectTypeLegacy.DynamicObject,          ObjectType.DynamicObject },
            { ObjectTypeLegacy.Corpse,                 ObjectType.Corpse },
            { ObjectTypeLegacy.AreaTrigger,            ObjectType.AreaTrigger },
            { ObjectTypeLegacy.SceneObject,            ObjectType.SceneObject },
            { ObjectTypeLegacy.Conversation,           ObjectType.Conversation }
        };

        public static ObjectType Convert(ObjectTypeLegacy type)
        {
            if (!ConvDictLegacy.ContainsKey(type))
                throw new ArgumentOutOfRangeException("0x" + type.ToString("X"));
            return ConvDictLegacy[type];
        }

        private static readonly Dictionary<ObjectType801, ObjectType> ConvDict801 = new Dictionary<ObjectType801, ObjectType>
        {
            { ObjectType801.Object,                 ObjectType.Object },
            { ObjectType801.Item,                   ObjectType.Item },
            { ObjectType801.Container,              ObjectType.Container },
            { ObjectType801.AzeriteEmpoweredItem,   ObjectType.AzeriteEmpoweredItem },
            { ObjectType801.AzeriteItem,            ObjectType.AzeriteItem },
            { ObjectType801.Unit,                   ObjectType.Unit },
            { ObjectType801.Player,                 ObjectType.Player },
            { ObjectType801.ActivePlayer,           ObjectType.ActivePlayer },
            { ObjectType801.GameObject,             ObjectType.GameObject },
            { ObjectType801.DynamicObject,          ObjectType.DynamicObject },
            { ObjectType801.Corpse,                 ObjectType.Corpse },
            { ObjectType801.AreaTrigger,            ObjectType.AreaTrigger },
            { ObjectType801.SceneObject,            ObjectType.SceneObject },
            { ObjectType801.Conversation,           ObjectType.Conversation }
        };

        public static ObjectType Convert(ObjectType801 type)
        {
            if (!ConvDict801.ContainsKey(type))
                throw new ArgumentOutOfRangeException("0x" + type.ToString("X"));
            return ConvDict801[type];
        }
    }
}
