using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ScenarioPOIData
    {
        public int CriteriaTreeID;
        public List<ScenarioBlobData> BlobData;
    }
}
