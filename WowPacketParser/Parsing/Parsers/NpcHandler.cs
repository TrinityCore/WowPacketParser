using System;
using System.Collections.Generic;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class NpcHandler
    {
        [Parser(Opcode.SMSG_GOSSIP_POI)]
        public static void HandleGossipPoi(Packet packet)
        {
            packet.ReadEnum<UnknownFlags>("Flags", TypeCode.Int32);
            packet.ReadVector2("Coordinates");
            packet.ReadEnum<GossipPoiIcon>("Icon", TypeCode.UInt32);
            packet.ReadInt32("Data");
            packet.ReadCString("Icon Name");
        }

        [Parser(Opcode.CMSG_TRAINER_BUY_SPELL, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        [Parser(Opcode.SMSG_TRAINER_BUY_FAILED)]
        [Parser(Opcode.SMSG_TRAINER_BUY_RESULT)]
        public static void HandleServerTrainerBuy(Packet packet)
        {
            packet.ReadGuid("GUID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_3_13329))
                packet.ReadInt32("Unk");
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_TRAINER_BUY_FAILED)
                || packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_TRAINER_BUY_RESULT))
                packet.ReadUInt32("Reason");
        }

        // Might be a completely different opcode on 4.2.2 (trainer related)
        // Subv says it is SMSG_TRAINER_REPORT_ERROR_IN_CONSOLE but I think he is trolling me.
        [Parser(Opcode.SMSG_TRAINER_BUY_SUCCEEDED)]
        public static void HandleServerTrainerBuySucceedeed(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
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
            packet.ReadInt32("Unknown Int32");
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            var npcTrainer = new NpcTrainer();

            var guid = packet.ReadGuid("GUID");

            npcTrainer.Type = packet.ReadEnum<TrainerType>("Type", TypeCode.Int32);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                packet.ReadInt32("Unk Int32");

            var count = packet.ReadInt32("Count");
            npcTrainer.TrainerSpells = new List<TrainerSpell>(count);
            for (var i = 0; i < count; i++)
            {
                var trainerSpell = new TrainerSpell();

                trainerSpell.Spell = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);

                packet.ReadEnum<TrainerSpellState>("State", TypeCode.Byte, i);

                trainerSpell.Cost = packet.ReadUInt32("Cost", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                {
                    trainerSpell.RequiredLevel = packet.ReadByte("Required Level", i);
                    trainerSpell.RequiredSkill = packet.ReadUInt32("Required Skill", i);
                    trainerSpell.RequiredSkillLevel = packet.ReadUInt32("Required Skill Level", i);
                    packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Chain Spell ID", i, 0);
                    packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Chain Spell ID", i, 1);
                }

                packet.ReadInt32("Profession Dialog", i);
                packet.ReadInt32("Profession Button", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_0_6a_13623))
                {
                    trainerSpell.RequiredLevel = packet.ReadByte("Required Level", i);
                    trainerSpell.RequiredSkill = packet.ReadUInt32("Required Skill", i);
                    trainerSpell.RequiredSkillLevel = packet.ReadUInt32("Required Skill Level", i);
                    packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Chain Spell ID", i, 0);
                    packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Chain Spell ID", i, 1);
                }

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_0_6a_13623))
                    packet.ReadInt32("Unk Int32", i);

                npcTrainer.TrainerSpells.Add(trainerSpell);
            }

            npcTrainer.Title = packet.ReadCString("Title");

            Storage.NpcTrainers.Add(guid.GetEntry(), npcTrainer, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_LIST_INVENTORY, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            var npcVendor = new NpcVendor();

            var guid = packet.ReadGuid("GUID");

            var itemCount = packet.ReadByte("Item Count");

            npcVendor.VendorItems = new List<VendorItem>(itemCount);
            for (var i = 0; i < itemCount; i++)
            {
                var vendorItem = new VendorItem();

                vendorItem.Slot = packet.ReadUInt32("Item Position", i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_3_13329))
                    packet.ReadUInt32("Unk Uint32", i);
                vendorItem.ItemId = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Item ID", i);
                packet.ReadInt32("Display ID", i);
                vendorItem.MaxCount = packet.ReadInt32("Max Count", i);
                packet.ReadInt32("Price", i);
                packet.ReadInt32("Max Durability", i);
                packet.ReadUInt32("Buy Count", i);
                vendorItem.ExtendedCostId = packet.ReadUInt32("Extended Cost", i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_3_13329))
                    packet.ReadByte("Unk Byte", i);
            }

            Storage.NpcVendors.Add(guid.GetEntry(), npcVendor, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_LIST_INVENTORY, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleVendorInventoryList422(Packet packet)
        {
            var npcVendor = new NpcVendor();

            var guidBytes = new byte[8];

            guidBytes[5] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[6] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[1] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[2] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[3] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[0] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[7] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[4] = (byte)(packet.ReadBit() ? 1 : 0);

            if (guidBytes[2] != 0) guidBytes[2] ^= packet.ReadByte();
            if (guidBytes[3] != 0) guidBytes[3] ^= packet.ReadByte();

            var itemCount = packet.ReadUInt32("Item Count");

            if (guidBytes[5] != 0) guidBytes[5] ^= packet.ReadByte();
            if (guidBytes[0] != 0) guidBytes[0] ^= packet.ReadByte();
            if (guidBytes[1] != 0) guidBytes[1] ^= packet.ReadByte();

            packet.ReadByte("Unk Byte");

            if (guidBytes[4] != 0) guidBytes[4] ^= packet.ReadByte();
            if (guidBytes[7] != 0) guidBytes[7] ^= packet.ReadByte();
            if (guidBytes[6] != 0) guidBytes[6] ^= packet.ReadByte();

            var guid = new Guid(BitConverter.ToUInt64(guidBytes, 0));

            packet.WriteGuid("Guid", guidBytes);

            npcVendor.VendorItems = new List<VendorItem>((int)itemCount);
            for (var i = 0; i < itemCount; i++)
            {
                var vendorItem = new VendorItem();

                packet.ReadInt32("Max Durability", i);
                vendorItem.Slot = packet.ReadUInt32("Item Position", i);
                vendorItem.ItemId = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Item ID", i);
                packet.ReadInt32("Unk Int32 1", i);
                packet.ReadInt32("Display ID", i);
                vendorItem.MaxCount = packet.ReadInt32("Max Count", i);
                packet.ReadUInt32("Buy Count", i);
                vendorItem.ExtendedCostId = packet.ReadUInt32("Extended Cost", i);
                packet.ReadInt32("Unk Int32 2", i);
                packet.ReadInt32("Price", i);

                npcVendor.VendorItems.Add(vendorItem);
            }

            Storage.NpcVendors.Add(guid.GetEntry(), npcVendor, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_LIST_INVENTORY, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleVendorInventoryList434(Packet packet)
        {
            var npcVendor = new NpcVendor();

            var guidBytes = new byte[8];

            guidBytes[1] = packet.ReadBit().ToByte();
            guidBytes[0] = packet.ReadBit().ToByte();

            var itemCount = packet.ReadBits("Item Count", 21);

            guidBytes[3] = packet.ReadBit().ToByte();
            guidBytes[6] = packet.ReadBit().ToByte();
            guidBytes[5] = packet.ReadBit().ToByte();
            guidBytes[2] = packet.ReadBit().ToByte();
            guidBytes[7] = packet.ReadBit().ToByte();

            var hasExtendedCost = new bool[itemCount];
            var enabler2 = new bool[itemCount];
            for (int i = 0; i < itemCount; ++i)
            {
                hasExtendedCost[i] = !packet.ReadBit();
                enabler2[i] = !packet.ReadBit();
            }

            guidBytes[4] = packet.ReadBit().ToByte();

            npcVendor.VendorItems = new List<VendorItem>((int)itemCount);
            for (int i = 0; i < itemCount; ++i)
            {
                var vendorItem = new VendorItem();

                vendorItem.Slot = packet.ReadUInt32("Item Position", i);
                packet.ReadInt32("Max Durability", i);
                if (hasExtendedCost[i])
                    vendorItem.ExtendedCostId = packet.ReadUInt32("Extended Cost", i);
                vendorItem.ItemId = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Item ID", i);
                packet.ReadInt32("Type", i); // 1 - item, 2 - currency
                packet.ReadInt32("Price", i);
                packet.ReadInt32("Display ID", i);
                if (enabler2[i])
                    packet.ReadInt32("Unk Int32 8", i);
                vendorItem.MaxCount = packet.ReadInt32("Max Count", i);
                packet.ReadUInt32("Buy Count", i);

                npcVendor.VendorItems.Add(vendorItem);
            }

            if (guidBytes[5] != 0) guidBytes[5] ^= packet.ReadByte();
            if (guidBytes[4] != 0) guidBytes[4] ^= packet.ReadByte();
            if (guidBytes[1] != 0) guidBytes[1] ^= packet.ReadByte();
            if (guidBytes[0] != 0) guidBytes[0] ^= packet.ReadByte();
            if (guidBytes[6] != 0) guidBytes[6] ^= packet.ReadByte();

            packet.ReadByte("Unk Byte");

            if (guidBytes[2] != 0) guidBytes[2] ^= packet.ReadByte();
            if (guidBytes[3] != 0) guidBytes[3] ^= packet.ReadByte();
            if (guidBytes[7] != 0) guidBytes[7] ^= packet.ReadByte();


            var guid = new Guid(BitConverter.ToUInt64(guidBytes, 0));
            packet.WriteGuid("Guid", guidBytes);

            Storage.NpcVendors.Add(guid.GetEntry(), npcVendor, packet.TimeSpan);
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
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Menu Id");
            packet.ReadUInt32("Gossip Id");

            if (packet.CanRead()) // if ( byte_F3777C[v3] & 1 )
                packet.ReadCString("Box Text");
        }

        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            var gossip = new Gossip();

            var guid = packet.ReadGuid("GUID");

            gossip.ObjectType = guid.GetObjectType();
            gossip.ObjectEntry = guid.GetEntry();

            var menuId = packet.ReadUInt32("Menu Id");
            var textId = packet.ReadUInt32("Text Id");

            if (guid.GetObjectType() == ObjectType.Unit)
                if (Storage.Objects.ContainsKey(guid))
                        ((Unit) Storage.Objects[guid].Item1).GossipId = menuId;

            var count = packet.ReadUInt32("Amount of Options");

            gossip.GossipOptions = new List<GossipOption>((int) count);
            for (var i = 0; i < count; i++)
            {
                var gossipOption = new GossipOption
                {
                    Index = packet.ReadUInt32("Index", i),
                    OptionIcon = packet.ReadEnum<GossipOptionIcon>("Icon", TypeCode.Byte, i),
                    Box = packet.ReadBoolean("Box", i),
                    RequiredMoney = packet.ReadUInt32("Required money", i),
                    OptionText = packet.ReadCString("Text", i),
                    BoxText = packet.ReadCString("Box Text", i)
                };

                gossip.GossipOptions.Add(gossipOption);
            }
            Storage.Gossips.Add(Tuple.Create(menuId, textId), gossip, packet.TimeSpan);
            packet.AddSniffData(StoreNameType.Gossip, (int)menuId, guid.GetEntry().ToString(CultureInfo.InvariantCulture));

            var questgossips = packet.ReadUInt32("Amount of Quest gossips");
            for (var i = 0; i < questgossips; i++)
            {
                packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest ID", i);

                packet.ReadUInt32("Icon", i);
                packet.ReadInt32("Level", i);
                packet.ReadEnum<QuestFlags>("Flags", TypeCode.UInt32, i);
                packet.ReadBoolean("Unk Bool", i);
                packet.ReadCString("Title", i);
            }
        }

        [Parser(Opcode.SMSG_THREAT_UPDATE)]
        [Parser(Opcode.SMSG_HIGHEST_THREAT_UPDATE)]
        public static void HandleThreatlistUpdate(Packet packet)
        {
            packet.ReadPackedGuid("GUID");

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_HIGHEST_THREAT_UPDATE))
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

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_THREAT_REMOVE))
                packet.ReadPackedGuid("Victim GUID");
        }

        [Parser(Opcode.CMSG_QUERY_COMPLETION_NPC_RESPONSE)] // 4.3.4
        public static void HandleQueryCompletionNPCResponse(Packet packet)
        {
            var count = packet.ReadBits("Count", 24);
            for (int i = 0; i < count; ++i)
                packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest", i);
        }

        [Parser(Opcode.SMSG_COMPLETION_NPC_RESPONSE)] // 4.3.4
        public static void HandleCompletionNPCResponse(Packet packet)
        {
            var count = packet.ReadBits("Count", 23);
            var counts = new uint[count];

            for (int i = 0; i < count; ++i)
                counts[i] = packet.ReadBits("Count", 24, i);

            for (int i = 0; i < count; ++i)
            {
                packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest", i);
                for (int j = 0; j < counts[i]; ++j)
                    packet.ReadEntryWithName<UInt32>(StoreNameType.Unit, "Unit", i, j);
            }
        }
    }
}
