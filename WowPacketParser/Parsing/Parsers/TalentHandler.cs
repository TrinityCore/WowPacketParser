using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class TalentHandler
    {
        public static void ReadTalentInfo(Packet packet)
        {
            packet.ReadUInt32("Free Talent count");
            var speccount = packet.ReadByte("Spec count");
            packet.ReadByte("Active Spec");
            for (var i = 0; i < speccount; ++i)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                    packet.ReadUInt32("TalentBranchSpec", i);
                var count2 = packet.ReadByte("Spec Talent Count", i);
                for (var j = 0; j < count2; ++j)
                {
                    packet.ReadUInt32("Talent Id", i, j);
                    packet.ReadByte("Rank", i, j);
                }

                var glyphs = packet.ReadByte("Glyph count", i);
                for (var j = 0; j < glyphs; ++j)
                    packet.ReadUInt16("Glyph", i, j);
            }
        }

        public static void ReadInspectPart(Packet packet)
        {
            var slotMask = packet.ReadUInt32("Slot Mask");
            var slot = 0;
            while (slotMask > 0)
            {
                if ((slotMask & 0x1) > 0)
                {
                    packet.ReadUInt32<ItemId>("Item Entry", (EquipmentSlotType)slot);
                    var enchantMask = packet.ReadUInt16();
                    if (enchantMask > 0)
                    {
                        var enchantName = string.Empty;
                        while (enchantMask > 0)
                        {
                            if ((enchantMask & 0x1) > 0)
                            {
                                enchantName += packet.ReadUInt16();
                                if (enchantMask > 1)
                                    enchantName += ", ";
                            }
                            enchantMask >>= 1;
                        }
                        packet.AddValue("Item Enchantments", enchantName, (EquipmentSlotType)slot);
                    }
                    packet.ReadUInt16("Unk UInt16", (EquipmentSlotType)slot);
                    packet.ReadPackedGuid("Creator GUID", (EquipmentSlotType)slot);
                    packet.ReadUInt32("Unk UInt32", (EquipmentSlotType)slot);
                }
                ++slot;
                slotMask >>= 1;
            }
        }

        [Parser(Opcode.SMSG_TALENTS_INVOLUNTARILY_RESET)]
        public static void HandleTalentsInvoluntarilyReset(Packet packet)
        {
            packet.ReadByte("Unk Byte");
        }

        [Parser(Opcode.SMSG_INSPECT_TALENT)]
        public static void HandleInspectTalent(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                packet.ReadGuid("GUID");
            else
                packet.ReadPackedGuid("GUID");

            ReadTalentInfo(packet);
            ReadInspectPart(packet);

            if (packet.CanRead()) // otherwise it would fail for players without a guild
            {
                packet.ReadGuid("Guild GUID");
                packet.ReadUInt32("Guild Level");
                packet.ReadUInt64("Guild Xp");
                packet.ReadUInt32("Guild Members");
            }
        }

        [Parser(Opcode.SMSG_INSPECT_RESULTS_UPDATE)]
        public static void HandleInspectResultsUpdate(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595)) // confirmed for 4.3.4
                packet.ReadPackedGuid("GUID");
            else if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                packet.ReadGuid("GUID");
            else
                packet.ReadPackedGuid("GUID");

            ReadInspectPart(packet);
        }

        [Parser(Opcode.MSG_TALENT_WIPE_CONFIRM)]
        public static void HandleTalent(Packet packet)
        {
            packet.ReadGuid("GUID");
            if (packet.Direction == Direction.ServerToClient)
                packet.ReadUInt32("Gold");
        }

        [Parser(Opcode.MSG_RESPEC_WIPE_CONFIRM)]
        public static void HandleRespecWipeConfirm(Packet packet)
        {
            packet.ReadByte("Spec Group");
            var guid = packet.StartBitStream(5, 3, 2, 7, 0, 6, 1, 4);
            packet.ParseBitStream(guid, 0, 1, 2, 3, 5, 6, 7, 4);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_UPDATE_TALENT_DATA, ClientVersionBuild.Zero, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleTalentsInfo(Packet packet)
        {
            var pet = packet.ReadBool("Pet Talents");
            if (pet)
            {
                packet.ReadUInt32("Unspent Talent");
                var count = packet.ReadByte("Talent Count");
                for (var i = 0; i < count; ++i)
                {
                    packet.ReadUInt32("Talent ID", i);
                    packet.ReadByte("Rank", i);
                }
            }
            else
                ReadTalentInfo(packet);
        }

        [Parser(Opcode.SMSG_UPDATE_TALENT_DATA, ClientVersionBuild.V5_1_0_16309)]
        public static void ReadTalentInfo510(Packet packet)
        {
            var specCount = packet.ReadByte("Spec Group count");
            packet.ReadByte("Active Spec Group");

            for (var i = 0; i < specCount; ++i)
            {
                packet.ReadUInt32("Spec Id", i);

                var spentTalents = packet.ReadByte("Spec Talent Count", i);
                for (var j = 0; j < spentTalents; ++j)
                    packet.ReadUInt16("Talent Id", i, j);

                var glyphCount = packet.ReadByte("Glyph count", i);
                for (var j = 0; j < glyphCount; ++j)
                    packet.ReadUInt16("Glyph", i, j);
            }
        }

        [Parser(Opcode.CMSG_LEARN_PREVIEW_TALENTS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_LEARN_PREVIEW_TALENTS_PET, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTalentPreviewTalents(Packet packet)
        {
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.CMSG_LEARN_PREVIEW_TALENTS_PET, Direction.ClientToServer))
                packet.ReadGuid("GUID");

            var count = packet.ReadUInt32("Talent Count");
            for (var i = 0; i < count; ++i)
            {
                packet.ReadUInt32("Talent ID", i);
                packet.ReadUInt32("Rank", i);
            }
        }

        [Parser(Opcode.CMSG_LEARN_PREVIEW_TALENTS, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_LEARN_PREVIEW_TALENTS_PET, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTalentPreviewTalents434(Packet packet)
        {
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.CMSG_LEARN_PREVIEW_TALENTS_PET, Direction.ClientToServer))
                packet.ReadGuid("GUID");
            else
                packet.ReadUInt32("Tab Page");

            var count = packet.ReadUInt32("Talent Count");
            for (var i = 0; i < count; ++i)
            {
                packet.ReadUInt32("Talent ID", i);
                packet.ReadUInt32("Rank", i);
            }
        }

        [Parser(Opcode.CMSG_LEARN_TALENT)]
        public static void HandleLearnTalent(Packet packet)
        {
            packet.ReadUInt32("Talent ID");
            packet.ReadUInt32("Rank");
        }

        [Parser(Opcode.CMSG_LEARN_TALENTS)] // 5.1.0
        public static void HandleLearnTalents(Packet packet)
        {
            var talentCount = packet.ReadBits("Learned Talent Count", 25);

            for (int i = 0; i < talentCount; i++)
                packet.ReadUInt16("Talent Id", i);
        }

        [Parser(Opcode.SMSG_TALENTS_ERROR)]
        public static void HandleTalentError(Packet packet)
        {
            packet.ReadInt32E<TalentError>("Talent Error");
        }

        [Parser(Opcode.CMSG_SET_SPECIALIZATION)]
        public static void HandleSetSpec(Packet packet)
        {
            packet.ReadInt32("Spec Group Id");
        }

        //[Parser(Opcode.CMSG_UNLEARN_TALENTS)]

        //[Parser(Opcode.CMSG_PET_LEARN_TALENT)]
        //[Parser(Opcode.CMSG_PET_UNLEARN_TALENTS)]
        //[Parser(Opcode.CMSG_SET_ACTIVE_TALENT_GROUP_OBSOLETE)]
        //[Parser(Opcode.CMSG_SET_PRIMARY_TALENT_TREE)] 4.0.6a
    }
}
