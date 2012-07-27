using System;
using System.Collections.Generic;
using System.Globalization;
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
            packet.ReadEntryWithName<UInt32>(StoreNameType.Area, "Area ID");
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

        [Parser(Opcode.SMSG_CONVERT_RUNE)]
        public static void HandleConvertRune(Packet packet)
        {
            packet.ReadByte("Index");
            packet.ReadByte("New Rune Type");
        }

        [Parser(Opcode.SMSG_ADD_RUNE_POWER)]
        public static void HandleAddRunePower(Packet packet)
        {
            packet.ReadUInt32("Mask?"); // TC: 1 << index
        }

        [Parser(Opcode.SMSG_COOLDOWN_EVENT)]
        public static void HandleCooldownEvent(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_NOTIFY_DEST_LOC_SPELL_CAST)]
        public static void HandleNotifyDestLocSpellCast(Packet packet)
        {
            // TODO: Verify and/or finish this
            // Everything is guessed
            packet.ReadGuid("Caster GUID");
            packet.ReadGuid("Target GUID");
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
            packet.ReadVector3("Position");
            packet.ReadVector3("Target Position");
            packet.ReadSingle("Evelation");
            packet.ReadSingle("Speed");
            packet.ReadUInt32("Duration");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_3_5a_12340))
                packet.ReadInt32("Unk");

            packet.ReadSingle("Unk");
        }

        [Parser(Opcode.SMSG_WEEKLY_SPELL_USAGE)]
        public static void HandleWeeklySpellUsage(Packet packet)
        {
            var count = packet.ReadBits("Count", 23);

            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt32("Unk Int32");
                packet.ReadByte("Unk Int8");
            }
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
            var spells = new List<uint>(count);
            for (var i = 0; i < count; i++)
            {
                uint spellId;
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                    spellId = (uint) packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);
                else
                    spellId = (uint) packet.ReadEntryWithName<UInt16>(StoreNameType.Spell, "Spell ID", i);

                packet.ReadInt16("Unk Int16", i);

                spells.Add(spellId);
            }

            var startSpell = new StartSpell();
            startSpell.Spells = spells;

            if (SessionHandler.LoggedInCharacter != null && SessionHandler.LoggedInCharacter.FirstLogin)
                Storage.StartSpells.Add(new Tuple<Race, Class>(SessionHandler.LoggedInCharacter.Race, SessionHandler.LoggedInCharacter.Class), startSpell, packet.TimeSpan);

            var cooldownCount = packet.ReadInt16("Cooldown Count");
            for (var i = 0; i < cooldownCount; i++)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                    packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Cooldown Spell ID", i);
                else
                    packet.ReadEntryWithName<UInt16>(StoreNameType.Spell, "Cooldown Spell ID", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                    packet.ReadInt32("Cooldown Cast Item ID", i);
                else
                    packet.ReadInt16("Cooldown Cast Item ID", i);

                packet.ReadInt16("Cooldown Spell Category", i);
                packet.ReadInt32("Cooldown Time", i);
                var catCd = packet.ReadUInt32();
                packet.WriteLine("[{0}] Cooldown Category Time: {1}", i, ((catCd >> 31) != 0 ? "Infinite" : (catCd & 0x7FFFFFFF).ToString(CultureInfo.InvariantCulture)));
            }
        }

        private static Aura ReadAuraUpdateBlock(ref Packet packet)
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

            aura.CasterGuid = !aura.AuraFlags.HasAnyFlag(AuraFlag.NotCaster) ? packet.ReadPackedGuid("Caster GUID") : new Guid();

            if (aura.AuraFlags.HasAnyFlag(AuraFlag.Duration))
            {
                aura.MaxDuration = packet.ReadInt32("Max Duration");
                aura.Duration = packet.ReadInt32("Duration");
            }
            else
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

            packet.AddSniffData(StoreNameType.Spell, (int)aura.SpellId, "AURA_UPDATE");

            return aura;
        }

        [Parser(Opcode.SMSG_AURA_UPDATE_ALL)]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            var guid = packet.ReadPackedGuid("GUID");

            var auras = new List<Aura>();
            while (packet.CanRead())
            {
                var aura = ReadAuraUpdateBlock(ref packet);
                auras.Add(aura);
            }

            // This only works if the parser saw UPDATE_OBJECT before this packet
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
                    else if (unit.AddedAuras == null)
                        unit.AddedAuras = new List<List<Aura>> { auras };
                    else
                        unit.AddedAuras.Add(auras);
                }
            }
        }

        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            packet.ReadByte("Cast Count");
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");

            if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
                packet.ReadInt32("Glyph Index");

            var castFlags = packet.ReadEnum<CastFlag>("Cast Flags", TypeCode.Byte);
            ReadSpellCastTargets(ref packet);
            if (castFlags.HasAnyFlag(CastFlag.Unknown1))
                HandleSpellMissileAndMove(ref packet);
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

        public static void HandleSpellMissileAndMove(ref Packet packet) // 4.3.4
        {
            packet.ReadSingle("Elevation");
            packet.ReadSingle("Missile speed");
            var hasMovement = packet.ReadBoolean("Has Movement Data");
            if (hasMovement)
            {
                var guid = new byte[8];
                var transportGuid = new byte[8];
                var hasTransTime2 = false;
                var hasTransTime3 = false;
                var hasFallDirection = false;
                var pos = new Vector4();

                pos.Z = packet.ReadSingle();
                pos.Y = packet.ReadSingle();
                pos.X = packet.ReadSingle();

                var bytes = new byte[8];
                var hasFallData = packet.ReadBit("Has fall data");
                var hasTime = !packet.ReadBit("Has timestamp");
                var hasO = !packet.ReadBit("Has O");
                packet.ReadBit("Has Spline");
                packet.ReadBit();
                guid[6] = bytes[4] = packet.ReadBit().ToByte(); //6
                guid[4] = bytes[5] = packet.ReadBit().ToByte(); //4
                var hasMovementFlags2 = !packet.ReadBit();
                var bytes2 = new byte[8];
                guid[3] = bytes2[0] = packet.ReadBit().ToByte(); //3
                guid[5] = bytes2[1] = packet.ReadBit().ToByte(); //5
                var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
                var hasPitch = !packet.ReadBit("Has Pitch");
                guid[7] = bytes2[6] = packet.ReadBit().ToByte(); //7
                var hasTrans = packet.ReadBit("Has transport");
                guid[2] = bytes2[5] = packet.ReadBit().ToByte(); //2
                var hasMovementFlags = !packet.ReadBit();
                guid[1] = packet.ReadBit().ToByte();
                guid[0] = packet.ReadBit().ToByte();
                if (hasTrans)
                {
                    transportGuid[6] = packet.ReadBit().ToByte();//54
                    transportGuid[2] = packet.ReadBit().ToByte();//50
                    transportGuid[5] = packet.ReadBit().ToByte();//53
                    hasTransTime2 = packet.ReadBit();
                    transportGuid[7] = packet.ReadBit().ToByte();//55
                    transportGuid[4] = packet.ReadBit().ToByte();//52
                    hasTransTime3 = packet.ReadBit();
                    transportGuid[0] = packet.ReadBit().ToByte();//48
                    transportGuid[1] = packet.ReadBit().ToByte();//49
                    transportGuid[3] = packet.ReadBit().ToByte();//51
                }

                if (hasMovementFlags2)
                    packet.ReadEnum<MovementFlagExtra>("Movement flags extra", 12);

                if (hasMovementFlags)
                    packet.ReadEnum<MovementFlag>("Movement flags", 30);

                if (hasFallData)
                    hasFallDirection = packet.ReadBit("hasFallDirection");

                packet.ParseBitStream(guid, 1, 4, 7, 3, 0, 2, 5, 6);

                if (hasTrans)
                {
                    var tpos = new Vector4();
                    packet.ReadSByte("Transport seat");
                    tpos.O = packet.ReadSingle();
                    packet.ReadUInt32("Transport time");

                    packet.ParseBitStream(transportGuid, 6, 5);

                    if (hasTransTime2)
                        packet.ReadUInt32("Transport time 2");

                    tpos.X = packet.ReadSingle();

                    packet.ParseBitStream(transportGuid, 4);

                    tpos.Z = packet.ReadSingle();

                    packet.ParseBitStream(transportGuid, 2, 0);

                    if (hasTransTime3)
                        packet.ReadUInt32("Transport time 3");

                    packet.ParseBitStream(transportGuid, 1, 3);

                    tpos.Y = packet.ReadSingle();

                    packet.ParseBitStream(transportGuid, 7);

                    packet.WriteGuid("Transport Guid", transportGuid);
                    packet.WriteLine("Transport Position: {0}", tpos);
                }

                if (hasO)
                    pos.O = packet.ReadSingle();

                if (hasSplineElev)
                    packet.ReadSingle("Spline elevation");

                if (hasFallData)
                {
                    packet.ReadUInt32("Fall time");
                    if (hasFallDirection)
                    {
                        packet.ReadSingle("Fall Cos");
                        packet.ReadSingle("Fall Sin");
                        packet.ReadSingle("Horizontal Speed");
                    }
                    packet.ReadSingle("Vertical Speed");
                }

                if (hasTime)
                    packet.ReadUInt32("Timestamp");

                if (hasPitch)
                    packet.ReadSingle("Pitch");

                packet.WriteGuid("Guid", guid);
                packet.WriteLine("Position: {0}", pos);
            }
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

            var spellId = packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_2_9056) && !isSpellGo)
                packet.ReadByte("Cast Count");

            var flagsTypeCode = ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056) ? TypeCode.Int32 : TypeCode.UInt16;
            var flags = packet.ReadEnum<CastFlag>("Cast Flags", flagsTypeCode);

            packet.ReadUInt32("Time");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                packet.ReadUInt32("Time2");

            if (isSpellGo)
            {
                var hitCount = packet.ReadByte("Hit Count");
                for (var i = 0; i < hitCount; i++)
                    packet.ReadGuid("Hit GUID", i);

                var missCount = packet.ReadByte("Miss Count");
                for (var i = 0; i < missCount; i++)
                {
                    var missGuid = packet.ReadGuid("Miss GUID", i);
                    packet.WriteLine("Miss GUID " + i + ": " + missGuid);

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
                        if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_2_14545))
                        {
                            var mask = 1 << i;
                            if ((mask & spellRuneState) == 0)
                                continue;

                            if ((mask & playerRuneState) != 0)
                                continue;
                        }

                        packet.ReadByte("Rune Cooldown Passed", i);
                    }
                }

                if (isSpellGo)
                {
                    if (flags.HasAnyFlag(CastFlag.AdjustMissile))
                    {
                        packet.ReadSingle("Evelation");
                        packet.ReadInt32("Delay time");
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
                        packet.ReadSByte("Unk Byte 2"); // Some count

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
                        packet.ReadInt32("CastSchoolImmunities");
                        packet.ReadInt32("CastImmunities");
                    }

                    if (flags.HasAnyFlag(CastFlag.HealPrediction))
                    {
                        packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Predicted Spell ID");

                        if (packet.ReadByte("Unk Byte") == 2)
                            packet.ReadPackedGuid("Unk Guid");
                    }
                }
            }

            if (isSpellGo)
                packet.AddSniffData(StoreNameType.Spell, spellId, "SPELL_GO");
        }

        [Parser(Opcode.SMSG_LEARNED_SPELL)]
        public static void HandleLearnedSpell(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                    packet.ReadInt32("Unk Int32");
                else
                    packet.ReadInt16("Unk Int16");
            }
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

        [Parser(Opcode.SMSG_REMOVED_SPELL, ClientVersionBuild.Zero, ClientVersionBuild.V3_1_0_9767)]
        public static void HandleRemovedSpell(Packet packet)
        {
            packet.ReadEntryWithName<UInt16>(StoreNameType.Spell, "Spell ID");
        }

        [Parser(Opcode.CMSG_CANCEL_AURA)]
        [Parser(Opcode.SMSG_REMOVED_SPELL, ClientVersionBuild.V3_1_0_9767)]
        [Parser(Opcode.CMSG_CANCEL_CHANNELLING)]
        public static void HandleRemovedSpell2(Packet packet)
        {
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_PLAY_SPELL_IMPACT)]
        public static void HandleCastVisual(Packet packet)
        {
            packet.ReadGuid("Caster GUID");
            packet.ReadUInt32("SpellVisualKit ID");
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleCastVisual434(Packet packet)
        {
            packet.ReadSingle("Z");
            packet.ReadInt32("SpellVisualKit ID"); // not confirmed
            packet.ReadInt16("Unk Int16 1"); // usually 0
            packet.ReadSingle("O");
            packet.ReadSingle("X");
            packet.ReadInt16("Unk Int16 2"); // usually 0
            packet.ReadSingle("Y");

            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[1] = (byte)(packet.ReadBit() ? 1 : 0);
            guid1[3] = (byte)(packet.ReadBit() ? 1 : 0);
            guid1[0] = (byte)(packet.ReadBit() ? 1 : 0);
            guid2[2] = (byte)(packet.ReadBit() ? 1 : 0);
            guid2[5] = (byte)(packet.ReadBit() ? 1 : 0);
            guid1[2] = (byte)(packet.ReadBit() ? 1 : 0);
            guid1[4] = (byte)(packet.ReadBit() ? 1 : 0);
            guid2[6] = (byte)(packet.ReadBit() ? 1 : 0);

            guid2[0] = (byte)(packet.ReadBit() ? 1 : 0);
            packet.ReadBit("Unk Bit"); // usually 1
            guid1[6] = (byte)(packet.ReadBit() ? 1 : 0);
            guid2[7] = (byte)(packet.ReadBit() ? 1 : 0);
            guid1[5] = (byte)(packet.ReadBit() ? 1 : 0);
            guid1[1] = (byte)(packet.ReadBit() ? 1 : 0);
            guid1[7] = (byte)(packet.ReadBit() ? 1 : 0);
            guid2[3] = (byte)(packet.ReadBit() ? 1 : 0);

            guid2[4] = (byte)(packet.ReadBit() ? 1 : 0);

            if (guid1[7] != 0) guid1[7] ^= packet.ReadByte();
            if (guid1[4] != 0) guid1[4] ^= packet.ReadByte();
            if (guid2[7] != 0) guid2[7] ^= packet.ReadByte();
            if (guid1[1] != 0) guid1[1] ^= packet.ReadByte();
            if (guid1[3] != 0) guid1[3] ^= packet.ReadByte();
            if (guid1[0] != 0) guid1[0] ^= packet.ReadByte();
            if (guid1[6] != 0) guid1[6] ^= packet.ReadByte();
            if (guid2[0] != 0) guid2[0] ^= packet.ReadByte();
            if (guid2[4] != 0) guid2[4] ^= packet.ReadByte();
            if (guid1[5] != 0) guid1[5] ^= packet.ReadByte();
            if (guid2[1] != 0) guid2[1] ^= packet.ReadByte();
            if (guid2[5] != 0) guid2[5] ^= packet.ReadByte();
            if (guid2[6] != 0) guid2[6] ^= packet.ReadByte();
            if (guid2[2] != 0) guid2[2] ^= packet.ReadByte();
            if (guid1[2] != 0) guid1[2] ^= packet.ReadByte();
            if (guid2[3] != 0) guid2[3] ^= packet.ReadByte();

            packet.WriteLine("Caster GUID1 {0}", new Guid(BitConverter.ToUInt64(guid1, 0))); // not confirmed
            packet.WriteLine("Unk GUID2 {0}", new Guid(BitConverter.ToUInt64(guid2, 0))); // usually 0
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL_KIT)] // 4.3.4
        public static void HandleCastVisualKit(Packet packet)
        {
            packet.ReadUInt32("Unk");
            packet.ReadUInt32("SpellVisualKit ID");
            packet.ReadUInt32("Unk");

            var guid = new byte[8];
            guid[4] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[7] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[5] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[3] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[1] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[2] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[0] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[6] = (byte)(packet.ReadBit() ? 1 : 0);

            if (guid[0] != 0) guid[0] ^= packet.ReadByte();
            if (guid[4] != 0) guid[4] ^= packet.ReadByte();
            if (guid[1] != 0) guid[1] ^= packet.ReadByte();
            if (guid[6] != 0) guid[6] ^= packet.ReadByte();
            if (guid[7] != 0) guid[7] ^= packet.ReadByte();
            if (guid[2] != 0) guid[2] ^= packet.ReadByte();
            if (guid[3] != 0) guid[3] ^= packet.ReadByte();
            if (guid[5] != 0) guid[5] ^= packet.ReadByte();

            packet.WriteLine("Caster GUID {0}", new Guid(BitConverter.ToUInt64(guid, 0)));
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
                case SpellCastFailureReason.NeedExoticAmmo:
                    if (packet.CanRead())
                        packet.ReadUInt32("Item SubclassMask");
                    break;
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
                    if (packet.CanRead())
                        packet.ReadUInt32("Reagent ID");
                    break;
                case SpellCastFailureReason.RequiresSpellFocus:
                    if (packet.CanRead())
                        packet.ReadUInt32("Spell Focus");
                    break;
                case SpellCastFailureReason.RequiresArea:
                    if (packet.CanRead())
                        packet.ReadEntryWithName<UInt32>(StoreNameType.Area, "Area ID");
                    break;
                case SpellCastFailureReason.CustomError:
                    if (packet.CanRead())
                        packet.ReadUInt32("Error ID");
                    break;
                case SpellCastFailureReason.PreventedByMechanic:
                    if (packet.CanRead())
                        packet.ReadEnum<SpellMechanic>("Mechanic", TypeCode.UInt32);
                    break;
                case SpellCastFailureReason.EquippedItemClass:
                case SpellCastFailureReason.EquippedItemClassMainhand:
                case SpellCastFailureReason.EquippedItemClassOffhand:
                    if (packet.CanRead())
                        packet.ReadEnum<ItemClass>("Class", TypeCode.UInt32);
                    if (packet.CanRead())
                        packet.ReadUInt32("SubclassMask");
                    break;
                case SpellCastFailureReason.MinSkill:
                    if (packet.CanRead())
                        packet.ReadUInt32("Skill Type"); // SkillLine.dbc
                    if (packet.CanRead())
                        packet.ReadUInt32("Required Amount");
                    break;
                case SpellCastFailureReason.FishingTooLow:
                    if (packet.CanRead())
                        packet.ReadUInt32("Required fishing skill");
                    break;
                // Following is post 3.3.5a
                case SpellCastFailureReason.NotReady:
                case SpellCastFailureReason.Silenced:
                case SpellCastFailureReason.NotStanding:
                    if (packet.CanRead())
                        packet.ReadInt32("Unk");
                    break;
                default:
                    if (packet.CanRead())
                        packet.ReadUInt32("Unknown1");
                    if (packet.CanRead())
                        packet.ReadUInt32("Unknown2");
                    break;
            }
        }

        [Parser(Opcode.SMSG_SPELL_FAILURE)]
        [Parser(Opcode.SMSG_SPELL_FAILED_OTHER)]
        public static void HandleSpellFailedOther(Packet packet)
        {
            packet.ReadPackedGuid("Guid");
            packet.ReadByte("Cast count");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ReadEnum<SpellCastFailureReason>("Reason", TypeCode.Byte);
        }

        [Parser(Opcode.SMSG_SPELLINSTAKILLLOG)]
        public static void HandleSpellInstakillLog(Packet packet)
        {
            packet.ReadGuid("Target GUID");
            packet.ReadGuid("Caster GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
        }

        [Parser(Opcode.SMSG_SPELLORDAMAGE_IMMUNE)]
        [Parser(Opcode.SMSG_PROCRESIST)]
        public static void HandleSpellProcResist(Packet packet)
        {
            packet.ReadGuid("Caster GUID");
            packet.ReadGuid("Target GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ReadBoolean("Unk bool");

            // not found in 3.3.5a sniff nor 3.3.3a
            //if (ClientVersion.Build == ClientVersionBuild.V3_3_5a_12340) // blizzard retardness, client doesn't even read this
            //{
            //    packet.ReadSingle("Unk");
            //    packet.ReadSingle("Unk");
            //}
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

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_0_6a_13623))
                return;

            var unkbool = packet.ReadBoolean("Has castflag Immunity"); // has castflag Immunity

            if (unkbool)
            {
                packet.ReadUInt32("CastSchoolImmunities");
                packet.ReadUInt32("CastImmunities");
            }

            var unkbool2 = packet.ReadBoolean("Has castflag HealPrediction");   // has castflag HealPrediction

            if (unkbool2)
            {
                packet.ReadPackedGuid("Target GUID");
                packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
                var unkbyte = packet.ReadByte("Unk");
                if (unkbyte == 2)
                    packet.ReadPackedGuid("Unk GUID");
            }
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
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                packet.ReadPackedGuid("GUID");
            packet.ReadUInt32("Result"); // FIXME Enum?
        }

        [Parser(Opcode.SMSG_CLEAR_TARGET)]
        [Parser(Opcode.CMSG_GET_MIRRORIMAGE_DATA)]
        [Parser(Opcode.SMSG_SPIRIT_HEALER_CONFIRM)]
        public static void HandleClearTarget(Packet packet)
        {
            packet.ReadGuid("GUID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595))
                packet.ReadUInt32("Unk Uint32");
        }

        [Parser(Opcode.SMSG_MIRRORIMAGE_DATA)]
        public static void HandleMirrorImageData(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Display ID");
            var race = packet.ReadEnum<Race>("Race", TypeCode.Byte);

            if (race == Race.None)
                return;

            packet.ReadEnum<Gender>("Gender", TypeCode.Byte);
            packet.ReadEnum<Class>("Class", TypeCode.Byte);

            if (!packet.CanRead())
                return;

            packet.ReadByte("Skin");
            packet.ReadByte("Face");
            packet.ReadByte("Hair Style");
            packet.ReadByte("Hair Color");
            packet.ReadByte("Facial Hair");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.ReadGuid("Unk");
            else
                packet.ReadUInt32("Unk");

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

        [Parser(Opcode.SMSG_SPELL_COOLDOWN)]
        public static void HandleSpellCooldown(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByte("Unk mask");
            while (packet.CanRead())
            {
                packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
                packet.ReadInt32("Time");
            }
        }

        [Parser(Opcode.SMSG_SET_FLAT_SPELL_MODIFIER, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        [Parser(Opcode.SMSG_SET_PCT_SPELL_MODIFIER, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleSetSpellModifier(Packet packet)
        {
            packet.ReadByte("Spell Mask bitpos");
            packet.ReadEnum<SpellModOp>("Spell Mod", TypeCode.Byte);
            packet.ReadInt32("Amount");
        }

        [Parser(Opcode.SMSG_SET_PCT_SPELL_MODIFIER, ClientVersionBuild.V4_0_6_13596)]
        [Parser(Opcode.SMSG_SET_FLAT_SPELL_MODIFIER, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleSetSpellModifierFlat406(Packet packet)
        {
            var modCount = packet.ReadUInt32("Modifier type count");
            for (var j = 0; j < modCount; ++j)
            {
                var modTypeCount = packet.ReadUInt32("Count", j);
                packet.ReadEnum<SpellModOp>("Spell Mod", TypeCode.Byte, j);
                for (var i = 0; i < modTypeCount; ++i)
                {
                    packet.ReadByte("Spell Mask bitpos", j, i);
                    packet.ReadSingle("Amount", j, i);
                }
            }
        }

        [Parser(Opcode.SMSG_DISPEL_FAILED)]
        public static void HandleDispelFailed(Packet packet)
        {
            packet.ReadGuid("Caster GUID");
            packet.ReadGuid("Target GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Dispelling Spell ID");

            for (var i = 0; packet.CanRead(); i++)
                packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Dispelled Spell ID", i);
        }

        [Parser(Opcode.CMSG_TOTEM_DESTROYED)]
        public static void HandleTotemDestroyed(Packet packet)
        {
            packet.ReadByte("Slot");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005)) // guessing, present on 4.3.4
                packet.ReadGuid("Unk");
        }

        [Parser(Opcode.SMSG_TOTEM_CREATED)]
        public static void HandleTotemCreated(Packet packet)
        {
            packet.ReadByte("Slot");
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Duration");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell Id");
        }

        [Parser(Opcode.CMSG_CANCEL_CAST)]
        public static void HandlePlayerCancelCast(Packet packet)
        {
            packet.ReadByte("Count");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell Id");
        }

        [Parser(Opcode.SMSG_UNIT_SPELLCAST_START)]
        public static void HandleUnitSpellcastStart(Packet packet)
        {
            packet.ReadPackedGuid("Caster GUID");
            packet.ReadPackedGuid("Target GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell Id");
            packet.ReadInt32("Time Casted");
            packet.ReadInt32("Cast time");

            if (packet.ReadBoolean("Unknown bool"))
            {
                packet.ReadUInt32("Unk");
                packet.ReadUInt32("Unk");
            }
        }

        [Parser(Opcode.SMSG_SPELL_DELAYED)]
        public static void HandleSpellDelayed(Packet packet)
        {
            packet.ReadPackedGuid("Caster GUID");
            packet.ReadInt32("Delay Time");
        }

        [Parser(Opcode.SMSG_SPELL_UPDATE_CHAIN_TARGETS)]
        public static void HandleUpdateChainTargets(Packet packet)
        {
            packet.ReadGuid("Caster GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            var count = packet.ReadInt32("Count");
            for (var i = 0; i < count; i++)
                packet.ReadGuid("Chain target");
        }

        [Parser(Opcode.SMSG_AURACASTLOG)]
        public static void HandleAuraCastLog(Packet packet)
        {
            packet.ReadGuid("Caster GUID");
            packet.ReadGuid("Target GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ReadSingle("Unk 1");
            packet.ReadSingle("Unk 2");
        }


        [Parser(Opcode.SMSG_SPELL_CHANCE_PROC_LOG)]
        public static void HandleChanceProcLog(Packet packet)
        {
            packet.ReadGuid("Caster GUID");
            packet.ReadGuid("Target GUID");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ReadSingle("unk1");
            packet.ReadSingle("unk2");
            packet.ReadEnum<UnknownFlags>("ProcFlags", TypeCode.UInt32);
            packet.ReadEnum<UnknownFlags>("Flags2", TypeCode.UInt32);
        }

        [Parser(Opcode.SMSG_SPELL_CATEGORY_COOLDOWN)]
        public static void HandleSpellCategoryCooldown(Packet packet)
        {
            var count = packet.ReadBits("Count", 23);

            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt32("Category Cooldown");
                packet.ReadInt32("Cooldown");
            }
        }

        [Parser(Opcode.SMSG_AURA_POINTS_DEPLETED)]
        public static void HandleAuraPointsDepleted(Packet packet)
        {
            var guid = packet.StartBitStream(2, 4, 1, 7, 5, 0, 3, 6);

            packet.ParseBitStream(guid, 5, 0);
            var points = packet.ReadByte();
            packet.ParseBitStream(guid, 3, 7, 4, 2);
            var slot = packet.ReadByte();
            packet.ParseBitStream(guid, 6, 1);
            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Slot ID?: {0}", slot);
            packet.WriteLine("Points?: {0}", points);

        }
    }
}
