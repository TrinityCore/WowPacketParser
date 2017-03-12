using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct AddonInfo
    {
        public byte Status;
        public bool InfoProvided;
        public bool KeyProvided;
        public bool UrlProvided;
        public byte KeyVersion;
        public uint Revision;
        public string Url;
        public fixed sbyte KeyData[256];
    }
}
once 