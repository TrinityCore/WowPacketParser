using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.SQL;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class NpcHandler
    {
        public static uint LastGossipPOIEntry;

        public static void ReadGossipOptionsData(uint menuId, Packet packet, params object[] idx)
        {
            GossipMenuOption gossipOption = new GossipMenuOption
            {
                MenuID = menuId
            };

            gossipOption.ID = (uint)packet.ReadInt32("ClientOption", idx);
            gossipOption.OptionIcon = (GossipOptionIcon?)packet.ReadByte("OptionNPC", idx);
            gossipOption.BoxCoded = packet.ReadByte("OptionFlags", idx) != 0;
            gossipOption.BoxMoney = (uint)packet.ReadInt32("OptionCost", idx);

            uint textLen = packet.ReadBits(12);
            uint confirmLen = packet.ReadBits(12);

            gossipOption.OptionText = packet.ReadWoWString("Text", textLen, idx);
            gossipOption.BoxText = packet.ReadWoWString("Confirm", confirmLen, idx);

            List<int> boxTextList;
            List<int> optionTextList;

            if (gossipOption.BoxText != string.Empty && SQLDatabase.BroadcastMaleTexts.TryGetValue(gossipOption.BoxText, out boxTextList))
            {
                if (boxTextList.Count == 1)
                    gossipOption.BoxBroadcastTextID = boxTextList[0];
                else
                {
                    gossipOption.BroadcastTextIDHelper += "BoxBroadcastTextID: ";
                    gossipOption.BroadcastTextIDHelper += string.Join(" - ", boxTextList);
                }
            }
            else
                gossipOption.BoxBroadcastTextID = 0;

            if (gossipOption.OptionText != string.Empty && SQLDatabase.BroadcastMaleTexts.TryGetValue(gossipOption.OptionText, out optionTextList))
            {
                if (optionTextList.Count == 1)
                    gossipOption.OptionBroadcastTextID = optionTextList[0];
                else
                {
                    gossipOption.BroadcastTextIDHelper += "OptionBroadcastTextID: ";
                    gossipOption.BroadcastTextIDHelper += string.Join(" - ", optionTextList);
                }
            }
            else
                gossipOption.OptionBroadcastTextID = 0;


            Storage.GossipMenuOptions.Add(gossipOption, packet.TimeSpan);
        }

        public static void ReadGossipQuestTextData(Packet packet, params object[] idx)
        {
            packet.ReadInt32("QuestID", idx);
            packet.ReadInt32("QuestType", idx);
            packet.ReadInt32("QuestLevel", idx);

            for (int j = 0; j < 2; ++j)
                packet.ReadInt32("QuestFlags", idx, j);

            packet.ResetBitReader();

            packet.ReadBit("Repeatable");

            uint questTitleLen = packet.ReadBits(9);

            packet.ReadWoWString("QuestTitle", questTitleLen, idx);
        }

        [Parser(Opcode.CMSG_BANKER_ACTIVATE)]
        [Parser(Opcode.CMSG_BINDER_ACTIVATE)]
        [Parser(Opcode.SMSG_BINDER_CONFIRM)]
        [Parser(Opcode.CMSG_TALK_TO_GOSSIP)]
        [Parser(Opcode.CMSG_LIST_INVENTORY)]
        [Parser(Opcode.CMSG_TRAINER_LIST)]
        [Parser(Opcode.SMSG_SHOW_BANK)]
        public static void HandleNpcHello(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.CMSG_QUERY_NPC_TEXT)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
            packet.ReadPackedGuid128("Guid");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // Can be masked
                return;

            Bit hasData = packet.ReadBit("Has Data");
            int size = packet.ReadInt32("Size");

            if (!hasData || size == 0)
                return; // nothing to do

            NpcTextMop npcText = new NpcTextMop
            {
                ID = (uint)entry.Key
            };

            var data = packet.ReadBytes(size);

            Packet pkt = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);
            npcText.Probabilities = new float[8];
            npcText.BroadcastTextId = new uint[8];
            for (int i = 0; i < 8; ++i)
                npcText.Probabilities[i] = pkt.ReadSingle("Probability", i);
            for (int i = 0; i < 8; ++i)
                npcText.BroadcastTextId[i] = pkt.ReadUInt32("Broadcast Text Id", i);

            pkt.ClosePacket(false);

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTextsMop.Add(npcText, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            packet.ReadPackedGuid128("GossipUnit");

            var menuEntry = packet.ReadUInt32("GossipID");
            var gossipIdx = packet.ReadUInt32("GossipIndex");

            var bits8 = packet.ReadBits(8);
            packet.ResetBitReader();
            packet.ReadWoWString("PromotionCode", bits8);

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipIdx), null, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_GOSSIP_POI)]
        public static void HandleGossipPoi(Packet packet)
        {
            PointsOfInterest gossipPOI = new PointsOfInterest();
            gossipPOI.ID = ++LastGossipPOIEntry;

            gossipPOI.Flags = packet.ReadBits("Flags", 14);
            uint bit84 = packet.ReadBits(6);

            Vector2 pos = packet.ReadVector2("Coordinates");
            gossipPOI.PositionX = pos.X;
            gossipPOI.PositionY = pos.Y;

            gossipPOI.Icon = packet.ReadUInt32E<GossipPOIIcon>("Icon");
            gossipPOI.Importance = packet.ReadUInt32("Importance");
            gossipPOI.Name = packet.ReadWoWString("Name", bit84);

            Storage.GossipPOIs.Add(gossipPOI, packet.TimeSpan);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            GossipMenu gossip = new GossipMenu();

            WowGuid guid = packet.ReadPackedGuid128("GossipGUID");

            gossip.ObjectType = guid.GetObjectType();
            gossip.ObjectEntry = guid.GetEntry();

            int menuId = packet.ReadInt32("GossipID");
            gossip.Entry = (uint)menuId;

            packet.ReadInt32("FriendshipFactionID");

            gossip.TextID = (uint)packet.ReadInt32("TextID");

            int int44 = packet.ReadInt32("GossipOptions");
            int int60 = packet.ReadInt32("GossipText");

            for (int i = 0; i < int44; ++i)
                ReadGossipOptionsData((uint)menuId, packet, i, "GossipOptions");

            for (int i = 0; i < int60; ++i)
                ReadGossipQuestTextData(packet, i, "GossipQuestText");

            if (guid.GetObjectType() == ObjectType.Unit)
                if (Storage.Objects.ContainsKey(guid))
                    ((Unit)Storage.Objects[guid].Item1).GossipId = (uint)menuId;

            Storage.Gossips.Add(gossip, packet.TimeSpan);

            packet.AddSniffData(StoreNameType.Gossip, menuId, guid.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            uint entry = packet.ReadPackedGuid128("VendorGUID").GetEntry();
            packet.ReadByte("Reason");
            int count = packet.ReadInt32("VendorItems");

            for (int i = 0; i < count; ++i)
            {
                NpcVendor vendor = new NpcVendor
                {
                    Entry = entry,
                    Slot = packet.ReadInt32("Muid", i),
                    Type = (uint)packet.ReadInt32("Type", i),
                    Item = ItemHandler.ReadItemInstance(packet, i)
                };

                int maxCount = packet.ReadInt32("Quantity", i);
                packet.ReadInt32("Price", i);
                packet.ReadInt32("Durability", i);
                int buyCount = packet.ReadInt32("StackCount", i);
                vendor.ExtendedCost = packet.ReadUInt32("ExtendedCostID", i);
                vendor.PlayerConditionID = packet.ReadUInt32("PlayerConditionFailed", i);

                packet.ResetBitReader();

                vendor.IgnoreFiltering = packet.ReadBit("DoNotFilterOnVendor", i);

                vendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                if (vendor.Type == 2)
                    vendor.MaxCount = (uint)buyCount;

                Storage.NpcVendors.Add(vendor, packet.TimeSpan);
            }
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            NpcTrainer npcTrainer = new NpcTrainer();

            uint entry = packet.ReadPackedGuid128("TrainerGUID").GetEntry();

            packet.ReadInt32("TrainerType");
            packet.ReadInt32("TrainerID");
            int count = packet.ReadInt32("Spells");

            for (int i = 0; i < count; ++i)
            {
                NpcTrainer trainer = new NpcTrainer
                {
                    ID = entry,
                    SpellID = packet.ReadInt32<SpellId>("SpellID", i),
                    MoneyCost = packet.ReadUInt32("MoneyCost", i),
                    ReqSkillLine = packet.ReadUInt32("ReqSkillLine", i),
                    ReqSkillRank = packet.ReadUInt32("ReqSkillRank", i)
                };


                for (var j = 0; j < 3; ++j)
                    packet.ReadInt32("ReqAbility", i, j);

                packet.ReadByteE<TrainerSpellState>("Usable", i);
                trainer.ReqLevel = packet.ReadByte("ReqLevel", i);
            }

            uint bits56 = packet.ReadBits(11);
            packet.ReadWoWString("Greeting", bits56);

            Storage.NpcTrainers.Add(npcTrainer, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_TRAINER_BUY_SPELL)]
        public static void HandleTrainerBuySpell(Packet packet)
        {
            packet.ReadPackedGuid128("TrainerGUID");
            packet.ReadInt32("TrainerID");
            packet.ReadInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.CMSG_SPELL_CLICK)]
        public static void HandleSpellClick(Packet packet)
        {
            WowGuid guid = packet.ReadPackedGuid128("SpellClickUnitGUID");
            packet.ReadBit("TryAutoDismount");

            if (guid.GetObjectType() == ObjectType.Unit)
                Storage.NpcSpellClicks.Add(guid, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_BUY_BANK_SLOT)]
        public static void HandleBuyBankSlot(Packet packet)
        {
            packet.ReadPackedGuid128("Banker");
        }

        [Parser(Opcode.CMSG_CLOSE_INTERACTION)] // trigger in CGGameUI::CloseInteraction
        public static void HandleCloseInteraction(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_SUPPRESS_NPC_GREETINGS)]
        public static void HandleSuppressNPCGreetings(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadBit("SuppressNPCGreetings");
        }

        [Parser(Opcode.SMSG_TRAINER_BUY_FAILED)]
        public static void HandleTrainerBuyFailed(Packet packet)
        {
            packet.ReadPackedGuid128("TrainerGUID");
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadUInt32("TrainerFailedReason");
        }

        [Parser(Opcode.CMSG_SPIRIT_HEALER_ACTIVATE)]
        public static void HandleSpiritHealerActivate(Packet packet)
        {
            packet.ReadPackedGuid128("Healer");
        }

        [Parser(Opcode.SMSG_PLAYER_TABARD_VENDOR_ACTIVATE)]
        public static void HandleTabardVendorActivate(Packet packet)
        {
            packet.ReadPackedGuid128("Vendor");
        }
    }
}
