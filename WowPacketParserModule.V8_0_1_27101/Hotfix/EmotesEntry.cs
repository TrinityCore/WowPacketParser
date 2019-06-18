using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Emotes, HasIndexInData = false)]
    public class EmotesEntry
    {
        public long RaceMask { get; set; }
        public string EmoteSlashCommand { get; set; }
        public int AnimID { get; set; }
        public uint EmoteFlags { get; set; }
        public byte EmoteSpecProc { get; set; }
        public uint EmoteSpecProcParam { get; set; }
        public uint EventSoundID { get; set; }
        public uint SpellVisualKitID { get; set; }
        public int ClassMask { get; set; }
    }
}
