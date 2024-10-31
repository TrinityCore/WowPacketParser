using System;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.AreaPoi)]
    public class AreaPOIEntry
    {
        public uint ID { get; set; }
        public uint Flags { get; set; }
        public uint Importance { get; set; }
        public uint FactionID { get; set; }
        public uint MapID { get; set; }
        public uint AreaID { get; set; }
        public uint Icon { get; set; }
        public Single PositionX { get; set; }
        public Single PositionY { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public uint WorldStateID { get; set; }
        public uint PlayerConditionID { get; set; }
        public uint WorldMapLink { get; set; }
        public uint PortLocID { get; set; }
    }
}