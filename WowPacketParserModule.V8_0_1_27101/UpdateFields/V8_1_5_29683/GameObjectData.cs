using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_1_5_29683
{
    public class GameObjectData : IMutableGameObjectData
    {
        public int? DisplayID { get; set; }
        public System.Nullable<uint> SpellVisualID { get; set; }
        public System.Nullable<uint> StateSpellVisualID { get; set; }
        public System.Nullable<uint> SpawnTrackingStateAnimID { get; set; }
        public System.Nullable<uint> SpawnTrackingStateAnimKitID { get; set; }
        public System.Nullable<uint> StateWorldEffectsQuestObjectiveID { get; set; }
        public System.Nullable<uint>[] StateWorldEffectIDs { get; set; }
        public WowGuid CreatedBy { get; set; }
        public WowGuid GuildGUID { get; set; }
        public uint? Flags { get; set; }
        public Quaternion? ParentRotation { get; set; }
        public int? FactionTemplate { get; set; }
        public int? Level { get; set; }
        public sbyte? State { get; set; }
        public sbyte? TypeID { get; set; }
        public uint? ArtKit { get; set; }
        public byte? PercentHealth { get; set; }
        public uint CustomParam { get; set; }
        public DynamicUpdateField<int> EnableDoodadSets { get; } = new DynamicUpdateField<int>();
    }
}

