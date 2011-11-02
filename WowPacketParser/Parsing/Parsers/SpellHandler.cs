using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.SMSG_COOLDOWN_EVENT)]
        public static void HandleCooldownEvent(Packet packet)
        {
            Console.WriteLine("Spell ID: " + Extensions.SpellLine(packet.ReadInt32()));
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_SEND_UNLEARN_SPELLS)]
        public static void HandleSendUnlearnSpells(Packet packet)
        {
            var count = packet.ReadInt32("Count");
            for (var i = 0; i < count; i++)
                Console.WriteLine("[" + i + "] Spell ID: " + Extensions.SpellLine(packet.ReadInt32()));
        }

        [Parser(Opcode.SMSG_RESUME_CAST_BAR)]
        public static void HandleResumeCastBar(Packet packet)
        {
            var casterGuid = packet.ReadPackedGuid();
            Console.WriteLine("Caster GUID: " + casterGuid);

            var targetGuid = packet.ReadPackedGuid();
            Console.WriteLine("Target GUID: " + targetGuid);

            var spellId = packet.ReadInt32();
            Console.WriteLine("Spell ID: " + Extensions.SpellLine(spellId));

            var castTime = packet.ReadInt32();
            Console.WriteLine("Cast Time: " + castTime);

            var unkInt = packet.ReadInt32();
            Console.WriteLine("Unk Int32: " + unkInt);
        }

        [Parser(Opcode.SMSG_INITIAL_SPELLS)]
        public static void HandleInitialSpells(Packet packet)
        {
            var talentSpec = packet.ReadByte();
            Console.WriteLine("Talent Spec: " + talentSpec);

            var count = packet.ReadInt16();
            Console.WriteLine("Spell Count: " + count);

            for (var i = 0; i < count; i++)
            {
                int spellId;
                if (ClientVersion.Version >= ClientVersionBuild.V3_1_0_9767)
                    spellId = packet.ReadInt32();
                else
                    spellId = packet.ReadUInt16();

                Console.WriteLine("Spell ID " + i + ": " + Extensions.SpellLine(spellId));

                var unk16 = packet.ReadInt16();
                Console.WriteLine("Unk Int16: " + unk16);
            }

            var cooldownCount = packet.ReadInt16();
            Console.WriteLine("Cooldown Count: " + cooldownCount);

            for (var i = 0; i < cooldownCount; i++)
            {
                var spellId = packet.ReadInt32();
                Console.WriteLine("Cooldown Spell ID " + i + ": " + Extensions.SpellLine(spellId));

                var castItemId = packet.ReadInt16();
                Console.WriteLine("Cooldown Cast Item ID " + i + ": " + castItemId);

                var spellCat = packet.ReadInt16();
                Console.WriteLine("Cooldown Spell Category " + i + ": " + spellCat);

                var cooldown = packet.ReadInt32();
                Console.WriteLine("Cooldown Time " + i + ": " + cooldown);

                var catCooldown = packet.ReadInt32();
                Console.WriteLine("Cooldown Category Time " + i + ": " + catCooldown);
            }
        }

        public static Aura ReadAuraUpdateBlock(Packet packet)
        {
            var aura = new Aura();

            aura.Slot = packet.ReadByte("Slot");

            var id = packet.ReadInt32();
            Console.WriteLine("ID: " + Extensions.SpellLine(id));
            if (id <= 0)
                return null;
            aura.SpellId = (uint)id;

            var type = ClientVersion.Version > ClientVersionBuild.V4_2_0_14333 ? TypeCode.Int16 : TypeCode.Byte;
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

            if (aura.AuraFlags.HasAnyFlag(AuraFlag.Unknown))
            {
                if (aura.AuraFlags.HasAnyFlag(AuraFlag.EffectIndex0))
                    packet.ReadInt32("Unknown Effect 0");
                if (aura.AuraFlags.HasAnyFlag(AuraFlag.EffectIndex1))
                    packet.ReadInt32("Unknown Effect 1");
                if (aura.AuraFlags.HasAnyFlag(AuraFlag.EffectIndex2))
                    packet.ReadInt32("Unknown Effect 2");
            }

            return aura;
        }

        [Parser(Opcode.SMSG_AURA_UPDATE_ALL)]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            packet.ReadPackedGuid("GUID");

            /*var aura = new Aura(); */
            while (packet.CanRead())
                /*aura =*/ ReadAuraUpdateBlock(packet);
                // TODO: Add this aura to a list of objects (searching by guid)
        }

        [Parser(Opcode.SMSG_SPELL_START)]
        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellStart(Packet packet)
        {
            bool isSpellGo = packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_SPELL_GO);

            packet.ReadPackedGuid("Caster GUID");
            packet.ReadPackedGuid("Caster Unit GUID");

            if (ClientVersion.Version > ClientVersionBuild.V2_4_3_8606)
                packet.ReadByte("Cast Count");

            Console.WriteLine("Spell ID: " + Extensions.SpellLine(packet.ReadInt32()));

            if (ClientVersion.Version <= ClientVersionBuild.V2_4_3_8606 && !isSpellGo)
                packet.ReadByte("Cast Count");

            var flagsTypeCode = ClientVersion.Version > ClientVersionBuild.V2_4_3_8606 ? TypeCode.Int32 : TypeCode.UInt16;
            var flags = packet.ReadEnum<CastFlag>("Cast Flags", flagsTypeCode);

            packet.ReadInt32("Time");

            if (isSpellGo)
            {
                var hitCount = packet.ReadByte("Hit Count");
                for (var i = 0; i < hitCount; i++)
                    packet.ReadGuid("Hit GUID", i);

                var missCount = packet.ReadByte("Miss Count");
                for (var i = 0; i < missCount; i++)
                {
                    var missGuid = packet.ReadGuid("Miss GUID", i);
                    Console.WriteLine("Miss GUID " + i + ": " + missGuid);

                    var missType = packet.ReadEnum<SpellMissType>("Miss Type", TypeCode.Byte, i);
                    if (missType != SpellMissType.Reflect)
                        continue;

                    packet.ReadEnum<SpellMissType>("Miss Reflect", TypeCode.Byte, i);
                }
            }

            var targetFlags = packet.ReadEnum<TargetFlag>("Target Flags", TypeCode.Int32);

            if (targetFlags.HasAnyFlag(TargetFlag.Unit | TargetFlag.PvpCorpse | TargetFlag.Object |
                TargetFlag.Corpse | TargetFlag.SpellDynamic4))
                packet.ReadPackedGuid("Target GUID");

            if (targetFlags.HasAnyFlag(TargetFlag.Item | TargetFlag.TradeItem))
                packet.ReadPackedGuid("Item Target GUID");

            if (targetFlags.HasAnyFlag(TargetFlag.SourceLocation))
            {
                if (ClientVersion.Version >= ClientVersionBuild.V3_2_0_10192)
                    packet.ReadPackedGuid("Source Transport GUID");

                packet.ReadVector3("Source Position");
            }

            if (targetFlags.HasAnyFlag(TargetFlag.DestinationLocation))
            {
                if (ClientVersion.Version >= ClientVersionBuild.V3_0_8_9464)
                    packet.ReadPackedGuid("Destination Transport GUID");

                packet.ReadVector3("Destination Position");
            }

            if (targetFlags.HasAnyFlag(TargetFlag.NameString))
                packet.ReadCString("Target String");

            if (ClientVersion.Version > ClientVersionBuild.V2_4_3_8606)
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

                        packet.ReadByte("Unk Byte 1", i);
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

            if (ClientVersion.Version > ClientVersionBuild.V2_4_3_8606)
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
            var spellId = packet.ReadInt32();
            Console.WriteLine("Spell ID: " + Extensions.SpellLine(spellId));

            var unk = packet.ReadInt16();
            Console.WriteLine("Unk Int16: " + unk);
        }

        [Parser(Opcode.CMSG_UPDATE_PROJECTILE_POSITION)]
        public static void HandleUpdateProjectilePosition(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);

            var spellId = packet.ReadInt32();
            Console.WriteLine("Spell ID: " + Extensions.SpellLine(spellId));

            var castId = packet.ReadByte();
            Console.WriteLine("Cast ID: " + castId);

            var pos = packet.ReadVector3();
            Console.WriteLine("Position: " + pos);
        }

        [Parser(Opcode.SMSG_SET_PROJECTILE_POSITION)]
        public static void HandleSetProjectilePosition(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);

            var castId = packet.ReadByte();
            Console.WriteLine("Cast ID: " + castId);

            var pos = packet.ReadVector3();
            Console.WriteLine("Position: " + pos);
        }

        [Parser(Opcode.SMSG_PERIODICAURALOG)]
        public static void HandleAuraCastLog(Packet packet)
        {
            var packGuid = packet.ReadPackedGuid();
            Console.WriteLine("Target GUID: " + packGuid);

            var guid = packet.ReadPackedGuid();
            Console.WriteLine("Caster GUID: " + guid);

            var spellId = packet.ReadUInt32();
            Console.WriteLine("Spell ID: " + Extensions.SpellLine((int)spellId));

            var count = packet.ReadUInt32();
            Console.WriteLine("Count: " + count);

            var aura = (AuraType)packet.ReadUInt32();
            Console.WriteLine("Aura: " + aura);

            switch (aura)
            {
                case AuraType.PeriodicDamage:
                case AuraType.PeriodicDamagePercent:
                {
                    var damage = packet.ReadUInt32();
                    Console.WriteLine("Damage: " + damage);

                    if (ClientVersion.Version > ClientVersionBuild.V2_4_3_8606)
                        packet.ReadUInt32("Over damage");

                    var spellProto = packet.ReadUInt32();
                    Console.WriteLine("Spell proto: " + spellProto);

                    var absorb = packet.ReadUInt32();
                    Console.WriteLine("Absorb: " + absorb);

                    var resist = packet.ReadUInt32();
                    Console.WriteLine("Resist: " + resist);

                    if (ClientVersion.Version >= ClientVersionBuild.V3_1_2_9901)
                        packet.ReadByte("Critical");

                    break;
                }
                case AuraType.PeriodicHeal:
                case AuraType.ObsModHealth:
                {
                    var damage = packet.ReadUInt32();
                    Console.WriteLine("Damage: " + damage);

                    if (ClientVersion.Version > ClientVersionBuild.V2_4_3_8606)
                        packet.ReadUInt32("Over damage");

                    if (ClientVersion.Version >= ClientVersionBuild.V3_3_5_12213) // no idea when this was added exactly
                        packet.ReadUInt32("Absorb");

                    if (ClientVersion.Version >= ClientVersionBuild.V3_1_2_9901)
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
                default:
                {
                    Console.WriteLine("Aura type not handled.");
                    break;
                }
            }
        }

        [Parser(Opcode.SMSG_REMOVED_SPELL)]
        [Parser(Opcode.CMSG_CANCEL_CHANNELLING)]
        public static void HandleRemovedSpell(Packet packet)
        {
            var spellId = packet.ReadUInt32();
            Console.WriteLine("Spell ID: " + Extensions.SpellLine((int) spellId));
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL)]
        [Parser(Opcode.SMSG_PLAY_SPELL_IMPACT)]
        public static void HandleCastVisual(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("Caster GUID: " + guid);
            var visual = packet.ReadUInt32();
            Console.WriteLine("SpellVisualKit ID: " + visual);
        }

        [Parser(Opcode.SMSG_CAST_FAILED)]
        public static void HandleCastFailed(Packet packet)
        {
            var castCount = packet.ReadByte();
            Console.WriteLine("Cast count: " + castCount);

            var spellId = packet.ReadUInt32();
            Console.WriteLine("Spell ID: " + Extensions.SpellLine((int) spellId));

            var result = packet.ReadEnum<SpellCastFailureReason>("Reason", TypeCode.Byte);

            switch (result)
            {
                case SpellCastFailureReason.DontReport:
                case SpellCastFailureReason.Interrupted:
                case SpellCastFailureReason.ItemNotReady:
                case SpellCastFailureReason.LineOfSight:
                case SpellCastFailureReason.TargetsDead:
                case SpellCastFailureReason.NothingToDispel:
                case SpellCastFailureReason.TargetAurastate:
                case SpellCastFailureReason.SpellInProgress:
                case SpellCastFailureReason.OutOfRange:
                case SpellCastFailureReason.CasterDead:
                case SpellCastFailureReason.NotInControl:
                case SpellCastFailureReason.BadTargets:
                case SpellCastFailureReason.UnitNotInfront:
                case SpellCastFailureReason.Stunned:
                case SpellCastFailureReason.NoValidTargets:
                case SpellCastFailureReason.NotMounted:
                case SpellCastFailureReason.NoPower:
                case SpellCastFailureReason.NotHere:
                case SpellCastFailureReason.Moving:
                case SpellCastFailureReason.Fizzle:
                case SpellCastFailureReason.CantDoThatRightNow:
                case SpellCastFailureReason.TargetFriendly:
                    // do nothing
                    break;
                case SpellCastFailureReason.RequiresSpellFocus:
                case SpellCastFailureReason.RequiresArea:
                case SpellCastFailureReason.Totems:
                case SpellCastFailureReason.TotemCategory:
                case SpellCastFailureReason.CustomError:
                case SpellCastFailureReason.TooManyOfItem:
                case SpellCastFailureReason.NotReady:
                    var misc = packet.ReadUInt32();
                    Console.WriteLine("ID/Misc: " + misc);
                    break;
                case SpellCastFailureReason.PreventedByMechanic:
                    var mechanic = (SpellMechanics)packet.ReadUInt32();
                    Console.WriteLine("Mechanic: " + mechanic);
                    break;
                case SpellCastFailureReason.EquippedItemClass:
                    var iClass = packet.ReadUInt32();
                    var iSubClass = packet.ReadUInt32();
                    Console.WriteLine("Class: " + iClass + " subclass : " + iSubClass);
                    break;
                default:
                    Console.WriteLine("Spell failure reason not handled.");
                    if (packet.GetLength() - packet.GetPosition() != 0) // a little trick so we can Catch 'Em All. remove this later
                    {
                        var st = packet.ReadUInt32();
                        Console.WriteLine("Something: " + st);
                    }
                    break;
            }
        }

        [Parser(Opcode.SMSG_SPELLNONMELEEDAMAGELOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            packet.ReadPackedGuid("Target GUID");
            packet.ReadPackedGuid("Caster GUID");
            Console.WriteLine("Spell ID: " + Extensions.SpellLine((int)packet.ReadUInt32()));
            packet.ReadUInt32("Damage");

            if (ClientVersion.Version >= ClientVersionBuild.V3_0_3_9183)
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
            Console.WriteLine("Spell ID: " + Extensions.SpellLine((int)packet.ReadUInt32()));
            packet.ReadUInt32("Damage");

            if (ClientVersion.Version >= ClientVersionBuild.V3_0_3_9183)
                packet.ReadUInt32("Overheal");

            if (ClientVersion.Version >= ClientVersionBuild.V3_3_5_12213) // no idea when this was added exactly
                packet.ReadUInt32("Absorb");

            packet.ReadBoolean("Critical");
            packet.ReadBoolean("Debug output");
        }

        [Parser(Opcode.SMSG_SPELLENERGIZELOG)]
        public static void HandleSpellEnergizeLog(Packet packet)
        {
            packet.ReadPackedGuid("Target GUID");
            packet.ReadPackedGuid("Caster GUID");
            Console.WriteLine("Spell ID: " + Extensions.SpellLine((int)packet.ReadUInt32()));
            packet.ReadEnum<PowerType>("Power type", TypeCode.UInt32);
            packet.ReadUInt32("Amount");
        }

        [Parser(Opcode.SMSG_PROCRESIST)]
        [Parser(Opcode.SMSG_SPELLORDAMAGE_IMMUNE)]
        public static void HandleSpellProcResist(Packet packet)
        {
            packet.ReadGuid("Caster GUID");
            packet.ReadGuid("Target GUID");
            Console.WriteLine("Spell ID: " + Extensions.SpellLine((int)packet.ReadUInt32()));
            packet.ReadBoolean("Debug output");
        }

        [Parser(Opcode.SMSG_SPELLLOGMISS)]
        public static void HandleSpellLogMiss(Packet packet)
        {
            Console.WriteLine("Spell ID: " + Extensions.SpellLine((int)packet.ReadUInt32()));
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
            packet.ReadTime("Time");
        }

        [Parser(Opcode.MSG_CHANNEL_START)]
        public static void HandleSpellChannelStart(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            Console.WriteLine("Spell ID: " + Extensions.SpellLine((int)packet.ReadUInt32()));
            packet.ReadUInt32("Duration");
        }
    }
}
