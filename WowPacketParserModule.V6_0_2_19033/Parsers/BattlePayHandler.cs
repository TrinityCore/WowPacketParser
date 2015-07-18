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
            packet.ResetBitReader();

            var bit4 = packet.ReadBit("HasCreatureDisplayInfoID", idx);
            var bit12 = packet.ReadBit("HasFileDataID", idx);

            var bits16 = packet.ReadBits(10);
            var bits529 = packet.ReadBits(10);
            var bits1042 = packet.ReadBits(13);

            var bit5144 = packet.ReadBit("HasFlags", idx);

            if (bit4)
                packet.ReadInt32("CreatureDisplayInfoID", idx);

            if (bit12)
                packet.ReadInt32("FileDataID", idx);

            packet.ReadWoWString("Name1", bits16, idx);
            packet.ReadWoWString("Name2", bits529, idx);
            packet.ReadWoWString("Name3", bits1042, idx);

            if (bit5144)
                packet.ReadInt32("Flags", idx);
        }

        private static void ReadBattlePayProduct60x(Packet packet, params object[] idx)
        {
            packet.ReadInt32("ProductID", idx);

            packet.ReadInt64("NormalPriceFixedPoint", idx);
            packet.ReadInt64("CurrentPriceFixedPoint", idx);

            var int11 = packet.ReadInt32("BattlepayProductItemCount", idx);

            packet.ReadByte("Type", idx);
            packet.ReadInt32("Flags", idx);

            for (int j = 0; j < int11; j++)
            {
                packet.ReadInt32("ID", idx, j);
                packet.ReadInt32("ItemID", idx, j);
                packet.ReadInt32("Quantity", idx, j);

                packet.ResetBitReader();

                var bit5160 = packet.ReadBit("HasBattlepayDisplayInfo", idx, j);
                packet.ReadBit("HasPet", idx, j);
                var bit5172 = packet.ReadBit("HasBATTLEPETRESULT", idx, j);

                if (bit5172)
                    packet.ReadBits("PetResult", 4);

                if (bit5160)
                    ReadBattlepayDisplayInfo(packet, idx);
            }

            packet.ResetBitReader();

            packet.ReadBits("ChoiceType", 2, idx);

            var bit5196 = packet.ReadBit("HasBattlepayDisplayInfo", idx);
            if (bit5196)
                ReadBattlepayDisplayInfo(packet, idx);
        }
        private static void ReadBattlePayProduct612(Packet packet, params object[] idx)
        {
            packet.ReadInt32("ProductID", idx);

            packet.ReadInt64("NormalPriceFixedPoint", idx);
            packet.ReadInt64("CurrentPriceFixedPoint", idx);

            packet.ReadByte("Type", idx);
            packet.ReadInt32("Flags", idx);

            packet.ResetBitReader();

            var int11 = packet.ReadBits("BattlepayProductItemCount", 7, idx);
            packet.ReadBits("UnkWod612 1", 7, idx);

            var bit5196 = packet.ReadBit("HasBattlepayDisplayInfo", idx);

            for (var j = 0; j < int11; j++)
            {
                packet.ReadInt32("ID", idx, j);
                packet.ReadInt32("ItemID", idx, j);
                packet.ReadInt32("Quantity", idx, j);

                packet.ResetBitReader();

                var bit5160 = packet.ReadBit("HasBattlepayDisplayInfo", idx, j);
                packet.ReadBit("HasPet", idx, j);
                var bit5172 = packet.ReadBit("HasBATTLEPETRESULT", idx, j);

                if (bit5172)
                    packet.ReadBits("PetResult", 4);

                if (bit5160)
                    ReadBattlepayDisplayInfo(packet, idx);
            }

            if (bit5196)
                ReadBattlepayDisplayInfo(packet, idx);
        }

        private static void ReadBattlePayDistributionObject(Packet packet, params object[] index)
        {
            packet.ReadInt64("DistributionID", index);

            packet.ReadInt32("Status", index);
            packet.ReadInt32("ProductID", index);

            packet.ReadPackedGuid128("TargetPlayer", index);
            packet.ReadInt32("TargetVirtualRealm", index);
            packet.ReadInt32("TargetNativeRealm", index);

            packet.ReadInt64("PurchaseID", index);

            packet.ResetBitReader();

            var bit5248 = packet.ReadBit("HasBattlePayProduct", index);

            packet.ReadBit("Revoked", index);

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
            packet.ReadInt64("PurchaseID", indexes);
            packet.ReadInt32("Status", indexes);
            packet.ReadInt32("ResultCode", indexes);
            packet.ReadInt32("ProductID", indexes);

            packet.ResetBitReader();

            var bits20 = packet.ReadBits(8);
            packet.ReadWoWString("WalletName", bits20, indexes);
        }

        [Parser(Opcode.SMSG_BATTLE_PAY_GET_PURCHASE_LIST_RESPONSE)]
        public static void HandleBattlePayGetPurchaseListResponse(Packet packet)
        {
            packet.ReadUInt32("Result");

            var int6 = packet.ReadUInt32("BattlePayPurchaseCount");

            for (int i = 0; i < int6; i++)
                ReadBattlePayPurchase(packet, i);
        }

        [Parser(Opcode.SMSG_BATTLE_PAY_GET_DISTRIBUTION_LIST_RESPONSE)]
        public static void HandleBattlePayGetDistributionListResponse(Packet packet)
        {
            packet.ReadUInt32("Result");

            var int6 = ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173) ?
                packet.ReadBits("BattlePayDistributionObjectCount", 11) : packet.ReadUInt32("BattlePayDistributionObjectCount");

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
            packet.ReadUInt32("Result");
            packet.ReadUInt32("CurrencyID");

            var int52 = packet.ReadUInt32("BattlePayDistributionObjectCount");
            var int36 = packet.ReadUInt32("BattlePayProductGroupCount");
            var int20 = packet.ReadUInt32("BattlePayShopEntryCount");

            for (uint index = 0; index < int52; index++)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_2_19802))
                    ReadBattlePayProduct612(packet, index);
                else
                    ReadBattlePayProduct60x(packet, index);
            }

            for (int i = 0; i < int36; i++)
            {
                packet.ReadInt32("GroupID", i);
                packet.ReadInt32("IconFileDataID", i);
                packet.ReadByte("DisplayType", i);
                packet.ReadInt32("Ordering", i);

                packet.ResetBitReader();

                var bits4 = packet.ReadBits(8);
                packet.ReadWoWString("Name", bits4, i);
            }

            for (uint i = 0; i < int20; i++)
            {
                packet.ReadUInt32("EntryID", i);
                packet.ReadUInt32("GroupID", i);
                packet.ReadUInt32("ProductID", i);
                packet.ReadInt32("Ordering", i);
                packet.ReadInt32("Flags", i);
                packet.ReadByte("BannerType", i);

                packet.ResetBitReader();

                var bit5172 = packet.ReadBit("HasBattlepayDisplayInfo", i);
                if (bit5172)
                    ReadBattlepayDisplayInfo(packet, i);
            }
        }

        [Parser(Opcode.CMSG_BATTLE_PAY_START_PURCHASE)]
        public static void HandleBattlePayStartPurchase(Packet packet)
        {
            packet.ReadInt32("ClientToken");
            packet.ReadInt32("ProductID");
            packet.ReadPackedGuid128("TargetCharacter");
        }

        [Parser(Opcode.SMSG_BATTLE_PAY_START_PURCHASE_RESPONSE)]
        public static void HandleBattlePayStartPurchaseResponse(Packet packet)
        {
            packet.ReadUInt64("PurchaseID");
            packet.ReadInt32("ClientToken");
            packet.ReadInt32("PurchaseResult");
        }

        [Parser(Opcode.SMSG_BATTLE_PAY_PURCHASE_UPDATE)]
        public static void HandleBattlePayPurchaseUpdate(Packet packet)
        {

            var battlePayPurchaseCount = packet.ReadUInt32("BattlePayPurchaseCount");
            for (int i = 0; i < battlePayPurchaseCount; i++)
                ReadBattlePayPurchase(packet, i);
        }

        [Parser(Opcode.SMSG_BATTLE_PAY_CONFIRM_PURCHASE)]
        public static void HandleBattlePayConfirmPurchase(Packet packet)
        {
            packet.ReadInt64("PurchaseID");
            packet.ReadInt64("CurrentPriceFixedPoint");
            packet.ReadInt32("ServerToken");
        }

        [Parser(Opcode.CMSG_BATTLE_PAY_CONFIRM_PURCHASE_RESPONSE)]
        public static void HandleBattlePayConfirmPurchaseResponse(Packet packet)
        {
            packet.ReadBit("ConfirmPurchase");
            packet.ReadInt32("ServerToken");
            packet.ReadInt64("ClientCurrentPriceFixedPoint");
        }

        [Parser(Opcode.SMSG_BATTLE_PAY_DELIVERY_ENDED)]
        public static void HandleBattlePayDeliveryEnded(Packet packet)
        {
            packet.ReadInt64("DistributionID");

            var itemCount = packet.ReadInt32("ItemCount");
            for (int i = 0; i < itemCount; i++)
                ItemHandler.ReadItemInstance(packet, i);
        }
    }
}
