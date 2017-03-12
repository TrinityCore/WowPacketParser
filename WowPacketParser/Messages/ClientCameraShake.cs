using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCameraShake
    {
        public int SoundID;
        public int CameraShakeID;
    }
}
