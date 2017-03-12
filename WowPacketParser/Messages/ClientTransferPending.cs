using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientTransferPending
    {
        public int MapID;
        public ShipTransferPending? Ship; // Optional
        public int? TransferSpellID; // Optional
    }
}
