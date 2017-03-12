using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientGmCharacterSave
    {
        public string CharacterName;
        public uint Flags;
    }
}
