using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliCreateSceneObjectCheat
    {
        public int EntryID;
        public CliSceneLocalScriptData ScriptData;
    }
}
