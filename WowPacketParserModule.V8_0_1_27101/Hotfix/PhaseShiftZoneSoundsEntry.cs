using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.PhaseShiftZoneSounds, HasIndexInData = false)]
    public class PhaseShiftZoneSoundsEntry
    {
        public ushort AreaID { get; set; }
        public byte WmoAreaID { get; set; }
        public ushort PhaseID { get; set; }
        public ushort PhaseGroupID { get; set; }
        public byte PhaseUseFlags { get; set; }
        public uint ZoneIntroMusicID { get; set; }
        public uint ZoneMusicID { get; set; }
        public ushort SoundAmbienceID { get; set; }
        public byte SoundProviderPreferencesID { get; set; }
        public uint UwZoneIntroMusicID { get; set; }
        public uint UwZoneMusicID { get; set; }
        public ushort UwSoundAmbienceID { get; set; }
        public byte UwSoundProviderPreferencesID { get; set; }
    }
}
