using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliCDBLookup
    {
        public string SearchString;
        public bool ReturnLocalizedStrings;
        public int Locale;
        public bool OnlySearchLocalizedFields;
    }
}
