using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGuildBankSwapItems
    {
        public byte BankSlot;
        public int StackCount;
        public ulong Banker;
        public bool AutoStore;
        public byte ContainerItemSlot;
        public uint ItemID;
        public byte ToSlot;
        public byte BankTab1;
        public byte BankTab;
        public int BankItemCount;
        public byte ContainerSlot;
        public uint ItemID1;
        public bool BankOnly;
        public byte BankSlot1;
    }
}
