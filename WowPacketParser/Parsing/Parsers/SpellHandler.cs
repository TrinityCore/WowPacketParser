using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;


namespace WowPacketParser.Parsing.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.SMSG_SEND_UNLEARN_SPELLS)]
        public static void HandleSendUnlearnSpells(Packet packet)
        {
            var count = packet.ReadInt32();
            Console.WriteLine("Count: " + count);

            for (var i = 0; i < count; i++)
            {
                var spellId = packet.ReadInt32();
                Console.WriteLine("Spell ID " + i + ": " + Extensions.SpellLine(spellId));
            }
        }

        [Parser(Opcode.SMSG_POWER_UPDATE)]
        public static void HandleSetPower(Packet packet)
        {
            var type = (PowerType)packet.ReadByte();
            Console.WriteLine("Power Type: " + type);

            var value = packet.ReadInt32();
            Console.WriteLine("Power Value: " + value);
        }

        [Parser(Opcode.SMSG_UNIT_SPELLCAST_START)]
        public static void HandleUnitSpellCastStart(Packet packet)
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

                var chr = SessionHandler.LoggedInCharacter;
                if (!chr.FirstLogin)
                    continue;

                SQLStore.WriteData(SQLStore.StartSpells.GetCommand(chr.Race, chr.Class, spellId));
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

        public static void ReadAuraUpdateBlock(Packet packet)
        {
            var slot = packet.ReadByte();
            Console.WriteLine("Slot: " + slot);

            var id = packet.ReadInt32();
            Console.WriteLine("ID: " + Extensions.SpellLine(id));

            if (id <= 0)
                return;

            var flags = (AuraFlag)packet.ReadByte();
            Console.WriteLine("Flags: " + flags);

            var level = packet.ReadByte();
            Console.WriteLine("Level: " + level);

            var charges = packet.ReadByte();
            Console.WriteLine("Charges: " + charges);

            if (!flags.HasFlag(AuraFlag.NotCaster))
            {
                var unkGuid = packet.ReadPackedGuid();
                Console.WriteLine("Caster GUID: " + unkGuid);
            }

            if (!flags.HasFlag(AuraFlag.Duration))
                return;

            var maxDura = packet.ReadInt32();
            Console.WriteLine("Max Duration: " + maxDura);

            var dura = packet.ReadInt32();
            Console.WriteLine("Duration: " + dura);
        }

        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            var pguid = packet.ReadPackedGuid();
            Console.WriteLine("GUID: " + pguid);

            ReadAuraUpdateBlock(packet);
        }

        [Parser(Opcode.SMSG_AURA_UPDATE_ALL)]
        public static void HandleAuraUpdateAll(Packet packet)
        {
            var pguid = packet.ReadPackedGuid();
            Console.WriteLine("GUID: " + pguid);

            while (!packet.IsRead())
                ReadAuraUpdateBlock(packet);
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

            if (packet.GetOpcode() == Opcode.SMSG_SPELL_GO)
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
                TargetFlag.Corpse | TargetFlag.SpellDynamic4 | TargetFlag.Item | TargetFlag.TradeItem
                | TargetFlag.SourceLocation | TargetFlag.DestinationLocation))
            {
                var tGuid = packet.ReadPackedGuid();
                Console.WriteLine("Target GUID: " + tGuid);
            }

            if (targetFlags.HasAnyFlag(TargetFlag.SourceLocation | TargetFlag.DestinationLocation))
            {
                var pos = packet.ReadVector3();
                Console.WriteLine("Position: " + pos);
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

            if (packet.GetOpcode() == Opcode.SMSG_SPELL_GO)
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

            if (packet.GetOpcode() == Opcode.SMSG_SPELL_GO)
            {
                if (flags.HasFlag(CastFlag.VisualChain))
                {
                    var unk5 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 2: " + unk5);

                    var unk6 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 3: " + unk6);
                }
            }

            if (packet.GetOpcode() == Opcode.SMSG_SPELL_START)
            {
                if (flags.HasFlag(CastFlag.Immunity))
                {
                    var unk4 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 4: " + unk4);

                    var unk5 = packet.ReadInt32();
                    Console.WriteLine("Unk Int32 5: " + unk5);
                }
            }

            if (packet.GetOpcode() != Opcode.SMSG_SPELL_GO)
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
                    var powerType = packet.ReadUInt32();
                    Console.WriteLine("Power type: " + powerType);

                    var damage = packet.ReadUInt32();
                    Console.WriteLine("Damage: " + damage);

                    break;
                }
                case AuraType.PeriodicManaLeech:
                {
                    var powerType = packet.ReadUInt32();
                    Console.WriteLine("Power type: " + powerType);

                    var amount = packet.ReadUInt32();
                    Console.WriteLine("Amount: " + amount);

                    var multiplier = packet.ReadSingle();
                    Console.WriteLine("Gain multiplier: " + multiplier);

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
        public static void HandleRemovedSpell(Packet packet)
        {
            var spellId = packet.ReadUInt32();
            Console.WriteLine("Spell ID: " + Extensions.SpellLine((int) spellId));
        }

        [Parser(Opcode.SMSG_CAST_FAILED)]
        public static void HandleCastFailed(Packet packet)
        {
            var castCount = packet.ReadByte();
            Console.WriteLine("Cast count: " + castCount);

            var spellId = packet.ReadUInt32();
            Console.WriteLine("Spell ID: " + Extensions.SpellLine((int) spellId));

            var result = (SpellCastFailureReason) packet.ReadByte();
            Console.WriteLine("Reason: " + result);

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
    }
}
