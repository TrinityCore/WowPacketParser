using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V1_60_1_69876.Parsers
{
    public static class ItemHandler
    {
        public static void ReadUIEventToast(Packet packet, params object[] args)
        {
            packet.ReadInt32("UiEventToastID", args);
            packet.ReadInt32("Asset", args);
        }
    }
}
