using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliSendSceneObjectEventCheat
    {
        public ulong SceneObjGUID;
        public CliSceneObjectEvent SceneEvent;
    }
}
