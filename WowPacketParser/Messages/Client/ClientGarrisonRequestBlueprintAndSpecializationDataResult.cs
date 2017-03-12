using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGarrisonRequestBlueprintAndSpecializationDataResult
    {
        public List<int> SpecializationsKnown;
        public List<int> BlueprintsKnown;
    }
}
