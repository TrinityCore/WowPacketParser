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

        private static void ReadBattlepayDisplayInfo(ref Packet packet, uint index)
        {
            packet.ResetBitReader();

            var bit4 = packet.ReadBit("HasCreatureDisplayInfoID", index);
            var bit12 = packet.ReadBit("HasFileDataID", index);

            var bits16 = packet.ReadBits(10);
            var bits529 = packet.ReadBits(10);
            var bits1042 = packet.ReadBits(13);

            var bit5144 = packet.ReadBit("HasFlags", index);

            if (bit4)
                packet.ReadInt32("CreatureDisplayInfoID", index);

            if (bit12)
                packet.ReadInt32("FileDataID", index);

            packet.ReadWoWString("Name1", bits16, index);
            packet.ReadWoWString("Name2", bits529, index);
            packet.ReadWoWString("Name3", bits1042, index);

            if (bit5144)
                packet.ReadInt32("Flags", index);
        }

        private static void ReadBattlePayProduct(ref Packet packet, uint index)
        {
            packet.ReadInt32("ProductID", index);

            packet.ReadInt64("NormalPriceFixedPoint", index);
            packet.ReadInt64("CurrentPriceFixedPoint", index);

            var int11 = packet.ReadInt32("BattlepayProductItemCount", index);

            packet.ReadByte("Type", index);
            packet.ReadInt32("Flags", index);

            for (int j = 0; j < int11; j++)
            {
                packet.ReadInt32("ID", index , j);
                packet.ReadInt32("ItemID", index, j);
                packet.ReadInt32("Quantity", index, j);

                packet.ResetBitReader();

                var bit5160 = packet.ReadBit("HasBattlepayDisplayInfo", index, j);
                packet.ReadBit("HasPet", index, j);
                var bit5172 = packet.ReadBit("HasBATTLEPETRESULT", index, j);

                if (bit5172)
                    packet.ReadBits("PetResult", 4);

                if (bit5160)
                    ReadBattlepayDisplayInfo(ref packet, index);
            }

            packet.ResetBitReader();

            packet.ReadBits("ChoiceType", 2, index);

            var bit5196 = packet.ReadBit("HasBattlepayDisplayInfo", index);
            if (bit5196)
                ReadBattlepayDisplayInfo(ref packet, index);
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
            {

                packet.ReadInt64("DistributionID",  index);

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
                    ReadBattlePayProduct(ref packet, index);
            }
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
