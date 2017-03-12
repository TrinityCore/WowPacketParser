using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientBattlePetSetFlags
    {
        public ulong BattlePetGUID;
        public uint Flags;
        public clibattlepetsetflagcontroltype ControlType;
    }
}
