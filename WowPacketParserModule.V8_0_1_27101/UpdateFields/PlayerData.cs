using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_1_5_29495
{
    public class PlayerData : IPlayerData
    {
        public WowGuid DuelArbiter { get; set; }
        public WowGuid WowAccount { get; set; }
        public WowGuid LootTargetGUID { get; set; }
        public uint PlayerFlags { get; set; }
        public uint PlayerFlagsEx { get; set; }
        public uint GuildRankID { get; set; }
        public uint GuildDeleteDate { get; set; }
        public int GuildLevel { get; set; }
        public byte SkinID { get; set; }
        public byte FaceID { get; set; }
        public byte HairStyleID { get; set; }
        public byte HairColorID { get; set; }
        public byte[] CustomDisplayOption { get; } = new byte[3];
        public byte FacialHairStyleID { get; set; }
        public byte PartyType { get; set; }
        public byte NativeSex { get; set; }
        public byte Inebriation { get; set; }
        public byte PvpTitle { get; set; }
        public byte ArenaFaction { get; set; }
        public uint DuelTeam { get; set; }
        public int GuildTimeStamp { get; set; }
        public IQuestLog[] QuestLog { get; } = new IQuestLog[100];
        public IVisibleItem[] VisibleItems { get; } = new IVisibleItem[19];
        public int PlayerTitle { get; set; }
        public int FakeInebriation { get; set; }
        public uint VirtualPlayerRealm { get; set; }
        public uint CurrentSpecID { get; set; }
        public int TaxiMountAnimKitID { get; set; }
        public float[] AvgItemLevel { get; } = new float[4];
        public byte CurrentBattlePetBreedQuality { get; set; }
        public int HonorLevel { get; set; }
        public int Field_B0 { get; set; }
        public int Field_B4 { get; set; }
        public DynamicUpdateField<IArenaCooldown> ArenaCooldowns { get; } = new DynamicUpdateField<IArenaCooldown>();
    }
}

