using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class AuthenticationHandler
    {
        [Parser(Opcode.CMSG_AUTH_SESSION)]
        public static void HandleAuthSession(Packet packet)
        {
            packet.Translator.ReadUInt64("DosResponse");
            packet.Translator.ReadInt16E<ClientVersionBuild>("Build");
            packet.Translator.ReadByte("BuildType");
            packet.Translator.ReadUInt32("RegionID");
            packet.Translator.ReadUInt32("BattlegroupID");
            packet.Translator.ReadUInt32("RealmID");
            packet.Translator.ReadBytes("LocalChallenge", 16);
            packet.Translator.ReadBytes("Digest", 24);
            packet.Translator.ReadBit("UseIPv6");

            var realmJoinTicketSize = packet.Translator.ReadInt32();
            packet.Translator.ReadBytes("RealmJoinTicket", realmJoinTicketSize);
        }

        [Parser(Opcode.SMSG_AUTH_RESPONSE)]
        public static void HandleAuthResponse(Packet packet)
        {
            packet.Translator.ReadUInt32E<BattlenetRpcErrorCode>("Result");

            var ok = packet.Translator.ReadBit("Success");
            var queued = packet.Translator.ReadBit("Queued");
            if (ok)
            {
                packet.Translator.ReadUInt32("VirtualRealmAddress");
                var realms = packet.Translator.ReadUInt32();
                packet.Translator.ReadUInt32("TimeRested");
                packet.Translator.ReadByte("ActiveExpansionLevel");
                packet.Translator.ReadByte("AccountExpansionLevel");
                packet.Translator.ReadUInt32("TimeSecondsUntilPCKick");
                var races = packet.Translator.ReadUInt32("AvailableRaces");
                var classes = packet.Translator.ReadUInt32("AvailableClasses");
                var templates = packet.Translator.ReadUInt32("Templates");
                packet.Translator.ReadUInt32("AccountCurrency");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_1_0_22900))
                    packet.Translator.ReadTime("Time");

                for (var i = 0; i < races; ++i)
                {
                    packet.Translator.ReadByteE<Race>("Race", "AvailableRaces", i);
                    packet.Translator.ReadByteE<ClientType>("RequiredExpansion", "AvailableRaces", i);
                }

                for (var i = 0; i < classes; ++i)
                {
                    packet.Translator.ReadByteE<Class>("Class", "AvailableClasses", i);
                    packet.Translator.ReadByteE<ClientType>("RequiredExpansion", "AvailableClasses", i);
                }

                packet.Translator.ResetBitReader();
                packet.Translator.ReadBit("IsExpansionTrial");
                packet.Translator.ReadBit("ForceCharacterTemplate");
                var horde = packet.Translator.ReadBit(); // NumPlayersHorde
                var alliance = packet.Translator.ReadBit(); // NumPlayersAlliance

                packet.Translator.ResetBitReader();
                packet.Translator.ReadUInt32("BillingPlan");
                packet.Translator.ReadUInt32("TimeRemain");
                packet.Translator.ReadBit("InGameRoom");
                packet.Translator.ReadBit("InGameRoom");
                packet.Translator.ReadBit("InGameRoom");

                if (horde)
                    packet.Translator.ReadUInt16("NumPlayersHorde");

                if (alliance)
                    packet.Translator.ReadUInt16("NumPlayersAlliance");

                for (var i = 0; i < realms; ++i)
                {
                    packet.Translator.ReadUInt32("RealmAddress", "VirtualRealms", i);
                    packet.Translator.ResetBitReader();
                    packet.Translator.ReadBit("IsLocal", "VirtualRealms", i);
                    packet.Translator.ReadBit("IsInternalRealm", "VirtualRealms", i);
                    var nameLen1 = packet.Translator.ReadBits(8);
                    var nameLen2 = packet.Translator.ReadBits(8);
                    packet.Translator.ReadWoWString("RealmNameActual", nameLen1, "VirtualRealms", i);
                    packet.Translator.ReadWoWString("RealmNameNormalized", nameLen2, "VirtualRealms", i);
                }

                for (var i = 0; i < templates; ++i)
                {
                    packet.Translator.ReadUInt32("TemplateSetId", i);
                    var templateClasses = packet.Translator.ReadUInt32();
                    for (var j = 0; j < templateClasses; ++j)
                    {
                        packet.Translator.ReadByteE<Class>("Class", i, j);
                        packet.Translator.ReadByte("FactionGroup", i, j);
                    }

                    packet.Translator.ResetBitReader();
                    var nameLen = packet.Translator.ReadBits(7);
                    var descLen = packet.Translator.ReadBits(10);
                    packet.Translator.ReadWoWString("Name", nameLen, i);
                    packet.Translator.ReadWoWString("Description", descLen, i);
                }
            }

            if (queued)
            {
                packet.Translator.ReadUInt32("WaitCount");
                packet.Translator.ReadUInt32("WaitTime");
                packet.Translator.ResetBitReader();
                packet.Translator.ReadBit("HasFCM");
            }
        }
    }
}
