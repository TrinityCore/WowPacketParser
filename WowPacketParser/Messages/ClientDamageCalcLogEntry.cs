using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDamageCalcLogEntry
    {
        public string Op;
        public uint Var;
        public float Value;
        public float Damage;
    }
}
