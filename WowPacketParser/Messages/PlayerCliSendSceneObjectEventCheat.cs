using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSendSceneObjectEventCheat
    {
        public ulong SceneObjGUID;
        public CliSceneObjectEvent SceneEvent;
    }
}
