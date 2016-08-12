using System;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.Vignette)]
    public class VignetteEntry
    {
        public uint ID { get; set; }
        public string Name { get; set; }
        public uint Icon { get; set; }
        public uint Flag { get; set; } // not 100% sure (8 & 32 as values only) - todo verify with more data
        public Single UnkFloat1 { get; set; }
        public Single UnkFloat2 { get; set; }
    }
}