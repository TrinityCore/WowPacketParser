using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class BattlePayHandler
    {
        [Parser(Opcode.CMSG_BATTLE_PAY_GET_PURCHASE_LIST_QUERY)]
        public static void HandleZeroLengthPackets(Packet packet)
        {
        }

        private static void ReadBattlepayDisplayInfo(ref Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();

            var bit4 = packet.ReadBit("HasCreatureDisplayInfoID", indexes);
            var bit12 = packet.ReadBit("HasFileDataID", indexes);

            var bits16 = packet.ReadBits(10);
            var bits529 = packet.ReadBits(10);
            var bits1042 = packet.ReadBits(13);

            var bit5144 = packet.ReadBit("HasFlags", indexes);

            if (bit4)
                packet.ReadInt32("CreatureDisplayInfoID", indexes);

            if (bit12)
                packet.ReadInt32("FileDataID", indexes);

            packet.ReadWoWString("Name1", bits16, indexes);
            packet.ReadWoWString("Name2", bits529, indexes);
            packet.ReadWoWString("Name3", bits1042, indexes);

            if (bit5144)
                packet.ReadInt32("Flags", indexes);
        }

        private static void ReadBattlePayProduct(ref Packet packet, params object[] indexes)
        {
            packet.ReadInt32("ProductID", indexes);

            packet.ReadInt64("NormalPriceFixedPoint", indexes);
            packet.ReadInt64("CurrentPriceFixedPoint", indexes);

            var int11 = packet.ReadInt32("BattlepayProductItemCount", indexes);

            packet.ReadByte("Type", indexes);
            packet.ReadInt32("Flags", indexes);

            for (int j = 0; j < int11; j++)
            {
                packet.ReadInt32("ID", indexes, j);
                packet.ReadInt32("ItemID", indexes, j);
                packet.ReadInt32("Quantity", indexes, j);

                packet.ResetBitReader();

                var bit5160 = packet.ReadBit("HasBattlepayDisplayInfo", indexes, j);
                packet.ReadBit("HasPet", indexes, j);
                var bit5172 = packet.ReadBit("HasBATTLEPETRESULT", indexes, j);

                if (bit5172)
                    packet.ReadBits("PetResult", 4);

                if (bit5160)
                    ReadBattlepayDisplayInfo(ref packet, indexes);
            }

            packet.ResetBitReader();

            packet.ReadBits("ChoiceType", 2, indexes);

            var bit5196 = packet.ReadBit("HasBattlepayDisplayInfo", indexes);
            if (bit5196)
                ReadBattlepayDisplayInfo(ref packet, indexes);
        }

        private static void ReadBattlePayDistributionObject(ref Packet packet, params object[] indexes)
        {
            packet.ReadInt64("DistributionID", indexes);

            packet.ReadInt32("Status", indexes);
            packet.ReadInt32("ProductID", indexes);

            packet.ReadPackedGuid128("TargetPlayer", indexes);
            packet.ReadInt32("TargetVirtualRealm", indexes);
            packet.ReadInt32("TargetNativeRealm", indexes);

            packet.ReadInt64("PurchaseID", indexes);

            packet.ResetBitReader();

            var bit5248 = packet.ReadBit("HasBattlePayProduct", indexes);

            packet.ReadBit("Revoked", indexes);

            if (bit5248)
                ReadBattlePayProduct(ref packet, indexes);
        }

        [Parser(Opcode.SMSG_BATTLE_PAY_GET_PURCHASE_LIST_RESPONSE)]
        public static void HandleBattlePayGetPurchaseListResponse(Packet packet)
        {
            packet.ReadUInt32("Result");

            var int6 = packet.ReadUInt32("BattlePayPurchaseCount");

            for (int i = 0; i < int6; i++)
            {
                packet.ReadInt64("PurchaseID", i);
                packet.ReadInt32("Status", i);
                packet.ReadInt32("ResultCode", i);
                packet.ReadInt32("ProductID", i);

                packet.ResetBitReader();

                var bits20 = packet.ReadBits(8);
                packet.ReadWoWString("WalletName", bits20, i);
            }
        }

        [Parser(Opcode.SMSG_BATTLE_PAY_GET_DISTRIBUTION_LIST_RESPONSE)]
        public static void HandleBattlePayGetDistributionListResponse(Packet packet)
        {
            packet.ReadUInt32("Result");

            var int6 = packet.ReadUInt32("BattlePayDistributionObjectCount");

            for (uint index = 0; index < int6; index++)
                ReadBattlePayDistributionObject(ref packet, index);
        }

        [Parser(Opcode.SMSG_BATTLE_PAY_DISTRIBUTION_UPDATE)]
        public static void HandleBattlePayDistributionUpdate(Packet packet)
        {
            ReadBattlePayDistributionObject(ref packet);
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
                ReadBattlePayProduct(ref packet, index);

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
                    ReadBattlepayDisplayInfo(ref packet, i);
            }
        }
    }
}
