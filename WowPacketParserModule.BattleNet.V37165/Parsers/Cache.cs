using System;
using WowPacketParser.Enums.Battlenet;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParserModule.BattleNet.V37165.Enums;

namespace WowPacketParserModule.BattleNet.V37165.Parsers
{
    public static class Cache
    {
        [BattlenetParser(CacheClientCommand.GetStreamItemsRequest)]
        public static void HandleGetStreamItemsRequest(BattlenetPacket packet)
        {
            packet.ReadSkip(31);
            packet.Read<uint>("Token", 0, 32);
            packet.Read<uint>("ReferenceTime", int.MinValue, 32);
            packet.Read<StreamDirection>("Direction", 0, 1);
            packet.Read<byte>("MaxItems", 0, 6);
            packet.ReadFourCC("Locale");
            if (packet.ReadBoolean())
            {
                packet.ReadFourCC("ItemName");
                packet.ReadFourCC("Channel");
            }
            else
                packet.Read<ushort>("Index", 0, 16);
        }

        [BattlenetParser(CacheServerCommand.GetStreamItemsResponse)]
        public static void HandleGetStreamItemsResponse(BattlenetPacket packet)
        {
            packet.Read<ushort>("Offset", 0, 16);
            packet.Read<ushort>("TotalNumItems", 0, 16);
            packet.Read<uint>("Token", 0, 32);
            var items = packet.Read<byte>(0, 6);
            for (var i = 0; i < items; ++i)
            {
                packet.ReadFixedLengthString("Type", 4, "Items", i);
                packet.ReadFourCC("Region", "Items", i);
                packet.ReadBytes("ModuleId", 32, "Items", i);
                packet.ReadSkip(27);
                packet.Read<uint>("PublicationTime", 0, 32, "Items", i);
            }
        }
    }
}
