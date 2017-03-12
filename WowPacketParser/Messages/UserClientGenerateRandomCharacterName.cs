using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientGenerateRandomCharacterName
    {
        public byte Sex;
        public byte Race;
    }
}
