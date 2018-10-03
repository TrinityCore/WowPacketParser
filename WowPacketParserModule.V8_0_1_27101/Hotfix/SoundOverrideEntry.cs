using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SoundOverride, HasIndexInData = false)]
    public class SoundOverrideEntry
    {
        public ushort ZoneIntroMusicID { get; set; }
        public ushort ZoneMusicID { get; set; }
        public ushort SoundAmbienceID { get; set; }
        public byte SoundProviderPreferencesID { get; set; }
        public byte Flags { get; set; }
    }
}
