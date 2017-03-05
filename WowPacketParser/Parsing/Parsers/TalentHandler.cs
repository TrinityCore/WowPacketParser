using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class TalentHandler
    {
        public static void ReadTalentInfo(Packet packet)
        {
            packet.Translator.ReadUInt32("Free Talent count");
            var speccount = packet.Translator.ReadByte("Spec count");
            packet.Translator.ReadByte("Active Spec");
            for (var i = 0; i < speccount; ++i)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                    packet.Translator.ReadUInt32("TalentBranchSpec", i);
                var count2 = packet.Translator.ReadByte("Spec Talent Count", i);
                for (var j = 0; j < count2; ++j)
                {
                    packet.Translator.ReadUInt32("Talent Id", i, j);
                    packet.Translator.ReadByte("Rank", i, j);
                }

                var glyphs = packet.Translator.ReadByte("Glyph count", i);
                for (var j = 0; j < glyphs; ++j)
                    packet.Translator.ReadUInt16("Glyph", i, j);
            }
        }

        public static void ReadInspectPart(Packet packet)
        {
            var slotMask = packet.Translator.ReadUInt32("Slot Mask");
            var slot = 0;
            while (slotMask > 0)
            {
                if ((slotMask & 0x1) > 0)
                {
                    packet.Translator.ReadUInt32<ItemId>("Item Entry", (EquipmentSlotType)slot);
                    var enchantMask = packet.Translator.ReadUInt16();
                    if (enchantMask > 0)
                    {
                        var enchantName = string.Empty;
                        while (enchantMask > 0)
                        {
                            if ((enchantMask & 0x1) > 0)
                            {
                                enchantName += packet.Translator.ReadUInt16();
                                if (enchantMask > 1)
                                    enchantName += ", ";
                            }
                            enchantMask >>= 1;
                        }
                        packet.AddValue("Item Enchantments", enchantName, (EquipmentSlotType)slot);
                    }
                    packet.Translator.ReadUInt16("Unk UInt16", (EquipmentSlotType)slot);
                    packet.Translator.ReadPackedGuid("Creator GUID", (EquipmentSlotType)slot);
                    packet.Translator.ReadUInt32("Unk UInt32", (EquipmentSlotType)slot);
                }
                ++slot;
                slotMask >>= 1;
            }
        }

        [Parser(Opcode.SMSG_TALENTS_INVOLUNTARILY_RESET)]
        public static void HandleTalentsInvoluntarilyReset(Packet packet)
        {
            packet.Translator.ReadByte("Unk Byte");
        }

        [Parser(Opcode.SMSG_INSPECT_TALENT)]
        public static void HandleInspectTalent(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                packet.Translator.ReadGuid("GUID");
            else
                packet.Translator.ReadPackedGuid("GUID");

            ReadTalentInfo(packet);
            ReadInspectPart(packet);

            if (packet.CanRead()) // otherwise it would fail for players without a guild
            {
                packet.Translator.ReadGuid("Guild GUID");
                packet.Translator.ReadUInt32("Guild Level");
                packet.Translator.ReadUInt64("Guild Xp");
                packet.Translator.ReadUInt32("Guild Members");
            }
        }

        [Parser(Opcode.SMSG_INSPECT_RESULTS_UPDATE)]
        public static void HandleInspectResultsUpdate(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595)) // confirmed for 4.3.4
                packet.Translator.ReadPackedGuid("GUID");
            else if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                packet.Translator.ReadGuid("GUID");
            else
                packet.Translator.ReadPackedGuid("GUID");

            ReadInspectPart(packet);
        }

        [Parser(Opcode.MSG_TALENT_WIPE_CONFIRM)]
        public static void HandleTalent(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            if (packet.Direction == Direction.ServerToClient)
                packet.Translator.ReadUInt32("Gold");
        }

        [Parser(Opcode.MSG_RESPEC_WIPE_CONFIRM)]
        public static void HandleRespecWipeConfirm(Packet packet)
        {
            packet.Translator.ReadByte("Spec Group");
            var guid = packet.Translator.StartBitStream(5, 3, 2, 7, 0, 6, 1, 4);
            packet.Translator.ParseBitStream(guid, 0, 1, 2, 3, 5, 6, 7, 4);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_UPDATE_TALENT_DATA, ClientVersionBuild.Zero, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleTalentsInfo(Packet packet)
        {
            var pet = packet.Translator.ReadBool("Pet Talents");
            if (pet)
            {
                packet.Translator.ReadUInt32("Unspent Talent");
                var count = packet.Translator.ReadByte("Talent Count");
                for (var i = 0; i < count; ++i)
                {
                    packet.Translator.ReadUInt32("Talent ID", i);
                    packet.Translator.ReadByte("Rank", i);
                }
            }
            else
                ReadTalentInfo(packet);
        }

        [Parser(Opcode.SMSG_UPDATE_TALENT_DATA, ClientVersionBuild.V5_1_0_16309)]
        public static void ReadTalentInfo510(Packet packet)
        {
            var specCount = packet.Translator.ReadByte("Spec Group count");
            packet.Translator.ReadByte("Active Spec Group");

            for (var i = 0; i < specCount; ++i)
            {
                packet.Translator.ReadUInt32("Spec Id", i);

                var spentTalents = packet.Translator.ReadByte("Spec Talent Count", i);
                for (var j = 0; j < spentTalents; ++j)
                    packet.Translator.ReadUInt16("Talent Id", i, j);

                var glyphCount = packet.Translator.ReadByte("Glyph count", i);
                for (var j = 0; j < glyphCount; ++j)
                    packet.Translator.ReadUInt16("Glyph", i, j);
            }
        }

        [Parser(Opcode.CMSG_LEARN_PREVIEW_TALENTS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_LEARN_PREVIEW_TALENTS_PET, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTalentPreviewTalents(Packet packet)
        {
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.CMSG_LEARN_PREVIEW_TALENTS_PET, Direction.ClientToServer))
                packet.Translator.ReadGuid("GUID");

            var count = packet.Translator.ReadUInt32("Talent Count");
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadUInt32("Talent ID", i);
                packet.Translator.ReadUInt32("Rank", i);
            }
        }

        [Parser(Opcode.CMSG_LEARN_PREVIEW_TALENTS, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_LEARN_PREVIEW_TALENTS_PET, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTalentPreviewTalents434(Packet packet)
        {
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.CMSG_LEARN_PREVIEW_TALENTS_PET, Direction.ClientToServer))
                packet.Translator.ReadGuid("GUID");
            else
                packet.Translator.ReadUInt32("Tab Page");

            var count = packet.Translator.ReadUInt32("Talent Count");
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadUInt32("Talent ID", i);
                packet.Translator.ReadUInt32("Rank", i);
            }
        }

        [Parser(Opcode.CMSG_LEARN_TALENT)]
        public static void HandleLearnTalent(Packet packet)
        {
            packet.Translator.ReadUInt32("Talent ID");
            packet.Translator.ReadUInt32("Rank");
        }

        [Parser(Opcode.CMSG_LEARN_TALENTS)] // 5.1.0
        public static void HandleLearnTalents(Packet packet)
        {
            var talentCount = packet.Translator.ReadBits("Learned Talent Count", 25);

            for (int i = 0; i < talentCount; i++)
                packet.Translator.ReadUInt16("Talent Id", i);
        }

        [Parser(Opcode.SMSG_TALENTS_ERROR)]
        public static void HandleTalentError(Packet packet)
        {
            packet.Translator.ReadInt32E<TalentError>("Talent Error");
        }

        [Parser(Opcode.CMSG_SET_SPECIALIZATION)]
        public static void HandleSetSpec(Packet packet)
        {
            packet.Translator.ReadInt32("Spec Group Id");
        }

        //[Parser(Opcode.CMSG_UNLEARN_TALENTS)]

        //[Parser(Opcode.CMSG_PET_LEARN_TALENT)]
        //[Parser(Opcode.CMSG_PET_UNLEARN_TALENTS)]
        //[Parser(Opcode.CMSG_SET_ACTIVE_TALENT_GROUP_OBSOLETE)]
        //[Parser(Opcode.CMSG_SET_PRIMARY_TALENT_TREE)] 4.0.6a
    }
}
