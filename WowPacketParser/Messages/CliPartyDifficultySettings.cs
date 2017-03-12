using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliPartyDifficultySettings
    {
        public uint DungeonDifficultyID;
        public uint RaidDifficultyID;
    }
}
