using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGmNamedPoints
    {
        public List<ClientGMNamedPointsData> NamedPoints;
    }
}
