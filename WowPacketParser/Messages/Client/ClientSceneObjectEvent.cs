using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSceneObjectEvent
    {
        public CliSceneObjectEvent SceneEvent;
        public ulong SceneObjectGUID;
    }
}
