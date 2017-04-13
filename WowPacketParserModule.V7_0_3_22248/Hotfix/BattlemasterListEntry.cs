using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.BattlemasterList, HasIndexInData = false)]
    public class BattlemasterListEntry
    {
        public string Name { get; set; }
        public uint IconFileDataID { get; set; }
        public string GameType { get; set; }
        [HotfixVersion(ClientVersionBuild.V7_2_0_23826, false)]
        public string ShortDescription { get; set; }
        [HotfixVersion(ClientVersionBuild.V7_2_0_23826, false)]
        public string LongDescription { get; set; }
        [HotfixArray(16)]
        public short[] MapID { get; set; }
        public ushort HolidayWorldState { get; set; }
        public ushort PlayerConditionID { get; set; }
        public byte InstanceType { get; set; }
        public byte GroupsAllowed { get; set; }
        public byte MaxGroupSize { get; set; }
        public byte MinLevel { get; set; }
        public byte MaxLevel { get; set; }
        public byte RatedPlayers { get; set; }
        public byte MinPlayers { get; set; }
        public byte MaxPlayers { get; set; }
        public byte Flags { get; set; }
    }
}