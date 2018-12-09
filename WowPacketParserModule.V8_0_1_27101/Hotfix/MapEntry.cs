using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Map, HasIndexInData = false)]
    public class MapEntry
    {
        public string Directory { get; set; }
        public string MapName { get; set; }
        public string MapDescription0 { get; set; }
        public string MapDescription1 { get; set; }
        public string PvpShortDescription { get; set; }
        public string PvpLongDescription { get; set; }
        [HotfixArray(2)]
        public float[] Corpse { get; set; }
        public byte MapType { get; set; }
        public sbyte InstanceType { get; set; }
        public byte ExpansionID { get; set; }
        public ushort AreaTableID { get; set; }
        public short LoadingScreenID { get; set; }
        public short TimeOfDayOverride { get; set; }
        public short ParentMapID { get; set; }
        public ushort CosmeticParentMapID { get; set; }
        public byte TimeOffset { get; set; }
        public float MinimapIconScale { get; set; }
        public short CorpseMapID { get; set; }
        public byte MaxPlayers { get; set; }
        public short WindSettingsID { get; set; }
        public int ZmpFileDataID { get; set; }
        [HotfixArray(2)]
        public uint[] Flags { get; set; }
    }
}
