using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct AvailableCharacterTemplateSet
    {
        public uint TemplateSetID;
        public string Name;
        public string Description;
        public List<AvailableCharacterTemplateClass> Classes;
    }
}
