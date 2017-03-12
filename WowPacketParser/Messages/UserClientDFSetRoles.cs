using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientDFSetRoles
    {
        public uint RolesDesired;
        public byte PartyIndex;
    }
}
