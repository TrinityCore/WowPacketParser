using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class TradeHandler
    {
        [Parser(Opcode.CMSG_INITIATE_TRADE)]
        public static void HandleInitiateTrade(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_SET_TRADE_ITEM)]
        public static void HandleTradeItem(Packet packet)
        {
            packet.ReadByte("Trade Slot");
            packet.ReadSByte("Bag");
            packet.ReadByte("Slot");
        }

        [Parser(Opcode.CMSG_CLEAR_TRADE_ITEM)]
        public static void HandleClearTradeItem(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596)) // Need correct version
                packet.ReadInt32("Slot");
            else
                packet.ReadByte("Slot");
        }

        [Parser(Opcode.CMSG_SET_TRADE_GOLD)]
        public static void HandleTradeGold(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596)) // Need correct version
                packet.ReadUInt64("Gold");
            else
                packet.ReadUInt32("Gold");
        }

        [Parser(Opcode.SMSG_TRADE_STATUS, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleTradeStatus422(Packet packet)
        {
            var guid = new byte[8];
            guid[2] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[1] = (byte)(packet.ReadBit() ? 1 : 0);
            packet.ReadBit("Unk Bit");
            guid[6] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[0] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[5] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[7] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[4] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[3] = (byte)(packet.ReadBit() ? 1 : 0);

            if (guid[5] != 0) guid[5] ^= packet.ReadByte();
            if (guid[2] != 0) guid[2] ^= packet.ReadByte();

            packet.ReadUInt32("Unk 1");
            packet.ReadByte("Unk 2");

            if (guid[7] != 0) guid[7] ^= packet.ReadByte();

            packet.ReadInt32("Unk 3");
            packet.ReadUInt32("Unk 4");

            if (guid[6] != 0) guid[6] ^= packet.ReadByte();

            packet.ReadUInt32("Unk 5");
            packet.ReadByte("Unk 6");

            if (guid[4] != 0) guid[4] ^= packet.ReadByte();

            packet.ReadUInt32("Unk 7");

            if (guid[3] != 0) guid[3] ^= packet.ReadByte();
            if (guid[1] != 0) guid[1] ^= packet.ReadByte();
            if (guid[0] != 0) guid[0] ^= packet.ReadByte();

            packet.ReadUInt32("Unk 8");

            packet.WriteLine("Guid: {0}", new Guid(BitConverter.ToUInt64(guid, 0)));
        }

        [Parser(Opcode.SMSG_TRADE_STATUS, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleTradeStatus(Packet packet)
        {
            var status = packet.ReadEnum<TradeStatus>("Status", TypeCode.UInt32);
            switch (status)
            {
                case TradeStatus.BeginTrade:
                    packet.ReadGuid("GUID");
                    break;
                case TradeStatus.OpenWindow:
                    packet.ReadUInt32("Trade Id");
                    break;
                case TradeStatus.CloseWindow:
                    packet.ReadUInt32("Unk UInt32 1");
                    packet.ReadByte("Unk Byte");
                    packet.ReadUInt32("Unk UInt32 2");
                    break;
                case TradeStatus.OnlyConjured:
                case TradeStatus.NotEligible:
                    packet.ReadByte("Unk Byte");
                    break;
            }
        }

        [Parser(Opcode.SMSG_TRADE_STATUS_EXTENDED, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleTradeStatusExtended422(Packet packet)
        {
            packet.AsHex();
            packet.ReadInt32("Unk 1");
            packet.ReadInt32("Unk 2");
            packet.ReadInt32("Unk 3");
            packet.ReadInt32("Unk 4");
            packet.ReadUInt64("Gold");
            packet.ReadByte("Trader?");
            var count = packet.ReadInt32("Unk Count");
            packet.ReadInt32("Unk 7");
            packet.ReadInt32("Unk 8");

            var guids1 = new byte[count][];
            var guids2 = new byte[count][];
            for (var i = 0; i < count; ++i)
            {
                guids1[i] = new byte[8];
                guids2[i] = new byte[8];
            }

            for (var i = 0; i < count; ++i)
            {
                guids1[i][0] = (byte)(packet.ReadBit() ? 1 : 0);
                guids1[i][5] = (byte)(packet.ReadBit() ? 1 : 0);
                guids1[i][7] = (byte)(packet.ReadBit() ? 1 : 0);
                guids1[i][1] = (byte)(packet.ReadBit() ? 1 : 0);
                guids1[i][6] = (byte)(packet.ReadBit() ? 1 : 0);

                guids2[i][5] = (byte)(packet.ReadBit() ? 1 : 0);
                guids2[i][3] = (byte)(packet.ReadBit() ? 1 : 0);
                guids2[i][0] = (byte)(packet.ReadBit() ? 1 : 0);
                guids2[i][6] = (byte)(packet.ReadBit() ? 1 : 0);
                guids2[i][2] = (byte)(packet.ReadBit() ? 1 : 0);
                guids2[i][4] = (byte)(packet.ReadBit() ? 1 : 0);
                guids2[i][1] = (byte)(packet.ReadBit() ? 1 : 0);

                guids1[i][3] = (byte)(packet.ReadBit() ? 1 : 0);

                guids2[i][7] = (byte)(packet.ReadBit() ? 1 : 0);

                guids1[i][2] = (byte)(packet.ReadBit() ? 1 : 0);
                guids1[i][4] = (byte)(packet.ReadBit() ? 1 : 0);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Unk 1", i);

                if (guids2[i][0] != 0)
                    guids2[i][0] ^= packet.ReadByte();

                if (guids2[i][3] != 0)
                    guids2[i][3] ^= packet.ReadByte();

                if (guids2[i][4] != 0)
                    guids2[i][4] ^= packet.ReadByte();

                packet.ReadInt32("Unk 2", i);

                if (guids1[i][7] != 0)
                    guids1[i][7] ^= packet.ReadByte();

                packet.ReadInt32("Item Id", i);
                packet.ReadInt32("Unk 4", i);
                packet.ReadInt32("Unk 5", i);

                if (guids2[i][2] != 0)
                    guids2[i][2] ^= packet.ReadByte();

                if (guids2[i][5] != 0)
                    guids2[i][5] ^= packet.ReadByte();

                packet.ReadInt32("Unk 6", i);

                if (guids1[i][1] != 0)
                    guids1[i][1] ^= packet.ReadByte();

                if (guids2[i][6] != 0)
                    guids2[i][6] ^= packet.ReadByte();

                if (guids1[i][0] != 0)
                    guids1[i][0] ^= packet.ReadByte();

                packet.ReadInt32("Unk 7", i);
                packet.ReadUInt32("Unk 8", i);
                packet.ReadInt32("Unk 9", i);

                if (guids1[i][5] != 0)
                    guids1[i][5] ^= packet.ReadByte();

                packet.ReadInt32("Unk 10", i);

                if (guids1[i][6] != 0)
                    guids1[i][6] ^= packet.ReadByte();

                if (guids2[i][7] != 0)
                    guids2[i][7] ^= packet.ReadByte();

                packet.ReadInt32("Unk 11", i);
                packet.ReadByte("Unk 12", i);

                if (guids2[i][1] != 0)
                    guids2[i][1] ^= packet.ReadByte();

                packet.ReadInt32("Unk 13", i);
                packet.ReadInt32("Unk 14", i);
                packet.ReadByte("Unk 15", i);

                if (guids1[i][4] != 0)
                    guids1[i][4] ^= packet.ReadByte();

                if (guids1[i][2] != 0)
                    guids1[i][2] ^= packet.ReadByte();

                packet.ReadInt32("Unk 16", i);

                if (guids1[i][3] != 0)
                    guids1[i][3] ^= packet.ReadByte();

                packet.WriteLine("Item Creator Guid: {0}", new Guid(BitConverter.ToUInt64(guids1[i], 0)));
                packet.WriteLine("Item Gift Creator Guid: {0}", new Guid(BitConverter.ToUInt64(guids2[i], 0)));
            }


        }

        [Parser(Opcode.SMSG_TRADE_STATUS_EXTENDED, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleTradeStatusExtended(Packet packet)
        {
            packet.ReadByte("Trader");
            packet.ReadUInt32("Trade Id");
            packet.ReadUInt32("Unk Slot 1");
            packet.ReadUInt32("Unk Slot 2");
            packet.ReadUInt32("Gold");
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");

            while (packet.CanRead())
            {
                var slot = packet.ReadByte("Slot Index");
                packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Item Entry", slot);
                packet.ReadUInt32("Item Display ID", slot);
                packet.ReadUInt32("Item Count", slot);
                packet.ReadUInt32("Item Wrapped", slot);
                packet.ReadGuid("Item Gift Creator GUID", slot);
                packet.ReadUInt32("Item Perm Enchantment Id", slot);
                for (var i = 0; i < 3; ++i)
                    packet.ReadUInt32("Item Enchantment Id", slot, i);
                packet.ReadGuid("Item Creator GUID", slot);
                packet.ReadInt32("Item Spell Charges", slot);
                packet.ReadInt32("Item Suffix Factor", slot);
                packet.ReadInt32("Item Random Property ID", slot);
                packet.ReadUInt32("Item Lock ID", slot);
                packet.ReadUInt32("Item Max Durability", slot);
                packet.ReadUInt32("Item Durability", slot);
            }
        }

        [Parser(Opcode.CMSG_ACCEPT_TRADE)]
        public static void HandleAcceptTrade(Packet packet)
        {
            packet.ReadUInt32("Unk UInt32");
        }

        [Parser(Opcode.CMSG_BEGIN_TRADE)]
        public static void HandleBeginTrade(Packet packet)
        {
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_2_14545))
                return;

            var guid = new byte[8];
            guid[5] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[6] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[4] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[0] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[2] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[3] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[7] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[1] = (byte)(packet.ReadBit() ? 1 : 0);

            if (guid[5] != 0)
                guid[5] ^= packet.ReadByte();

            if (guid[2] != 0)
                guid[2] ^= packet.ReadByte();

            if (guid[3] != 0)
                guid[3] ^= packet.ReadByte();

            if (guid[4] != 0)
                guid[4] ^= packet.ReadByte();

            if (guid[1] != 0)
                guid[1] ^= packet.ReadByte();

            if (guid[0] != 0)
                guid[0] ^= packet.ReadByte();

            if (guid[6] != 0)
                guid[6] ^= packet.ReadByte();

            if (guid[7] != 0)
                guid[7] ^= packet.ReadByte();

            packet.WriteLine("Guid: {0}", new Guid(BitConverter.ToUInt64(guid, 0)));
        }

        [Parser(Opcode.CMSG_IGNORE_TRADE)]
        [Parser(Opcode.CMSG_BUSY_TRADE)]
        [Parser(Opcode.CMSG_CANCEL_TRADE)]
        [Parser(Opcode.CMSG_UNACCEPT_TRADE)]
        public static void HandleNullTrade(Packet packet)
        {
        }
    }
}
