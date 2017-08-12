using System;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.TaxiNodes)]
    public class TaxiNodesEntry
    {
        public uint ID { get; set; }
        public uint MapID { get; set; }
        public Single PosX { get; set; }
        public Single PosY { get; set; }
        public Single PosZ { get; set; }
        public string Name { get; set; }
        [HotfixArray(2)]
        public uint[] MountCreatureID { get; set; }
        public uint ConditionID { get; set; }
        public uint LearnableIndex { get; set; }
        public uint Flags { get; set; }
        public Single MapOffsetX { get; set; }
        public Single MapOffsetY { get; set; }
    }
}