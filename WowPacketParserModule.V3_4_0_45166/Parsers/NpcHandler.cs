using System;
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

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class NpcHandler
    {
        public static GossipMessageOption ReadGossipOptionsData(uint menuId, WowGuid npcGuid, Packet packet, params object[] idx)
        {
            GossipMessageOption gossipMessageOption = new();
            GossipMenuOption gossipOption = new GossipMenuOption
            {
                MenuID = menuId
            };

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_4_1_47014))
                gossipOption.OptionID = gossipMessageOption.OptionIndex = (uint)packet.ReadInt32("OptionID", idx);
            else
                gossipOption.GossipOptionID = packet.ReadInt32("GossipOptionID", idx);
            gossipOption.OptionNpc = (GossipOptionNpc?)packet.ReadByte("OptionNPC", idx);
            gossipMessageOption.OptionNpc = (int)gossipOption.OptionNpc;
            gossipOption.BoxCoded = gossipMessageOption.BoxCoded = packet.ReadByte("OptionFlags", idx) != 0;
            gossipOption.BoxMoney = ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_4_59817)
                ? packet.ReadUInt64("OptionCost", idx)
                : packet.ReadUInt32("OptionCost", idx);
            gossipOption.Language = packet.ReadUInt32E<Language>("Language", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
            {
                gossipOption.Flags = packet.ReadInt32("Flags", idx);
                gossipOption.OptionID = gossipMessageOption.OptionIndex = (uint)packet.ReadInt32("OrderIndex", idx);
            }

            packet.ResetBitReader();
            uint textLen = packet.ReadBits(12);
            uint confirmLen = packet.ReadBits(12);
            bool hasSpellId = false;
            bool hasOverrideIconId = false;
            packet.ReadBits("Status", 2, idx);
            hasSpellId = packet.ReadBit();
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                hasOverrideIconId = packet.ReadBit();

            uint failureDescriptionLength = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_4_59817))
                failureDescriptionLength = packet.ReadBits(8);

            uint rewardsCount = packet.ReadUInt32();
            for (uint i = 0; i < rewardsCount; ++i)
            {
                packet.ResetBitReader();
                packet.ReadBits("Type", 1, idx, "TreasureItem", i);
                packet.ReadInt32("ID", idx, "TreasureItem", i);
                packet.ReadInt32("Quantity", idx, "TreasureItem", i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_4_59817))
                    packet.ReadByte("ItemContext", idx, "TreasureItem", i);
            }

            gossipOption.OptionText = gossipMessageOption.Text = packet.ReadWoWString("Text", textLen, idx);
            gossipMessageOption.BoxText = packet.ReadWoWString("Confirm", confirmLen, idx);

            if (!string.IsNullOrEmpty(gossipMessageOption.BoxText))
                gossipOption.BoxText = gossipMessageOption.BoxText;

            if (hasSpellId)
                gossipOption.SpellID = packet.ReadInt32("SpellID", idx);

            if (hasOverrideIconId)
                gossipOption.OverrideIconID = packet.ReadInt32("OverrideIconID", idx);

            if (failureDescriptionLength > 1)
                packet.ReadDynamicString("FailureDescription", failureDescriptionLength);

            gossipOption.FillBroadcastTextIDs();

            if (Settings.TargetedDatabase < TargetedDatabase.Shadowlands)
                gossipOption.FillOptionType(npcGuid);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                Storage.GossipOptionIdToOrderIndexMap.Add((gossipOption.MenuID.GetValueOrDefault(), gossipOption.GossipOptionID.GetValueOrDefault()), gossipOption.OptionID.GetValueOrDefault());
            Storage.GossipMenuOptions.Add((gossipOption.MenuID, gossipOption.OptionID), gossipOption, packet.TimeSpan);

            return gossipMessageOption;
        }

        public static GossipQuestOption ReadGossipQuestTextData(Packet packet, params object[] idx)
        {
            var gossipQuest = new GossipQuestOption();
            gossipQuest.QuestId = (uint)packet.ReadInt32("QuestID", idx);
            packet.ReadInt32("ContentTuningID", idx);
            packet.ReadInt32("QuestType", idx);
            packet.ReadInt32("QuestLevel", idx);
            packet.ReadInt32("QuestMaxScalingLevel", idx);

            for (int j = 0; j < 2; ++j)
                packet.ReadInt32("QuestFlags", idx, j);

            packet.ResetBitReader();

            packet.ReadBit("Repeatable", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51505)) // assumption
                packet.ReadBit("Important", idx);

            uint questTitleLen = packet.ReadBits(9);
            gossipQuest.Title = packet.ReadWoWString("QuestTitle", questTitleLen, idx);

            return gossipQuest;
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE, ClientVersionBuild.V3_4_0_44832, ClientVersionBuild.V3_4_4_59817)]
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
            CoreParsers.NpcHandler.AddGossipAddon(packetGossip.MenuId, friendshipFactionID, 0, guid, packet.TimeSpan);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_4_1_47014))
                gossip.TextID = packetGossip.TextId = (uint)packet.ReadInt32("TextID");

            int optionsCount = packet.ReadInt32("GossipOptionsCount");
            int questsCount = packet.ReadInt32("GossipQuestsCount");

            bool hasBroadcastTextID = false;
            bool hasBroadcastTextID2 = false;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
            {
                hasBroadcastTextID = packet.ReadBit("HasBroadcastTextID");
                hasBroadcastTextID2 = packet.ReadBit("HasBroadcastTextID2");
            }

            for (int i = 0; i < optionsCount; ++i)
                packetGossip.Options.Add(ReadGossipOptionsData((uint)menuId, guid, packet, i, "GossipOptions"));

            if (hasBroadcastTextID)
                gossip.TextID = (uint)packet.ReadInt32("BroadcastTextID");

            if (hasBroadcastTextID2)
                gossip.TextID = (uint)packet.ReadInt32("BroadcastTextID2");

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

        [Parser(Opcode.SMSG_VENDOR_INVENTORY, ClientVersionBuild.V3_4_3_51505, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleVendorInventory(Packet packet)
        {
            uint entry = packet.ReadPackedGuid128("VendorGUID").GetEntry();
            packet.ReadByte("Reason");
            int count = packet.ReadInt32("VendorItems");

            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt64("Price", i);

                NpcVendor vendor = new NpcVendor
                {
                    Entry = entry,
                    Slot = packet.ReadInt32("Muid", i),
                    Type = (uint)packet.ReadInt32("Type", i)
                };

                packet.ReadInt32("Durability", i);
                int buyCount = packet.ReadInt32("StackCount", i);
                int maxCount = packet.ReadInt32("Quantity", i);
                vendor.ExtendedCost = packet.ReadUInt32("ExtendedCostID", i);
                vendor.PlayerConditionID = packet.ReadUInt32("PlayerConditionFailed", i);
                packet.ReadBit("Locked", i);
                vendor.IgnoreFiltering = packet.ReadBit("DoNotFilterOnVendor", i);
                packet.ReadBit("Refundable", i);

                vendor.Item = Substructures.ItemHandler.ReadItemInstance(packet, i).ItemID;

                vendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                if (vendor.Type == 2)
                    vendor.MaxCount = (uint)buyCount;

                Storage.NpcVendors.Add(vendor, packet.TimeSpan);
            }

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
        }

        public static void AddBroadcastTextToGossip(uint menuID, uint broadcastTextID, WowGuid guid)
        {
            NpcText925 npcText = null;
            if (!Storage.GossipToNpcTextMap.TryGetValue(menuID, out npcText))
            {
                npcText = new NpcText925();
                npcText.ObjectType = guid.GetObjectType();
                npcText.ObjectEntry = guid.GetEntry();
                Storage.GossipToNpcTextMap.Add(menuID, npcText);
            }
            npcText.AddBroadcastTextIfNotExists(broadcastTextID, 1.0f);
        }

        public static GossipQuestOption ReadGossipQuestTextData344(Packet packet, params object[] idx)
        {
            var gossipQuest = new GossipQuestOption();
            gossipQuest.QuestId = (uint)packet.ReadInt32("QuestID", idx);
            packet.ReadInt32("ContentTuningID", idx);
            packet.ReadInt32("QuestType", idx);
            packet.ReadInt32("QuestLevel", idx);
            packet.ReadInt32("QuestMaxScalingLevel", idx);
            packet.ReadInt32("Unused1102", idx);
            packet.ReadInt32E<QuestFlags>("Flags", idx);
            packet.ReadInt32E<QuestFlagsEx>("FlagsEx", idx);
            packet.ReadInt32E<QuestFlagsEx2>("FlagsEx2", idx);

            packet.ResetBitReader();

            packet.ReadBit("Repeatable", idx);
            packet.ReadBit("ResetByScheduler", idx);
            packet.ReadBit("Important", idx);
            packet.ReadBit("Meta", idx);

            uint questTitleLen = packet.ReadBits(9);
            gossipQuest.Title = packet.ReadWoWString("QuestTitle", questTitleLen, idx);

            return gossipQuest;
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleNpcGossip344(Packet packet)
        {
            PacketGossipMessage packetGossip = packet.Holder.GossipMessage = new();

            WowGuid guid = packet.ReadPackedGuid128("GossipGUID");
            packetGossip.GossipSource = guid;

            int menuId = packet.ReadInt32("GossipID");
            packetGossip.MenuId = (uint)menuId;
            packet.ReadInt32("LfgDungeonsID");

            int friendshipFactionID = packet.ReadInt32("FriendshipFactionID");
            CoreParsers.NpcHandler.AddGossipAddon(packetGossip.MenuId, friendshipFactionID, 0, guid, packet.TimeSpan);

            uint broadcastTextID = 0;
            uint npcTextID = 0;
            int optionsCount = packet.ReadInt32("GossipOptionsCount");
            int questsCount = packet.ReadInt32("GossipQuestsCount");

            bool hasTextID = false;
            bool hasBroadcastTextID = false;
            hasTextID = packet.ReadBit("HasTextID");
            hasBroadcastTextID = packet.ReadBit("HasBroadcastTextID");

            for (int i = 0; i < optionsCount; ++i)
                packetGossip.Options.Add(ReadGossipOptionsData((uint)menuId, guid, packet, i, "GossipOptions"));

            if (hasTextID)
                npcTextID = (uint)packet.ReadInt32("TextID");

            if (hasBroadcastTextID)
                broadcastTextID = (uint)packet.ReadInt32("BroadcastTextID");

            if (!hasTextID && hasBroadcastTextID)
                npcTextID = SQLDatabase.GetNPCTextIDByMenuIDAndBroadcastText(menuId, broadcastTextID);

            if (npcTextID != 0)
            {
                GossipMenu gossip = new();
                gossip.MenuID = packetGossip.MenuId;
                gossip.TextID = packetGossip.TextId = npcTextID;
                gossip.ObjectType = guid.GetObjectType();
                gossip.ObjectEntry = guid.GetEntry();
                Storage.Gossips.Add(gossip, packet.TimeSpan);
            }
            else if (hasBroadcastTextID)
                AddBroadcastTextToGossip(packetGossip.MenuId, broadcastTextID, guid);

            for (int i = 0; i < questsCount; ++i)
                packetGossip.Quests.Add(ReadGossipQuestTextData344(packet, i, "GossipQuests"));

            if (guid.GetObjectType() == ObjectType.Unit && !CoreParsers.NpcHandler.HasLastGossipOption(packet.TimeSpan, (uint)menuId))
            {
                CreatureTemplateGossip creatureTemplateGossip = new()
                {
                    CreatureID = guid.GetEntry(),
                    MenuID = (uint)menuId
                };
                Storage.CreatureTemplateGossips.Add(creatureTemplateGossip);
                Storage.CreatureDefaultGossips.Add(guid.GetEntry(), (uint)menuId);
            }

            CoreParsers.NpcHandler.UpdateLastGossipOptionActionMessage(packet.TimeSpan, (uint)menuId);

            packet.AddSniffData(StoreNameType.Gossip, menuId, guid.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            PacketGossipSelect packetGossip = packet.Holder.GossipSelect = new();
            packetGossip.GossipUnit = packet.ReadPackedGuid128("GossipUnit");

            var menuID = packetGossip.MenuId = packet.ReadUInt32("MenuID");
            uint optionID = 0;
            var gossipOptionId = packet.ReadInt32("GossipOptionID");
            Storage.GossipOptionIdToOrderIndexMap.TryGetValue((menuID, gossipOptionId), out optionID);
            packetGossip.OptionId = optionID;

            var bits8 = packet.ReadBits(8);
            packet.ResetBitReader();
            packet.ReadWoWString("PromotionCode", bits8);

            CoreParsers.NpcHandler.LastGossipOption.GossipSelectOption(menuID, optionID, packet.TimeSpan);
            CoreParsers.NpcHandler.TempGossipOptionPOI.GossipSelectOption(menuID, optionID, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_GOSSIP_COMPLETE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleGossipComplete(Packet packet)
        {
            packet.ReadBit("SuppressSound");
        }

        [Parser(Opcode.CMSG_CLOSE_INTERACTION)] // trigger in CGGameUI::CloseInteraction
        public static void HandleCloseInteraction(Packet packet)
        {
            var packetGossip = packet.Holder.GossipClose = new PacketGossipClose();
            CoreParsers.NpcHandler.LastGossipOption.Reset();
            packetGossip.GossipSource = packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_TRAINER_LIST, ClientVersionBuild.V3_4_4_59817)]
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
            for (var i = 0; i < count; ++i)
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

                packet.ReadInt32("Unk440", i);
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

        [Parser(Opcode.SMSG_VENDOR_INVENTORY, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleVendorInventory344(Packet packet)
        {
            uint entry = packet.ReadPackedGuid128("VendorGUID").GetEntry();
            packet.ReadInt32("Reason");

            int count = packet.ReadInt32("VendorItems");

            for (int i = 0; i < count; ++i)
            {
                NpcVendor vendor = new NpcVendor
                {
                    Entry = entry,
                };

                packet.ReadInt64("Price", i);
                vendor.Slot = packet.ReadInt32("Muid", i);
                vendor.Type = (uint)packet.ReadInt32("Type", i);
                int buyCount = packet.ReadInt32("StackCount", i);
                int maxCount = packet.ReadInt32("Quantity", i);
                vendor.ExtendedCost = packet.ReadUInt32("ExtendedCostID", i);
                vendor.PlayerConditionID = packet.ReadUInt32("PlayerConditionFailed", i);

                packet.ResetBitReader();
                packet.ReadBit("Locked", i);
                vendor.IgnoreFiltering = packet.ReadBit("DoNotFilterOnVendor", i);
                packet.ReadBit("Refundable", i);

                packet.ResetBitReader();
                vendor.Item = Substructures.ItemHandler.ReadItemInstance(packet, i).ItemID;

                vendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                if (vendor.Type == 2)
                    vendor.MaxCount = (uint)buyCount;

                Storage.NpcVendors.Add(vendor, packet.TimeSpan);
            }

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
        }

        [Parser(Opcode.CMSG_BINDER_ACTIVATE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BINDER_CONFIRM, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_TALK_TO_GOSSIP, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_LIST_INVENTORY, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_TRAINER_LIST, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleNpcHello(Packet packet)
        {
            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
            var guid = CoreParsers.NpcHandler.LastGossipOption.Guid = packet.ReadPackedGuid128("Guid");

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.CMSG_TALK_TO_GOSSIP, Direction.ClientToServer))
                packet.Holder.GossipHello = new PacketGossipHello { GossipSource = guid };
        }

        [Parser(Opcode.CMSG_BANKER_ACTIVATE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBankerActivate(Packet packet)
        {
            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
            var guid = CoreParsers.NpcHandler.LastGossipOption.Guid = packet.ReadPackedGuid128("Guid");
            packet.ReadInt32E<PlayerInteractionType>("InteractionType");

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.CMSG_TALK_TO_GOSSIP, Direction.ClientToServer))
                packet.Holder.GossipHello = new PacketGossipHello { GossipSource = guid };
        }

        [Parser(Opcode.SMSG_GOSSIP_OPTION_NPC_INTERACTION, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleGossipOptionNPCInteraction(Packet packet)
        {
            packet.ReadPackedGuid128("GossipGUID");
            var gossipNpcOptionID = packet.ReadInt32("GossipNpcOptionID");
            var hasFriendshipFactionID = packet.ReadBit();

            if (hasFriendshipFactionID)
                packet.ReadInt32("FriendshipFactionID");

            CoreParsers.NpcHandler.AddGossipNpcOption(gossipNpcOptionID, packet.TimeSpan, true);
        }

        [Parser(Opcode.SMSG_GOSSIP_POI, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleGossipPoi(Packet packet)
        {
            var protoPoi = packet.Holder.GossipPoi = new();
            PointsOfInterest gossipPOI = new PointsOfInterest();

            gossipPOI.ID = protoPoi.Id = packet.ReadInt32("ID");
            gossipPOI.Flags = protoPoi.Flags = (uint)packet.ReadInt32("Flags");
            Vector3 pos = packet.ReadVector3("Coordinates");
            gossipPOI.PositionX = pos.X;
            gossipPOI.PositionY = pos.Y;
            gossipPOI.PositionZ = pos.Z;
            protoPoi.Coordinates = new Vec2() { X = pos.X, Y = pos.Y };
            protoPoi.Height = pos.Z;

            gossipPOI.Icon = packet.ReadInt32E<GossipPOIIcon>("Icon");
            gossipPOI.Importance = protoPoi.Importance = (uint)packet.ReadInt32("Importance");
            protoPoi.Icon = (uint)gossipPOI.Icon;
            gossipPOI.WMOGroupID = packet.ReadInt32("WMOGroupID");

            packet.ResetBitReader();
            uint bit84 = packet.ReadBits(6);
            gossipPOI.Name = protoPoi.Name = packet.ReadWoWString("Name", bit84);

            Storage.GossipPOIs.Add(gossipPOI, packet.TimeSpan);
            CoreParsers.NpcHandler.UpdateTempGossipOptionActionPOI(packet.TimeSpan, gossipPOI.ID);
        }

        [Parser(Opcode.SMSG_NPC_INTERACTION_OPEN_RESULT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleNpcInteractionOpenResult(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadInt32("InteractionType");
            packet.ReadBit("Success");
        }

        [Parser(Opcode.SMSG_TRAINER_BUY_FAILED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleTrainerBuyFailed(Packet packet)
        {
            packet.ReadPackedGuid128("TrainerGUID");
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadUInt32("TrainerFailedReason");
        }

        [Parser(Opcode.CMSG_BUY_BANK_SLOT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBuyBankSlot(Packet packet)
        {
            packet.ReadPackedGuid128("Banker");
        }

        [Parser(Opcode.CMSG_SPELL_CLICK, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSpellClick(Packet packet)
        {
            WowGuid guid = packet.ReadPackedGuid128("SpellClickUnitGUID");
            packet.Holder.SpellClick = new() { Target = guid };
            packet.ReadBit("TryAutoDismount");

            if (guid.GetObjectType() == ObjectType.Unit)
                Storage.NpcSpellClicks.Add(guid, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_SPIRIT_HEALER_ACTIVATE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSpiritHealerActivate(Packet packet)
        {
            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
            CoreParsers.NpcHandler.LastGossipOption.Guid = packet.ReadPackedGuid128("Healer");
        }

        [Parser(Opcode.CMSG_TABARD_VENDOR_ACTIVATE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleTabardVendorActivate(Packet packet)
        {
            packet.ReadPackedGuid128("Vendor");
            packet.ReadInt32("Type");
        }

        [Parser(Opcode.CMSG_TRAINER_BUY_SPELL, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleTrainerBuySpell(Packet packet)
        {
            packet.ReadPackedGuid128("TrainerGUID");
            packet.ReadInt32("TrainerID");
            packet.ReadInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.SMSG_GOSSIP_QUEST_UPDATE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleGossipQuestUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("GossipGUID");
            ReadGossipQuestTextData344(packet);
        }
    }
}
