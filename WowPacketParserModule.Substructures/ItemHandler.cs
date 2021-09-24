using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.Substructures
{
    public static class ItemHandler
    {
        public static ItemInstance ReadItemInstance602(Packet packet, params object[] indexes)
        {
            ItemInstance instance = new ItemInstance();
            instance.ItemID = packet.ReadInt32<ItemId>("ItemID", indexes);
            instance.RandomPropertiesSeed = packet.ReadUInt32("RandomPropertiesSeed", indexes);
            instance.RandomPropertiesID = packet.ReadUInt32("RandomPropertiesID", indexes);

            packet.ResetBitReader();

            var hasBonuses = packet.ReadBit("HasItemBonus", indexes);
            var hasModifications = packet.ReadBit("HasModifications", indexes);
            if (hasBonuses)
            {
                instance.Context = packet.ReadByte("Context", indexes);

                var bonusCount = packet.ReadUInt32();
                instance.BonusListIDs = new uint[bonusCount];
                for (var j = 0; j < bonusCount; ++j)
                    instance.BonusListIDs[j] = packet.ReadUInt32("BonusListID", indexes, j);
            }

            if (hasModifications)
            {
                var mask = packet.ReadUInt32();
                for (var j = 0; mask != 0; mask >>= 1, ++j)
                {
                    if ((mask & 1) != 0)
                    {
                        ItemModifier mod = (ItemModifier)j;
                        instance.ItemModifier[mod] = packet.ReadInt32(mod.ToString(), indexes);
                    }
                }
            }
            return instance;
        }

        public static ItemInstance ReadItemInstance815(Packet packet, params object[] indexes)
        {
            ItemInstance instance = new ItemInstance();
            instance.ItemID = packet.ReadInt32<ItemId>("ItemID", indexes);

            packet.ResetBitReader();
            var hasBonuses = packet.ReadBit("HasItemBonus", indexes);
            var hasModifications = packet.ReadBit("HasModifications", indexes);
            if (hasBonuses)
            {
                instance.Context = packet.ReadByte("Context", indexes);

                var bonusCount = packet.ReadUInt32();
                instance.BonusListIDs = new uint[bonusCount];
                for (var j = 0; j < bonusCount; ++j)
                    instance.BonusListIDs[j] = packet.ReadUInt32("BonusListID", indexes, j);
            }

            if (hasModifications)
            {
                var mask = packet.ReadUInt32();
                for (var j = 0; mask != 0; mask >>= 1, ++j)
                {
                    if ((mask & 1) != 0)
                    {
                        ItemModifier mod = (ItemModifier)j;
                        instance.ItemModifier[mod] = packet.ReadInt32(mod.ToString(), indexes);
                    }
                }
            }
            return instance;
        }

        public static ItemInstance ReadItemInstance901(Packet packet, params object[] indexes)
        {
            ItemInstance instance = new ItemInstance();
            instance.ItemID = packet.ReadInt32<ItemId>("ItemID", indexes);

            packet.ResetBitReader();
            var hasBonuses = packet.ReadBit("HasItemBonus", indexes);

            {
                packet.ResetBitReader();
                var modificationCount = packet.ReadBits(6);
                for (var j = 0u; j < modificationCount; ++j)
                {
                    var value = packet.ReadInt32();
                    ItemModifier mod = packet.ReadByteE<ItemModifier>();
                    packet.AddValue(mod.ToString(), value, indexes);
                    instance.ItemModifier[mod] = value;
                }
            }

            if (hasBonuses)
            {
                instance.Context = packet.ReadByte("Context", indexes);

                var bonusCount = packet.ReadUInt32();
                instance.BonusListIDs = new uint[bonusCount];
                for (var j = 0; j < bonusCount; ++j)
                    instance.BonusListIDs[j] = packet.ReadUInt32("BonusListID", indexes, j);
            }

            return instance;
        }

        public static ItemInstance ReadItemInstance251(Packet packet, params object[] indexes)
        {
            ItemInstance instance = new ItemInstance();
            instance.ItemID = packet.ReadInt32<ItemId>("ItemID", indexes);
            instance.RandomPropertiesSeed = packet.ReadUInt32("RandomPropertiesSeed", indexes);
            instance.RandomPropertiesID = packet.ReadUInt32("RandomPropertiesID", indexes);

            packet.ResetBitReader();
            var hasBonuses = packet.ReadBit("HasItemBonus", indexes);

            {
                packet.ResetBitReader();
                var modificationCount = packet.ReadBits(6);
                for (var j = 0u; j < modificationCount; ++j)
                {
                    var value = packet.ReadInt32();
                    ItemModifier mod = packet.ReadByteE<ItemModifier>();
                    packet.AddValue(mod.ToString(), value, indexes);
                    instance.ItemModifier[mod] = value;
                }
            }

            if (hasBonuses)
            {
                instance.Context = packet.ReadByte("Context", indexes);

                var bonusCount = packet.ReadUInt32();
                instance.BonusListIDs = new uint[bonusCount];
                for (var j = 0; j < bonusCount; ++j)
                    instance.BonusListIDs[j] = packet.ReadUInt32("BonusListID", indexes, j);
            }

            return instance;
        }

        public static ItemInstance ReadItemInstance(Packet packet, params object[] indexes)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V2_5_1_38835) && ClientVersion.IsBurningCrusadeClassicClientVersionBuild(ClientVersion.Build))
                return ReadItemInstance251(packet, indexes);
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V8_1_5_29683) || ClientVersion.IsClassicVanillaClientVersionBuild(ClientVersion.Build))
                return ReadItemInstance602(packet, indexes);
            if (ClientVersion.AddedInVersion(ClientType.Shadowlands))
                return ReadItemInstance901(packet, indexes);
            return ReadItemInstance815(packet, indexes);
        }
    }
}
