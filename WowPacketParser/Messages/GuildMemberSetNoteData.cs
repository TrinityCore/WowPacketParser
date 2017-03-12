using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GuildMemberSetNoteData
    {
        public ulong NoteeGUID;
        public string Note;
        public bool IsPublic;
    }
}
