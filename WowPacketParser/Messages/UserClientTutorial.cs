using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientTutorial
    {
        public TutorialAction Action;
        public uint TutorialBit;
    }
}
