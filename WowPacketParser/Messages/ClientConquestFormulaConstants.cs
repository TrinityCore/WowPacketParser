using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientConquestFormulaConstants
    {
        public float PvpCPExpCoefficient;
        public uint PvpMaxCPPerWeek;
        public uint PvpMinCPPerWeek;
        public float PvpCPBaseCoefficient;
        public float PvpCPNumerator;
    }
}
