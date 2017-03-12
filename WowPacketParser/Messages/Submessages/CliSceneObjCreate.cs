using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliSceneObjCreate
    {
        public CliSceneLocalScriptData? LocalScriptData; // Optional
        public PetBattleFullUpdate? PetBattleFullUpdate; // Optional
    }
}
