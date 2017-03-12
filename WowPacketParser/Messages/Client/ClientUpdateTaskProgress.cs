using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientUpdateTaskProgress
    {
        public List<ClientTaskProgress> Progress;
    }
}
