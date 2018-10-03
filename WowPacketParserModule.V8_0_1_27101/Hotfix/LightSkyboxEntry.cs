using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.LightSkybox, HasIndexInData = false)]
    public class LightSkyboxEntry
    {
        public string Name { get; set; }
        public byte Flags { get; set; }
        public int SkyboxFileDataID { get; set; }
        public int CelestialSkyboxFileDataID { get; set; }
    }
}
