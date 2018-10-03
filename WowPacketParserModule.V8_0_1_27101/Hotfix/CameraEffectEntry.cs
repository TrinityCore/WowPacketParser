using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CameraEffect, HasIndexInData = false)]
    public class CameraEffectEntry
    {
        public byte Flags { get; set; }
    }
}
