using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SoundEmitters)]
    public class SoundEmittersEntry
    {
        public string Name { get; set; }
        [HotfixArray(3)]
        public float[] Position { get; set; }
        [HotfixArray(3)]
        public float[] Direction { get; set; }
        public int ID { get; set; }
        public uint SoundEntriesID { get; set; }
        public ushort WorldStateExpressionID { get; set; }
        public byte EmitterType { get; set; }
        public ushort PhaseID { get; set; }
        public uint PhaseGroupID { get; set; }
        public byte PhaseUseFlags { get; set; }
        public byte Flags { get; set; }
        public short MapID { get; set; }
    }
}
