namespace WowPacketParser.Enums.Version.V2_4_3_8606
{
    // ReSharper disable InconsistentNaming
    // 2.4.3
    public enum GameObjectField
    {
        GAMEOBJECT_FIELD_CREATED_BY = ObjectField.OBJECT_END + 0x0000, // Size: 2, Type: LONG, Flags: PUBLIC
        GAMEOBJECT_DISPLAYID = ObjectField.OBJECT_END + 0x0002, // Size: 1, Type: INT, Flags: PUBLIC
        GAMEOBJECT_FLAGS = ObjectField.OBJECT_END + 0x0003, // Size: 1, Type: INT, Flags: PUBLIC
        GAMEOBJECT_ROTATION = ObjectField.OBJECT_END + 0x0004, // Size: 4, Type: FLOAT, Flags: PUBLIC
        GAMEOBJECT_STATE = ObjectField.OBJECT_END + 0x0008, // Size: 1, Type: INT, Flags: PUBLIC
        GAMEOBJECT_POS_X = ObjectField.OBJECT_END + 0x0009, // Size: 1, Type: FLOAT, Flags: PUBLIC
        GAMEOBJECT_POS_Y = ObjectField.OBJECT_END + 0x000A, // Size: 1, Type: FLOAT, Flags: PUBLIC
        GAMEOBJECT_POS_Z = ObjectField.OBJECT_END + 0x000B, // Size: 1, Type: FLOAT, Flags: PUBLIC
        GAMEOBJECT_FACING = ObjectField.OBJECT_END + 0x000C, // Size: 1, Type: FLOAT, Flags: PUBLIC
        GAMEOBJECT_DYNAMIC = ObjectField.OBJECT_END + 0x000D, // Size: 1, Type: INT, Flags: DYNAMIC
        GAMEOBJECT_FACTION = ObjectField.OBJECT_END + 0x000E, // Size: 1, Type: INT, Flags: PUBLIC
        GAMEOBJECT_TYPE_ID = ObjectField.OBJECT_END + 0x000F, // Size: 1, Type: INT, Flags: PUBLIC
        GAMEOBJECT_LEVEL = ObjectField.OBJECT_END + 0x0010, // Size: 1, Type: INT, Flags: PUBLIC
        GAMEOBJECT_ARTKIT = ObjectField.OBJECT_END + 0x0011, // Size: 1, Type: INT, Flags: PUBLIC
        GAMEOBJECT_ANIMPROGRESS = ObjectField.OBJECT_END + 0x0012, // Size: 1, Type: INT, Flags: PUBLIC
        GAMEOBJECT_PADDING = ObjectField.OBJECT_END + 0x0013, // Size: 1, Type: INT, Flags: PUBLIC
        GAMEOBJECT_END = ObjectField.OBJECT_END + 0x0014
    }

    public enum DynamicObjectField
    {
        DYNAMICOBJECT_CASTER = ObjectField.OBJECT_END + 0x0000, // Size: 2, Type: LONG, Flags: PUBLIC
        DYNAMICOBJECT_BYTES = ObjectField.OBJECT_END + 0x0002, // Size: 1, Type: BYTES, Flags: PUBLIC
        DYNAMICOBJECT_SPELLID = ObjectField.OBJECT_END + 0x0003, // Size: 1, Type: INT, Flags: PUBLIC
        DYNAMICOBJECT_RADIUS = ObjectField.OBJECT_END + 0x0004, // Size: 1, Type: FLOAT, Flags: PUBLIC
        DYNAMICOBJECT_POS_X = ObjectField.OBJECT_END + 0x0005, // Size: 1, Type: FLOAT, Flags: PUBLIC
        DYNAMICOBJECT_POS_Y = ObjectField.OBJECT_END + 0x0006, // Size: 1, Type: FLOAT, Flags: PUBLIC
        DYNAMICOBJECT_POS_Z = ObjectField.OBJECT_END + 0x0007, // Size: 1, Type: FLOAT, Flags: PUBLIC
        DYNAMICOBJECT_FACING = ObjectField.OBJECT_END + 0x0008, // Size: 1, Type: FLOAT, Flags: PUBLIC
        DYNAMICOBJECT_CASTTIME = ObjectField.OBJECT_END + 0x000, // Size: 1, Type: INT, Flags: PUBLIC
        DYNAMICOBJECT_END = ObjectField.OBJECT_END + 0x0006
    }
}
