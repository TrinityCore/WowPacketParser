using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGmNamedPoints
    {
        public List<ClientGMNamedPointsData> NamedPoints;
    }
}
