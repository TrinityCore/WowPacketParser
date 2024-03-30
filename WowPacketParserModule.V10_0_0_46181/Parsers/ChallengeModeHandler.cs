using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class ChallengeModeHandler
    {
        [Parser(Opcode.SMSG_CHALLENGE_MODE_START)]
        public static void HandleChallengeModeStart(Packet packet)
        {
            packet.ReadUInt32<MapId>("MapID");
            packet.ReadUInt32("ChallengeID");
            packet.ReadInt32("ChallengeLevel");

            for (var i = 0; i < 4; i++)
            {
                packet.ReadUInt32("AffixID", i);
            }

            packet.ReadInt32("DeathCount");
            var playerCount = packet.ReadUInt32();

            packet.ResetBitReader();
            packet.ReadBit("WasActiveKeystoneCharged");

            for (var i = 0; i < playerCount; i++)
            {
                InstanceHandler.ReadEncounterStartPlayerInfo(packet, "PlayerInfo", i);
            }
        }
    }
}
