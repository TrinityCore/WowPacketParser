using System;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.Location)]
    public class LocationEntry
    {
        public uint ID { get; set; }
        public Single LocX { get; set; }
        public Single LocY { get; set; }
        public Single LocZ { get; set; }
        [HotfixArray(3)]
        public Single[] Rotation { get; set; }
    }
}