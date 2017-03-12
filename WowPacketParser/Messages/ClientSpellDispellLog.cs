using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellDispellLog
    {
        public List<ClientSpellDispellData> DispellData;
        public bool IsBreak;
        public ulong TargetGUID;
        public bool IsSteal;
        public int DispelledBySpellID;
        public ulong CasterGUID;
    }
}
