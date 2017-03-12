using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct TalentGroupInfo
    {
        public int SpecID;
        public List<ushort> TalentIDs;
        public fixed ushort GlyphIDs[6];
    }
}
