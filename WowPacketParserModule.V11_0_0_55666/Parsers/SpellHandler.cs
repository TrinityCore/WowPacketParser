using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.SMSG_SPELL_VISUAL_LOAD_SCREEN, ClientVersionBuild.V11_2_0_62213)]
        public static void HandleSpellVisualLoadScreen(Packet packet)
        {
            packet.ReadInt32("SpellVisualKitID");
            packet.ReadInt32("Duration");
            packet.ReadInt32("Delay");
        }

        [Parser(Opcode.SMSG_MIRROR_IMAGE_COMPONENTED_DATA, ClientVersionBuild.V11_1_5_60392)]
        public static void HandleMirrorImageData(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadInt32("ChrModelID");

            packet.ReadByte("RaceID");
            packet.ReadByte("Gender");
            packet.ReadByte("ClassID");
            var customizationCount = packet.ReadUInt32();
            packet.ReadPackedGuid128("GuildGUID");
            var itemDisplayCount = packet.ReadInt32("ItemDisplayCount");
            packet.ReadInt32("SpellVisualKitID");
            packet.ReadSingle("DisplayScale");

            for (var j = 0u; j < customizationCount; ++j)
                V9_0_1_36216.Parsers.CharacterHandler.ReadChrCustomizationChoice(packet, "Customizations", j);

            for (var i = 0u; i < itemDisplayCount; i++)
                packet.ReadInt32("ItemDisplayID", i);
        }
    }
}
