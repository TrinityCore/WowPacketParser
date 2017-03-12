using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliSendSceneObjectEventCheat
    {
        public ulong SceneObjGUID;
        public CliSceneObjectEvent SceneEvent;
    }
}
