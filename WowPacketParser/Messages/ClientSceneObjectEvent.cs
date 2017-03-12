using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSceneObjectEvent
    {
        public CliSceneObjectEvent SceneEvent;
        public ulong SceneObjectGUID;
    }
}
