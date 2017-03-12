using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientGmCharacterRestore
    {
        public List<byte> Data;
        public uint Locale;
        public uint ClientToken;
        public uint ToAccount;
        public bool Compressed;
    }
}
