using System;
using System.Collections.Generic;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WoWPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.SQL;

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

        [Parser(Opcode.SMSG_GOSSIP_POI)]
        public static void HandleGossipPoi(Packet packet)
        {
            PointsOfInterest gossipPOI = new PointsOfInterest();

            gossipPOI.Flags = (uint)packet.ReadInt32E<UnknownFlags>("Flags");
            Vector2 pos = packet.ReadVector2("Coordinates");
            gossipPOI.PositionX = pos.X;
            gossipPOI.PositionY = pos.Y;

            gossipPOI.Icon = packet.ReadUInt32E<GossipPOIIcon>("Icon");
            gossipPOI.Importance = packet.ReadUInt32("Importance");
            gossipPOI.Name = packet.ReadCString("Name");

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

            if (TempGossipOptionPOI.HasSelection)
            {
                if ((packet.TimeSpan - TempGossipOptionPOI.TimeSpan).Duration() <= TimeSpan.FromMilliseconds(2500))
                {
                    if (TempGossipOptionPOI.ActionMenuId != null)
                    {
                        Storage.GossipMenuOptionActions.Add(new GossipMenuOptionAction { MenuId = TempGossipOptionPOI.MenuId, OptionIndex = TempGossipOptionPOI.OptionIndex, ActionMenuId = TempGossipOptionPOI.ActionMenuId, ActionPoiId = gossipPOI.ID }, packet.TimeSpan);
                        //clear temp
                        TempGossipOptionPOI.Reset();
                    }
                }
                else
                {
                    LastGossipOption.Reset();
                    TempGossipOptionPOI.Reset();
                }
            }
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

                if (LastGossipOption.HasSelection)
                {
                    if ((packet.TimeSpan - LastGossipOption.TimeSpan).Duration() <= TimeSpan.FromMilliseconds(2500))
                        Storage.CreatureTrainers.Add(new CreatureTrainer { CreatureId = LastGossipOption.Guid.GetEntry(), MenuID = LastGossipOption.MenuId, OptionIndex = LastGossipOption.OptionIndex, TrainerId = trainer.Id }, packet.TimeSpan);
                }
                else
                    Storage.CreatureTrainers.Add(new CreatureTrainer { CreatureId = LastGossipOption.Guid.GetEntry(), MenuID = 0, OptionIndex = 0, TrainerId = trainer.Id }, packet.TimeSpan);
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

        [Parser(Opcode.SMSG_VENDOR_INVENTORY, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleVendorInventoryList434(Packet packet)
        {
            var guidBytes = new byte[8];

            guidBytes[1] = packet.ReadBit();
            guidBytes[0] = packet.ReadBit();

            uint count = packet.ReadBits("Item Count", 21);

            guidBytes[3] = packet.ReadBit();
            guidBytes[6] = packet.ReadBit();
            guidBytes[5] = packet.ReadBit();
            guidBytes[2] = packet.ReadBit();
            guidBytes[7] = packet.ReadBit();

            var hasExtendedCost = new bool[count];
            var hasCondition = new bool[count];
            for (int i = 0; i < count; ++i)
            {
                hasExtendedCost[i] = !packet.ReadBit();
                hasCondition[i] = !packet.ReadBit();
            }

            guidBytes[4] = packet.ReadBit();

            var tempList = new List<NpcVendor>();
            for (int i = 0; i < count; ++i)
            {
                NpcVendor npcVendor = new NpcVendor
                {
                    Slot = packet.ReadInt32("Item Position", i)
                };

                packet.ReadInt32("Max Durability", i);
                if (hasExtendedCost[i])
                    npcVendor.ExtendedCost = packet.ReadUInt32("Extended Cost", i);
                npcVendor.Item = packet.ReadInt32<ItemId>("Item ID", i);
                npcVendor.Type = packet.ReadUInt32("Type", i); // 1 - item, 2 - currency
                packet.ReadInt32("Price", i);
                packet.ReadInt32("Display ID", i);
                if (hasCondition[i])
                    packet.ReadInt32("Row ID", i);
                int maxCount = packet.ReadInt32("Max Count", i);
                npcVendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                uint buyCount = packet.ReadUInt32("Buy Count", i);

                if (npcVendor.Type == 2)
                    npcVendor.MaxCount = buyCount;

                tempList.Add(npcVendor);
            }

            packet.ReadXORByte(guidBytes, 5);
            packet.ReadXORByte(guidBytes, 4);
            packet.ReadXORByte(guidBytes, 1);
            packet.ReadXORByte(guidBytes, 0);
            packet.ReadXORByte(guidBytes, 6);

            packet.ReadByte("Unk Byte");

            packet.ReadXORByte(guidBytes, 2);
            packet.ReadXORByte(guidBytes, 3);
            packet.ReadXORByte(guidBytes, 7);

            uint entry = packet.WriteGuid("GUID", guidBytes).GetEntry();
            tempList.ForEach(v =>
            {
                v.Entry = entry;
                Storage.NpcVendors.Add(v, packet.TimeSpan);
            });

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
            packet.ReadGuid("GUID");
            var menuEntry = packet.ReadUInt32("Menu Id");
            var gossipId = packet.ReadUInt32("GossipMenu Id");

            if (packet.CanRead()) // if ( byte_F3777C[v3] & 1 )
                packet.ReadCString("Box Text");

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipId), null, packet.TimeSpan);

            LastGossipOption.MenuId = menuEntry;
            LastGossipOption.OptionIndex = gossipId;
            LastGossipOption.ActionMenuId = null;
            LastGossipOption.ActionPoiId = null;
            LastGossipOption.TimeSpan = packet.TimeSpan;

            TempGossipOptionPOI.Guid = LastGossipOption.Guid;
            TempGossipOptionPOI.MenuId = menuEntry;
            TempGossipOptionPOI.OptionIndex = gossipId;
            TempGossipOptionPOI.ActionMenuId = null;
            TempGossipOptionPOI.ActionPoiId = null;
            TempGossipOptionPOI.TimeSpan = packet.TimeSpan;
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
            gossip.Entry = packetGossip.MenuId = menuId;

            if (ClientVersion.AddedInVersion(ClientType.MistsOfPandaria))
                packet.ReadUInt32("Friendship Faction");

            gossip.TextID = packetGossip.TextId = packet.ReadUInt32("Text Id");

            uint count = packet.ReadUInt32("Amount of Options");

            for (int i = 0; i < count; i++)
            {
                GossipMenuOption gossipOption = new GossipMenuOption
                {
                    MenuId = menuId,
                };
                GossipMenuOptionBox gossipMenuOptionBox = new GossipMenuOptionBox
                {
                    MenuId = menuId
                };

                gossipOption.OptionIndex = gossipMenuOptionBox.OptionIndex = packet.ReadUInt32("Index", i);
                gossipOption.OptionIcon = packet.ReadByteE<GossipOptionIcon>("Icon", i);
                gossipMenuOptionBox.BoxCoded = packet.ReadBool("Box", i);
                gossipMenuOptionBox.BoxMoney = packet.ReadUInt32("Required money", i);
                gossipOption.OptionText = packet.ReadCString("Text", i);
                gossipMenuOptionBox.BoxText = packet.ReadCString("Box Text", i);

                Storage.GossipMenuOptions.Add(gossipOption, packet.TimeSpan);
                if (!gossipMenuOptionBox.IsEmpty)
                    Storage.GossipMenuOptionBoxes.Add(gossipMenuOptionBox, packet.TimeSpan);
                
                packetGossip.Options.Add(new GossipMessageOption()
                {
                    OptionIndex = gossipOption.OptionIndex.Value,
                    OptionIcon = (int)gossipOption.OptionIcon,
                    BoxCoded = gossipMenuOptionBox.BoxCoded.Value,
                    BoxCost = gossipMenuOptionBox.BoxMoney.Value,
                    Text = gossipOption.OptionText,
                    BoxText = gossipMenuOptionBox.BoxText
                });
            }

            uint questgossips = packet.ReadUInt32("Amount of Quest gossips");
            for (int i = 0; i < questgossips; i++)
            {
                packet.ReadUInt32<QuestId>("Quest ID", i);

                packet.ReadUInt32("Icon", i);
                packet.ReadInt32("Level", i);
                packet.ReadUInt32E<QuestFlags>("Flags", i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                    packet.ReadUInt32E<QuestFlagsEx>("Flags 2", i);

                packet.ReadBool("Change Icon", i);
                packet.ReadCString("Title", i);
            }

            if (guid.GetObjectType() == ObjectType.Unit)
                if (Storage.Objects.ContainsKey(guid))
                    ((Unit)Storage.Objects[guid].Item1).GossipId = menuId;

            Storage.Gossips.Add(gossip, packet.TimeSpan);
            if (LastGossipOption.HasSelection)
            {
                if ((packet.TimeSpan - LastGossipOption.TimeSpan).Duration() <= TimeSpan.FromMilliseconds(2500))
                {
                    Storage.GossipMenuOptionActions.Add(new GossipMenuOptionAction { MenuId = LastGossipOption.MenuId, OptionIndex = LastGossipOption.OptionIndex, ActionMenuId = menuId, ActionPoiId = LastGossipOption.ActionPoiId }, packet.TimeSpan);

                    //keep temp data (for case SMSG_GOSSIP_POI is delayed)
                    TempGossipOptionPOI.Guid = LastGossipOption.Guid;
                    TempGossipOptionPOI.MenuId = LastGossipOption.MenuId;
                    TempGossipOptionPOI.OptionIndex = LastGossipOption.OptionIndex;
                    TempGossipOptionPOI.ActionMenuId = gossip.Entry;
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

            packet.AddSniffData(StoreNameType.Gossip, (int)menuId, guid.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.SMSG_GOSSIP_COMPLETE)]
        public static void HandleGossipComplete(Packet packet)
        {
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
