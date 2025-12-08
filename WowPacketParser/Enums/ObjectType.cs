namespace WowPacketParser.Enums
{
    public enum ObjectType
    {
        Object                  = 0,
        Item                    = 1,
        Container               = 2,
        AzeriteEmpoweredItem    = 3,
        AzeriteItem             = 4,
        Unit                    = 5,
        Player                  = 6,
        ActivePlayer            = 7,
        GameObject              = 8,
        DynamicObject           = 9,
        Corpse                  = 10,
        AreaTrigger             = 11,
        SceneObject             = 12,
        Conversation            = 13,
        Map                     = 14,
        MeshObject              = 15,
        AiGroup                 = 16,
        Scenario                = 17,
        LootObject              = 18
    }

    public enum ObjectTypeLegacy
    {
        Object        = 0,
        Item          = 1,
        Container     = 2,
        Unit          = 3,
        Player        = 4,
        GameObject    = 5,
        DynamicObject = 6,
        Corpse        = 7,
        AreaTrigger   = 8,
        SceneObject   = 9,
        Conversation  = 10
    }

    public enum ObjectType801
    {
        Object                  = 0,
        Item                    = 1,
        Container               = 2,
        AzeriteEmpoweredItem    = 3,
        AzeriteItem             = 4,
        Unit                    = 5,
        Player                  = 6,
        ActivePlayer            = 7,
        GameObject              = 8,
        DynamicObject           = 9,
        Corpse                  = 10,
        AreaTrigger             = 11,
        SceneObject             = 12,
        Conversation            = 13,
        MeshObject              = 14,
        AiGroup                 = 15,
        Scenario                = 16,
        LootObject              = 17,

        Max
    }

    public enum ObjectTypeBCC
    {
        Object = 0,
        Item = 1,
        Container = 2,
        Unit = 3,
        Player = 4,
        ActivePlayer = 5,
        GameObject = 6,
        DynamicObject = 7,
        Corpse = 8,
        AreaTrigger = 9,
        SceneObject = 10,
        Conversation = 11,
    }
}
