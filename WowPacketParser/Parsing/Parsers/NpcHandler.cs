using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

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
            var guid = packet.ReadGuid("GUID");

            packet.ReadEnum<TrainerType>("Type", TypeCode.Int32);

            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                var spell = packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);

                packet.ReadEnum<TrainerSpellState>("State", TypeCode.Byte, i);

                var cost = packet.ReadInt32("Cost", i);

                packet.ReadInt32("Profession Dialog", i);

                packet.ReadInt32("Profession Button", i);

                var reqLevel = packet.ReadByte("Required Level", i);

                var reqSkill = packet.ReadInt32("Required Skill", i);

                var reqSkLvl = packet.ReadInt32("Required Skill Level", i);

                packet.ReadInt32("Chain Node 1", i);

                packet.ReadInt32("Chain Node 2", i);

                packet.ReadInt32("Unk Int32", i);

                SQLStore.WriteData(SQLStore.TrainerSpells.GetCommand(guid.GetEntry(), spell, cost, reqLevel,
                    reqSkill, reqSkLvl));
            }

            packet.ReadCString("Title");
        }

        [Parser(Opcode.SMSG_LIST_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            var guid = packet.ReadBits(48);

            var itemCount = packet.ReadInt32();
            packet.Writer.WriteLine("Item Count: " + itemCount);

            var itemCountunk = packet.ReadByte();
            packet.Writer.WriteLine("itemCountunk: " + itemCountunk);

            var itemCountunk1 = packet.ReadByte();
            packet.Writer.WriteLine("itemCountunk1: " + itemCountunk1);

            for (var i = 0; i < itemCount; i++)
            {
                var price = packet.ReadInt32();
                packet.Writer.WriteLine("Price " + i + ": " + price);

                var unk = packet.ReadInt32();
                packet.Writer.WriteLine("unk " + i + ": " + unk);

                var unk1 = packet.ReadInt32();
                packet.Writer.WriteLine("unk1 " + i + ": " + unk1);

                var maxDura = packet.ReadInt32();
                packet.Writer.WriteLine("Max Durability " + i + ": " + maxDura);

                var extendedCost = packet.ReadInt32();
                packet.Writer.WriteLine("Extended Cost " + i + ": " + extendedCost);

                var buyCount = packet.ReadInt32();
                packet.Writer.WriteLine("Buy Count " + i + ": " + buyCount);

                var maxCount = packet.ReadInt32();
                packet.Writer.WriteLine("Max Count " + i + ": " + maxCount);

                var position = packet.ReadInt32();
                packet.Writer.WriteLine("Item Position " + position + ": " + position);

                var dispid = packet.ReadInt32();
                packet.Writer.WriteLine("Display ID " + i + ": " + dispid);

                var itemId = packet.ReadInt32();
                packet.Writer.WriteLine("Item ID " + i + ": " + itemId);

                SQLStore.WriteData(SQLStore.VendorItems.GetCommand(guid, itemId, maxCount,
                    extendedCost));
            }
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
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Menu id");
            packet.ReadUInt32("Text id");

            var count = packet.ReadUInt32("Amount of Options");

            for (var i = 0; i < count; i++)
            {
                packet.ReadUInt32("Index", i);
                packet.ReadByte("Icon", i);
                packet.ReadBoolean("Box", i);
                packet.ReadUInt32("Required money", i);
                packet.ReadCString("Text", i);
                packet.ReadCString("Box Text", i);
            }

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
