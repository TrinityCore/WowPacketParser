using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template")]
    public class UnitTemplateNonWDB
    {
        [DBFieldName("gossip_menu_id")] public uint GossipMenuId;
        [DBFieldName("minlevel")] public int MinLevel;
        [DBFieldName("maxlevel")] public int MaxLevel;
        [DBFieldName("faction")] public uint Faction;
        [DBFieldName("npcflag")] public uint NpcFlag;
        [DBFieldName("speed_walk")] public float SpeedWalk;
        [DBFieldName("speed_run")] public float SpeedRun;
        [DBFieldName("BaseAttackTime")] public uint BaseAttackTime;
        [DBFieldName("RangeAttackTime")] public uint RangedAttackTime;
        [DBFieldName("unit_class")] public uint UnitClass;
        [DBFieldName("unit_flags")] public uint UnitFlag;
        [DBFieldName("unit_flags2")] public uint UnitFlag2;
        [DBFieldName("dynamicflags")] public uint DynamicFlag;
        [DBFieldName("VehicleId")] public uint VehicleId;
        [DBFieldName("HoverHeight")] public float HoverHeight;
    }

    [DBTableName("creature_difficulty_misc")]
    public class CreatureDifficultyMisc
    {
        [DBFieldName("CreatureId")]
        public uint CreatureId;

        [DBFieldName("GossipMenuId")]
        public uint GossipMenuId;

        [DBFieldName("NpcFlag")]
        public uint NpcFlag;

        [DBFieldName("SpeedWalk")]
        public float SpeedWalk;

        [DBFieldName("SpeedRun")]
        public float SpeedRun;

        [DBFieldName("BaseAttackTime")]
        public uint BaseAttackTime;

        [DBFieldName("RangeAttackTime")]
        public uint RangedAttackTime;

        [DBFieldName("UnitClass")]
        public uint UnitClass;

        [DBFieldName("UnitFlags")]
        public uint UnitFlag;

        [DBFieldName("UnitFlags2")]
        public uint UnitFlag2;

        [DBFieldName("DynamicFlags")]
        public uint DynamicFlag;

        [DBFieldName("VehicleId")]
        public uint VehicleId;

        [DBFieldName("HoverHeight")]
        public float HoverHeight;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
