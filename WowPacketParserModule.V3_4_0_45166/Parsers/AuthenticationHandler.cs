using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class AuthenticationHandler
    {
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
                var classes = packet.ReadUInt32("AvailableClasses");
                var templates = packet.ReadUInt32("Templates");
                packet.ReadUInt32("AccountCurrency");
                packet.ReadTime64("Time");

                for (var i = 0; i < classes; ++i)
                {
                    packet.ReadByteE<Race>("RaceID", "AvailableClasses", i);
                    var classesForRace = packet.ReadUInt32();
                    for (var j = 0u; j < classesForRace; ++j)
                    {
                        packet.ReadByteE<Class>("ClassID", "AvailableClasses", i, "Classes", j);
                        packet.ReadByteE<ClientType>("ActiveExpansionLevel", "AvailableClasses", i, "Classes", j);
                        packet.ReadByteE<ClientType>("AccountExpansionLevel", "AvailableClasses", i, "Classes", j);
                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51505)) // assumption
                            packet.ReadByte("MinActiveExpansionLevel", "AvailableClasses", i, "Classes", j);
                    }
                }

                packet.ResetBitReader();
                packet.ReadBit("IsExpansionTrial");
                packet.ReadBit("ForceCharacterTemplate");
                var horde = packet.ReadBit(); // NumPlayersHorde
                var alliance = packet.ReadBit(); // NumPlayersAlliance
                var trialExpiration = packet.ReadBit(); // ExpansionTrialExpiration
                var hasNewBuildKeys = false;
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51505)) // assumption
                    hasNewBuildKeys = packet.ReadBit();

                packet.ResetBitReader();
                packet.ReadUInt32("BillingPlan");
                packet.ReadUInt32("TimeRemain");
                packet.ReadUInt32("Unk_V7_3_5");

                packet.ReadBit("InGameRoom");
                packet.ReadBit("InGameRoom");
                packet.ReadBit("InGameRoom");

                if (horde)
                    packet.ReadUInt16("NumPlayersHorde");

                if (alliance)
                    packet.ReadUInt16("NumPlayersAlliance");

                if (trialExpiration)
                    packet.ReadInt64("ExpansionTrialExpiration");

                if (hasNewBuildKeys)
                {
                    var newBuildKey = new byte[16];
                    var someKey = new byte[16];
                    for (var i = 0; i < 16; i++)
                    {
                        newBuildKey[i] = packet.ReadByte();
                        someKey[i] = packet.ReadByte();
                    }
                    packet.AddValue("NewBuildKey", Encoding.UTF8.GetString(newBuildKey));
                    packet.AddValue("SomeKey", Encoding.UTF8.GetString(someKey));
                }

                for (var i = 0; i < realms; ++i)
                {
                    packet.ReadUInt32("RealmAddress", "VirtualRealms", i);
                    packet.ResetBitReader();
                    packet.ReadBit("IsLocal", "VirtualRealms", i);
                    packet.ReadBit("IsInternalRealm", "VirtualRealms", i);

                    var bitsCount = 8;

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724) && ClientVersion.RemovedInVersion(ClientVersionBuild.V8_1_5_29683))
                        bitsCount = 9;

                    var nameLen1 = packet.ReadBits(bitsCount);
                    var nameLen2 = packet.ReadBits(bitsCount);
                    packet.ReadWoWString("RealmNameActual", nameLen1, "VirtualRealms", i);
                    packet.ReadWoWString("RealmNameNormalized", nameLen2, "VirtualRealms", i);
                }

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
