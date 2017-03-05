using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class LootHandler
    {
        [Parser(Opcode.SMSG_LOOT_MONEY_NOTIFY)]
        public static void HandleLootMoneyNotify(Packet packet)
        {
            packet.Translator.ReadUInt32("Gold");

            if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing)) // no idea when this was added, doesn't exist in 2.4.1
                packet.Translator.ReadBool("Solo Loot"); // true = YOU_LOOT_MONEY, false = LOOT_MONEY_SPLIT

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623) && ClientVersion.RemovedInVersion(ClientVersionBuild.V4_3_0_15005)) // remove confirmed for 430
                packet.Translator.ReadUInt32("Guild Gold");
        }

        [Parser(Opcode.CMSG_LOOT_UNIT, ClientVersionBuild.Zero, ClientVersionBuild.V5_1_0_16309)]
        [Parser(Opcode.CMSG_LOOT_RELEASE, ClientVersionBuild.Zero, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleLoot(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_LOOT_UNIT, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleLoot510(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(1, 2, 7, 3, 6, 0, 4, 5);
            packet.Translator.ParseBitStream(guid, 1, 3, 5, 4, 0, 7, 6, 2);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_LOOT_RELEASE, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleLootRelease510(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(4, 0, 6, 2, 3, 7, 1, 5);
            packet.Translator.ParseBitStream(guid, 0, 4, 1, 6, 7, 5, 3, 2);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_LOOT_MASTER_GIVE)]
        public static void HandleLootMasterGive(Packet packet)
        {
            packet.Translator.ReadGuid("Loot GUID");
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadGuid("Player GUID");
        }

        [Parser(Opcode.CMSG_SET_LOOT_METHOD)]
        public static void HandleLootMethod(Packet packet)
        {
            packet.Translator.ReadUInt32E<LootMethod>("Loot Method");
            packet.Translator.ReadGuid("Master GUID");
            packet.Translator.ReadUInt32E<ItemQuality>("Loot Threshold");
        }

        [Parser(Opcode.CMSG_OPT_OUT_OF_LOOT)]
        public static void HandleOptOutOfLoot(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595))
                packet.Translator.ReadBool("Always Pass");
            else
                packet.Translator.ReadUInt32("Always Pass");
        }

        [Parser(Opcode.SMSG_LOOT_ALL_PASSED)]
        public static void HandleLootAllPassed(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadUInt32("Slot");
            packet.Translator.ReadUInt32<ItemId>("Entry");
            packet.Translator.ReadInt32("Random Property Id");
            packet.Translator.ReadInt32("Random Suffix");
        }

        [Parser(Opcode.SMSG_LOOT_LIST)]
        public static void Handle(Packet packet)
        {
            packet.Translator.ReadGuid("Creature GUID");
            packet.Translator.ReadPackedGuid("Master Loot GUID?");
            packet.Translator.ReadPackedGuid("Looter GUID");
        }

        [Parser(Opcode.SMSG_LOOT_RELEASE)]
        public static void HandleLootReleaseResponse(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadBool("Unk Bool"); // true calls CGUnit_C::UpdateLootAnimKit and CGameUI::CloseLoot
        }

        [Parser(Opcode.SMSG_LOOT_REMOVED)]
        public static void HandleLootRemoved(Packet packet)
        {
            packet.Translator.ReadByte("Slot");
        }

        [Parser(Opcode.SMSG_LOOT_RESPONSE)]
        public static void HandleLootResponse(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            var lootType = packet.Translator.ReadByteE<LootType>("Loot Type");
            if (lootType == LootType.None)
            {
                packet.Translator.ReadByte("Slot");
                return;
            }

            var count = packet.Translator.ReadByte("Drop Count");

            byte currencyCount = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                currencyCount = packet.Translator.ReadByte("Currency Count");

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadByte("Slot", i);
                packet.Translator.ReadUInt32<ItemId>("Entry", i);
                packet.Translator.ReadUInt32("Count", i);
                packet.Translator.ReadUInt32("Display ID", i);
                packet.Translator.ReadInt32("Random Suffix", i);
                packet.Translator.ReadInt32("Random Property Id", i);
                packet.Translator.ReadByteE<LootSlotType>("Slot Type", i);
            }

            for (int i = 0; i < currencyCount; ++i)
            {
                packet.Translator.ReadByte("Slot", i);
                packet.Translator.ReadInt32("Currency Id", i);
                packet.Translator.ReadInt32("Count", i); // unconfirmed
            }
        }

        [Parser(Opcode.CMSG_LOOT_ROLL)]
        public static void HandleLootRoll(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadUInt32("Slot");
            packet.Translator.ReadByteE<LootRollType>("Roll Type");
        }

        [Parser(Opcode.SMSG_LOOT_ROLL)]
        public static void HandleLootRollResponse(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadUInt32("Slot");
            packet.Translator.ReadGuid("Player GUID");
            packet.Translator.ReadUInt32<ItemId>("Entry");
            packet.Translator.ReadInt32("Random Property Id");
            packet.Translator.ReadInt32("Random Suffix");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                packet.Translator.ReadInt32("Roll Number");
            else
                packet.Translator.ReadByte("Roll Number");
            packet.Translator.ReadByteE<LootRollType>("Roll Type");
            packet.Translator.ReadBool("Auto Pass");
        }

        [Parser(Opcode.SMSG_LOOT_ROLL_WON)]
        public static void HandleLootWon(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadUInt32("Slot");
            packet.Translator.ReadUInt32<ItemId>("Entry");
            packet.Translator.ReadInt32("Random Property Id");
            packet.Translator.ReadInt32("Random Suffix");
            packet.Translator.ReadGuid("Player GUID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                packet.Translator.ReadInt32("Roll Number");
            else
                packet.Translator.ReadByte("Roll Number");
            packet.Translator.ReadByteE<LootRollType>("Roll Type");
        }

        [Parser(Opcode.SMSG_LOOT_START_ROLL)]
        public static void HandleStartLoot(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadInt32<MapId>("Map ID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685)) // probably earlier
                packet.Translator.ReadUInt32("Slot");
            packet.Translator.ReadUInt32<ItemId>("Entry");
            packet.Translator.ReadInt32("Random Suffix");
            packet.Translator.ReadInt32("Random Property Id");
            packet.Translator.ReadUInt32("Count");
            packet.Translator.ReadUInt32("Roll time");
            packet.Translator.ReadByteE<LootVoteFlags>("Roll Vote Mask");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_0_14333))
                packet.Translator.ReadByte("unk"); //amount of players? need verification.
        }

        [Parser(Opcode.SMSG_LOOT_SLOT_CHANGED)]
        public static void HandleLootSlotChanged(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadUInt32<ItemId>("Entry");
            packet.Translator.ReadUInt32("Display ID");
            packet.Translator.ReadInt32("Unk UInt32 1");
            packet.Translator.ReadInt32("Unk UInt32 2"); // only seen 0
            packet.Translator.ReadUInt32("Count");
        }

        [Parser(Opcode.SMSG_LOOT_MASTER_LIST)]
        public static void HandleLootMasterList(Packet packet)
        {
            var count = packet.Translator.ReadByte("Count");
            for (var i = 0; i < count; i++)
                packet.Translator.ReadGuid("GUID", i);
        }

        [Parser(Opcode.SMSG_LOOT_CONTENTS)] //4.3.4
        public static void HandleLootContents(Packet packet)
        {
            var count1 = packet.Translator.ReadBits("Loot Items Count", 21);
            for (var i = 0; i < count1; i++)
            {
                packet.Translator.ReadUInt32("Display ID", i);
                packet.Translator.ReadInt32("Random Suffix Factor", i);
                packet.Translator.ReadInt32("Item Count", i);
                packet.Translator.ReadUInt32<ItemId>("Item Entry", i);
                packet.Translator.ReadInt32("Unk Int32", i); // possibly random property id or looted count
            }
        }

        [Parser(Opcode.CMSG_LOOT_CURRENCY)]
        [Parser(Opcode.SMSG_CURRENCY_LOOT_REMOVED)]
        public static void HandleLootCurrency(Packet packet)
        {
            packet.Translator.ReadByte("Slot");
        }

        [Parser(Opcode.SMSG_LOOT_CLEAR_MONEY)]
        [Parser(Opcode.CMSG_LOOT_MONEY)]
        public static void HandleNullLoot(Packet packet)
        {
        }
    }
}
