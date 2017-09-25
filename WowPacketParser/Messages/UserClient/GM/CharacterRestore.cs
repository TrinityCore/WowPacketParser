using System.Collections.Generic;

namespace WowPacketParser.Messages.UserClient.GM
{
    public unsafe struct CharacterRestore
    {
        public List<byte> Data;
        public uint Locale;
        public uint ClientToken;
        public uint ToAccount;
        public bool Compressed;
    }
}
