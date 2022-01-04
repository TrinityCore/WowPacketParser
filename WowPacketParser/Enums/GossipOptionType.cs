namespace WowPacketParser.Enums
{
    public enum GossipOptionType : int
    {
        None                    = 0,    //UNIT_NPC_FLAG_NONE                (0)
        Gossip                  = 1,    //UNIT_NPC_FLAG_GOSSIP              (1)
        Questgiver              = 2,    //UNIT_NPC_FLAG_QUESTGIVER          (2)
        Vendor                  = 3,    //UNIT_NPC_FLAG_VENDOR              (128)
        Taxivendor              = 4,    //UNIT_NPC_FLAG_TAXIVENDOR          (8192)
        Trainer                 = 5,    //UNIT_NPC_FLAG_TRAINER             (16)
        Spirithealer            = 6,    //UNIT_NPC_FLAG_SPIRITHEALER        (16384)
        Spiritguide             = 7,    //UNIT_NPC_FLAG_SPIRITGUIDE         (32768)
        Innkeeper               = 8,    //UNIT_NPC_FLAG_INNKEEPER           (65536)
        Banker                  = 9,    //UNIT_NPC_FLAG_BANKER              (131072)
        Petitioner              = 10,   //UNIT_NPC_FLAG_PETITIONER          (262144)
        TabardDesigner          = 11,   //UNIT_NPC_FLAG_TABARDDESIGNER      (524288)
        Battlefield             = 12,   //UNIT_NPC_FLAG_BATTLEFIELDPERSON   (1048576)
        Auctioneer              = 13,   //UNIT_NPC_FLAG_AUCTIONEER          (2097152)
        StablePet               = 14,   //UNIT_NPC_FLAG_STABLE              (4194304)
        Armorer                 = 15,   //UNIT_NPC_FLAG_ARMORER             (4096)
        UnlearnTalents          = 16,   //UNIT_NPC_FLAG_TRAINER             (16) (bonus option for GOSSIP_OPTION_TRAINER)
        UnlearnPetTalentsOld    = 17,   // deprecated
        LearnDualSpec           = 18,   //UNIT_NPC_FLAG_TRAINER             (16) (bonus option for GOSSIP_OPTION_TRAINER)
        OutdoorPvP              = 19,   //added by code (option for outdoor pvp creatures)
        Transmogrifier          = 20,   //UNIT_NPC_FLAG_TRANSMOGRIFIER
    }
}
