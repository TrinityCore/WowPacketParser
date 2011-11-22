namespace WowPacketParser.Enums
{
    public enum WardenServerOpcode
    {
        /*
         * 
            WARDEN_SMSG_MODULE_USE                      = 0,
            WARDEN_SMSG_MODULE_CACHE                    = 1,
            WARDEN_SMSG_CHEAT_CHECKS_REQUEST            = 2,
            WARDEN_SMSG_MODULE_INITIALIZE               = 3,
            WARDEN_SMSG_MEM_CHECKS_REQUEST              = 4,        // byte len; while(!EOF) { byte unk(1); byte index(++); string module(can be 0); int offset; byte len; byte[] bytes_to_compare[len]; }
            WARDEN_SMSG_HASH_REQUEST                    = 5
         * 
         */
        ModuleInfo = 0,
        ModuleChunk = 1,
        CheatChecks = 2,
        Data = 3,
        Seed = 5
    }

    public enum WardenClientOpcode
    {
        ModuleLoadFailed = 0,
        ModuleLoaded = 1,
        CheatCheckResults = 2,
        TransformedSeed = 4
    }
}
