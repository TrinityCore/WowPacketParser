using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("destructible_model_data")]
    public sealed record DestructibleModelDataHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("State0ImpactEffectDoodadSet")]
        public sbyte? State0ImpactEffectDoodadSet;

        [DBFieldName("State0AmbientDoodadSet")]
        public byte? State0AmbientDoodadSet;

        [DBFieldName("State1Wmo")]
        public int? State1Wmo;

        [DBFieldName("State1DestructionDoodadSet")]
        public sbyte? State1DestructionDoodadSet;

        [DBFieldName("State1ImpactEffectDoodadSet")]
        public sbyte? State1ImpactEffectDoodadSet;

        [DBFieldName("State1AmbientDoodadSet")]
        public byte? State1AmbientDoodadSet;

        [DBFieldName("State2Wmo")]
        public int? State2Wmo;

        [DBFieldName("State2DestructionDoodadSet")]
        public sbyte? State2DestructionDoodadSet;

        [DBFieldName("State2ImpactEffectDoodadSet")]
        public sbyte? State2ImpactEffectDoodadSet;

        [DBFieldName("State2AmbientDoodadSet")]
        public byte? State2AmbientDoodadSet;

        [DBFieldName("State3Wmo")]
        public int? State3Wmo;

        [DBFieldName("State3InitDoodadSet")]
        public byte? State3InitDoodadSet;

        [DBFieldName("State3AmbientDoodadSet")]
        public byte? State3AmbientDoodadSet;

        [DBFieldName("EjectDirection")]
        public byte? EjectDirection;

        [DBFieldName("DoNotHighlight")]
        public byte? DoNotHighlight;

        [DBFieldName("State0Wmo")]
        public int? State0Wmo;

        [DBFieldName("HealEffect")]
        public byte? HealEffect;

        [DBFieldName("HealEffectSpeed")]
        public ushort? HealEffectSpeed;

        [DBFieldName("State0NameSet")]
        public sbyte? State0NameSet;

        [DBFieldName("State1NameSet")]
        public sbyte? State1NameSet;

        [DBFieldName("State2NameSet")]
        public sbyte? State2NameSet;

        [DBFieldName("State3NameSet")]
        public sbyte? State3NameSet;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("destructible_model_data")]
    public sealed record DestructibleModelDataHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("State0ImpactEffectDoodadSet")]
        public sbyte? State0ImpactEffectDoodadSet;

        [DBFieldName("State0AmbientDoodadSet")]
        public byte? State0AmbientDoodadSet;

        [DBFieldName("State1Wmo")]
        public uint? State1Wmo;

        [DBFieldName("State1DestructionDoodadSet")]
        public sbyte? State1DestructionDoodadSet;

        [DBFieldName("State1ImpactEffectDoodadSet")]
        public sbyte? State1ImpactEffectDoodadSet;

        [DBFieldName("State1AmbientDoodadSet")]
        public byte? State1AmbientDoodadSet;

        [DBFieldName("State2Wmo")]
        public uint? State2Wmo;

        [DBFieldName("State2DestructionDoodadSet")]
        public sbyte? State2DestructionDoodadSet;

        [DBFieldName("State2ImpactEffectDoodadSet")]
        public sbyte? State2ImpactEffectDoodadSet;

        [DBFieldName("State2AmbientDoodadSet")]
        public byte? State2AmbientDoodadSet;

        [DBFieldName("State3Wmo")]
        public uint? State3Wmo;

        [DBFieldName("State3InitDoodadSet")]
        public byte? State3InitDoodadSet;

        [DBFieldName("State3AmbientDoodadSet")]
        public byte? State3AmbientDoodadSet;

        [DBFieldName("EjectDirection")]
        public byte? EjectDirection;

        [DBFieldName("DoNotHighlight")]
        public byte? DoNotHighlight;

        [DBFieldName("State0Wmo")]
        public uint? State0Wmo;

        [DBFieldName("HealEffect")]
        public byte? HealEffect;

        [DBFieldName("HealEffectSpeed")]
        public ushort? HealEffectSpeed;

        [DBFieldName("State0NameSet")]
        public sbyte? State0NameSet;

        [DBFieldName("State1NameSet")]
        public sbyte? State1NameSet;

        [DBFieldName("State2NameSet")]
        public sbyte? State2NameSet;

        [DBFieldName("State3NameSet")]
        public sbyte? State3NameSet;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
