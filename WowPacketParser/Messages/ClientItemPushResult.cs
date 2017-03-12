using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientItemPushResult
    {
        public uint BattlePetBreedQuality;
        public ulong ItemGUID;
        public int SlotInBag;
        public int QuantityInInventory;
        public int Quantity;
        public bool Pushed;
        public bool DisplayText;
        public int BattlePetLevel;
        public byte Slot;
        public bool Created;
        public int BattlePetBreedID;
        public bool IsBonusRoll;
        public ulong PlayerGUID;
        public ItemInstance Item;
        public int BattlePetSpeciesID;
    }
}
