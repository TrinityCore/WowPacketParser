using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.WMOAreaTable)]
    public class WMOAreaTableEntry
    {
        public string AreaName { get; set; }
        public uint ID { get; set; }
        public ushort WmoID { get; set; }
        public byte NameSetID { get; set; }
        public int WmoGroupID { get; set; }
        public byte SoundProviderPref { get; set; }
        public byte SoundProviderPrefUnderwater { get; set; }
        public ushort AmbienceID { get; set; }
        public ushort UwAmbience { get; set; }
        public ushort ZoneMusic { get; set; }
        public uint UwZoneMusic { get; set; }
        public ushort IntroSound { get; set; }
        public ushort UwIntroSound { get; set; }
        public ushort AreaTableID { get; set; }
        public byte Flags { get; set; }
    }
}
