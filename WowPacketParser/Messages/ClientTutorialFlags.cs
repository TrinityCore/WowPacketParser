using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientTutorialFlags
    {
        public fixed byte TutorialData[32];
    }
}
