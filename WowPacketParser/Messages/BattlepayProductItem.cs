using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct BattlepayProductItem
    {
        public uint ID;
        public uint ItemID;
        public uint Quantity;
        public BattlepayDisplayInfo? DisplayInfo; // Optional
        public bool HasPet;
        public BATTLEPETRESULT? PetResult; // Optional
        public bool HasMount;
    }
}
