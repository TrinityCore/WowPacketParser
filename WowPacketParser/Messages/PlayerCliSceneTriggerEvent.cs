using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSceneTriggerEvent
    {
        public uint SceneInstanceID;
        public string Event;
    }
}
