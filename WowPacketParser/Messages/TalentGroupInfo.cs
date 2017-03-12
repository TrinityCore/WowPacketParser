using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct TalentGroupInfo
    {
        public int SpecID;
        public List<ushort> TalentIDs;
        public fixed ushort GlyphIDs[6];
    }
}
