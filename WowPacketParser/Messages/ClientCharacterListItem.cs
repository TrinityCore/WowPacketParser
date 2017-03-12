using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCharacterListItem
    {
        public uint DisplayID;
        public uint DisplayEnchantID;
        public byte InvType;
    }
}
