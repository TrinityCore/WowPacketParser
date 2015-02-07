namespace WowPacketParser.Enums
{
    public enum HighGuidType
    {
        Null             = 0,
        Uniq             = 1,
        Player           = 2,
        Item             = 3,
        StaticDoor       = 4,
        Transport        = 5,
        Conversation     = 6,
        Creature         = 7,
        Vehicle          = 8,
        Pet              = 9,
        GameObject       = 10,
        DynamicObject    = 11,
        AreaTrigger      = 12,
        Corpse           = 13,
        LootObject       = 14,
        SceneObject      = 15,
        Scenario         = 16,
        AIGroup          = 17,
        DynamicDoor      = 18,
        ClientActor      = 19,
        Vignette         = 20,
        CallForHelp      = 21,
        AIResource       = 22,
        AILock           = 23,
        AILockTicket     = 24,
        ChatChannel      = 25,
        Party            = 26,
        Guild            = 27,
        WowAccount       = 28,
        BNetAccount      = 29,
        GMTask           = 30,
        MobileSession    = 31,
        RaidGroup        = 32,
        Spell            = 33,
        Mail             = 34,
        WebObj           = 35,
        LFGObject        = 36,
        LFGList          = 37,
        UserRouter       = 38,
        PVPQueueGroup    = 39,
        UserClient       = 40,
        PetBattle        = 41,
        UniqueUserClient = 42,
        BattlePet        = 43
    }

    public enum HighGuidTypeLegacy
    {
        None          = -1,
        Player        = 0x000, // Seen 0x280 for players too
        BattleGround1 = 0x101,
        InstanceSave  = 0x104,
        Group         = 0x105,
        BattleGround2 = 0x109,
        MOTransport   = 0x10C,
        Guild         = 0x10F,
        Item          = 0x400, // Container
        DynObject     = 0xF00, // Corpses
        GameObject    = 0xF01,
        Transport     = 0xF02,
        Unit          = 0xF03,
        Pet           = 0xF04,
        Vehicle       = 0xF05
    }
}
