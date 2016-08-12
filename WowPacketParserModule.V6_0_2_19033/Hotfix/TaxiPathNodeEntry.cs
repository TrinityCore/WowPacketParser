using System;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.TaxiPathNode)]
    public class TaxiPathNodeEntry
    {
        public uint ID { get; set; }
        public uint PathID { get; set; }
        public uint NodeIndex { get; set; }
        public uint MapID { get; set; }
        public Single LocX { get; set; }
        public Single LocY { get; set; }
        public Single LocZ { get; set; }
        public uint Flags { get; set; }
        public uint Delay { get; set; }
        public uint ArrivalEventID { get; set; }
        public uint DepartureEventID { get; set; }
    }
}