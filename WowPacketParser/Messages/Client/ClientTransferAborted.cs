using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientTransferAborted
    {
        public TransferAbort TransfertAbort;
        public byte Arg;
        public uint MapID;
    }
}
