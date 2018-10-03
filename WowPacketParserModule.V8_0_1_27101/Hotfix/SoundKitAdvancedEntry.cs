using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SoundKitAdvanced)]
    public class SoundKitAdvancedEntry
    {
        public int ID { get; set; }
        public uint SoundKitID { get; set; }
        public float InnerRadius2D { get; set; }
        public float OuterRadius2D { get; set; }
        public uint TimeA { get; set; }
        public uint TimeB { get; set; }
        public uint TimeC { get; set; }
        public uint TimeD { get; set; }
        public int RandomOffsetRange { get; set; }
        public sbyte Usage { get; set; }
        public uint TimeIntervalMin { get; set; }
        public uint TimeIntervalMax { get; set; }
        public uint DelayMin { get; set; }
        public uint DelayMax { get; set; }
        public byte VolumeSliderCategory { get; set; }
        public float DuckToSFX { get; set; }
        public float DuckToMusic { get; set; }
        public float DuckToAmbience { get; set; }
        public float DuckToDialog { get; set; }
        public float DuckToSuppressors { get; set; }
        public float DuckToCinematicSFX { get; set; }
        public float DuckToCinematicMusic { get; set; }
        public float InnerRadiusOfInfluence { get; set; }
        public float OuterRadiusOfInfluence { get; set; }
        public uint TimeToDuck { get; set; }
        public uint TimeToUnduck { get; set; }
        public float InsideAngle { get; set; }
        public float OutsideAngle { get; set; }
        public float OutsideVolume { get; set; }
        public byte MinRandomPosOffset { get; set; }
        public ushort MaxRandomPosOffset { get; set; }
        public int MsOffset { get; set; }
        public uint TimeCooldownMin { get; set; }
        public uint TimeCooldownMax { get; set; }
        public byte MaxInstancesBehavior { get; set; }
        public byte VolumeControlType { get; set; }
        public int VolumeFadeInTimeMin { get; set; }
        public int VolumeFadeInTimeMax { get; set; }
        public uint VolumeFadeInCurveID { get; set; }
        public int VolumeFadeOutTimeMin { get; set; }
        public int VolumeFadeOutTimeMax { get; set; }
        public uint VolumeFadeOutCurveID { get; set; }
        public float ChanceToPlay { get; set; }
    }
}
