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
            packet.Read<uint>(31);
            packet.Read<uint>("Token", 32);
            packet.Stream.AddValue("ReferenceTime", packet.Read<uint>(32) + int.MinValue);
            packet.Read<StreamDirection>("Direction", 1);
            packet.Read<byte>("MaxItems", 6);
            packet.ReadFourCC("Locale");
            if (packet.Read<bool>(1))
            {
                packet.ReadFourCC("ItemName");
                packet.ReadFourCC("Channel");
            }
            else
                packet.Read<ushort>("Index", 16);
        }

        [BattlenetParser(CacheServerCommand.GetStreamItemsResponse)]
        public static void HandleGetStreamItemsResponse(BattlenetPacket packet)
        {
            packet.Read<ushort>("Offset", 16);
            packet.Read<ushort>("TotalNumItems", 16);
            packet.Read<uint>("Token", 32);
            var items = packet.Read<byte>(6);
            for (var i = 0; i < items; ++i)
            {
                packet.ReadString("Type", 4, "Items", i);
                packet.ReadFourCC("Region", "Items", i);
                packet.Stream.AddValue("ModuleId", Utilities.ByteArrayToHexString(packet.ReadBytes(32)), "Items", i);
                packet.Read<uint>(27);
                packet.Read<uint>("PublicationTime", 32, "Items", i);
            }
        }
    }
}
