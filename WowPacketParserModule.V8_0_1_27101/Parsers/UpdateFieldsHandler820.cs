using System.Collections;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing.Parsers;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_2_0_30898
{
    public class UpdateFieldHandler : UpdateFieldsHandlerBase
    {
        public override IObjectData ReadCreateObjectData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            var data = new ObjectData();
            data.EntryID = packet.ReadInt32("EntryID", indexes);
            data.DynamicFlags = packet.ReadUInt32("DynamicFlags", indexes);
            data.Scale = packet.ReadSingle("Scale", indexes);
            return data;
        }

        public override IObjectData ReadUpdateObjectData(Packet packet, params object[] indexes)
        {
            var data = new ObjectData();
            var rawChangesMask = new int[1];
            rawChangesMask[0] = (int)packet.ReadBits(4);
            var changesMask = new BitArray(rawChangesMask);

            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.EntryID = packet.ReadInt32("EntryID", indexes);
                }
                if (changesMask[2])
                {
                    data.DynamicFlags = packet.ReadUInt32("DynamicFlags", indexes);
                }
                if (changesMask[3])
                {
                    data.Scale = packet.ReadSingle("Scale", indexes);
                }
            }
            return data;
        }

        public static IItemEnchantment ReadCreateItemEnchantment(Packet packet, params object[] indexes)
        {
            var data = new ItemEnchantment();
            data.ID = packet.ReadInt32("ID", indexes);
            data.Duration = packet.ReadUInt32("Duration", indexes);
            data.Charges = packet.ReadInt16("Charges", indexes);
            data.Inactive = packet.ReadUInt16("Inactive", indexes);
            return data;
        }

        public static IItemEnchantment ReadUpdateItemEnchantment(Packet packet, IItemEnchantment existingData, params object[] indexes)
        {
            var data = existingData as ItemEnchantment;
            if (data == null)
                data = new ItemEnchantment();
            var rawChangesMask = new int[1];
            rawChangesMask[0] = (int)packet.ReadBits(5);
            var changesMask = new BitArray(rawChangesMask);

            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.ID = packet.ReadInt32("ID", indexes);
                }
                if (changesMask[2])
                {
                    data.Duration = packet.ReadUInt32("Duration", indexes);
                }
                if (changesMask[3])
                {
                    data.Charges = packet.ReadInt16("Charges", indexes);
                }
                if (changesMask[4])
                {
                    data.Inactive = packet.ReadUInt16("Inactive", indexes);
                }
            }
            return data;
        }

        public static IArtifactPower ReadCreateArtifactPower(Packet packet, params object[] indexes)
        {
            var data = new ArtifactPower();
            data.ArtifactPowerID = packet.ReadInt16("ArtifactPowerID", indexes);
            data.PurchasedRank = packet.ReadByte("PurchasedRank", indexes);
            data.CurrentRankWithBonus = packet.ReadByte("CurrentRankWithBonus", indexes);
            return data;
        }

        public static IArtifactPower ReadUpdateArtifactPower(Packet packet, IArtifactPower existingData, params object[] indexes)
        {
            var data = existingData as ArtifactPower;
            if (data == null)
                data = new ArtifactPower();
            data.ArtifactPowerID = packet.ReadInt16("ArtifactPowerID", indexes);
            data.PurchasedRank = packet.ReadByte("PurchasedRank", indexes);
            data.CurrentRankWithBonus = packet.ReadByte("CurrentRankWithBonus", indexes);
            return data;
        }

        public static ISocketedGem ReadCreateSocketedGem(Packet packet, params object[] indexes)
        {
            var data = new SocketedGem();
            data.ItemID = packet.ReadInt32("ItemID", indexes);
            for (var i = 0; i < 16; ++i)
            {
                data.BonusListIDs[i] = packet.ReadUInt16("BonusListIDs", indexes, i);
            }
            data.Context = packet.ReadByte("Context", indexes);
            return data;
        }

        public static ISocketedGem ReadUpdateSocketedGem(Packet packet, ISocketedGem existingData, params object[] indexes)
        {
            var data = existingData as SocketedGem;
            if (data == null)
                data = new SocketedGem();
            var rawChangesMask = new int[1];
            var rawMaskMask = new int[1];
            rawMaskMask[0] = (int)packet.ReadBits(1);
            var maskMask = new BitArray(rawMaskMask);
            if (maskMask[0])
                rawChangesMask[0] = (int)packet.ReadBits(32);
            var changesMask = new BitArray(rawChangesMask);

            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.ItemID = packet.ReadInt32("ItemID", indexes);
                }
                if (changesMask[2])
                {
                    data.Context = packet.ReadByte("Context", indexes);
                }
            }
            if (changesMask[3])
            {
                for (var i = 0; i < 16; ++i)
                {
                    if (changesMask[4 + i])
                    {
                        data.BonusListIDs[i] = packet.ReadUInt16("BonusListIDs", indexes, i);
                    }
                }
            }
            return data;
        }

        public override IItemData ReadCreateItemData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            var data = new ItemData();
            data.BonusListIDs = new int[packet.ReadUInt32()];
            for (var i = 0; i < data.BonusListIDs.Length; ++i)
            {
                data.BonusListIDs[i] = packet.ReadInt32("BonusListIDs", indexes, i);
            }
            data.Owner = packet.ReadPackedGuid128("Owner", indexes);
            data.ContainedIn = packet.ReadPackedGuid128("ContainedIn", indexes);
            data.Creator = packet.ReadPackedGuid128("Creator", indexes);
            data.GiftCreator = packet.ReadPackedGuid128("GiftCreator", indexes);
            if ((flags & UpdateFieldFlag.Owner) != UpdateFieldFlag.None)
            {
                data.StackCount = packet.ReadUInt32("StackCount", indexes);
                data.Expiration = packet.ReadUInt32("Expiration", indexes);
                for (var i = 0; i < 5; ++i)
                {
                    data.SpellCharges[i] = packet.ReadInt32("SpellCharges", indexes, i);
                }
            }
            data.DynamicFlags = packet.ReadUInt32("DynamicFlags", indexes);
            for (var i = 0; i < 13; ++i)
            {
                data.Enchantment[i] = ReadCreateItemEnchantment(packet, indexes, "Enchantment", i);
            }
            if ((flags & UpdateFieldFlag.Owner) != UpdateFieldFlag.None)
            {
                data.Durability = packet.ReadUInt32("Durability", indexes);
                data.MaxDurability = packet.ReadUInt32("MaxDurability", indexes);
            }
            data.CreatePlayedTime = packet.ReadUInt32("CreatePlayedTime", indexes);
            if ((flags & UpdateFieldFlag.Owner) != UpdateFieldFlag.None)
            {
                data.ModifiersMask = packet.ReadUInt32("ModifiersMask", indexes);
            }
            data.Context = packet.ReadInt32("Context", indexes);
            if ((flags & UpdateFieldFlag.Owner) != UpdateFieldFlag.None)
            {
                data.ArtifactXP = packet.ReadUInt64("ArtifactXP", indexes);
                data.ItemAppearanceModID = packet.ReadByte("ItemAppearanceModID", indexes);
            }
            data.Modifiers.Resize(packet.ReadUInt32());
            data.ArtifactPowers.Resize(packet.ReadUInt32());
            data.Gems.Resize(packet.ReadUInt32());
            if ((flags & UpdateFieldFlag.Owner) != UpdateFieldFlag.None)
            {
                data.ZoneFlags = packet.ReadUInt32("ZoneFlags", indexes);
            }
            for (var i = 0; i < data.Modifiers.Count; ++i)
            {
                data.Modifiers[i] = packet.ReadInt32("Modifiers", indexes, i);
            }
            for (var i = 0; i < data.ArtifactPowers.Count; ++i)
            {
                data.ArtifactPowers[i] = ReadCreateArtifactPower(packet, indexes, "ArtifactPowers", i);
            }
            for (var i = 0; i < data.Gems.Count; ++i)
            {
                data.Gems[i] = ReadCreateSocketedGem(packet, indexes, "Gems", i);
            }
            return data;
        }

        public override IItemData ReadUpdateItemData(Packet packet, params object[] indexes)
        {
            var data = new ItemData();
            var rawChangesMask = new int[2];
            var rawMaskMask = new int[1];
            rawMaskMask[0] = (int)packet.ReadBits(2);
            var maskMask = new BitArray(rawMaskMask);
            for (var i = 0; i < 2; ++i)
                if (maskMask[i])
                    rawChangesMask[i] = (int)packet.ReadBits(32);
            var changesMask = new BitArray(rawChangesMask);

            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.BonusListIDs = Enumerable.Range(0, (int)packet.ReadBits(32)).Select(x => new int()).Cast<int>().ToArray();
                    for (var i = 0; i < data.BonusListIDs.Length; ++i)
                    {
                        data.BonusListIDs[i] = packet.ReadInt32("BonusListIDs", indexes, i);
                    }
                }
            }
            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[2])
                {
                    data.Modifiers.ReadUpdateMask(packet);
                }
                if (changesMask[3])
                {
                    data.ArtifactPowers.ReadUpdateMask(packet);
                }
                if (changesMask[4])
                {
                    data.Gems.ReadUpdateMask(packet);
                }
            }
            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[2])
                {
                    for (var i = 0; i < data.Modifiers.Count; ++i)
                    {
                        if (data.Modifiers.UpdateMask[i])
                        {
                            data.Modifiers[i] = packet.ReadInt32("Modifiers", indexes, i);
                        }
                    }
                }
                if (changesMask[3])
                {
                    for (var i = 0; i < data.ArtifactPowers.Count; ++i)
                    {
                        if (data.ArtifactPowers.UpdateMask[i])
                        {
                            data.ArtifactPowers[i] = ReadUpdateArtifactPower(packet, data.ArtifactPowers[i] as ArtifactPower, indexes, "ArtifactPowers", i);
                        }
                    }
                }
                if (changesMask[4])
                {
                    for (var i = 0; i < data.Gems.Count; ++i)
                    {
                        if (data.Gems.UpdateMask[i])
                        {
                            data.Gems[i] = ReadUpdateSocketedGem(packet, data.Gems[i] as SocketedGem, indexes, "Gems", i);
                        }
                    }
                }
                if (changesMask[5])
                {
                    data.Owner = packet.ReadPackedGuid128("Owner", indexes);
                }
                if (changesMask[6])
                {
                    data.ContainedIn = packet.ReadPackedGuid128("ContainedIn", indexes);
                }
                if (changesMask[7])
                {
                    data.Creator = packet.ReadPackedGuid128("Creator", indexes);
                }
                if (changesMask[8])
                {
                    data.GiftCreator = packet.ReadPackedGuid128("GiftCreator", indexes);
                }
                if (changesMask[9])
                {
                    data.StackCount = packet.ReadUInt32("StackCount", indexes);
                }
                if (changesMask[10])
                {
                    data.Expiration = packet.ReadUInt32("Expiration", indexes);
                }
                if (changesMask[11])
                {
                    data.DynamicFlags = packet.ReadUInt32("DynamicFlags", indexes);
                }
                if (changesMask[12])
                {
                    data.Durability = packet.ReadUInt32("Durability", indexes);
                }
                if (changesMask[13])
                {
                    data.MaxDurability = packet.ReadUInt32("MaxDurability", indexes);
                }
                if (changesMask[14])
                {
                    data.CreatePlayedTime = packet.ReadUInt32("CreatePlayedTime", indexes);
                }
                if (changesMask[15])
                {
                    data.ModifiersMask = packet.ReadUInt32("ModifiersMask", indexes);
                }
                if (changesMask[16])
                {
                    data.Context = packet.ReadInt32("Context", indexes);
                }
                if (changesMask[17])
                {
                    data.ArtifactXP = packet.ReadUInt64("ArtifactXP", indexes);
                }
                if (changesMask[18])
                {
                    data.ItemAppearanceModID = packet.ReadByte("ItemAppearanceModID", indexes);
                }
                if (changesMask[19])
                {
                    data.ZoneFlags = packet.ReadUInt32("ZoneFlags", indexes);
                }
            }
            if (changesMask[20])
            {
                for (var i = 0; i < 5; ++i)
                {
                    if (changesMask[21 + i])
                    {
                        data.SpellCharges[i] = packet.ReadInt32("SpellCharges", indexes, i);
                    }
                }
            }
            if (changesMask[26])
            {
                for (var i = 0; i < 13; ++i)
                {
                    if (changesMask[27 + i])
                    {
                        data.Enchantment[i] = ReadUpdateItemEnchantment(packet, data.Enchantment[i] as ItemEnchantment, indexes, "Enchantment", i);
                    }
                }
            }
            return data;
        }

        public override IContainerData ReadCreateContainerData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            var data = new ContainerData();
            for (var i = 0; i < 36; ++i)
            {
                data.Slots[i] = packet.ReadPackedGuid128("Slots", indexes, i);
            }
            data.NumSlots = packet.ReadUInt32("NumSlots", indexes);
            return data;
        }

        public override IContainerData ReadUpdateContainerData(Packet packet, params object[] indexes)
        {
            var data = new ContainerData();
            var rawChangesMask = new int[2];
            var rawMaskMask = new int[1];
            rawMaskMask[0] = (int)packet.ReadBits(2);
            var maskMask = new BitArray(rawMaskMask);
            for (var i = 0; i < 2; ++i)
                if (maskMask[i])
                    rawChangesMask[i] = (int)packet.ReadBits(32);
            var changesMask = new BitArray(rawChangesMask);

            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.NumSlots = packet.ReadUInt32("NumSlots", indexes);
                }
            }
            if (changesMask[2])
            {
                for (var i = 0; i < 36; ++i)
                {
                    if (changesMask[3 + i])
                    {
                        data.Slots[i] = packet.ReadPackedGuid128("Slots", indexes, i);
                    }
                }
            }
            return data;
        }

        public override IAzeriteEmpoweredItemData ReadCreateAzeriteEmpoweredItemData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            var data = new AzeriteEmpoweredItemData();
            for (var i = 0; i < 5; ++i)
            {
                data.Selections[i] = packet.ReadInt32("Selections", indexes, i);
            }
            return data;
        }

        public override IAzeriteEmpoweredItemData ReadUpdateAzeriteEmpoweredItemData(Packet packet, params object[] indexes)
        {
            var data = new AzeriteEmpoweredItemData();
            var rawChangesMask = new int[1];
            var rawMaskMask = new int[1];
            rawMaskMask[0] = (int)packet.ReadBits(1);
            var maskMask = new BitArray(rawMaskMask);
            if (maskMask[0])
                rawChangesMask[0] = (int)packet.ReadBits(32);
            var changesMask = new BitArray(rawChangesMask);

            packet.ResetBitReader();
            if (changesMask[0])
            {
                for (var i = 0; i < 5; ++i)
                {
                    if (changesMask[1 + i])
                    {
                        data.Selections[i] = packet.ReadInt32("Selections", indexes, i);
                    }
                }
            }
            return data;
        }

        public static IUnlockedAzeriteEssence ReadCreateUnlockedAzeriteEssence(Packet packet, params object[] indexes)
        {
            var data = new UnlockedAzeriteEssence();
            data.AzeriteEssenceID = packet.ReadUInt32("AzeriteEssenceID", indexes);
            data.Rank = packet.ReadUInt32("Rank", indexes);
            return data;
        }

        public static IUnlockedAzeriteEssence ReadUpdateUnlockedAzeriteEssence(Packet packet, IUnlockedAzeriteEssence existingData, params object[] indexes)
        {
            var data = existingData as UnlockedAzeriteEssence;
            if (data == null)
                data = new UnlockedAzeriteEssence();
            data.AzeriteEssenceID = packet.ReadUInt32("AzeriteEssenceID", indexes);
            data.Rank = packet.ReadUInt32("Rank", indexes);
            return data;
        }

        public static ISelectedAzeriteEssences ReadCreateSelectedAzeriteEssences(Packet packet, params object[] indexes)
        {
            var data = new SelectedAzeriteEssences();
            packet.ResetBitReader();
            for (var i = 0; i < 3; ++i)
            {
                data.AzeriteEssenceID[i] = packet.ReadUInt32("AzeriteEssenceID", indexes, i);
            }
            data.SpecializationID = packet.ReadUInt32("SpecializationID", indexes);
            data.Enabled = packet.ReadBits("Enabled", 1, indexes);
            return data;
        }

        public static ISelectedAzeriteEssences ReadUpdateSelectedAzeriteEssences(Packet packet, ISelectedAzeriteEssences existingData, params object[] indexes)
        {
            var data = existingData as SelectedAzeriteEssences;
            if (data == null)
                data = new SelectedAzeriteEssences();
            packet.ResetBitReader();
            var rawChangesMask = new int[1];
            var rawMaskMask = new int[1];
            rawMaskMask[0] = (int)packet.ReadBits(1);
            var maskMask = new BitArray(rawMaskMask);
            if (maskMask[0])
                rawChangesMask[0] = (int)packet.ReadBits(32);
            var changesMask = new BitArray(rawChangesMask);

            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.Enabled = packet.ReadBits("Enabled", 1, indexes);
                }
                if (changesMask[2])
                {
                    data.SpecializationID = packet.ReadUInt32("SpecializationID", indexes);
                }
            }
            if (changesMask[3])
            {
                for (var i = 0; i < 3; ++i)
                {
                    if (changesMask[4 + i])
                    {
                        data.AzeriteEssenceID[i] = packet.ReadUInt32("AzeriteEssenceID", indexes, i);
                    }
                }
            }
            return data;
        }

        public override IAzeriteItemData ReadCreateAzeriteItemData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            var data = new AzeriteItemData();
            if ((flags & UpdateFieldFlag.Owner) != UpdateFieldFlag.None)
            {
                data.Xp = packet.ReadUInt64("Xp", indexes);
                data.Level = packet.ReadUInt32("Level", indexes);
                data.AuraLevel = packet.ReadUInt32("AuraLevel", indexes);
                data.KnowledgeLevel = packet.ReadUInt32("KnowledgeLevel", indexes);
                data.DEBUGknowledgeWeek = packet.ReadInt32("DEBUGknowledgeWeek", indexes);
            }
            data.UnlockedEssences.Resize(packet.ReadUInt32());
            data.SelectedEssences.Resize(packet.ReadUInt32());
            data.UnlockedEssenceMilestones.Resize(packet.ReadUInt32());
            for (var i = 0; i < data.UnlockedEssences.Count; ++i)
            {
                data.UnlockedEssences[i] = ReadCreateUnlockedAzeriteEssence(packet, indexes, "UnlockedEssences", i);
            }
            for (var i = 0; i < data.UnlockedEssenceMilestones.Count; ++i)
            {
                data.UnlockedEssenceMilestones[i] = packet.ReadUInt32("UnlockedEssenceMilestones", indexes, i);
            }
            for (var i = 0; i < data.SelectedEssences.Count; ++i)
            {
                data.SelectedEssences[i] = ReadCreateSelectedAzeriteEssences(packet, indexes, "SelectedEssences", i);
            }
            return data;
        }

        public override IAzeriteItemData ReadUpdateAzeriteItemData(Packet packet, params object[] indexes)
        {
            var data = new AzeriteItemData();
            var rawChangesMask = new int[1];
            rawChangesMask[0] = (int)packet.ReadBits(9);
            var changesMask = new BitArray(rawChangesMask);

            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.UnlockedEssences.ReadUpdateMask(packet);
                }
                if (changesMask[2])
                {
                    data.SelectedEssences.ReadUpdateMask(packet);
                }
                if (changesMask[3])
                {
                    data.UnlockedEssenceMilestones.ReadUpdateMask(packet);
                }
            }
            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    for (var i = 0; i < data.UnlockedEssences.Count; ++i)
                    {
                        if (data.UnlockedEssences.UpdateMask[i])
                        {
                            data.UnlockedEssences[i] = ReadUpdateUnlockedAzeriteEssence(packet, data.UnlockedEssences[i] as UnlockedAzeriteEssence, indexes, "UnlockedEssences", i);
                        }
                    }
                }
                if (changesMask[3])
                {
                    for (var i = 0; i < data.UnlockedEssenceMilestones.Count; ++i)
                    {
                        if (data.UnlockedEssenceMilestones.UpdateMask[i])
                        {
                            data.UnlockedEssenceMilestones[i] = packet.ReadUInt32("UnlockedEssenceMilestones", indexes, i);
                        }
                    }
                }
                if (changesMask[2])
                {
                    for (var i = 0; i < data.SelectedEssences.Count; ++i)
                    {
                        if (data.SelectedEssences.UpdateMask[i])
                        {
                            data.SelectedEssences[i] = ReadUpdateSelectedAzeriteEssences(packet, data.SelectedEssences[i] as SelectedAzeriteEssences, indexes, "SelectedEssences", i);
                        }
                    }
                }
                if (changesMask[4])
                {
                    data.Xp = packet.ReadUInt64("Xp", indexes);
                }
                if (changesMask[5])
                {
                    data.Level = packet.ReadUInt32("Level", indexes);
                }
                if (changesMask[6])
                {
                    data.AuraLevel = packet.ReadUInt32("AuraLevel", indexes);
                }
                if (changesMask[7])
                {
                    data.KnowledgeLevel = packet.ReadUInt32("KnowledgeLevel", indexes);
                }
                if (changesMask[8])
                {
                    data.DEBUGknowledgeWeek = packet.ReadInt32("DEBUGknowledgeWeek", indexes);
                }
            }
            return data;
        }

        public static IUnitChannel ReadCreateUnitChannel(Packet packet, params object[] indexes)
        {
            var data = new UnitChannel();
            data.SpellID = packet.ReadInt32("SpellID", indexes);
            data.SpellXSpellVisualID = packet.ReadInt32("SpellXSpellVisualID", indexes);
            return data;
        }

        public static IUnitChannel ReadUpdateUnitChannel(Packet packet, IUnitChannel existingData, params object[] indexes)
        {
            var data = existingData as UnitChannel;
            if (data == null)
                data = new UnitChannel();
            data.SpellID = packet.ReadInt32("SpellID", indexes);
            data.SpellXSpellVisualID = packet.ReadInt32("SpellXSpellVisualID", indexes);
            return data;
        }

        public static IVisibleItem ReadCreateVisibleItem(Packet packet, params object[] indexes)
        {
            var data = new VisibleItem();
            data.ItemID = packet.ReadInt32("ItemID", indexes);
            data.ItemAppearanceModID = packet.ReadUInt16("ItemAppearanceModID", indexes);
            data.ItemVisual = packet.ReadUInt16("ItemVisual", indexes);
            return data;
        }

        public static IVisibleItem ReadUpdateVisibleItem(Packet packet, IVisibleItem existingData, params object[] indexes)
        {
            var data = existingData as VisibleItem;
            if (data == null)
                data = new VisibleItem();
            var rawChangesMask = new int[1];
            rawChangesMask[0] = (int)packet.ReadBits(4);
            var changesMask = new BitArray(rawChangesMask);

            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.ItemID = packet.ReadInt32("ItemID", indexes);
                }
                if (changesMask[2])
                {
                    data.ItemAppearanceModID = packet.ReadUInt16("ItemAppearanceModID", indexes);
                }
                if (changesMask[3])
                {
                    data.ItemVisual = packet.ReadUInt16("ItemVisual", indexes);
                }
            }
            return data;
        }

        public static IPassiveSpellHistory ReadCreatePassiveSpellHistory(Packet packet, params object[] indexes)
        {
            var data = new PassiveSpellHistory();
            data.SpellID = packet.ReadInt32("SpellID", indexes);
            data.AuraSpellID = packet.ReadInt32("AuraSpellID", indexes);
            return data;
        }

        public static IPassiveSpellHistory ReadUpdatePassiveSpellHistory(Packet packet, IPassiveSpellHistory existingData, params object[] indexes)
        {
            var data = existingData as PassiveSpellHistory;
            if (data == null)
                data = new PassiveSpellHistory();
            data.SpellID = packet.ReadInt32("SpellID", indexes);
            data.AuraSpellID = packet.ReadInt32("AuraSpellID", indexes);
            return data;
        }

        public override IUnitData ReadCreateUnitData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            var data = new UnitData();
            data.DisplayID = packet.ReadInt32("DisplayID", indexes);
            for (var i = 0; i < 2; ++i)
            {
                data.NpcFlags[i] = packet.ReadUInt32("NpcFlags", indexes, i);
            }
            data.StateSpellVisualID = packet.ReadUInt32("StateSpellVisualID", indexes);
            data.StateAnimID = packet.ReadUInt32("StateAnimID", indexes);
            data.StateAnimKitID = packet.ReadUInt32("StateAnimKitID", indexes);
            data.StateWorldEffectIDs = new System.Nullable<uint>[packet.ReadUInt32()];
            data.StateWorldEffectsQuestObjectiveID = packet.ReadUInt32("StateWorldEffectsQuestObjectiveID", indexes);
            for (var i = 0; i < data.StateWorldEffectIDs.Length; ++i)
            {
                data.StateWorldEffectIDs[i] = packet.ReadUInt32("StateWorldEffectIDs", indexes, i);
            }
            data.Charm = packet.ReadPackedGuid128("Charm", indexes);
            data.Summon = packet.ReadPackedGuid128("Summon", indexes);
            if ((flags & UpdateFieldFlag.Owner) != UpdateFieldFlag.None)
            {
                data.Critter = packet.ReadPackedGuid128("Critter", indexes);
            }
            data.CharmedBy = packet.ReadPackedGuid128("CharmedBy", indexes);
            data.SummonedBy = packet.ReadPackedGuid128("SummonedBy", indexes);
            data.CreatedBy = packet.ReadPackedGuid128("CreatedBy", indexes);
            data.DemonCreator = packet.ReadPackedGuid128("DemonCreator", indexes);
            data.LookAtControllerTarget = packet.ReadPackedGuid128("LookAtControllerTarget", indexes);
            data.Target = packet.ReadPackedGuid128("Target", indexes);
            data.BattlePetCompanionGUID = packet.ReadPackedGuid128("BattlePetCompanionGUID", indexes);
            data.BattlePetDBID = packet.ReadUInt64("BattlePetDBID", indexes);
            data.ChannelData = ReadCreateUnitChannel(packet, indexes, "ChannelData");
            data.SummonedByHomeRealm = packet.ReadUInt32("SummonedByHomeRealm", indexes);
            data.Race = packet.ReadByte("Race", indexes);
            data.ClassId = packet.ReadByte("ClassId", indexes);
            data.PlayerClassId = packet.ReadByte("PlayerClassId", indexes);
            data.Sex = packet.ReadByte("Sex", indexes);
            data.DisplayPower = packet.ReadByte("DisplayPower", indexes);
            data.OverrideDisplayPowerID = packet.ReadUInt32("OverrideDisplayPowerID", indexes);
            data.Health = packet.ReadInt64("Health", indexes);
            for (var i = 0; i < 6; ++i)
            {
                data.Power[i] = packet.ReadInt32("Power", indexes, i);
                data.MaxPower[i] = packet.ReadInt32("MaxPower", indexes, i);
            }
            if ((flags & (UpdateFieldFlag.Owner | UpdateFieldFlag.UnitAll)) != UpdateFieldFlag.None)
            {
                for (var i = 0; i < 6; ++i)
                {
                    data.PowerRegenFlatModifier[i] = packet.ReadSingle("PowerRegenFlatModifier", indexes, i);
                    data.PowerRegenInterruptedFlatModifier[i] = packet.ReadSingle("PowerRegenInterruptedFlatModifier", indexes, i);
                }
            }
            data.MaxHealth = packet.ReadInt64("MaxHealth", indexes);
            data.Level = packet.ReadInt32("Level", indexes);
            data.EffectiveLevel = packet.ReadInt32("EffectiveLevel", indexes);
            data.ContentTuningID = packet.ReadInt32("ContentTuningID", indexes);
            data.ScalingLevelMin = packet.ReadInt32("ScalingLevelMin", indexes);
            data.ScalingLevelMax = packet.ReadInt32("ScalingLevelMax", indexes);
            data.ScalingLevelDelta = packet.ReadInt32("ScalingLevelDelta", indexes);
            data.ScalingFactionGroup = packet.ReadInt32("ScalingFactionGroup", indexes);
            data.ScalingHealthItemLevelCurveID = packet.ReadInt32("ScalingHealthItemLevelCurveID", indexes);
            data.ScalingDamageItemLevelCurveID = packet.ReadInt32("ScalingDamageItemLevelCurveID", indexes);
            data.FactionTemplate = packet.ReadInt32("FactionTemplate", indexes);
            for (var i = 0; i < 3; ++i)
            {
                data.VirtualItems[i] = ReadCreateVisibleItem(packet, indexes, "VirtualItems", i);
            }
            data.Flags = packet.ReadUInt32("Flags", indexes);
            data.Flags2 = packet.ReadUInt32("Flags2", indexes);
            data.Flags3 = packet.ReadUInt32("Flags3", indexes);
            data.AuraState = packet.ReadUInt32("AuraState", indexes);
            for (var i = 0; i < 2; ++i)
            {
                data.AttackRoundBaseTime[i] = packet.ReadUInt32("AttackRoundBaseTime", indexes, i);
            }
            if ((flags & UpdateFieldFlag.Owner) != UpdateFieldFlag.None)
            {
                data.RangedAttackRoundBaseTime = packet.ReadUInt32("RangedAttackRoundBaseTime", indexes);
            }
            data.BoundingRadius = packet.ReadSingle("BoundingRadius", indexes);
            data.CombatReach = packet.ReadSingle("CombatReach", indexes);
            data.DisplayScale = packet.ReadSingle("DisplayScale", indexes);
            data.NativeDisplayID = packet.ReadInt32("NativeDisplayID", indexes);
            data.NativeXDisplayScale = packet.ReadSingle("NativeXDisplayScale", indexes);
            data.MountDisplayID = packet.ReadInt32("MountDisplayID", indexes);
            data.CosmeticMountDisplayID = packet.ReadInt32("CosmeticMountDisplayID", indexes);
            if ((flags & (UpdateFieldFlag.Owner | UpdateFieldFlag.Empath)) != UpdateFieldFlag.None)
            {
                data.MinDamage = packet.ReadSingle("MinDamage", indexes);
                data.MaxDamage = packet.ReadSingle("MaxDamage", indexes);
                data.MinOffHandDamage = packet.ReadSingle("MinOffHandDamage", indexes);
                data.MaxOffHandDamage = packet.ReadSingle("MaxOffHandDamage", indexes);
            }
            data.StandState = packet.ReadByte("StandState", indexes);
            data.PetTalentPoints = packet.ReadByte("PetTalentPoints", indexes);
            data.VisFlags = packet.ReadByte("VisFlags", indexes);
            data.AnimTier = packet.ReadByte("AnimTier", indexes);
            data.PetNumber = packet.ReadUInt32("PetNumber", indexes);
            data.PetNameTimestamp = packet.ReadUInt32("PetNameTimestamp", indexes);
            data.PetExperience = packet.ReadUInt32("PetExperience", indexes);
            data.PetNextLevelExperience = packet.ReadUInt32("PetNextLevelExperience", indexes);
            data.ModCastingSpeed = packet.ReadSingle("ModCastingSpeed", indexes);
            data.ModSpellHaste = packet.ReadSingle("ModSpellHaste", indexes);
            data.ModHaste = packet.ReadSingle("ModHaste", indexes);
            data.ModRangedHaste = packet.ReadSingle("ModRangedHaste", indexes);
            data.ModHasteRegen = packet.ReadSingle("ModHasteRegen", indexes);
            data.ModTimeRate = packet.ReadSingle("ModTimeRate", indexes);
            data.CreatedBySpell = packet.ReadInt32("CreatedBySpell", indexes);
            data.EmoteState = packet.ReadInt32("EmoteState", indexes);
            if ((flags & UpdateFieldFlag.Owner) != UpdateFieldFlag.None)
            {
                for (var i = 0; i < 4; ++i)
                {
                    data.Stats[i] = packet.ReadInt32("Stats", indexes, i);
                    data.StatPosBuff[i] = packet.ReadInt32("StatPosBuff", indexes, i);
                    data.StatNegBuff[i] = packet.ReadInt32("StatNegBuff", indexes, i);
                }
            }
            if ((flags & (UpdateFieldFlag.Owner | UpdateFieldFlag.Empath)) != UpdateFieldFlag.None)
            {
                for (var i = 0; i < 7; ++i)
                {
                    data.Resistances[i] = packet.ReadInt32("Resistances", indexes, i);
                }
            }
            if ((flags & UpdateFieldFlag.Owner) != UpdateFieldFlag.None)
            {
                for (var i = 0; i < 7; ++i)
                {
                    data.BonusResistanceMods[i] = packet.ReadInt32("BonusResistanceMods", indexes, i);
                    data.PowerCostModifier[i] = packet.ReadInt32("PowerCostModifier", indexes, i);
                    data.PowerCostMultiplier[i] = packet.ReadSingle("PowerCostMultiplier", indexes, i);
                }
            }
            data.BaseMana = packet.ReadInt32("BaseMana", indexes);
            if ((flags & UpdateFieldFlag.Owner) != UpdateFieldFlag.None)
            {
                data.BaseHealth = packet.ReadInt32("BaseHealth", indexes);
            }
            data.SheatheState = packet.ReadByte("SheatheState", indexes);
            data.PvpFlags = packet.ReadByte("PvpFlags", indexes);
            data.PetFlags = packet.ReadByte("PetFlags", indexes);
            data.ShapeshiftForm = packet.ReadByte("ShapeshiftForm", indexes);
            if ((flags & UpdateFieldFlag.Owner) != UpdateFieldFlag.None)
            {
                data.AttackPower = packet.ReadInt32("AttackPower", indexes);
                data.AttackPowerModPos = packet.ReadInt32("AttackPowerModPos", indexes);
                data.AttackPowerModNeg = packet.ReadInt32("AttackPowerModNeg", indexes);
                data.AttackPowerMultiplier = packet.ReadSingle("AttackPowerMultiplier", indexes);
                data.RangedAttackPower = packet.ReadInt32("RangedAttackPower", indexes);
                data.RangedAttackPowerModPos = packet.ReadInt32("RangedAttackPowerModPos", indexes);
                data.RangedAttackPowerModNeg = packet.ReadInt32("RangedAttackPowerModNeg", indexes);
                data.RangedAttackPowerMultiplier = packet.ReadSingle("RangedAttackPowerMultiplier", indexes);
                data.MainHandWeaponAttackPower = packet.ReadInt32("MainHandWeaponAttackPower", indexes);
                data.OffHandWeaponAttackPower = packet.ReadInt32("OffHandWeaponAttackPower", indexes);
                data.RangedWeaponAttackPower = packet.ReadInt32("RangedWeaponAttackPower", indexes);
                data.SetAttackSpeedAura = packet.ReadInt32("SetAttackSpeedAura", indexes);
                data.Lifesteal = packet.ReadSingle("Lifesteal", indexes);
                data.MinRangedDamage = packet.ReadSingle("MinRangedDamage", indexes);
                data.MaxRangedDamage = packet.ReadSingle("MaxRangedDamage", indexes);
                data.ManaCostModifierModifier = packet.ReadSingle("ManaCostModifierModifier", indexes);
                data.MaxHealthModifier = packet.ReadSingle("MaxHealthModifier", indexes);
            }
            data.HoverHeight = packet.ReadSingle("HoverHeight", indexes);
            data.MinItemLevelCutoff = packet.ReadInt32("MinItemLevelCutoff", indexes);
            data.MinItemLevel = packet.ReadInt32("MinItemLevel", indexes);
            data.MaxItemLevel = packet.ReadInt32("MaxItemLevel", indexes);
            data.AzeriteItemLevel = packet.ReadInt32("AzeriteItemLevel", indexes);
            data.WildBattlePetLevel = packet.ReadInt32("WildBattlePetLevel", indexes);
            data.BattlePetCompanionNameTimestamp = packet.ReadUInt32("BattlePetCompanionNameTimestamp", indexes);
            data.InteractSpellID = packet.ReadInt32("InteractSpellID", indexes);
            data.ScaleDuration = packet.ReadInt32("ScaleDuration", indexes);
            data.SpellOverrideNameID = packet.ReadInt32("SpellOverrideNameID", indexes);
            data.LooksLikeMountID = packet.ReadInt32("LooksLikeMountID", indexes);
            data.LooksLikeCreatureID = packet.ReadInt32("LooksLikeCreatureID", indexes);
            data.LookAtControllerID = packet.ReadInt32("LookAtControllerID", indexes);
            data.TaxiNodesID = packet.ReadInt32("TaxiNodesID", indexes);
            data.GuildGUID = packet.ReadPackedGuid128("GuildGUID", indexes);
            data.PassiveSpells.Resize(packet.ReadUInt32());
            data.WorldEffects.Resize(packet.ReadUInt32());
            data.ChannelObjects.Resize(packet.ReadUInt32());
            for (var i = 0; i < data.PassiveSpells.Count; ++i)
            {
                data.PassiveSpells[i] = ReadCreatePassiveSpellHistory(packet, indexes, "PassiveSpells", i);
            }
            for (var i = 0; i < data.WorldEffects.Count; ++i)
            {
                data.WorldEffects[i] = packet.ReadInt32("WorldEffects", indexes, i);
            }
            for (var i = 0; i < data.ChannelObjects.Count; ++i)
            {
                data.ChannelObjects[i] = packet.ReadPackedGuid128("ChannelObjects", indexes, i);
            }
            return data;
        }

        public override IUnitData ReadUpdateUnitData(Packet packet, params object[] indexes)
        {
            var data = new UnitData();
            var rawChangesMask = new int[6];
            var rawMaskMask = new int[1];
            rawMaskMask[0] = (int)packet.ReadBits(6);
            var maskMask = new BitArray(rawMaskMask);
            for (var i = 0; i < 6; ++i)
                if (maskMask[i])
                    rawChangesMask[i] = (int)packet.ReadBits(32);
            var changesMask = new BitArray(rawChangesMask);

            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.StateWorldEffectIDs = Enumerable.Range(0, (int)packet.ReadBits(32)).Select(x => new uint()).Cast<System.Nullable<uint>>().ToArray();
                    for (var i = 0; i < data.StateWorldEffectIDs.Length; ++i)
                    {
                        data.StateWorldEffectIDs[i] = packet.ReadUInt32("StateWorldEffectIDs", indexes, i);
                    }
                }
            }
            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[2])
                {
                    data.PassiveSpells.ReadUpdateMask(packet);
                }
                if (changesMask[3])
                {
                    data.WorldEffects.ReadUpdateMask(packet);
                }
                if (changesMask[4])
                {
                    data.ChannelObjects.ReadUpdateMask(packet);
                }
            }
            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[2])
                {
                    for (var i = 0; i < data.PassiveSpells.Count; ++i)
                    {
                        if (data.PassiveSpells.UpdateMask[i])
                        {
                            data.PassiveSpells[i] = ReadUpdatePassiveSpellHistory(packet, data.PassiveSpells[i] as PassiveSpellHistory, indexes, "PassiveSpells", i);
                        }
                    }
                }
                if (changesMask[3])
                {
                    for (var i = 0; i < data.WorldEffects.Count; ++i)
                    {
                        if (data.WorldEffects.UpdateMask[i])
                        {
                            data.WorldEffects[i] = packet.ReadInt32("WorldEffects", indexes, i);
                        }
                    }
                }
                if (changesMask[4])
                {
                    for (var i = 0; i < data.ChannelObjects.Count; ++i)
                    {
                        if (data.ChannelObjects.UpdateMask[i])
                        {
                            data.ChannelObjects[i] = packet.ReadPackedGuid128("ChannelObjects", indexes, i);
                        }
                    }
                }
                if (changesMask[5])
                {
                    data.DisplayID = packet.ReadInt32("DisplayID", indexes);
                }
                if (changesMask[6])
                {
                    data.StateSpellVisualID = packet.ReadUInt32("StateSpellVisualID", indexes);
                }
                if (changesMask[7])
                {
                    data.StateAnimID = packet.ReadUInt32("StateAnimID", indexes);
                }
                if (changesMask[8])
                {
                    data.StateAnimKitID = packet.ReadUInt32("StateAnimKitID", indexes);
                }
                if (changesMask[9])
                {
                    data.StateWorldEffectsQuestObjectiveID = packet.ReadUInt32("StateWorldEffectsQuestObjectiveID", indexes);
                }
                if (changesMask[10])
                {
                    data.Charm = packet.ReadPackedGuid128("Charm", indexes);
                }
                if (changesMask[11])
                {
                    data.Summon = packet.ReadPackedGuid128("Summon", indexes);
                }
                if (changesMask[12])
                {
                    data.Critter = packet.ReadPackedGuid128("Critter", indexes);
                }
                if (changesMask[13])
                {
                    data.CharmedBy = packet.ReadPackedGuid128("CharmedBy", indexes);
                }
                if (changesMask[14])
                {
                    data.SummonedBy = packet.ReadPackedGuid128("SummonedBy", indexes);
                }
                if (changesMask[15])
                {
                    data.CreatedBy = packet.ReadPackedGuid128("CreatedBy", indexes);
                }
                if (changesMask[16])
                {
                    data.DemonCreator = packet.ReadPackedGuid128("DemonCreator", indexes);
                }
                if (changesMask[17])
                {
                    data.LookAtControllerTarget = packet.ReadPackedGuid128("LookAtControllerTarget", indexes);
                }
                if (changesMask[18])
                {
                    data.Target = packet.ReadPackedGuid128("Target", indexes);
                }
                if (changesMask[19])
                {
                    data.BattlePetCompanionGUID = packet.ReadPackedGuid128("BattlePetCompanionGUID", indexes);
                }
                if (changesMask[20])
                {
                    data.BattlePetDBID = packet.ReadUInt64("BattlePetDBID", indexes);
                }
                if (changesMask[21])
                {
                    data.ChannelData = ReadUpdateUnitChannel(packet, data.ChannelData as UnitChannel, indexes, "ChannelData");
                }
                if (changesMask[22])
                {
                    data.SummonedByHomeRealm = packet.ReadUInt32("SummonedByHomeRealm", indexes);
                }
                if (changesMask[23])
                {
                    data.Race = packet.ReadByte("Race", indexes);
                }
                if (changesMask[24])
                {
                    data.ClassId = packet.ReadByte("ClassId", indexes);
                }
                if (changesMask[25])
                {
                    data.PlayerClassId = packet.ReadByte("PlayerClassId", indexes);
                }
                if (changesMask[26])
                {
                    data.Sex = packet.ReadByte("Sex", indexes);
                }
                if (changesMask[27])
                {
                    data.DisplayPower = packet.ReadByte("DisplayPower", indexes);
                }
                if (changesMask[28])
                {
                    data.OverrideDisplayPowerID = packet.ReadUInt32("OverrideDisplayPowerID", indexes);
                }
                if (changesMask[29])
                {
                    data.Health = packet.ReadInt64("Health", indexes);
                }
                if (changesMask[30])
                {
                    data.MaxHealth = packet.ReadInt64("MaxHealth", indexes);
                }
                if (changesMask[31])
                {
                    data.Level = packet.ReadInt32("Level", indexes);
                }
            }
            if (changesMask[32])
            {
                if (changesMask[33])
                {
                    data.EffectiveLevel = packet.ReadInt32("EffectiveLevel", indexes);
                }
                if (changesMask[34])
                {
                    data.ContentTuningID = packet.ReadInt32("ContentTuningID", indexes);
                }
                if (changesMask[35])
                {
                    data.ScalingLevelMin = packet.ReadInt32("ScalingLevelMin", indexes);
                }
                if (changesMask[36])
                {
                    data.ScalingLevelMax = packet.ReadInt32("ScalingLevelMax", indexes);
                }
                if (changesMask[37])
                {
                    data.ScalingLevelDelta = packet.ReadInt32("ScalingLevelDelta", indexes);
                }
                if (changesMask[38])
                {
                    data.ScalingFactionGroup = packet.ReadInt32("ScalingFactionGroup", indexes);
                }
                if (changesMask[39])
                {
                    data.ScalingHealthItemLevelCurveID = packet.ReadInt32("ScalingHealthItemLevelCurveID", indexes);
                }
                if (changesMask[40])
                {
                    data.ScalingDamageItemLevelCurveID = packet.ReadInt32("ScalingDamageItemLevelCurveID", indexes);
                }
                if (changesMask[41])
                {
                    data.FactionTemplate = packet.ReadInt32("FactionTemplate", indexes);
                }
                if (changesMask[42])
                {
                    data.Flags = packet.ReadUInt32("Flags", indexes);
                }
                if (changesMask[43])
                {
                    data.Flags2 = packet.ReadUInt32("Flags2", indexes);
                }
                if (changesMask[44])
                {
                    data.Flags3 = packet.ReadUInt32("Flags3", indexes);
                }
                if (changesMask[45])
                {
                    data.AuraState = packet.ReadUInt32("AuraState", indexes);
                }
                if (changesMask[46])
                {
                    data.RangedAttackRoundBaseTime = packet.ReadUInt32("RangedAttackRoundBaseTime", indexes);
                }
                if (changesMask[47])
                {
                    data.BoundingRadius = packet.ReadSingle("BoundingRadius", indexes);
                }
                if (changesMask[48])
                {
                    data.CombatReach = packet.ReadSingle("CombatReach", indexes);
                }
                if (changesMask[49])
                {
                    data.DisplayScale = packet.ReadSingle("DisplayScale", indexes);
                }
                if (changesMask[50])
                {
                    data.NativeDisplayID = packet.ReadInt32("NativeDisplayID", indexes);
                }
                if (changesMask[51])
                {
                    data.NativeXDisplayScale = packet.ReadSingle("NativeXDisplayScale", indexes);
                }
                if (changesMask[52])
                {
                    data.MountDisplayID = packet.ReadInt32("MountDisplayID", indexes);
                }
                if (changesMask[53])
                {
                    data.CosmeticMountDisplayID = packet.ReadInt32("CosmeticMountDisplayID", indexes);
                }
                if (changesMask[54])
                {
                    data.MinDamage = packet.ReadSingle("MinDamage", indexes);
                }
                if (changesMask[55])
                {
                    data.MaxDamage = packet.ReadSingle("MaxDamage", indexes);
                }
                if (changesMask[56])
                {
                    data.MinOffHandDamage = packet.ReadSingle("MinOffHandDamage", indexes);
                }
                if (changesMask[57])
                {
                    data.MaxOffHandDamage = packet.ReadSingle("MaxOffHandDamage", indexes);
                }
                if (changesMask[58])
                {
                    data.StandState = packet.ReadByte("StandState", indexes);
                }
                if (changesMask[59])
                {
                    data.PetTalentPoints = packet.ReadByte("PetTalentPoints", indexes);
                }
                if (changesMask[60])
                {
                    data.VisFlags = packet.ReadByte("VisFlags", indexes);
                }
                if (changesMask[61])
                {
                    data.AnimTier = packet.ReadByte("AnimTier", indexes);
                }
                if (changesMask[62])
                {
                    data.PetNumber = packet.ReadUInt32("PetNumber", indexes);
                }
                if (changesMask[63])
                {
                    data.PetNameTimestamp = packet.ReadUInt32("PetNameTimestamp", indexes);
                }
            }
            if (changesMask[64])
            {
                if (changesMask[65])
                {
                    data.PetExperience = packet.ReadUInt32("PetExperience", indexes);
                }
                if (changesMask[66])
                {
                    data.PetNextLevelExperience = packet.ReadUInt32("PetNextLevelExperience", indexes);
                }
                if (changesMask[67])
                {
                    data.ModCastingSpeed = packet.ReadSingle("ModCastingSpeed", indexes);
                }
                if (changesMask[68])
                {
                    data.ModSpellHaste = packet.ReadSingle("ModSpellHaste", indexes);
                }
                if (changesMask[69])
                {
                    data.ModHaste = packet.ReadSingle("ModHaste", indexes);
                }
                if (changesMask[70])
                {
                    data.ModRangedHaste = packet.ReadSingle("ModRangedHaste", indexes);
                }
                if (changesMask[71])
                {
                    data.ModHasteRegen = packet.ReadSingle("ModHasteRegen", indexes);
                }
                if (changesMask[72])
                {
                    data.ModTimeRate = packet.ReadSingle("ModTimeRate", indexes);
                }
                if (changesMask[73])
                {
                    data.CreatedBySpell = packet.ReadInt32("CreatedBySpell", indexes);
                }
                if (changesMask[74])
                {
                    data.EmoteState = packet.ReadInt32("EmoteState", indexes);
                }
                if (changesMask[75])
                {
                    data.BaseMana = packet.ReadInt32("BaseMana", indexes);
                }
                if (changesMask[76])
                {
                    data.BaseHealth = packet.ReadInt32("BaseHealth", indexes);
                }
                if (changesMask[77])
                {
                    data.SheatheState = packet.ReadByte("SheatheState", indexes);
                }
                if (changesMask[78])
                {
                    data.PvpFlags = packet.ReadByte("PvpFlags", indexes);
                }
                if (changesMask[79])
                {
                    data.PetFlags = packet.ReadByte("PetFlags", indexes);
                }
                if (changesMask[80])
                {
                    data.ShapeshiftForm = packet.ReadByte("ShapeshiftForm", indexes);
                }
                if (changesMask[81])
                {
                    data.AttackPower = packet.ReadInt32("AttackPower", indexes);
                }
                if (changesMask[82])
                {
                    data.AttackPowerModPos = packet.ReadInt32("AttackPowerModPos", indexes);
                }
                if (changesMask[83])
                {
                    data.AttackPowerModNeg = packet.ReadInt32("AttackPowerModNeg", indexes);
                }
                if (changesMask[84])
                {
                    data.AttackPowerMultiplier = packet.ReadSingle("AttackPowerMultiplier", indexes);
                }
                if (changesMask[85])
                {
                    data.RangedAttackPower = packet.ReadInt32("RangedAttackPower", indexes);
                }
                if (changesMask[86])
                {
                    data.RangedAttackPowerModPos = packet.ReadInt32("RangedAttackPowerModPos", indexes);
                }
                if (changesMask[87])
                {
                    data.RangedAttackPowerModNeg = packet.ReadInt32("RangedAttackPowerModNeg", indexes);
                }
                if (changesMask[88])
                {
                    data.RangedAttackPowerMultiplier = packet.ReadSingle("RangedAttackPowerMultiplier", indexes);
                }
                if (changesMask[89])
                {
                    data.MainHandWeaponAttackPower = packet.ReadInt32("MainHandWeaponAttackPower", indexes);
                }
                if (changesMask[90])
                {
                    data.OffHandWeaponAttackPower = packet.ReadInt32("OffHandWeaponAttackPower", indexes);
                }
                if (changesMask[91])
                {
                    data.RangedWeaponAttackPower = packet.ReadInt32("RangedWeaponAttackPower", indexes);
                }
                if (changesMask[92])
                {
                    data.SetAttackSpeedAura = packet.ReadInt32("SetAttackSpeedAura", indexes);
                }
                if (changesMask[93])
                {
                    data.Lifesteal = packet.ReadSingle("Lifesteal", indexes);
                }
                if (changesMask[94])
                {
                    data.MinRangedDamage = packet.ReadSingle("MinRangedDamage", indexes);
                }
                if (changesMask[95])
                {
                    data.MaxRangedDamage = packet.ReadSingle("MaxRangedDamage", indexes);
                }
            }
            if (changesMask[96])
            {
                if (changesMask[97])
                {
                    data.ManaCostModifierModifier = packet.ReadSingle("ManaCostModifierModifier", indexes);
                }
                if (changesMask[98])
                {
                    data.MaxHealthModifier = packet.ReadSingle("MaxHealthModifier", indexes);
                }
                if (changesMask[99])
                {
                    data.HoverHeight = packet.ReadSingle("HoverHeight", indexes);
                }
                if (changesMask[100])
                {
                    data.MinItemLevelCutoff = packet.ReadInt32("MinItemLevelCutoff", indexes);
                }
                if (changesMask[101])
                {
                    data.MinItemLevel = packet.ReadInt32("MinItemLevel", indexes);
                }
                if (changesMask[102])
                {
                    data.MaxItemLevel = packet.ReadInt32("MaxItemLevel", indexes);
                }
                if (changesMask[103])
                {
                    data.AzeriteItemLevel = packet.ReadInt32("AzeriteItemLevel", indexes);
                }
                if (changesMask[104])
                {
                    data.WildBattlePetLevel = packet.ReadInt32("WildBattlePetLevel", indexes);
                }
                if (changesMask[105])
                {
                    data.BattlePetCompanionNameTimestamp = packet.ReadUInt32("BattlePetCompanionNameTimestamp", indexes);
                }
                if (changesMask[106])
                {
                    data.InteractSpellID = packet.ReadInt32("InteractSpellID", indexes);
                }
                if (changesMask[107])
                {
                    data.ScaleDuration = packet.ReadInt32("ScaleDuration", indexes);
                }
                if (changesMask[108])
                {
                    data.SpellOverrideNameID = packet.ReadInt32("SpellOverrideNameID", indexes);
                }
                if (changesMask[109])
                {
                    data.LooksLikeMountID = packet.ReadInt32("LooksLikeMountID", indexes);
                }
                if (changesMask[110])
                {
                    data.LooksLikeCreatureID = packet.ReadInt32("LooksLikeCreatureID", indexes);
                }
                if (changesMask[111])
                {
                    data.LookAtControllerID = packet.ReadInt32("LookAtControllerID", indexes);
                }
                if (changesMask[112])
                {
                    data.TaxiNodesID = packet.ReadInt32("TaxiNodesID", indexes);
                }
                if (changesMask[113])
                {
                    data.GuildGUID = packet.ReadPackedGuid128("GuildGUID", indexes);
                }
            }
            if (changesMask[114])
            {
                for (var i = 0; i < 2; ++i)
                {
                    if (changesMask[115 + i])
                    {
                        data.NpcFlags[i] = packet.ReadUInt32("NpcFlags", indexes, i);
                    }
                }
            }
            if (changesMask[117])
            {
                for (var i = 0; i < 6; ++i)
                {
                    if (changesMask[118 + i])
                    {
                        data.Power[i] = packet.ReadInt32("Power", indexes, i);
                    }
                    if (changesMask[124 + i])
                    {
                        data.MaxPower[i] = packet.ReadInt32("MaxPower", indexes, i);
                    }
                    if (changesMask[130 + i])
                    {
                        data.PowerRegenFlatModifier[i] = packet.ReadSingle("PowerRegenFlatModifier", indexes, i);
                    }
                    if (changesMask[136 + i])
                    {
                        data.PowerRegenInterruptedFlatModifier[i] = packet.ReadSingle("PowerRegenInterruptedFlatModifier", indexes, i);
                    }
                }
            }
            if (changesMask[142])
            {
                for (var i = 0; i < 3; ++i)
                {
                    if (changesMask[143 + i])
                    {
                        data.VirtualItems[i] = ReadUpdateVisibleItem(packet, data.VirtualItems[i] as VisibleItem, indexes, "VirtualItems", i);
                    }
                }
            }
            if (changesMask[146])
            {
                for (var i = 0; i < 2; ++i)
                {
                    if (changesMask[147 + i])
                    {
                        data.AttackRoundBaseTime[i] = packet.ReadUInt32("AttackRoundBaseTime", indexes, i);
                    }
                }
            }
            if (changesMask[149])
            {
                for (var i = 0; i < 4; ++i)
                {
                    if (changesMask[150 + i])
                    {
                        data.Stats[i] = packet.ReadInt32("Stats", indexes, i);
                    }
                    if (changesMask[154 + i])
                    {
                        data.StatPosBuff[i] = packet.ReadInt32("StatPosBuff", indexes, i);
                    }
                    if (changesMask[158 + i])
                    {
                        data.StatNegBuff[i] = packet.ReadInt32("StatNegBuff", indexes, i);
                    }
                }
            }
            if (changesMask[162])
            {
                for (var i = 0; i < 7; ++i)
                {
                    if (changesMask[163 + i])
                    {
                        data.Resistances[i] = packet.ReadInt32("Resistances", indexes, i);
                    }
                    if (changesMask[170 + i])
                    {
                        data.BonusResistanceMods[i] = packet.ReadInt32("BonusResistanceMods", indexes, i);
                    }
                    if (changesMask[177 + i])
                    {
                        data.PowerCostModifier[i] = packet.ReadInt32("PowerCostModifier", indexes, i);
                    }
                    if (changesMask[184 + i])
                    {
                        data.PowerCostMultiplier[i] = packet.ReadSingle("PowerCostMultiplier", indexes, i);
                    }
                }
            }
            return data;
        }

        public static IQuestLog ReadCreateQuestLog(Packet packet, params object[] indexes)
        {
            var data = new QuestLog();
            data.QuestID = packet.ReadInt32("QuestID", indexes);
            data.StateFlags = packet.ReadUInt32("StateFlags", indexes);
            data.EndTime = packet.ReadUInt32("EndTime", indexes);
            data.AcceptTime = packet.ReadUInt32("AcceptTime", indexes);
            for (var i = 0; i < 24; ++i)
            {
                data.ObjectiveProgress[i] = packet.ReadInt16("ObjectiveProgress", indexes, i);
            }
            return data;
        }

        public static IQuestLog ReadUpdateQuestLog(Packet packet, IQuestLog existingData, params object[] indexes)
        {
            var data = existingData as QuestLog;
            if (data == null)
                data = new QuestLog();
            var rawChangesMask = new int[1];
            var rawMaskMask = new int[1];
            rawMaskMask[0] = (int)packet.ReadBits(1);
            var maskMask = new BitArray(rawMaskMask);
            if (maskMask[0])
                rawChangesMask[0] = (int)packet.ReadBits(32);
            var changesMask = new BitArray(rawChangesMask);

            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.QuestID = packet.ReadInt32("QuestID", indexes);
                }
                if (changesMask[2])
                {
                    data.StateFlags = packet.ReadUInt32("StateFlags", indexes);
                }
                if (changesMask[3])
                {
                    data.EndTime = packet.ReadUInt32("EndTime", indexes);
                }
                if (changesMask[4])
                {
                    data.AcceptTime = packet.ReadUInt32("AcceptTime", indexes);
                }
            }
            if (changesMask[5])
            {
                for (var i = 0; i < 24; ++i)
                {
                    if (changesMask[6 + i])
                    {
                        data.ObjectiveProgress[i] = packet.ReadInt16("ObjectiveProgress", indexes, i);
                    }
                }
            }
            return data;
        }

        public static IArenaCooldown ReadCreateArenaCooldown(Packet packet, params object[] indexes)
        {
            var data = new ArenaCooldown();
            data.SpellID = packet.ReadInt32("SpellID", indexes);
            data.Charges = packet.ReadInt32("Charges", indexes);
            data.Flags = packet.ReadUInt32("Flags", indexes);
            data.StartTime = packet.ReadUInt32("StartTime", indexes);
            data.EndTime = packet.ReadUInt32("EndTime", indexes);
            data.NextChargeTime = packet.ReadUInt32("NextChargeTime", indexes);
            data.MaxCharges = packet.ReadByte("MaxCharges", indexes);
            return data;
        }

        public static IArenaCooldown ReadUpdateArenaCooldown(Packet packet, IArenaCooldown existingData, params object[] indexes)
        {
            var data = existingData as ArenaCooldown;
            if (data == null)
                data = new ArenaCooldown();
            var rawChangesMask = new int[1];
            rawChangesMask[0] = (int)packet.ReadBits(8);
            var changesMask = new BitArray(rawChangesMask);

            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.SpellID = packet.ReadInt32("SpellID", indexes);
                }
                if (changesMask[2])
                {
                    data.Charges = packet.ReadInt32("Charges", indexes);
                }
                if (changesMask[3])
                {
                    data.Flags = packet.ReadUInt32("Flags", indexes);
                }
                if (changesMask[4])
                {
                    data.StartTime = packet.ReadUInt32("StartTime", indexes);
                }
                if (changesMask[5])
                {
                    data.EndTime = packet.ReadUInt32("EndTime", indexes);
                }
                if (changesMask[6])
                {
                    data.NextChargeTime = packet.ReadUInt32("NextChargeTime", indexes);
                }
                if (changesMask[7])
                {
                    data.MaxCharges = packet.ReadByte("MaxCharges", indexes);
                }
            }
            return data;
        }

        public override IPlayerData ReadCreatePlayerData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            var data = new PlayerData();
            data.DuelArbiter = packet.ReadPackedGuid128("DuelArbiter", indexes);
            data.WowAccount = packet.ReadPackedGuid128("WowAccount", indexes);
            data.LootTargetGUID = packet.ReadPackedGuid128("LootTargetGUID", indexes);
            data.PlayerFlags = packet.ReadUInt32("PlayerFlags", indexes);
            data.PlayerFlagsEx = packet.ReadUInt32("PlayerFlagsEx", indexes);
            data.GuildRankID = packet.ReadUInt32("GuildRankID", indexes);
            data.GuildDeleteDate = packet.ReadUInt32("GuildDeleteDate", indexes);
            data.GuildLevel = packet.ReadInt32("GuildLevel", indexes);
            data.SkinID = packet.ReadByte("SkinID", indexes);
            data.FaceID = packet.ReadByte("FaceID", indexes);
            data.HairStyleID = packet.ReadByte("HairStyleID", indexes);
            data.HairColorID = packet.ReadByte("HairColorID", indexes);
            for (var i = 0; i < 3; ++i)
            {
                data.CustomDisplayOption[i] = packet.ReadByte("CustomDisplayOption", indexes, i);
            }
            data.FacialHairStyleID = packet.ReadByte("FacialHairStyleID", indexes);
            data.PartyType = packet.ReadByte("PartyType", indexes);
            data.NativeSex = packet.ReadByte("NativeSex", indexes);
            data.Inebriation = packet.ReadByte("Inebriation", indexes);
            data.PvpTitle = packet.ReadByte("PvpTitle", indexes);
            data.ArenaFaction = packet.ReadByte("ArenaFaction", indexes);
            data.DuelTeam = packet.ReadUInt32("DuelTeam", indexes);
            data.GuildTimeStamp = packet.ReadInt32("GuildTimeStamp", indexes);
            if ((flags & UpdateFieldFlag.PartyMember) != UpdateFieldFlag.None)
            {
                for (var i = 0; i < 100; ++i)
                {
                    data.QuestLog[i] = ReadCreateQuestLog(packet, indexes, "QuestLog", i);
                }
            }
            for (var i = 0; i < 19; ++i)
            {
                data.VisibleItems[i] = ReadCreateVisibleItem(packet, indexes, "VisibleItems", i);
            }
            data.PlayerTitle = packet.ReadInt32("PlayerTitle", indexes);
            data.FakeInebriation = packet.ReadInt32("FakeInebriation", indexes);
            data.VirtualPlayerRealm = packet.ReadUInt32("VirtualPlayerRealm", indexes);
            data.CurrentSpecID = packet.ReadUInt32("CurrentSpecID", indexes);
            data.TaxiMountAnimKitID = packet.ReadInt32("TaxiMountAnimKitID", indexes);
            for (var i = 0; i < 4; ++i)
            {
                data.AvgItemLevel[i] = packet.ReadSingle("AvgItemLevel", indexes, i);
            }
            data.CurrentBattlePetBreedQuality = packet.ReadByte("CurrentBattlePetBreedQuality", indexes);
            data.HonorLevel = packet.ReadInt32("HonorLevel", indexes);
            data.ArenaCooldowns.Resize(packet.ReadUInt32());
            data.Field_B0 = packet.ReadInt32("Field_B0", indexes);
            data.Field_B4 = packet.ReadInt32("Field_B4", indexes);
            for (var i = 0; i < data.ArenaCooldowns.Count; ++i)
            {
                data.ArenaCooldowns[i] = ReadCreateArenaCooldown(packet, indexes, "ArenaCooldowns", i);
            }
            return data;
        }

        public override IPlayerData ReadUpdatePlayerData(Packet packet, params object[] indexes)
        {
            var data = new PlayerData();
            var rawChangesMask = new int[6];
            var rawMaskMask = new int[1];
            rawMaskMask[0] = (int)packet.ReadBits(6);
            var maskMask = new BitArray(rawMaskMask);
            for (var i = 0; i < 6; ++i)
                if (maskMask[i])
                    rawChangesMask[i] = (int)packet.ReadBits(32);
            var changesMask = new BitArray(rawChangesMask);

            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.ArenaCooldowns.ReadUpdateMask(packet);
                }
            }
            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    for (var i = 0; i < data.ArenaCooldowns.Count; ++i)
                    {
                        if (data.ArenaCooldowns.UpdateMask[i])
                        {
                            data.ArenaCooldowns[i] = ReadUpdateArenaCooldown(packet, data.ArenaCooldowns[i] as ArenaCooldown, indexes, "ArenaCooldowns", i);
                        }
                    }
                }
                if (changesMask[2])
                {
                    data.DuelArbiter = packet.ReadPackedGuid128("DuelArbiter", indexes);
                }
                if (changesMask[3])
                {
                    data.WowAccount = packet.ReadPackedGuid128("WowAccount", indexes);
                }
                if (changesMask[4])
                {
                    data.LootTargetGUID = packet.ReadPackedGuid128("LootTargetGUID", indexes);
                }
                if (changesMask[5])
                {
                    data.PlayerFlags = packet.ReadUInt32("PlayerFlags", indexes);
                }
                if (changesMask[6])
                {
                    data.PlayerFlagsEx = packet.ReadUInt32("PlayerFlagsEx", indexes);
                }
                if (changesMask[7])
                {
                    data.GuildRankID = packet.ReadUInt32("GuildRankID", indexes);
                }
                if (changesMask[8])
                {
                    data.GuildDeleteDate = packet.ReadUInt32("GuildDeleteDate", indexes);
                }
                if (changesMask[9])
                {
                    data.GuildLevel = packet.ReadInt32("GuildLevel", indexes);
                }
                if (changesMask[10])
                {
                    data.SkinID = packet.ReadByte("SkinID", indexes);
                }
                if (changesMask[11])
                {
                    data.FaceID = packet.ReadByte("FaceID", indexes);
                }
                if (changesMask[12])
                {
                    data.HairStyleID = packet.ReadByte("HairStyleID", indexes);
                }
                if (changesMask[13])
                {
                    data.HairColorID = packet.ReadByte("HairColorID", indexes);
                }
                if (changesMask[14])
                {
                    data.FacialHairStyleID = packet.ReadByte("FacialHairStyleID", indexes);
                }
                if (changesMask[15])
                {
                    data.PartyType = packet.ReadByte("PartyType", indexes);
                }
                if (changesMask[16])
                {
                    data.NativeSex = packet.ReadByte("NativeSex", indexes);
                }
                if (changesMask[17])
                {
                    data.Inebriation = packet.ReadByte("Inebriation", indexes);
                }
                if (changesMask[18])
                {
                    data.PvpTitle = packet.ReadByte("PvpTitle", indexes);
                }
                if (changesMask[19])
                {
                    data.ArenaFaction = packet.ReadByte("ArenaFaction", indexes);
                }
                if (changesMask[20])
                {
                    data.DuelTeam = packet.ReadUInt32("DuelTeam", indexes);
                }
                if (changesMask[21])
                {
                    data.GuildTimeStamp = packet.ReadInt32("GuildTimeStamp", indexes);
                }
                if (changesMask[22])
                {
                    data.PlayerTitle = packet.ReadInt32("PlayerTitle", indexes);
                }
                if (changesMask[23])
                {
                    data.FakeInebriation = packet.ReadInt32("FakeInebriation", indexes);
                }
                if (changesMask[24])
                {
                    data.VirtualPlayerRealm = packet.ReadUInt32("VirtualPlayerRealm", indexes);
                }
                if (changesMask[25])
                {
                    data.CurrentSpecID = packet.ReadUInt32("CurrentSpecID", indexes);
                }
                if (changesMask[26])
                {
                    data.TaxiMountAnimKitID = packet.ReadInt32("TaxiMountAnimKitID", indexes);
                }
                if (changesMask[27])
                {
                    data.CurrentBattlePetBreedQuality = packet.ReadByte("CurrentBattlePetBreedQuality", indexes);
                }
                if (changesMask[28])
                {
                    data.HonorLevel = packet.ReadInt32("HonorLevel", indexes);
                }
                if (changesMask[29])
                {
                    data.Field_B0 = packet.ReadInt32("Field_B0", indexes);
                }
                if (changesMask[30])
                {
                    data.Field_B4 = packet.ReadInt32("Field_B4", indexes);
                }
            }
            if (changesMask[31])
            {
                for (var i = 0; i < 3; ++i)
                {
                    if (changesMask[32 + i])
                    {
                        data.CustomDisplayOption[i] = packet.ReadByte("CustomDisplayOption", indexes, i);
                    }
                }
            }
            if (changesMask[35])
            {
                for (var i = 0; i < 100; ++i)
                {
                    if (changesMask[36 + i])
                    {
                        data.QuestLog[i] = ReadUpdateQuestLog(packet, data.QuestLog[i] as QuestLog, indexes, "QuestLog", i);
                    }
                }
            }
            if (changesMask[136])
            {
                for (var i = 0; i < 19; ++i)
                {
                    if (changesMask[137 + i])
                    {
                        data.VisibleItems[i] = ReadUpdateVisibleItem(packet, data.VisibleItems[i] as VisibleItem, indexes, "VisibleItems", i);
                    }
                }
            }
            if (changesMask[156])
            {
                for (var i = 0; i < 4; ++i)
                {
                    if (changesMask[157 + i])
                    {
                        data.AvgItemLevel[i] = packet.ReadSingle("AvgItemLevel", indexes, i);
                    }
                }
            }
            return data;
        }

        public static ISkillInfo ReadCreateSkillInfo(Packet packet, params object[] indexes)
        {
            var data = new SkillInfo();
            for (var i = 0; i < 256; ++i)
            {
                data.SkillLineID[i] = packet.ReadUInt16("SkillLineID", indexes, i);
                data.SkillStep[i] = packet.ReadUInt16("SkillStep", indexes, i);
                data.SkillRank[i] = packet.ReadUInt16("SkillRank", indexes, i);
                data.SkillStartingRank[i] = packet.ReadUInt16("SkillStartingRank", indexes, i);
                data.SkillMaxRank[i] = packet.ReadUInt16("SkillMaxRank", indexes, i);
                data.SkillTempBonus[i] = packet.ReadInt16("SkillTempBonus", indexes, i);
                data.SkillPermBonus[i] = packet.ReadUInt16("SkillPermBonus", indexes, i);
            }
            return data;
        }

        public static ISkillInfo ReadUpdateSkillInfo(Packet packet, ISkillInfo existingData, params object[] indexes)
        {
            var data = existingData as SkillInfo;
            if (data == null)
                data = new SkillInfo();
            var rawChangesMask = new int[57];
            var rawMaskMask = new int[2];
            for (var i = 0; i < 1; ++i)
                rawMaskMask[i] = packet.ReadInt32();
            rawMaskMask[1] = (int)packet.ReadBits(25);
            var maskMask = new BitArray(rawMaskMask);
            for (var i = 0; i < 57; ++i)
                if (maskMask[i])
                    rawChangesMask[i] = (int)packet.ReadBits(32);
            var changesMask = new BitArray(rawChangesMask);

            packet.ResetBitReader();
            if (changesMask[0])
            {
                for (var i = 0; i < 256; ++i)
                {
                    if (changesMask[1 + i])
                    {
                        data.SkillLineID[i] = packet.ReadUInt16("SkillLineID", indexes, i);
                    }
                    if (changesMask[257 + i])
                    {
                        data.SkillStep[i] = packet.ReadUInt16("SkillStep", indexes, i);
                    }
                    if (changesMask[513 + i])
                    {
                        data.SkillRank[i] = packet.ReadUInt16("SkillRank", indexes, i);
                    }
                    if (changesMask[769 + i])
                    {
                        data.SkillStartingRank[i] = packet.ReadUInt16("SkillStartingRank", indexes, i);
                    }
                    if (changesMask[1025 + i])
                    {
                        data.SkillMaxRank[i] = packet.ReadUInt16("SkillMaxRank", indexes, i);
                    }
                    if (changesMask[1281 + i])
                    {
                        data.SkillTempBonus[i] = packet.ReadInt16("SkillTempBonus", indexes, i);
                    }
                    if (changesMask[1537 + i])
                    {
                        data.SkillPermBonus[i] = packet.ReadUInt16("SkillPermBonus", indexes, i);
                    }
                }
            }
            return data;
        }

        public static IRestInfo ReadCreateRestInfo(Packet packet, params object[] indexes)
        {
            var data = new RestInfo();
            data.Threshold = packet.ReadUInt32("Threshold", indexes);
            data.StateID = packet.ReadByte("StateID", indexes);
            return data;
        }

        public static IRestInfo ReadUpdateRestInfo(Packet packet, IRestInfo existingData, params object[] indexes)
        {
            var data = existingData as RestInfo;
            if (data == null)
                data = new RestInfo();
            var rawChangesMask = new int[1];
            rawChangesMask[0] = (int)packet.ReadBits(3);
            var changesMask = new BitArray(rawChangesMask);

            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.Threshold = packet.ReadUInt32("Threshold", indexes);
                }
                if (changesMask[2])
                {
                    data.StateID = packet.ReadByte("StateID", indexes);
                }
            }
            return data;
        }

        public static IPVPInfo ReadCreatePVPInfo(Packet packet, params object[] indexes)
        {
            var data = new PVPInfo();
            packet.ResetBitReader();
            data.Field_0 = packet.ReadUInt32("Field_0", indexes);
            data.Field_4 = packet.ReadUInt32("Field_4", indexes);
            data.Field_8 = packet.ReadUInt32("Field_8", indexes);
            data.Field_C = packet.ReadUInt32("Field_C", indexes);
            data.Rating = packet.ReadUInt32("Rating", indexes);
            data.Field_14 = packet.ReadUInt32("Field_14", indexes);
            data.Field_18 = packet.ReadUInt32("Field_18", indexes);
            data.PvpTierID = packet.ReadUInt32("PvpTierID", indexes);
            data.Field_20 = packet.ReadBit("Field_20", indexes);
            return data;
        }

        public static IPVPInfo ReadUpdatePVPInfo(Packet packet, IPVPInfo existingData, params object[] indexes)
        {
            var data = existingData as PVPInfo;
            if (data == null)
                data = new PVPInfo();
            packet.ResetBitReader();
            var rawChangesMask = new int[1];
            rawChangesMask[0] = (int)packet.ReadBits(10);
            var changesMask = new BitArray(rawChangesMask);

            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.Field_20 = packet.ReadBit("Field_20", indexes);
                }
            }
            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[2])
                {
                    data.Field_0 = packet.ReadUInt32("Field_0", indexes);
                }
                if (changesMask[3])
                {
                    data.Field_4 = packet.ReadUInt32("Field_4", indexes);
                }
                if (changesMask[4])
                {
                    data.Field_8 = packet.ReadUInt32("Field_8", indexes);
                }
                if (changesMask[5])
                {
                    data.Field_C = packet.ReadUInt32("Field_C", indexes);
                }
                if (changesMask[6])
                {
                    data.Rating = packet.ReadUInt32("Rating", indexes);
                }
                if (changesMask[7])
                {
                    data.Field_14 = packet.ReadUInt32("Field_14", indexes);
                }
                if (changesMask[8])
                {
                    data.Field_18 = packet.ReadUInt32("Field_18", indexes);
                }
                if (changesMask[9])
                {
                    data.PvpTierID = packet.ReadUInt32("PvpTierID", indexes);
                }
            }
            return data;
        }

        public static ICharacterRestriction ReadCreateCharacterRestriction(Packet packet, params object[] indexes)
        {
            var data = new CharacterRestriction();
            packet.ResetBitReader();
            data.Field_0 = packet.ReadInt32("Field_0", indexes);
            data.Field_4 = packet.ReadInt32("Field_4", indexes);
            data.Field_8 = packet.ReadInt32("Field_8", indexes);
            data.Type = packet.ReadBits("Type", 5, indexes);
            return data;
        }

        public static ICharacterRestriction ReadUpdateCharacterRestriction(Packet packet, ICharacterRestriction existingData, params object[] indexes)
        {
            var data = existingData as CharacterRestriction;
            if (data == null)
                data = new CharacterRestriction();
            packet.ResetBitReader();
            data.Field_0 = packet.ReadInt32("Field_0", indexes);
            data.Field_4 = packet.ReadInt32("Field_4", indexes);
            data.Field_8 = packet.ReadInt32("Field_8", indexes);
            data.Type = packet.ReadBits("Type", 5, indexes);
            return data;
        }

        public static ISpellPctModByLabel ReadCreateSpellPctModByLabel(Packet packet, params object[] indexes)
        {
            var data = new SpellPctModByLabel();
            data.ModIndex = packet.ReadInt32("ModIndex", indexes);
            data.ModifierValue = packet.ReadSingle("ModifierValue", indexes);
            data.LabelID = packet.ReadInt32("LabelID", indexes);
            return data;
        }

        public static ISpellPctModByLabel ReadUpdateSpellPctModByLabel(Packet packet, ISpellPctModByLabel existingData, params object[] indexes)
        {
            var data = existingData as SpellPctModByLabel;
            if (data == null)
                data = new SpellPctModByLabel();
            data.ModIndex = packet.ReadInt32("ModIndex", indexes);
            data.ModifierValue = packet.ReadSingle("ModifierValue", indexes);
            data.LabelID = packet.ReadInt32("LabelID", indexes);
            return data;
        }

        public static ISpellFlatModByLabel ReadCreateSpellFlatModByLabel(Packet packet, params object[] indexes)
        {
            var data = new SpellFlatModByLabel();
            data.ModIndex = packet.ReadInt32("ModIndex", indexes);
            data.ModifierValue = packet.ReadInt32("ModifierValue", indexes);
            data.LabelID = packet.ReadInt32("LabelID", indexes);
            return data;
        }

        public static ISpellFlatModByLabel ReadUpdateSpellFlatModByLabel(Packet packet, ISpellFlatModByLabel existingData, params object[] indexes)
        {
            var data = existingData as SpellFlatModByLabel;
            if (data == null)
                data = new SpellFlatModByLabel();
            data.ModIndex = packet.ReadInt32("ModIndex", indexes);
            data.ModifierValue = packet.ReadInt32("ModifierValue", indexes);
            data.LabelID = packet.ReadInt32("LabelID", indexes);
            return data;
        }

        public static IResearch ReadCreateResearch(Packet packet, params object[] indexes)
        {
            var data = new Research();
            data.ResearchProjectID = packet.ReadInt16("ResearchProjectID", indexes);
            return data;
        }

        public static IResearch ReadUpdateResearch(Packet packet, IResearch existingData, params object[] indexes)
        {
            var data = existingData as Research;
            if (data == null)
                data = new Research();
            data.ResearchProjectID = packet.ReadInt16("ResearchProjectID", indexes);
            return data;
        }

        public override IActivePlayerData ReadCreateActivePlayerData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            var data = new ActivePlayerData();
            packet.ResetBitReader();
            for (var i = 0; i < 195; ++i)
            {
                data.InvSlots[i] = packet.ReadPackedGuid128("InvSlots", indexes, i);
            }
            data.FarsightObject = packet.ReadPackedGuid128("FarsightObject", indexes);
            data.SummonedBattlePetGUID = packet.ReadPackedGuid128("SummonedBattlePetGUID", indexes);
            data.KnownTitles.Resize(packet.ReadUInt32());
            data.Coinage = packet.ReadUInt64("Coinage", indexes);
            data.XP = packet.ReadInt32("XP", indexes);
            data.NextLevelXP = packet.ReadInt32("NextLevelXP", indexes);
            data.TrialXP = packet.ReadInt32("TrialXP", indexes);
            data.Skill = ReadCreateSkillInfo(packet, indexes, "Skill");
            data.CharacterPoints = packet.ReadInt32("CharacterPoints", indexes);
            data.MaxTalentTiers = packet.ReadInt32("MaxTalentTiers", indexes);
            data.TrackCreatureMask = packet.ReadInt32("TrackCreatureMask", indexes);
            for (var i = 0; i < 2; ++i)
            {
                data.TrackResourceMask[i] = packet.ReadUInt32("TrackResourceMask", indexes, i);
            }
            data.MainhandExpertise = packet.ReadSingle("MainhandExpertise", indexes);
            data.OffhandExpertise = packet.ReadSingle("OffhandExpertise", indexes);
            data.RangedExpertise = packet.ReadSingle("RangedExpertise", indexes);
            data.CombatRatingExpertise = packet.ReadSingle("CombatRatingExpertise", indexes);
            data.BlockPercentage = packet.ReadSingle("BlockPercentage", indexes);
            data.DodgePercentage = packet.ReadSingle("DodgePercentage", indexes);
            data.DodgePercentageFromAttribute = packet.ReadSingle("DodgePercentageFromAttribute", indexes);
            data.ParryPercentage = packet.ReadSingle("ParryPercentage", indexes);
            data.ParryPercentageFromAttribute = packet.ReadSingle("ParryPercentageFromAttribute", indexes);
            data.CritPercentage = packet.ReadSingle("CritPercentage", indexes);
            data.RangedCritPercentage = packet.ReadSingle("RangedCritPercentage", indexes);
            data.OffhandCritPercentage = packet.ReadSingle("OffhandCritPercentage", indexes);
            data.SpellCritPercentage = packet.ReadSingle("SpellCritPercentage", indexes);
            data.ShieldBlock = packet.ReadInt32("ShieldBlock", indexes);
            data.ShieldBlockCritPercentage = packet.ReadSingle("ShieldBlockCritPercentage", indexes);
            data.Mastery = packet.ReadSingle("Mastery", indexes);
            data.Speed = packet.ReadSingle("Speed", indexes);
            data.Avoidance = packet.ReadSingle("Avoidance", indexes);
            data.Sturdiness = packet.ReadSingle("Sturdiness", indexes);
            data.Versatility = packet.ReadInt32("Versatility", indexes);
            data.VersatilityBonus = packet.ReadSingle("VersatilityBonus", indexes);
            data.PvpPowerDamage = packet.ReadSingle("PvpPowerDamage", indexes);
            data.PvpPowerHealing = packet.ReadSingle("PvpPowerHealing", indexes);
            for (var i = 0; i < 192; ++i)
            {
                data.ExploredZones[i] = packet.ReadUInt64("ExploredZones", indexes, i);
            }
            for (var i = 0; i < 2; ++i)
            {
                data.RestInfo[i] = ReadCreateRestInfo(packet, indexes, "RestInfo", i);
            }
            for (var i = 0; i < 7; ++i)
            {
                data.ModDamageDonePos[i] = packet.ReadInt32("ModDamageDonePos", indexes, i);
                data.ModDamageDoneNeg[i] = packet.ReadInt32("ModDamageDoneNeg", indexes, i);
                data.ModDamageDonePercent[i] = packet.ReadSingle("ModDamageDonePercent", indexes, i);
            }
            data.ModHealingDonePos = packet.ReadInt32("ModHealingDonePos", indexes);
            data.ModHealingPercent = packet.ReadSingle("ModHealingPercent", indexes);
            data.ModHealingDonePercent = packet.ReadSingle("ModHealingDonePercent", indexes);
            data.ModPeriodicHealingDonePercent = packet.ReadSingle("ModPeriodicHealingDonePercent", indexes);
            for (var i = 0; i < 3; ++i)
            {
                data.WeaponDmgMultipliers[i] = packet.ReadSingle("WeaponDmgMultipliers", indexes, i);
                data.WeaponAtkSpeedMultipliers[i] = packet.ReadSingle("WeaponAtkSpeedMultipliers", indexes, i);
            }
            data.ModSpellPowerPercent = packet.ReadSingle("ModSpellPowerPercent", indexes);
            data.ModResiliencePercent = packet.ReadSingle("ModResiliencePercent", indexes);
            data.OverrideSpellPowerByAPPercent = packet.ReadSingle("OverrideSpellPowerByAPPercent", indexes);
            data.OverrideAPBySpellPowerPercent = packet.ReadSingle("OverrideAPBySpellPowerPercent", indexes);
            data.ModTargetResistance = packet.ReadInt32("ModTargetResistance", indexes);
            data.ModTargetPhysicalResistance = packet.ReadInt32("ModTargetPhysicalResistance", indexes);
            data.LocalFlags = packet.ReadInt32("LocalFlags", indexes);
            data.GrantableLevels = packet.ReadByte("GrantableLevels", indexes);
            data.MultiActionBars = packet.ReadByte("MultiActionBars", indexes);
            data.LifetimeMaxRank = packet.ReadByte("LifetimeMaxRank", indexes);
            data.NumRespecs = packet.ReadByte("NumRespecs", indexes);
            data.PvpMedals = packet.ReadUInt32("PvpMedals", indexes);
            for (var i = 0; i < 12; ++i)
            {
                data.BuybackPrice[i] = packet.ReadUInt32("BuybackPrice", indexes, i);
                data.BuybackTimestamp[i] = packet.ReadUInt32("BuybackTimestamp", indexes, i);
            }
            data.TodayHonorableKills = packet.ReadUInt16("TodayHonorableKills", indexes);
            data.YesterdayHonorableKills = packet.ReadUInt16("YesterdayHonorableKills", indexes);
            data.LifetimeHonorableKills = packet.ReadUInt32("LifetimeHonorableKills", indexes);
            data.WatchedFactionIndex = packet.ReadInt32("WatchedFactionIndex", indexes);
            for (var i = 0; i < 32; ++i)
            {
                data.CombatRatings[i] = packet.ReadInt32("CombatRatings", indexes, i);
            }
            data.MaxLevel = packet.ReadInt32("MaxLevel", indexes);
            data.ScalingPlayerLevelDelta = packet.ReadInt32("ScalingPlayerLevelDelta", indexes);
            data.MaxCreatureScalingLevel = packet.ReadInt32("MaxCreatureScalingLevel", indexes);
            for (var i = 0; i < 4; ++i)
            {
                data.NoReagentCostMask[i] = packet.ReadUInt32("NoReagentCostMask", indexes, i);
            }
            data.PetSpellPower = packet.ReadInt32("PetSpellPower", indexes);
            for (var i = 0; i < 2; ++i)
            {
                data.ProfessionSkillLine[i] = packet.ReadInt32("ProfessionSkillLine", indexes, i);
            }
            data.UiHitModifier = packet.ReadSingle("UiHitModifier", indexes);
            data.UiSpellHitModifier = packet.ReadSingle("UiSpellHitModifier", indexes);
            data.HomeRealmTimeOffset = packet.ReadInt32("HomeRealmTimeOffset", indexes);
            data.ModPetHaste = packet.ReadSingle("ModPetHaste", indexes);
            data.LocalRegenFlags = packet.ReadByte("LocalRegenFlags", indexes);
            data.AuraVision = packet.ReadByte("AuraVision", indexes);
            data.NumBackpackSlots = packet.ReadByte("NumBackpackSlots", indexes);
            data.OverrideSpellsID = packet.ReadInt32("OverrideSpellsID", indexes);
            data.LfgBonusFactionID = packet.ReadInt32("LfgBonusFactionID", indexes);
            data.LootSpecID = packet.ReadUInt16("LootSpecID", indexes);
            data.OverrideZonePVPType = packet.ReadUInt32("OverrideZonePVPType", indexes);
            for (var i = 0; i < 4; ++i)
            {
                data.BagSlotFlags[i] = packet.ReadUInt32("BagSlotFlags", indexes, i);
            }
            for (var i = 0; i < 7; ++i)
            {
                data.BankBagSlotFlags[i] = packet.ReadUInt32("BankBagSlotFlags", indexes, i);
            }
            for (var i = 0; i < 875; ++i)
            {
                data.QuestCompleted[i] = packet.ReadUInt64("QuestCompleted", indexes, i);
            }
            data.Honor = packet.ReadInt32("Honor", indexes);
            data.HonorNextLevel = packet.ReadInt32("HonorNextLevel", indexes);
            data.PvpRewardAchieved = packet.ReadInt32("PvpRewardAchieved", indexes);
            data.PvpTierMaxFromWins = packet.ReadInt32("PvpTierMaxFromWins", indexes);
            data.PvpLastWeeksRewardAchieved = packet.ReadInt32("PvpLastWeeksRewardAchieved", indexes);
            data.PvpLastWeeksTierMaxFromWins = packet.ReadInt32("PvpLastWeeksTierMaxFromWins", indexes);
            data.PvpLastWeeksRewardClaimed = packet.ReadInt32("PvpLastWeeksRewardClaimed", indexes);
            data.NumBankSlots = packet.ReadByte("NumBankSlots", indexes);
            data.ResearchSites.Resize(packet.ReadUInt32());
            data.ResearchSiteProgress.Resize(packet.ReadUInt32());
            data.DailyQuestsCompleted.Resize(packet.ReadUInt32());
            data.AvailableQuestLineXQuestIDs.Resize(packet.ReadUInt32());
            data.Heirlooms.Resize(packet.ReadUInt32());
            data.HeirloomFlags.Resize(packet.ReadUInt32());
            data.Toys.Resize(packet.ReadUInt32());
            data.ToyFlags.Resize(packet.ReadUInt32());
            data.Transmog.Resize(packet.ReadUInt32());
            data.ConditionalTransmog.Resize(packet.ReadUInt32());
            data.SelfResSpells.Resize(packet.ReadUInt32());
            data.CharacterRestrictions.Resize(packet.ReadUInt32());
            data.SpellPctModByLabel.Resize(packet.ReadUInt32());
            data.SpellFlatModByLabel.Resize(packet.ReadUInt32());
            for (var i = 0; i < 1; ++i)
            {
                data.Research[i].Resize(packet.ReadUInt32());
                for (var j = 0; j < data.Research[i].Count; ++j)
                {
                    data.Research[i][j] = ReadCreateResearch(packet, indexes, "Research", i, j);
                }
            }
            for (var i = 0; i < data.KnownTitles.Count; ++i)
            {
                data.KnownTitles[i] = packet.ReadUInt64("KnownTitles", indexes, i);
            }
            for (var i = 0; i < data.ResearchSites.Count; ++i)
            {
                data.ResearchSites[i] = packet.ReadUInt16("ResearchSites", indexes, i);
            }
            for (var i = 0; i < data.ResearchSiteProgress.Count; ++i)
            {
                data.ResearchSiteProgress[i] = packet.ReadUInt32("ResearchSiteProgress", indexes, i);
            }
            for (var i = 0; i < data.DailyQuestsCompleted.Count; ++i)
            {
                data.DailyQuestsCompleted[i] = packet.ReadInt32("DailyQuestsCompleted", indexes, i);
            }
            for (var i = 0; i < data.AvailableQuestLineXQuestIDs.Count; ++i)
            {
                data.AvailableQuestLineXQuestIDs[i] = packet.ReadInt32("AvailableQuestLineXQuestIDs", indexes, i);
            }
            for (var i = 0; i < data.Heirlooms.Count; ++i)
            {
                data.Heirlooms[i] = packet.ReadInt32("Heirlooms", indexes, i);
            }
            for (var i = 0; i < data.HeirloomFlags.Count; ++i)
            {
                data.HeirloomFlags[i] = packet.ReadUInt32("HeirloomFlags", indexes, i);
            }
            for (var i = 0; i < data.Toys.Count; ++i)
            {
                data.Toys[i] = packet.ReadInt32("Toys", indexes, i);
            }
            for (var i = 0; i < data.ToyFlags.Count; ++i)
            {
                data.ToyFlags[i] = packet.ReadUInt32("ToyFlags", indexes, i);
            }
            for (var i = 0; i < data.Transmog.Count; ++i)
            {
                data.Transmog[i] = packet.ReadUInt32("Transmog", indexes, i);
            }
            for (var i = 0; i < data.ConditionalTransmog.Count; ++i)
            {
                data.ConditionalTransmog[i] = packet.ReadInt32("ConditionalTransmog", indexes, i);
            }
            for (var i = 0; i < data.SelfResSpells.Count; ++i)
            {
                data.SelfResSpells[i] = packet.ReadInt32("SelfResSpells", indexes, i);
            }
            for (var i = 0; i < data.SpellPctModByLabel.Count; ++i)
            {
                data.SpellPctModByLabel[i] = ReadCreateSpellPctModByLabel(packet, indexes, "SpellPctModByLabel", i);
            }
            for (var i = 0; i < data.SpellFlatModByLabel.Count; ++i)
            {
                data.SpellFlatModByLabel[i] = ReadCreateSpellFlatModByLabel(packet, indexes, "SpellFlatModByLabel", i);
            }
            for (var i = 0; i < 6; ++i)
            {
                data.PvpInfo[i] = ReadCreatePVPInfo(packet, indexes, "PvpInfo", i);
            }
            data.BackpackAutoSortDisabled = packet.ReadBit("BackpackAutoSortDisabled", indexes);
            data.BankAutoSortDisabled = packet.ReadBit("BankAutoSortDisabled", indexes);
            data.SortBagsRightToLeft = packet.ReadBit("SortBagsRightToLeft", indexes);
            data.InsertItemsLeftToRight = packet.ReadBit("InsertItemsLeftToRight", indexes);
            for (var i = 0; i < data.CharacterRestrictions.Count; ++i)
            {
                data.CharacterRestrictions[i] = ReadCreateCharacterRestriction(packet, indexes, "CharacterRestrictions", i);
            }
            return data;
        }

        public override IActivePlayerData ReadUpdateActivePlayerData(Packet packet, params object[] indexes)
        {
            var data = new ActivePlayerData();
            packet.ResetBitReader();
            var rawChangesMask = new int[47];
            var rawMaskMask = new int[2];
            for (var i = 0; i < 1; ++i)
                rawMaskMask[i] = packet.ReadInt32();
            rawMaskMask[1] = (int)packet.ReadBits(15);
            var maskMask = new BitArray(rawMaskMask);
            for (var i = 0; i < 47; ++i)
                if (maskMask[i])
                    rawChangesMask[i] = (int)packet.ReadBits(32);
            var changesMask = new BitArray(rawChangesMask);

            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.BackpackAutoSortDisabled = packet.ReadBit("BackpackAutoSortDisabled", indexes);
                }
                if (changesMask[2])
                {
                    data.BankAutoSortDisabled = packet.ReadBit("BankAutoSortDisabled", indexes);
                }
                if (changesMask[3])
                {
                    data.SortBagsRightToLeft = packet.ReadBit("SortBagsRightToLeft", indexes);
                }
                if (changesMask[4])
                {
                    data.InsertItemsLeftToRight = packet.ReadBit("InsertItemsLeftToRight", indexes);
                }
                if (changesMask[5])
                {
                    data.KnownTitles.ReadUpdateMask(packet);
                }
                if (changesMask[6])
                {
                    data.ResearchSites.ReadUpdateMask(packet);
                }
                if (changesMask[7])
                {
                    data.ResearchSiteProgress.ReadUpdateMask(packet);
                }
                if (changesMask[8])
                {
                    data.DailyQuestsCompleted.ReadUpdateMask(packet);
                }
                if (changesMask[9])
                {
                    data.AvailableQuestLineXQuestIDs.ReadUpdateMask(packet);
                }
                if (changesMask[10])
                {
                    data.Heirlooms.ReadUpdateMask(packet);
                }
                if (changesMask[11])
                {
                    data.HeirloomFlags.ReadUpdateMask(packet);
                }
                if (changesMask[12])
                {
                    data.Toys.ReadUpdateMask(packet);
                }
                if (changesMask[13])
                {
                    data.ToyFlags.ReadUpdateMask(packet);
                }
                if (changesMask[14])
                {
                    data.Transmog.ReadUpdateMask(packet);
                }
                if (changesMask[15])
                {
                    data.ConditionalTransmog.ReadUpdateMask(packet);
                }
                if (changesMask[16])
                {
                    data.SelfResSpells.ReadUpdateMask(packet);
                }
                if (changesMask[17])
                {
                    data.CharacterRestrictions.ReadUpdateMask(packet);
                }
                if (changesMask[18])
                {
                    data.SpellPctModByLabel.ReadUpdateMask(packet);
                }
                if (changesMask[19])
                {
                    data.SpellFlatModByLabel.ReadUpdateMask(packet);
                }
            }
            if (changesMask[20])
            {
                for (var i = 0; i < 1; ++i)
                {
                    if (changesMask[21 + i])
                    {
                        data.Research[i].ReadUpdateMask(packet);
                    }
                }
            }
            if (changesMask[20])
            {
                for (var i = 0; i < 1; ++i)
                {
                    if (changesMask[21 + i])
                    {
                        for (var j = 0; j < data.Research[i].Count; ++j)
                        {
                            if (data.Research[i].UpdateMask[j])
                            {
                                data.Research[i][j] = ReadUpdateResearch(packet, data.Research[i][j] as Research, indexes, "Research", i, j);
                            }
                        }
                    }
                }
            }
            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[5])
                {
                    for (var i = 0; i < data.KnownTitles.Count; ++i)
                    {
                        if (data.KnownTitles.UpdateMask[i])
                        {
                            data.KnownTitles[i] = packet.ReadUInt64("KnownTitles", indexes, i);
                        }
                    }
                }
                if (changesMask[6])
                {
                    for (var i = 0; i < data.ResearchSites.Count; ++i)
                    {
                        if (data.ResearchSites.UpdateMask[i])
                        {
                            data.ResearchSites[i] = packet.ReadUInt16("ResearchSites", indexes, i);
                        }
                    }
                }
                if (changesMask[7])
                {
                    for (var i = 0; i < data.ResearchSiteProgress.Count; ++i)
                    {
                        if (data.ResearchSiteProgress.UpdateMask[i])
                        {
                            data.ResearchSiteProgress[i] = packet.ReadUInt32("ResearchSiteProgress", indexes, i);
                        }
                    }
                }
                if (changesMask[8])
                {
                    for (var i = 0; i < data.DailyQuestsCompleted.Count; ++i)
                    {
                        if (data.DailyQuestsCompleted.UpdateMask[i])
                        {
                            data.DailyQuestsCompleted[i] = packet.ReadInt32("DailyQuestsCompleted", indexes, i);
                        }
                    }
                }
                if (changesMask[9])
                {
                    for (var i = 0; i < data.AvailableQuestLineXQuestIDs.Count; ++i)
                    {
                        if (data.AvailableQuestLineXQuestIDs.UpdateMask[i])
                        {
                            data.AvailableQuestLineXQuestIDs[i] = packet.ReadInt32("AvailableQuestLineXQuestIDs", indexes, i);
                        }
                    }
                }
                if (changesMask[10])
                {
                    for (var i = 0; i < data.Heirlooms.Count; ++i)
                    {
                        if (data.Heirlooms.UpdateMask[i])
                        {
                            data.Heirlooms[i] = packet.ReadInt32("Heirlooms", indexes, i);
                        }
                    }
                }
                if (changesMask[11])
                {
                    for (var i = 0; i < data.HeirloomFlags.Count; ++i)
                    {
                        if (data.HeirloomFlags.UpdateMask[i])
                        {
                            data.HeirloomFlags[i] = packet.ReadUInt32("HeirloomFlags", indexes, i);
                        }
                    }
                }
                if (changesMask[12])
                {
                    for (var i = 0; i < data.Toys.Count; ++i)
                    {
                        if (data.Toys.UpdateMask[i])
                        {
                            data.Toys[i] = packet.ReadInt32("Toys", indexes, i);
                        }
                    }
                }
                if (changesMask[13])
                {
                    for (var i = 0; i < data.ToyFlags.Count; ++i)
                    {
                        if (data.ToyFlags.UpdateMask[i])
                        {
                            data.ToyFlags[i] = packet.ReadUInt32("ToyFlags", indexes, i);
                        }
                    }
                }
                if (changesMask[14])
                {
                    for (var i = 0; i < data.Transmog.Count; ++i)
                    {
                        if (data.Transmog.UpdateMask[i])
                        {
                            data.Transmog[i] = packet.ReadUInt32("Transmog", indexes, i);
                        }
                    }
                }
                if (changesMask[15])
                {
                    for (var i = 0; i < data.ConditionalTransmog.Count; ++i)
                    {
                        if (data.ConditionalTransmog.UpdateMask[i])
                        {
                            data.ConditionalTransmog[i] = packet.ReadInt32("ConditionalTransmog", indexes, i);
                        }
                    }
                }
                if (changesMask[16])
                {
                    for (var i = 0; i < data.SelfResSpells.Count; ++i)
                    {
                        if (data.SelfResSpells.UpdateMask[i])
                        {
                            data.SelfResSpells[i] = packet.ReadInt32("SelfResSpells", indexes, i);
                        }
                    }
                }
                if (changesMask[18])
                {
                    for (var i = 0; i < data.SpellPctModByLabel.Count; ++i)
                    {
                        if (data.SpellPctModByLabel.UpdateMask[i])
                        {
                            data.SpellPctModByLabel[i] = ReadUpdateSpellPctModByLabel(packet, data.SpellPctModByLabel[i] as SpellPctModByLabel, indexes, "SpellPctModByLabel", i);
                        }
                    }
                }
                if (changesMask[19])
                {
                    for (var i = 0; i < data.SpellFlatModByLabel.Count; ++i)
                    {
                        if (data.SpellFlatModByLabel.UpdateMask[i])
                        {
                            data.SpellFlatModByLabel[i] = ReadUpdateSpellFlatModByLabel(packet, data.SpellFlatModByLabel[i] as SpellFlatModByLabel, indexes, "SpellFlatModByLabel", i);
                        }
                    }
                }
                if (changesMask[17])
                {
                    for (var i = 0; i < data.CharacterRestrictions.Count; ++i)
                    {
                        if (data.CharacterRestrictions.UpdateMask[i])
                        {
                            data.CharacterRestrictions[i] = ReadUpdateCharacterRestriction(packet, data.CharacterRestrictions[i] as CharacterRestriction, indexes, "CharacterRestrictions", i);
                        }
                    }
                }
                if (changesMask[22])
                {
                    data.FarsightObject = packet.ReadPackedGuid128("FarsightObject", indexes);
                }
                if (changesMask[23])
                {
                    data.SummonedBattlePetGUID = packet.ReadPackedGuid128("SummonedBattlePetGUID", indexes);
                }
                if (changesMask[24])
                {
                    data.Coinage = packet.ReadUInt64("Coinage", indexes);
                }
                if (changesMask[25])
                {
                    data.XP = packet.ReadInt32("XP", indexes);
                }
                if (changesMask[26])
                {
                    data.NextLevelXP = packet.ReadInt32("NextLevelXP", indexes);
                }
                if (changesMask[27])
                {
                    data.TrialXP = packet.ReadInt32("TrialXP", indexes);
                }
                if (changesMask[28])
                {
                    data.Skill = ReadUpdateSkillInfo(packet, data.Skill as SkillInfo, indexes, "Skill");
                }
                if (changesMask[29])
                {
                    data.CharacterPoints = packet.ReadInt32("CharacterPoints", indexes);
                }
                if (changesMask[30])
                {
                    data.MaxTalentTiers = packet.ReadInt32("MaxTalentTiers", indexes);
                }
                if (changesMask[31])
                {
                    data.TrackCreatureMask = packet.ReadInt32("TrackCreatureMask", indexes);
                }
                if (changesMask[32])
                {
                    data.MainhandExpertise = packet.ReadSingle("MainhandExpertise", indexes);
                }
                if (changesMask[33])
                {
                    data.OffhandExpertise = packet.ReadSingle("OffhandExpertise", indexes);
                }
            }
            if (changesMask[34])
            {
                if (changesMask[35])
                {
                    data.RangedExpertise = packet.ReadSingle("RangedExpertise", indexes);
                }
                if (changesMask[36])
                {
                    data.CombatRatingExpertise = packet.ReadSingle("CombatRatingExpertise", indexes);
                }
                if (changesMask[37])
                {
                    data.BlockPercentage = packet.ReadSingle("BlockPercentage", indexes);
                }
                if (changesMask[38])
                {
                    data.DodgePercentage = packet.ReadSingle("DodgePercentage", indexes);
                }
                if (changesMask[39])
                {
                    data.DodgePercentageFromAttribute = packet.ReadSingle("DodgePercentageFromAttribute", indexes);
                }
                if (changesMask[40])
                {
                    data.ParryPercentage = packet.ReadSingle("ParryPercentage", indexes);
                }
                if (changesMask[41])
                {
                    data.ParryPercentageFromAttribute = packet.ReadSingle("ParryPercentageFromAttribute", indexes);
                }
                if (changesMask[42])
                {
                    data.CritPercentage = packet.ReadSingle("CritPercentage", indexes);
                }
                if (changesMask[43])
                {
                    data.RangedCritPercentage = packet.ReadSingle("RangedCritPercentage", indexes);
                }
                if (changesMask[44])
                {
                    data.OffhandCritPercentage = packet.ReadSingle("OffhandCritPercentage", indexes);
                }
                if (changesMask[45])
                {
                    data.SpellCritPercentage = packet.ReadSingle("SpellCritPercentage", indexes);
                }
                if (changesMask[46])
                {
                    data.ShieldBlock = packet.ReadInt32("ShieldBlock", indexes);
                }
                if (changesMask[47])
                {
                    data.ShieldBlockCritPercentage = packet.ReadSingle("ShieldBlockCritPercentage", indexes);
                }
                if (changesMask[48])
                {
                    data.Mastery = packet.ReadSingle("Mastery", indexes);
                }
                if (changesMask[49])
                {
                    data.Speed = packet.ReadSingle("Speed", indexes);
                }
                if (changesMask[50])
                {
                    data.Avoidance = packet.ReadSingle("Avoidance", indexes);
                }
                if (changesMask[51])
                {
                    data.Sturdiness = packet.ReadSingle("Sturdiness", indexes);
                }
                if (changesMask[52])
                {
                    data.Versatility = packet.ReadInt32("Versatility", indexes);
                }
                if (changesMask[53])
                {
                    data.VersatilityBonus = packet.ReadSingle("VersatilityBonus", indexes);
                }
                if (changesMask[54])
                {
                    data.PvpPowerDamage = packet.ReadSingle("PvpPowerDamage", indexes);
                }
                if (changesMask[55])
                {
                    data.PvpPowerHealing = packet.ReadSingle("PvpPowerHealing", indexes);
                }
                if (changesMask[56])
                {
                    data.ModHealingDonePos = packet.ReadInt32("ModHealingDonePos", indexes);
                }
                if (changesMask[57])
                {
                    data.ModHealingPercent = packet.ReadSingle("ModHealingPercent", indexes);
                }
                if (changesMask[58])
                {
                    data.ModHealingDonePercent = packet.ReadSingle("ModHealingDonePercent", indexes);
                }
                if (changesMask[59])
                {
                    data.ModPeriodicHealingDonePercent = packet.ReadSingle("ModPeriodicHealingDonePercent", indexes);
                }
                if (changesMask[60])
                {
                    data.ModSpellPowerPercent = packet.ReadSingle("ModSpellPowerPercent", indexes);
                }
                if (changesMask[61])
                {
                    data.ModResiliencePercent = packet.ReadSingle("ModResiliencePercent", indexes);
                }
                if (changesMask[62])
                {
                    data.OverrideSpellPowerByAPPercent = packet.ReadSingle("OverrideSpellPowerByAPPercent", indexes);
                }
                if (changesMask[63])
                {
                    data.OverrideAPBySpellPowerPercent = packet.ReadSingle("OverrideAPBySpellPowerPercent", indexes);
                }
                if (changesMask[64])
                {
                    data.ModTargetResistance = packet.ReadInt32("ModTargetResistance", indexes);
                }
                if (changesMask[65])
                {
                    data.ModTargetPhysicalResistance = packet.ReadInt32("ModTargetPhysicalResistance", indexes);
                }
            }
            if (changesMask[66])
            {
                if (changesMask[67])
                {
                    data.LocalFlags = packet.ReadInt32("LocalFlags", indexes);
                }
                if (changesMask[68])
                {
                    data.GrantableLevels = packet.ReadByte("GrantableLevels", indexes);
                }
                if (changesMask[69])
                {
                    data.MultiActionBars = packet.ReadByte("MultiActionBars", indexes);
                }
                if (changesMask[70])
                {
                    data.LifetimeMaxRank = packet.ReadByte("LifetimeMaxRank", indexes);
                }
                if (changesMask[71])
                {
                    data.NumRespecs = packet.ReadByte("NumRespecs", indexes);
                }
                if (changesMask[72])
                {
                    data.PvpMedals = packet.ReadUInt32("PvpMedals", indexes);
                }
                if (changesMask[73])
                {
                    data.TodayHonorableKills = packet.ReadUInt16("TodayHonorableKills", indexes);
                }
                if (changesMask[74])
                {
                    data.YesterdayHonorableKills = packet.ReadUInt16("YesterdayHonorableKills", indexes);
                }
                if (changesMask[75])
                {
                    data.LifetimeHonorableKills = packet.ReadUInt32("LifetimeHonorableKills", indexes);
                }
                if (changesMask[76])
                {
                    data.WatchedFactionIndex = packet.ReadInt32("WatchedFactionIndex", indexes);
                }
                if (changesMask[77])
                {
                    data.MaxLevel = packet.ReadInt32("MaxLevel", indexes);
                }
                if (changesMask[78])
                {
                    data.ScalingPlayerLevelDelta = packet.ReadInt32("ScalingPlayerLevelDelta", indexes);
                }
                if (changesMask[79])
                {
                    data.MaxCreatureScalingLevel = packet.ReadInt32("MaxCreatureScalingLevel", indexes);
                }
                if (changesMask[80])
                {
                    data.PetSpellPower = packet.ReadInt32("PetSpellPower", indexes);
                }
                if (changesMask[81])
                {
                    data.UiHitModifier = packet.ReadSingle("UiHitModifier", indexes);
                }
                if (changesMask[82])
                {
                    data.UiSpellHitModifier = packet.ReadSingle("UiSpellHitModifier", indexes);
                }
                if (changesMask[83])
                {
                    data.HomeRealmTimeOffset = packet.ReadInt32("HomeRealmTimeOffset", indexes);
                }
                if (changesMask[84])
                {
                    data.ModPetHaste = packet.ReadSingle("ModPetHaste", indexes);
                }
                if (changesMask[85])
                {
                    data.LocalRegenFlags = packet.ReadByte("LocalRegenFlags", indexes);
                }
                if (changesMask[86])
                {
                    data.AuraVision = packet.ReadByte("AuraVision", indexes);
                }
                if (changesMask[87])
                {
                    data.NumBackpackSlots = packet.ReadByte("NumBackpackSlots", indexes);
                }
                if (changesMask[88])
                {
                    data.OverrideSpellsID = packet.ReadInt32("OverrideSpellsID", indexes);
                }
                if (changesMask[89])
                {
                    data.LfgBonusFactionID = packet.ReadInt32("LfgBonusFactionID", indexes);
                }
                if (changesMask[90])
                {
                    data.LootSpecID = packet.ReadUInt16("LootSpecID", indexes);
                }
                if (changesMask[91])
                {
                    data.OverrideZonePVPType = packet.ReadUInt32("OverrideZonePVPType", indexes);
                }
                if (changesMask[92])
                {
                    data.Honor = packet.ReadInt32("Honor", indexes);
                }
                if (changesMask[93])
                {
                    data.HonorNextLevel = packet.ReadInt32("HonorNextLevel", indexes);
                }
                if (changesMask[94])
                {
                    data.PvpRewardAchieved = packet.ReadInt32("PvpRewardAchieved", indexes);
                }
                if (changesMask[95])
                {
                    data.PvpTierMaxFromWins = packet.ReadInt32("PvpTierMaxFromWins", indexes);
                }
                if (changesMask[96])
                {
                    data.PvpLastWeeksRewardAchieved = packet.ReadInt32("PvpLastWeeksRewardAchieved", indexes);
                }
                if (changesMask[97])
                {
                    data.PvpLastWeeksTierMaxFromWins = packet.ReadInt32("PvpLastWeeksTierMaxFromWins", indexes);
                }
            }
            if (changesMask[98])
            {
                if (changesMask[99])
                {
                    data.PvpLastWeeksRewardClaimed = packet.ReadInt32("PvpLastWeeksRewardClaimed", indexes);
                }
                if (changesMask[100])
                {
                    data.NumBankSlots = packet.ReadByte("NumBankSlots", indexes);
                }
            }
            if (changesMask[101])
            {
                for (var i = 0; i < 195; ++i)
                {
                    if (changesMask[102 + i])
                    {
                        data.InvSlots[i] = packet.ReadPackedGuid128("InvSlots", indexes, i);
                    }
                }
            }
            if (changesMask[297])
            {
                for (var i = 0; i < 2; ++i)
                {
                    if (changesMask[298 + i])
                    {
                        data.TrackResourceMask[i] = packet.ReadUInt32("TrackResourceMask", indexes, i);
                    }
                }
            }
            if (changesMask[300])
            {
                for (var i = 0; i < 192; ++i)
                {
                    if (changesMask[301 + i])
                    {
                        data.ExploredZones[i] = packet.ReadUInt64("ExploredZones", indexes, i);
                    }
                }
            }
            if (changesMask[493])
            {
                for (var i = 0; i < 2; ++i)
                {
                    if (changesMask[494 + i])
                    {
                        data.RestInfo[i] = ReadUpdateRestInfo(packet, data.RestInfo[i] as RestInfo, indexes, "RestInfo", i);
                    }
                }
            }
            if (changesMask[496])
            {
                for (var i = 0; i < 7; ++i)
                {
                    if (changesMask[497 + i])
                    {
                        data.ModDamageDonePos[i] = packet.ReadInt32("ModDamageDonePos", indexes, i);
                    }
                    if (changesMask[504 + i])
                    {
                        data.ModDamageDoneNeg[i] = packet.ReadInt32("ModDamageDoneNeg", indexes, i);
                    }
                    if (changesMask[511 + i])
                    {
                        data.ModDamageDonePercent[i] = packet.ReadSingle("ModDamageDonePercent", indexes, i);
                    }
                }
            }
            if (changesMask[518])
            {
                for (var i = 0; i < 3; ++i)
                {
                    if (changesMask[519 + i])
                    {
                        data.WeaponDmgMultipliers[i] = packet.ReadSingle("WeaponDmgMultipliers", indexes, i);
                    }
                    if (changesMask[522 + i])
                    {
                        data.WeaponAtkSpeedMultipliers[i] = packet.ReadSingle("WeaponAtkSpeedMultipliers", indexes, i);
                    }
                }
            }
            if (changesMask[525])
            {
                for (var i = 0; i < 12; ++i)
                {
                    if (changesMask[526 + i])
                    {
                        data.BuybackPrice[i] = packet.ReadUInt32("BuybackPrice", indexes, i);
                    }
                    if (changesMask[538 + i])
                    {
                        data.BuybackTimestamp[i] = packet.ReadUInt32("BuybackTimestamp", indexes, i);
                    }
                }
            }
            if (changesMask[550])
            {
                for (var i = 0; i < 32; ++i)
                {
                    if (changesMask[551 + i])
                    {
                        data.CombatRatings[i] = packet.ReadInt32("CombatRatings", indexes, i);
                    }
                }
            }
            if (changesMask[590])
            {
                for (var i = 0; i < 4; ++i)
                {
                    if (changesMask[591 + i])
                    {
                        data.NoReagentCostMask[i] = packet.ReadUInt32("NoReagentCostMask", indexes, i);
                    }
                }
            }
            if (changesMask[595])
            {
                for (var i = 0; i < 2; ++i)
                {
                    if (changesMask[596 + i])
                    {
                        data.ProfessionSkillLine[i] = packet.ReadInt32("ProfessionSkillLine", indexes, i);
                    }
                }
            }
            if (changesMask[598])
            {
                for (var i = 0; i < 4; ++i)
                {
                    if (changesMask[599 + i])
                    {
                        data.BagSlotFlags[i] = packet.ReadUInt32("BagSlotFlags", indexes, i);
                    }
                }
            }
            if (changesMask[603])
            {
                for (var i = 0; i < 7; ++i)
                {
                    if (changesMask[604 + i])
                    {
                        data.BankBagSlotFlags[i] = packet.ReadUInt32("BankBagSlotFlags", indexes, i);
                    }
                }
            }
            if (changesMask[611])
            {
                for (var i = 0; i < 875; ++i)
                {
                    if (changesMask[612 + i])
                    {
                        data.QuestCompleted[i] = packet.ReadUInt64("QuestCompleted", indexes, i);
                    }
                }
            }
            if (changesMask[583])
            {
                for (var i = 0; i < 6; ++i)
                {
                    if (changesMask[584 + i])
                    {
                        data.PvpInfo[i] = ReadUpdatePVPInfo(packet, data.PvpInfo[i] as PVPInfo, indexes, "PvpInfo", i);
                    }
                }
            }
            return data;
        }

        public override IGameObjectData ReadCreateGameObjectData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            var data = new GameObjectData();
            data.DisplayID = packet.ReadInt32("DisplayID", indexes);
            data.SpellVisualID = packet.ReadUInt32("SpellVisualID", indexes);
            data.StateSpellVisualID = packet.ReadUInt32("StateSpellVisualID", indexes);
            data.SpawnTrackingStateAnimID = packet.ReadUInt32("SpawnTrackingStateAnimID", indexes);
            data.SpawnTrackingStateAnimKitID = packet.ReadUInt32("SpawnTrackingStateAnimKitID", indexes);
            data.StateWorldEffectIDs = new System.Nullable<uint>[packet.ReadUInt32()];
            data.StateWorldEffectsQuestObjectiveID = packet.ReadUInt32("StateWorldEffectsQuestObjectiveID", indexes);
            for (var i = 0; i < data.StateWorldEffectIDs.Length; ++i)
            {
                data.StateWorldEffectIDs[i] = packet.ReadUInt32("StateWorldEffectIDs", indexes, i);
            }
            data.CreatedBy = packet.ReadPackedGuid128("CreatedBy", indexes);
            data.GuildGUID = packet.ReadPackedGuid128("GuildGUID", indexes);
            data.Flags = packet.ReadUInt32("Flags", indexes);
            data.ParentRotation = packet.ReadQuaternion("ParentRotation", indexes);
            data.FactionTemplate = packet.ReadInt32("FactionTemplate", indexes);
            data.Level = packet.ReadInt32("Level", indexes);
            data.State = packet.ReadSByte("State", indexes);
            data.TypeID = packet.ReadSByte("TypeID", indexes);
            data.PercentHealth = packet.ReadByte("PercentHealth", indexes);
            data.ArtKit = packet.ReadUInt32("ArtKit", indexes);
            data.EnableDoodadSets.Resize(packet.ReadUInt32());
            data.CustomParam = packet.ReadUInt32("CustomParam", indexes);
            for (var i = 0; i < data.EnableDoodadSets.Count; ++i)
            {
                data.EnableDoodadSets[i] = packet.ReadInt32("EnableDoodadSets", indexes, i);
            }
            return data;
        }

        public override IGameObjectData ReadUpdateGameObjectData(Packet packet, params object[] indexes)
        {
            var data = new GameObjectData();
            var rawChangesMask = new int[1];
            rawChangesMask[0] = (int)packet.ReadBits(20);
            var changesMask = new BitArray(rawChangesMask);

            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.StateWorldEffectIDs = Enumerable.Range(0, (int)packet.ReadBits(32)).Select(x => new uint()).Cast<System.Nullable<uint>>().ToArray();
                    for (var i = 0; i < data.StateWorldEffectIDs.Length; ++i)
                    {
                        data.StateWorldEffectIDs[i] = packet.ReadUInt32("StateWorldEffectIDs", indexes, i);
                    }
                }
            }
            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[2])
                {
                    data.EnableDoodadSets.ReadUpdateMask(packet);
                }
            }
            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[2])
                {
                    for (var i = 0; i < data.EnableDoodadSets.Count; ++i)
                    {
                        if (data.EnableDoodadSets.UpdateMask[i])
                        {
                            data.EnableDoodadSets[i] = packet.ReadInt32("EnableDoodadSets", indexes, i);
                        }
                    }
                }
                if (changesMask[3])
                {
                    data.DisplayID = packet.ReadInt32("DisplayID", indexes);
                }
                if (changesMask[4])
                {
                    data.SpellVisualID = packet.ReadUInt32("SpellVisualID", indexes);
                }
                if (changesMask[5])
                {
                    data.StateSpellVisualID = packet.ReadUInt32("StateSpellVisualID", indexes);
                }
                if (changesMask[6])
                {
                    data.SpawnTrackingStateAnimID = packet.ReadUInt32("SpawnTrackingStateAnimID", indexes);
                }
                if (changesMask[7])
                {
                    data.SpawnTrackingStateAnimKitID = packet.ReadUInt32("SpawnTrackingStateAnimKitID", indexes);
                }
                if (changesMask[8])
                {
                    data.StateWorldEffectsQuestObjectiveID = packet.ReadUInt32("StateWorldEffectsQuestObjectiveID", indexes);
                }
                if (changesMask[9])
                {
                    data.CreatedBy = packet.ReadPackedGuid128("CreatedBy", indexes);
                }
                if (changesMask[10])
                {
                    data.GuildGUID = packet.ReadPackedGuid128("GuildGUID", indexes);
                }
                if (changesMask[11])
                {
                    data.Flags = packet.ReadUInt32("Flags", indexes);
                }
                if (changesMask[12])
                {
                    data.ParentRotation = packet.ReadQuaternion("ParentRotation", indexes);
                }
                if (changesMask[13])
                {
                    data.FactionTemplate = packet.ReadInt32("FactionTemplate", indexes);
                }
                if (changesMask[14])
                {
                    data.Level = packet.ReadInt32("Level", indexes);
                }
                if (changesMask[15])
                {
                    data.State = packet.ReadSByte("State", indexes);
                }
                if (changesMask[16])
                {
                    data.TypeID = packet.ReadSByte("TypeID", indexes);
                }
                if (changesMask[17])
                {
                    data.PercentHealth = packet.ReadByte("PercentHealth", indexes);
                }
                if (changesMask[18])
                {
                    data.ArtKit = packet.ReadUInt32("ArtKit", indexes);
                }
                if (changesMask[19])
                {
                    data.CustomParam = packet.ReadUInt32("CustomParam", indexes);
                }
            }
            return data;
        }

        public override IDynamicObjectData ReadCreateDynamicObjectData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            var data = new DynamicObjectData();
            data.Caster = packet.ReadPackedGuid128("Caster", indexes);
            data.SpellXSpellVisualID = packet.ReadInt32("SpellXSpellVisualID", indexes);
            data.SpellID = packet.ReadInt32("SpellID", indexes);
            data.Radius = packet.ReadSingle("Radius", indexes);
            data.CastTime = packet.ReadUInt32("CastTime", indexes);
            data.Type = packet.ReadByte("Type", indexes);
            return data;
        }

        public override IDynamicObjectData ReadUpdateDynamicObjectData(Packet packet, params object[] indexes)
        {
            var data = new DynamicObjectData();
            var rawChangesMask = new int[1];
            rawChangesMask[0] = (int)packet.ReadBits(7);
            var changesMask = new BitArray(rawChangesMask);

            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.Caster = packet.ReadPackedGuid128("Caster", indexes);
                }
                if (changesMask[2])
                {
                    data.SpellXSpellVisualID = packet.ReadInt32("SpellXSpellVisualID", indexes);
                }
                if (changesMask[3])
                {
                    data.SpellID = packet.ReadInt32("SpellID", indexes);
                }
                if (changesMask[4])
                {
                    data.Radius = packet.ReadSingle("Radius", indexes);
                }
                if (changesMask[5])
                {
                    data.CastTime = packet.ReadUInt32("CastTime", indexes);
                }
                if (changesMask[6])
                {
                    data.Type = packet.ReadByte("Type", indexes);
                }
            }
            return data;
        }

        public override ICorpseData ReadCreateCorpseData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            var data = new CorpseData();
            data.DynamicFlags = packet.ReadUInt32("DynamicFlags", indexes);
            data.Owner = packet.ReadPackedGuid128("Owner", indexes);
            data.PartyGUID = packet.ReadPackedGuid128("PartyGUID", indexes);
            data.GuildGUID = packet.ReadPackedGuid128("GuildGUID", indexes);
            data.DisplayID = packet.ReadUInt32("DisplayID", indexes);
            for (var i = 0; i < 19; ++i)
            {
                data.Items[i] = packet.ReadUInt32("Items", indexes, i);
            }
            data.Unused = packet.ReadByte("Unused", indexes);
            data.RaceID = packet.ReadByte("RaceID", indexes);
            data.Sex = packet.ReadByte("Sex", indexes);
            data.SkinID = packet.ReadByte("SkinID", indexes);
            data.FaceID = packet.ReadByte("FaceID", indexes);
            data.HairStyleID = packet.ReadByte("HairStyleID", indexes);
            data.HairColorID = packet.ReadByte("HairColorID", indexes);
            data.FacialHairStyleID = packet.ReadByte("FacialHairStyleID", indexes);
            data.Flags = packet.ReadUInt32("Flags", indexes);
            data.FactionTemplate = packet.ReadInt32("FactionTemplate", indexes);
            for (var i = 0; i < 3; ++i)
            {
                data.CustomDisplayOption[i] = packet.ReadByte("CustomDisplayOption", indexes, i);
            }
            return data;
        }

        public override ICorpseData ReadUpdateCorpseData(Packet packet, params object[] indexes)
        {
            var data = new CorpseData();
            var rawChangesMask = new int[2];
            var rawMaskMask = new int[1];
            rawMaskMask[0] = (int)packet.ReadBits(2);
            var maskMask = new BitArray(rawMaskMask);
            for (var i = 0; i < 2; ++i)
                if (maskMask[i])
                    rawChangesMask[i] = (int)packet.ReadBits(32);
            var changesMask = new BitArray(rawChangesMask);

            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.DynamicFlags = packet.ReadUInt32("DynamicFlags", indexes);
                }
                if (changesMask[2])
                {
                    data.Owner = packet.ReadPackedGuid128("Owner", indexes);
                }
                if (changesMask[3])
                {
                    data.PartyGUID = packet.ReadPackedGuid128("PartyGUID", indexes);
                }
                if (changesMask[4])
                {
                    data.GuildGUID = packet.ReadPackedGuid128("GuildGUID", indexes);
                }
                if (changesMask[5])
                {
                    data.DisplayID = packet.ReadUInt32("DisplayID", indexes);
                }
                if (changesMask[6])
                {
                    data.Unused = packet.ReadByte("Unused", indexes);
                }
                if (changesMask[7])
                {
                    data.RaceID = packet.ReadByte("RaceID", indexes);
                }
                if (changesMask[8])
                {
                    data.Sex = packet.ReadByte("Sex", indexes);
                }
                if (changesMask[9])
                {
                    data.SkinID = packet.ReadByte("SkinID", indexes);
                }
                if (changesMask[10])
                {
                    data.FaceID = packet.ReadByte("FaceID", indexes);
                }
                if (changesMask[11])
                {
                    data.HairStyleID = packet.ReadByte("HairStyleID", indexes);
                }
                if (changesMask[12])
                {
                    data.HairColorID = packet.ReadByte("HairColorID", indexes);
                }
                if (changesMask[13])
                {
                    data.FacialHairStyleID = packet.ReadByte("FacialHairStyleID", indexes);
                }
                if (changesMask[14])
                {
                    data.Flags = packet.ReadUInt32("Flags", indexes);
                }
                if (changesMask[15])
                {
                    data.FactionTemplate = packet.ReadInt32("FactionTemplate", indexes);
                }
            }
            if (changesMask[16])
            {
                for (var i = 0; i < 19; ++i)
                {
                    if (changesMask[17 + i])
                    {
                        data.Items[i] = packet.ReadUInt32("Items", indexes, i);
                    }
                }
            }
            if (changesMask[36])
            {
                for (var i = 0; i < 3; ++i)
                {
                    if (changesMask[37 + i])
                    {
                        data.CustomDisplayOption[i] = packet.ReadByte("CustomDisplayOption", indexes, i);
                    }
                }
            }
            return data;
        }

        public static IScaleCurve ReadCreateScaleCurve(Packet packet, params object[] indexes)
        {
            var data = new ScaleCurve();
            packet.ResetBitReader();
            data.StartTimeOffset = packet.ReadUInt32("StartTimeOffset", indexes);
            for (var i = 0; i < 2; ++i)
            {
                data.Points[i] = packet.ReadVector2("Points", indexes, i);
            }
            data.ParameterCurve = packet.ReadUInt32("ParameterCurve", indexes);
            data.OverrideActive = packet.ReadBit("OverrideActive", indexes);
            return data;
        }

        public static IScaleCurve ReadUpdateScaleCurve(Packet packet, IScaleCurve existingData, params object[] indexes)
        {
            var data = existingData as ScaleCurve;
            if (data == null)
                data = new ScaleCurve();
            packet.ResetBitReader();
            var rawChangesMask = new int[1];
            rawChangesMask[0] = (int)packet.ReadBits(7);
            var changesMask = new BitArray(rawChangesMask);

            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.OverrideActive = packet.ReadBit("OverrideActive", indexes);
                }
            }
            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[2])
                {
                    data.StartTimeOffset = packet.ReadUInt32("StartTimeOffset", indexes);
                }
                if (changesMask[3])
                {
                    data.ParameterCurve = packet.ReadUInt32("ParameterCurve", indexes);
                }
            }
            if (changesMask[4])
            {
                for (var i = 0; i < 2; ++i)
                {
                    if (changesMask[5 + i])
                    {
                        data.Points[i] = packet.ReadVector2("Points", indexes, i);
                    }
                }
            }
            return data;
        }

        public override IAreaTriggerData ReadCreateAreaTriggerData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            var data = new AreaTriggerData();
            data.OverrideScaleCurve = ReadCreateScaleCurve(packet, indexes, "OverrideScaleCurve");
            data.Caster = packet.ReadPackedGuid128("Caster", indexes);
            data.Duration = packet.ReadUInt32("Duration", indexes);
            data.TimeToTarget = packet.ReadUInt32("TimeToTarget", indexes);
            data.TimeToTargetScale = packet.ReadUInt32("TimeToTargetScale", indexes);
            data.TimeToTargetExtraScale = packet.ReadUInt32("TimeToTargetExtraScale", indexes);
            data.SpellID = packet.ReadInt32("SpellID", indexes);
            data.SpellForVisuals = packet.ReadInt32("SpellForVisuals", indexes);
            data.SpellXSpellVisualID = packet.ReadInt32("SpellXSpellVisualID", indexes);
            data.BoundsRadius2D = packet.ReadSingle("BoundsRadius2D", indexes);
            data.DecalPropertiesID = packet.ReadUInt32("DecalPropertiesID", indexes);
            data.CreatingEffectGUID = packet.ReadPackedGuid128("CreatingEffectGUID", indexes);
            data.ExtraScaleCurve = ReadCreateScaleCurve(packet, indexes, "ExtraScaleCurve");
            return data;
        }

        public override IAreaTriggerData ReadUpdateAreaTriggerData(Packet packet, params object[] indexes)
        {
            var data = new AreaTriggerData();
            var rawChangesMask = new int[1];
            rawChangesMask[0] = (int)packet.ReadBits(14);
            var changesMask = new BitArray(rawChangesMask);

            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.OverrideScaleCurve = ReadUpdateScaleCurve(packet, data.OverrideScaleCurve as ScaleCurve, indexes, "OverrideScaleCurve");
                }
                if (changesMask[3])
                {
                    data.Caster = packet.ReadPackedGuid128("Caster", indexes);
                }
                if (changesMask[4])
                {
                    data.Duration = packet.ReadUInt32("Duration", indexes);
                }
                if (changesMask[5])
                {
                    data.TimeToTarget = packet.ReadUInt32("TimeToTarget", indexes);
                }
                if (changesMask[6])
                {
                    data.TimeToTargetScale = packet.ReadUInt32("TimeToTargetScale", indexes);
                }
                if (changesMask[7])
                {
                    data.TimeToTargetExtraScale = packet.ReadUInt32("TimeToTargetExtraScale", indexes);
                }
                if (changesMask[8])
                {
                    data.SpellID = packet.ReadInt32("SpellID", indexes);
                }
                if (changesMask[9])
                {
                    data.SpellForVisuals = packet.ReadInt32("SpellForVisuals", indexes);
                }
                if (changesMask[10])
                {
                    data.SpellXSpellVisualID = packet.ReadInt32("SpellXSpellVisualID", indexes);
                }
                if (changesMask[11])
                {
                    data.BoundsRadius2D = packet.ReadSingle("BoundsRadius2D", indexes);
                }
                if (changesMask[12])
                {
                    data.DecalPropertiesID = packet.ReadUInt32("DecalPropertiesID", indexes);
                }
                if (changesMask[13])
                {
                    data.CreatingEffectGUID = packet.ReadPackedGuid128("CreatingEffectGUID", indexes);
                }
                if (changesMask[2])
                {
                    data.ExtraScaleCurve = ReadUpdateScaleCurve(packet, data.ExtraScaleCurve as ScaleCurve, indexes, "ExtraScaleCurve");
                }
            }
            return data;
        }

        public override ISceneObjectData ReadCreateSceneObjectData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            var data = new SceneObjectData();
            data.ScriptPackageID = packet.ReadInt32("ScriptPackageID", indexes);
            data.RndSeedVal = packet.ReadUInt32("RndSeedVal", indexes);
            data.CreatedBy = packet.ReadPackedGuid128("CreatedBy", indexes);
            data.SceneType = packet.ReadUInt32("SceneType", indexes);
            return data;
        }

        public override ISceneObjectData ReadUpdateSceneObjectData(Packet packet, params object[] indexes)
        {
            var data = new SceneObjectData();
            var rawChangesMask = new int[1];
            rawChangesMask[0] = (int)packet.ReadBits(5);
            var changesMask = new BitArray(rawChangesMask);

            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.ScriptPackageID = packet.ReadInt32("ScriptPackageID", indexes);
                }
                if (changesMask[2])
                {
                    data.RndSeedVal = packet.ReadUInt32("RndSeedVal", indexes);
                }
                if (changesMask[3])
                {
                    data.CreatedBy = packet.ReadPackedGuid128("CreatedBy", indexes);
                }
                if (changesMask[4])
                {
                    data.SceneType = packet.ReadUInt32("SceneType", indexes);
                }
            }
            return data;
        }

        public static IConversationLine ReadCreateConversationLine(Packet packet, params object[] indexes)
        {
            var data = new ConversationLine();
            data.ConversationLineID = packet.ReadInt32("ConversationLineID", indexes);
            data.StartTime = packet.ReadUInt32("StartTime", indexes);
            data.UiCameraID = packet.ReadInt32("UiCameraID", indexes);
            data.ActorIndex = packet.ReadByte("ActorIndex", indexes);
            data.Flags = packet.ReadByte("Flags", indexes);
            return data;
        }

        public static IConversationLine ReadUpdateConversationLine(Packet packet, IConversationLine existingData, params object[] indexes)
        {
            var data = existingData as ConversationLine;
            if (data == null)
                data = new ConversationLine();
            data.ConversationLineID = packet.ReadInt32("ConversationLineID", indexes);
            data.StartTime = packet.ReadUInt32("StartTime", indexes);
            data.UiCameraID = packet.ReadInt32("UiCameraID", indexes);
            data.ActorIndex = packet.ReadByte("ActorIndex", indexes);
            data.Flags = packet.ReadByte("Flags", indexes);
            return data;
        }

        public static IConversationActor ReadCreateConversationActor(Packet packet, params object[] indexes)
        {
            var data = new ConversationActor();
            packet.ResetBitReader();
            data.CreatureID = packet.ReadUInt32("CreatureID", indexes);
            data.CreatureDisplayInfoID = packet.ReadUInt32("CreatureDisplayInfoID", indexes);
            data.ActorGUID = packet.ReadPackedGuid128("ActorGUID", indexes);
            data.Id = packet.ReadInt32("Id", indexes);
            data.Type = packet.ReadBits("Type", 1, indexes);
            return data;
        }

        public static IConversationActor ReadUpdateConversationActor(Packet packet, IConversationActor existingData, params object[] indexes)
        {
            packet.ResetBitReader();
            uint creatureID = packet.ReadUInt32("CreatureID", indexes);
            uint creatureDisplayInfoID = packet.ReadUInt32("CreatureDisplayInfoID", indexes);
            WowGuid actorGUID = packet.ReadPackedGuid128("ActorGUID", indexes);
            int id = packet.ReadInt32("Id", indexes);
            uint type = packet.ReadBits("Type", 1, indexes);

            var data = existingData as ConversationActor;
            if (data == null)
            {
                data = new ConversationActor();
                data.CreatureID = creatureID;
                data.CreatureDisplayInfoID = creatureDisplayInfoID;
                data.ActorGUID = actorGUID;
                data.Id = id;
                data.Type = type;
            }
            return data;
        }

        public override IConversationData ReadCreateConversationData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            var data = new ConversationData();
            data.Lines = new IConversationLine[packet.ReadUInt32()];
            data.LastLineEndTime = packet.ReadInt32("LastLineEndTime", indexes);
            data.Progress = packet.ReadUInt32("Progress", indexes);
            for (var i = 0; i < data.Lines.Length; ++i)
            {
                data.Lines[i] = ReadCreateConversationLine(packet, indexes, "Lines", i);
            }
            data.Actors.Resize(packet.ReadUInt32());
            for (var i = 0; i < data.Actors.Count; ++i)
            {
                data.Actors[i] = ReadCreateConversationActor(packet, indexes, "Actors", i);
            }
            return data;
        }

        public override IConversationData ReadUpdateConversationData(Packet packet, params object[] indexes)
        {
            var data = new ConversationData();
            var rawChangesMask = new int[1];
            rawChangesMask[0] = (int)packet.ReadBits(5);
            var changesMask = new BitArray(rawChangesMask);

            if (changesMask[0])
            {
                if (changesMask[1])
                {
                    data.Lines = Enumerable.Range(0, (int)packet.ReadBits(32)).Select(x => new ConversationLine()).Cast<IConversationLine>().ToArray();
                    for (var i = 0; i < data.Lines.Length; ++i)
                    {
                        data.Lines[i] = ReadUpdateConversationLine(packet, data.Lines[i] as ConversationLine, indexes, "Lines", i);
                    }
                }
            }
            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[2])
                {
                    data.Actors.ReadUpdateMask(packet);
                }
            }
            packet.ResetBitReader();
            if (changesMask[0])
            {
                if (changesMask[2])
                {
                    for (var i = 0; i < data.Actors.Count; ++i)
                    {
                        if (data.Actors.UpdateMask[i])
                        {
                            data.Actors[i] = ReadUpdateConversationActor(packet, data.Actors[i] as ConversationActor, indexes, "Actors", i);
                        }
                    }
                }
                if (changesMask[3])
                {
                    data.LastLineEndTime = packet.ReadInt32("LastLineEndTime", indexes);
                }
                if (changesMask[4])
                {
                    data.Progress = packet.ReadUInt32("Progress", indexes);
                }
            }
            return data;
        }

    }
}
