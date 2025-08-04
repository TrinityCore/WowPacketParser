using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class SpellHandler
    {
        public static void ReadSpellCastLogData(Packet packet, params object[] idx)
        {
            packet.ReadInt64("Health", idx);
            packet.ReadInt32("AttackPower", idx);
            packet.ReadInt32("SpellPower", idx);
            packet.ReadInt32("Armor", idx);
            packet.ReadInt32("Unknown_1105_1", idx);
            packet.ReadInt32("Unknown_1105_2", idx);

            packet.ResetBitReader();

            var spellLogPowerDataCount = packet.ReadBits("SpellLogPowerData", 9, idx);

            // SpellLogPowerData
            for (var i = 0; i < spellLogPowerDataCount; ++i)
            {
                packet.ReadByteE<PowerType>("PowerType", idx, i);
                packet.ReadInt32("Amount", idx, i);
                packet.ReadInt32("Cost", idx, i);
            }
        }

        public static void ReadTalentGroupInfo(Packet packet, params object[] idx)
        {
            packet.ReadInt32("SpecId", idx);

            var talentIDsCount = packet.ReadUInt32("TalentIDsCount", idx);
            var pvpTalentIDsCount = packet.ReadUInt32("PvPTalentIDsCount", idx);
            var glyphCount = packet.ReadUInt32("GlyphCount", idx);

            for (var i = 0; i < talentIDsCount; ++i)
                packet.ReadUInt16("TalentID", idx, i);

            for (var i = 0; i < pvpTalentIDsCount; ++i)
            {
                packet.ReadUInt16("PvPTalentID", idx, i);
                packet.ReadByte("Slot", idx, i);
            }

            for (var i = 0; i < glyphCount; ++i)
                packet.ReadUInt32("GlyphID", idx, i);
        }

        public static void ReadTalentInfoUpdate(Packet packet, params object[] idx)
        {
            packet.ReadByte("ActiveGroup", idx);
            packet.ReadInt32("PrimarySpecialization", idx);

            var talentGroupsCount = packet.ReadUInt32("TalentGroupsCount", idx);
            for (var i = 0; i < talentGroupsCount; ++i)
                ReadTalentGroupInfo(packet, idx, "TalentGroupsCount", i);
        }

        public static void ReadTalentInfoUpdateClassic(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("UnspentTalentPoints", idx);

            // This is always 0 or 1 (index)
            // Random values if IsPetTalents (probably uninitialized on serverside)
            packet.ReadByte("ActiveSpecGroup", idx);
            var specCount = packet.ReadUInt32("SpecCount", idx);

            for (var i = 0; i < specCount; ++i)
            {
                packet.ReadByte("TalentCount", idx, i);                     // Blizzard doing blizzard things
                var talentCount = packet.ReadUInt32("TalentCount", idx, i); // Blizzard doing blizzard things
                packet.ReadByte("GlyphCount", idx, i);                      // Blizzard doing blizzard things - Random values if IsPetTalents (probably uninitialized on serverside)
                var glyphCount = packet.ReadUInt32("GlyphCount", idx, i);   // Blizzard doing blizzard things
                // This is 0 (without dualspec learnt) and 1 or 2 with dualspec learnt
                // SpecID 0 and 1 = Index 0 (SpecGroup)
                // SpecID 2 = Index 1 (SpecGroup)
                packet.ReadByte("SpecID", idx, i);
                packet.ReadUInt32("PrimarySpecialization", idx, i);

                for (var j = 0; j < talentCount; ++j)
                {
                    packet.ReadUInt32("TalentID", idx, i, "TalentInfo", j);
                    packet.ReadUInt32("Rank", idx, i, "TalentInfo", j);
                }

                for (var k = 0; k < glyphCount; ++k)
                {
                    packet.ReadUInt16("Glyph", idx, i, "GlyphInfo", k);
                }
            }

            packet.ReadBit("IsPetTalents", idx);
        }

        [Parser(Opcode.SMSG_PLAYER_BOUND)]
        public static void HandlePlayerBound(Packet packet)
        {
            packet.ReadPackedGuid128("BinderID");
            packet.ReadUInt32<AreaId>("AreaID");
        }

        [Parser(Opcode.SMSG_CLEAR_TARGET)]
        public static void HandleClearTarget(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_MOUNT_RESULT)]
        public static void HandleMountResult(Packet packet)
        {
            packet.ReadInt32E<MountResult>("Result");
        }

        [Parser(Opcode.SMSG_DISMOUNT_RESULT)]
        public static void HandleDismountResult(Packet packet)
        {
            packet.ReadUInt32("Result");
        }

        [Parser(Opcode.SMSG_RESURRECT_REQUEST)]
        public static void HandleResurrectRequest(Packet packet)
        {
            packet.ReadPackedGuid128("ResurrectOffererGUID");

            packet.ReadUInt32("ResurrectOffererVirtualRealmAddress");
            packet.ReadUInt32("PetNumber");
            packet.ReadInt32<SpellId>("SpellID");

            var len = packet.ReadBits(11);

            packet.ReadBit("UseTimer");
            packet.ReadBit("Sickness");

            packet.ReadWoWString("Name", len);
        }

        [Parser(Opcode.SMSG_DISENCHANT_CREDIT)]
        public static void HandleDisenchantCredit(Packet packet)
        {
            packet.ReadPackedGuid128("Disenchanter");
            Substructures.ItemHandler.ReadItemInstance(packet);
        }

        [Parser(Opcode.SMSG_MISSILE_CANCEL)]
        public static void HandleMissileCancel(Packet packet)
        {
            packet.ReadPackedGuid128("OwnerGUID");
            packet.ReadUInt32<SpellId>("SpellID");
            packet.ReadBit("Reverse");
        }

        [Parser(Opcode.SMSG_SPELL_VISUAL_LOAD_SCREEN)]
        public static void HandleSpellVisualLoadScreen(Packet packet)
        {
            packet.ReadInt32("SpellVisualKitID");
            packet.ReadInt32("Delay");
        }

        [Parser(Opcode.SMSG_LEARN_TALENT_FAILED)]
        public static void HandleLearnTalentFailed(Packet packet)
        {
            packet.ReadBits("Reason", 4);
            packet.ReadInt32("SpellID");
            var count = packet.ReadUInt32("TalentCount");
            for (int i = 0; i < count; i++)
                packet.ReadUInt16("Talent");
        }

        [Parser(Opcode.SMSG_LEARN_PVP_TALENT_FAILED)]
        public static void HandleLearnPvPTalentFailed(Packet packet)
        {
            packet.ReadBits("Reason", 4);
            packet.ReadUInt32<SpellId>("SpellID");

            var talentCount = packet.ReadUInt32("TalentCount");
            for (int i = 0; i < talentCount; i++)
            {
                packet.ReadUInt16("PvPTalentID", i);
                packet.ReadByte("Slot", i);
            }
        }

        [Parser(Opcode.SMSG_UPDATE_TALENT_DATA)]
        public static void ReadUpdateTalentData(Packet packet)
        {
            ReadTalentInfoUpdate(packet, "Info");
        }

        [Parser(Opcode.SMSG_UPDATE_TALENT_DATA_CLASSIC)]
        public static void ReadUpdateTalentDataClassic(Packet packet)
        {
            ReadTalentInfoUpdateClassic(packet, "Info");
        }

        [Parser(Opcode.SMSG_RESPEC_WIPE_CONFIRM)]
        public static void HandleRespecWipeConfirm(Packet packet)
        {
            packet.ReadSByte("RespecType");
            packet.ReadUInt32("Cost");
            packet.ReadPackedGuid128("RespecMaster");
        }
    }
}
