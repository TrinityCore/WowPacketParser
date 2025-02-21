using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spawn_tracking_state")]
    public sealed record SpawnTrackingState : IDataModel
    {
        [DBFieldName("SpawnType", true)]
        public byte? SpawnType;

        [DBFieldName("SpawnId", true, true)]
        public string SpawnId;

        [DBFieldName("State", true)]
        public byte? State;

        [DBFieldName("Visible")]
        public bool Visible;

        [DBFieldName("StateSpellVisualId", false, false, true)]
        public uint? StateSpellVisualId;

        [DBFieldName("StateAnimId", false, false, true)]
        public uint? StateAnimId;

        [DBFieldName("StateAnimKitId", false, false, true)]
        public uint? StateAnimKitId;

        [DBFieldName("StateWorldEffects", false, false, true)]
        public string StateWorldEffects;
    }
}
