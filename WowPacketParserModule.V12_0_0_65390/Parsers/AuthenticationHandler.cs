using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class AuthenticationHandler
    {
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
            V8_0_1_27101.Parsers.AuthenticationHandler.ReadGameTime(packet, "GameTime");
            packet.ReadTime64("Time", indexes);

            for (var i = 0; i < realms; ++i)
                V8_0_1_27101.Parsers.SessionHandler.ReadVirtualRealmInfo(packet, "VirtualRealms", indexes, i);

            for (var i = 0; i < classes; ++i)
                V8_0_1_27101.Parsers.AuthenticationHandler.ReadRaceClassAvailability(packet, "AvailableClasses", i);

            for (var i = 0; i < templates; ++i)
                V8_0_1_27101.Parsers.AuthenticationHandler.ReadAvailableCharacterTemplateSet(packet, "Templates", i);

            packet.ResetBitReader();
            packet.ReadBit("IsExpansionTrial", indexes);
            packet.ReadBit("ForceCharacterTemplate", indexes);
            var horde = packet.ReadBit(); // NumPlayersHorde
            var alliance = packet.ReadBit(); // NumPlayersAlliance
            var trialExpiration = packet.ReadBit(); // ExpansionTrialExpiration
            var hasNewBuildKeys = packet.ReadBit();

            if (horde)
                packet.ReadUInt16("NumPlayersHorde", indexes);

            if (alliance)
                packet.ReadUInt16("NumPlayersAlliance", indexes);

            if (trialExpiration)
                packet.ReadInt64("ExpansionTrialExpiration", indexes);

            if (hasNewBuildKeys)
                V8_0_1_27101.Parsers.AuthenticationHandler.ReadBaseBuildKey(packet, "CurrentBuild");
        }

        [Parser(Opcode.SMSG_AUTH_RESPONSE, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleAuthResponse(Packet packet)
        {
            packet.ReadUInt32E<BattlenetRpcErrorCode>("Result");

            var ok = packet.ReadBit("Success");
            var queued = packet.ReadBit("Queued");
            if (ok)
                ReadAuthSuccessInfo(packet, "SuccessInfo");

            if (queued)
                V8_0_1_27101.Parsers.AuthenticationHandler.ReadAuthWaitInfo(packet, "WaitInfo");
        }
    }
}
