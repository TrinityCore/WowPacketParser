using System;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public static class ObjectTypeConverter
    {
        public static ObjectType Convert(ObjectTypeLegacy type)
        {
            return type switch
            {
                ObjectTypeLegacy.Object => ObjectType.Object,
                ObjectTypeLegacy.Item => ObjectType.Item,
                ObjectTypeLegacy.Container => ObjectType.Container,
                ObjectTypeLegacy.Unit => ObjectType.Unit,
                ObjectTypeLegacy.Player => ObjectType.Player,
                ObjectTypeLegacy.GameObject => ObjectType.GameObject,
                ObjectTypeLegacy.DynamicObject => ObjectType.DynamicObject,
                ObjectTypeLegacy.Corpse => ObjectType.Corpse,
                ObjectTypeLegacy.AreaTrigger => ObjectType.AreaTrigger,
                ObjectTypeLegacy.SceneObject => ObjectType.SceneObject,
                ObjectTypeLegacy.Conversation => ObjectType.Conversation,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, "0x" + type.ToString("X"))
            };
        }

        public static ObjectType Convert(ObjectType801 type)
        {
            return type switch
            {
                ObjectType801.Object => ObjectType.Object,
                ObjectType801.Item => ObjectType.Item,
                ObjectType801.Container => ObjectType.Container,
                ObjectType801.AzeriteEmpoweredItem => ObjectType.AzeriteEmpoweredItem,
                ObjectType801.AzeriteItem => ObjectType.AzeriteItem,
                ObjectType801.Unit => ObjectType.Unit,
                ObjectType801.Player => ObjectType.Player,
                ObjectType801.ActivePlayer => ObjectType.ActivePlayer,
                ObjectType801.GameObject => ObjectType.GameObject,
                ObjectType801.DynamicObject => ObjectType.DynamicObject,
                ObjectType801.Corpse => ObjectType.Corpse,
                ObjectType801.AreaTrigger => ObjectType.AreaTrigger,
                ObjectType801.SceneObject => ObjectType.SceneObject,
                ObjectType801.Conversation => ObjectType.Conversation,
                ObjectType801.MeshObject => ObjectType.MeshObject,
                ObjectType801.AiGroup => ObjectType.AiGroup,
                ObjectType801.Scenario => ObjectType.Scenario,
                ObjectType801.LootObject => ObjectType.LootObject,
                ObjectType801.Max => ObjectType.Object,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, "0x" + type.ToString("X"))
            };
        }

        public static ObjectType Convert(ObjectTypeBCC type)
        {
            return type switch
            {
                ObjectTypeBCC.Object => ObjectType.Object,
                ObjectTypeBCC.Item => ObjectType.Item,
                ObjectTypeBCC.Container => ObjectType.Container,
                ObjectTypeBCC.Unit => ObjectType.Unit,
                ObjectTypeBCC.Player => ObjectType.Player,
                ObjectTypeBCC.ActivePlayer => ObjectType.ActivePlayer,
                ObjectTypeBCC.GameObject => ObjectType.GameObject,
                ObjectTypeBCC.DynamicObject => ObjectType.DynamicObject,
                ObjectTypeBCC.Corpse => ObjectType.Corpse,
                ObjectTypeBCC.AreaTrigger => ObjectType.AreaTrigger,
                ObjectTypeBCC.SceneObject => ObjectType.SceneObject,
                ObjectTypeBCC.Conversation => ObjectType.Conversation,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, "0x" + type.ToString("X"))
            };
        }
    }
}
