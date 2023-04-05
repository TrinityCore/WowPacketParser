using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using WowPacketParser.DBC;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.SQL;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class NpcHandler
    {
        public static uint LastGossipPOIEntry;

        public static GossipMessageOption ReadGossipOptionsData(uint menuId, WowGuid npcGuid, Packet packet, params object[] idx)
        {
            GossipMessageOption gossipMessageOption = new();
            GossipMenuOption gossipOption = new GossipMenuOption
            {
                MenuID = menuId
            };

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V10_0_0_46181))
                gossipOption.OptionID = gossipMessageOption.OptionIndex = (uint)packet.ReadInt32("OptionID", idx);
            else
                gossipOption.GossipOptionID = packet.ReadInt32("GossipOptionID", idx);

            gossipOption.OptionNpc = (GossipOptionNpc?)packet.ReadByte("OptionNPC", idx);
            gossipMessageOption.OptionNpc = (int) gossipOption.OptionNpc;
            gossipOption.BoxCoded = gossipMessageOption.BoxCoded = packet.ReadByte("OptionFlags", idx) != 0;
            gossipOption.BoxMoney = gossipMessageOption.BoxCost = (uint)packet.ReadInt32("OptionCost", idx);
            if (ClientVersion.AddedInVersion(ClientBranch.Retail, ClientVersionBuild.V9_2_0_42423) ||
                ClientVersion.AddedInVersion(ClientBranch.Classic, ClientVersionBuild.V1_14_1_40666) ||
                ClientVersion.AddedInVersion(ClientBranch.TBC, ClientVersionBuild.V2_5_3_41812) ||
                ClientVersion.AddedInVersion(ClientBranch.WotLK, ClientVersionBuild.V3_4_0_45166))
                gossipOption.Language = packet.ReadUInt32E<Language>("Language", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_0_46181))
            {
                gossipOption.Flags = packet.ReadInt32("Flags", idx);
                gossipOption.OptionID = gossipMessageOption.OptionIndex = (uint)packet.ReadInt32("OrderIndex", idx);
            }

            packet.ResetBitReader();
            uint textLen = packet.ReadBits(12);
            uint confirmLen = packet.ReadBits(12);
            bool hasSpellId = false;
            bool hasOverrideIconId = false;
            if (ClientVersion.AddedInVersion(ClientType.Shadowlands) || ClientVersion.IsWotLKClientVersionBuild(ClientVersion.Build))
            {
                packet.ReadBits("Status", 2, idx);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_2_36639) || ClientVersion.IsWotLKClientVersionBuild(ClientVersion.Build))
                    hasSpellId = packet.ReadBit();
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_0_46181))
                    hasOverrideIconId = packet.ReadBit();

                uint rewardsCount = packet.ReadUInt32();
                for (uint i = 0; i < rewardsCount; ++i)
                {
                    packet.ResetBitReader();
                    packet.ReadBits("Type", 1, idx, "TreasureItem", i);
                    packet.ReadInt32("ID", idx, "TreasureItem", i);
                    packet.ReadInt32("Quantity", idx, "TreasureItem", i);
                }
            }

            gossipOption.OptionText = gossipMessageOption.Text = packet.ReadWoWString("Text", textLen, idx);
            gossipMessageOption.BoxText = packet.ReadWoWString("Confirm", confirmLen, idx);

            if (!string.IsNullOrEmpty(gossipMessageOption.BoxText))
                gossipOption.BoxText = gossipMessageOption.BoxText;

            if (hasSpellId)
                gossipOption.SpellID = packet.ReadInt32("SpellID", idx);

            if (hasOverrideIconId)
                gossipOption.OverrideIconID = packet.ReadInt32("OverrideIconID", idx);

            gossipOption.FillBroadcastTextIDs();

            if (Settings.TargetedDatabase < TargetedDatabase.Shadowlands)
                gossipOption.FillOptionType(npcGuid);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_0_46181))
                Storage.GossipOptionIdToOrderIndexMap.Add((gossipOption.MenuID.GetValueOrDefault(), gossipOption.GossipOptionID.GetValueOrDefault()), gossipOption.OptionID.GetValueOrDefault());
            Storage.GossipMenuOptions.Add((gossipOption.MenuID, gossipOption.OptionID), gossipOption, packet.TimeSpan);

            return gossipMessageOption;
        }

        public static GossipQuestOption ReadGossipQuestTextData(Packet packet, params object[] idx)
        {
            var gossipQuest = new GossipQuestOption();
            gossipQuest.QuestId = (uint)packet.ReadInt32("QuestID", idx);
            packet.ReadInt32("QuestType", idx);
            packet.ReadInt32("QuestLevel", idx);

            for (int j = 0; j < 2; ++j)
                packet.ReadInt32("QuestFlags", idx, j);

            packet.ResetBitReader();

            packet.ReadBit("Repeatable", idx);

            uint questTitleLen = packet.ReadBits(9);

            gossipQuest.Title = packet.ReadWoWString("QuestTitle", questTitleLen, idx);

            return gossipQuest;
        }

        [Parser(Opcode.CMSG_BANKER_ACTIVATE)]
        [Parser(Opcode.CMSG_BINDER_ACTIVATE)]
        [Parser(Opcode.SMSG_BINDER_CONFIRM)]
        [Parser(Opcode.CMSG_TALK_TO_GOSSIP)]
        [Parser(Opcode.CMSG_LIST_INVENTORY)]
        [Parser(Opcode.CMSG_TRAINER_LIST)]
        public static void HandleNpcHello(Packet packet)
        {
            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
            var guid = CoreParsers.NpcHandler.LastGossipOption.Guid = packet.ReadPackedGuid128("Guid");

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.CMSG_TALK_TO_GOSSIP, Direction.ClientToServer))
                packet.Holder.GossipHello = new PacketGossipHello { GossipSource = guid };
        }

        [Parser(Opcode.SMSG_SHOW_BANK)]
        public static void HandleShowBank(Packet packet)
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
            var proto = packet.Holder.NpcText = new() { Entry = npcText.ID.Value };
            for (int i = 0; i < 8; ++i)
                proto.Texts.Add(new PacketNpcTextEntry(){Probability = npcText.Probabilities[i], BroadcastTextId = npcText.BroadcastTextId[i]});
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            PacketGossipSelect packetGossip = packet.Holder.GossipSelect = new();
            packetGossip.GossipUnit = packet.ReadPackedGuid128("GossipUnit");

            var menuID = packetGossip.MenuId = packet.ReadUInt32("MenuID");
            uint optionID = 0;
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V10_0_0_46181))
                optionID = packetGossip.OptionId = packet.ReadUInt32("OptionID");
            else
            {
                var gossipOptionId = packet.ReadInt32("GossipOptionID");
                Storage.GossipOptionIdToOrderIndexMap.TryGetValue((menuID, gossipOptionId), out optionID);
                packetGossip.OptionId = optionID;
            }

            var bits8 = packet.ReadBits(8);
            packet.ResetBitReader();
            packet.ReadWoWString("PromotionCode", bits8);

            CoreParsers.NpcHandler.LastGossipOption.GossipSelectOption(menuID, optionID, packet.TimeSpan);
            CoreParsers.NpcHandler.TempGossipOptionPOI.GossipSelectOption(menuID, optionID, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_GOSSIP_POI)]
        public static void HandleGossipPoi(Packet packet)
        {
            var protoPoi = packet.Holder.GossipPoi = new();
            PointsOfInterest gossipPOI = new PointsOfInterest();

            gossipPOI.Flags = protoPoi.Flags = packet.ReadBits("Flags", 14);
            uint bit84 = packet.ReadBits(6);

            Vector2 pos = packet.ReadVector2("Coordinates");
            protoPoi.Coordinates = pos;
            gossipPOI.PositionX = pos.X;
            gossipPOI.PositionY = pos.Y;

            gossipPOI.Icon = packet.ReadUInt32E<GossipPOIIcon>("Icon");
            gossipPOI.Importance = protoPoi.Importance = packet.ReadUInt32("Importance");
            gossipPOI.Name = protoPoi.Name = packet.ReadWoWString("Name", bit84);
            protoPoi.Icon = (uint)gossipPOI.Icon;

            // DB PART STARTS HERE
            if (Settings.DBEnabled)
            {
                foreach (var poi in SQLDatabase.POIs)
                {
                    if (gossipPOI.Name == poi.Name && (uint)gossipPOI.Icon == poi.Icon)
                    {
                        if (Math.Abs(pos.X - poi.PositionX) <= 0.99f && Math.Abs(pos.Y - poi.PositionY) <= 0.99f)
                        {
                            gossipPOI.ID = poi.ID;
                            break;
                        }
                    }
                }

                if (gossipPOI.ID == null)
                {
                    gossipPOI.ID = (SQLDatabase.POIs[SQLDatabase.POIs.Count - 1].ID + 1);

                    // Add to list to prevent double data while parsing
                    var poiData = new SQLDatabase.POIData()
                    {
                        ID = (uint)gossipPOI.ID,
                        PositionX = (float)gossipPOI.PositionX,
                        PositionY = (float)gossipPOI.PositionY,
                        Icon = (uint)gossipPOI.Icon,
                        Flags = (uint)gossipPOI.Flags,
                        Importance = (uint)gossipPOI.Importance,
                        Name = gossipPOI.Name
                    };

                    SQLDatabase.POIs.Add(poiData);
                }
            }
            else
            {
                gossipPOI.ID = "@PID+" + LastGossipPOIEntry.ToString();
                ++LastGossipPOIEntry;
            }

            Storage.GossipPOIs.Add(gossipPOI, packet.TimeSpan);
            CoreParsers.NpcHandler.UpdateTempGossipOptionActionPOI(packet.TimeSpan, gossipPOI.ID);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            PacketGossipMessage packetGossip = packet.Holder.GossipMessage = new();
            GossipMenu gossip = new GossipMenu();

            WowGuid guid = packet.ReadPackedGuid128("GossipGUID");
            packetGossip.GossipSource = guid;

            gossip.ObjectType = guid.GetObjectType();
            gossip.ObjectEntry = guid.GetEntry();

            int menuId = packet.ReadInt32("GossipID");
            gossip.MenuID = packetGossip.MenuId = (uint)menuId;

            int friendshipFactionID = packet.ReadInt32("FriendshipFactionID");
            CoreParsers.NpcHandler.AddGossipAddon(packetGossip.MenuId, friendshipFactionID, guid, packet.TimeSpan);

            gossip.TextID = packetGossip.TextId = (uint)packet.ReadInt32("TextID");

            int optionsCount = packet.ReadInt32("GossipOptionsCount");
            int questsCount = packet.ReadInt32("GossipQuestsCount");

            for (int i = 0; i < optionsCount; ++i)
                packetGossip.Options.Add(ReadGossipOptionsData((uint)menuId, guid, packet, i, "GossipOptions"));

            for (int i = 0; i < questsCount; ++i)
                packetGossip.Quests.Add(ReadGossipQuestTextData(packet, i, "GossipQuests"));

            if (guid.GetObjectType() == ObjectType.Unit)
            {
                CreatureTemplateGossip creatureTemplateGossip = new()
                {
                    CreatureID = guid.GetEntry(),
                    MenuID = (uint)menuId
                };
                Storage.CreatureTemplateGossips.Add(creatureTemplateGossip);
                Storage.CreatureDefaultGossips.Add(guid.GetEntry(), (uint)menuId);
            }

            Storage.Gossips.Add(gossip, packet.TimeSpan);
            CoreParsers.NpcHandler.UpdateLastGossipOptionActionMessage(packet.TimeSpan, gossip.MenuID);

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
                    Item = Substructures.ItemHandler.ReadItemInstance(packet, i).ItemID
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

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            Trainer trainer = new Trainer();

            WowGuid guid = packet.ReadPackedGuid128("TrainerGUID");
            bool hasFaction = false;
            float discount = 1.0f;

            if (Settings.UseDBC && Settings.RecalcDiscount)
                if (Storage.Objects != null && Storage.Objects.ContainsKey(guid))
                {
                    WoWObject obj = Storage.Objects[guid].Item1;
                    if (obj.Type == ObjectType.Unit)
                    {
                        int factionTemplateId = (obj as Unit).UnitData.FactionTemplate ?? 0;
                        int faction = 0;

                        if (factionTemplateId != 0 && DBC.FactionTemplate.ContainsKey(factionTemplateId))
                            faction = DBC.FactionTemplate[factionTemplateId].Faction;

                        ulong reputation = 0;

                        if (CoreParsers.AchievementHandler.FactionReputationStore.ContainsKey(faction))
                        {
                            reputation = CoreParsers.AchievementHandler.FactionReputationStore[faction];
                            hasFaction = true;
                        }

                        uint multiplier = 0;

                        if (reputation >= 3000) // Friendly
                            multiplier = 1;
                        if (reputation >= 9000) // Honored
                            multiplier = 2;
                        if (reputation >= 21000) // Revered
                            multiplier = 3;
                        if (reputation >= 42000) // Exalted
                            multiplier = 4;

                        if (multiplier != 0)
                            discount = 1.0f - 0.05f * multiplier;

                        packet.WriteLine("ReputationDiscount: {0}%", (int)((discount * 100) - 100));
                    }
                }

            trainer.Type = packet.ReadInt32E<TrainerType>("TrainerType");
            trainer.Id = packet.ReadUInt32("TrainerID");

            var count = packet.ReadUInt32("Spells");

            for (var i = 0u; i < count; ++i)
            {
                TrainerSpell trainerSpell = new TrainerSpell
                {
                    TrainerId = trainer.Id,
                    SpellId = packet.ReadUInt32<SpellId>("SpellID", i)
                };

                uint moneyCost = packet.ReadUInt32("MoneyCost", i);
                uint moneyCostOriginal = moneyCost;

                if (Settings.UseDBC && Settings.RecalcDiscount && hasFaction)
                {
                    moneyCostOriginal = (uint)(Math.Round((moneyCost / discount) / 5)) * 5;
                    packet.WriteLine("[{0}] MoneyCostOriginal: {1}", i, moneyCostOriginal);
                    trainerSpell.FactionHelper = "MoneyCost recalculated";
                }
                else
                {
                    trainerSpell.FactionHelper = "No Faction found! MoneyCost not recalculated!";
                }

                trainerSpell.MoneyCost = moneyCostOriginal;
                trainerSpell.ReqSkillLine = packet.ReadUInt32("ReqSkillLine", i);
                trainerSpell.ReqSkillRank = packet.ReadUInt32("ReqSkillRank", i);

                trainerSpell.ReqAbility = new uint[3];
                for (var j = 0; j < 3; ++j)
                    trainerSpell.ReqAbility[j] = packet.ReadUInt32("ReqAbility", i, j);

                packet.ReadByteE<TrainerSpellState>("Usable", i);
                trainerSpell.ReqLevel = packet.ReadByte("ReqLevel", i);

                Storage.TrainerSpells.Add(trainerSpell, packet.TimeSpan);
            }
            packet.ResetBitReader();
            uint greetingLength = packet.ReadBits(11);
            trainer.Greeting = packet.ReadWoWString("Greeting", greetingLength);

            Storage.Trainers.Add(trainer, packet.TimeSpan);
            CoreParsers.NpcHandler.AddToCreatureTrainers(trainer.Id, packet.TimeSpan);
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
            packet.Holder.SpellClick = new() { Target = guid };
            packet.ReadBit("TryAutoDismount");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479) && ClientVersion.RemovedInVersion(ClientVersionBuild.V10_0_7_48676))
                packet.ReadBit("IsSoftInteract");

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
            var packetGossip = packet.Holder.GossipClose = new PacketGossipClose();
            CoreParsers.NpcHandler.LastGossipOption.Reset();
            packetGossip.GossipSource = packet.ReadPackedGuid128("Guid");
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
            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
            CoreParsers.NpcHandler.LastGossipOption.Guid = packet.ReadPackedGuid128("Healer");
        }

        [Parser(Opcode.SMSG_PLAYER_TABARD_VENDOR_ACTIVATE)]
        public static void HandleTabardVendorActivate(Packet packet)
        {
            packet.ReadPackedGuid128("Vendor");
        }
    }
}
