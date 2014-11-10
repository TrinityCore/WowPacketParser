using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class LfgHandler
    {
        public static void ReadRideTicket(ref Packet packet)
        {
            packet.ReadPackedGuid128("RequesterGuid");
            packet.ReadInt32("Id");
            packet.ReadInt32("Type");
            packet.ReadTime("Time");
        }

        [Parser(Opcode.SMSG_LFG_PLAYER_INFO)]
        public static void HandleLfgPlayerLockInfoResponse(Packet packet)
        {
            var int16 = packet.ReadInt32("DungeonCount");

            // LFGBlackList
            packet.ResetBitReader();
            var bit16 = packet.ReadBit("HasPlayerGuid");
            var int24 = packet.ReadInt32("LFGBlackListCount");

            if (bit16)
                packet.ReadPackedGuid128("PlayerGuid");

            for (var i = 0; i < int24; ++i)
            {
                packet.ReadInt32("Slot", i);
                packet.ReadInt32("Reason", i);
                packet.ReadInt32("SubReason1", i);
                packet.ReadInt32("SubReason2", i);
            }

            // LfgPlayerDungeonInfo
            for (var i = 0; i < int16; ++i)
            {
                packet.ReadInt32("Slot", i);
                packet.ReadInt32("CompletionQuantity", i);
                packet.ReadInt32("CompletionLimit", i);
                packet.ReadInt32("CompletionCurrencyID", i);
                packet.ReadInt32("SpecificQuantity", i);
                packet.ReadInt32("SpecificLimit", i);
                packet.ReadInt32("OverallQuantity", i);
                packet.ReadInt32("OverallLimit", i);
                packet.ReadInt32("PurseWeeklyQuantity", i);
                packet.ReadInt32("PurseWeeklyLimit", i);
                packet.ReadInt32("PurseQuantity", i);
                packet.ReadInt32("PurseLimit", i);
                packet.ReadInt32("Quantity", i);
                packet.ReadInt32("CompletedMask", i);

                var int64 = packet.ReadInt32("ShortageRewardCount", i);

                // Rewards
                packet.ReadUInt32("Mask", i);
                packet.ReadInt32("RewardMoney", i);
                packet.ReadInt32("RewardXP", i);

                var int20 = packet.ReadInt32("ItemCount", i);
                var int36 = packet.ReadInt32("CurrencyCount", i);
                var int52 = packet.ReadInt32("BonusCurrencyCount", i);

                // Item
                for (var j = 0; j < int20; ++j)
                {
                    packet.ReadInt32("ItemID", i, j);
                    packet.ReadInt32("Quantity", i, j);
                }

                // Currency
                for (var j = 0; j < int36; ++j)
                {
                    packet.ReadInt32("CurrencyID", i, j);
                    packet.ReadInt32("Quantity", i, j);
                }

                // BonusCurrency
                for (var j = 0; j < int52; ++j)
                {
                    packet.ReadInt32("CurrencyID", i, j);
                    packet.ReadInt32("Quantity", i, j);
                }

                packet.ResetBitReader();

                var bit3 = packet.ReadBit("hasBit3", i);
                if (bit3)
                    packet.ReadInt32("Unk 1", i);

                // ShortageReward
                for (var j = 0; j < int64; ++j)
                {
                    // Rewards
                    packet.ReadInt32("Mask", i, j);
                    packet.ReadInt32("RewardMoney", i, j);
                    packet.ReadInt32("RewardXP", i, j);

                    var int200 = packet.ReadInt32("ItemCount", i, j);
                    var int360 = packet.ReadInt32("CurrencyCount", i, j);
                    var int520 = packet.ReadInt32("QuantityCount", i, j);

                    // Item
                    for (var k = 0; k < int200; ++k)
                    {
                        packet.ReadInt32("ItemID", i, j, k);
                        packet.ReadInt32("Quantity", i, j, k);
                    }

                    // Currency
                    for (var k = 0; k < int360; ++k)
                    {
                        packet.ReadInt32("CurrencyID", i, j, k);
                        packet.ReadInt32("Quantity", i, j, k);
                    }

                    // BonusCurrency
                    for (var k = 0; k < int520; ++k)
                    {
                        packet.ReadInt32("CurrencyID", i, j, k);
                        packet.ReadInt32("Quantity", i, j, k);
                    }

                    packet.ResetBitReader();

                    var bit30 = packet.ReadBit("HasBit30", i, j);
                    if (bit30)
                        packet.ReadInt32("Unk 2", i, j);
                }

                packet.ResetBitReader();

                packet.ReadBit("FirstReward", i);
                packet.ReadBit("ShortageEligible", i);
            }
        }

        [Parser(Opcode.SMSG_LFG_JOIN_RESULT)]
        public static void HandleLfgJoinResult(Packet packet)
        {
            ReadRideTicket(ref packet);

            packet.ReadByte("Result");
            packet.ReadByte("ResultDetail");

            var int16 = packet.ReadInt32("BlackListCount");
            for (int i = 0; i < int16; i++)
            {
                packet.ReadPackedGuid128("Guid", i);

                var int160 = packet.ReadInt32("SlotsCount", i);

                for (int j = 0; j < int160; j++)
                {
                    packet.ReadInt32("Slot", i, j);
                    packet.ReadInt32("Reason", i, j);
                    packet.ReadInt32("SubReason1", i, j);
                    packet.ReadInt32("SubReason2", i, j);
                }
            }
        }

        [Parser(Opcode.SMSG_LFG_UPDATE_STATUS)]
        public static void HandleLfgQueueStatusUpdate(Packet packet)
        {
            ReadRideTicket(ref packet);

            packet.ReadByte("SubType");
            packet.ReadByte("Reason");

            for (int i = 0; i < 3; i++)
                packet.ReadByte("Needs", i);

            var int8 = packet.ReadInt32("SlotsCount");
            packet.ReadInt32("RequestedRoles");
            var int4 = packet.ReadInt32("SuspendedPlayersCount");

            for (int i = 0; i < int8; i++)
                packet.ReadInt32("Slots", i);

            for (int i = 0; i < int4; i++)
                packet.ReadPackedGuid128("SuspendedPlayers", i);

            packet.ResetBitReader();

            packet.ReadBit("IsParty");
            packet.ReadBit("NotifyUI");
            packet.ReadBit("Joined");
            packet.ReadBit("LfgJoined");
            packet.ReadBit("Queued");

            var bits56 = packet.ReadBits(8);
            packet.ReadWoWString("Comment", bits56);
        }
    }
}
