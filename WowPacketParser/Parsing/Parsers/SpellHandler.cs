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
                var spellId = packet.ReadInt32();
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

            var slot = packet.ReadByte();
            Console.WriteLine("Slot: " + slot);
            aura.Slot = slot;

            var id = packet.ReadInt32();
            Console.WriteLine("ID: " + Extensions.SpellLine(id));
            if (id <= 0)
                return null;
            aura.SpellId = (uint)id;

            var flags = (AuraFlag)packet.ReadByte();
            Console.WriteLine("Flags: " + flags);
            aura.AuraFlags = flags;

            var level = packet.ReadByte();
            Console.WriteLine("Level: " + level);
            aura.Level = level;

            var charges = packet.ReadByte();
            Console.WriteLine("Charges: " + charges);
            aura.Charges = charges;

            if (!flags.HasFlag(AuraFlag.NotCaster))
            {
                var unkGuid = packet.ReadPackedGuid();
                Console.WriteLine("Caster GUID: " + unkGuid);
                aura.CasterGuid = unkGuid;
            }
            else
                aura.CasterGuid = new Guid(); // Is this needed?

            if (flags.HasFlag(AuraFlag.Duration))
            {
                var maxDura = packet.ReadInt32();
                Console.WriteLine("Max Duration: " + maxDura);
                aura.MaxDuration = maxDura;

                var dura = packet.ReadInt32();
                Console.WriteLine("Duration: " + dura);
                aura.Duration = dura;
            }
            else // Is this needed?
            {
                aura.MaxDuration = 0;
                aura.Duration = 0;
            }

            return aura;
        }

        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            var pguid = packet.ReadPackedGuid();
            Console.WriteLine("GUID: " + pguid);

            ReadAuraUpdateBlock(packet);
            // TODO: Add this aura to a list of objects (searching by guid)
        }

        [Parser(Opcode.SMSG_AURA_UPDATE_ALL)]
        public static void HandleAuraUpdateAll(Packet packet)
        {
            var pguid = packet.ReadPackedGuid();
            Console.WriteLine("GUID: " + pguid);

            /*var aura =*/ new Aura();
            while (packet.CanRead())
                /*aura =*/ ReadAuraUpdateBlock(packet);
            // TODO: Add this aura to a list of objects (searching by guid)
        }

        [Parser(Opcode.SMSG_SPELL_START)]
        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellStart(Packet packet)
        {
            var casterGuid = packet.ReadPackedGuid();
            Console.WriteLine("Caster GUID: " + casterGuid);

            var casterUnit = packet.ReadPackedGuid();
            Console.WriteLine("Caster Unit GUID: " + casterUnit);

            var count = packet.ReadByte();
            Console.WriteLine("Cast Count: " + count);

            var spellId = packet.ReadInt32();
            Console.WriteLine("Spell ID: " + Extensions.SpellLine(spellId));

            var flags = (CastFlag)packet.ReadInt32();
            Console.WriteLine("Cast Flags: " + flags);

            var time = packet.ReadInt32();
            Console.WriteLine("Time: " + time);

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_SPELL_GO))
            {
                var hitCount = packet.ReadByte();
                Console.WriteLine("Hit Count: " + hitCount);

                for (var i = 0; i < hitCount; i++)
                {
                    var hitGuid = packet.ReadGuid();
                    Console.WriteLine("Hit GUID " + i + ": " + hitGuid);
                }

                var missCount = packet.ReadByte();
                Console.WriteLine("Miss Count: " + missCount);

                for (var i = 0; i < missCount; i++)
                {
                    var missGuid = packet.ReadGuid();
                    Console.WriteLine("Miss GUID " + i + ": " + missGuid);

                    var missType = (SpellMissType)packet.ReadByte();
                    Console.WriteLine("Miss Type " + i + ": " + missType);

                    if (missType != SpellMissType.Reflect)
                        continue;

                    var reflectResult = (SpellMissType)packet.ReadByte();
                    Console.WriteLine("Miss Reflect " + i + ": " + reflectResult);
                }
            }

            var targetFlags = (TargetFlag)packet.ReadInt32();
            Console.WriteLine("Target Flags: " + targetFlags);

            if (targetFlags.HasAnyFlag(TargetFlag.Unit | TargetFlag.PvpCorpse | TargetFlag.Object |
                TargetFlag.Corpse | TargetFlag.SpellDynamic4))
            {
                var tGuid = packet.ReadPackedGuid();
                Console.WriteLine("Target GUID: " + tGuid);
            }

            if (targetFlags.HasAnyFlag(TargetFlag.Item | TargetFlag.TradeItem))
            {
                var tGuid = packet.ReadPackedGuid();
                Console.WriteLine("Item Target GUID: " + tGuid);
            }

            if (targetFlags.HasFlag(TargetFlag.SourceLocation))
            {
                var tGuid = packet.ReadPackedGuid();
                Console.WriteLine("Source Transport GUID: " + tGuid);
                var pos = packet.ReadVector3();
                Console.WriteLine("Source Position: " + pos);
            }

            if (targetFlags.HasFlag(TargetFlag.DestinationLocation))
            {
                var tGuid = packet.ReadPackedGuid();
                Console.WriteLine("Destination Transport GUID: " + tGuid);
                var pos = packet.ReadVector3();
                Console.WriteLine("Destination Position: " + pos);
            }

            if (targetFlags.HasFlag(TargetFlag.NameString))
            {
                var targetStr = packet.ReadCString();
                Console.WriteLine("Target String: " + targetStr);
            }

            if (flags.HasFlag(CastFlag.PredictedPower))
            {
                var runeCooldown = packet.ReadInt32();
                Console.WriteLine("Rune Cooldown: " + runeCooldown);
            }

            if (flags.HasFlag(CastFlag.RuneInfo))
            {
                var spellRuneState = packet.ReadByte();
                Console.WriteLine("Spell Rune State: " + spellRuneState);

                var playerRuneState = packet.ReadByte();
                Console.WriteLine("Player Rune State: " + playerRuneState);

                for (var i = 0; i < 6; i++)
                {
                    var mask = 1 << i;
                    if ((mask & spellRuneState) == 0)
                        continue;

                    if ((mask & playerRuneState) != 0)
                        continue;

                    var unk = packet.ReadByte();
                    Console.WriteLine("Unk Byte 1 " + i + ": " + unk);
                }
            }

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_SPELL_GO))
            {
                if (flags.HasFlag(CastFlag.AdjustMissile))
                {
                    var unk1 = packet.ReadSingle();
                    Console.WriteLine("Unk Single: " + unk1);

                    var unk2 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 1: " + unk2);
                }
            }

            if (flags.HasFlag(CastFlag.Projectile))
            {
                var ammoDispId = packet.ReadInt32();
                Console.WriteLine("Ammo Display ID: " + ammoDispId);

                var ammoInvType = (InventoryType)packet.ReadInt32();
                Console.WriteLine("Ammo Inventory Type: " + ammoInvType);
            }

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_SPELL_GO))
            {
                if (flags.HasFlag(CastFlag.VisualChain))
                {
                    var unk5 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 2: " + unk5);

                    var unk6 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 3: " + unk6);
                }
            }

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_SPELL_START))
            {
                if (flags.HasFlag(CastFlag.Immunity))
                {
                    var unk4 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 4: " + unk4);

                    var unk5 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 5: " + unk5);
                }
            }

            if (packet.Opcode != Opcodes.GetOpcode(Opcode.SMSG_SPELL_GO))
                return;

            if (targetFlags.HasFlag(TargetFlag.DestinationLocation))
            {
                var unkByte = packet.ReadByte();
                Console.WriteLine("Unk Byte 2: " + unkByte);
            }

            if (!targetFlags.HasFlag(TargetFlag.ExtraTargets))
                return;

            var unkInt = packet.ReadInt32();
            Console.WriteLine("Extra Targets Count: " + unkInt);

            for (var i = 0; i < unkInt; i++)
            {
                var pos = packet.ReadVector3();
                Console.WriteLine("Extra Target Position " + i + ": " + pos);

                var guid = packet.ReadGuid();
                Console.WriteLine("Extra Target GUID " + i + ": " + guid);
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

                    var overDamage = packet.ReadUInt32();
                    Console.WriteLine("Over damage: " + overDamage);

                    var spellProto = packet.ReadUInt32();
                    Console.WriteLine("Spell proto: " + spellProto);

                    var absorb = packet.ReadUInt32();
                    Console.WriteLine("Absorb: " + absorb);

                    var resist = packet.ReadUInt32();
                    Console.WriteLine("Resist: " + resist);

                    var critical = packet.ReadByte();
                    Console.WriteLine("Critical: " + critical);

                    break;
                }
                case AuraType.PeriodicHeal:
                case AuraType.ObsModHealth:
                {
                    var damage = packet.ReadUInt32();
                    Console.WriteLine("Damage: " + damage);

                    var overDamage = packet.ReadUInt32();
                    Console.WriteLine("Over damage: " + overDamage);

                    var absorb = packet.ReadUInt32();
                    Console.WriteLine("Absorb: " + absorb);

                    var critical = packet.ReadByte();
                    Console.WriteLine("Critical: " + critical);

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
            packet.ReadUInt32("Overkill");
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

    }
}
