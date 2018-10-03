using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.TransportPhysics, HasIndexInData = false)]
    public class TransportPhysicsEntry
    {
        public float WaveAmp { get; set; }
        public float WaveTimeScale { get; set; }
        public float RollAmp { get; set; }
        public float RollTimeScale { get; set; }
        public float PitchAmp { get; set; }
        public float PitchTimeScale { get; set; }
        public float MaxBank { get; set; }
        public float MaxBankTurnSpeed { get; set; }
        public float SpeedDampThresh { get; set; }
        public float SpeedDamp { get; set; }
    }
}
