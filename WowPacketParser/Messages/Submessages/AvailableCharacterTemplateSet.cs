using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct AvailableCharacterTemplateSet
    {
        public uint TemplateSetID;
        public string Name;
        public string Description;
        public List<AvailableCharacterTemplateClass> Classes;
    }
}
