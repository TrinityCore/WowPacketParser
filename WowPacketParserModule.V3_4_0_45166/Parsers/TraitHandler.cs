using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class TraitHandler
    {
        public static void ReadTraitEntry(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("TraitNodeID", indexes);
            packet.ReadInt32("TraitNodeEntryID", indexes);
            packet.ReadInt32("Rank", indexes);
            packet.ReadInt32("GrantedRanks", indexes);
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
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_4_59817))
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

        [Parser(Opcode.SMSG_TRAIT_CONFIG_COMMIT_FAILED, ClientVersionBuild.V3_4_4_59817)]
        public static void ReadTraitsCommitConfigFailed(Packet packet)
        {
            packet.ReadInt32("ConfigID");
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadBits("Reason", 4);
        }
    }
}
