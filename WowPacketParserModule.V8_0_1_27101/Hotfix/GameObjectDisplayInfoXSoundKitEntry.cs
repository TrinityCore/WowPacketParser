using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GameObjectDisplayInfoXSoundKit, HasIndexInData = false)]
    public class GameObjectDisplayInfoXSoundKitEntry
    {
        public uint SoundKitID { get; set; }
        public sbyte EventIndex { get; set; }
        public int GameObjectDisplayInfoID { get; set; }
    }
}
