using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class LfgHandler
    {
        [Parser(Opcode.SMSG_LFG_PROPOSAL_UPDATE)]
        public static void HandleLfgProposalUpdate(Packet packet)
        {
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet);

            packet.ReadUInt64("InstanceID");
            packet.ReadUInt32("ProposalID");
            packet.ReadUInt32("Slot");
            packet.ReadSByte("State");
            packet.ReadUInt32("CompletedMask");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_0_24920))
                packet.ReadUInt32("EncounterMask");

            var playerCount = packet.ReadUInt32("PlayersCount");
            packet.ReadByte();
            packet.ReadBit("ValidCompletedMask");
            packet.ReadBit("ProposalSilent");
            packet.ReadBit("IsRequeue");

            for (var i = 0u; i < playerCount; i++)
            {
                packet.ReadUInt32("Roles", i);

                packet.ResetBitReader();

                packet.ReadBit("Me", i);
                packet.ReadBit("SameParty", i);
                packet.ReadBit("MyParty", i);
                packet.ReadBit("Responded", i);
                packet.ReadBit("Accepted", i);
            }
        }

        [Parser(Opcode.CMSG_QUICK_JOIN_AUTO_ACCEPT_REQUESTS)]
        public static void HandleLfgQuickJoinAutoAcceptRequests(Packet packet)
        {
            packet.ReadBit("AutoAccept");
        }
    }
}
