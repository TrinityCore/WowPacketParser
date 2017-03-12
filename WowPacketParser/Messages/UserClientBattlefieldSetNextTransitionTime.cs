using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientBattlefieldSetNextTransitionTime
    {
        public int SecondsUntilTransition;
        public int QueueID;
    }
}
