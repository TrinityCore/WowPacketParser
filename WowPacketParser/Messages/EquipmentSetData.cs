using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct EquipmentSetData
    {
        public ulong Guid;
        public uint SetID;
        public string SetName;
        public string SetIcon;
        public fixed ulong Pieces[19];
    }
}
