using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class SpellHandler
    {
        public static void ReadSpellCastLogData(ref Packet packet)
        {
            packet.ReadInt32("Health");
            packet.ReadInt32("AttackPower");
            packet.ReadInt32("SpellPower");

            var int3 = packet.ReadInt32("SpellLogPowerData");

            // SpellLogPowerData
            for (var i = 0; i < int3; ++i)
            {
                packet.ReadInt32("PowerType", i);
                packet.ReadInt32("Amount", i);
            }

            packet.ResetBitReader();

            var bit32 = packet.ReadBit("bit32");

            if (bit32)
                packet.ReadSingle("Float7");
        }
        public static void ReadSpellCastRequest(ref Packet packet)
        {
            packet.ReadByte("CastID");
            packet.ReadUInt32("SpellID");
            packet.ReadUInt32("Misc");

            // SpellTargetData
            packet.ResetBitReader();

            packet.ReadEnum<TargetFlag>("Flags", 21);
            var bit72 = packet.ReadBit("HasSrcLocation");
            var bit112 = packet.ReadBit("HasDstLocation");
            var bit124 = packet.ReadBit("HasOrientation");
            var bits128 = packet.ReadBits(7);

            packet.ReadPackedGuid128("Unit Guid");
            packet.ReadPackedGuid128("Item Guid");

            if (bit72)
            {
                packet.ReadPackedGuid128("SrcLocation Guid");
                packet.ReadVector3("SrcLocation");
            }

            if (bit112)
            {
                packet.ReadPackedGuid128("DstLocation Guid");
                packet.ReadVector3("DstLocation");
            }

            if (bit124)
                packet.ReadSingle("Orientation");

            packet.ReadWoWString("Name", bits128);

            // MissileTrajectoryRequest
            packet.ReadSingle("Pitch");
            packet.ReadSingle("Speed");

            packet.ReadPackedGuid128("Guid");

            packet.ResetBitReader();

            packet.ReadBits("SendCastFlags", 5);
            var bit456 = packet.ReadBit("HasMoveUpdate"); // MoveUpdate

            var bits116 = packet.ReadBits("SpellWeightCount", 2); // SpellWeight

            // MoveUpdate
            if (bit456)
                MovementHandler.ReadMovementStats(ref packet);

            // SpellWeight
            for (var i = 0; i < bits116; ++i)
            {
                packet.ResetBitReader();
                packet.ReadBits("Type", 2, i);
                packet.ReadInt32("ID", i);
                packet.ReadInt32("Quantity", i);
            }
        }

        [Parser(Opcode.SMSG_LEARNED_SPELL)]
        public static void HandleLearnSpell(Packet packet)
        {
            var count = packet.ReadUInt32("Spell Count");

            for (var i = 0; i < count; ++i)
                packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID", i);
            packet.ReadBit("Unk Bits");
        }

        [Parser(Opcode.SMSG_INITIAL_SPELLS)]
        public static void HandleInitialSpells(Packet packet)
        {
            packet.ReadBit("Unk Bit");
            var count = packet.ReadUInt32("Spell Count");

            var spells = new List<uint>((int)count);
            for (var i = 0; i < count; i++)
            {
                var spellId = packet.ReadEntry<UInt32>(StoreNameType.Spell, "Spell ID", i);
                spells.Add(spellId);
            }

            var startSpell = new StartSpell { Spells = spells };

            WoWObject character;
            if (Storage.Objects.TryGetValue(WowPacketParser.Parsing.Parsers.SessionHandler.LoginGuid, out character))
            {
                var player = character as Player;
                if (player != null && player.FirstLogin)
                    Storage.StartSpells.Add(new Tuple<Race, Class>(player.Race, player.Class), startSpell, packet.TimeSpan);
            }
        }

        [Parser(Opcode.SMSG_SPELL_CATEGORY_COOLDOWN)]
        public static void HandleSpellCategoryCooldown(Packet packet)
        {
            var count = packet.ReadUInt32("Spell Count");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Category", i);
                packet.ReadInt32("ModCooldown", i);
            }
        }

        [Parser(Opcode.SMSG_SET_PCT_SPELL_MODIFIER)]
        [Parser(Opcode.SMSG_SET_FLAT_SPELL_MODIFIER)]
        public static void HandleSetSpellModifierFlat(Packet packet)
        {
            var modCount = packet.ReadUInt32("Modifier type count");

            for (var j = 0; j < modCount; ++j)
            {
                packet.ReadEnum<SpellModOp>("Spell Mod", TypeCode.Byte, j);

                var modTypeCount = packet.ReadUInt32("Count", j);
                for (var i = 0; i < modTypeCount; ++i)
                {
                    packet.ReadSingle("Amount", j, i);
                    packet.ReadByte("Spell Mask bitpos", j, i);
                }
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            packet.ReadBit("bit16");
            var guid = packet.ReadPackedGuid128("Guid");
            var count = packet.ReadUInt32("AuraCount");

            var auras = new List<Aura>();
            for (var i = 0; i < count; ++i)
            {
                var aura = new Aura();

                packet.ReadByte("Slot", i);

                packet.ResetBitReader();
                var hasAura = packet.ReadBit("HasAura", i);
                if (hasAura)
                {
                    aura.SpellId = (uint)packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID", i);
                    aura.AuraFlags = packet.ReadEnum<AuraFlagMoP>("Flags", TypeCode.Byte, i);
                    packet.ReadInt32("Effect Mask", i);
                    aura.Level = packet.ReadUInt16("Caster Level", i);
                    aura.Charges = packet.ReadByte("Charges", i);

                    var int72 = packet.ReadUInt32("Int56 Count", i);
                    var effectCount = packet.ReadUInt32("Effect Count", i);

                    for (var j = 0; j < int72; ++j)
                        packet.ReadSingle("Float15", i, j);

                    for (var j = 0; j < effectCount; ++j)
                        packet.ReadSingle("Effect Value", i, j);

                    packet.ResetBitReader();
                    var hasCasterGUID = packet.ReadBit("hasCasterGUID", i);
                    var hasDuration = packet.ReadBit("hasDuration", i);
                    var hasMaxDuration = packet.ReadBit("hasMaxDuration", i);

                    if (hasCasterGUID)
                        packet.ReadPackedGuid128("Caster Guid", i);

                    if (hasDuration)
                        aura.Duration = packet.ReadInt32("Duration", i);
                    else
                        aura.Duration = 0;

                    if (hasMaxDuration)
                        aura.MaxDuration = packet.ReadInt32("Max Duration", i);
                    else
                        aura.MaxDuration = 0;

                    auras.Add(aura);
                    packet.AddSniffData(StoreNameType.Spell, (int)aura.SpellId, "AURA_UPDATE");
                }
            }

            if (Storage.Objects.ContainsKey(guid))
            {
                var unit = Storage.Objects[guid].Item1 as Unit;
                if (unit != null)
                {
                    // If this is the first packet that sends auras
                    // (hopefully at spawn time) add it to the "Auras" field,
                    // if not create another row of auras in AddedAuras
                    // (similar to ChangedUpdateFields)

                    if (unit.Auras == null)
                        unit.Auras = auras;
                    else
                        unit.AddedAuras.Add(auras);
                }
            }
        }

        [Parser(Opcode.SMSG_TALENTS_INFO)]
        public static void ReadTalentInfo(Packet packet)
        {
            packet.ReadByte("Active Spec Group");

            var specCount = packet.ReadInt32("Spec Group count");
            for (var i = 0; i < specCount; ++i)
            {
                packet.ReadUInt32("Spec Id", i);
                var spentTalents = packet.ReadInt32("Spec Talent Count", i);

                for (var j = 0; j < 6; ++j)
                    packet.ReadUInt16("Glyph", i, j);

                for (var j = 0; j < spentTalents; ++j)
                    packet.ReadUInt16("Talent Id", i, j);
            }
        }

        [Parser(Opcode.CMSG_CAST_SPELL)]
        [Parser(Opcode.CMSG_PET_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.CMSG_PET_CAST_SPELL, Direction.ClientToServer))
                packet.ReadPackedGuid128("PetGUID");

            ReadSpellCastRequest(ref packet);
        }

        [Parser(Opcode.SMSG_SPELL_START)]
        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellStart(Packet packet)
        {
            bool isSpellGo = packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_SPELL_GO, Direction.ServerToClient);

            packet.ReadPackedGuid128("Caster Guid");
            packet.ReadPackedGuid128("CasterUnit Guid");

            packet.ReadByte("CastID");

            packet.ReadUInt32("SpellID");
            packet.ReadUInt32("CastFlags");
            packet.ReadUInt32("CastTime");

            var int52 = packet.ReadUInt32("HitTargets");
            var int68 = packet.ReadUInt32("MissTargets");
            var int84 = packet.ReadUInt32("MissStatus");

            // SpellTargetData
            packet.ResetBitReader();

            packet.ReadEnum<TargetFlag>("Flags", 21);
            var bit72 = packet.ReadBit("HasSrcLocation");
            var bit112 = packet.ReadBit("HasDstLocation");
            var bit124 = packet.ReadBit("HasOrientation");
            var bits128 = packet.ReadBits(7);

            packet.ReadPackedGuid128("Unit Guid");
            packet.ReadPackedGuid128("Item Guid");

            if (bit72)
            {
                packet.ReadPackedGuid128("SrcLocation Guid");
                packet.ReadVector3("SrcLocation");
            }

            if (bit112)
            {
                packet.ReadPackedGuid128("DstLocation Guid");
                packet.ReadVector3("DstLocation");
            }

            if (bit124)
                packet.ReadSingle("Orientation");

            packet.ReadWoWString("Name", bits128);

            var int360 = packet.ReadUInt32("SpellPowerData");

            // MissileTrajectoryResult
            packet.ReadUInt32("TravelTime");
            packet.ReadSingle("Pitch");

            // SpellAmmo
            packet.ReadUInt32("DisplayID");
            packet.ReadByte("InventoryType");

            packet.ReadByte("DestLocSpellCastIndex");

            var int428 = packet.ReadUInt32("TargetPoints");

            // CreatureImmunities
            packet.ReadUInt32("School");
            packet.ReadUInt32("Value");

            // SpellHealPrediction
            packet.ReadUInt32("Points");
            packet.ReadByte("Type");
            packet.ReadPackedGuid128("BeaconGUID");

            // HitTargets
            for (var i = 0; i < int52; ++i)
                packet.ReadPackedGuid128("HitTarget Guid", i);

            // MissTargets
            for (var i = 0; i < int68; ++i)
                packet.ReadPackedGuid128("MissTarget Guid", i);

            // MissStatus
            for (var i = 0; i < int84; ++i)
            {
                packet.ResetBitReader();
                if (packet.ReadBits("Reason", 4, i) == 11)
                    packet.ReadBits("ReflectStatus", 4, i);
            }

            // SpellPowerData
            for (var i = 0; i < int360; ++i)
            {
                packet.ReadInt32("Cost", i);
                packet.ReadEnum<PowerType>("Type", TypeCode.Byte, i);
            }

            // TargetPoints
            for (var i = 0; i < int428; ++i)
            {
                packet.ReadPackedGuid128("Transport Guid");
                packet.ReadVector3("Location");
            }

            packet.ResetBitReader();

            packet.ReadBits("CastFlagsEx", 18);

            var bit396 = packet.ReadBit("HasRuneData");
            var bit424 = packet.ReadBit("HasProjectileVisual");

            // RuneData
            if (bit396)
            {
                packet.ReadByte("Start");
                packet.ReadByte("Count");

                packet.ResetBitReader();
                var bits1 = packet.ReadBits("CooldownCount", 3);

                for (var i = 0; i < bits1; ++i)
                    packet.ReadByte("Cooldowns", i);
            }

            // ProjectileVisual
            if (bit424)
                for (var i = 0; i < 2; ++i)
                    packet.ReadInt32("Id", i);

            if (isSpellGo)
            {

                packet.ResetBitReader();

                var bit52 = packet.ReadBit("SpellCastLogData");
                if (bit52)
                    ReadSpellCastLogData(ref packet);
            }
        }

        [Parser(Opcode.SMSG_WEEKLY_SPELL_USAGE)]
        public static void HandleWeeklySpellUsage(Packet packet)
        {
            var count = packet.ReadUInt32("Count");

            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt32("Category");
                packet.ReadByte("Uses");
            }
        }

        [Parser(Opcode.SMSG_CHANNEL_UPDATE)]
        public static void HandleSpellChannelUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadInt32("TimeRemaining");
        }

        [Parser(Opcode.SMSG_CHANNEL_START)]
        public static void HandleSpellChannelStart(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadInt32("SpellID");
            packet.ReadInt32("ChannelDuration");

            var bit84 = packet.ReadBit("HasInterruptImmunities");
            var bit24 = packet.ReadBit("HasHealPrediction");

            // SpellChannelStartInterruptImmunities
            if (bit84)
            {
                packet.ReadInt32("SchoolImmunities");
                packet.ReadInt32("Immunities");
            }

            // SpellTargetedHealPrediction
            if (bit24)
            {
                packet.ReadPackedGuid128("TargetGUID");

                // SpellHealPrediction
                packet.ReadInt32("Points");
                packet.ReadByte("Type");
                packet.ReadPackedGuid128("BeaconGUID");
            }
        }

        [Parser(Opcode.SMSG_SPELL_UPDATE_CHAIN_TARGETS)]
        public static void HandleUpdateChainTargets(Packet packet)
        {
            packet.ReadPackedGuid128("Caster GUID");
            packet.ReadEntry<UInt32>(StoreNameType.Spell, "Spell ID");
            var count = packet.ReadInt32("Count");
            for (var i = 0; i < count; i++)
                packet.ReadPackedGuid128("Targets", i);
        }

        [Parser(Opcode.CMSG_CANCEL_AURA)]
        public static void HandleCanelAura(Packet packet)
        {
            packet.ReadEntry<UInt32>(StoreNameType.Spell, "SpellID");
            packet.ReadPackedGuid128("CasterGUID");
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL)]
        public static void HandleCastVisual(Packet packet)
        {
            packet.ReadPackedGuid128("Source");
            packet.ReadPackedGuid128("Target");

            packet.ReadVector3("TargetPosition");

            packet.ReadInt32("SpellVisualID");
            packet.ReadSingle("TravelSpeed");

            packet.ReadInt16("MissReason");
            packet.ReadInt16("ReflectStatus");

            packet.ReadSingle("Orientation");

            packet.ReadBit("SpeedAsTime");
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL_KIT)]
        public static void HandleCastVisualKit(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
            packet.ReadInt32("KitType");
            packet.ReadUInt32("Duration");
            packet.ReadInt32("KitRecID");
        }

        [Parser(Opcode.CMSG_UNLEARN_SKILL)]
        public static void HandleUnlearnSkill(Packet packet)
        {
            packet.ReadInt32("SkillLine");
        }

        [Parser(Opcode.SMSG_CAST_FAILED)]
        public static void HandleCastFailed(Packet packet)
        {
            packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID");
            packet.ReadInt32("Reason");
            packet.ReadInt32("FailedArg1");
            packet.ReadInt32("FailedArg2");

            packet.ReadByte("Cast count");
        }

        [Parser(Opcode.SMSG_SPELL_FAILURE)]
        public static void HandleSpellFailure(Packet packet)
        {
            packet.ReadPackedGuid128("CasterUnit");
            packet.ReadByte("CastID");
            packet.ReadEntry<UInt32>(StoreNameType.Spell, "SpellID");
            packet.ReadEnum<SpellCastFailureReason>("Reason", TypeCode.Byte);
        }

        [Parser(Opcode.SMSG_SPELL_FAILED_OTHER)]
        public static void HandleSpellFailedOther(Packet packet)
        {
            packet.ReadPackedGuid128("CasterUnit");
            packet.ReadByte("CastID");
            packet.ReadEntry<UInt32>(StoreNameType.Spell, "SpellID");
            packet.ReadEnum<SpellCastFailureReason>("Reason", TypeCode.Int16);
        }

        [Parser(Opcode.SMSG_REFRESH_SPELL_HISTORY)]
        [Parser(Opcode.SMSG_SEND_SPELL_HISTORY)]
        public static void HandleSendSpellHistory(Packet packet)
        {
            var int4 = packet.ReadInt32("SpellHistoryEntryCount");
            for (int i = 0; i < int4; i++)
            {
                packet.ReadUInt32("SpellID", i);
                packet.ReadUInt32("ItemID", i);
                packet.ReadUInt32("Category", i);
                packet.ReadInt32("RecoveryTime", i);
                packet.ReadInt32("CategoryRecoveryTime", i);

                packet.ResetBitReader();

                packet.ReadBit("OnHold", i);
            }
        }

        [Parser(Opcode.SMSG_SEND_SPELL_CHARGES)]
        public static void HandleSendSpellCharges(Packet packet)
        {
            var int4 = packet.ReadInt32("SpellChargeEntryCount");
            for (int i = 0; i < int4; i++)
            {
                packet.ReadUInt32("Category", i);
                packet.ReadUInt32("NextRecoveryTime", i);
                packet.ReadByte("ConsumedCharges", i);
            }
        }

        [Parser(Opcode.SMSG_AURA_POINTS_DEPLETED)]
        public static void HandleAuraPointsDepleted(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
            packet.ReadByte("Slot");
            packet.ReadByte("EffectIndex");
        }

        [Parser(Opcode.SMSG_SPELL_MULTISTRIKE_EFFECT)]
        public static void HandleSpellMultistrikeEffect(Packet packet)
        {
            packet.ReadPackedGuid128("Caster");
            packet.ReadPackedGuid128("Target");
            packet.ReadInt32("SpellID");
            packet.ReadInt16("ProcCount");
            packet.ReadInt16("ProcNum");
        }

        [Parser(Opcode.SMSG_SPELLDISPELLOG)]
        public static void HandleSpellInterruptLog(Packet packet)
        {
            packet.ReadBit("Is Steal");
            packet.ReadBit("Is Break");
            packet.ReadPackedGuid128("Target GUID");
            packet.ReadPackedGuid128("Caster GUID");
            packet.ReadUInt32("Spell ID");
            var dataSize = packet.ReadUInt32("Dispel count");
            for (var i = 0; i < dataSize; ++i)
            {
                packet.ResetBitReader();
                packet.ReadUInt32("Spell ID", i);
                packet.ReadBit("Is Harmful", i);
                var hasRolled = packet.ReadBit("Has Rolled", i);
                var hasNeeded = packet.ReadBit("Has Needed", i);
                if (hasRolled)
                    packet.ReadUInt32("Rolled", i);
                if (hasNeeded)
                    packet.ReadUInt32("Needed", i);
            }
        }

        [Parser(Opcode.SMSG_RESUME_CAST_BAR)]
        public static void HandleResumeCastBar(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadPackedGuid128("Target");

            packet.ReadUInt32("SpellID");
            packet.ReadUInt32("TimeRemaining");
            packet.ReadUInt32("TotalTime");

            var result = packet.ReadBit("HasInterruptImmunities");
            if (result)
            {
                packet.ReadUInt32("SchoolImmunities");
                packet.ReadUInt32("Immunities");
            }
        }
    }
}
