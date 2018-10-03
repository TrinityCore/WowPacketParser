using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SoundEmitterPillPoints, HasIndexInData = false)]
    public class SoundEmitterPillPointsEntry
    {
        [HotfixArray(3)]
        public float[] Position { get; set; }
        public ushort SoundEmittersID { get; set; }
    }
}
