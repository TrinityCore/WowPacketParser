using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientTransferPending
    {
        public int MapID;
        public ShipTransferPending? Ship; // Optional
        public int? TransferSpellID; // Optional
    }
}
