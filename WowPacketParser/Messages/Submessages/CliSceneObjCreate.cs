namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliSceneObjCreate
    {
        public CliSceneLocalScriptData? LocalScriptData; // Optional
        public PetBattleFullUpdate? PetBattleFullUpdate; // Optional
    }
}
