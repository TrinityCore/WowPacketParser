using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class GuildHandler
    {
        private static void ReadEmblemInfo(ref Packet packet)
        {
            packet.ReadInt32("Emblem Style");
            packet.ReadInt32("Emblem Color");
            packet.ReadInt32("Emblem Border Style");
            packet.ReadInt32("Emblem Border Color");
            packet.ReadInt32("Emblem Background Color");
        }

        [Parser(Opcode.CMSG_GUILD_ROSTER)]
        [Parser(Opcode.CMSG_GUILD_ACCEPT)]
        [Parser(Opcode.CMSG_GUILD_DECLINE)]
        [Parser(Opcode.CMSG_GUILD_INFO)]
        [Parser(Opcode.CMSG_GUILD_LEAVE)]
        [Parser(Opcode.CMSG_GUILD_DISBAND)]
        [Parser(Opcode.CMSG_GUILD_DEL_RANK)]
        [Parser(Opcode.SMSG_GUILD_CANCEL)] // Fires GUILD_INVITE_CANCEL
        public static void HandleGuildEmpty(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_GUILD_ROSTER, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleGuildRoster406(Packet packet)
        {
            packet.ReadGuid("Guild GUID");
            packet.ReadGuid("Player GUID");
        }

        [Parser(Opcode.CMSG_GUILD_ROSTER, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleGuildRoster(Packet packet)
        {
            var bits = new bool[8];
            for (int c = 7; c >= 0; c--)
                bits[c] = packet.ReadBit();

            var bytes = new byte[8];
            if (bits[0]) bytes[7] = (byte)(packet.ReadByte() ^ 1);
            if (bits[5]) bytes[4] = (byte)(packet.ReadByte() ^ 1);
            if (bits[4]) bytes[5] = (byte)(packet.ReadByte() ^ 1);
            if (bits[7]) bytes[0] = (byte)(packet.ReadByte() ^ 1);
            if (bits[3]) bytes[1] = (byte)(packet.ReadByte() ^ 1);
            if (bits[2]) bytes[2] = (byte)(packet.ReadByte() ^ 1);
            if (bits[1]) bytes[6] = (byte)(packet.ReadByte() ^ 1);
            if (bits[6]) bytes[3] = (byte)(packet.ReadByte() ^ 1);
            packet.WriteLine("GUID: {0}", new Guid(BitConverter.ToUInt64(bytes, 0)));
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER)]
        public static void HandleGuildRosterResponse(Packet packet)
        {
            var size = packet.ReadUInt32("Number Of Members");
            packet.ReadCString("MOTD");
            packet.ReadCString("Info");

            var numFields = packet.ReadInt32("Number Of Ranks");
            for (var i = 0; i < numFields; i++)
            {
                packet.ReadEnum<GuildRankRightsFlag>("Rights", TypeCode.UInt32, i);
                packet.ReadInt32("Money Per Day", i);

                for (var j = 0; j < 6; j++)
                {
                    packet.ReadEnum<GuildBankRightsFlag>("Tab Rights", TypeCode.UInt32, i, j);
                    packet.ReadInt32("Tab Slots", i, j);
                }
            }

            for (var i = 0; i < size; i++)
            {
                packet.ReadGuid("GUID", i);
                var online = packet.ReadBoolean("Online", i);

                packet.ReadCString("Name", i);
                packet.ReadUInt32("Rank Id", i);
                packet.ReadByte("Level", i);
                packet.ReadByte("Class", i);
                packet.ReadByte("Unk", i);
                packet.ReadEntryWithName<Int32>(StoreNameType.Zone, "Zone Id", i);

                if (!online)
                    packet.ReadUInt32("Last Online", i);

                packet.ReadCString("Public Note", i);
                packet.ReadCString("Officer Note", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleGuildRoster422(Packet packet)
        {
            packet.AsHex(); // FIXME
        }

        [Parser(Opcode.SMSG_COMPRESSED_GUILD_ROSTER)]
        public static void HandleCompressedGuildRoster(Packet packet)
        {
            using (var packet2 = packet.Inflate(packet.ReadInt32()))
            {
                HandleGuildRoster422(packet2);
            }
        }

        [Parser(Opcode.CMSG_GUILD_UPDATE_PARTY_STATE)]
        public static void HandleGuildUpdatePartyState(Packet packet)
        {
            packet.ReadGuid("Guild GUID");
            packet.ReadGuid("Player GUID");
        }

        [Parser(Opcode.CMSG_GUILD_QUERY)]
        public static void HandleGuildQuery(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596)) // Not sure when it was changed
            {
                packet.ReadGuid("Guild GUID");
                packet.ReadGuid("Player GUID");
            }
            else
                packet.ReadUInt32("Guild Id");
        }

        [Parser(Opcode.SMSG_GUILD_QUERY_RESPONSE)]
        public static void HandleGuildQueryResponse(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596)) // Not sure when it was changed
                packet.ReadGuid("Guild GUID");
            else
                packet.ReadUInt32("Guild Id");

            packet.ReadCString("Guild Name");
            for (var i = 0; i < 10; i++)
                packet.ReadCString("Rank Name", i);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596)) // Not sure when it was changed
            {
                for (var i = 0; i < 10; i++)
                    packet.ReadUInt32("Creation Order", i);

                for (var i = 0; i < 10; i++)
                    packet.ReadUInt32("Rights Order", i);
            }

            ReadEmblemInfo(ref packet);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.ReadUInt32("Ranks");
        }

        [Parser(Opcode.CMSG_GUILD_RANK)]
        public static void HandleGuildRank(Packet packet)
        {
            packet.ReadUInt32("Rank Id");
            packet.ReadEnum<GuildRankRightsFlag>("Rights", TypeCode.UInt32);
            packet.ReadCString("Name");
            packet.ReadInt32("Money Per Day");
            for (var i = 0; i < 6; i++)
            {
                packet.ReadEnum<GuildBankRightsFlag>("Tab Rights", TypeCode.UInt32, i);
                packet.ReadInt32("Tab Slots", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_RANK)]
        public static void HandleGuildRankServer(Packet packet)
        {
            const int guildBankMaxTabs = 8;

            var count = packet.ReadUInt32("Rank Count");
            for (var i = 0; i < count; i++)
            {
                packet.ReadUInt32("Creation Order", i);
                packet.ReadUInt32("Rights Order", i);
                packet.ReadCString("Name", i);
                packet.ReadEnum<GuildRankRightsFlag>("Rights", TypeCode.Int32, i);

                for (int j = 0; j < guildBankMaxTabs; j++)
                    packet.ReadEnum<GuildBankRightsFlag>("Tab Rights", TypeCode.Int32, i, j);

                for (int j = 0; j < guildBankMaxTabs; j++)
                    packet.ReadInt32("Tab Slots", i, j);

                packet.ReadInt32("Gold Per Day", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_RANK, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleGuildRankServer422(Packet packet)
        {
            const int guildBankMaxTabs = 8;

            var count = packet.ReadUInt32("Rank Count");
            for (int i = 0; i < count; i++)
            {
                packet.ReadCString("Name", i);
                packet.ReadInt32("Creation Order", i);

                for (int j = 0; j < guildBankMaxTabs; j++)
                    packet.ReadEnum<GuildBankRightsFlag>("Tab Rights", TypeCode.Int32, i, j);

                packet.ReadInt32("Gold Per Day", i);

                for (int j = 0; j < guildBankMaxTabs; j++)
                    packet.ReadInt32("Tab Slots", i, j);

                packet.ReadInt32("Rights Order", i);
                packet.ReadEnum<GuildRankRightsFlag>("Rights", TypeCode.Int32, i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_CREATE)]
        [Parser(Opcode.CMSG_GUILD_INVITE)]
        [Parser(Opcode.CMSG_GUILD_PROMOTE)]
        [Parser(Opcode.CMSG_GUILD_DEMOTE)]
        [Parser(Opcode.CMSG_GUILD_REMOVE)]
        [Parser(Opcode.CMSG_GUILD_LEADER)]
        [Parser(Opcode.CMSG_GUILD_ADD_RANK)]
        public static void HandleGuildCreate(Packet packet)
        {
            packet.ReadCString("Name");
        }

        [Parser(Opcode.SMSG_GUILD_INVITE)]
        public static void HandleGuildInvite(Packet packet)
        {
            packet.ReadCString("Invitee Name");
            packet.ReadCString("Guild Name");
        }

        [Parser(Opcode.SMSG_GUILD_INFO)]
        public static void HandleGuildInfo(Packet packet)
        {
            packet.ReadCString("Name");
            packet.ReadPackedTime("Creation Date");
            packet.ReadUInt32("Number of Players");
            packet.ReadUInt32("Number of Accounts");
        }

        [Parser(Opcode.CMSG_GUILD_MOTD)]
        public static void HandleGuildMOTD(Packet packet)
        {
            packet.ReadCString("MOTD");
        }

        [Parser(Opcode.SMSG_GUILD_EVENT)]
        public static void HandleGuildEvent(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.ReadEnum<GuildEventType442>("Event Type", TypeCode.Byte);
            else
                packet.ReadEnum<GuildEventType>("Event Type", TypeCode.Byte);

            var size = packet.ReadByte("Param Count");
            for (var i = 0; i < size; i++)
                packet.ReadCString("Param", i);

            if (packet.CanRead()) // FIXME 4 5 6 16 17 (GuildEventType changed for 4.2.2)
                packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_GUILD_COMMAND_RESULT)]
        public static void HandleGuildCommandResult(Packet packet)
        {
            packet.ReadEnum<GuildCommandType>("Command Type", TypeCode.UInt32);
            packet.ReadCString("Param");
            packet.ReadEnum<GuildCommandError>("Command Result", TypeCode.UInt32);
        }

        [Parser(Opcode.MSG_SAVE_GUILD_EMBLEM)]
        public static void HandleGuildEmblem(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadGuid("GUID");
                ReadEmblemInfo(ref packet);
            }
            else
                packet.ReadEnum<GuildEmblemError>("Result", TypeCode.UInt32);
        }

        [Parser(Opcode.CMSG_GUILD_SET_PUBLIC_NOTE)]
        public static void HandleGuildSetPublicNote(Packet packet)
        {
            packet.ReadCString("Player Name");
            packet.ReadCString("Public Note");
        }

        [Parser(Opcode.CMSG_GUILD_SET_OFFICER_NOTE)]
        public static void HandleGuildSetOfficerNote(Packet packet)
        {
            packet.ReadCString("Player Name");
            packet.ReadCString("Officer Note");
        }

        [Parser(Opcode.CMSG_GUILD_INFO_TEXT)]
        public static void HandleGuildInfoText(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
            {
                packet.ReadGuid("Player GUID");
                packet.ReadGuid("Guild GUID");
            }
            packet.ReadCString("Text");
        }

        [Parser(Opcode.CMSG_GUILD_BANKER_ACTIVATE)]
        public static void HandleGuildBankerActivate(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadBoolean("Full Slot List");
        }

        [Parser(Opcode.CMSG_GUILD_BANK_QUERY_TAB)]
        public static void HandleGuildBankQueryTab(Packet packet)
        {
            packet.ReadGuid("GUID");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.ReadByte("Tab Id");
            packet.ReadBoolean("Full Slot List"); // false = only slots updated in last operation are shown. True = all slots updated
        }

        [Parser(Opcode.SMSG_GUILD_BANK_LIST)]
        public static void HandleGuildBankList(Packet packet)
        {
            packet.ReadUInt64("Money");
            var tabId = packet.ReadByte("Tab Id");
            packet.ReadInt32("Remaining Withdraw");
            if (packet.ReadBoolean("Full Slot List") && tabId == 0)
            {
                var size = packet.ReadByte("Number of Tabs");
                for (var i = 0; i < size; i++)
                {
                    packet.ReadCString("Tab Name", i);
                    packet.ReadCString("Tab Icon", i);
                }
            }

            var slots = packet.ReadByte("Number of Slots");
            for (var i = 0; i < slots; i++)
            {
                packet.ReadByte("Slot Id", i);
                var entry = packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Item Entry", i);
                if (entry > 0)
                {
                    packet.ReadEnum<ItemFlag>("Item Flags?", TypeCode.UInt32, i);
                    var ramdonEnchant = packet.ReadInt32("Random Item Property Id", i);
                    if (ramdonEnchant != 0)
                        packet.ReadUInt32("Item Suffix Factor", i);
                    packet.ReadUInt32("Stack Count", i);
                    packet.ReadUInt32("Unk Uint32 2", i); // Only seen 0
                    packet.ReadByte("Spell Charges", i);
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                    {
                        packet.ReadInt32("Unk 1 Int32", i);
                        packet.ReadInt32("Unk 2 Int32", i);
                    }

                    var enchantment = packet.ReadByte("Number of Enchantments", i);
                    for (var j = 0; j < enchantment; j++)
                    {
                        packet.ReadByte("Enchantment Slot Id", i, j);
                        packet.ReadUInt32("Enchantment Id", i, j);
                    }
                }
            }
        }

        [Parser(Opcode.CMSG_GUILD_BANK_SWAP_ITEMS)]
        public static void HandleGuildBankSwapItems(Packet packet)
        {
            packet.ReadGuid("GUID");
            var bankToBank = packet.ReadBoolean("BankToBank");
            if (bankToBank)
            {
                packet.ReadByte("Dest Tab Id");
                packet.ReadByte("Dest Slot Id");
                packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Dest Item Entry");
                packet.ReadByte("Tab Id");
                packet.ReadByte("Slot Id");
                packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Item Entry");
                packet.ReadByte("Unk Byte 1");
                packet.ReadUInt32("Amount");
            }
            else
            {
                packet.ReadByte("Tab Id");
                packet.ReadByte("Slot Id");
                packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Item Entry");
                var autostore = packet.ReadBoolean("Autostore");
                if (autostore)
                {
                    packet.ReadUInt32("Autostore Count");
                    packet.ReadBoolean("From Bank To Player");
                    packet.ReadUInt32("Unk Uint32 2");
                }
                else
                {
                    packet.ReadByte("Bag");
                    packet.ReadByte("Slot");
                    packet.ReadBoolean("From Bank To Player");
                    packet.ReadUInt32("Amount");
                }
            }
        }

        [Parser(Opcode.CMSG_GUILD_BANK_BUY_TAB)]
        public static void HandleGuildBankBuyTab(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByte("Tab Id");
        }

        [Parser(Opcode.CMSG_GUILD_BANK_UPDATE_TAB)]
        public static void HandleGuildBankUpdateTab(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByte("Tab Id");
            packet.ReadCString("Tab Name");
            packet.ReadCString("Tab Icon");
        }

        [Parser(Opcode.CMSG_GUILD_BANK_DEPOSIT_MONEY)]
        [Parser(Opcode.CMSG_GUILD_BANK_WITHDRAW_MONEY)]
        public static void HandleGuildBankDepositMoney(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Money");
        }

        [Parser(Opcode.MSG_QUERY_GUILD_BANK_TEXT)]
        public static void HandleGuildQueryBankText(Packet packet)
        {
            packet.ReadByte("Tab Id");
            if (packet.Direction == Direction.ServerToClient)
                packet.ReadCString("Text");
        }

        [Parser(Opcode.CMSG_SET_GUILD_BANK_TEXT)]
        public static void HandleGuildSetBankText(Packet packet)
        {
            packet.ReadByte("Tab Id");
            packet.ReadCString("Tab Text");
        }

        [Parser(Opcode.MSG_GUILD_PERMISSIONS)]
        public static void HandleGuildPermissions(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
                return;

            packet.ReadUInt32("Rank Id");
            packet.ReadEnum<GuildRankRightsFlag>("Rights", TypeCode.UInt32);
            packet.ReadInt32("Remaining Money");
            packet.ReadByte("Tab size");
            var tabSize = ClientVersion.AddedInVersion(ClientType.Cataclysm) ? 8 : 6;
            for (var i = 0; i < tabSize; i++)
            {
                packet.ReadEnum<GuildBankRightsFlag>("Tab Rights", TypeCode.Int32, i);
                packet.ReadInt32("Tab Slots", i);
            }
        }

        [Parser(Opcode.MSG_GUILD_BANK_MONEY_WITHDRAWN)]
        public static void HandleGuildBankMoneyWithdrawn(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                packet.ReadInt32("Remaining Money");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                    packet.ReadInt32("Guild Money");
            }
        }

        [Parser(Opcode.MSG_GUILD_EVENT_LOG_QUERY)]
        public static void HandleGuildEventLogQuery(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
                return;

            var size = packet.ReadByte("Log size");
            for (var i = 0; i < size; i++)
            {
                var type = packet.ReadEnum<GuildEventLogType>("Type", TypeCode.Byte);
                packet.ReadGuid("GUID");
                if (type != GuildEventLogType.JoinGuild && type != GuildEventLogType.LeaveGuild)
                    packet.ReadGuid("GUID 2");
                if (type == GuildEventLogType.PromotePlayer || type == GuildEventLogType.DemotePlayer)
                    packet.ReadByte("Rank");
                packet.ReadUInt32("Time Ago");
            }
        }

        [Parser(Opcode.MSG_GUILD_BANK_LOG_QUERY)]
        public static void HandleGuildBankLogQuery(Packet packet)
        {
            packet.ReadByte("Tab Id");
            if (packet.Direction == Direction.ServerToClient)
            {
                var size = packet.ReadByte("Size");
                for (var i = 0; i < size; i++)
                {
                    var type = packet.ReadEnum<GuildBankEventLogType>("Bank Log Event Type", TypeCode.Byte, i);
                    packet.ReadGuid("[" + i + "] GUID", i);
                    if (type == GuildBankEventLogType.BuySlot)
                        packet.ReadUInt32("Cost", i);
                    else
                    {
                        if (type == GuildBankEventLogType.DepositMoney ||
                            type == GuildBankEventLogType.WithdrawMoney ||
                            type == GuildBankEventLogType.RepairMoney)
                            packet.ReadUInt32("Money", i);
                        else
                        {
                            packet.ReadUInt32("Item Entry", i);
                            packet.ReadUInt32("Stack Count", i);
                            if (type == GuildBankEventLogType.MoveItem ||
                                type == GuildBankEventLogType.MoveItem2)
                                packet.ReadByte("Tab Id", i);
                        }
                    }
                    packet.ReadUInt32("Time", i);
                }
            }
        }

        [Parser(Opcode.SMSG_GUILD_DECLINE)]
        public static void HandleGuildDecline(Packet packet)
        {
            packet.ReadCString("Reason");
            packet.ReadBoolean("Auto decline");
        }
        // Missing Opcodes
        //[Parser(Opcode.CMSG_MAELSTROM_RENAME_GUILD)]
        //[Parser(Opcode.UMSG_UPDATE_GUILD)]
        //[Parser(Opcode.UMSG_DELETE_GUILD_CHARTER)]

        [Parser(Opcode.SMSG_PETITION_SHOWLIST)]
        public static void HandlePetitionShowList(Packet packet)
        {
            packet.ReadGuid("GUID");
            var counter = packet.ReadByte("Counter");
            for (var i = 0; i < counter; i++)
            {
                packet.ReadUInt32("Index");
                packet.ReadUInt32("Charter Entry");
                packet.ReadUInt32("Charter Display");
                packet.ReadUInt32("Charter Cost");
                packet.ReadUInt32("Unk Uint32 1");
                packet.ReadUInt32("Required signs");
            }
        }

        [Parser(Opcode.CMSG_PETITION_BUY)]
        public static void HandlePetitionBuy(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Unk UInt32 1");
            packet.ReadUInt64("Unk UInt64 1");
            packet.ReadCString("Name");
            packet.ReadCString("Text");
            packet.ReadUInt32("Unk UInt32 2");
            packet.ReadUInt32("Unk UInt32 3");
            packet.ReadUInt32("Unk UInt32 4");
            packet.ReadUInt32("Unk UInt32 5");
            packet.ReadUInt32("Unk UInt32 6");
            packet.ReadUInt32("Unk UInt32 7");
            packet.ReadUInt32("Unk UInt32 8");
            packet.ReadUInt16("Unk UInt16 1");
            packet.ReadUInt32("Unk UInt32 9");
            packet.ReadUInt32("Unk UInt32 10");
            packet.ReadUInt32("Unk UInt32 11");

            for (var i = 0; i < 10; i++)
                packet.ReadCString("Unk String", i);

            packet.ReadUInt32("Client Index");
            packet.ReadUInt32("Unk UInt32 12");
        }

        [Parser(Opcode.CMSG_PETITION_SHOW_SIGNATURES)]
        public static void HandlePetitionShowSignatures(Packet packet)
        {
            packet.ReadGuid("Petition GUID");
        }

        [Parser(Opcode.SMSG_PETITION_SHOW_SIGNATURES)]
        public static void HandlePetitionShowSignaturesServer(Packet packet)
        {
            packet.ReadGuid("Petition GUID");
            packet.ReadGuid("Owner GUID");
            packet.ReadUInt32("Guild/Team GUID");
            var counter = packet.ReadByte("Sign count");
            for (var i = 0; i < counter; i++)
            {
                packet.ReadGuid("Player GUID", i);
                packet.ReadUInt32("Unk UInt32 1", i);
            }
        }

        [Parser(Opcode.CMSG_PETITION_SIGN)]
        public static void HandlePetitionSign(Packet packet)
        {
            packet.ReadGuid("Petition GUID");
            packet.ReadByte("Unk Byte 1");
        }

        [Parser(Opcode.SMSG_PETITION_SIGN_RESULTS)]
        public static void HandlePetitionSignResult(Packet packet)
        {
            packet.ReadGuid("Petition GUID");
            packet.ReadGuid("Player GUID");
            packet.ReadEnum<PetitionResultType>("Petition Result", TypeCode.UInt32);
        }

        [Parser(Opcode.MSG_PETITION_DECLINE)]
        public static void HandlePetitionDecline(Packet packet)
        {
            packet.ReadGuid(packet.Direction == Direction.ClientToServer ? "Petition GUID" : "Player GUID");
        }

        [Parser(Opcode.CMSG_OFFER_PETITION)]
        public static void HandlePetitionOffer(Packet packet)
        {
            packet.ReadUInt32("Unk UInt3 1");
            packet.ReadGuid("Petition GUID");
            packet.ReadGuid("Target GUID");
        }

        [Parser(Opcode.CMSG_TURN_IN_PETITION)]
        public static void HandlePetitionTurnIn(Packet packet)
        {
            packet.ReadGuid("Petition GUID");
        }

        [Parser(Opcode.SMSG_TURN_IN_PETITION_RESULTS)]
        public static void HandlePetitionTurnInResults(Packet packet)
        {
            packet.ReadEnum<PetitionResultType>("Result", TypeCode.UInt32);
        }

        [Parser(Opcode.CMSG_PETITION_QUERY)]
        public static void HandlePetitionQuery(Packet packet)
        {
            packet.ReadUInt32("Guild/Team GUID");
            packet.ReadGuid("Petition GUID");
        }

        [Parser(Opcode.SMSG_PETITION_QUERY_RESPONSE)]
        public static void HandlePetitionQueryResponse(Packet packet)
        {
            packet.ReadUInt32("Guild/Team GUID");
            packet.ReadGuid("Owner GUID");
            packet.ReadCString("Name");
            packet.ReadCString("Text");
            packet.ReadUInt32("Signs Needed");
            packet.ReadUInt32("Signs Needed");
            packet.ReadUInt32("Unk UInt32 4");
            packet.ReadUInt32("Unk UInt32 5");
            packet.ReadUInt32("Unk UInt32 6");
            packet.ReadUInt32("Unk UInt32 7");
            packet.ReadUInt32("Unk UInt32 8");
            packet.ReadUInt16("Unk UInt16 1");
            packet.ReadUInt32("Unk UInt32 (Level?)");
            packet.ReadUInt32("Unk UInt32 (Level?)");
            packet.ReadUInt32("Unk UInt32 11");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                for (var i = 0; i < 10; i++)
                    packet.ReadCString("Unk String", i);

            packet.ReadUInt32("Client Index");
            packet.ReadUInt32("Petition Type (0: Guild / 1: Arena)");
        }

        [Parser(Opcode.MSG_PETITION_RENAME)]
        public static void HandlePetitionRename(Packet packet)
        {
            packet.ReadGuid("Petition GUID");
            packet.ReadCString("New Name");
        }

        [Parser(Opcode.SMSG_OFFER_PETITION_ERROR)]
        public static void HandlePetitionError(Packet packet)
        {
            packet.ReadGuid("Petition GUID");
        }

        [Parser(Opcode.CMSG_GUILDFINDER_JOIN)]
        public static void HandleGuildFinderJoin(Packet packet)
        {
            packet.ReadBit("Join");
            packet.ReadEnum<GuildFinderOptionsAvailability>("Availability", TypeCode.UInt32);
            packet.ReadEnum<GuildFinderOptionsRoles>("Class Roles", TypeCode.UInt32);
            packet.ReadEnum<GuildFinderOptionsInterest>("Guild Interests", TypeCode.UInt32);
            packet.ReadEnum<GuildFinderOptionsLevel>("Level", TypeCode.UInt32);
            packet.ReadCString("Comment");
        }

        [Parser(Opcode.SMSG_GUILDFINDER_SEARCH_RESULT)]
        public static void HandleGuildFinderSearchResult(Packet packet)
        {
            var count = packet.ReadInt32("Count");
            if (count == 0)
                return;
            var guids = new byte[count][];

            for (int x = 0; x < guids.Length; x++)
               guids[x] = new byte[8];

            for (var i = 0; i < count; ++i)
            {
                guids[i][7] = (byte)(packet.ReadBit() ? 1 : 0);
                guids[i][4] = (byte)(packet.ReadBit() ? 1 : 0);
                guids[i][5] = (byte)(packet.ReadBit() ? 1 : 0);
                guids[i][0] = (byte)(packet.ReadBit() ? 1 : 0);
                guids[i][2] = (byte)(packet.ReadBit() ? 1 : 0);
                guids[i][6] = (byte)(packet.ReadBit() ? 1 : 0);
                guids[i][1] = (byte)(packet.ReadBit() ? 1 : 0);
                guids[i][3] = (byte)(packet.ReadBit() ? 1 : 0);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Guild Emblem Border Color", i);

                if (guids[i][4] != 0) // 12
                    guids[i][4] ^= packet.ReadByte();

                packet.ReadCString("Guild Description", i);

                if (guids[i][6] != 0) // 14
                    guids[i][6] ^= packet.ReadByte();

                packet.ReadInt32("Guild Emblem Texture File", i);
                packet.ReadInt32("Guild Level", i);

                if (guids[i][5] != 0) // 13
                    guids[i][5] ^= packet.ReadByte();

                packet.ReadInt32("Unk 2", i);
                packet.ReadEnum<GuildFinderOptionsRoles>("Class Roles", TypeCode.UInt32, i);
                packet.ReadCString("Guild Name", i);
                packet.ReadByte("Cached", i);

                if (guids[i][3] != 0) // 11
                    guids[i][3] ^= packet.ReadByte();

                packet.ReadInt32("Achievement Points", i);

                if (guids[i][0] != 0) // 8
                    guids[i][0] ^= packet.ReadByte();

                packet.ReadInt32("Guild Emblem Color", i);
                packet.ReadInt32("Guild Emblem Background Color", i);
                packet.ReadByte("Request Pending", i);
                packet.ReadEnum<GuildFinderOptionsInterest>("Guild Interests", TypeCode.UInt32, i);
                packet.ReadEnum<GuildFinderOptionsAvailability>("Availability", TypeCode.UInt32, i);

                if (guids[i][7] != 0) // 15
                    guids[i][7] ^= packet.ReadByte();

                packet.ReadInt32("Number of members", i);

                if (guids[i][2] != 0) // 10
                    guids[i][2] ^= packet.ReadByte();

                packet.ReadInt32("Unk 5", i);

                if (guids[i][1] != 0) // 9
                    guids[i][1] ^= packet.ReadByte();

                packet.WriteLine("[{0}] Guild Guid: {1}", i, new Guid(BitConverter.ToUInt64(guids[i], 0)));
            }
        }

    }
}
