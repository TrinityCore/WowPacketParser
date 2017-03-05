using System;
using System.Collections.Generic;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Parsing.Parsers
{
    public static class NpcHandler
    {
        public static uint LastGossipPOIEntry;

        [Parser(Opcode.SMSG_GOSSIP_POI)]
        public static void HandleGossipPoi(Packet packet)
        {
            PointsOfInterest gossipPOI = new PointsOfInterest
            {
                ID = ++LastGossipPOIEntry,
                Flags = (uint) packet.Translator.ReadInt32E<UnknownFlags>("Flags")
            };

            Vector2 pos = packet.Translator.ReadVector2("Coordinates");
            gossipPOI.PositionX = pos.X;
            gossipPOI.PositionY = pos.Y;

            gossipPOI.Icon = packet.Translator.ReadUInt32E<GossipPOIIcon>("Icon");
            gossipPOI.Importance = packet.Translator.ReadUInt32("Data");
            gossipPOI.Name = packet.Translator.ReadCString("Icon Name");

            Storage.GossipPOIs.Add(gossipPOI, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_TRAINER_BUY_SPELL, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        [Parser(Opcode.SMSG_TRAINER_BUY_FAILED, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_TRAINER_BUY_RESULT)]
        public static void HandleServerTrainerBuy(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_3_13329))
                packet.Translator.ReadInt32("Unk");
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_TRAINER_BUY_FAILED, Direction.ServerToClient)
                || packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_TRAINER_BUY_RESULT, Direction.ServerToClient))
                packet.Translator.ReadUInt32("Reason");
        }

        [Parser(Opcode.SMSG_TRAINER_BUY_FAILED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTrainerBuyFailed434(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadUInt32("Reason");
        }

        // Might be a completely different opcode on 4.2.2 (trainer related)
        // Subv says it is SMSG_TRAINER_REPORT_ERROR_IN_CONSOLE but I think he is trolling me.
        [Parser(Opcode.SMSG_TRAINER_BUY_SUCCEEDED)]
        public static void HandleServerTrainerBuySucceedeed(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            if (ClientVersion.Build == ClientVersionBuild.V4_2_2_14545)
                packet.Translator.ReadInt32("Trainer Service"); // <TS>

            /* Comments about TS:
             * if !TS, "Trainer service <TS> unavailable"
             * if TS == 1, "Not enough money for trainer service <TS>"
             * Anyway... could only find 0s (and one 1)
             * */
        }

        [Parser(Opcode.CMSG_TRAINER_BUY_SPELL, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleTrainerBuySpell(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadInt32("TrainerID"); // same TrainerID exists in SMSG_TRAINER_LIST
            packet.Translator.ReadInt32<SpellId>("Spell ID");
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            uint entry = packet.Translator.ReadGuid("GUID").GetEntry();

            packet.Translator.ReadInt32E<TrainerType>("Type");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                packet.Translator.ReadInt32("TrainerID"); // Same TrainerID exists in CMSG_TRAINER_BUY_SPELL

            int count = packet.Translator.ReadInt32("Count");
            for (int i = 0; i < count; ++i)
            {
                NpcTrainer trainer = new NpcTrainer
                {
                    ID = entry,
                    SpellID = packet.Translator.ReadInt32<SpellId>("Spell ID", i)
                };

                packet.Translator.ReadByteE<TrainerSpellState>("State", i);

                trainer.MoneyCost = packet.Translator.ReadUInt32("Cost", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                {
                    trainer.ReqLevel = packet.Translator.ReadByte("Required Level", i);
                    trainer.ReqSkillLine = packet.Translator.ReadUInt32("Required Skill", i);
                    trainer.ReqSkillRank = packet.Translator.ReadUInt32("Required Skill Level", i);
                    if (ClientVersion.RemovedInVersion(ClientVersionBuild.V5_1_0_16309))
                    {
                        packet.Translator.ReadInt32<SpellId>("Chain Spell ID", i, 0);
                        packet.Translator.ReadInt32<SpellId>("Chain Spell ID", i, 1);
                    }
                    else
                        packet.Translator.ReadInt32<SpellId>("Required Spell ID", i);
                }

                packet.Translator.ReadInt32("Profession Dialog", i);
                packet.Translator.ReadInt32("Profession Button", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_0_6a_13623))
                {
                    trainer.ReqLevel = packet.Translator.ReadByte("Required Level", i);
                    trainer.ReqSkillLine = packet.Translator.ReadUInt32("Required Skill", i);
                    trainer.ReqSkillRank = packet.Translator.ReadUInt32("Required Skill Level", i);
                    packet.Translator.ReadInt32<SpellId>("Chain Spell ID", i, 0);
                    packet.Translator.ReadInt32<SpellId>("Chain Spell ID", i, 1);
                }

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_0_6a_13623))
                    packet.Translator.ReadInt32("Unk Int32", i);

                Storage.NpcTrainers.Add(trainer, packet.TimeSpan);
            }

            packet.Translator.ReadCString("Title");
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            uint entry = packet.Translator.ReadGuid("GUID").GetEntry();
            int count = packet.Translator.ReadByte("Item Count");

            if (count == 0)
            {
                packet.Translator.ReadByte("Unk 1");
                return;
            }

            for (int i = 0; i < count; i++)
            {
                NpcVendor vendor = new NpcVendor
                {
                    Entry = entry,
                    Slot = packet.Translator.ReadInt32("Item Position", i)
                };

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_3_13329))
                    vendor.Type = packet.Translator.ReadUInt32("Item Type", i); // not confirmed

                vendor.Item = packet.Translator.ReadInt32<ItemId>("Item ID", i);
                packet.Translator.ReadInt32("Display ID", i);
                int maxCount = packet.Translator.ReadInt32("Max Count", i);
                packet.Translator.ReadInt32("Price", i);
                packet.Translator.ReadInt32("Max Durability", i);
                uint buyCount = packet.Translator.ReadUInt32("Buy Count", i);
                vendor.ExtendedCost = packet.Translator.ReadUInt32("Extended Cost", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_3_13329))
                    packet.Translator.ReadByte("Unk Byte", i);

                vendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                if (vendor.Type == 2)
                    vendor.MaxCount = buyCount;

                Storage.NpcVendors.Add(vendor, packet.TimeSpan);
            }
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleVendorInventoryList422(Packet packet)
        {
            var guidBytes = packet.Translator.StartBitStream(5, 6, 1, 2, 3, 0, 7, 4);

            packet.Translator.ReadXORByte(guidBytes, 2);
            packet.Translator.ReadXORByte(guidBytes, 3);

            uint count = packet.Translator.ReadUInt32("Item Count");

            packet.Translator.ReadXORByte(guidBytes, 5);
            packet.Translator.ReadXORByte(guidBytes, 0);
            packet.Translator.ReadXORByte(guidBytes, 1);

            packet.Translator.ReadByte("Unk Byte");

            packet.Translator.ReadXORByte(guidBytes, 4);
            packet.Translator.ReadXORByte(guidBytes, 7);
            packet.Translator.ReadXORByte(guidBytes, 6);

            uint entry = packet.Translator.WriteGuid("GUID", guidBytes).GetEntry();

            for (int i = 0; i < count; i++)
            {
                NpcVendor npcVendor = new NpcVendor
                {
                    Entry = entry
                };

                packet.Translator.ReadInt32("Max Durability", i);
                npcVendor.Slot = packet.Translator.ReadInt32("Item Position", i);
                npcVendor.Item = packet.Translator.ReadInt32<ItemId>("Item ID", i);
                packet.Translator.ReadInt32("Unk Int32 1", i);
                packet.Translator.ReadInt32("Display ID", i);
                int maxCount = packet.Translator.ReadInt32("Max Count", i);
                npcVendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                packet.Translator.ReadUInt32("Buy Count", i);
                npcVendor.ExtendedCost = packet.Translator.ReadUInt32("Extended Cost", i);
                packet.Translator.ReadInt32("Unk Int32 2", i);
                packet.Translator.ReadInt32("Price", i);

                // where's the vendorItem.Type (1/2)?

                Storage.NpcVendors.Add(npcVendor, packet.TimeSpan);
            }
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleVendorInventoryList434(Packet packet)
        {
            var guidBytes = new byte[8];

            guidBytes[1] = packet.Translator.ReadBit();
            guidBytes[0] = packet.Translator.ReadBit();

            uint count = packet.Translator.ReadBits("Item Count", 21);

            guidBytes[3] = packet.Translator.ReadBit();
            guidBytes[6] = packet.Translator.ReadBit();
            guidBytes[5] = packet.Translator.ReadBit();
            guidBytes[2] = packet.Translator.ReadBit();
            guidBytes[7] = packet.Translator.ReadBit();

            var hasExtendedCost = new bool[count];
            var hasCondition = new bool[count];
            for (int i = 0; i < count; ++i)
            {
                hasExtendedCost[i] = !packet.Translator.ReadBit();
                hasCondition[i] = !packet.Translator.ReadBit();
            }

            guidBytes[4] = packet.Translator.ReadBit();

            var tempList = new List<NpcVendor>();
            for (int i = 0; i < count; ++i)
            {
                NpcVendor npcVendor = new NpcVendor
                {
                    Slot = packet.Translator.ReadInt32("Item Position", i)
                };

                packet.Translator.ReadInt32("Max Durability", i);
                if (hasExtendedCost[i])
                    npcVendor.ExtendedCost = packet.Translator.ReadUInt32("Extended Cost", i);
                npcVendor.Item = packet.Translator.ReadInt32<ItemId>("Item ID", i);
                npcVendor.Type = packet.Translator.ReadUInt32("Type", i); // 1 - item, 2 - currency
                packet.Translator.ReadInt32("Price", i);
                packet.Translator.ReadInt32("Display ID", i);
                if (hasCondition[i])
                    packet.Translator.ReadInt32("Row ID", i);
                int maxCount = packet.Translator.ReadInt32("Max Count", i);
                npcVendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                uint buyCount = packet.Translator.ReadUInt32("Buy Count", i);

                if (npcVendor.Type == 2)
                    npcVendor.MaxCount = buyCount;

                tempList.Add(npcVendor);
            }

            packet.Translator.ReadXORByte(guidBytes, 5);
            packet.Translator.ReadXORByte(guidBytes, 4);
            packet.Translator.ReadXORByte(guidBytes, 1);
            packet.Translator.ReadXORByte(guidBytes, 0);
            packet.Translator.ReadXORByte(guidBytes, 6);

            packet.Translator.ReadByte("Unk Byte");

            packet.Translator.ReadXORByte(guidBytes, 2);
            packet.Translator.ReadXORByte(guidBytes, 3);
            packet.Translator.ReadXORByte(guidBytes, 7);

            uint entry = packet.Translator.WriteGuid("GUID", guidBytes).GetEntry();
            tempList.ForEach(v =>
            {
                v.Entry = entry;
                Storage.NpcVendors.Add(v, packet.TimeSpan);
            });
        }

        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        [Parser(Opcode.CMSG_TRAINER_LIST)]
        [Parser(Opcode.CMSG_LIST_INVENTORY)]
        [Parser(Opcode.MSG_TABARDVENDOR_ACTIVATE)]
        [Parser(Opcode.CMSG_BANKER_ACTIVATE)]
        [Parser(Opcode.CMSG_SPIRIT_HEALER_ACTIVATE)]
        [Parser(Opcode.CMSG_BINDER_ACTIVATE)]
        [Parser(Opcode.SMSG_BINDER_CONFIRM)]
        [Parser(Opcode.SMSG_SHOW_BANK)]
        public static void HandleNpcHello(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            var menuEntry = packet.Translator.ReadUInt32("Menu Id");
            var gossipId = packet.Translator.ReadUInt32("GossipMenu Id");

            if (packet.CanRead()) // if ( byte_F3777C[v3] & 1 )
                packet.Translator.ReadCString("Box Text");

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipId), null, packet.TimeSpan);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            GossipMenu gossip = new GossipMenu();

            WowGuid guid = packet.Translator.ReadGuid("GUID");

            gossip.ObjectType = guid.GetObjectType();
            gossip.ObjectEntry = guid.GetEntry();

            uint menuId = packet.Translator.ReadUInt32("Menu Id");
            gossip.Entry = menuId;

            if (ClientVersion.AddedInVersion(ClientType.MistsOfPandaria))
                packet.Translator.ReadUInt32("Friendship Faction");

            gossip.TextID = packet.Translator.ReadUInt32("Text Id");

            uint count = packet.Translator.ReadUInt32("Amount of Options");

            for (int i = 0; i < count; i++)
            {
                GossipMenuOption gossipOption = new GossipMenuOption
                {
                    MenuID = menuId,
                    ID = packet.Translator.ReadUInt32("Index", i),
                    OptionIcon = packet.Translator.ReadByteE<GossipOptionIcon>("Icon", i),
                    BoxCoded = packet.Translator.ReadBool("Box", i),
                    BoxMoney = packet.Translator.ReadUInt32("Required money", i),
                    OptionText = packet.Translator.ReadCString("Text", i),
                    BoxText = packet.Translator.ReadCString("Box Text", i)
                };

                Storage.GossipMenuOptions.Add(gossipOption, packet.TimeSpan);
            }

            uint questgossips = packet.Translator.ReadUInt32("Amount of Quest gossips");
            for (int i = 0; i < questgossips; i++)
            {
                packet.Translator.ReadUInt32<QuestId>("Quest ID", i);

                packet.Translator.ReadUInt32("Icon", i);
                packet.Translator.ReadInt32("Level", i);
                packet.Translator.ReadUInt32E<QuestFlags>("Flags", i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                    packet.Translator.ReadUInt32E<QuestFlags2>("Flags 2", i);

                packet.Translator.ReadBool("Change Icon", i);
                packet.Translator.ReadCString("Title", i);
            }

            if (guid.GetObjectType() == ObjectType.Unit)
                if (Storage.Objects.ContainsKey(guid))
                    ((Unit)Storage.Objects[guid].Item1).GossipId = menuId;

            Storage.Gossips.Add(gossip, packet.TimeSpan);

            packet.AddSniffData(StoreNameType.Gossip, (int)menuId, guid.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.SMSG_THREAT_UPDATE)]
        [Parser(Opcode.SMSG_HIGHEST_THREAT_UPDATE)]
        public static void HandleThreatlistUpdate(Packet packet)
        {
            packet.Translator.ReadPackedGuid("GUID");

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_HIGHEST_THREAT_UPDATE, Direction.ServerToClient))
                packet.Translator.ReadPackedGuid("New Highest");

            var count = packet.Translator.ReadUInt32("Size");
            for (int i = 0; i < count; i++)
            {
                packet.Translator.ReadPackedGuid("Hostile", i);
                packet.Translator.ReadUInt32("Threat", i);
            }
        }

        [Parser(Opcode.SMSG_THREAT_CLEAR)]
        [Parser(Opcode.SMSG_THREAT_REMOVE)]
        public static void HandleRemoveThreatlist(Packet packet)
        {
            packet.Translator.ReadPackedGuid("GUID");

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_THREAT_REMOVE, Direction.ServerToClient))
                packet.Translator.ReadPackedGuid("Victim GUID");
        }
    }
}
