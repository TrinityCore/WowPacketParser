using System.Collections.Generic;

namespace WowPacketParser.Messages.UserClient
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
