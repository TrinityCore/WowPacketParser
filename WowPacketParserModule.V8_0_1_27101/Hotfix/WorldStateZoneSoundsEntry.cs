using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.WorldStateZoneSounds, HasIndexInData = false)]
    public class WorldStateZoneSoundsEntry
    {
        public ushort WorldStateID { get; set; }
        public ushort WorldStateValue { get; set; }
        public ushort AreaID { get; set; }
        public int WmoAreaID { get; set; }
        public ushort ZoneIntroMusicID { get; set; }
        public ushort ZoneMusicID { get; set; }
        public ushort SoundAmbienceID { get; set; }
        public byte SoundProviderPreferencesID { get; set; }
    }
}
