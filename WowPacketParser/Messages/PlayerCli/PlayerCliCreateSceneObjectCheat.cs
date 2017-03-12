using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliCreateSceneObjectCheat
    {
        public int EntryID;
        public CliSceneLocalScriptData ScriptData;
    }
}
