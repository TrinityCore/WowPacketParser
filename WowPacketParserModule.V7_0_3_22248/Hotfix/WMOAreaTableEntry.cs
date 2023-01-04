using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.WmoAreaTable)]
    public class WMOAreaTableEntry
    {
        public int WMOGroupID { get; set; }
        public string AreaName { get; set; }
        public short WMOID { get; set; }
        public ushort AmbienceID { get; set; }
        public ushort ZoneMusic { get; set; }
        public ushort IntroSound { get; set; }
        public ushort AreaTableID { get; set; }
        public ushort UWIntroSound { get; set; }
        public ushort UWAmbience { get; set; }
        public sbyte NameSet { get; set; }
        public byte SoundProviderPref { get; set; }
        public byte SoundProviderPrefUnderwater { get; set; }
        public byte Flags { get; set; }
        public uint ID { get; set; }
        public uint UWZoneMusic { get; set; }
    }
}