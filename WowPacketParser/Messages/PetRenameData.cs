using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PetRenameData
    {
        public int PetNumber;
        public string NewName;
        public bool HasDeclinedNames;
        public string[/*5*/] DeclinedNames;
    }
}
