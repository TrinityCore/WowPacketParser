using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGarrisonRequestBlueprintAndSpecializationDataResult
    {
        public List<int> SpecializationsKnown;
        public List<int> BlueprintsKnown;
    }
}
