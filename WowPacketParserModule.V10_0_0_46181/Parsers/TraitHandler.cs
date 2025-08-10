using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class TraitHandler
    {
        public static void ReadTraitEntry(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("TraitNodeID", indexes);
            packet.ReadInt32("TraitNodeEntryID", indexes);
            packet.ReadInt32("Rank", indexes);
            packet.ReadInt32("GrantedRanks", indexes);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_2_0_62213))
                packet.ReadInt32("BonusRanks", indexes);
        }

        public static void ReadTraitSubTreeCache(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();

            packet.ReadInt32("TraitSubTreeID", indexes);
            var entries = packet.ReadUInt32();

            for (var i = 0u; i < entries; ++i)
                ReadTraitEntry(packet, indexes, "TraitEntry", i);

            packet.ReadBit("Active", indexes);
        }

        public static void ReadTraitConfig(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("ID", indexes);
            var type = packet.ReadInt32("Type", indexes);
            var entries = packet.ReadUInt32();

            uint subtrees = 0;
            if (ClientVersion.AddedInVersion(ClientBranch.Retail, ClientVersionBuild.V11_0_0_55666)
                || ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                subtrees = packet.ReadUInt32();

            switch (type)
            {
                case 1:
                    packet.ReadInt32("ChrSpecializationID", indexes);
                    packet.ReadInt32("CombatConfigFlags", indexes);
                    packet.ReadInt32("LocalIdentifier", indexes);
                    break;
                case 2:
                    packet.ReadInt32("SkillLineID", indexes);
                    break;
                case 3:
                    packet.ReadInt32("TraitSystemID", indexes);
                    break;
            }

            for (var i = 0u; i < entries; ++i)
                ReadTraitEntry(packet, indexes, "TraitEntry", i);

            packet.ResetBitReader();
            var nameLength = packet.ReadBits(9);

            for (var i = 0u; i < subtrees; ++i)
                ReadTraitSubTreeCache(packet, indexes, "TraitSubTreeCache", i);

            packet.ReadWoWString("Name", nameLength, indexes);
        }

        [Parser(Opcode.CMSG_TRAITS_COMMIT_CONFIG)]
        public static void ReadTraitsCommitConfig(Packet packet)
        {
            ReadTraitConfig(packet, "Config");
            packet.ReadInt32("SavedConfigID");
            packet.ReadInt32("SavedLocalIdentifier");
        }

        [Parser(Opcode.SMSG_TRAIT_CONFIG_COMMIT_FAILED)]
        public static void ReadTraitsCommitConfigFailed(Packet packet)
        {
            packet.ReadInt32("ConfigID");
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadBits("Reason", 4);
        }

        [Parser(Opcode.CMSG_CLASS_TALENTS_REQUEST_NEW_CONFIG)]
        public static void ReadClassTalentsRequestNewConfig(Packet packet)
        {
            ReadTraitConfig(packet, "Config");
        }

        [Parser(Opcode.CMSG_CLASS_TALENTS_RENAME_CONFIG)]
        public static void ReadClassTalentsRenameConfig(Packet packet)
        {
            packet.ReadInt32("ConfigID");

            var nameLength = packet.ReadBits(9);
            packet.ResetBitReader();

            packet.ReadWoWString("Name", nameLength);
        }

        [Parser(Opcode.CMSG_CLASS_TALENTS_DELETE_CONFIG)]
        public static void ReadClassTalentsDeleteConfig(Packet packet)
        {
            packet.ReadInt32("ConfigID");
        }

        [Parser(Opcode.CMSG_CLASS_TALENTS_SET_STARTER_BUILD_ACTIVE)]
        public static void ReadClassTalentsSetStarterBuildActive(Packet packet)
        {
            packet.ReadInt32("ConfigID");
            packet.ReadBit("Active");
        }

        [Parser(Opcode.CMSG_CLASS_TALENTS_SET_USES_SHARED_ACTION_BARS)]
        public static void ReadClassTalentsSetUsesSharedActionBars(Packet packet)
        {
            packet.ReadInt32("ConfigID");
            packet.ReadBit("UsesShared");
            packet.ReadBit("IsLastSelectedSavedConfig");
        }
    }
}
