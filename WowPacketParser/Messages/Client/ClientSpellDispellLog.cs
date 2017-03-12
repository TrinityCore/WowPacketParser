using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
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
