using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPetDismissSound
    {
        public int ModelID;
        public Vector3 ModelPosition;
    }
}
