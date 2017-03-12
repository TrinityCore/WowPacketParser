using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGetMirrorImageData
    {
        public ulong UnitGUID;
        public int DisplayID;
    }
}
