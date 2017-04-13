using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.Map, HasIndexInData = false)]
    public class MapEntry
    {
        public string Directory { get; set; }
        [HotfixArray(2)]
        public uint[] Flags { get; set; }
        public float MinimapIconScale { get; set; }
        [HotfixArray(2)]
        public float[] CorpsePos { get; set; }
        public string MapName { get; set; }
        public string MapDescription0 { get; set; }
        public string MapDescription1 { get; set; }
        [HotfixVersion(ClientVersionBuild.V7_2_0_23826, false)]
        public string ShortDescription { get; set; }
        [HotfixVersion(ClientVersionBuild.V7_2_0_23826, false)]
        public string LongDescription { get; set; }
        public ushort AreaTableID { get; set; }
        public ushort LoadingScreenID { get; set; }
        public short CorpseMapID { get; set; }
        public ushort TimeOfDayOverride { get; set; }
        public short ParentMapID { get; set; }
        public short CosmeticParentMapID { get; set; }
        public ushort WindSettingsID { get; set; }
        public byte InstanceType { get; set; }
        public byte Unk5 { get; set; }
        public byte ExpansionID { get; set; }
        public byte MaxPlayers { get; set; }
        public byte TimeOffset { get; set; }
    }
}