using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class BattlePayHandler
    {
        [Parser(Opcode.CMSG_BATTLE_PAY_GET_PRODUCT_LIST)]
        [Parser(Opcode.CMSG_BATTLE_PAY_GET_PURCHASE_LIST)]
        public static void HandleZeroLengthPackets(Packet packet)
        {
        }

        private static void ReadBattlepayDisplayInfo(Packet packet, params object[] idx)
        {
            packet.Translator.ResetBitReader();

            var bit4 = packet.Translator.ReadBit("HasCreatureDisplayInfoID", idx);
            var bit12 = packet.Translator.ReadBit("HasFileDataID", idx);

            var bits16 = packet.Translator.ReadBits(10);
            var bits529 = packet.Translator.ReadBits(10);
            var bits1042 = packet.Translator.ReadBits(13);

            var bit5144 = packet.Translator.ReadBit("HasFlags", idx);

            if (bit4)
                packet.Translator.ReadInt32("CreatureDisplayInfoID", idx);

            if (bit12)
                packet.Translator.ReadInt32("FileDataID", idx);

            packet.Translator.ReadWoWString("Name1", bits16, idx);
            packet.Translator.ReadWoWString("Name2", bits529, idx);
            packet.Translator.ReadWoWString("Name3", bits1042, idx);

            if (bit5144)
                packet.Translator.ReadInt32("Flags", idx);
        }

        private static void ReadBattlePayProduct60x(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("ProductID", idx);

            packet.Translator.ReadInt64("NormalPriceFixedPoint", idx);
            packet.Translator.ReadInt64("CurrentPriceFixedPoint", idx);

            var int11 = packet.Translator.ReadInt32("BattlepayProductItemCount", idx);

            packet.Translator.ReadByte("Type", idx);
            packet.Translator.ReadInt32("Flags", idx);

            for (int j = 0; j < int11; j++)
            {
                packet.Translator.ReadInt32("ID", idx, j);
                packet.Translator.ReadInt32("ItemID", idx, j);
                packet.Translator.ReadInt32("Quantity", idx, j);

                packet.Translator.ResetBitReader();

                var bit5160 = packet.Translator.ReadBit("HasBattlepayDisplayInfo", idx, j);
                packet.Translator.ReadBit("HasPet", idx, j);
                var bit5172 = packet.Translator.ReadBit("HasBATTLEPETRESULT", idx, j);

                if (bit5172)
                    packet.Translator.ReadBits("PetResult", 4);

                if (bit5160)
                    ReadBattlepayDisplayInfo(packet, idx);
            }

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBits("ChoiceType", 2, idx);

            var bit5196 = packet.Translator.ReadBit("HasBattlepayDisplayInfo", idx);
            if (bit5196)
                ReadBattlepayDisplayInfo(packet, idx);
        }
        private static void ReadBattlePayProduct612(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("ProductID", idx);

            packet.Translator.ReadInt64("NormalPriceFixedPoint", idx);
            packet.Translator.ReadInt64("CurrentPriceFixedPoint", idx);

            packet.Translator.ReadByte("Type", idx);
            packet.Translator.ReadInt32("Flags", idx);

            packet.Translator.ResetBitReader();

            var int11 = packet.Translator.ReadBits("BattlepayProductItemCount", 7, idx);
            packet.Translator.ReadBits("UnkWod612 1", 7, idx);

            var bit5196 = packet.Translator.ReadBit("HasBattlepayDisplayInfo", idx);

            for (var j = 0; j < int11; j++)
            {
                packet.Translator.ReadInt32("ID", idx, j);
                packet.Translator.ReadInt32("ItemID", idx, j);
                packet.Translator.ReadInt32("Quantity", idx, j);

                packet.Translator.ResetBitReader();

                var bit5160 = packet.Translator.ReadBit("HasBattlepayDisplayInfo", idx, j);
                packet.Translator.ReadBit("HasPet", idx, j);
                var bit5172 = packet.Translator.ReadBit("HasBATTLEPETRESULT", idx, j);

                if (bit5172)
                    packet.Translator.ReadBits("PetResult", 4);

                if (bit5160)
                    ReadBattlepayDisplayInfo(packet, idx);
            }

            if (bit5196)
                ReadBattlepayDisplayInfo(packet, idx);
        }

        private static void ReadBattlePayDistributionObject(Packet packet, params object[] index)
        {
            packet.Translator.ReadInt64("DistributionID", index);

            packet.Translator.ReadInt32("Status", index);
            packet.Translator.ReadInt32("ProductID", index);

            packet.Translator.ReadPackedGuid128("TargetPlayer", index);
            packet.Translator.ReadInt32("TargetVirtualRealm", index);
            packet.Translator.ReadInt32("TargetNativeRealm", index);

            packet.Translator.ReadInt64("PurchaseID", index);

            packet.Translator.ResetBitReader();

            var bit5248 = packet.Translator.ReadBit("HasBattlePayProduct", index);

            packet.Translator.ReadBit("Revoked", index);

            if (bit5248)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_2_19802))
                    ReadBattlePayProduct612(packet, index);
                else
                    ReadBattlePayProduct60x(packet, index);
            }
        }

        private static void ReadBattlePayPurchase(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt64("PurchaseID", indexes);
            packet.Translator.ReadInt32("Status", indexes);
            packet.Translator.ReadInt32("ResultCode", indexes);
            packet.Translator.ReadInt32("ProductID", indexes);

            packet.Translator.ResetBitReader();

            var bits20 = packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("WalletName", bits20, indexes);
        }

        [Parser(Opcode.SMSG_BATTLE_PAY_GET_PURCHASE_LIST_RESPONSE)]
        public static void HandleBattlePayGetPurchaseListResponse(Packet packet)
        {
            packet.Translator.ReadUInt32("Result");

            var int6 = packet.Translator.ReadUInt32("BattlePayPurchaseCount");

            for (int i = 0; i < int6; i++)
                ReadBattlePayPurchase(packet, i);
        }

        [Parser(Opcode.SMSG_BATTLE_PAY_GET_DISTRIBUTION_LIST_RESPONSE)]
        public static void HandleBattlePayGetDistributionListResponse(Packet packet)
        {
            packet.Translator.ReadUInt32("Result");

            var int6 = ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173) ?
                packet.Translator.ReadBits("BattlePayDistributionObjectCount", 11) : packet.Translator.ReadUInt32("BattlePayDistributionObjectCount");

            for (uint index = 0; index < int6; index++)
                ReadBattlePayDistributionObject(packet, index);
        }

        [Parser(Opcode.SMSG_BATTLE_PAY_DISTRIBUTION_UPDATE)]
        public static void HandleBattlePayDistributionUpdate(Packet packet)
        {
            ReadBattlePayDistributionObject(packet);
        }

        [Parser(Opcode.SMSG_BATTLE_PAY_GET_PRODUCT_LIST_RESPONSE)]
        public static void HandletBattlePayGetProductListResponse(Packet packet)
        {
            packet.Translator.ReadUInt32("Result");
            packet.Translator.ReadUInt32("CurrencyID");

            var int52 = packet.Translator.ReadUInt32("BattlePayDistributionObjectCount");
            var int36 = packet.Translator.ReadUInt32("BattlePayProductGroupCount");
            var int20 = packet.Translator.ReadUInt32("BattlePayShopEntryCount");

            for (uint index = 0; index < int52; index++)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_2_19802))
                    ReadBattlePayProduct612(packet, index);
                else
                    ReadBattlePayProduct60x(packet, index);
            }

            for (int i = 0; i < int36; i++)
            {
                packet.Translator.ReadInt32("GroupID", i);
                packet.Translator.ReadInt32("IconFileDataID", i);
                packet.Translator.ReadByte("DisplayType", i);
                packet.Translator.ReadInt32("Ordering", i);

                packet.Translator.ResetBitReader();

                var bits4 = packet.Translator.ReadBits(8);
                packet.Translator.ReadWoWString("Name", bits4, i);
            }

            for (uint i = 0; i < int20; i++)
            {
                packet.Translator.ReadUInt32("EntryID", i);
                packet.Translator.ReadUInt32("GroupID", i);
                packet.Translator.ReadUInt32("ProductID", i);
                packet.Translator.ReadInt32("Ordering", i);
                packet.Translator.ReadInt32("Flags", i);
                packet.Translator.ReadByte("BannerType", i);

                packet.Translator.ResetBitReader();

                var bit5172 = packet.Translator.ReadBit("HasBattlepayDisplayInfo", i);
                if (bit5172)
                    ReadBattlepayDisplayInfo(packet, i);
            }
        }

        [Parser(Opcode.CMSG_BATTLE_PAY_START_PURCHASE)]
        public static void HandleBattlePayStartPurchase(Packet packet)
        {
            packet.Translator.ReadInt32("ClientToken");
            packet.Translator.ReadInt32("ProductID");
            packet.Translator.ReadPackedGuid128("TargetCharacter");
        }

        [Parser(Opcode.SMSG_BATTLE_PAY_START_PURCHASE_RESPONSE)]
        public static void HandleBattlePayStartPurchaseResponse(Packet packet)
        {
            packet.Translator.ReadUInt64("PurchaseID");
            packet.Translator.ReadInt32("ClientToken");
            packet.Translator.ReadInt32("PurchaseResult");
        }

        [Parser(Opcode.SMSG_BATTLE_PAY_PURCHASE_UPDATE)]
        public static void HandleBattlePayPurchaseUpdate(Packet packet)
        {

            var battlePayPurchaseCount = packet.Translator.ReadUInt32("BattlePayPurchaseCount");
            for (int i = 0; i < battlePayPurchaseCount; i++)
                ReadBattlePayPurchase(packet, i);
        }

        [Parser(Opcode.SMSG_BATTLE_PAY_CONFIRM_PURCHASE)]
        public static void HandleBattlePayConfirmPurchase(Packet packet)
        {
            packet.Translator.ReadInt64("PurchaseID");
            packet.Translator.ReadInt64("CurrentPriceFixedPoint");
            packet.Translator.ReadInt32("ServerToken");
        }

        [Parser(Opcode.CMSG_BATTLE_PAY_CONFIRM_PURCHASE_RESPONSE)]
        public static void HandleBattlePayConfirmPurchaseResponse(Packet packet)
        {
            packet.Translator.ReadBit("ConfirmPurchase");
            packet.Translator.ReadInt32("ServerToken");
            packet.Translator.ReadInt64("ClientCurrentPriceFixedPoint");
        }

        [Parser(Opcode.SMSG_BATTLE_PAY_DELIVERY_ENDED)]
        public static void HandleBattlePayDeliveryEnded(Packet packet)
        {
            packet.Translator.ReadInt64("DistributionID");

            var itemCount = packet.Translator.ReadInt32("ItemCount");
            for (int i = 0; i < itemCount; i++)
                ItemHandler.ReadItemInstance(packet, i);
        }

        [Parser(Opcode.CMSG_BATTLE_PAY_DISTRIBUTION_ASSIGN_TO_TARGET)]
        public static void HandleBattlePayDistributionAssignToTarget(Packet packet)
        {
            packet.Translator.ReadInt32("ClientToken");
            packet.Translator.ReadInt64("DistributionID");
            packet.Translator.ReadPackedGuid128("TargetCharacter");
            packet.Translator.ReadInt32("ProductChoice");
        }

        [Parser(Opcode.SMSG_CHARACTER_UPGRADE_STARTED)]
        public static void HandleCharacterUpgradeStarted(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("CharacterGUID");
        }
    }
}
