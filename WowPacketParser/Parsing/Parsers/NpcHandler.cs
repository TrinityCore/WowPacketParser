using System;
using System.Collections.Generic;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Proto;
using WowPacketParser.SQL;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Parsing.Parsers
{
    public class GossipOptionSelection
    {
        public WowGuid Guid { get; set; }
        public uint? MenuId { get; set; }
        public uint? OptionIndex { get; set; }
        public uint? ActionMenuId { get; set; }
        public object ActionPoiId { get; set; }
        public TimeSpan TimeSpan { get; set; }

        public bool HasSelection { get { return MenuId.HasValue && OptionIndex.HasValue; } }

        public void GossipSelectOption(uint? menuId, uint? optionId, TimeSpan timespan)
        {
            MenuId = menuId;
            OptionIndex = optionId;
            ActionMenuId = null;
            ActionPoiId = null;
            TimeSpan = timespan;
        }

        public void Reset()
        {
            Guid = null;
            MenuId = null;
            OptionIndex = null;
            ActionMenuId = null;
            ActionPoiId = null;
            TimeSpan = TimeSpan.Zero;
        }
    }

    public static class NpcHandler
    {
        public static uint LastGossipPOIEntry;
        public static GossipOptionSelection LastGossipOption = new GossipOptionSelection();
        public static GossipOptionSelection TempGossipOptionPOI = new GossipOptionSelection();

        public static void AddGossipNpcOption(int gossipNpcOptionId, TimeSpan timeSpan, bool checkDelay = false)
        {
            if (LastGossipOption.HasSelection)
                if (!checkDelay || (timeSpan - LastGossipOption.TimeSpan).Duration() <= TimeSpan.FromMilliseconds(2500))
                    Storage.GossipMenuOptions[(LastGossipOption.MenuId, LastGossipOption.OptionIndex)].Item1.GossipNpcOptionID = gossipNpcOptionId;

            LastGossipOption.Reset();
            TempGossipOptionPOI.Reset();
        }

        public static void AddToCreatureTrainers(uint? trainerId, TimeSpan timeSpan, bool checkDelay = false)
        {
            if (LastGossipOption.HasSelection)
            {
                if (!checkDelay || (timeSpan - LastGossipOption.TimeSpan).Duration() <= TimeSpan.FromMilliseconds(2500))
                    Storage.CreatureTrainers.Add(new CreatureTrainer { CreatureID = LastGossipOption.Guid.GetEntry(), MenuID = LastGossipOption.MenuId, OptionID = LastGossipOption.OptionIndex, TrainerID = trainerId }, timeSpan);
            }
            else
                Storage.CreatureTrainers.Add(new CreatureTrainer { CreatureID = LastGossipOption.Guid.GetEntry(), MenuID = 0, OptionID = 0, TrainerID = trainerId }, timeSpan);

            LastGossipOption.Reset();
            TempGossipOptionPOI.Reset();
        }

        public static void UpdateTempGossipOptionActionPOI(TimeSpan timeSpan, object gossipPOIID)
        {
            LastGossipOption.ActionPoiId = gossipPOIID;
            TempGossipOptionPOI.ActionPoiId = gossipPOIID;

            if (!TempGossipOptionPOI.HasSelection)
                return;

            if ((timeSpan - TempGossipOptionPOI.TimeSpan).Duration() <= TimeSpan.FromMilliseconds(2500))
            {
                Storage.GossipMenuOptions[(TempGossipOptionPOI.MenuId, TempGossipOptionPOI.OptionIndex)].Item1.ActionMenuID = TempGossipOptionPOI.ActionMenuId.GetValueOrDefault(0);
                Storage.GossipMenuOptions[(TempGossipOptionPOI.MenuId, TempGossipOptionPOI.OptionIndex)].Item1.ActionPoiID = gossipPOIID;

                //clear temp
                TempGossipOptionPOI.Reset();
            }
            else
            {
                LastGossipOption.Reset();
                TempGossipOptionPOI.Reset();
            }
        }

        public static void UpdateLastGossipOptionActionMessage(TimeSpan timeSpan, uint? menuId)
        {
            if (!LastGossipOption.HasSelection)
                return;

            if ((timeSpan - LastGossipOption.TimeSpan).Duration() <= TimeSpan.FromMilliseconds(2500))
            {
                Storage.GossipMenuOptions[(LastGossipOption.MenuId, LastGossipOption.OptionIndex)].Item1.ActionMenuID = menuId;
                Storage.GossipMenuOptions[(LastGossipOption.MenuId, LastGossipOption.OptionIndex)].Item1.ActionPoiID = LastGossipOption.ActionPoiId ?? 0;

                //keep temp data (for case SMSG_GOSSIP_POI is delayed)
                TempGossipOptionPOI.Guid = LastGossipOption.Guid;
                TempGossipOptionPOI.MenuId = LastGossipOption.MenuId;
                TempGossipOptionPOI.OptionIndex = LastGossipOption.OptionIndex;
                TempGossipOptionPOI.ActionMenuId = menuId;
                TempGossipOptionPOI.TimeSpan = LastGossipOption.TimeSpan;

                // clear lastgossip so no faulty linkings appear
                LastGossipOption.Reset();
            }
            else
            {
                LastGossipOption.Reset();
                TempGossipOptionPOI.Reset();
            }
        }

        public static void AddGossipAddon(uint menuID, int friendshipFactionID, int lfgDungeonsID, WowGuid guid, TimeSpan timeSpan)
        {
            if (friendshipFactionID <= 0 && lfgDungeonsID <= 0)
                return;

            GossipMenuAddon gossipMenuAddon = new();
            gossipMenuAddon.MenuID = menuID;
            gossipMenuAddon.FriendshipFactionID = friendshipFactionID;
            gossipMenuAddon.LfgDungeonsID = lfgDungeonsID;
            gossipMenuAddon.ObjectType = guid.GetObjectType();
            gossipMenuAddon.ObjectEntry = guid.GetEntry();
            Storage.GossipMenuAddons.Add(gossipMenuAddon, timeSpan);
        }

        public static void AddGossipOptionAddon(int? garrTalentTreeID, TimeSpan timeSpan, bool checkDelay = false)
        {
            if (garrTalentTreeID != 0)
                if (LastGossipOption.HasSelection)
                    if (!checkDelay || (timeSpan - LastGossipOption.TimeSpan).Duration() <= TimeSpan.FromMilliseconds(2500))
                        Storage.GossipMenuOptionAddons.Add(new GossipMenuOptionAddon { MenuID = LastGossipOption.MenuId, OptionID = LastGossipOption.OptionIndex, GarrTalentTreeID = garrTalentTreeID }, timeSpan);

            LastGossipOption.Reset();
            TempGossipOptionPOI.Reset();
        }

        [Parser(Opcode.SMSG_GOSSIP_POI)]
        public static void HandleGossipPoi(Packet packet)
        {
            var protoPoi = packet.Holder.GossipPoi = new();
            PointsOfInterest gossipPOI = new PointsOfInterest();

            gossipPOI.Flags = protoPoi.Flags = (uint)packet.ReadInt32E<UnknownFlags>("Flags");
            Vector2 pos = packet.ReadVector2("Coordinates");
            protoPoi.Coordinates = pos;
            gossipPOI.PositionX = pos.X;
            gossipPOI.PositionY = pos.Y;

            gossipPOI.Icon = packet.ReadUInt32E<GossipPOIIcon>("Icon");
            gossipPOI.Importance = protoPoi.Importance = packet.ReadUInt32("Importance");
            gossipPOI.Name = protoPoi.Name = packet.ReadCString("Name");
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
            UpdateTempGossipOptionActionPOI(packet.TimeSpan, gossipPOI.ID);
        }

        [Parser(Opcode.CMSG_TRAINER_BUY_SPELL, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        [Parser(Opcode.SMSG_TRAINER_BUY_FAILED, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_TRAINER_BUY_RESULT)]
        public static void HandleServerTrainerBuy(Packet packet)
        {
            packet.ReadGuid("GUID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_3_13329))
                packet.ReadInt32("Unk");
            packet.ReadInt32<SpellId>("Spell ID");
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_TRAINER_BUY_FAILED, Direction.ServerToClient)
                || packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_TRAINER_BUY_RESULT, Direction.ServerToClient))
                packet.ReadUInt32("Reason");
        }

        [Parser(Opcode.SMSG_TRAINER_BUY_FAILED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTrainerBuyFailed434(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadInt32<SpellId>("Spell ID");
            packet.ReadUInt32("Reason");
        }

        // Might be a completely different opcode on 4.2.2 (trainer related)
        // Subv says it is SMSG_TRAINER_REPORT_ERROR_IN_CONSOLE but I think he is trolling me.
        [Parser(Opcode.SMSG_TRAINER_BUY_SUCCEEDED)]
        public static void HandleServerTrainerBuySucceedeed(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadInt32<SpellId>("Spell ID");
            if (ClientVersion.Build == ClientVersionBuild.V4_2_2_14545)
                packet.ReadInt32("Trainer Service"); // <TS>

            /* Comments about TS:
             * if !TS, "Trainer service <TS> unavailable"
             * if TS == 1, "Not enough money for trainer service <TS>"
             * Anyway... could only find 0s (and one 1)
             * */
        }

        [Parser(Opcode.CMSG_TRAINER_BUY_SPELL, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleTrainerBuySpell(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadInt32("TrainerID");
            packet.ReadInt32<SpellId>("Spell ID");
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            Trainer trainer = new Trainer();
            uint trainerId = 0;

            WowGuid guid = packet.ReadGuid("GUID");
            uint entry = guid.GetEntry();

            trainer.Type = packet.ReadInt32E<TrainerType>("TrainerType");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                trainerId = packet.ReadUInt32("TrainerID");

            int count = packet.ReadInt32("Spells");
            for (int i = 0; i < count; ++i)
            {
                NpcTrainer npcTrainer = new NpcTrainer
                {
                    ID = entry,
                };

                TrainerSpell trainerSpell = new TrainerSpell
                {
                    TrainerId = trainerId,
                };

                int spellId = packet.ReadInt32<SpellId>("SpellID", i);
                npcTrainer.SpellID = spellId;
                trainerSpell.SpellId = (uint)spellId;

                packet.ReadByteE<TrainerSpellState>("State", i);

                uint moneyCost = packet.ReadUInt32("MoneyCost", i);
                npcTrainer.MoneyCost = moneyCost;
                trainerSpell.MoneyCost = moneyCost;

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                {
                    byte reqLevel = packet.ReadByte("Required Level", i);
                    uint reqSkillLine = packet.ReadUInt32("Required Skill", i);
                    uint reqSkillRank = packet.ReadUInt32("Required Skill Level", i);

                    npcTrainer.ReqLevel = reqLevel;
                    npcTrainer.ReqSkillLine = reqSkillLine;
                    npcTrainer.ReqSkillRank = reqSkillRank;

                    trainerSpell.ReqLevel = reqLevel;
                    trainerSpell.ReqSkillLine = reqSkillLine;
                    trainerSpell.ReqSkillRank = reqSkillRank;

                    if (ClientVersion.RemovedInVersion(ClientVersionBuild.V5_1_0_16309))
                    {
                        trainerSpell.ReqAbility = new uint[3];
                        trainerSpell.ReqAbility[0] = packet.ReadUInt32<SpellId>("Chain Spell ID", i, 0);
                        trainerSpell.ReqAbility[1] = packet.ReadUInt32<SpellId>("Chain Spell ID", i, 1);
                    }
                    else
                        packet.ReadInt32<SpellId>("Required Spell ID", i);
                }

                packet.ReadInt32("Profession Dialog", i);
                packet.ReadInt32("Profession Button", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_0_6a_13623))
                {
                    npcTrainer.ReqLevel = packet.ReadByte("Required Level", i);
                    npcTrainer.ReqSkillLine = packet.ReadUInt32("Required Skill", i);
                    npcTrainer.ReqSkillRank = packet.ReadUInt32("Required Skill Level", i);
                    packet.ReadInt32<SpellId>("Chain Spell ID", i, 0);
                    packet.ReadInt32<SpellId>("Chain Spell ID", i, 1);
                }

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_0_6a_13623))
                    packet.ReadInt32("Unk Int32", i);

                Storage.NpcTrainers.Add(npcTrainer, packet.TimeSpan);

                if (trainerId > 0)
                    Storage.TrainerSpells.Add(trainerSpell, packet.TimeSpan);
            }

            trainer.Greeting = packet.ReadCString("Greeting");

            if (trainerId > 0)
            {
                Storage.Trainers.Add(trainer, packet.TimeSpan);

                AddToCreatureTrainers(trainer.Id, packet.TimeSpan, true);
            }

            LastGossipOption.Reset();
            TempGossipOptionPOI.Reset();
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            uint entry = packet.ReadGuid("GUID").GetEntry();
            int count = packet.ReadByte("Item Count");

            if (count == 0)
            {
                packet.ReadByte("Unk 1");
                return;
            }

            for (int i = 0; i < count; i++)
            {
                NpcVendor vendor = new NpcVendor
                {
                    Entry = entry,
                    Slot = packet.ReadInt32("Item Position", i)
                };

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_3_13329))
                    vendor.Type = packet.ReadUInt32("Item Type", i); // not confirmed

                vendor.Item = packet.ReadInt32<ItemId>("Item ID", i);
                packet.ReadInt32("Display ID", i);
                int maxCount = packet.ReadInt32("Max Count", i);
                packet.ReadInt32("Price", i);
                packet.ReadInt32("Max Durability", i);
                uint buyCount = packet.ReadUInt32("Buy Count", i);
                vendor.ExtendedCost = packet.ReadUInt32("Extended Cost", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_3_13329))
                    packet.ReadByte("Unk Byte", i);

                vendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                if (vendor.Type == 2)
                    vendor.MaxCount = buyCount;

                Storage.NpcVendors.Add(vendor, packet.TimeSpan);
            }

            LastGossipOption.Reset();
            TempGossipOptionPOI.Reset();
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleVendorInventoryList422(Packet packet)
        {
            var guidBytes = packet.StartBitStream(5, 6, 1, 2, 3, 0, 7, 4);

            packet.ReadXORByte(guidBytes, 2);
            packet.ReadXORByte(guidBytes, 3);

            uint count = packet.ReadUInt32("Item Count");

            packet.ReadXORByte(guidBytes, 5);
            packet.ReadXORByte(guidBytes, 0);
            packet.ReadXORByte(guidBytes, 1);

            packet.ReadByte("Unk Byte");

            packet.ReadXORByte(guidBytes, 4);
            packet.ReadXORByte(guidBytes, 7);
            packet.ReadXORByte(guidBytes, 6);

            uint entry = packet.WriteGuid("GUID", guidBytes).GetEntry();

            for (int i = 0; i < count; i++)
            {
                NpcVendor npcVendor = new NpcVendor
                {
                    Entry = entry
                };

                packet.ReadInt32("Max Durability", i);
                npcVendor.Slot = packet.ReadInt32("Item Position", i);
                npcVendor.Item = packet.ReadInt32<ItemId>("Item ID", i);
                packet.ReadInt32("Unk Int32 1", i);
                packet.ReadInt32("Display ID", i);
                int maxCount = packet.ReadInt32("Max Count", i);
                npcVendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                packet.ReadUInt32("Buy Count", i);
                npcVendor.ExtendedCost = packet.ReadUInt32("Extended Cost", i);
                packet.ReadInt32("Unk Int32 2", i);
                packet.ReadInt32("Price", i);

                // where's the vendorItem.Type (1/2)?

                Storage.NpcVendors.Add(npcVendor, packet.TimeSpan);
            }

            LastGossipOption.Reset();
            TempGossipOptionPOI.Reset();
        }

        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        [Parser(Opcode.CMSG_TRAINER_LIST)]
        [Parser(Opcode.CMSG_LIST_INVENTORY)]
        [Parser(Opcode.MSG_TABARDVENDOR_ACTIVATE)]
        [Parser(Opcode.CMSG_BANKER_ACTIVATE)]
        [Parser(Opcode.CMSG_SPIRIT_HEALER_ACTIVATE)]
        [Parser(Opcode.CMSG_BINDER_ACTIVATE)]
        public static void HandleNpcHello(Packet packet)
        {
            LastGossipOption.Reset();
            TempGossipOptionPOI.Reset();
            var guid = LastGossipOption.Guid = packet.ReadGuid("GUID");

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.CMSG_GOSSIP_HELLO, Direction.ClientToServer))
                packet.Holder.GossipHello = new PacketGossipHello { GossipSource = guid };
        }

        [Parser(Opcode.SMSG_BINDER_CONFIRM)]
        public static void HandleNpcHelloServer(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_SHOW_BANK)]
        public static void HandleShowBank(Packet packet)
        {
            packet.ReadGuid("GUID");
            LastGossipOption.Reset();
            TempGossipOptionPOI.Reset();
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            PacketGossipSelect packetGossip = packet.Holder.GossipSelect = new();

            packetGossip.GossipUnit = packet.ReadGuid("GUID");
            var menuID = packetGossip.MenuId = packet.ReadUInt32("MenuID");
            var optionID = packetGossip.OptionId = packet.ReadUInt32("OptionID");

            if (packet.CanRead()) // if ( byte_F3777C[v3] & 1 )
                packet.ReadCString("Box Text");

            Storage.GossipSelects.Add(Tuple.Create(menuID, optionID), null, packet.TimeSpan);

            LastGossipOption.GossipSelectOption(menuID, optionID, packet.TimeSpan);
            TempGossipOptionPOI.GossipSelectOption(menuID, optionID, packet.TimeSpan);
            TempGossipOptionPOI.Guid = LastGossipOption.Guid;
        }

        public static GossipQuestOption ReadGossipQuestTextData(Packet packet, params object[] idx)
        {
            var gossipQuest = new GossipQuestOption();
            gossipQuest.QuestId = (uint)packet.ReadInt32("QuestID", idx);
            packet.ReadInt32("QuestType", idx);
            packet.ReadInt32("QuestLevel", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685))
                packet.ReadUInt32E<QuestFlags>("Flags", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                packet.ReadUInt32E<QuestFlagsEx>("FlagsEx", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685))
                packet.ReadBool("Repeatable", idx);

            gossipQuest.Title = packet.ReadCString("Title", idx);
            return gossipQuest;
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            PacketGossipMessage packetGossip = packet.Holder.GossipMessage = new();
            GossipMenu gossip = new GossipMenu();

            WowGuid guid = packet.ReadGuid("GUID");
            packetGossip.GossipSource = guid;

            gossip.ObjectType = guid.GetObjectType();
            gossip.ObjectEntry = guid.GetEntry();

            uint menuId = packet.ReadUInt32("Menu Id");
            gossip.MenuID = packetGossip.MenuId = menuId;

            if (ClientVersion.AddedInVersion(ClientType.MistsOfPandaria))
            {
                var friendshipFactionID = packet.ReadInt32("Friendship Faction");
                AddGossipAddon(packetGossip.MenuId, friendshipFactionID, 0, guid, packet.TimeSpan);
            }

            gossip.TextID = packetGossip.TextId = packet.ReadUInt32("Text Id");

            uint count = packet.ReadUInt32("Amount of Options");

            for (int i = 0; i < count; i++)
            {
                GossipMenuOption gossipOption = new GossipMenuOption
                {
                    MenuID = menuId,
                };

                gossipOption.OptionID = packet.ReadUInt32("OptionID", i);
                gossipOption.OptionNpc = packet.ReadByteE<GossipOptionNpc>("OptionNPC", i);
                gossipOption.BoxCoded = packet.ReadBool("Box", i);
                gossipOption.BoxMoney = packet.ReadUInt32("Required money", i);
                gossipOption.OptionText = packet.ReadCString("Text", i);
                var boxText = packet.ReadCString("Box Text", i);

                gossipOption.FillBroadcastTextIDs();

                if (Settings.TargetedDatabase < TargetedDatabase.Shadowlands)
                    gossipOption.FillOptionType(guid);

                if (!string.IsNullOrEmpty(boxText))
                    gossipOption.BoxText = boxText;

                packetGossip.Options.Add(new GossipMessageOption()
                {
                    OptionIndex = gossipOption.OptionID.Value,
                    OptionNpc = (int)gossipOption.OptionNpc,
                    BoxCoded = gossipOption.BoxCoded.Value,
                    BoxCost = gossipOption.BoxMoney.Value,
                    Text = gossipOption.OptionText,
                    BoxText = gossipOption.BoxText
                });
            }

            uint questsCount = packet.ReadUInt32("GossipQuestsCount");
            for (int i = 0; i < questsCount; i++)
                ReadGossipQuestTextData(packet, i, "GossipQuests");

            if (guid.GetObjectType() == ObjectType.Unit)
            {
                CreatureTemplateGossip creatureTemplateGossip = new()
                {
                    CreatureID = guid.GetEntry(),
                    MenuID = menuId
                };
                Storage.CreatureTemplateGossips.Add(creatureTemplateGossip);
                Storage.CreatureDefaultGossips.Add(guid.GetEntry(), menuId);
            }

            Storage.Gossips.Add(gossip, packet.TimeSpan);
            UpdateLastGossipOptionActionMessage(packet.TimeSpan, menuId);

            packet.AddSniffData(StoreNameType.Gossip, (int)menuId, guid.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.SMSG_GOSSIP_COMPLETE)]
        public static void HandleGossipComplete(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
                packet.ReadBit("SuppressSound");
        }

        [Parser(Opcode.SMSG_THREAT_UPDATE)]
        [Parser(Opcode.SMSG_HIGHEST_THREAT_UPDATE)]
        public static void HandleThreatlistUpdate(Packet packet)
        {
            packet.ReadPackedGuid("GUID");

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_HIGHEST_THREAT_UPDATE, Direction.ServerToClient))
                packet.ReadPackedGuid("New Highest");

            var count = packet.ReadUInt32("Size");
            for (int i = 0; i < count; i++)
            {
                packet.ReadPackedGuid("Hostile", i);
                packet.ReadUInt32("Threat", i);
            }
        }

        [Parser(Opcode.SMSG_THREAT_CLEAR)]
        [Parser(Opcode.SMSG_THREAT_REMOVE)]
        public static void HandleRemoveThreatlist(Packet packet)
        {
            packet.ReadPackedGuid("GUID");

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_THREAT_REMOVE, Direction.ServerToClient))
                packet.ReadPackedGuid("Victim GUID");
        }
    }
}
