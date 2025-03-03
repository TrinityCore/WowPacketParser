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
            packet.ReadUInt64("DosResponse");
            packet.ReadInt16E<ClientVersionBuild>("Build");
            packet.ReadByte("BuildType");
            packet.ReadUInt32("RegionID");
            packet.ReadUInt32("BattlegroupID");
            packet.ReadUInt32("RealmID");
            packet.ReadBytes("LocalChallenge", 16);
            packet.ReadBytes("Digest", 24);
            packet.ReadBit("UseIPv6");

            var realmJoinTicketSize = packet.ReadInt32();
            packet.ReadBytes("RealmJoinTicket", realmJoinTicketSize);
        }

        [Parser(Opcode.SMSG_AUTH_RESPONSE)]
        public static void HandleAuthResponse(Packet packet)
        {
            packet.ReadUInt32E<BattlenetRpcErrorCode>("Result");

            var ok = packet.ReadBit("Success");
            var queued = packet.ReadBit("Queued");
            if (ok)
            {
                packet.ReadUInt32("VirtualRealmAddress");
                var realms = packet.ReadUInt32();
                packet.ReadUInt32("TimeRested");
                packet.ReadByte("ActiveExpansionLevel");
                packet.ReadByte("AccountExpansionLevel");
                packet.ReadUInt32("TimeSecondsUntilPCKick");
                var races = 0u;
                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V7_3_5_25848))
                    races = packet.ReadUInt32("AvailableRaces");

                var classes = packet.ReadUInt32("AvailableClasses");
                var templates = packet.ReadUInt32("Templates");
                packet.ReadUInt32("AccountCurrency");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_1_0_22900))
                    packet.ReadTime("Time");

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V7_3_5_25848))
                {
                    for (var i = 0; i < races; ++i)
                    {
                        packet.ReadByteE<Race>("Race", "AvailableRaces", i);
                        packet.ReadByteE<ClientType>("RequiredExpansion", "AvailableRaces", i);
                    }
                }

                for (var i = 0; i < classes; ++i)
                {
                    packet.ReadByteE<Class>("Class", "AvailableClasses", i);
                    packet.ReadByteE<ClientType>("RequiredExpansion", "AvailableClasses", i);
                }

                packet.ResetBitReader();
                packet.ReadBit("IsExpansionTrial");
                packet.ReadBit("ForceCharacterTemplate");
                var horde = packet.ReadBit(); // NumPlayersHorde
                var alliance = packet.ReadBit(); // NumPlayersAlliance

                packet.ResetBitReader();
                packet.ReadUInt32("BillingPlan");
                packet.ReadUInt32("TimeRemain");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_5_26822))
                    packet.ReadUInt32("Unk_V7_3_5_26822");

                packet.ReadBit("InGameRoom");
                packet.ReadBit("InGameRoom");
                packet.ReadBit("InGameRoom");

                if (horde)
                    packet.ReadUInt16("NumPlayersHorde");

                if (alliance)
                    packet.ReadUInt16("NumPlayersAlliance");

                for (var i = 0; i < realms; ++i)
                    V6_0_2_19033.Parsers.SessionHandler.ReadVirtualRealmInfo(packet, "VirtualRealms", i);

                for (var i = 0; i < templates; ++i)
                {
                    packet.ReadUInt32("TemplateSetId", i);
                    var templateClasses = packet.ReadUInt32();
                    for (var j = 0; j < templateClasses; ++j)
                    {
                        packet.ReadByteE<Class>("Class", i, j);
                        packet.ReadByte("FactionGroup", i, j);
                    }

                    packet.ResetBitReader();
                    var nameLen = packet.ReadBits(7);
                    var descLen = packet.ReadBits(10);
                    packet.ReadWoWString("Name", nameLen, i);
                    packet.ReadWoWString("Description", descLen, i);
                }
            }

            if (queued)
            {
                packet.ReadUInt32("WaitCount");
                packet.ReadUInt32("WaitTime");
                packet.ResetBitReader();
                packet.ReadBit("HasFCM");
            }
        }
    }
}
