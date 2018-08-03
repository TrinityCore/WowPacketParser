namespace WowPacketParser.Enums.Version.V3_0_2_9056
{
    // ReSharper disable InconsistentNaming
    // 3.0.2
    public enum ObjectField
    {
        OBJECT_FIELD_GUID = 0x0000, // Size: 2, Type: LONG, Flags: PUBLIC
        OBJECT_FIELD_TYPE = 0x0002, // Size: 1, Type: INT, Flags: PUBLIC
        OBJECT_FIELD_ENTRY = 0x0003, // Size: 1, Type: INT, Flags: PUBLIC
        OBJECT_FIELD_SCALE_X = 0x0004, // Size: 1, Type: FLOAT, Flags: PUBLIC
        OBJECT_FIELD_PADDING = 0x0005, // Size: 1, Type: INT, Flags: NONE
        OBJECT_END = 0x0006
    }

    public enum GameObjectField
    {
        GAMEOBJECT_FIELD_CREATED_BY = ObjectField.OBJECT_END + 0x0000, // Size: 2, Type: LONG, Flags: PUBLIC
        GAMEOBJECT_DISPLAYID = ObjectField.OBJECT_END + 0x0002, // Size: 1, Type: INT, Flags: PUBLIC
        GAMEOBJECT_FLAGS = ObjectField.OBJECT_END + 0x0003, // Size: 1, Type: INT, TWO_SHORT: PUBLIC
        GAMEOBJECT_ROTATION = ObjectField.OBJECT_END + 0x0004, // Size: 2, Type: Packed Quaternion, Flags: PUBLIC
        GAMEOBJECT_PARENTROTATION = ObjectField.OBJECT_END + 0x0006, // Size: 4, Type: FLOAT, Flags: PUBLIC
        GAMEOBJECT_POS_X = ObjectField.OBJECT_END + 0x000A, // Size: 1, Type: FLOAT, Flags: PUBLIC
        GAMEOBJECT_POS_Y = ObjectField.OBJECT_END + 0x000B, // Size: 1, Type: FLOAT, Flags: PUBLIC
        GAMEOBJECT_POS_Z = ObjectField.OBJECT_END + 0x000C, // Size: 1, Type: FLOAT, Flags: PUBLIC
        GAMEOBJECT_FACING = ObjectField.OBJECT_END + 0x000D, // Size: 1, Type: FLOAT, Flags: PUBLIC
        GAMEOBJECT_DYNAMIC = ObjectField.OBJECT_END + 0x000E, // Size: 1, Type: TWO_SHORT, Flags: DYNAMIC
        GAMEOBJECT_FACTION = ObjectField.OBJECT_END + 0x000F, // Size: 1, Type: INT, Flags: PUBLIC
        GAMEOBJECT_LEVEL = ObjectField.OBJECT_END + 0x0010, // Size: 1, Type: INT, Flags: PUBLIC
        GAMEOBJECT_BYTES_1 = ObjectField.OBJECT_END + 0x0011, // Size: 1, Type: BYTES, Flags: PUBLIC
        GAMEOBJECT_END = ObjectField.OBJECT_END + 0x0013
    }
}
