using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMOTD
    {
        public List<ClientMOTDStruct> Text;
    }
}
