using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.LookAtController, HasIndexInData = false)]
    public class LookAtControllerEntry
    {
        public float ReactionEnableDistance { get; set; }
        public uint ReactionWarmUpTimeMSMin { get; set; }
        public uint ReactionWarmUpTimeMSMax { get; set; }
        public ushort ReactionEnableFOVDeg { get; set; }
        public float ReactionGiveupDistance { get; set; }
        public uint ReactionGiveupFOVDeg { get; set; }
        public ushort ReactionGiveupTimeMS { get; set; }
        public ushort ReactionIgnoreTimeMinMS { get; set; }
        public ushort ReactionIgnoreTimeMaxMS { get; set; }
        public byte MaxTorsoYaw { get; set; }
        public byte MaxTorsoYawWhileMoving { get; set; }
        public uint MaxTorsoPitchUp { get; set; }
        public uint MaxTorsoPitchDown { get; set; }
        public byte MaxHeadYaw { get; set; }
        public byte MaxHeadPitch { get; set; }
        public float TorsoSpeedFactor { get; set; }
        public float HeadSpeedFactor { get; set; }
        public byte Flags { get; set; }
    }
}
