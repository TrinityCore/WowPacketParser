﻿using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template")]
    public class UnitTemplateNonWDB
    {
        [DBFieldName("gossip_menu_id")] public uint GossipMenuId;
        [DBFieldName("minlevel")] public uint MinLevel;
        [DBFieldName("maxlevel")] public uint MaxLevel;
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
}
