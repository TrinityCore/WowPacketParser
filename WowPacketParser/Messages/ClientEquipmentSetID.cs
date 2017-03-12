using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientEquipmentSetID
    {
        public ulong GUID;
        public int SetID;
    }
}
