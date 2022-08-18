using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class AddonHandler
    {
        public static void ReadAddOnInfo(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();

            var nameLength = (int)packet.ReadBits(10);
            var versionLength = (int)packet.ReadBits(10);
            packet.ReadBit("Loaded", indexes);
            packet.ReadBit("Disabled", indexes);
            if (nameLength > 1)
                packet.ReadDynamicString("Name", nameLength - 1, indexes);

            if (versionLength > 1)
                packet.ReadDynamicString("Version", versionLength - 1, indexes);
        }
    }
}
