using System;
using System.Collections.Generic;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class NpcHandler
    {
        public static uint LastGossipPOIEntry = 0;

        [Parser(Opcode.CMSG_BANKER_ACTIVATE)]
        [Parser(Opcode.CMSG_BINDER_ACTIVATE)]
        [Parser(Opcode.SMSG_BINDER_CONFIRM)]
        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        [Parser(Opcode.CMSG_LIST_INVENTORY)]
        [Parser(Opcode.CMSG_TRAINER_LIST)]
        public static void HandleNpcHello(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.CMSG_NPC_TEXT_QUERY)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
            packet.ReadPackedGuid128("Guid");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NPC_TEXT_UPDATE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var npcText = new NpcTextMop();

            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // Can be masked
                return;

            var hasData = packet.ReadBit("Has Data");
            if (!hasData)
                return; // nothing to do

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);

            var pkt = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);
            npcText.Probabilities = new float[8];
            npcText.BroadcastTextId = new uint[8];
            for (var i = 0; i < 8; ++i)
                npcText.Probabilities[i] = pkt.ReadSingle("Probability", i);
            for (var i = 0; i < 8; ++i)
                npcText.BroadcastTextId[i] = pkt.ReadUInt32("Broadcast Text Id", i);

            pkt.ClosePacket(false);

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTextsMop.Add((uint)entry.Key, npcText, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");

            var gossipId = packet.ReadUInt32("Gossip Id");
            var menuEntry = packet.ReadUInt32("Menu Id");

            var bits8 = packet.ReadBits(8);
            packet.ResetBitReader();
            packet.ReadWoWString("Box Text", bits8);

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipId), null, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_GOSSIP_POI)]
        public static void HandleGossipPoi(Packet packet)
        {
            ++LastGossipPOIEntry;

            var gossipPOI = new GossipPOI();

            gossipPOI.Flags = (uint)packet.ReadBits("Flags", 14);
            var bit84 = packet.ReadBits(6);
            var pos = packet.ReadVector2("Coordinates");
            gossipPOI.Icon = packet.ReadEnum<GossipPOIIcon>("Icon", TypeCode.UInt32);
            gossipPOI.Importance = packet.ReadUInt32("Data");
            gossipPOI.Name = packet.ReadWoWString("Icon Name", bit84);

            gossipPOI.PositionX = pos.X;
            gossipPOI.PositionY = pos.Y;

            Storage.GossipPOIs.Add(LastGossipPOIEntry, gossipPOI, packet.TimeSpan);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            var gossip = new Gossip();

            var guid = packet.ReadPackedGuid128("GossipGUID");

            gossip.ObjectType = guid.GetObjectType();
            gossip.ObjectEntry = guid.GetEntry();

            var menuId = packet.ReadInt32("GossipID");
            packet.ReadInt32("FriendshipFactionID");
            var textId = packet.ReadInt32("TextID");

            var int44 = packet.ReadInt32("GossipOptions");
            var int60 = packet.ReadInt32("GossipText");

            // ClientGossipOptions
            gossip.GossipOptions = new List<GossipOption>(int44);
            for (var i = 0; i < int44; ++i)
            {
                var gossipOption = new GossipOption();

                gossipOption.Index = (uint)packet.ReadInt32("ClientOption", i);
                packet.ReadByte("OptionNPC", i);
                packet.ReadByte("OptionFlags", i);
                gossipOption.RequiredMoney = (uint)packet.ReadInt32("OptionCost", i);

                var bits3 = packet.ReadBits(12);
                var bits753 = packet.ReadBits(12);

                gossipOption.OptionText = packet.ReadWoWString("Text", bits3, i);
                gossipOption.BoxText = packet.ReadWoWString("Confirm", bits753, i);

                gossip.GossipOptions.Add(gossipOption);
            }

            // ClientGossipOptions
            for (var i = 0; i < int60; ++i)
            {
                packet.ReadInt32("QuestID", i);
                packet.ReadInt32("QuestType", i);
                packet.ReadInt32("QuestLevel", i);

                for (var j = 0; j < 2; ++j)
                    packet.ReadInt32("QuestFlags", i, j);

                packet.ResetBitReader();

                packet.ReadBit("Repeatable");

                var bits13 = packet.ReadBits(9);

                packet.ReadWoWString("QuestTitle", bits13, i);

                if (guid.GetObjectType() == ObjectType.Unit)
                    if (Storage.Objects.ContainsKey(guid))
                        ((Unit)Storage.Objects[guid].Item1).GossipId = (uint)menuId;

                if (Storage.Gossips.ContainsKey(Tuple.Create((uint)menuId, (uint)textId)))
                {
                    var oldGossipOptions = Storage.Gossips[Tuple.Create((uint)menuId, (uint)textId)];
                    if (oldGossipOptions != null)
                        foreach (var gossipOptions in gossip.GossipOptions)
                            oldGossipOptions.Item1.GossipOptions.Add(gossipOptions);
                }
                else
                    Storage.Gossips.Add(Tuple.Create((uint)menuId, (uint)textId), gossip, packet.TimeSpan);

                packet.AddSniffData(StoreNameType.Gossip, (int)menuId, guid.GetEntry().ToString(CultureInfo.InvariantCulture));
            }
        }

        [Parser(Opcode.SMSG_LIST_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            var npcVendor = new NpcVendor();

            var guid = packet.ReadPackedGuid128("VendorGUID");

            packet.ReadByte("Reason");

            var int9 = packet.ReadInt32("VendorItems");

            npcVendor.VendorItems = new List<VendorItem>(int9);
            for (var i = 0; i < int9; ++i)
            {
                var vendorItem = new VendorItem();

                vendorItem.Slot = (uint)packet.ReadInt32("Muid", i);
                vendorItem.Type = (uint)packet.ReadInt32("Type", i);

                // ItemInstance
                //if (ItemInstance)
                {
                    vendorItem.ItemId = (uint)packet.ReadEntry<Int32>(StoreNameType.Item, "ItemID", i);
                    packet.ReadUInt32("RandomPropertiesSeed", i);
                    packet.ReadUInt32("RandomPropertiesID", i);

                    packet.ResetBitReader();

                    var hasBonuses = packet.ReadBit("HasItemBonus", i);
                    var hasModifications = packet.ReadBit("HasModifications", i);
                    if (hasBonuses)
                    {
                        packet.ReadByte("Context", i);

                        var bonusCount = packet.ReadUInt32();
                        for (var j = 0; j < bonusCount; ++j)
                            packet.ReadUInt32("BonusListID", i, j);
                    }

                    if (hasModifications)
                    {
                        var modificationCount = packet.ReadUInt32() / 4;
                        for (var j = 0; j < modificationCount; ++j)
                            packet.ReadUInt32("Modification", i, j);
                    }
                }

                var maxCount = packet.ReadInt32("Quantity", i);
                packet.ReadInt32("Price", i);
                packet.ReadInt32("Durability", i);
                var buyCount = packet.ReadInt32("StackCount", i);
                vendorItem.ExtendedCostId = (uint)packet.ReadInt32("ExtendedCostID", i);
                packet.ReadInt32("PlayerConditionFailed", i);

                packet.ResetBitReader();

                packet.ReadBit("DoNotFilterOnVendor", i);

                vendorItem.MaxCount = maxCount == -1 ? 0 : maxCount; // TDB
                if (vendorItem.Type == 2)
                    vendorItem.MaxCount = (int)buyCount;

                npcVendor.VendorItems.Add(vendorItem);
            }

            Storage.NpcVendors.Add(guid.GetEntry(), npcVendor, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            var npcTrainer = new NpcTrainer();

            var guid = packet.ReadPackedGuid128("TrainerGUID");

            packet.ReadInt32("TrainerType");
            packet.ReadInt32("TrainerID");
            var int36 = packet.ReadInt32("Spells");

            npcTrainer.TrainerSpells = new List<TrainerSpell>(int36);
            // ClientTrainerListSpell
            for (var i = 0; i < int36; ++i)
            {
                var trainerSpell = new TrainerSpell();

                trainerSpell.Spell = (uint)packet.ReadEntry<Int32>(StoreNameType.Spell, "SpellID", i);
                trainerSpell.Cost = (uint)packet.ReadInt32("MoneyCost", i);
                trainerSpell.RequiredSkill = (uint)packet.ReadInt32("ReqSkillLine", i);
                trainerSpell.RequiredSkillLevel = (uint)packet.ReadInt32("ReqSkillRank", i);

                for (var j = 0; j < 3; ++j)
                    packet.ReadInt32("ReqAbility", i, j);

                packet.ReadEnum<TrainerSpellState>("Usable", TypeCode.Byte, i);
                trainerSpell.RequiredLevel = packet.ReadByte("ReqLevel", i);

                npcTrainer.TrainerSpells.Add(trainerSpell);
            }

            var bits56 = packet.ReadBits(11);
            npcTrainer.Title = packet.ReadWoWString("Greeting", bits56);

            if (Storage.NpcTrainers.ContainsKey(guid.GetEntry()))
            {
                var oldTrainer = Storage.NpcTrainers[guid.GetEntry()];
                if (oldTrainer != null)
                {
                    foreach (var trainerSpell in npcTrainer.TrainerSpells)
                        oldTrainer.Item1.TrainerSpells.Add(trainerSpell);
                }
            }
            else
                Storage.NpcTrainers.Add(guid.GetEntry(), npcTrainer, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_TRAINER_BUY_SPELL)]
        public static void HandleTrainerBuySpell(Packet packet)
        {
            packet.ReadPackedGuid128("TrainerGUID");
            packet.ReadInt32("TrainerID");
            packet.ReadEntry<Int32>(StoreNameType.Spell, "SpellID");
        }

        [Parser(Opcode.CMSG_SPELLCLICK)]
        public static void HandleSpellClick(Packet packet)
        {
            var guid = packet.ReadPackedGuid128("SpellClickUnitGUID");
            packet.ReadBit("TryAutoDismount");

            if (guid.GetObjectType() == ObjectType.Unit)
                Storage.NpcSpellClicks.Add(guid, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_BUY_BANK_SLOT)]
        public static void HandleBuyBankSlot(Packet packet)
        {
            packet.ReadPackedGuid128("Banker");
        }
    }
}
