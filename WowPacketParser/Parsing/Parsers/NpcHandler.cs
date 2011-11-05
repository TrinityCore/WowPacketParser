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
            Console.WriteLine("Flags: 0x" + flags.ToString("X8"));

            var pos = packet.ReadVector2();
            Console.WriteLine("Coordinates: " + pos);

            var icon = (GossipPoiIcon)packet.ReadInt32();
            Console.WriteLine("Icon: " + icon);

            var data = packet.ReadInt32();
            Console.WriteLine("Data: " + data);

            var iconName = packet.ReadCString();
            Console.WriteLine("Icon Name: " + iconName);
        }

        [Parser(Opcode.SMSG_TRAINER_BUY_SUCCEEDED)]
        public static void HandleServerTrainerBuySucceedeed(Packet packet)
        {
            packet.ReadGuid("GUID");
            Console.WriteLine("Spell ID: " + StoreGetters.GetName(StoreNameType.Spell, packet.ReadInt32()));
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            var guid = packet.ReadGuid("GUID");

            var type = (TrainerType)packet.ReadInt32();
            Console.WriteLine("Type: " + type);

            var count = packet.ReadInt32();
            Console.WriteLine("Count: " + count);

            for (var i = 0; i < count; i++)
            {
                var spell = packet.ReadInt32();
                Console.WriteLine("Spell ID " + i + ": " + StoreGetters.GetName(StoreNameType.Spell, spell));

                var state = (TrainerSpellState)packet.ReadByte();
                Console.WriteLine("State " + i + ": " + state);

                var cost = packet.ReadInt32();
                Console.WriteLine("Cost " + i + ": " + cost);

                var profDialog = packet.ReadInt32();
                Console.WriteLine("Profession Dialog " + i + ": " + profDialog);

                var profButton = packet.ReadInt32();
                Console.WriteLine("Profession Button " + i + ": " + profButton);

                var reqLevel = packet.ReadByte();
                Console.WriteLine("Required Level " + i + ": " + reqLevel);

                var reqSkill = packet.ReadInt32();
                Console.WriteLine("Required Skill " + i + ": " + reqSkill);

                var reqSkLvl = packet.ReadInt32();
                Console.WriteLine("Required Skill Level " + i + ": " + reqSkLvl);

                var chainNode1 = packet.ReadInt32();
                Console.WriteLine("Chain Node 1 " + i + ": " + chainNode1);

                var chainNode2 = packet.ReadInt32();
                Console.WriteLine("Chain Node 2 " + i + ": " + chainNode2);

                var unk = packet.ReadInt32();
                Console.WriteLine("Unk Int32 " + i + ": " + unk);

                SQLStore.WriteData(SQLStore.TrainerSpells.GetCommand(guid.GetEntry(), spell, cost, reqLevel,
                    reqSkill, reqSkLvl));
            }

            var titleStr = packet.ReadCString();
            Console.WriteLine("Title: " + titleStr);
        }

        [Parser(Opcode.SMSG_LIST_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            var guid = packet.ReadGuid("GUID");

            var itemCount = packet.ReadByte();
            Console.WriteLine("Item Count: " + itemCount);

            for (var i = 0; i < itemCount; i++)
            {
                var position = packet.ReadInt32();
                Console.WriteLine("Item Position " + position + ": " + position);

                var itemId = packet.ReadInt32();
                Console.WriteLine("Item ID " + i + ": " + itemId);

                var dispid = packet.ReadInt32();
                Console.WriteLine("Display ID " + i + ": " + dispid);

                var maxCount = packet.ReadInt32();
                Console.WriteLine("Max Count " + i + ": " + maxCount);

                var price = packet.ReadInt32();
                Console.WriteLine("Price " + i + ": " + price);

                var maxDura = packet.ReadInt32();
                Console.WriteLine("Max Durability " + i + ": " + maxDura);

                var buyCount = packet.ReadInt32();
                Console.WriteLine("Buy Count " + i + ": " + buyCount);

                var extendedCost = packet.ReadInt32();
                Console.WriteLine("Extended Cost " + i + ": " + extendedCost);

                SQLStore.WriteData(SQLStore.VendorItems.GetCommand(guid.GetEntry(), itemId, maxCount,
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

            var count = packet.ReadUInt32("- Amount of Options");

            for (var i = 0; i < count; i++)
            {
                if (i != 0)
                    Console.WriteLine("\t--");

                var index = packet.ReadUInt32();
                Console.WriteLine("\tIndex: " + index);

                var icon = packet.ReadByte();
                Console.WriteLine("\tIcon: " + icon);

                var box = packet.ReadBoolean();
                Console.WriteLine("\tBox: " + box);

                var boxMoney = packet.ReadUInt32();
                if (box) // Only print if there's a box. avaliable.
                    Console.WriteLine("\tRequired money: " + boxMoney);

                var text = packet.ReadCString();
                Console.WriteLine("\tText: " + text);

                var boxText = packet.ReadCString();
                if (box) // Only print if there's a box avaliable.
                    Console.WriteLine("\tBox text: " + boxText);
            }

            var questgossips = packet.ReadUInt32();
            Console.WriteLine("- Amount of Quest gossips: " + questgossips);

            for (var i = 0; i < questgossips; i++)
            {
                if (i != 0)
                    Console.WriteLine("\t--");

                var questID = packet.ReadUInt32();
                Console.WriteLine("\tQuest ID: " + questID);

                var questicon = packet.ReadUInt32();
                Console.WriteLine("\tIcon: " + questicon);

                var questlevel = packet.ReadInt32();
                Console.WriteLine("\tLevel: " + questlevel);

                var flags = (QuestFlag)(packet.ReadUInt32() | 0xFFFF);
                Console.WriteLine("\tFlags: " + flags);

                var unk1 = packet.ReadBoolean();
                Console.WriteLine("\tUnknown bool: " + unk1);

                var title = packet.ReadCString();
                Console.WriteLine("\tTitle: " + title);
            }
        }


        [Parser(Opcode.SMSG_THREAT_UPDATE)]
        [Parser(Opcode.SMSG_HIGHEST_THREAT_UPDATE)]
        public static void HandleThreatlistUpdate(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            Console.WriteLine("GUID: " + guid);

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_HIGHEST_THREAT_UPDATE))
            {
                var newhigh = packet.ReadPackedGuid();
                Console.WriteLine("New Highest: " + newhigh);
            }

            var count = packet.ReadUInt32();
            Console.WriteLine("Size: " + count);
            for (int i = 0; i < count; i++)
            {
                packet.ReadPackedGuid("Hostile");
                var threat = packet.ReadUInt32();
                // No idea why, but this is in core.
                /*if (packet.Opcode == Opcode.SMSG_THREAT_UPDATE)
                    threat *= 100;*/
                Console.WriteLine("Threat: " + threat);
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
