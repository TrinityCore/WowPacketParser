using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetEncounterProfiler
    {
        public bool Enable;
        public int EncounterID;
        public string MailTo;
    }
}
