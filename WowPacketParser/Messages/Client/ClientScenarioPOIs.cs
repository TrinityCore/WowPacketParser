using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientScenarioPOIs
    {
        public List<ScenarioPOIData> PoiInfo;
    }
}
