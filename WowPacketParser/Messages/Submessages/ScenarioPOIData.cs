using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct ScenarioPOIData
    {
        public int CriteriaTreeID;
        public List<ScenarioBlobData> BlobData;
    }
}
