using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class ArtifactHandler
    {
        public static void ReadArtifactPowerChoice(Packet packet, params object[] index)
        {
            packet.ReadInt16("ArtifactPowerID");
            packet.ReadByte("Rank");
        }

        [Parser(Opcode.CMSG_ARTIFACT_ADD_POWER)]
        public static void HandleArtifactAddPower(Packet packet)
        {
            packet.ReadPackedGuid128("ArtifactGUID");
            packet.ReadPackedGuid128("ForgeGUID");
            var count = packet.ReadUInt32("PowerChoiceCount");
            for (int i = 0; i < count; i++)
                ReadArtifactPowerChoice(packet, i);
        }

        [Parser(Opcode.CMSG_ARTIFACT_SET_APPEARANCE)]
        public static void HandleArtifactSetAppearance(Packet packet)
        {
            packet.ReadPackedGuid128("ArtifactGUID");
            packet.ReadPackedGuid128("ForgeGUID");
            packet.ReadInt32("ArtifactAppearanceID");
        }

        [Parser(Opcode.CMSG_CONFIRM_ARTIFACT_RESPEC)]
        public static void HandleConfirmArtifactRespec(Packet packet)
        {
            packet.ReadPackedGuid128("ArtifactGUID");
            packet.ReadPackedGuid128("NpcGUID");
        }

        [Parser(Opcode.SMSG_ARTIFACT_FORGE_OPENED)]
        public static void HandleArtifactForgeOpened(Packet packet)
        {
            packet.ReadPackedGuid128("ArtifactGUID");
            packet.ReadPackedGuid128("ForgeGUID");
        }

        [Parser(Opcode.SMSG_ARTIFACT_RESPEC_CONFIRM)]
        public static void HandleArtifactRespecConfirm(Packet packet)
        {
            packet.ReadPackedGuid128("ArtifactGUID");
            packet.ReadPackedGuid128("NpcGUID");
        }

        [Parser(Opcode.SMSG_ARTIFACT_XP_GAIN)]
        public static void HandleArtifactXPGain(Packet packet)
        {
            packet.ReadPackedGuid128("ArtifactGUID");
            packet.ReadUInt64("Amount");
        }

        [Parser(Opcode.SMSG_ARTIFACT_KNOWLEDGE)]
        public static void HandleArtifactKnowledge(Packet packet)
        {
            packet.ReadInt32("ArtifactCategory");
            packet.ReadByte("KnowledgeLevel");
        }
    }
}
