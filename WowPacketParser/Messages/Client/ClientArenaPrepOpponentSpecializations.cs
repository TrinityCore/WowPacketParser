using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientArenaPrepOpponentSpecializations
    {
        public List<ClientOpponentSpecData> OpponentData;

        [Parser(Opcode.SMSG_ARENA_PREP_OPPONENT_SPECIALIZATIONS)]
        public static void HandleArenaPrepOpponentSpecializations(Packet packet)
        {
            var count = packet.ReadInt32("OpponentDataCount");
            for (var i = 0; i < count; ++i)
                ClientOpponentSpecData.Read6(packet, "OpponentData", i);
        }
    }
}
