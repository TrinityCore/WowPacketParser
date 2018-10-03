using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiModelScene, HasIndexInData = false)]
    public class UiModelSceneEntry
    {
        public sbyte UiSystemType { get; set; }
        public byte Flags { get; set; }
    }
}
