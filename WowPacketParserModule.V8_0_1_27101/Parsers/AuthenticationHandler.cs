using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class AuthenticationHandler
    {
        [Parser(Opcode.CMSG_AUTH_SESSION)]
        public static void HandleAuthSession(Packet packet)
        {
            packet.ReadUInt64("DosResponse");
            packet.ReadUInt32("RegionID");
            packet.ReadUInt32("BattlegroupID");
            packet.ReadUInt32("RealmID");
            packet.ReadBytes("LocalChallenge", 16);
            packet.ReadBytes("Digest", 24);
            packet.ReadBit("UseIPv6");

            var realmJoinTicketSize = packet.ReadInt32();
            packet.ReadBytes("RealmJoinTicket", realmJoinTicketSize);
        }

        public static void ReadClassAvailability(Packet packet, params object[] indexes)
        {
            packet.ReadByteE<Class>("ClassID", indexes);
            packet.ReadByteE<ClientType>("ActiveExpansionLevel", indexes);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_3_0_33062))
                packet.ReadByteE<ClientType>("AccountExpansionLevel", indexes);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479))
                packet.ReadByte("MinActiveExpansionLevel", indexes);
        }

        public static void ReadRaceClassAvailability(Packet packet, params object[] indexes)
        {
            packet.ReadByteE<Race>("RaceID", indexes);
            var classesForRace = packet.ReadUInt32();
            for (var j = 0u; j < classesForRace; ++j)
                ReadClassAvailability(packet, indexes, "Classes", j);
        }

        public static void ReadAvailableCharacterTemplateSet(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("TemplateSetId", indexes);
            var templateClasses = packet.ReadUInt32();
            for (var j = 0; j < templateClasses; ++j)
            {
                packet.ReadByteE<Class>("Class", indexes, j);
                packet.ReadByte("FactionGroup", indexes, j);
            }

            packet.ResetBitReader();
            var nameLen = packet.ReadBits(7);
            var descLen = packet.ReadBits(10);
            packet.ReadWoWString("Name", nameLen, indexes);
            packet.ReadWoWString("Description", descLen, indexes);
        }

        public static void ReadGameTime(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();
            packet.ReadUInt32("BillingType", indexes);
            packet.ReadUInt32("MinutesRemaining", indexes);
            packet.ReadUInt32("RealBillingType", indexes);

            packet.ReadBit("IsInIGR", indexes);
            packet.ReadBit("IsPaidForByIGR", indexes);
            packet.ReadBit("IsCAISEnabled", indexes);
        }

        public static void ReadBaseBuildKey(Packet packet, params object[] indexes)
        {
            var buildKey = new byte[16];
            var configKey = new byte[16];
            for (var i = 0; i < 16; i++)
            {
                buildKey[i] = packet.ReadByte();
                configKey[i] = packet.ReadByte();
            }
            packet.AddValue("BuildKey", indexes, Convert.ToHexString(buildKey));
            packet.AddValue("ConfigKey", indexes, Convert.ToHexString(configKey));
        }

        public static void ReadAuthSuccessInfo(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("VirtualRealmAddress", indexes);
            var realms = packet.ReadUInt32();
            packet.ReadUInt32("TimeRested", indexes);
            packet.ReadByte("ActiveExpansionLevel", indexes);
            packet.ReadByte("AccountExpansionLevel", indexes);
            packet.ReadUInt32("TimeSecondsUntilPCKick", indexes);
            var classes = packet.ReadUInt32("AvailableClasses", indexes);
            var templates = packet.ReadUInt32("Templates", indexes);
            packet.ReadUInt32("AccountCurrency", indexes);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_5_37503) &&
                ClientVersion.Expansion != ClientType.Classic)
                packet.ReadTime64("Time", indexes);
            else
                packet.ReadTime("Time", indexes);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_3_0_33062))
            {
                for (var i = 0; i < classes; ++i)
                    ReadRaceClassAvailability(packet, "AvailableClasses", i);
            }
            else
            {
                for (var i = 0; i < classes; ++i)
                    ReadClassAvailability(packet, indexes, "AvailableClasses", i);
            }

            packet.ResetBitReader();
            packet.ReadBit("IsExpansionTrial", indexes);
            packet.ReadBit("ForceCharacterTemplate", indexes);
            var horde = packet.ReadBit(); // NumPlayersHorde
            var alliance = packet.ReadBit(); // NumPlayersAlliance
            var trialExpiration = packet.ReadBit(); // ExpansionTrialExpiration
            var hasNewBuildKeys = false;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_5_50232))
                hasNewBuildKeys = packet.ReadBit();

            ReadGameTime(packet, "GameTime");

            if (horde)
                packet.ReadUInt16("NumPlayersHorde", indexes);

            if (alliance)
                packet.ReadUInt16("NumPlayersAlliance", indexes);

            if (trialExpiration)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479))
                    packet.ReadInt64("ExpansionTrialExpiration", indexes);
                else
                    packet.ReadInt32("ExpansionTrialExpiration", indexes);
            }

            if (hasNewBuildKeys)
                ReadBaseBuildKey(packet, "CurrentBuild");

            for (var i = 0; i < realms; ++i)
                SessionHandler.ReadVirtualRealmInfo(packet, "VirtualRealms", indexes, i);

            for (var i = 0; i < templates; ++i)
                ReadAvailableCharacterTemplateSet(packet, "Templates", i);
        }

        public static void ReadAuthWaitInfo(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("WaitCount", indexes);
            packet.ReadUInt32("WaitTime", indexes);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_5_52902))
                packet.ReadUInt32("AllowedFactionGroupForCharacterCreate", indexes);
            packet.ResetBitReader();
            packet.ReadBit("HasFCM", indexes);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_5_52902))
                packet.ReadBit("CanCreateOnlyIfExisting", indexes);
        }

        [Parser(Opcode.SMSG_AUTH_RESPONSE)]
        public static void HandleAuthResponse(Packet packet)
        {
            packet.ReadUInt32E<BattlenetRpcErrorCode>("Result");

            var ok = packet.ReadBit("Success");
            var queued = packet.ReadBit("Queued");
            if (ok)
                ReadAuthSuccessInfo(packet, "SuccessInfo");

            if (queued)
                ReadAuthWaitInfo(packet, "WaitInfo");
        }
    }
}
