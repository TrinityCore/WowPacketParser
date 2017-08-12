using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.Emotes, HasIndexInData = false)]
    public class EmotesEntry
    {
        public string EmoteSlashCommand { get; set; }
        public uint SpellVisualKitID { get; set; }
        public uint EmoteFlags { get; set; }
        public ushort AnimID { get; set; }
        public byte EmoteSpecProc { get; set; }
        public uint EmoteSpecProcParam { get; set; }
        public uint EmoteSoundID { get; set; }
        public uint ClassMask { get; set; }
        public uint RaceMask { get; set; }
    }
}