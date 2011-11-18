using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
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
            var flags = packet.ReadInt32();
            packet.Writer.WriteLine("Flags: 0x" + flags.ToString("X8"));

            var pos = packet.ReadVector2();
            packet.Writer.WriteLine("Coordinates: " + pos);

            var icon = (GossipPoiIcon)packet.ReadInt32();
            packet.Writer.WriteLine("Icon: " + icon);

            var data = packet.ReadInt32();
            packet.Writer.WriteLine("Data: " + data);

            var iconName = packet.ReadCString();
            packet.Writer.WriteLine("Icon Name: " + iconName);
        }

        [Parser(Opcode.SMSG_TRAINER_BUY_SUCCEEDED)]
        public static void HandleServerTrainerBuySucceedeed(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            var npcTrainer = new NpcTrainer();

            var guid = packet.ReadGuid("GUID");

            npcTrainer.Type = packet.ReadEnum<TrainerType>("Type", TypeCode.Int32);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_0_14333))
                packet.ReadInt32("Unk Int32");
            
            var count = packet.ReadInt32("Count");
            npcTrainer.TrainerSpells = new List<TrainerSpell>(count);
            for (var i = 0; i < count; i++)
            {
                var trainerSpell = new TrainerSpell();

                trainerSpell.Spell = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);

                packet.ReadEnum<TrainerSpellState>("State", TypeCode.Byte, i);

                trainerSpell.Cost = packet.ReadUInt32("Cost", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_0_14333))
                {
                    trainerSpell.RequiredLevel = packet.ReadByte("Required Level", i);
                    trainerSpell.RequiredSkill = packet.ReadUInt32("Required Skill", i);
                    trainerSpell.RequiredSkillLevel = packet.ReadUInt32("Required Skill Level", i);
                }

                packet.ReadInt32("Profession Dialog", i);
                packet.ReadInt32("Profession Button", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_0_14333))
                {
                    trainerSpell.RequiredLevel = packet.ReadByte("Required Level", i);
                    trainerSpell.RequiredSkill = packet.ReadUInt32("Required Skill", i);
                    trainerSpell.RequiredSkillLevel = packet.ReadUInt32("Required Skill Level", i);
                }

                packet.ReadInt32("Chain Node 1", i);
                packet.ReadInt32("Chain Node 2", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_0_14333))
                    packet.ReadInt32("Unk Int32", i);

                npcTrainer.TrainerSpells.Add(trainerSpell);
            }

            npcTrainer.Title = packet.ReadCString("Title");

            Stuffing.NpcTrainers.TryAdd(guid.GetEntry(), npcTrainer);
        }

        private static ulong ReadByte(ref Packet packet, int index)
        {
            var tmp = (ulong)packet.ReadByte();
            if ((tmp % 2) == 0)
                tmp++;
            else
                tmp--;
            // Debug: packet.Writer.WriteLine("Read {0} = {1}", index, tmp.ToString("X2"));
            return (tmp << 8*index);
        }

        // WIP
        [Parser(Opcode.SMSG_LIST_INVENTORY, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleVendorInventoryList422(Packet packet)
        {
            var npcVendor = new NpcVendor();
            var flags = packet.ReadEnum<UnknownFlags>("Unk Flags", TypeCode.Byte);

            ulong tmp = 0;

            if (flags.HasFlag(UnknownFlags.Unk5))
                tmp += ReadByte(ref packet, 2);

            var itemCount = packet.ReadUInt32("Item Count");

            if (flags.HasFlag(UnknownFlags.Unk8))
                tmp += ReadByte(ref packet, 5);

            if (flags.HasFlag(UnknownFlags.Unk2)) // Flag?
                tmp += ReadByte(ref packet, 0);

            if (flags.HasFlag(UnknownFlags.Unk3)) // Flag?
                tmp += ReadByte(ref packet, 1);

            if (flags.HasFlag(UnknownFlags.Unk6)) // Flag?
                ReadByte(ref packet, 3);

            if (flags.HasFlag(UnknownFlags.Unk1))
                tmp += ReadByte(ref packet, 4);

            if (flags.HasFlag(UnknownFlags.Unk7)) // Flag?
                tmp += ReadByte(ref packet, 7);

            if (!flags.HasFlag(UnknownFlags.Unk4)) // Flag?
                tmp += ReadByte(ref packet, 6);

            var guid = new Guid(tmp);
            packet.Writer.WriteLine("GUID: " + guid);

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
                vendorItem.BuyCount = packet.ReadUInt32("Buy Count", i);
                vendorItem.ExtendedCostId = packet.ReadUInt32("Extended Cost", i);
                packet.ReadInt32("Unk Int32 2", i);
                packet.ReadInt32("Price", i);

                npcVendor.VendorItems.Add(vendorItem);
            }

            Stuffing.NpcVendors.TryAdd(guid.GetEntry(), npcVendor);
        }

        [Parser(Opcode.SMSG_LIST_INVENTORY, ClientVersionBuild.V1_12_1_5875, ClientVersionBuild.V4_2_0_14333)]
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
                vendorItem.ItemId = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Item ID", i);
                packet.ReadInt32("Display ID", i);
                vendorItem.MaxCount = packet.ReadInt32("Max Count", i);
                packet.ReadInt32("Price", i);
                packet.ReadInt32("Max Durability", i);
                vendorItem.BuyCount = packet.ReadUInt32("Buy Count", i);
                vendorItem.ExtendedCostId = packet.ReadUInt32("Extended Cost", i);
            }

            Stuffing.NpcVendors.TryAdd(guid.GetEntry(), npcVendor);
        }

        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        [Parser(Opcode.CMSG_TRAINER_LIST)]
        [Parser(Opcode.CMSG_LIST_INVENTORY)]
        [Parser(Opcode.MSG_TABARDVENDOR_ACTIVATE)]
        [Parser(Opcode.CMSG_BANKER_ACTIVATE)]
        [Parser(Opcode.CMSG_SPIRIT_HEALER_ACTIVATE)]
        [Parser(Opcode.CMSG_BINDER_ACTIVATE)]
        [Parser(Opcode.SMSG_SHOW_BANK)]
        public static void HandleNpcHello(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Menu id");
            packet.ReadUInt32("Gossip id");
        }

        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            var guid = packet.ReadGuid("GUID");
            var menuid = packet.ReadUInt32("Menu id");
            var textid = packet.ReadUInt32("Text id");

            var count = packet.ReadUInt32("Amount of Options");

            GossipMenu gossip = new GossipMenu
            {
                MenuId = menuid,
                GossipOptions = new List<GossipOption>(),
                NpcTextIds = new List<uint>()
            };
            
            gossip = Stuffing.Gossips.GetOrAdd(guid.GetEntry(), gossip);
            
            gossip.NpcTextIds.Add(textid);

            for (var i = 0; i < count; i++)
            {
                var index = packet.ReadUInt32("Index", i);
                var icon = packet.ReadByte("Icon", i);
                var box = packet.ReadBoolean("Box", i);
                var reqmoney = packet.ReadUInt32("Required money", i);
                var text = packet.ReadCString("Text", i);
                var boxtext = packet.ReadCString("Box Text", i);
                GossipOption opt = new GossipOption
                {
                    Box = box,
                    RequiredMoney = reqmoney,
                    Index = index,
                    OptionIcon = icon,
                    OptionText = text,
                    BoxText = boxtext
                };
                gossip.GossipOptions.Add(opt);
            }

            Stuffing.Gossips.AddOrUpdate(guid.GetEntry(), gossip, (a,b) => 
            {
                b.GossipOptions = gossip.GossipOptions;
                b.NpcTextIds = gossip.NpcTextIds;
                return b;
            });

            var questgossips = packet.ReadUInt32("Amount of Quest gossips");
            for (var i = 0; i < questgossips; i++)
            {
                packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest ID", i);

                packet.ReadUInt32("Icon", i);
                packet.ReadInt32("Level", i);
                packet.ReadEnum<QuestFlag>("Flags", TypeCode.UInt32, i);
                packet.ReadBoolean("Unk Bool", i);
                packet.ReadCString("Title", i);
            }
        }

        [Parser(Opcode.SMSG_THREAT_UPDATE)]
        [Parser(Opcode.SMSG_HIGHEST_THREAT_UPDATE)]
        public static void HandleThreatlistUpdate(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            packet.Writer.WriteLine("GUID: " + guid);

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_HIGHEST_THREAT_UPDATE))
            {
                var newhigh = packet.ReadPackedGuid();
                packet.Writer.WriteLine("New Highest: " + newhigh);
            }

            var count = packet.ReadUInt32();
            packet.Writer.WriteLine("Size: " + count);
            for (int i = 0; i < count; i++)
            {
                packet.ReadPackedGuid("Hostile");
                var threat = packet.ReadUInt32();
                // No idea why, but this is in core.
                /*if (packet.Opcode == Opcode.SMSG_THREAT_UPDATE)
                    threat *= 100;*/
                packet.Writer.WriteLine("Threat: " + threat);
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
    }
}
