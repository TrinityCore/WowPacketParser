using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.Substructures
{
    public static class ItemHandler
    {
        public static int ReadItemInstance602(Packet packet, params object[] indexes)
        {
            var itemId = packet.ReadInt32<ItemId>("ItemID", indexes);
            packet.ReadUInt32("RandomPropertiesSeed", indexes);
            packet.ReadUInt32("RandomPropertiesID", indexes);

            packet.ResetBitReader();

            var hasBonuses = packet.ReadBit("HasItemBonus", indexes);
            var hasModifications = packet.ReadBit("HasModifications", indexes);
            if (hasBonuses)
            {
                packet.ReadByte("Context", indexes);

                var bonusCount = packet.ReadUInt32();
                for (var j = 0; j < bonusCount; ++j)
                    packet.ReadUInt32("BonusListID", indexes, j);
            }

            if (hasModifications)
            {
                var mask = packet.ReadUInt32();
                for (var j = 0; mask != 0; mask >>= 1, ++j)
                    if ((mask & 1) != 0)
                        packet.ReadInt32(((ItemModifier)j).ToString(), indexes);
            }

            packet.ResetBitReader();

            return itemId;
        }

        public static int ReadItemInstance815(Packet packet, params object[] indexes)
        {
            var itemId = packet.ReadInt32<ItemId>("ItemID", indexes);

            packet.ResetBitReader();
            var hasBonuses = packet.ReadBit("HasItemBonus", indexes);
            var hasModifications = packet.ReadBit("HasModifications", indexes);
            if (hasBonuses)
            {
                packet.ReadByte("Context", indexes);

                var bonusCount = packet.ReadUInt32();
                for (var j = 0; j < bonusCount; ++j)
                    packet.ReadInt32("BonusListID", indexes, j);
            }

            if (hasModifications)
            {
                var mask = packet.ReadUInt32();
                for (var j = 0; mask != 0; mask >>= 1, ++j)
                    if ((mask & 1) != 0)
                        packet.ReadUInt32(((ItemModifier)j).ToString(), indexes);
            }

            packet.ResetBitReader();

            return itemId;
        }

        public static int ReadItemInstance(Packet packet, params object[] indexes)
        {
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V8_1_5_29683))
                return ReadItemInstance602(packet, indexes);
            return ReadItemInstance815(packet, indexes);
        }
    }
}
