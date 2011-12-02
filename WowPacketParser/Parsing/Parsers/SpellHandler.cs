using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.SMSG_PLAYERBOUND)]
        public static void HandlePlayerBound(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Area ID");
        }

        [Parser(Opcode.CMSG_CANCEL_TEMP_ENCHANTMENT)]
        public static void HandleCancelTempEnchantment(Packet packet)
        {
            packet.ReadUInt32("Slot");
        }

        [Parser(Opcode.SMSG_SUPERCEDED_SPELL)]
        public static void HandleSupercededSpell(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Next Spell ID");
        }

        [Parser(Opcode.SMSG_RESYNC_RUNES)]
        public static void HandleResyncRunes(Packet packet)
        {
            var count = packet.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
            {
                packet.ReadByte("Rune Type");
                packet.ReadByte("Cooldown Time");
            }
        }

        [Parser(Opcode.SMSG_COOLDOWN_EVENT)]
        public static void HandleCooldownEvent(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_SEND_UNLEARN_SPELLS)]
        public static void HandleSendUnlearnSpells(Packet packet)
        {
            var count = packet.ReadInt32("Count");
            for (var i = 0; i < count; i++)
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);
        }

        [Parser(Opcode.SMSG_RESUME_CAST_BAR)]
        public static void HandleResumeCastBar(Packet packet)
        {
            packet.ReadPackedGuid("Caster GUID");

            packet.ReadPackedGuid("Target GUID");

            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");

            packet.ReadInt32("Cast Time");

            packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_INITIAL_SPELLS)]
        public static void HandleInitialSpells(Packet packet)
        {
            packet.ReadByte("Talent Spec");

            var count = packet.ReadInt16("Spell Count");
            for (var i = 0; i < count; i++)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                    packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);
                else
                    packet.ReadEntryWithName<UInt16>(StoreNameType.Spell, "Spell ID", i);

                packet.ReadInt16("Unk Int16", i);
            }

            var cooldownCount = packet.ReadInt16("Cooldown Count");
            for (var i = 0; i < cooldownCount; i++)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                    packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Cooldown Spell ID", i);
                else
                    packet.ReadEntryWithName<UInt16>(StoreNameType.Spell, "Cooldown Spell ID", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                    packet.ReadInt32("Cooldown Cast Item ID");
                else
                    packet.ReadInt16("Cooldown Cast Item ID");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                    packet.ReadInt32("Cooldown Spell Category", i);
                else
                    packet.ReadInt16("Cooldown Spell Category", i);

                packet.ReadInt32("Cooldown Time", i);
                packet.ReadInt32("Cooldown Category Time", i);
            }
        }

        public static Aura ReadAuraUpdateBlock(ref Packet packet)
        {
            var aura = new Aura();

            aura.Slot = packet.ReadByte("Slot");

            var id = packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
            if (id <= 0)
                return null;
            aura.SpellId = (uint)id;

            var type = ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_0_14333) ? TypeCode.Int16 : TypeCode.Byte;
            aura.AuraFlags = packet.ReadEnum<AuraFlag>("Flags", type);

            aura.Level = packet.ReadByte("Level");

            aura.Charges = packet.ReadByte("Charges");

            if (!aura.AuraFlags.HasAnyFlag(AuraFlag.NotCaster))
                aura.CasterGuid = packet.ReadPackedGuid("Caster GUID");
            else
                aura.CasterGuid = new Guid(); // Is this needed?

            if (aura.AuraFlags.HasAnyFlag(AuraFlag.Duration))
            {
                aura.MaxDuration = packet.ReadInt32("Max Duration");
                aura.Duration = packet.ReadInt32("Duration");
            }
            else // Is this needed?
            {
                aura.MaxDuration = 0;
                aura.Duration = 0;
            }

            if (aura.AuraFlags.HasAnyFlag(AuraFlag.Scalable))
            {
                // This aura is scalable with level/talents
                // Here we show each effect value after scaling
                if (aura.AuraFlags.HasAnyFlag(AuraFlag.EffectIndex0))
                    packet.ReadInt32("Effect 0 Value");
                if (aura.AuraFlags.HasAnyFlag(AuraFlag.EffectIndex1))
                    packet.ReadInt32("Effect 1 Value");
                if (aura.AuraFlags.HasAnyFlag(AuraFlag.EffectIndex2))
                    packet.ReadInt32("Effect 2 Value");
            }

            return aura;
        }

        [Parser(Opcode.SMSG_AURA_UPDATE_ALL)]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            packet.ReadPackedGuid("GUID");

            /*Aura aura = null; */
            while (packet.CanRead())
            {
                /*aura =*/
                ReadAuraUpdateBlock(ref packet);
                // TODO: Add this aura to a list of objects (searching by guid)
            }
        }

        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            packet.ReadByte("Cast Count");
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");

            if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                packet.ReadInt32("Glyph Index");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_0_14333))
                packet.ReadEnum<CastFlag>("Cast Flags", TypeCode.Byte);

            ReadSpellCastTargets(ref packet);
        }

        public static TargetFlag ReadSpellCastTargets(ref Packet packet)
        {
            var targetFlags = packet.ReadEnum<TargetFlag>("Target Flags", TypeCode.Int32);

            if (targetFlags.HasAnyFlag(TargetFlag.Unit | TargetFlag.CorpseEnemy | TargetFlag.GameObject |
                TargetFlag.CorpseAlly | TargetFlag.UnitMinipet))
                packet.ReadPackedGuid("Target GUID");

            if (targetFlags.HasAnyFlag(TargetFlag.Item | TargetFlag.TradeItem))
                packet.ReadPackedGuid("Item Target GUID");

            if (targetFlags.HasAnyFlag(TargetFlag.SourceLocation))
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                    packet.ReadPackedGuid("Source Transport GUID");

                packet.ReadVector3("Source Position");
            }

            if (targetFlags.HasAnyFlag(TargetFlag.DestinationLocation))
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_8_9464))
                    packet.ReadPackedGuid("Destination Transport GUID");

                packet.ReadVector3("Destination Position");
            }

            if (targetFlags.HasAnyFlag(TargetFlag.NameString))
                packet.ReadCString("Target String");

            return targetFlags;
        }

        [Parser(Opcode.SMSG_SPELL_START)]
        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellStart(Packet packet)
        {
            bool isSpellGo = packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_SPELL_GO);

            packet.ReadPackedGuid("Caster GUID");
            packet.ReadPackedGuid("Caster Unit GUID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.ReadByte("Cast Count");

            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_2_9056) && !isSpellGo)
                packet.ReadByte("Cast Count");

            var flagsTypeCode = ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056) ? TypeCode.Int32 : TypeCode.UInt16;
            var flags = packet.ReadEnum<CastFlag>("Cast Flags", flagsTypeCode);

            packet.ReadInt32("Time");

            if (isSpellGo)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                    packet.ReadInt32("unk");
                var hitCount = packet.ReadByte("Hit Count");
                for (var i = 0; i < hitCount; i++)
                    packet.ReadGuid("Hit GUID", i);

                var missCount = packet.ReadByte("Miss Count");
                for (var i = 0; i < missCount; i++)
                {
                    var missGuid = packet.ReadGuid("Miss GUID", i);
                    packet.Writer.WriteLine("Miss GUID " + i + ": " + missGuid);

                    var missType = packet.ReadEnum<SpellMissType>("Miss Type", TypeCode.Byte, i);
                    if (missType != SpellMissType.Reflect)
                        continue;

                    packet.ReadEnum<SpellMissType>("Miss Reflect", TypeCode.Byte, i);
                }
            }

            var targetFlags = ReadSpellCastTargets(ref packet);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
            {
                if (flags.HasAnyFlag(CastFlag.PredictedPower))
                    packet.ReadInt32("Rune Cooldown");

                if (flags.HasAnyFlag(CastFlag.RuneInfo))
                {
                    var spellRuneState = packet.ReadByte("Spell Rune State");
                    var playerRuneState = packet.ReadByte("Player Rune State");

                    for (var i = 0; i < 6; i++)
                    {
                        var mask = 1 << i;
                        if ((mask & spellRuneState) == 0)
                            continue;

                        if ((mask & playerRuneState) != 0)
                            continue;

                        packet.ReadByte("Rune Cooldown Passed", i);
                    }
                }

                if (isSpellGo)
                {
                    if (flags.HasAnyFlag(CastFlag.AdjustMissile))
                    {
                        packet.ReadSingle("Unk Single");
                        packet.ReadInt32("Unk Int32 1");
                    }
                }
            }

            if (flags.HasAnyFlag(CastFlag.Projectile))
            {
                packet.ReadInt32("Ammo Display ID");
                packet.ReadEnum<InventoryType>("Ammo Inventory Type", TypeCode.Int32);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
            {
                if (isSpellGo)
                {
                    if (flags.HasAnyFlag(CastFlag.VisualChain))
                    {
                        packet.ReadInt32("Unk Int32 2");
                        packet.ReadInt32("Unk Int32 3");
                    }

                    if (targetFlags.HasAnyFlag(TargetFlag.DestinationLocation))
                        packet.ReadByte("Unk Byte 2");

                    if (targetFlags.HasAnyFlag(TargetFlag.ExtraTargets))
                    {
                        var targetCount = packet.ReadInt32("Extra Targets Count");
                        for (var i = 0; i < targetCount; i++)
                        {
                            packet.ReadVector3("Extra Target Position", i);
                            packet.ReadGuid("Extra Target GUID", i);
                        }
                    }
                }
                else
                {
                    if (flags.HasAnyFlag(CastFlag.Immunity))
                    {
                        packet.ReadInt32("Unk Int32 4");
                        packet.ReadInt32("Unk Int32 5");
                    }
                }
            }
        }

        [Parser(Opcode.SMSG_LEARNED_SPELL)]
        public static void HandleLearnedSpell(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                packet.ReadInt16("Unk Int16");
        }

        [Parser(Opcode.CMSG_UPDATE_PROJECTILE_POSITION)]
        public static void HandleUpdateProjectilePosition(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
            packet.ReadByte("Cast ID");
            packet.ReadVector3("Position");
        }

        [Parser(Opcode.SMSG_SET_PROJECTILE_POSITION)]
        public static void HandleSetProjectilePosition(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByte("Cast ID");
            packet.ReadVector3("Position");
        }

        [Parser(Opcode.SMSG_PERIODICAURALOG)]
        public static void HandleAuraCastLog(Packet packet)
        {
            packet.ReadPackedGuid("Target GUID");
            packet.ReadPackedGuid("Caster GUID");
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
            packet.ReadUInt32("Count");

            var aura = packet.ReadEnum<AuraType>("Aura Type", TypeCode.UInt32);
            switch (aura)
            {
                case AuraType.PeriodicDamage:
                case AuraType.PeriodicDamagePercent:
                {
                    packet.ReadUInt32("Damage");

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                        packet.ReadUInt32("Over damage");

                    packet.ReadUInt32("Spell Proto");
                    packet.ReadUInt32("Absorb");
                    packet.ReadUInt32("Resist");

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_2_9901))
                        packet.ReadByte("Critical");

                    break;
                }
                case AuraType.PeriodicHeal:
                case AuraType.ObsModHealth:
                {
                    packet.ReadUInt32("Damage");

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                        packet.ReadUInt32("Over damage");

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685)) // no idea when this was added exactly
                        packet.ReadUInt32("Absorb");

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_2_9901))
                        packet.ReadByte("Critical");

                    break;
                }
                case AuraType.ObsModPower:
                case AuraType.PeriodicEnergize:
                {
                    packet.ReadEnum<PowerType>("Power type", TypeCode.Int32);
                    packet.ReadUInt32("Amount");
                    break;
                }
                case AuraType.PeriodicManaLeech:
                {
                    packet.ReadEnum<PowerType>("Power type", TypeCode.Int32);
                    packet.ReadUInt32("Amount");
                    packet.ReadSingle("Gain multiplier");
                    break;
                }
            }
        }

        [Parser(Opcode.SMSG_REMOVED_SPELL)]
        public static void HandleRemovedSpell(Packet packet)
        {
            packet.ReadEntryWithName<UInt16>(StoreNameType.Spell, "Spell ID");
        }

        [Parser(Opcode.SMSG_REMOVED_SPELL, ClientVersionBuild.V3_1_0_9767)]
        [Parser(Opcode.CMSG_CANCEL_CHANNELLING)]
        public static void HandleRemovedSpell2(Packet packet)
        {
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL)]
        [Parser(Opcode.SMSG_PLAY_SPELL_IMPACT)]
        public static void HandleCastVisual(Packet packet)
        {
            packet.ReadGuid("Caster GUID");
            packet.ReadUInt32("SpellVisualKit ID");
        }

        [Parser(Opcode.SMSG_PET_CAST_FAILED)]
        [Parser(Opcode.SMSG_CAST_FAILED)]
        public static void HandleCastFailed(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                packet.ReadByte("Cast count");

            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");

            var result = packet.ReadEnum<SpellCastFailureReason>("Reason", TypeCode.Byte);

            if (ClientVersion.RemovedInVersion(ClientType.WrathOfTheLichKing))
                packet.ReadByte("Cast count");

            switch (result)
            {
                case SpellCastFailureReason.TooManyOfItem:
                    if (packet.CanRead())
                        packet.ReadUInt32("Limit");
                    break;
                case SpellCastFailureReason.Totems:
                case SpellCastFailureReason.TotemCategory:
                    if (packet.CanRead())
                        packet.ReadUInt32("Totem 1");
                    if (packet.CanRead())
                        packet.ReadUInt32("Totem 2");
                    break;
                case SpellCastFailureReason.Reagents:
                    packet.ReadUInt32("Reagent ID");
                    break;
                case SpellCastFailureReason.RequiresSpellFocus:
                    packet.ReadUInt32("Spell Focus");
                    break;
                case SpellCastFailureReason.RequiresArea:
                    packet.ReadUInt32("Area ID");
                    break;
                case SpellCastFailureReason.NotReady:
                    packet.ReadInt32("Unk");
                    break;
                case SpellCastFailureReason.CustomError:
                    packet.ReadUInt32("Error ID");
                    break;
                case SpellCastFailureReason.PreventedByMechanic:
                    packet.ReadEnum<SpellMechanics>("Mechanic", TypeCode.UInt32);
                    break;
                case SpellCastFailureReason.EquippedItemClass:
                    packet.ReadEnum<ItemClass>("Class", TypeCode.UInt32);
                    packet.ReadUInt32("Subclass");
                    break;
            }
        }

        [Parser(Opcode.SMSG_SPELLNONMELEEDAMAGELOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            packet.ReadPackedGuid("Target GUID");
            packet.ReadPackedGuid("Caster GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ReadUInt32("Damage");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183))
                packet.ReadUInt32("Overkill");

            packet.ReadByte("SchoolMask");
            packet.ReadUInt32("Absorb");
            packet.ReadUInt32("Resist");
            packet.ReadBoolean("Show spellname in log");
            packet.ReadByte("Unk byte");
            packet.ReadUInt32("Blocked");
            packet.ReadEnum<SpellHitType>("HitType", TypeCode.Int32);
            packet.ReadBoolean("Debug output");
        }

        [Parser(Opcode.SMSG_SPELLHEALLOG)]
        public static void HandleSpellHealLog(Packet packet)
        {
            packet.ReadPackedGuid("Target GUID");
            packet.ReadPackedGuid("Caster GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ReadUInt32("Damage");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183))
                packet.ReadUInt32("Overheal");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685)) // no idea when this was added exactly
                packet.ReadUInt32("Absorb");

            packet.ReadBoolean("Critical");
            packet.ReadBoolean("Debug output");
        }

        [Parser(Opcode.SMSG_SPELLINSTAKILLLOG)]
        public static void HandleSpellInstakillLog(Packet packet)
        {
            packet.ReadGuid("Target GUID");
            packet.ReadGuid("Caster GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
        }

        [Parser(Opcode.SMSG_SPELLENERGIZELOG)]
        public static void HandleSpellEnergizeLog(Packet packet)
        {
            packet.ReadPackedGuid("Target GUID");
            packet.ReadPackedGuid("Caster GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ReadEnum<PowerType>("Power type", TypeCode.UInt32);
            packet.ReadUInt32("Amount");
        }

        [Parser(Opcode.SMSG_PROCRESIST)]
        [Parser(Opcode.SMSG_SPELLORDAMAGE_IMMUNE)]
        public static void HandleSpellProcResist(Packet packet)
        {
            packet.ReadGuid("Caster GUID");
            packet.ReadGuid("Target GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ReadBoolean("Debug output");
        }

        [Parser(Opcode.SMSG_SPELLLOGMISS)]
        public static void HandleSpellLogMiss(Packet packet)
        {
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ReadGuid("Caster GUID");
            packet.ReadBoolean("Unk bool");

            var count = packet.ReadUInt32("Target count");
            for (var i = 0; i < count; ++i)
            {
                packet.ReadGuid("Target GUID");
                packet.ReadEnum<SpellMissType>("Miss info", TypeCode.Byte);
            }
        }

        [Parser(Opcode.MSG_CHANNEL_UPDATE)]
        public static void HandleSpellChannelUpdate(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadUInt32("Unk");
        }

        [Parser(Opcode.MSG_CHANNEL_START)]
        public static void HandleSpellChannelStart(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ReadInt32("Duration");
        }

        [Parser(Opcode.SMSG_BREAK_TARGET)]
        [Parser(Opcode.SMSG_DISMOUNT)]
        public static void HandleDismount(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
        }

        [Parser(Opcode.SMSG_MOUNTRESULT)]
        public static void HandleMountResult(Packet packet)
        {
            packet.ReadUInt32("Result"); // FIXME Enum?
        }

        [Parser(Opcode.SMSG_CLEAR_TARGET)]
        [Parser(Opcode.CMSG_GET_MIRRORIMAGE_DATA)]
        [Parser(Opcode.SMSG_SPIRIT_HEALER_CONFIRM)]
        public static void HandleClearTarget(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_MIRRORIMAGE_DATA)]
        public static void HandleMirrorImageData(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Display ID");
            packet.ReadEnum<Race>("Race", TypeCode.Byte);
            packet.ReadEnum<Gender>("Gender", TypeCode.Byte);
            packet.ReadEnum<Class>("Class", TypeCode.Byte);

            if (!packet.CanRead())
                return;

            packet.ReadByte("Skin");
            packet.ReadByte("Face");
            packet.ReadByte("Hair Style");
            packet.ReadByte("Hair Color");
            packet.ReadByte("Facial Hair");
            packet.ReadUInt32("Unk"); // uint64 in 4.3

            EquipmentSlotType[] slots = {
                EquipmentSlotType.Head, EquipmentSlotType.Shoulders, EquipmentSlotType.Shirt,
                EquipmentSlotType.Chest, EquipmentSlotType.Waist, EquipmentSlotType.Legs,
                EquipmentSlotType.Feet, EquipmentSlotType.Wrists, EquipmentSlotType.Hands,
                EquipmentSlotType.Back, EquipmentSlotType.Tabard };

            for (var i = 0; i < 11; ++i)
                packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "[" + slots[i] + "] Item Entry");
        }

        [Parser(Opcode.SMSG_CLEAR_COOLDOWN)]
        public static void HandleClearCooldown(Packet packet)
        {
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_SET_FLAT_SPELL_MODIFIER)]
        [Parser(Opcode.SMSG_SET_PCT_SPELL_MODIFIER)]
        public static void HandleSetSpellModifier(Packet packet)
        {
            packet.ReadByte("Effect");
            packet.ReadEnum<SpellModOp>("Spell Mod", TypeCode.Byte);
            packet.ReadInt32("Amount");
        }

        [Parser(Opcode.SMSG_SET_PCT_SPELL_MODIFIER, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleSetSpellModifier422(Packet packet)
        {
            packet.ReadByte("Effect");
            packet.ReadEnum<SpellModOp>("Spell Mod", TypeCode.Byte);
            packet.ReadByte("Amount");
            packet.ReadByte("Unk");
        }

        [Parser(Opcode.SMSG_DISPEL_FAILED)]
        public static void HandleDispelFailed(Packet packet)
        {
            packet.ReadGuid("Caster GUID");
            packet.ReadGuid("Target GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Dispel Spell ID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
        }

        [Parser(Opcode.SMSG_TOTEM_CREATED)]
        public static void HandleTotemCreated(Packet packet)
        {
            packet.ReadByte("Unk Byte");
            packet.ReadGuid("GUID1");
            packet.ReadGuid("GUID2");
        }

        [Parser(Opcode.CMSG_CANCEL_CAST)]
        public static void HandlePlayerCancelCast(Packet packet)
        {
            packet.ReadByte("Count");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell Id");
        }
    }
}
